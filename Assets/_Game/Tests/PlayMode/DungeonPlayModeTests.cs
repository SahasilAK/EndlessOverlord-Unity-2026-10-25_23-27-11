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
    public sealed class DungeonPlayModeTests
    {
        private Keyboard _keyboard;
        private ArenaController _arena;
        private PlayerActor _player;

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
            Assert.IsNotNull(_player, "Player actor missing from GameArena");
        }

        private static IEnumerator WaitForScene(string sceneName, float timeout = 5f)
        {
            float elapsed = 0f;
            while (SceneManager.GetActiveScene().name != sceneName)
            {
                elapsed += Time.unscaledDeltaTime;
                if (elapsed > timeout)
                    Assert.Fail($"Timed out waiting for scene {sceneName}");
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator ProceduralArena_InstantiatesWallsObstaclesAndPortals_MatchesLayout()
        {
            yield return StartArena(0x12345678L);

            var run = GameSession.Run;
            Assert.IsNotNull(run);
            Assert.IsNotNull(run.Layout);

            var worldGo = GameObject.Find("DungeonWorld");
            Assert.IsNotNull(worldGo, "DungeonWorld container GameObject missing");

            var floorGo = GameObject.Find("ArenaFloor");
            Assert.IsNotNull(floorGo, "ArenaFloor missing");
            var floorSr = floorGo.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(floorSr);
            Assert.AreEqual(run.Layout.Biome.FloorColor.R, floorSr.color.r, 0.01f);

            var wallsGo = GameObject.Find("Walls");
            Assert.IsNotNull(wallsGo);
            Assert.AreEqual(run.Layout.Walls.Count, wallsGo.transform.childCount);

            var obsGo = GameObject.Find("Obstacles");
            Assert.IsNotNull(obsGo);
            Assert.AreEqual(run.Layout.Obstacles.Count, obsGo.transform.childCount);

            var portalsGo = GameObject.Find("Portals");
            Assert.IsNotNull(portalsGo);
            Assert.AreEqual(run.Layout.Portals.Count, portalsGo.transform.childCount);

            // Player position should be at PlayerSpawn
            Assert.AreEqual(run.Layout.PlayerSpawn.X, _player.Position.x, 0.1f);
            Assert.AreEqual(run.Layout.PlayerSpawn.Y, _player.Position.y, 0.1f);
        }

        private void Hold(params Key[] keys)
        {
            if (_keyboard != null)
                _keyboard.MakeCurrent();
            InputSystem.QueueStateEvent(_keyboard, new KeyboardState(keys));
            InputSystem.Update();
        }

        [UnityTest]
        public IEnumerator Player_CanMoveAndDashInProceduralArena()
        {
            yield return StartArena(0x87654321L);

            if (_arena != null && _arena.WaveDirector != null)
            {
                _arena.WaveDirector.ClearAllEnemies();
                _arena.WaveDirector.enabled = false;
            }

            yield return new WaitForSeconds(0.2f);

            Vector2 initialPos = _player.Position;

            for (float t = 0; t < 0.5f; t += Time.fixedDeltaTime)
            {
                Hold(Key.D);
                yield return new WaitForFixedUpdate();
            }

            Hold(); // Release

            // Player should have moved to the right
            Assert.Greater(_player.Position.x, initialPos.x + 0.5f);
        }

        [UnityTest]
        public IEnumerator Enemies_SpawnEmergingFromProceduralPortals()
        {
            yield return StartArena(0xABCDEF01L);

            var director = _arena.WaveDirector;
            Assert.IsNotNull(director);

            // Wait until first enemy spawns (delay is 0.5s)
            float waitTimer = 0f;
            while (director.ActiveInvaderCount == 0 && waitTimer < 3f)
            {
                waitTimer += Time.deltaTime;
                yield return null;
            }

            Assert.Greater(director.ActiveInvaderCount, 0, "No invader spawned within 3 seconds");

            var invader = Object.FindAnyObjectByType<InvaderActor>();
            Assert.IsNotNull(invader);

            // Verify invader spawned near one of the valid portals
            var portals = GameSession.Run.Layout.Portals;
            bool nearPortal = false;
            for (int i = 0; i < portals.Count; i++)
            {
                float dist = Vector2.Distance(invader.Position, portals[i].Position.ToUnity());
                if (dist <= 2.0f) // Portal jitter is +/- 0.5f
                {
                    nearPortal = true;
                    break;
                }
            }

            Assert.IsTrue(nearPortal, $"Invader spawned at {invader.Position}, not near any procedural portal!");
        }

        [UnityTest]
        public IEnumerator ConsecutiveRuns_ProduceDistinctDungeonLayouts()
        {
            yield return StartArena(111111L);
            string fingerprintA = GameSession.Run.Layout.Fingerprint;
            string biomeA = GameSession.Run.Layout.Biome.Name;

            yield return StartArena(999999L);
            string fingerprintB = GameSession.Run.Layout.Fingerprint;
            string biomeB = GameSession.Run.Layout.Biome.Name;

            Assert.AreNotEqual(fingerprintA, fingerprintB, "Consecutive runs should produce distinct fingerprints.");
        }
    }
}
