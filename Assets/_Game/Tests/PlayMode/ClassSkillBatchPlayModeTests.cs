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
    public sealed class ClassSkillBatchPlayModeTests
    {
        private Keyboard _keyboard;
        private ArenaController _arena;
        private PlayerActor _player;

        [SetUp]
        public void SetUp()
        {
            _keyboard = InputSystem.AddDevice<Keyboard>();
            ResetLichLordProfile();
        }

        [TearDown]
        public void TearDown()
        {
            if (_keyboard != null)
                InputSystem.RemoveDevice(_keyboard);
            Time.timeScale = 1f;
            ResetLichLordProfile();
        }

        private static void ResetLichLordProfile()
        {
            var profile = SaveStorage.Load();
            var charRec = profile.GetOrAddCharacter(CharacterCatalog.LichLordId);
            charRec.EquippedLoadout = new List<string> { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
            charRec.AutocastSettings = new List<bool> { false, false, false, false, false };
            profile.SaveCharacter(charRec);
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;
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
                if (elapsed >= timeout)
                    Assert.Fail($"Timed out waiting for scene {sceneName}");
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator LichLord_BranchA_UndeadMinions_SpawnAndEngageInvaders()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch A — Undead Summoning", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_03", // Grave Spearman
                    "lich_lord_active_04", // Grave Hound
                    "lich_lord_active_05", // Bone Mage
                    "lich_lord_active_06", // Ossuary Guardian
                    "lich_lord_active_07"  // Wight Captain
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout, "AbilityLoadoutController missing from Player");
            loadout.SyncLoadoutFromRun();

            // Cast slots 0 to 4 (refill essence before each cast to respect MaxResource=100)
            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                bool cast = loadout.TryActivateSlot(slot);
                Assert.IsTrue(cast, $"Slot {slot} must activate successfully.");
            }

            yield return new WaitForSeconds(0.2f);

            // Verify all 5 minions spawned
            var minions = Object.FindObjectsByType<UndeadMinionActor>();
            Assert.GreaterOrEqual(minions.Length, 5, "At least 5 undead minions should be spawned in the arena.");

            // Spawn an invader near the summons and verify combat interaction
            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.Swordsman);
            float startHp = invader.Health.Current;

            yield return new WaitForSeconds(0.8f);

            Assert.Less(invader.Health.Current, startHp, "Summoned minions should pursue and inflict damage on the invader.");
        }

        [UnityTest]
        public IEnumerator LichLord_BranchA_Abomination_And_SkeletalDragon_CastAndDamage()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 80, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch A — Undead Summoning", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_08", // Corpse Collector (Scavenger)
                    "lich_lord_active_09", // Dread Abomination
                    "lich_lord_active_10", // Skeletal Dragon
                    string.Empty,
                    string.Empty
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Corpse Collector) should activate.");

            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Dread Abomination) should activate.");

            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Skeletal Dragon) should activate.");

            yield return new WaitForSeconds(0.2f);

            var minions = Object.FindObjectsByType<UndeadMinionActor>();
            Assert.GreaterOrEqual(minions.Length, 3);

            // Spawn an invader in front of the dragon and abomination
            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2.0f, InvaderArchetype.Swordsman);
            float startHp = invader.Health.Current;

            yield return new WaitForSeconds(1.0f);

            Assert.Less(invader.Health.Current, startHp, "Abomination or Dragon must damage the invader.");
        }

        [UnityTest]
        public IEnumerator LichLord_WightCaptain_Aura_IncreasesNearbyMinionDamage()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            // Test static aura detection without captain
            var (damageNoCapt, speedNoCapt) = UndeadMinionActor.GetCaptainBuff(Vector2.zero);
            Assert.AreEqual(1.0f, damageNoCapt, 0.01f);
            Assert.AreEqual(1.0f, speedNoCapt, 0.01f);

            // Spawn a captain minion at (0, 0)
            var captain = UndeadMinionActor.Spawn(UndeadMinionType.Captain, Vector2.zero);
            yield return null;

            // Position (1, 0) is within 5.5m: should receive aura buff (+30% dmg, +25% speed)
            var (damageWithCapt, speedWithCapt) = UndeadMinionActor.GetCaptainBuff(new Vector2(1f, 0f));
            Assert.AreEqual(1.30f, damageWithCapt, 0.01f);
            Assert.AreEqual(1.25f, speedWithCapt, 0.01f);

            // Position (10, 0) is outside 5.5m: should NOT receive aura buff
            var (damageFar, speedFar) = UndeadMinionActor.GetCaptainBuff(new Vector2(10f, 0f));
            Assert.AreEqual(1.0f, damageFar, 0.01f);
            Assert.AreEqual(1.0f, speedFar, 0.01f);

            Object.Destroy(captain.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator LichLord_MilestoneUltimates_ExecuteAndClear()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 250, profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_ultimate_100", // Ossuary Dragon
                    "lich_lord_ultimate_150", // Eternal Winter Court
                    "lich_lord_ultimate_200", // Black Star Eclipse
                    "lich_lord_ultimate_250", // World Grave Awakening
                    string.Empty
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            // 1. Cast Ultimate 100: Ossuary Dragon (cost 75)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Ossuary Dragon) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 2. Cast Ultimate 150: Eternal Winter Court (spawns 2 frost guardians, cost 75)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Eternal Winter Court) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 3. Cast Ultimate 200: Black Star Eclipse (cost 80, harvests 40 Soul Essence)
            GameSession.Run.Hud.AddResource(100f);
            float essenceBefore = GameSession.Run.Hud.Resource;
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Black Star Eclipse) should activate.");
            yield return new WaitForSeconds(0.1f);
            // Black Star Eclipse costs 80 and immediately restores 40 essence (net change around -40)
            Assert.GreaterOrEqual(GameSession.Run.Hud.Resource, essenceBefore - 45f);

            // 4. Cast Ultimate 250: World Grave Awakening (spawns army, cost 90)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (World Grave Awakening) should activate.");
            yield return new WaitForSeconds(0.2f);

            // Verify minions exist in the scene
            var allMinions = Object.FindObjectsByType<UndeadMinionActor>();
            Assert.GreaterOrEqual(allMinions.Length, 3);
            var sentinels = Object.FindObjectsByType<BoneSentinelActor>();
            Assert.GreaterOrEqual(sentinels.Length, 4);
        }
    }
}
