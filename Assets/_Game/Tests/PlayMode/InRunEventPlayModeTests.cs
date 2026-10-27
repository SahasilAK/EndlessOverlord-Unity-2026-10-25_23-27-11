using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class InRunEventPlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed = 4444)
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
            Assert.IsNotNull(_player, "Player actor must exist");
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
        public IEnumerator WaveDirector_Wave3_SpawnsMagicalShrine_AndPlayerCanActivateIt()
        {
            yield return StartArena(5555);

            var director = _arena.WaveDirector;
            Assert.IsNotNull(director);

            // Start Wave 3 (Milestone Magical Shrine event)
            director.StartWave(3);
            yield return null;

            Assert.AreEqual(InRunEventType.MagicalShrine, _player.Run.LastWaveEvent, "Wave 3 must trigger a Magical Shrine event");

            // Look for ShrineActor in scene
            var shrineActor = Object.FindAnyObjectByType<ShrineActor>();
            Assert.IsNotNull(shrineActor, "A ShrineActor should be present in the arena for Wave 3");
            Assert.IsFalse(shrineActor.IsUsed, "Shrine should initially be unused");

            // Teleport player onto the shrine to activate it
            _player.Teleport(shrineActor.transform.position);

            // Wait a few frames for Update interaction
            yield return null;
            yield return null;

            Assert.IsTrue(shrineActor.IsUsed, "Shrine should be marked used after player enters its radius");
        }

        [UnityTest]
        public IEnumerator WaveDirector_Wave7_TriggersHolyCrusade_WithEventRecorded()
        {
            yield return StartArena(7777);

            var director = _arena.WaveDirector;
            Assert.IsNotNull(director);

            // Start Wave 7 (Milestone Holy Crusade event)
            director.StartWave(7);
            yield return null;

            Assert.AreEqual(InRunEventType.HolyCrusade, _player.Run.LastWaveEvent, "Wave 7 must trigger Holy Crusade event");
            Assert.Greater(director.Ledger.TotalEnemies, 0);
        }
    }
}
