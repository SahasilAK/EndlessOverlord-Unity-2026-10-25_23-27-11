using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class RewardPlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed = 12345L)
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
        public IEnumerator WaveClear_OpensRewardModal_AndPausesSimulation()
        {
            yield return StartArena(101L);

            // Buffer player health so they cannot die during test
            _player.Health.IncreaseMax(500f, healDelta: true);

            // Clear wave 1 spawns to trigger wave clear
            _arena.WaveDirector.ClearAllEnemies();
            while (_arena.WaveDirector.Ledger.SpawnsRemaining > 0)
            {
                _arena.WaveDirector.Ledger.TryRecordSpawn();
                _arena.WaveDirector.Ledger.TryRecordDeath();
            }

            yield return null;

            Assert.AreEqual(RunState.ChoosingReward, GameSession.Machine.Current);
            Assert.AreEqual(0f, Time.timeScale, "Gameplay simulation must pause during reward selection");
            Assert.IsTrue(_arena.WaveDirector.RewardModal.IsOpen, "Reward modal dialog must open on wave clear");
        }

        [UnityTest]
        public IEnumerator RewardSelection_AtomicallyAppliesBuff_AndUnlocksIntermission()
        {
            yield return StartArena(202L);
            _player.Health.IncreaseMax(500f, healDelta: true);

            // Clear wave 1
            _arena.WaveDirector.ClearAllEnemies();
            while (_arena.WaveDirector.Ledger.SpawnsRemaining > 0)
            {
                _arena.WaveDirector.Ledger.TryRecordSpawn();
                _arena.WaveDirector.Ledger.TryRecordDeath();
            }
            yield return null;

            var modal = _arena.WaveDirector.RewardModal;
            Assert.IsTrue(modal.IsOpen);
            var offer = modal.CurrentOffer;
            Assert.IsNotNull(offer);
            Assert.AreEqual(3, offer.Choices.Count);

            var pickedDef = offer.Choices[0];
            var run = GameSession.State.Run;

            Assert.AreEqual(0, run.Rewards.GetRank(pickedDef.Id));

            // Select card
            bool pickSuccess = offer.TryPick(pickedDef.Id, run.Rewards);
            Assert.IsTrue(pickSuccess);
            Assert.IsTrue(offer.IsCommitted);
            Assert.AreEqual(1, run.Rewards.GetRank(pickedDef.Id));

            // Double pick rejected
            Assert.IsFalse(offer.TryPick(pickedDef.Id, run.Rewards));

            // Progress to intermission
            GameSession.Machine.TryTransition(RunState.Intermission);
            yield return null;

            Assert.AreEqual(RunState.Intermission, GameSession.Machine.Current);
        }

        [UnityTest]
        public IEnumerator Intermission_NextWave_UnpausesAndAdvances()
        {
            yield return StartArena(303L);
            _player.Health.IncreaseMax(500f, healDelta: true);

            // Trigger wave 1 clear
            _arena.WaveDirector.ClearAllEnemies();
            while (_arena.WaveDirector.Ledger.SpawnsRemaining > 0)
            {
                _arena.WaveDirector.Ledger.TryRecordSpawn();
                _arena.WaveDirector.Ledger.TryRecordDeath();
            }
            yield return null;

            // Pick reward and advance to intermission
            var offer = _arena.WaveDirector.RewardModal.CurrentOffer;
            offer.TryPick(offer.Choices[0].Id, GameSession.State.Run.Rewards);
            GameSession.Machine.TryTransition(RunState.Intermission);
            yield return null;

            Assert.AreEqual(RunState.Intermission, GameSession.Machine.Current);
            Assert.AreEqual(0f, Time.timeScale);

            // Advance to next wave
            _arena.WaveDirector.StartWave(2);
            yield return null;

            Assert.AreEqual(RunState.PlayingWave, GameSession.Machine.Current);
            Assert.AreEqual(1f, Time.timeScale, "Simulation must unpause on next wave start");
            Assert.AreEqual(2, _arena.WaveDirector.CurrentWave);
        }

        [UnityTest]
        public IEnumerator RewardImpact_DirectlyModifiesRuntimeMechanics()
        {
            yield return StartArena(404L);
            var run = GameSession.State.Run;

            float initialMaxHp = _player.Health.Max;
            float initialCurrentHp = _player.Health.Current;

            // Apply Soul Harvest (+30 Max HP)
            var hpDef = RewardRegistry.GetById(RewardRegistry.MaxHealthId);
            run.Rewards.TryAdd(hpDef);
            yield return null;

            Assert.AreEqual(initialMaxHp + 30f, _player.Health.Max, 1e-4f);
            Assert.AreEqual(initialCurrentHp + 30f, _player.Health.Current, 1e-4f);

            // Apply Bone Reinforcement (+5 Armor)
            var armorDef = RewardRegistry.GetById(RewardRegistry.ArmorId);
            run.Rewards.TryAdd(armorDef);
            yield return null;

            // Base 0 armor takes 20 dmg -> with 5 armor takes 20 * 100 / 105 = 19.047f
            float dealt = _player.ReceiveDamage(20f);
            Assert.AreEqual(19.0476f, dealt, 0.05f);

            // Apply Grave Barrage (+1 Projectile)
            var projDef = RewardRegistry.GetById(RewardRegistry.ExtraProjId);
            run.Rewards.TryAdd(projDef);
            yield return null;

            Assert.AreEqual(1, run.Rewards.ExtraProjectiles);
        }

        [UnityTest]
        public IEnumerator PauseDuringRewardSelection_ResumesToChoosingReward()
        {
            yield return StartArena(505L);
            _player.Health.IncreaseMax(500f, healDelta: true);

            // Clear wave 1
            _arena.WaveDirector.ClearAllEnemies();
            while (_arena.WaveDirector.Ledger.SpawnsRemaining > 0)
            {
                _arena.WaveDirector.Ledger.TryRecordSpawn();
                _arena.WaveDirector.Ledger.TryRecordDeath();
            }
            yield return null;

            Assert.AreEqual(RunState.ChoosingReward, GameSession.Machine.Current);

            // Pause
            Assert.IsTrue(GameSession.Machine.TryPause());
            Assert.AreEqual(RunState.Paused, GameSession.Machine.Current);

            // Resume
            Assert.IsTrue(GameSession.Machine.TryResume());
            Assert.AreEqual(RunState.ChoosingReward, GameSession.Machine.Current, "Resuming must restore ChoosingReward, not jump to PlayingWave");
        }
    }
}
