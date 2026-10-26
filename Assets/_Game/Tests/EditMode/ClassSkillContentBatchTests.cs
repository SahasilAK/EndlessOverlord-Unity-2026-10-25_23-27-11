using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class ClassSkillContentBatchTests
    {
        [Test]
        public void LichLord_BranchA_AllTenAbilitiesRegisteredAndValid()
        {
            var branchAIds = new[]
            {
                "lich_lord_active_01",
                "lich_lord_active_02",
                "lich_lord_active_03",
                "lich_lord_active_04",
                "lich_lord_active_05",
                "lich_lord_active_06",
                "lich_lord_active_07",
                "lich_lord_active_08",
                "lich_lord_active_09",
                "lich_lord_active_10"
            };

            for (int i = 0; i < branchAIds.Length; i++)
            {
                string id = branchAIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch A — Undead Summoning", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsTrue(def.Effects.HasFlag(AbilityEffectType.Summon));
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                // Milestone rank checks
                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void LichLord_AllFiveUltimatesRegistered_Levels50Through250()
        {
            var ultimateIds = new[]
            {
                ("lich_lord_ultimate_50", 50),
                ("lich_lord_ultimate_100", 100),
                ("lich_lord_ultimate_150", 150),
                ("lich_lord_ultimate_200", 200),
                ("lich_lord_ultimate_250", 250)
            };

            foreach (var (id, reqLevel) in ultimateIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ultimate {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId);
                Assert.IsTrue(def.IsUltimate);
                Assert.AreEqual(reqLevel, def.RequiredLevel);
                Assert.Greater(def.ResourceCost, 50f);
                Assert.Greater(def.CooldownDuration, 20f);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);
            }
        }

        [Test]
        public void DevHarness_SimulatesProgression_LevelsRanksAndLoadouts()
        {
            var profile = new PlayerSaveProfile();
            string charId = CharacterCatalog.LichLordId;

            // 1. Level grant
            DevHarness.SetCharacterLevel(charId, 100, profile);
            var rec = profile.GetOrAddCharacter(charId);
            Assert.AreEqual(100, rec.Level);

            DevHarness.GrantCharacterLevels(charId, 50, profile);
            rec = profile.GetOrAddCharacter(charId);
            Assert.AreEqual(150, rec.Level);

            // 2. Set ability rank and mastery
            DevHarness.SetAbilityRank(charId, "lich_lord_active_10", rank: 15, masteryXp: 350, ascensionRank: 2, profile: profile);
            rec = profile.GetOrAddCharacter(charId);
            var abilityProg = rec.UnlockedAbilities.Find(a => a.AbilityId == "lich_lord_active_10");
            Assert.IsNotNull(abilityProg);
            Assert.AreEqual(15, abilityProg.Rank);
            Assert.AreEqual(350, abilityProg.MasteryXp);
            Assert.AreEqual(2, abilityProg.AscensionRank);

            // 3. Unlock Branch
            DevHarness.UnlockBranch(charId, "Branch A — Undead Summoning", profile);
            rec = profile.GetOrAddCharacter(charId);
            for (int i = 1; i <= 10; i++)
            {
                string expectedId = $"lich_lord_active_{i:00}";
                Assert.IsTrue(rec.UnlockedAbilities.Exists(a => a.AbilityId == expectedId), $"Branch A ability {expectedId} must be unlocked by harness.");
            }

            // 4. Equip loadout
            var loadout = new List<string>
            {
                "lich_lord_active_01",
                "lich_lord_active_03",
                "lich_lord_active_07",
                "lich_lord_active_10",
                "lich_lord_ultimate_100"
            };
            DevHarness.EquipLoadout(charId, loadout, profile);
            rec = profile.GetOrAddCharacter(charId);
            for (int slot = 0; slot < 5; slot++)
            {
                Assert.AreEqual(loadout[slot], rec.EquippedLoadout[slot]);
            }
        }

        [Test]
        public void LichLord_ExclusiveWaveSkills_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "lich_lord_wave_01",
                "lich_lord_wave_02",
                "lich_lord_wave_03",
                "lich_lord_wave_04",
                "lich_lord_wave_05",
                "lich_lord_wave_06",
                "lich_lord_wave_07",
                "lich_lord_wave_08",
                "lich_lord_wave_09",
                "lich_lord_wave_10"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId, $"Skill {id} must be exclusive to Lich Lord.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Lich Lord exclusive wave skills
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 20; seed++)
            {
                var demonOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 12345L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < demonOffer.Choices.Count; c++)
                {
                    var choice = demonOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.LichLordId, choice.CharacterId, $"Demon Monarch must never be offered Lich Lord exclusive skill {choice.Id}");
                }
            }

            // Lich Lord drafting CAN receive Lich Lord exclusive skills
            bool foundLichSkill = false;
            for (int seed = 1; seed <= 30; seed++)
            {
                var lichOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 99999L, inventory: inv, characterId: CharacterCatalog.LichLordId);
                for (int c = 0; c < lichOffer.Choices.Count; c++)
                {
                    if (lichOffer.Choices[c].CharacterId == CharacterCatalog.LichLordId)
                    {
                        foundLichSkill = true;
                        break;
                    }
                }
                if (foundLichSkill) break;
            }
            Assert.IsTrue(foundLichSkill, "Lich Lord draft must offer Lich Lord exclusive wave skills.");
        }

        [Test]
        public void LichLord_BranchB_AllTenAbilitiesRegisteredAndValid()
        {
            var branchBIds = new[]
            {
                "lich_lord_active_11",
                "lich_lord_active_12",
                "lich_lord_active_13",
                "lich_lord_active_14",
                "lich_lord_active_15",
                "lich_lord_active_16",
                "lich_lord_active_17",
                "lich_lord_active_18",
                "lich_lord_active_19",
                "lich_lord_active_20"
            };

            for (int i = 0; i < branchBIds.Length; i++)
            {
                string id = branchBIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch B — Frost and Death Magic", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.AreNotEqual(AbilityEffectType.None, def.Effects);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                // Milestone rank checks
                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void LichLord_BranchB_WaveSkills11Through20_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "lich_lord_wave_11",
                "lich_lord_wave_12",
                "lich_lord_wave_13",
                "lich_lord_wave_14",
                "lich_lord_wave_15",
                "lich_lord_wave_16",
                "lich_lord_wave_17",
                "lich_lord_wave_18",
                "lich_lord_wave_19",
                "lich_lord_wave_20"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId, $"Skill {id} must be exclusive to Lich Lord.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Lich Lord exclusive wave skills 11-20
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var demonOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 54321L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < demonOffer.Choices.Count; c++)
                {
                    var choice = demonOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.LichLordId, choice.CharacterId, $"Demon Monarch must never be offered Lich Lord exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void LichLord_BranchC_AllTenAbilitiesRegisteredAndValid()
        {
            var branchCIds = new[]
            {
                "lich_lord_active_21",
                "lich_lord_active_22",
                "lich_lord_active_23",
                "lich_lord_active_24",
                "lich_lord_active_25",
                "lich_lord_active_26",
                "lich_lord_active_27",
                "lich_lord_active_28",
                "lich_lord_active_29",
                "lich_lord_active_30"
            };

            for (int i = 0; i < branchCIds.Length; i++)
            {
                string id = branchCIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch C — Fire, Wind and Thunder Sorcery", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.AreNotEqual(AbilityEffectType.None, def.Effects);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                // Milestone rank checks
                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void LichLord_BranchC_WaveSkills21Through30_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "lich_lord_wave_21",
                "lich_lord_wave_22",
                "lich_lord_wave_23",
                "lich_lord_wave_24",
                "lich_lord_wave_25",
                "lich_lord_wave_26",
                "lich_lord_wave_27",
                "lich_lord_wave_28",
                "lich_lord_wave_29",
                "lich_lord_wave_30"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId, $"Skill {id} must be exclusive to Lich Lord.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Lich Lord exclusive wave skills 21-30
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var demonOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 98765L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < demonOffer.Choices.Count; c++)
                {
                    var choice = demonOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.LichLordId, choice.CharacterId, $"Demon Monarch must never be offered Lich Lord exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void LichLord_BranchD_AllTenAbilitiesRegisteredAndValid()
        {
            var branchDIds = new[]
            {
                "lich_lord_active_31",
                "lich_lord_active_32",
                "lich_lord_active_33",
                "lich_lord_active_34",
                "lich_lord_active_35",
                "lich_lord_active_36",
                "lich_lord_active_37",
                "lich_lord_active_38",
                "lich_lord_active_39",
                "lich_lord_active_40"
            };

            for (int i = 0; i < branchDIds.Length; i++)
            {
                string id = branchDIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch D — Forbidden Dark Magic", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.AreNotEqual(AbilityEffectType.None, def.Effects);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                // Milestone rank checks
                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void LichLord_BranchD_WaveSkills31Through40_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "lich_lord_wave_31",
                "lich_lord_wave_32",
                "lich_lord_wave_33",
                "lich_lord_wave_34",
                "lich_lord_wave_35",
                "lich_lord_wave_36",
                "lich_lord_wave_37",
                "lich_lord_wave_38",
                "lich_lord_wave_39",
                "lich_lord_wave_40"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId, $"Skill {id} must be exclusive to Lich Lord.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Lich Lord exclusive wave skills 31-40
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var demonOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 11223L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < demonOffer.Choices.Count; c++)
                {
                    var choice = demonOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.LichLordId, choice.CharacterId, $"Demon Monarch must never be offered Lich Lord exclusive skill {choice.Id}");
                }
            }
        }
    }
}
