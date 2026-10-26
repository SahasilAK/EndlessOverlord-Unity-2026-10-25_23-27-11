using System.Collections;
using System.IO;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class SaveAndProgressionPlayModeTests
    {
        private Keyboard _keyboard;
        private ArenaController _arena;
        private PlayerActor _player;

        [SetUp]
        public void SetUp()
        {
            _keyboard = InputSystem.AddDevice<Keyboard>();
        }

        [TearDown]
        public void TearDown()
        {
            if (_keyboard != null)
                InputSystem.RemoveDevice(_keyboard);
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed = 888L)
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
        public IEnumerator PlayerDeath_ShowsResultsView_AndCommitsRewardsIdempotently()
        {
            yield return StartArena(101L);
            var run = GameSession.State.Run;

            // Kill player
            _player.Health.TakeDamage(_player.Health.Max + 100f);
            yield return null;

            Assert.AreEqual(RunState.Death, GameSession.Machine.Current);

            // Open results view
            _arena.ShowResults();
            yield return null;

            Assert.AreEqual(RunState.Results, GameSession.Machine.Current);
            Assert.IsTrue(_arena.ResultsView.IsOpen);

            var profile = GameSession.State.Profile;
            Assert.IsNotNull(profile);
            Assert.IsTrue(profile.HasClaimedRun(run.RunId));
            int initialWallet = profile.MetaWallet;
            Assert.IsTrue(initialWallet > 0, "Should have earned meta souls from run");

            // Showing results again with same summary must not double credit
            _arena.ResultsView.Show(run.BuildSummary(), profile);
            Assert.AreEqual(initialWallet, profile.MetaWallet, "Idempotent payout must prevent double grant");
        }

        [UnityTest]
        public IEnumerator MetaUpgradePurchase_PersistsLocally_AndBuffsNextRun()
        {
            yield return StartArena(202L);

            // Give profile 100 souls and purchase Soul Vitality (+15 Max HP)
            var profile = GameSession.State.Profile ?? SaveStorage.Load();
            profile.MetaWallet = 100;
            profile.SetUpgradeRank(PersistentUpgrades.SoulVitalityId, 0); // reset
            Assert.IsTrue(profile.TryPurchaseUpgrade(PersistentUpgrades.SoulVitalityId, 30));
            Assert.AreEqual(1, profile.GetUpgradeRank(PersistentUpgrades.SoulVitalityId));
            SaveStorage.Save(profile);

            // Return to MainMenu
            _arena.ReturnToMenu();
            yield return WaitForScene(SceneNames.MainMenu);

            // Start fresh run
            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.IsTrue(menu.StartSelectedRun());

            yield return WaitForScene(SceneNames.GameArena);
            var newArena = Object.FindAnyObjectByType<ArenaController>();
            var newPlayer = newArena.Player;

            // Lich Lord base HP = 100. With 1 rank of Soul Vitality (+15 HP), starting Max HP must be 115!
            Assert.AreEqual(115f, newPlayer.Health.Max, 1e-4f);
            Assert.AreEqual(115f, newPlayer.Health.Current, 1e-4f);
        }
    }
}
