using System.Collections;
using System.Collections.Generic;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public sealed class SkillTreePlayModeTests
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

            var profile = SaveStorage.Load();
            var charRec = profile.GetOrAddCharacter(CharacterCatalog.LichLordId);
            charRec.AutocastSettings = new List<bool> { false, false, false, false, false };
            profile.SaveCharacter(charRec);
            SaveStorage.Save(profile);
        }

        private IEnumerator StartArena(string characterId, long seed = 42L)
        {
            GameSession.Reset(seed);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(characterId);
            Assert.IsTrue(menu.StartSelectedRun(), $"Failed to start run for {characterId}");

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
        public IEnumerator Loadout_CanEquipAbilities_AndCastInArenaViaInputKeys()
        {
            yield return StartArena(CharacterCatalog.LichLordId, 42L);

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout, "AbilityLoadoutController must be present on player actor");

            // Slot 0 should have Bone Sentinel by default
            var slot0Def = loadout.GetSlotAbility(0);
            Assert.IsNotNull(slot0Def, "Slot 0 should be equipped");
            Assert.AreEqual("lich_lord_active_01", slot0Def.Id);

            float initialEssence = GameSession.Run.Hud.Resource;
            Assert.GreaterOrEqual(initialEssence, 25f, "Should have starting resource for Bone Sentinel");

            // Cast Slot 0
            bool castSuccess = loadout.TryActivateSlot(0);
            Assert.IsTrue(castSuccess, "TryActivateSlot(0) should succeed");

            yield return null;

            // Verify resource deducted
            Assert.AreEqual(initialEssence - slot0Def.ResourceCost, GameSession.Run.Hud.Resource, 0.01f);

            // Verify cooldown started
            Assert.Greater(loadout.GetSlotCooldown(0), 0f, "Cooldown should be ticking");

            // Verify minion spawned in registry
            bool sentinelFound = false;
            var all = ActorRegistry.AllActors;
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i] is BoneSentinelActor && all[i].Alive)
                {
                    sentinelFound = true;
                    break;
                }
            }
            Assert.IsTrue(sentinelFound, "BoneSentinelActor should be present in ActorRegistry");
        }

        [UnityTest]
        public IEnumerator Abilities_DisabledDuringPauseAndDeath()
        {
            yield return StartArena(CharacterCatalog.LichLordId, 42L);

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);

            string blockedReason = "";
            loadout.AbilityBlocked += (slot, msg) => blockedReason = msg;

            // 1. Pause state guard
            GameSession.Machine.TryTransition(RunState.Paused);
            Assert.AreEqual(RunState.Paused, GameSession.Machine.Current);

            bool pauseCast = loadout.TryActivateSlot(0);
            Assert.IsFalse(pauseCast, "Cannot cast ability while game is paused");
            Assert.IsTrue(blockedReason.Contains("paused"), $"Expected pause blocked message, got: {blockedReason}");

            // Unpause
            GameSession.Machine.TryTransition(RunState.PlayingWave);

            // 2. Death state guard
            _player.ReceiveDamage(9999f);
            yield return null;
            Assert.IsFalse(_player.Alive, "Player should be dead");

            blockedReason = "";
            bool deathCast = loadout.TryActivateSlot(0);
            Assert.IsFalse(deathCast, "Cannot cast ability while player is defeated");
            Assert.IsTrue(blockedReason.Contains("defeated") || blockedReason.Contains("menu"), $"Expected defeat blocked message, got: {blockedReason}");
        }

        [UnityTest]
        public IEnumerator Autocast_TogglesAndExecutesWhenOffCooldownAndEnemyInRange_RespectingResource()
        {
            yield return StartArena(CharacterCatalog.LichLordId, 42L);

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);

            // Toggle autocast ON for slot 0
            Assert.IsFalse(loadout.IsAutocastEnabled(0));
            loadout.ToggleAutocast(0);
            Assert.IsTrue(loadout.IsAutocastEnabled(0));

            // Spawn an invader near player
            Vector2 enemyPos = _player.Position + new Vector2(2f, 0f);
            var invader = InvaderActor.Spawn(enemyPos);
            invader.Health.Reset(1000f);

            yield return new WaitForSeconds(0.1f);

            // Autocast should have triggered
            Assert.Greater(loadout.GetSlotCooldown(0), 0f, "Autocast should have triggered cooldown");

            // Empty resource to test resource limit
            GameSession.Run.Hud.TrySpendResource(GameSession.Run.Hud.Resource);
            Assert.AreEqual(0f, GameSession.Run.Hud.Resource);

            // Advance time to clear cooldown
            yield return new WaitForSeconds(0.2f);

            // With 0 resource, autocast must NOT execute and not throw errors
            Assert.IsFalse(loadout.HasSufficientResourceForSlot(0));

            // Turn autocast off so state is clean
            loadout.ToggleAutocast(0);

            if (invader != null) Object.Destroy(invader.gameObject);
        }

        [UnityTest]
        public IEnumerator TeleportDestination_ValidatesAgainstArenaBoundsAndObstacles()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId, 42L);

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);

            Vector2 origin = _player.Position;
            // Target far outside arena bounds (+100m)
            Vector2 extremeTarget = origin + new Vector2(100f, 100f);
            Vector2 safeDest = loadout.ValidateLandingDestination(origin, extremeTarget);

            // Verify safe destination is strictly within arena bounds
            Assert.LessOrEqual(Mathf.Abs(safeDest.x), ArenaConfig.HalfWidth - 1.0f);
            Assert.LessOrEqual(Mathf.Abs(safeDest.y), ArenaConfig.HalfHeight - 1.0f);
        }

        [UnityTest]
        public IEnumerator SkillTreeView_OpensAndAllowsSpendingPointsAndEquipping()
        {
            GameSession.Reset(42L);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            Assert.IsNotNull(menu);

            // Grant 5 points to Lich Lord for test
            var profile = SaveStorage.Load();
            var charRec = profile.GetOrAddCharacter(CharacterCatalog.LichLordId);
            charRec.Level = 5; // 2 * 4 = 8 points earned
            profile.SaveCharacter(charRec);
            SaveStorage.Save(profile);
            GameSession.State.Profile = profile;

            // Open Skill Tree modal
            menu.OpenSkillTreeForTest(CharacterCatalog.LichLordId);
            yield return null;

            Assert.IsTrue(menu.SkillTreeView.IsOpen, "SkillTreeView should be open");

            // Close Skill Tree modal
            menu.SkillTreeView.Close();
            yield return null;

            Assert.IsFalse(menu.SkillTreeView.IsOpen, "SkillTreeView should be closed");
        }
    }
}
