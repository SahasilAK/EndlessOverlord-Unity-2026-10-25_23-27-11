using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    /// <summary>Real-time combat integration tests in the actual GameArena scene, driven by real Input System key events.</summary>
    public class CombatPlayModeTests
    {
        private Keyboard _keyboard;
        private ArenaController _arena;
        private PlayerActor _player;
        private Rigidbody2D _rb;

        [SetUp]
        public void SetUp()
        {
            _keyboard = Keyboard.current ?? InputSystem.AddDevice<Keyboard>();
            _keyboard.MakeCurrent();
        }

        [TearDown]
        public void TearDown()
        {
            if (_keyboard != null)
            {
                InputSystem.QueueStateEvent(_keyboard, new KeyboardState());
                InputSystem.Update();
            }
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed)
        {
            GameSession.Reset(seed);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);
            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.IsTrue(menu.StartSelectedRun());
            yield return WaitForScene(SceneNames.GameArena);
            _arena = Object.FindAnyObjectByType<ArenaController>();
            _player = _arena.Player;
            Assert.IsNotNull(_player, "player actor missing");
            _rb = _player.GetComponent<Rigidbody2D>();
            Assert.IsNotNull(_rb);
        }

        private static IEnumerator WaitForScene(string name)
        {
            float deadline = Time.realtimeSinceStartup + 10f;
            while (SceneManager.GetActiveScene().name != name && Time.realtimeSinceStartup < deadline)
                yield return null;
            Assert.AreEqual(name, SceneManager.GetActiveScene().name);
            yield return null;
            yield return null;
        }

        private void Hold(params Key[] keys)
        {
            if (_keyboard != null)
                _keyboard.MakeCurrent();
            InputSystem.QueueStateEvent(_keyboard, new KeyboardState(keys));
            InputSystem.Update();
        }

        private void IsolatePlayer()
        {
            if (_arena != null && _arena.WaveDirector != null)
            {
                _arena.WaveDirector.ClearAllEnemies();
                _arena.WaveDirector.enabled = false;
            }
        }

        [UnityTest]
        public IEnumerator PlayerMovesWithKeyboardAndDiagonalSpeedIsNormalized()
        {
            yield return StartArena(11);
            IsolatePlayer();
            yield return new WaitForSeconds(0.2f);

            Vector2 start = _rb.position;
            for (float t = 0; t < 0.5f; t += Time.fixedDeltaTime)
            {
                Hold(Key.D);
                yield return new WaitForFixedUpdate();
            }
            float straightSpeed = _rb.linearVelocity.magnitude;
            Vector2 moved = _rb.position - start;
            Debug.Log("[P2 timing] straight speed=" + straightSpeed.ToString("0.00") + " moved=" + moved);
            Assert.Greater(moved.x, 1.5f, "player did not move right");
            Assert.AreEqual(CombatTuning.PlayerMoveSpeed, straightSpeed, 0.3f);

            for (float t = 0; t < 0.2f; t += Time.fixedDeltaTime)
            {
                Hold(Key.D, Key.W);
                yield return new WaitForFixedUpdate();
            }
            float diagonalSpeed = _rb.linearVelocity.magnitude;
            Debug.Log("[P2 timing] diagonal speed=" + diagonalSpeed.ToString("0.00"));
            Assert.AreEqual(CombatTuning.PlayerMoveSpeed, diagonalSpeed, 0.3f, "diagonal movement must not be faster");
            Hold();
        }

        [UnityTest]
        public IEnumerator DashMovesFastRespectsCooldownAndWallsStopThePlayer()
        {
            yield return StartArena(12);
            IsolatePlayer();
            yield return new WaitForSeconds(0.2f);

            Vector2 start = _rb.position;
            Hold(Key.D);
            yield return null;
            Hold(Key.D, Key.Space);
            yield return null;
            Hold(Key.D);
            float peak = 0f;
            for (int i = 0; i < 12; i++)
            {
                yield return new WaitForFixedUpdate();
                peak = Mathf.Max(peak, _rb.linearVelocity.magnitude);
            }
            Debug.Log("[P2 timing] dash peak speed=" + peak.ToString("0.0") + " moved=" + (_rb.position - start));
            Assert.Greater(peak, CombatTuning.DashSpeed * 0.8f, "dash did not happen");
            Assert.IsFalse(_player.Dash.CooldownReady, "dash cooldown should be running");

            // A second press inside the cooldown must not start another dash.
            Hold(Key.D, Key.Space);
            yield return null;
            Hold(Key.D);
            yield return new WaitForFixedUpdate();
            Assert.Less(_rb.linearVelocity.magnitude, CombatTuning.PlayerMoveSpeed + 0.5f, "dash restarted during cooldown");

            // Keep walking right: the wall collider must stop the player at the arena edge.
            yield return new WaitForSeconds(5f);
            Hold();
            Assert.Less(_rb.position.x, ArenaConfig.HalfWidth, "player escaped through the wall");
            Hold();
        }

        [UnityTest]
        public IEnumerator AutoAttackKillsInvaderWithoutAnyAttackInput()
        {
            yield return StartArena(13);
            var director = _arena.WaveDirector;
            var attacker = _player.GetComponent<AutoAttacker>();
            float t0 = Time.time;
            float deadline = Time.realtimeSinceStartup + 20f;
            while (GameSession.Run.Hud.Score == 0 && Time.realtimeSinceStartup < deadline)
                yield return null;
            Debug.Log("[P2 timing] first kill after " + (Time.time - t0).ToString("0.00") + "s, shots=" + attacker.ShotsFired + ", playerHP=" + _player.Health.Current);
            Assert.Greater(GameSession.Run.Hud.Score, 0, "auto attack never killed an invader");
            Assert.Greater(attacker.ShotsFired, 0);
            Assert.Greater(GameSession.Run.Hud.Resource, 0f, "kill should grant resource");
            Assert.AreEqual(RunState.PlayingWave, GameSession.Machine.Current);
        }

        [UnityTest]
        public IEnumerator InvaderStrikeHurtsIfNotDodgedButDashAvoidsIt()
        {
            // Control run: standing still next to an invader eventually takes a melee hit.
            yield return StartArena(14);
            IsolatePlayer();
            float maxHp = _player.Health.Max;
            var inv = InvaderActor.Spawn(_rb.position + new Vector2(1.0f, 0f));
            inv.Health.Reset(100000f);        // keep it alive so the strike definitely happens
            float deadline = Time.realtimeSinceStartup + 3f;
            while (_player.Health.Current >= maxHp && Time.realtimeSinceStartup < deadline)
                yield return null;
            Assert.Less(_player.Health.Current, maxHp, "undodged strike should deal damage");
            Assert.AreEqual(maxHp - CombatTuning.InvaderMeleeDamage, _player.Health.Current, 0.01f);

            // Dodge run: dash away as soon as the windup begins.
            yield return StartArena(15);
            IsolatePlayer();
            maxHp = _player.Health.Max;
            inv = InvaderActor.Spawn(_rb.position + new Vector2(1.0f, 0f));
            inv.Health.Reset(100000f);
            deadline = Time.realtimeSinceStartup + 3f;
            while (inv.Melee.Phase != MeleePhase.Windup && Time.realtimeSinceStartup < deadline)
                yield return null;
            Assert.AreEqual(MeleePhase.Windup, inv.Melee.Phase, "invader never telegraphed");
            Hold(Key.A);
            yield return null;
            Hold(Key.A, Key.Space);
            yield return null;
            Hold();
            // Wait through the strike moment and its recovery.
            float end = Time.realtimeSinceStartup + 1.0f;
            while (Time.realtimeSinceStartup < end) yield return null;
            Assert.AreEqual(maxHp, _player.Health.Current, 0.01f, "dash-away should avoid the telegraphed strike");
        }

        [UnityTest]
        public IEnumerator PlayerDeathIsSafeAndHappensOnce()
        {
            yield return StartArena(16);
            IsolatePlayer();
            int deaths = 0;
            _player.Health.Died += () => deaths++;

            float dealt = _player.ReceiveDamage(100000f);
            Assert.Greater(dealt, 0f);
            Assert.AreEqual(0f, _player.ReceiveDamage(100000f), "damage after death must be ignored");
            yield return null;
            Assert.AreEqual(1, deaths);
            Assert.AreEqual(RunState.Death, GameSession.Machine.Current);
            Assert.AreEqual(0f, Time.timeScale);

            _arena.Input.RaisePause();       // pausing while dead must be harmless
            Assert.AreEqual(RunState.Death, GameSession.Machine.Current);

            _arena.ReturnToMenu();
            yield return WaitForScene(SceneNames.MainMenu);
            Assert.AreEqual(RunState.MainMenu, GameSession.Machine.Current);
            Assert.IsNull(GameSession.Run);
            Assert.AreEqual(1f, Time.timeScale);
            Assert.AreEqual(0, ActorRegistry.Count, "actors leaked out of the arena");
        }
    }
}
