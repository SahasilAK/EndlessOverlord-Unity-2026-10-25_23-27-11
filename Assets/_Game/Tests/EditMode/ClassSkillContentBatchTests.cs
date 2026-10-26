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
        [Test]
        public void LichLord_BranchE_AllTenAbilitiesRegisteredAndValid()
        {
            var branchEIds = new[]
            {
                "lich_lord_active_41",
                "lich_lord_active_42",
                "lich_lord_active_43",
                "lich_lord_active_44",
                "lich_lord_active_45",
                "lich_lord_active_46",
                "lich_lord_active_47",
                "lich_lord_active_48",
                "lich_lord_active_49",
                "lich_lord_active_50"
            };

            for (int i = 0; i < branchEIds.Length; i++)
            {
                string id = branchEIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.LichLordId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch E — Necromantic Rituals", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);

                // Verify milestones
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);
                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void LichLord_BranchE_WaveSkills41Through50_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "lich_lord_wave_41",
                "lich_lord_wave_42",
                "lich_lord_wave_43",
                "lich_lord_wave_44",
                "lich_lord_wave_45",
                "lich_lord_wave_46",
                "lich_lord_wave_47",
                "lich_lord_wave_48",
                "lich_lord_wave_49",
                "lich_lord_wave_50"
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

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Lich Lord exclusive wave skills 41-50
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
        public void LichLord_AllFiftyActiveAbilities_RegisteredAndValid()
        {
            var allLichAbilities = AbilityRegistry.GetForCharacter(CharacterCatalog.LichLordId);
            // 50 branch actives + 5 milestone ultimates = 55 total
            Assert.AreEqual(55, allLichAbilities.Count, "Lich Lord must have 50 active branch abilities and 5 milestone ultimates registered.");
        }

        // =========================================================================
        // CURSED GOLEM (Batch 6 / Cursed Golem Batch 1)
        // =========================================================================

        [Test]
        public void CursedGolem_BranchA_AllTenAbilitiesRegisteredAndValid()
        {
            var branchAIds = new[]
            {
                "cursed_golem_active_01",
                "cursed_golem_active_02",
                "cursed_golem_active_03",
                "cursed_golem_active_04",
                "cursed_golem_active_05",
                "cursed_golem_active_06",
                "cursed_golem_active_07",
                "cursed_golem_active_08",
                "cursed_golem_active_09",
                "cursed_golem_active_10"
            };

            for (int i = 0; i < branchAIds.Length; i++)
            {
                string id = branchAIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch A — Colossal Physical Combat", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsTrue(def.Effects.HasFlag(AbilityEffectType.MeleeArc) || def.Effects.HasFlag(AbilityEffectType.RadialAoE));
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void CursedGolem_AllFiveUltimatesRegistered_Levels50Through250()
        {
            var ultimateIds = new[]
            {
                ("cursed_golem_ultimate_50", 50),
                ("cursed_golem_ultimate_100", 100),
                ("cursed_golem_ultimate_150", 150),
                ("cursed_golem_ultimate_200", 200),
                ("cursed_golem_ultimate_250", 250)
            };

            foreach (var (id, reqLevel) in ultimateIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ultimate {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId);
                Assert.IsTrue(def.IsUltimate);
                Assert.AreEqual(reqLevel, def.RequiredLevel);
                Assert.GreaterOrEqual(def.ResourceCost, 50f);
                Assert.GreaterOrEqual(def.CooldownDuration, 20f);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);
            }
        }

        [Test]
        public void CursedGolem_BranchA_WaveSkills01Through10_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "cursed_golem_wave_01",
                "cursed_golem_wave_02",
                "cursed_golem_wave_03",
                "cursed_golem_wave_04",
                "cursed_golem_wave_05",
                "cursed_golem_wave_06",
                "cursed_golem_wave_07",
                "cursed_golem_wave_08",
                "cursed_golem_wave_09",
                "cursed_golem_wave_10"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId, $"Skill {id} must be exclusive to Cursed Golem.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Lich Lord drafting should NEVER receive Cursed Golem exclusive wave skills 01-10
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var lichOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 98765L, inventory: inv, characterId: CharacterCatalog.LichLordId);
                for (int c = 0; c < lichOffer.Choices.Count; c++)
                {
                    var choice = lichOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.CursedGolemId, choice.CharacterId, $"Lich Lord must never be offered Cursed Golem exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void CursedGolem_BranchB_AllTenAbilitiesRegisteredAndValid()
        {
            var branchBIds = new[]
            {
                "cursed_golem_active_11",
                "cursed_golem_active_12",
                "cursed_golem_active_13",
                "cursed_golem_active_14",
                "cursed_golem_active_15",
                "cursed_golem_active_16",
                "cursed_golem_active_17",
                "cursed_golem_active_18",
                "cursed_golem_active_19",
                "cursed_golem_active_20"
            };

            for (int i = 0; i < branchBIds.Length; i++)
            {
                string id = branchBIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch B — Earth and Stone Magic", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void CursedGolem_BranchB_WaveSkills11Through20_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "cursed_golem_wave_11",
                "cursed_golem_wave_12",
                "cursed_golem_wave_13",
                "cursed_golem_wave_14",
                "cursed_golem_wave_15",
                "cursed_golem_wave_16",
                "cursed_golem_wave_17",
                "cursed_golem_wave_18",
                "cursed_golem_wave_19",
                "cursed_golem_wave_20"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId, $"Skill {id} must be exclusive to Cursed Golem.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Lich Lord drafting should NEVER receive Cursed Golem exclusive wave skills 11-20
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var lichOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 54321L, inventory: inv, characterId: CharacterCatalog.LichLordId);
                for (int c = 0; c < lichOffer.Choices.Count; c++)
                {
                    var choice = lichOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.CursedGolemId, choice.CharacterId, $"Lich Lord must never be offered Cursed Golem exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void CursedGolem_BranchC_AllTenAbilitiesRegisteredAndValid()
        {
            var branchCIds = new[]
            {
                "cursed_golem_active_21",
                "cursed_golem_active_22",
                "cursed_golem_active_23",
                "cursed_golem_active_24",
                "cursed_golem_active_25",
                "cursed_golem_active_26",
                "cursed_golem_active_27",
                "cursed_golem_active_28",
                "cursed_golem_active_29",
                "cursed_golem_active_30"
            };

            for (int i = 0; i < branchCIds.Length; i++)
            {
                string id = branchCIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch C — Ancient Runic Magic", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void CursedGolem_BranchC_WaveSkills21Through30_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "cursed_golem_wave_21",
                "cursed_golem_wave_22",
                "cursed_golem_wave_23",
                "cursed_golem_wave_24",
                "cursed_golem_wave_25",
                "cursed_golem_wave_26",
                "cursed_golem_wave_27",
                "cursed_golem_wave_28",
                "cursed_golem_wave_29",
                "cursed_golem_wave_30"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId, $"Skill {id} must be exclusive to Cursed Golem.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Cursed Golem exclusive wave skills 21-30
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var demonOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 77777L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < demonOffer.Choices.Count; c++)
                {
                    var choice = demonOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.CursedGolemId, choice.CharacterId, $"Demon Monarch must never be offered Cursed Golem exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void CursedGolem_BranchD_AllTenAbilitiesRegisteredAndValid()
        {
            var branchDIds = new[]
            {
                "cursed_golem_active_31",
                "cursed_golem_active_32",
                "cursed_golem_active_33",
                "cursed_golem_active_34",
                "cursed_golem_active_35",
                "cursed_golem_active_36",
                "cursed_golem_active_37",
                "cursed_golem_active_38",
                "cursed_golem_active_39",
                "cursed_golem_active_40"
            };

            for (int i = 0; i < branchDIds.Length; i++)
            {
                string id = branchDIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch D — Fortress and Defense", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void CursedGolem_BranchD_WaveSkills31Through40_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "cursed_golem_wave_31",
                "cursed_golem_wave_32",
                "cursed_golem_wave_33",
                "cursed_golem_wave_34",
                "cursed_golem_wave_35",
                "cursed_golem_wave_36",
                "cursed_golem_wave_37",
                "cursed_golem_wave_38",
                "cursed_golem_wave_39",
                "cursed_golem_wave_40"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId, $"Skill {id} must be exclusive to Cursed Golem.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Cursed Golem exclusive wave skills 31-40
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var demonOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 88888L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < demonOffer.Choices.Count; c++)
                {
                    var choice = demonOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.CursedGolemId, choice.CharacterId, $"Demon Monarch must never be offered Cursed Golem exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void CursedGolem_BranchE_AllTenAbilitiesRegisteredAndValid()
        {
            var branchEIds = new[]
            {
                "cursed_golem_active_41",
                "cursed_golem_active_42",
                "cursed_golem_active_43",
                "cursed_golem_active_44",
                "cursed_golem_active_45",
                "cursed_golem_active_46",
                "cursed_golem_active_47",
                "cursed_golem_active_48",
                "cursed_golem_active_49",
                "cursed_golem_active_50"
            };

            for (int i = 0; i < branchEIds.Length; i++)
            {
                string id = branchEIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch E — Gravity and Cataclysm", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void CursedGolem_BranchE_WaveSkills41Through50_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "cursed_golem_wave_41",
                "cursed_golem_wave_42",
                "cursed_golem_wave_43",
                "cursed_golem_wave_44",
                "cursed_golem_wave_45",
                "cursed_golem_wave_46",
                "cursed_golem_wave_47",
                "cursed_golem_wave_48",
                "cursed_golem_wave_49",
                "cursed_golem_wave_50"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.CursedGolemId, def.CharacterId, $"Skill {id} must be exclusive to Cursed Golem.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Cursed Golem exclusive wave skills 41-50
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var demonOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 99999L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < demonOffer.Choices.Count; c++)
                {
                    var choice = demonOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.CursedGolemId, choice.CharacterId, $"Demon Monarch must never be offered Cursed Golem exclusive skill {choice.Id}");
                }
            }
        }

        // =========================================================================
        // DEMON MONARCH (Milestone P10 Batches 11-15)
        // =========================================================================

        [Test]
        public void DemonMonarch_BranchA_AllTenAbilitiesRegisteredAndValid()
        {
            var branchAIds = new[]
            {
                "demon_monarch_active_01",
                "demon_monarch_active_02",
                "demon_monarch_active_03",
                "demon_monarch_active_04",
                "demon_monarch_active_05",
                "demon_monarch_active_06",
                "demon_monarch_active_07",
                "demon_monarch_active_08",
                "demon_monarch_active_09",
                "demon_monarch_active_10"
            };

            for (int i = 0; i < branchAIds.Length; i++)
            {
                string id = branchAIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch A — Infernal Swordsmanship", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void DemonMonarch_AllFiveUltimatesRegistered_Levels50Through250()
        {
            var ultimateIds = new[]
            {
                ("demon_monarch_ultimate_50", 50),
                ("demon_monarch_ultimate_100", 100),
                ("demon_monarch_ultimate_150", 150),
                ("demon_monarch_ultimate_200", 200),
                ("demon_monarch_ultimate_250", 250)
            };

            foreach (var (id, reqLevel) in ultimateIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ultimate {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId);
                Assert.IsTrue(def.IsUltimate);
                Assert.AreEqual(reqLevel, def.RequiredLevel);
                Assert.GreaterOrEqual(def.ResourceCost, 50f);
                Assert.GreaterOrEqual(def.CooldownDuration, 20f);
                Assert.IsNotEmpty(def.Name);
                Assert.AreEqual("Milestone Ultimates", def.BranchName);
                Assert.AreEqual(4, def.Milestones.Count);
            }
        }

        [Test]
        public void DemonMonarch_BranchA_WaveSkills01Through10_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "demon_monarch_wave_01",
                "demon_monarch_wave_02",
                "demon_monarch_wave_03",
                "demon_monarch_wave_04",
                "demon_monarch_wave_05",
                "demon_monarch_wave_06",
                "demon_monarch_wave_07",
                "demon_monarch_wave_08",
                "demon_monarch_wave_09",
                "demon_monarch_wave_10"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId, $"Skill {id} must be exclusive to Demon Monarch.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Cursed Golem drafting should NEVER receive Demon Monarch exclusive wave skills 01-10
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var golemOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 10101L, inventory: inv, characterId: CharacterCatalog.CursedGolemId);
                for (int c = 0; c < golemOffer.Choices.Count; c++)
                {
                    var choice = golemOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.DemonMonarchId, choice.CharacterId, $"Cursed Golem must never be offered Demon Monarch exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void DemonMonarch_BranchB_AllTenAbilitiesRegisteredAndValid()
        {
            var branchBIds = new[]
            {
                "demon_monarch_active_11",
                "demon_monarch_active_12",
                "demon_monarch_active_13",
                "demon_monarch_active_14",
                "demon_monarch_active_15",
                "demon_monarch_active_16",
                "demon_monarch_active_17",
                "demon_monarch_active_18",
                "demon_monarch_active_19",
                "demon_monarch_active_20"
            };

            for (int i = 0; i < branchBIds.Length; i++)
            {
                string id = branchBIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch B — Hellfire Magic", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void DemonMonarch_BranchB_WaveSkills11Through20_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "demon_monarch_wave_11",
                "demon_monarch_wave_12",
                "demon_monarch_wave_13",
                "demon_monarch_wave_14",
                "demon_monarch_wave_15",
                "demon_monarch_wave_16",
                "demon_monarch_wave_17",
                "demon_monarch_wave_18",
                "demon_monarch_wave_19",
                "demon_monarch_wave_20"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId, $"Skill {id} must be exclusive to Demon Monarch.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Cursed Golem drafting should NEVER receive Demon Monarch exclusive wave skills 11-20
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var golemOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 20202L, inventory: inv, characterId: CharacterCatalog.CursedGolemId);
                for (int c = 0; c < golemOffer.Choices.Count; c++)
                {
                    var choice = golemOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.DemonMonarchId, choice.CharacterId, $"Cursed Golem must never be offered Demon Monarch exclusive skill {choice.Id}");
                }
            }
        }

        // =========================================================================
        // DEMON MONARCH (Batch 13 / Demon Monarch Branch C)
        // =========================================================================

        [Test]
        public void DemonMonarch_BranchC_AllTenAbilitiesRegisteredAndValid()
        {
            var branchCIds = new[]
            {
                "demon_monarch_active_21",
                "demon_monarch_active_22",
                "demon_monarch_active_23",
                "demon_monarch_active_24",
                "demon_monarch_active_25",
                "demon_monarch_active_26",
                "demon_monarch_active_27",
                "demon_monarch_active_28",
                "demon_monarch_active_29",
                "demon_monarch_active_30"
            };

            for (int i = 0; i < branchCIds.Length; i++)
            {
                string id = branchCIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch C — Demonic Powers", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void DemonMonarch_BranchC_WaveSkills21Through30_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "demon_monarch_wave_21",
                "demon_monarch_wave_22",
                "demon_monarch_wave_23",
                "demon_monarch_wave_24",
                "demon_monarch_wave_25",
                "demon_monarch_wave_26",
                "demon_monarch_wave_27",
                "demon_monarch_wave_28",
                "demon_monarch_wave_29",
                "demon_monarch_wave_30"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId, $"Skill {id} must be exclusive to Demon Monarch.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Cursed Golem drafting should NEVER receive Demon Monarch exclusive wave skills 21-30
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var golemOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 30303L, inventory: inv, characterId: CharacterCatalog.CursedGolemId);
                for (int c = 0; c < golemOffer.Choices.Count; c++)
                {
                    var choice = golemOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.DemonMonarchId, choice.CharacterId, $"Cursed Golem must never be offered Demon Monarch exclusive skill {choice.Id}");
                }
            }
        }

        // =========================================================================
        // DEMON MONARCH (Batch 14 / Demon Monarch Branch D)
        // =========================================================================

        [Test]
        public void DemonMonarch_BranchD_AllTenAbilitiesRegisteredAndValid()
        {
            var branchDIds = new[]
            {
                "demon_monarch_active_31",
                "demon_monarch_active_32",
                "demon_monarch_active_33",
                "demon_monarch_active_34",
                "demon_monarch_active_35",
                "demon_monarch_active_36",
                "demon_monarch_active_37",
                "demon_monarch_active_38",
                "demon_monarch_active_39",
                "demon_monarch_active_40"
            };

            for (int i = 0; i < branchDIds.Length; i++)
            {
                string id = branchDIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch D — Devastating Physical Attacks", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void DemonMonarch_BranchD_WaveSkills31Through40_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "demon_monarch_wave_31",
                "demon_monarch_wave_32",
                "demon_monarch_wave_33",
                "demon_monarch_wave_34",
                "demon_monarch_wave_35",
                "demon_monarch_wave_36",
                "demon_monarch_wave_37",
                "demon_monarch_wave_38",
                "demon_monarch_wave_39",
                "demon_monarch_wave_40"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId, $"Skill {id} must be exclusive to Demon Monarch.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Cursed Golem drafting should NEVER receive Demon Monarch exclusive wave skills 31-40
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var golemOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 40404L, inventory: inv, characterId: CharacterCatalog.CursedGolemId);
                for (int c = 0; c < golemOffer.Choices.Count; c++)
                {
                    var choice = golemOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.DemonMonarchId, choice.CharacterId, $"Cursed Golem must never be offered Demon Monarch exclusive skill {choice.Id}");
                }
            }
        }

        // =========================================================================
        // DEMON MONARCH (Batch 15 / Demon Monarch Branch E - 100% COMPLETE)
        // =========================================================================

        [Test]
        public void DemonMonarch_BranchE_AllTenAbilitiesRegisteredAndValid()
        {
            var branchEIds = new[]
            {
                "demon_monarch_active_41",
                "demon_monarch_active_42",
                "demon_monarch_active_43",
                "demon_monarch_active_44",
                "demon_monarch_active_45",
                "demon_monarch_active_46",
                "demon_monarch_active_47",
                "demon_monarch_active_48",
                "demon_monarch_active_49",
                "demon_monarch_active_50"
            };

            for (int i = 0; i < branchEIds.Length; i++)
            {
                string id = branchEIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch E — Infernal Ascension", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void DemonMonarch_BranchE_WaveSkills41Through50_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "demon_monarch_wave_41",
                "demon_monarch_wave_42",
                "demon_monarch_wave_43",
                "demon_monarch_wave_44",
                "demon_monarch_wave_45",
                "demon_monarch_wave_46",
                "demon_monarch_wave_47",
                "demon_monarch_wave_48",
                "demon_monarch_wave_49",
                "demon_monarch_wave_50"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.DemonMonarchId, def.CharacterId, $"Skill {id} must be exclusive to Demon Monarch.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Cursed Golem drafting should NEVER receive Demon Monarch exclusive wave skills 41-50
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var golemOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 50505L, inventory: inv, characterId: CharacterCatalog.CursedGolemId);
                for (int c = 0; c < golemOffer.Choices.Count; c++)
                {
                    var choice = golemOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.DemonMonarchId, choice.CharacterId, $"Cursed Golem must never be offered Demon Monarch exclusive skill {choice.Id}");
                }
            }
        }

        // =========================================================================
        // VAMPIRE QUEEN (Batch 16 / Vampire Queen Branch A & Ultimates)
        // =========================================================================

        [Test]
        public void VampireQueen_BranchA_AllTenAbilitiesRegisteredAndValid()
        {
            var branchAIds = new[]
            {
                "vampire_queen_active_01",
                "vampire_queen_active_02",
                "vampire_queen_active_03",
                "vampire_queen_active_04",
                "vampire_queen_active_05",
                "vampire_queen_active_06",
                "vampire_queen_active_07",
                "vampire_queen_active_08",
                "vampire_queen_active_09",
                "vampire_queen_active_10"
            };

            for (int i = 0; i < branchAIds.Length; i++)
            {
                string id = branchAIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch A — Blood Sorcery", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void VampireQueen_AllFiveUltimatesRegistered_Levels50Through250()
        {
            var ultimateIds = new[]
            {
                ("vampire_queen_ultimate_50", 50),
                ("vampire_queen_ultimate_100", 100),
                ("vampire_queen_ultimate_150", 150),
                ("vampire_queen_ultimate_200", 200),
                ("vampire_queen_ultimate_250", 250)
            };

            foreach (var (id, reqLevel) in ultimateIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ultimate {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId);
                Assert.IsTrue(def.IsUltimate);
                Assert.AreEqual(reqLevel, def.RequiredLevel);
                Assert.AreEqual("Milestone Ultimates", def.BranchName);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.AreEqual(4, def.Milestones.Count);
            }
        }

        [Test]
        public void VampireQueen_BranchA_WaveSkills01Through10_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "vampire_queen_wave_01",
                "vampire_queen_wave_02",
                "vampire_queen_wave_03",
                "vampire_queen_wave_04",
                "vampire_queen_wave_05",
                "vampire_queen_wave_06",
                "vampire_queen_wave_07",
                "vampire_queen_wave_08",
                "vampire_queen_wave_09",
                "vampire_queen_wave_10"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId, $"Skill {id} must be exclusive to Vampire Queen.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Vampire Queen exclusive wave skills 01-10
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var monarchOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 60606L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < monarchOffer.Choices.Count; c++)
                {
                    var choice = monarchOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.VampireQueenId, choice.CharacterId, $"Demon Monarch must never be offered Vampire Queen exclusive skill {choice.Id}");
                }
            }
        }

        // =========================================================================
        // VAMPIRE QUEEN (Batch 17 / Vampire Queen Branch B)
        // =========================================================================

        [Test]
        public void VampireQueen_BranchB_AllTenAbilitiesRegisteredAndValid()
        {
            var branchBIds = new[]
            {
                "vampire_queen_active_11",
                "vampire_queen_active_12",
                "vampire_queen_active_13",
                "vampire_queen_active_14",
                "vampire_queen_active_15",
                "vampire_queen_active_16",
                "vampire_queen_active_17",
                "vampire_queen_active_18",
                "vampire_queen_active_19",
                "vampire_queen_active_20"
            };

            for (int i = 0; i < branchBIds.Length; i++)
            {
                string id = branchBIds[i];
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered in AbilityRegistry.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch B — Precision & Rapier", def.BranchName);
                Assert.GreaterOrEqual(def.Tier, 1);
                Assert.LessOrEqual(def.Tier, 5);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count, $"Ability {id} must define milestones at ranks 5, 10, 15, and 20.");

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void VampireQueen_BranchB_WaveSkills11Through20_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "vampire_queen_wave_11",
                "vampire_queen_wave_12",
                "vampire_queen_wave_13",
                "vampire_queen_wave_14",
                "vampire_queen_wave_15",
                "vampire_queen_wave_16",
                "vampire_queen_wave_17",
                "vampire_queen_wave_18",
                "vampire_queen_wave_19",
                "vampire_queen_wave_20"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId, $"Skill {id} must be exclusive to Vampire Queen.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Vampire Queen exclusive wave skills 11-20
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var monarchOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 70707L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < monarchOffer.Choices.Count; c++)
                {
                    var choice = monarchOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.VampireQueenId, choice.CharacterId, $"Demon Monarch must never be offered Vampire Queen exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void VampireQueen_BranchC_AllTenAbilitiesRegisteredAndValid()
        {
            var expectedIds = new[]
            {
                "vampire_queen_active_21",
                "vampire_queen_active_22",
                "vampire_queen_active_23",
                "vampire_queen_active_24",
                "vampire_queen_active_25",
                "vampire_queen_active_26",
                "vampire_queen_active_27",
                "vampire_queen_active_28",
                "vampire_queen_active_29",
                "vampire_queen_active_30"
            };

            foreach (var id in expectedIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch C — Swarm & Dominance", def.BranchName);
                Assert.Greater(def.BaseDamage, 0f);
                Assert.Greater(def.BaseRadius, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void VampireQueen_BranchC_WaveSkills21Through30_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "vampire_queen_wave_21",
                "vampire_queen_wave_22",
                "vampire_queen_wave_23",
                "vampire_queen_wave_24",
                "vampire_queen_wave_25",
                "vampire_queen_wave_26",
                "vampire_queen_wave_27",
                "vampire_queen_wave_28",
                "vampire_queen_wave_29",
                "vampire_queen_wave_30"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId, $"Skill {id} must be exclusive to Vampire Queen.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Vampire Queen exclusive wave skills 21-30
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var monarchOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 80808L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < monarchOffer.Choices.Count; c++)
                {
                    var choice = monarchOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.VampireQueenId, choice.CharacterId, $"Demon Monarch must never be offered Vampire Queen exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void VampireQueen_BranchD_AllTenAbilitiesRegisteredAndValid()
        {
            var expectedIds = new[]
            {
                "vampire_queen_active_31",
                "vampire_queen_active_32",
                "vampire_queen_active_33",
                "vampire_queen_active_34",
                "vampire_queen_active_35",
                "vampire_queen_active_36",
                "vampire_queen_active_37",
                "vampire_queen_active_38",
                "vampire_queen_active_39",
                "vampire_queen_active_40"
            };

            foreach (var id in expectedIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch D — Mist & Domination", def.BranchName);
                Assert.Greater(def.BaseDamage, 0f);
                Assert.Greater(def.BaseRadius, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void VampireQueen_BranchD_WaveSkills31Through40_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "vampire_queen_wave_31",
                "vampire_queen_wave_32",
                "vampire_queen_wave_33",
                "vampire_queen_wave_34",
                "vampire_queen_wave_35",
                "vampire_queen_wave_36",
                "vampire_queen_wave_37",
                "vampire_queen_wave_38",
                "vampire_queen_wave_39",
                "vampire_queen_wave_40"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId, $"Skill {id} must be exclusive to Vampire Queen.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Vampire Queen exclusive wave skills 31-40
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var monarchOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 90909L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < monarchOffer.Choices.Count; c++)
                {
                    var choice = monarchOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.VampireQueenId, choice.CharacterId, $"Demon Monarch must never be offered Vampire Queen exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void VampireQueen_BranchE_AllTenAbilitiesRegisteredAndValid()
        {
            var expectedIds = new[]
            {
                "vampire_queen_active_41",
                "vampire_queen_active_42",
                "vampire_queen_active_43",
                "vampire_queen_active_44",
                "vampire_queen_active_45",
                "vampire_queen_active_46",
                "vampire_queen_active_47",
                "vampire_queen_active_48",
                "vampire_queen_active_49",
                "vampire_queen_active_50"
            };

            foreach (var id in expectedIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch E — Forbidden Royal Blood Rituals", def.BranchName);
                Assert.Greater(def.BaseDamage, 0f);
                Assert.Greater(def.BaseRadius, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void VampireQueen_BranchE_WaveSkills41Through50_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "vampire_queen_wave_41",
                "vampire_queen_wave_42",
                "vampire_queen_wave_43",
                "vampire_queen_wave_44",
                "vampire_queen_wave_45",
                "vampire_queen_wave_46",
                "vampire_queen_wave_47",
                "vampire_queen_wave_48",
                "vampire_queen_wave_49",
                "vampire_queen_wave_50"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.VampireQueenId, def.CharacterId, $"Skill {id} must be exclusive to Vampire Queen.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Vampire Queen exclusive wave skills 41-50
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var monarchOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 101010L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < monarchOffer.Choices.Count; c++)
                {
                    var choice = monarchOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.VampireQueenId, choice.CharacterId, $"Demon Monarch must never be offered Vampire Queen exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void ShadowReaper_BranchA_AllTenAbilitiesRegisteredAndValid()
        {
            var expectedIds = new[]
            {
                "shadow_reaper_active_01",
                "shadow_reaper_active_02",
                "shadow_reaper_active_03",
                "shadow_reaper_active_04",
                "shadow_reaper_active_05",
                "shadow_reaper_active_06",
                "shadow_reaper_active_07",
                "shadow_reaper_active_08",
                "shadow_reaper_active_09",
                "shadow_reaper_active_10"
            };

            foreach (var id in expectedIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.ShadowReaperId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch A — Scythe Mastery", def.BranchName);
                Assert.Greater(def.BaseDamage, 0f);
                Assert.Greater(def.BaseRadius, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void ShadowReaper_AllFiveUltimatesRegistered_Levels50Through250()
        {
            var expectedUltimates = new (string id, int level)[]
            {
                ("shadow_reaper_ultimate_50", 50),
                ("shadow_reaper_ultimate_100", 100),
                ("shadow_reaper_ultimate_150", 150),
                ("shadow_reaper_ultimate_200", 200),
                ("shadow_reaper_ultimate_250", 250)
            };

            foreach (var (id, level) in expectedUltimates)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ultimate {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.ShadowReaperId, def.CharacterId);
                Assert.IsTrue(def.IsUltimate);
                Assert.AreEqual(level, def.RequiredLevel);
                Assert.Greater(def.BaseDamage, 0f);
                Assert.Greater(def.BaseRadius, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);
            }
        }

        [Test]
        public void ShadowReaper_BranchA_WaveSkills01Through10_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "shadow_reaper_wave_01",
                "shadow_reaper_wave_02",
                "shadow_reaper_wave_03",
                "shadow_reaper_wave_04",
                "shadow_reaper_wave_05",
                "shadow_reaper_wave_06",
                "shadow_reaper_wave_07",
                "shadow_reaper_wave_08",
                "shadow_reaper_wave_09",
                "shadow_reaper_wave_10"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.ShadowReaperId, def.CharacterId, $"Skill {id} must be exclusive to Shadow Reaper.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Shadow Reaper exclusive wave skills 01-10
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var monarchOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 121212L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < monarchOffer.Choices.Count; c++)
                {
                    var choice = monarchOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.ShadowReaperId, choice.CharacterId, $"Demon Monarch must never be offered Shadow Reaper exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void ShadowReaper_BranchB_AllTenAbilitiesRegisteredAndValid()
        {
            var expectedIds = new[]
            {
                "shadow_reaper_active_11",
                "shadow_reaper_active_12",
                "shadow_reaper_active_13",
                "shadow_reaper_active_14",
                "shadow_reaper_active_15",
                "shadow_reaper_active_16",
                "shadow_reaper_active_17",
                "shadow_reaper_active_18",
                "shadow_reaper_active_19",
                "shadow_reaper_active_20"
            };

            foreach (var id in expectedIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.ShadowReaperId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch B — Shadow Movement", def.BranchName);
                Assert.Greater(def.BaseDamage, 0f);
                Assert.Greater(def.BaseRadius, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void ShadowReaper_BranchB_WaveSkills11Through20_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "shadow_reaper_wave_11",
                "shadow_reaper_wave_12",
                "shadow_reaper_wave_13",
                "shadow_reaper_wave_14",
                "shadow_reaper_wave_15",
                "shadow_reaper_wave_16",
                "shadow_reaper_wave_17",
                "shadow_reaper_wave_18",
                "shadow_reaper_wave_19",
                "shadow_reaper_wave_20"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.ShadowReaperId, def.CharacterId, $"Skill {id} must be exclusive to Shadow Reaper.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Shadow Reaper exclusive wave skills 11-20
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var monarchOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 131313L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < monarchOffer.Choices.Count; c++)
                {
                    var choice = monarchOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.ShadowReaperId, choice.CharacterId, $"Demon Monarch must never be offered Shadow Reaper exclusive skill {choice.Id}");
                }
            }
        }

        [Test]
        public void ShadowReaper_BranchC_AllTenAbilitiesRegisteredAndValid()
        {
            var expectedIds = new[]
            {
                "shadow_reaper_active_21",
                "shadow_reaper_active_22",
                "shadow_reaper_active_23",
                "shadow_reaper_active_24",
                "shadow_reaper_active_25",
                "shadow_reaper_active_26",
                "shadow_reaper_active_27",
                "shadow_reaper_active_28",
                "shadow_reaper_active_29",
                "shadow_reaper_active_30"
            };

            foreach (var id in expectedIds)
            {
                var def = AbilityRegistry.Get(id);
                Assert.IsNotNull(def, $"Ability {id} must be registered.");
                Assert.AreEqual(CharacterCatalog.ShadowReaperId, def.CharacterId);
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.AreEqual("Branch C — Haunting & Shadow Clones", def.BranchName);
                Assert.Greater(def.BaseDamage, 0f);
                Assert.Greater(def.BaseRadius, 0f);
                Assert.Greater(def.CooldownDuration, 0f);
                Assert.Greater(def.ResourceCost, 0f);
                Assert.IsNotNull(def.Milestones);
                Assert.AreEqual(4, def.Milestones.Count);

                Assert.AreEqual(5, def.Milestones[0].Rank);
                Assert.AreEqual(10, def.Milestones[1].Rank);
                Assert.AreEqual(15, def.Milestones[2].Rank);
                Assert.AreEqual(20, def.Milestones[3].Rank);
            }
        }

        [Test]
        public void ShadowReaper_BranchC_WaveSkills21Through30_RegisteredAndDraftExclusivity()
        {
            var expectedWaveSkills = new[]
            {
                "shadow_reaper_wave_21",
                "shadow_reaper_wave_22",
                "shadow_reaper_wave_23",
                "shadow_reaper_wave_24",
                "shadow_reaper_wave_25",
                "shadow_reaper_wave_26",
                "shadow_reaper_wave_27",
                "shadow_reaper_wave_28",
                "shadow_reaper_wave_29",
                "shadow_reaper_wave_30"
            };

            foreach (var id in expectedWaveSkills)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Wave skill {id} must be registered in RewardRegistry.");
                Assert.AreEqual(CharacterCatalog.ShadowReaperId, def.CharacterId, $"Skill {id} must be exclusive to Shadow Reaper.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.Greater(def.ValuePerRank, 0f);
            }

            // Test draft exclusion: Demon Monarch drafting should NEVER receive Shadow Reaper exclusive wave skills 21-30
            var inv = new RewardInventory();
            for (int seed = 1; seed <= 25; seed++)
            {
                var monarchOffer = RewardDraftSystem.GenerateOffer(waveNumber: seed, seed: seed * 131313L, inventory: inv, characterId: CharacterCatalog.DemonMonarchId);
                for (int c = 0; c < monarchOffer.Choices.Count; c++)
                {
                    var choice = monarchOffer.Choices[c];
                    Assert.AreNotEqual(CharacterCatalog.ShadowReaperId, choice.CharacterId, $"Demon Monarch must never be offered Shadow Reaper exclusive skill {choice.Id}");
                }
            }
        }
    }
}
