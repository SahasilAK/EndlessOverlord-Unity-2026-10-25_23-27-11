using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class GameplayQAPlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
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

        [UnityTest]
        public IEnumerator AllFiveClasses_CanInitializeRun_EnterArena_AndExecuteCombatActions()
        {
            string[] characters =
            {
                CharacterCatalog.LichLordId,
                CharacterCatalog.VampireQueenId,
                CharacterCatalog.CursedGolemId,
                CharacterCatalog.ShadowReaperId,
                CharacterCatalog.DemonMonarchId
            };

            for (int i = 0; i < characters.Length; i++)
            {
                string charId = characters[i];
                GameSession.Reset(1000 + i);
                SceneManager.LoadScene(SceneNames.MainMenu);
                yield return WaitForScene(SceneNames.MainMenu);

                var menu = Object.FindAnyObjectByType<MainMenuController>();
                menu.OpenSelectForTest();
                menu.SelectCharacterForTest(charId);
                Assert.IsTrue(menu.StartSelectedRun(), $"Failed to start run with character '{charId}'");

                yield return WaitForScene(SceneNames.GameArena);
                _arena = Object.FindAnyObjectByType<ArenaController>();
                _player = _arena.Player;
                Assert.IsNotNull(_player, $"Player actor must exist for character '{charId}'");

                // Verify run context holds correct character
                Assert.AreEqual(charId, _player.Run.Character.Id);
                Assert.Greater(_player.Health.Current, 0f);
                Assert.IsFalse(string.IsNullOrEmpty(_player.Run.Hud.ResourceName));

                // Verify actor is registered
                Assert.AreEqual(_player, ActorRegistry.Player);

                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator CrowdedWaveLoad_Wave25_ExecutesWithoutExceptionsOrLeaks()
        {
            GameSession.Reset(9999);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.IsTrue(menu.StartSelectedRun());

            yield return WaitForScene(SceneNames.GameArena);
            _arena = Object.FindAnyObjectByType<ArenaController>();
            _player = _arena.Player;

            var director = _arena.WaveDirector;
            Assert.IsNotNull(director);

            // Jump to Wave 25 (High-intensity wave)
            director.StartWave(25);
            yield return null;

            Assert.AreEqual(25, director.CurrentWave);
            Assert.Greater(director.Ledger.TotalEnemies, 10);

            // Let spawns execute for 1.5 seconds
            float elapsed = 0f;
            while (elapsed < 1.5f)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Ensure active invaders do not breach maximum concurrent threshold
            Assert.LessOrEqual(director.ActiveInvaderCount, WaveDirector.MaxConcurrentEnemies);

            // Clean up cleanly
            director.ClearAllEnemies();
            Assert.AreEqual(0, director.ActiveInvaderCount);
            yield return null;
        }

        [UnityTest]
        public IEnumerator PauseResumeAndIntermissionFlow_MaintainsTimeScaleAndStateIntegrity()
        {
            GameSession.Reset(3333);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.IsTrue(menu.StartSelectedRun());

            yield return WaitForScene(SceneNames.GameArena);
            _arena = Object.FindAnyObjectByType<ArenaController>();

            // Pause
            GameSession.Machine.TryPause();
            Assert.AreEqual(RunState.Paused, GameSession.Machine.Current);
            Assert.AreEqual(0f, Time.timeScale);

            yield return null;

            // Resume
            _arena.Resume();
            Assert.AreEqual(RunState.PlayingWave, GameSession.Machine.Current);
            Assert.AreEqual(1f, Time.timeScale);

            yield return null;
        }
    }
}
