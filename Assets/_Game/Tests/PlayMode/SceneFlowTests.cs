using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class SceneFlowPlayModeTests
    {
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
            Assert.AreEqual(name, SceneManager.GetActiveScene().name, "scene did not load in time");
            yield return null;   // let Start() of the scene's controllers run
            yield return null;
        }

        [UnityTest]
        public IEnumerator MenuSelectStartPauseResumeReturn()
        {
            GameSession.Reset(2026);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            Assert.IsNotNull(menu, "MainMenuController missing");
            Assert.AreEqual(RunState.MainMenu, GameSession.Machine.Current);

            menu.OpenSelectForTest();
            Assert.AreEqual(RunState.CharacterSelect, GameSession.Machine.Current);

            menu.SelectCharacterForTest("unplayable_character");   // unknown/unplayable character: must be rejected
            Assert.IsNull(GameSession.State.SelectedCharacterId);
            Assert.IsFalse(menu.StartSelectedRun(), "run must not start without a playable selection");

            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.AreEqual(CharacterCatalog.LichLordId, GameSession.State.SelectedCharacterId);
            Assert.IsTrue(menu.StartSelectedRun());
            Assert.IsFalse(menu.StartSelectedRun(), "double start must be rejected");

            yield return WaitForScene(SceneNames.GameArena);
            Assert.AreEqual(RunState.PlayingWave, GameSession.Machine.Current);
            var firstSeed = GameSession.Run.Seed;
            Assert.AreNotEqual(0L, firstSeed);

            var arena = Object.FindAnyObjectByType<ArenaController>();
            Assert.IsNotNull(arena);
            Assert.IsNotNull(arena.Input);

            arena.Input.RaisePause();
            Assert.AreEqual(RunState.Paused, GameSession.Machine.Current);
            Assert.AreEqual(0f, Time.timeScale);

            arena.Input.RaisePause();
            Assert.AreEqual(RunState.PlayingWave, GameSession.Machine.Current);
            Assert.AreEqual(1f, Time.timeScale);

            arena.Input.RaisePause();                      // pause again, then use the Return path
            Assert.AreEqual(RunState.Paused, GameSession.Machine.Current);
            arena.ReturnToMenu();
            arena.ReturnToMenu();                          // second call must be harmless
            yield return WaitForScene(SceneNames.MainMenu);
            Assert.AreEqual(RunState.MainMenu, GameSession.Machine.Current);
            Assert.IsNull(GameSession.Run);
            Assert.AreEqual(1f, Time.timeScale);

            // Second run must get a different recorded seed.
            menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.IsTrue(menu.StartSelectedRun());
            yield return WaitForScene(SceneNames.GameArena);
            Assert.AreNotEqual(firstSeed, GameSession.Run.Seed);
            Assert.AreEqual(2, GameSession.Run.RunNumber);
        }
    }
}
