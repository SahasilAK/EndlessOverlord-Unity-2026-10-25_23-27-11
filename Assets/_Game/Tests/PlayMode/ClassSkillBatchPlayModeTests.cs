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
        private ArenaController _arena;
        private PlayerActor _player;

        [SetUp]
        public void SetUp()
        {
            ResetLichLordProfile();
            ResetCursedGolemProfile();
        }

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
            ResetLichLordProfile();
            ResetCursedGolemProfile();
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

        private static void ResetCursedGolemProfile()
        {
            var profile = SaveStorage.Load();
            var charRec = profile.GetOrAddCharacter(CharacterCatalog.CursedGolemId);
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

        [UnityTest]
        public IEnumerator LichLord_BranchB_HazardZonesAndLantern_CastAndSlowInvader()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 80, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch B — Frost and Death Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_12", // Permafrost Sigil
                    "lich_lord_active_13", // Chill Lance
                    "lich_lord_active_14", // Wailing Blizzard
                    "lich_lord_active_16", // Shiver Pulse
                    "lich_lord_active_18"  // Frozen Lantern
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            // Spawn an invader in front of player
            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.Swordsman);
            float startHp = invader.Health.Current;

            // 1. Cast Slot 0 (Permafrost Sigil)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Permafrost Sigil) should activate.");
            yield return new WaitForSeconds(0.1f);

            var hazards = Object.FindObjectsByType<HazardZoneActor>();
            Assert.GreaterOrEqual(hazards.Length, 1, "Permafrost Sigil should create a HazardZoneActor.");

            // 2. Cast Slot 1 (Chill Lance)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Chill Lance) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 3. Cast Slot 2 (Wailing Blizzard)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Wailing Blizzard) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 4. Cast Slot 3 (Shiver Pulse)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (Shiver Pulse) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 5. Cast Slot 4 (Frozen Lantern)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(4), "Slot 4 (Frozen Lantern) should activate.");
            yield return new WaitForSeconds(0.1f);

            var lanterns = Object.FindObjectsByType<FrozenLanternActor>();
            Assert.GreaterOrEqual(lanterns.Length, 1, "Frozen Lantern should spawn an orbiting lantern.");

            yield return new WaitForSeconds(0.5f);

            // Verify invader took damage and was slowed
            Assert.Less(invader.Health.Current, startHp, "Invader should have received damage from frost spells.");
            Assert.IsTrue(invader.IsSlowed, "Invader should have active slow debuff applied by frost spells.");
            Assert.Less(invader.MoveSpeed, invader.BaseMoveSpeed, "Invader's MoveSpeed should be reduced by slow multiplier.");
        }

        [UnityTest]
        public IEnumerator LichLord_BranchB_GlacialTomb_And_WinterOfTheDead_Cast()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 100, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch B — Frost and Death Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_15", // Glacial Tomb
                    "lich_lord_active_17", // Rime Chain
                    "lich_lord_active_19", // Hail Crown
                    "lich_lord_active_20", // Winter of the Dead
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

            // Spawn minion to test Winter of the Dead buff / healing
            var minion = UndeadMinionActor.Spawn(UndeadMinionType.Guardian, _player.Position + Vector2.left * 1.5f);
            minion.ReceiveDamage(30f);
            float minionHpBefore = minion.Health.Current;

            // Spawn two invaders
            var invader1 = InvaderActor.Spawn(_player.Position + Vector2.right * 2.0f, InvaderArchetype.Swordsman);
            var invader2 = InvaderActor.Spawn(_player.Position + Vector2.right * 3.5f, InvaderArchetype.Swordsman);
            float hp1Before = invader1.Health.Current;
            float hp2Before = invader2.Health.Current;

            // 1. Glacial Tomb on nearest invader
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Glacial Tomb should activate.");
            yield return new WaitForSeconds(0.1f);
            Assert.Less(invader1.Health.Current, hp1Before, "Glacial Tomb should deal heavy damage to nearest invader.");

            // 2. Rime Chain
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Rime Chain should activate.");
            yield return new WaitForSeconds(0.1f);
            Assert.Less(invader2.Health.Current, hp2Before, "Rime Chain should jump and damage secondary invader.");

            // 3. Hail Crown
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Hail Crown should activate.");
            yield return new WaitForSeconds(0.1f);

            // 4. Winter of the Dead
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Winter of the Dead should activate.");
            yield return new WaitForSeconds(0.1f);

            // Verify minion was healed by Winter of the Dead
            Assert.Greater(minion.Health.Current, minionHpBefore, "Winter of the Dead should restore health to player summons.");
        }

        [UnityTest]
        public IEnumerator LichLord_BranchC_FireAndWind_CastAndDamageInvader()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 90, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch C — Fire, Wind and Thunder Sorcery", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_21", // Witchfire Orb
                    "lich_lord_active_22", // Graveflame Pillar
                    "lich_lord_active_23", // Ashen Gust
                    "lich_lord_active_27", // Storm of Ash
                    "lich_lord_active_28"  // Phantom Cyclone
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            // Spawn invader in front of player
            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 1.8f, InvaderArchetype.Swordsman);
            float startHp = invader.Health.Current;

            // 1. Cast Slot 0 (Witchfire Orb)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Witchfire Orb) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 2. Cast Slot 1 (Graveflame Pillar)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Graveflame Pillar) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 3. Cast Slot 2 (Ashen Gust)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Ashen Gust) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 4. Cast Slot 3 (Storm of Ash)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (Storm of Ash) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 5. Cast Slot 4 (Phantom Cyclone)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(4), "Slot 4 (Phantom Cyclone) should activate.");
            yield return new WaitForSeconds(0.3f);

            var hazards = Object.FindObjectsByType<HazardZoneActor>();
            Assert.GreaterOrEqual(hazards.Length, 2, "Graveflame Pillar, Storm of Ash or Cyclone should spawn hazard zones.");

            Assert.Less(invader.Health.Current, startHp, "Invader should have received damage from fire and wind spells.");
        }

        [UnityTest]
        public IEnumerator LichLord_BranchC_ThunderAndLightning_CastAndDamageInvader()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 100, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch C — Fire, Wind and Thunder Sorcery", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_24", // Static Cadaver
                    "lich_lord_active_25", // Deathbolt
                    "lich_lord_active_26", // Corpse Spark
                    "lich_lord_active_29", // Ember Ossuary
                    "lich_lord_active_30"  // Thunder Coffin
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            // Spawn two invaders
            var invader1 = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.Swordsman);
            var invader2 = InvaderActor.Spawn(_player.Position + Vector2.right * 3.0f, InvaderArchetype.Swordsman);
            float hp1Before = invader1.Health.Current;
            float hp2Before = invader2.Health.Current;

            // 1. Static Cadaver
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Static Cadaver) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 2. Deathbolt
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Deathbolt) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 3. Corpse Spark (chaining to invader 2)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Corpse Spark) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 4. Ember Ossuary
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (Ember Ossuary) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 5. Thunder Coffin
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(4), "Slot 4 (Thunder Coffin) should activate.");
            yield return new WaitForSeconds(0.3f);

            Assert.Less(invader1.Health.Current, hp1Before, "Invader 1 should have received damage from lightning spells.");
            Assert.Less(invader2.Health.Current, hp2Before, "Invader 2 should have received chained damage.");
            Assert.IsTrue(invader1.IsSlowed, "Invader 1 should be trapped/slowed by Thunder Coffin / Static Cadaver.");
        }

        [UnityTest]
        public IEnumerator LichLord_BranchD_LifeDrainAndCurses_CastAndHealPlayer()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 95, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch D — Forbidden Dark Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_31", // Soul Siphon
                    "lich_lord_active_32", // Hex of Frailty
                    "lich_lord_active_33", // Withering Field
                    "lich_lord_active_35", // Spirit Leech
                    "lich_lord_active_36"  // Doom Bind
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            // Damage player to verify siphon healing
            _player.ReceiveDamage(25f);
            float playerHpBefore = _player.Health.Current;

            // Spawn high-health boss invader in front of player so it survives all 5 spell casts
            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            // 1. Cast Slot 0 (Soul Siphon)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Soul Siphon) should activate.");
            yield return new WaitForSeconds(0.1f);
            Assert.Greater(_player.Health.Current, playerHpBefore, "Soul Siphon should heal the player upon damaging target.");

            // 2. Cast Slot 1 (Hex of Frailty)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Hex of Frailty) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 3. Cast Slot 2 (Withering Field)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Withering Field) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 4. Cast Slot 3 (Spirit Leech)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (Spirit Leech) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 5. Cast Slot 4 (Doom Bind)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(4), "Slot 4 (Doom Bind) should activate.");
            yield return new WaitForSeconds(0.3f);

            Assert.Less(invader.Health.Current, startHp, "Invader should have received damage from dark magic spells.");
            Assert.IsTrue(invader.IsSlowed, "Invader should have root/slow applied by Doom Bind.");
        }

        [UnityTest]
        public IEnumerator LichLord_BranchD_BlackSun_EbonGrasp_UmbralNova_CastAndDamage()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 100, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch D — Forbidden Dark Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_34", // Black Sun
                    "lich_lord_active_37", // Ebon Grasp
                    "lich_lord_active_38", // Curse Spread
                    "lich_lord_active_39", // Death Sentence
                    "lich_lord_active_40"  // Umbral Nova
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            // Spawn two invaders
            var invader1 = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.Swordsman);
            var invader2 = InvaderActor.Spawn(_player.Position + Vector2.right * 3.0f, InvaderArchetype.Swordsman);
            float hp1Before = invader1.Health.Current;
            float hp2Before = invader2.Health.Current;

            // 1. Black Sun (gravity hazard)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Black Sun) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 2. Ebon Grasp (ground claw hazard)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Ebon Grasp) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 3. Curse Spread (chain jumping)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Curse Spread) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 4. Death Sentence
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (Death Sentence) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 5. Umbral Nova (massive void explosion)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(4), "Slot 4 (Umbral Nova) should activate.");
            yield return new WaitForSeconds(0.3f);

            var hazards = Object.FindObjectsByType<HazardZoneActor>();
            Assert.GreaterOrEqual(hazards.Length, 2, "Black Sun and Ebon Grasp should create hazard zones.");

            Assert.Less(invader1.Health.Current, hp1Before, "Invader 1 should have received damage from forbidden void spells.");
            Assert.Less(invader2.Health.Current, hp2Before, "Invader 2 should have received chained/aoe damage.");
        }
        [UnityTest]
        public IEnumerator LichLord_BranchE_NecromanticRituals_SummonsAndFormation_Execute()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 95, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch E — Necromantic Rituals", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_41", // Reanimate Fallen
                    "lich_lord_active_42", // Tomb Relay
                    "lich_lord_active_43", // Legion Formation
                    "lich_lord_active_45", // Boneshield
                    "lich_lord_active_48"  // Death Pact
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            // 1. Cast Slot 0 (Reanimate Fallen) -> Spawns minion
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Reanimate Fallen) should activate.");
            yield return new WaitForSeconds(0.15f);

            var minions = Object.FindObjectsByType<UndeadMinionActor>(FindObjectsSortMode.None);
            Assert.GreaterOrEqual(minions.Length, 1, "Reanimate Fallen should spawn at least 1 minion.");
            var initialMinionPos = minions[0].Position;

            // 2. Cast Slot 1 (Tomb Relay) -> Relocates summons
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Tomb Relay) should activate.");
            yield return new WaitForSeconds(0.15f);

            // Minion position should have changed due to relay
            Assert.AreNotEqual(initialMinionPos, minions[0].Position, "Tomb Relay should move minions to target location.");

            // 3. Cast Slot 2 (Legion Formation)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Legion Formation) should activate.");
            yield return new WaitForSeconds(0.15f);

            // 4. Cast Slot 3 (Boneshield) -> Grants barrier to player
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (Boneshield) should activate.");
            yield return new WaitForSeconds(0.15f);
            Assert.Greater(_player.BarrierAmount, 0f, "Boneshield should grant barrier absorption to player.");

            // 5. Cast Slot 4 (Death Pact) -> Sacrifices minion, heals player, restores essence
            // Deal 100 damage so 70 is absorbed by Boneshield and remaining penetrates to health
            _player.ReceiveDamage(100f);
            float hpBeforeSacrifice = _player.Health.Current;
            Assert.Less(hpBeforeSacrifice, _player.Health.Max, "Player should have taken health damage after barrier broke.");
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(4), "Slot 4 (Death Pact) should activate.");
            yield return new WaitForSeconds(0.15f);
            Assert.Greater(_player.Health.Current, hpBeforeSacrifice, "Death Pact should heal the player upon sacrificing minion.");
        }

        [UnityTest]
        public IEnumerator LichLord_BranchE_SoulFurnace_CorpseExplosion_CrownOfSilence_CastAndDamage()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.LichLordId, 100, profile);
            DevHarness.UnlockBranch(CharacterCatalog.LichLordId, "Branch E — Necromantic Rituals", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.LichLordId,
                new List<string>
                {
                    "lich_lord_active_44", // Soul Furnace
                    "lich_lord_active_46", // Corpse Explosion
                    "lich_lord_active_47", // Soul Anchor
                    "lich_lord_active_49", // Necropolis Gate
                    "lich_lord_active_50"  // Crown of Silence
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            // Spawn boss so it absorbs full damage
            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            // 1. Cast Slot 0 (Soul Furnace)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Soul Furnace) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 2. Cast Slot 1 (Corpse Explosion)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Corpse Explosion) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 3. Cast Slot 2 (Soul Anchor)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Soul Anchor) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 4. Cast Slot 3 (Necropolis Gate)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (Necropolis Gate) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 5. Cast Slot 4 (Crown of Silence)
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(4), "Slot 4 (Crown of Silence) should activate.");
            yield return new WaitForSeconds(0.3f);

            var hazards = Object.FindObjectsByType<HazardZoneActor>();
            Assert.GreaterOrEqual(hazards.Length, 3, "Soul Furnace, Soul Anchor, and Necropolis Gate should produce hazard zones.");

            Assert.Less(invader.Health.Current, startHp, "Invader should have received damage from necrotic ritual spells.");
            Assert.IsTrue(invader.IsSlowed, "Invader should be slowed by Crown of Silence / rituals.");
        }

        // =========================================================================
        // CURSED GOLEM (Batch 6 / Cursed Golem Batch 1)
        // =========================================================================

        [UnityTest]
        public IEnumerator CursedGolem_BranchA_Part1_ColossalPhysicalAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch A — Colossal Physical Combat", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_01", // Titan Punch
                    "cursed_golem_active_02", // Stonebreaker
                    "cursed_golem_active_03", // Boulder Uppercut
                    "cursed_golem_active_04", // Mountain Slam
                    "cursed_golem_active_05"  // Granite Sweep
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch A physical abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchA_Part2_ColossalPhysicalAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch A — Colossal Physical Combat", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_06", // Crushing Charge
                    "cursed_golem_active_07", // Bedrock Hammer
                    "cursed_golem_active_08", // Colossus Grip
                    "cursed_golem_active_09", // Double Fist Impact
                    "cursed_golem_active_10"  // Worldbreaker Punch
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch A physical abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_Ultimates_ExecuteAndGrantBarriersAndDisplacement()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 250, profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_ultimate_50",  // Colossus Awakening
                    "cursed_golem_ultimate_100", // Faultline Dominion
                    "cursed_golem_ultimate_150", // Heart of the Mountain
                    "cursed_golem_ultimate_200", // Gravitic Cataclysm
                    "cursed_golem_ultimate_250"  // Worldstone Avatar
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 1.5f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            // 1. Ultimate 50 (Colossus Awakening) -> 120 Barrier
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(0), "Slot 0 (Colossus Awakening) should activate.");
            Assert.GreaterOrEqual(_player.BarrierAmount, 120f, "Colossus Awakening should grant at least 120 barrier.");
            yield return new WaitForSeconds(0.1f);

            // 2. Ultimate 100 (Faultline Dominion) -> Slows enemies
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(1), "Slot 1 (Faultline Dominion) should activate.");
            Assert.IsTrue(invader.IsSlowed, "Faultline Dominion should slow hit invaders.");
            yield return new WaitForSeconds(0.1f);

            // 3. Ultimate 150 (Heart of the Mountain) -> 160 Barrier & Heal
            // Damage player first
            _player.ReceiveDamage(150f);
            float hpBeforeHeal = _player.Health.Current;
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(2), "Slot 2 (Heart of the Mountain) should activate.");
            Assert.GreaterOrEqual(_player.BarrierAmount, 160f, "Heart of the Mountain should grant at least 160 barrier.");
            Assert.GreaterOrEqual(_player.Health.Current, hpBeforeHeal, "Heart of the Mountain should restore health.");
            yield return new WaitForSeconds(0.1f);

            // 4. Ultimate 200 (Gravitic Cataclysm) -> Vortex damage & pull
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(3), "Slot 3 (Gravitic Cataclysm) should activate.");
            yield return new WaitForSeconds(0.1f);

            // 5. Ultimate 250 (Worldstone Avatar) -> 200 Barrier & Massive Damage
            GameSession.Run.Hud.AddResource(100f);
            Assert.IsTrue(loadout.TryActivateSlot(4), "Slot 4 (Worldstone Avatar) should activate.");
            Assert.GreaterOrEqual(_player.BarrierAmount, 200f, "Worldstone Avatar should grant at least 200 barrier.");
            yield return new WaitForSeconds(0.2f);

            Assert.Less(invader.Health.Current, startHp, "Invader should have received heavy damage from ultimates.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchB_Part1_EarthAndStoneAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch B — Earth and Stone Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_11", // Stone Spike
                    "cursed_golem_active_12", // Boulder Throw
                    "cursed_golem_active_13", // Rockfall
                    "cursed_golem_active_14", // Stone Wall
                    "cursed_golem_active_15"  // Earth Pillar
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem Branch B slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 50f, "Stone Wall should grant barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch B earth abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchB_Part2_EarthAndStoneAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch B — Earth and Stone Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_16", // Rolling Boulder
                    "cursed_golem_active_17", // Crystal Eruption
                    "cursed_golem_active_18", // Tremor Field
                    "cursed_golem_active_19", // Living Stone
                    "cursed_golem_active_20"  // Mountain's Wrath
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem Branch B slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            var minions = Object.FindObjectsByType<UndeadMinionActor>();
            Assert.GreaterOrEqual(minions.Length, 1, "Living Stone should summon a stone fragment construct.");

            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch B earth abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchC_Part1_AncientRunicAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch C — Ancient Runic Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_21", // Rune of Crushing
                    "cursed_golem_active_22", // Rune of Protection
                    "cursed_golem_active_23", // Rune of Binding
                    "cursed_golem_active_24", // Rune of Reflection
                    "cursed_golem_active_25"  // Rune of Detonation
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem Branch C slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 60f, "Rune of Protection/Reflection should grant barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch C runic abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchC_Part2_AncientRunicAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch C — Ancient Runic Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_26", // Rune of Reinforcement
                    "cursed_golem_active_27", // Rune of Reversal
                    "cursed_golem_active_28", // Rune of Resonance
                    "cursed_golem_active_29", // Ancient Rune Circle
                    "cursed_golem_active_30"  // Runeheart Awakening
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem Branch C slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 100f, "Runeheart Awakening should grant massive barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch C runic abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchD_Part1_FortressAndDefenseAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch D — Fortress and Defense", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_31", // Granite Skin
                    "cursed_golem_active_32", // Living Fortress
                    "cursed_golem_active_33", // Cursed Regeneration
                    "cursed_golem_active_34", // Obsidian Shell
                    "cursed_golem_active_35"  // Retaliation Quake
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem Branch D slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 60f, "Granite Skin / Living Fortress / Obsidian Shell should grant barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch D fortress abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchD_Part2_FortressAndDefenseAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch D — Fortress and Defense", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_36", // Stoneheart Guard
                    "cursed_golem_active_37", // Immovable Titan
                    "cursed_golem_active_38", // Fortress Roar
                    "cursed_golem_active_39", // Ancient Reassembly
                    "cursed_golem_active_40"  // Adamantine Form
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem Branch D slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 150f, "Adamantine Form / Ancient Reassembly should grant massive barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch D fortress abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchE_Part1_GravityAndCataclysmAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch E — Gravity and Cataclysm", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_41", // Gravity Pull
                    "cursed_golem_active_42", // Crushing Field
                    "cursed_golem_active_43", // Meteoric Fist
                    "cursed_golem_active_44", // Gravitic Pulse
                    "cursed_golem_active_45"  // Stone Orbit
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem Branch E slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 70f, "Stone Orbit should grant barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch E gravity abilities.");
        }

        [UnityTest]
        public IEnumerator CursedGolem_BranchE_Part2_GravityAndCataclysmAbilities_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.CursedGolemId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.CursedGolemId, "Branch E — Gravity and Cataclysm", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.CursedGolemId,
                new List<string>
                {
                    "cursed_golem_active_46", // Gravity Prison
                    "cursed_golem_active_47", // Falling Mountain
                    "cursed_golem_active_48", // Worldstone Core
                    "cursed_golem_active_49", // Titan's Judgment
                    "cursed_golem_active_50"  // Colossus Ascension
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Cursed Golem Branch E slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 160f, "Colossus Ascension should grant massive barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Cursed Golem Branch E gravity and cataclysm abilities.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchA_Part1_InfernalSwordsmanship_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch A — Infernal Swordsmanship", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_01", // Infernal Cleave
                    "demon_monarch_active_02", // Hellbrand Thrust
                    "demon_monarch_active_03", // Flaming Arc
                    "demon_monarch_active_04", // Ember Uppercut
                    "demon_monarch_active_05"  // Blazing Riposte
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch A slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 60f, "Blazing Riposte should grant barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch A swordsmanship abilities.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchA_Part2_InfernalSwordsmanship_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch A — Infernal Swordsmanship", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_06", // Molten Lunge
                    "demon_monarch_active_07", // Burning Whirl
                    "demon_monarch_active_08", // Volcanic Slash
                    "demon_monarch_active_09", // Sundering Edge
                    "demon_monarch_active_10"  // Royal Execution
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch A slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch A swordsmanship abilities.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_Ultimates_ExecuteAndGrantBarriersAndDisplacement()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 250, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Milestone Ultimates", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_ultimate_50",
                    "demon_monarch_ultimate_100",
                    "demon_monarch_ultimate_150",
                    "demon_monarch_ultimate_200",
                    "demon_monarch_ultimate_250"
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2.5f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch ultimate slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 250f, "Throne of Eternal Flame should grant 250 barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch ultimates.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchB_Part1_HellfireMagic_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch B — Hellfire Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_11", // Hellfire Wave
                    "demon_monarch_active_12", // Magma Fissure
                    "demon_monarch_active_13", // Cinder Rain
                    "demon_monarch_active_14", // Inferno Spiral
                    "demon_monarch_active_15"  // Pyre Ring
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch B slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch B hellfire abilities.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchB_Part2_HellfireMagic_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch B — Hellfire Magic", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_16", // Flame Vortex
                    "demon_monarch_active_17", // Molten Chains
                    "demon_monarch_active_18", // Ember Lance
                    "demon_monarch_active_19", // Searing Brand
                    "demon_monarch_active_20"  // Cataclysm Fire
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch B slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch B hellfire abilities.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchC_Part1_DemonicPowers_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch C — Demonic Powers", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_21", // Throne of Embers
                    "demon_monarch_active_22", // Hellstep
                    "demon_monarch_active_23", // Ash Doppelganger
                    "demon_monarch_active_24", // Bloodforged Blade
                    "demon_monarch_active_25"  // Furnace Heart
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch C slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 25f, "Demonic powers should grant defensive barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch C demonic abilities.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchC_Part2_DemonicPowers_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch C — Demonic Powers", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_26", // Terror Roar
                    "demon_monarch_active_27", // Infernal Grasp
                    "demon_monarch_active_28", // Blasphemous Banner
                    "demon_monarch_active_29", // Wrath Magnet
                    "demon_monarch_active_30"  // Demonic Standard
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch C slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 30f, "Blasphemous Banner should grant protective barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch C demonic abilities.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchD_Part1_DevastatingPhysicalAttacks_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch D — Devastating Physical Attacks", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_31", // Brutal Smash
                    "demon_monarch_active_32", // Titanic Swing
                    "demon_monarch_active_33", // Rupture Stomp
                    "demon_monarch_active_34", // Colossal Crush
                    "demon_monarch_active_35"  // Iron Sledge
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch D slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 40f, "Iron Sledge should grant protective iron barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch D physical attacks.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchD_Part2_DevastatingPhysicalAttacks_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch D — Devastating Physical Attacks", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_36", // Devastating Quake
                    "demon_monarch_active_37", // Bone Pulverizer
                    "demon_monarch_active_38", // Brutal Cleaver
                    "demon_monarch_active_39", // Sunder the Earth
                    "demon_monarch_active_40"  // Wrathful Guillotine
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch D slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 50f, "Wrathful Guillotine should grant execution barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch D physical attacks.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchE_Part1_InfernalAscension_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch E — Infernal Ascension", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_41", // Crown of Flames
                    "demon_monarch_active_42", // Wing Buffet
                    "demon_monarch_active_43", // Demonic Ascendance
                    "demon_monarch_active_44", // Molten Halo
                    "demon_monarch_active_45"  // Sovereign Glare
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch E slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 30f, "Crown of Flames / Demonic Ascendance should grant barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch E ascension abilities.");
        }

        [UnityTest]
        public IEnumerator DemonMonarch_BranchE_Part2_InfernalAscension_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.DemonMonarchId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.DemonMonarchId, "Branch E — Infernal Ascension", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.DemonMonarchId,
                new List<string>
                {
                    "demon_monarch_active_46", // Overlord's Wrath
                    "demon_monarch_active_47", // Hellfire Nova
                    "demon_monarch_active_48", // Pillar of Damnation
                    "demon_monarch_active_49", // Crown of the Abyss
                    "demon_monarch_active_50"  // Infernal Ascension
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Demon Monarch Branch E slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 60f, "Crown of the Abyss / Infernal Ascension should grant major barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Demon Monarch Branch E ascension abilities.");
        }

        [UnityTest]
        public IEnumerator VampireQueen_BranchA_Part1_BloodSorcery_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.VampireQueenId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.VampireQueenId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.VampireQueenId, "Branch A — Blood Sorcery", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.VampireQueenId,
                new List<string>
                {
                    "vampire_queen_active_01", // Sanguine Burst
                    "vampire_queen_active_02", // Crimson Lance
                    "vampire_queen_active_03", // Blood Well
                    "vampire_queen_active_04", // Transfusion
                    "vampire_queen_active_05"  // Sanguine Ward
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Vampire Queen Branch A slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 35f, "Sanguine Ward should grant protective barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Vampire Queen Branch A blood abilities.");
        }

        [UnityTest]
        public IEnumerator VampireQueen_BranchA_Part2_BloodSorcery_ExecuteAndDamageInvaders()
        {
            yield return StartArena(CharacterCatalog.VampireQueenId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.VampireQueenId, 50, profile);
            DevHarness.UnlockBranch(CharacterCatalog.VampireQueenId, "Branch A — Blood Sorcery", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.VampireQueenId,
                new List<string>
                {
                    "vampire_queen_active_06", // Blood Boil
                    "vampire_queen_active_07", // Exsanguinate
                    "vampire_queen_active_08", // Crimson Tempest
                    "vampire_queen_active_09", // Haemomancy
                    "vampire_queen_active_10"  // Heartstopper
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Vampire Queen Branch A slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Vampire Queen Branch A blood abilities.");
        }

        [UnityTest]
        public IEnumerator VampireQueen_AllFiveUltimates_ExecuteSuccessfully()
        {
            yield return StartArena(CharacterCatalog.VampireQueenId);

            var profile = SaveStorage.Load();
            DevHarness.SetCharacterLevel(CharacterCatalog.VampireQueenId, 250, profile);
            DevHarness.UnlockBranch(CharacterCatalog.VampireQueenId, "Milestone Ultimates", profile);
            DevHarness.EquipLoadout(
                CharacterCatalog.VampireQueenId,
                new List<string>
                {
                    "vampire_queen_ultimate_50",  // Crimson Masquerade
                    "vampire_queen_ultimate_100", // Blood Moon Ascension
                    "vampire_queen_ultimate_150", // Sovereign Feast
                    "vampire_queen_ultimate_200", // Grand Vampire Court
                    "vampire_queen_ultimate_250"  // Apocalypse of Scarlet
                },
                profile,
                activeRunLoadout: GameSession.Run.Loadout
            );
            SaveStorage.Save(profile);
            SaveStorage.CurrentProfile = profile;

            var loadout = _player.GetComponent<AbilityLoadoutController>();
            Assert.IsNotNull(loadout);
            loadout.SyncLoadoutFromRun();

            var invader = InvaderActor.Spawn(_player.Position + Vector2.right * 2f, InvaderArchetype.RoyalChampion);
            float startHp = invader.Health.Current;

            for (int slot = 0; slot < 5; slot++)
            {
                GameSession.Run.Hud.AddResource(100f);
                Assert.IsTrue(loadout.TryActivateSlot(slot), $"Vampire Queen Ultimate slot {slot} should activate.");
                yield return new WaitForSeconds(0.1f);
            }

            Assert.GreaterOrEqual(_player.BarrierAmount, 100f, "Vampire Queen ultimates should grant massive barrier.");
            Assert.Less(invader.Health.Current, startHp, "Invader should take damage from Vampire Queen ultimates.");
        }
    }
}
