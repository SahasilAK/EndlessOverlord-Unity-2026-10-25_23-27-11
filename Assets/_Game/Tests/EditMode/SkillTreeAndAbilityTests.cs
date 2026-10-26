using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    [TestFixture]
    public sealed class SkillTreeAndAbilityTests
    {
        [Test]
        public void PointLedger_CalculatesCorrectly_AtLevels1_10_50_AndMigrated()
        {
            // Formula: earned(level) = 2*(level-1) + floor(level/10) for level >= 1
            Assert.AreEqual(0, CharacterSkillTree.CalculateEarnedSkillPoints(1), "Level 1 earns 0 points");
            Assert.AreEqual(2, CharacterSkillTree.CalculateEarnedSkillPoints(2), "Level 2 earns 2 points");
            Assert.AreEqual(19, CharacterSkillTree.CalculateEarnedSkillPoints(10), "Level 10 earns 2*9 + 1 = 19 points");
            Assert.AreEqual(103, CharacterSkillTree.CalculateEarnedSkillPoints(50), "Level 50 earns 2*49 + 5 = 103 points");

            // Available points = max(0, earned - spent)
            Assert.AreEqual(19, CharacterSkillTree.CalculateAvailableSkillPoints(10, 0));
            Assert.AreEqual(14, CharacterSkillTree.CalculateAvailableSkillPoints(10, 5));
            Assert.AreEqual(0, CharacterSkillTree.CalculateAvailableSkillPoints(10, 19));
            Assert.AreEqual(0, CharacterSkillTree.CalculateAvailableSkillPoints(10, 25), "Never negative available points");

            // CharacterRecord property checks
            var rec = new CharacterRecord { CharacterId = CharacterCatalog.LichLordId, Level = 10, SpentSkillPoints = 7 };
            Assert.AreEqual(19, rec.EarnedSkillPoints);
            Assert.AreEqual(12, rec.AvailableSkillPoints);
        }

        [Test]
        public void TreeDependencyUnlock_EnforcesPrerequisitesAndLevel()
        {
            var rec = new CharacterRecord
            {
                CharacterId = CharacterCatalog.LichLordId,
                Level = 10,
                SpentSkillPoints = 0,
                UnlockedAbilities = new List<AbilityProgressRecord>()
            };

            // Tier 1 Bone Sentinel has no prereqs and can be unlocked
            Assert.IsTrue(CharacterSkillTree.CanUnlock("lich_lord_active_01", rec, out string err1), "Tier 1 should unlock");
            Assert.IsTrue(string.IsNullOrEmpty(err1));

            // Tier 2 Crypt Archer requires Bone Sentinel to be unlocked first
            Assert.IsFalse(CharacterSkillTree.CanUnlock("lich_lord_active_02", rec, out string err2), "Tier 2 requires prereq");
            Assert.IsTrue(err2.Contains("Prerequisite") || err2.Contains("unlocked"), $"Expected prereq error, got: {err2}");

            // Unlock Tier 1
            bool spent1 = CharacterSkillTree.TrySpendPoint("lich_lord_active_01", ref rec, out _);
            Assert.IsTrue(spent1);
            Assert.AreEqual(1, CharacterSkillTree.GetAbilityRank(rec, "lich_lord_active_01"));
            Assert.AreEqual(1, rec.SpentSkillPoints);

            // Now Tier 2 can be unlocked
            Assert.IsTrue(CharacterSkillTree.CanUnlock("lich_lord_active_02", rec, out string err3));
            bool spent2 = CharacterSkillTree.TrySpendPoint("lich_lord_active_02", ref rec, out _);
            Assert.IsTrue(spent2);
            Assert.AreEqual(1, CharacterSkillTree.GetAbilityRank(rec, "lich_lord_active_02"));

            // Level 50 Ultimate requires Level 50
            Assert.IsFalse(CharacterSkillTree.CanUnlock("lich_lord_ultimate_50", rec, out string err4), "Ultimate requires Level 50");
            Assert.IsTrue(err4.Contains("level 50"), $"Expected level error, got: {err4}");

            // Raise character level to 50
            rec.Level = 50;
            Assert.IsTrue(CharacterSkillTree.CanUnlock("lich_lord_ultimate_50", rec, out string err5));
            bool spentUlt = CharacterSkillTree.TrySpendPoint("lich_lord_ultimate_50", ref rec, out _);
            Assert.IsTrue(spentUlt);
            Assert.AreEqual(1, CharacterSkillTree.GetAbilityRank(rec, "lich_lord_ultimate_50"));

            // Cross-character unlock rejection
            Assert.IsFalse(CharacterSkillTree.CanUnlock("demon_monarch_active_01", rec, out string errCross), "Cross-character ability rejected");
            Assert.IsTrue(errCross.Contains("belongs to character"), $"Expected character mismatch error, got: {errCross}");
        }

        [Test]
        public void AbilityLoadout_EnforcesFiveSlotLimit_AndRejectsInvalidOrLockedSkills()
        {
            var loadout = new AbilityLoadout();
            Assert.AreEqual(5, loadout.Slots.Count, "Must have exactly 5 slots");

            // Validator: only lich_lord_active_01 and lich_lord_ultimate_50 are unlocked
            var unlocked = new HashSet<string> { "lich_lord_active_01", "lich_lord_ultimate_50" };
            bool IsUnlocked(string id) => unlocked.Contains(id);

            // Cannot equip to slot 5 (out of bounds)
            Assert.IsFalse(loadout.TryEquip(5, "lich_lord_active_01", IsUnlocked, out string errBounds));
            Assert.IsTrue(errBounds.Contains("out of bounds"));

            // Cannot equip locked skill
            Assert.IsFalse(loadout.TryEquip(0, "lich_lord_active_02", IsUnlocked, out string errLocked));
            Assert.IsTrue(errLocked.Contains("locked"));

            // Can equip unlocked skill
            Assert.IsTrue(loadout.TryEquip(0, "lich_lord_active_01", IsUnlocked, out string errOk));
            Assert.AreEqual("lich_lord_active_01", loadout.GetSlot(0));

            // Cannot duplicate same skill across multiple slots
            Assert.IsFalse(loadout.TryEquip(1, "lich_lord_active_01", IsUnlocked, out string errDup));
            Assert.IsTrue(errDup.Contains("already equipped in slot 1"));

            // Can equip ultimate into any slot (consumes 1 of the 5 slots)
            Assert.IsTrue(loadout.TryEquip(4, "lich_lord_ultimate_50", IsUnlocked, out _));
            Assert.AreEqual("lich_lord_ultimate_50", loadout.GetSlot(4));

            // Autocast toggle
            Assert.IsFalse(loadout.IsAutocastEnabled(0));
            loadout.ToggleAutocast(0);
            Assert.IsTrue(loadout.IsAutocastEnabled(0));
            loadout.ToggleAutocast(0);
            Assert.IsFalse(loadout.IsAutocastEnabled(0));

            // Unequip
            Assert.IsTrue(loadout.TryUnequip(0));
            Assert.AreEqual(string.Empty, loadout.GetSlot(0));
        }

        [Test]
        public void Respec_RefundsPoints_WhilePreservingMasteryXpAndAscension()
        {
            var rec = new CharacterRecord
            {
                CharacterId = CharacterCatalog.LichLordId,
                Level = 10,
                SpentSkillPoints = 0,
                UnlockedAbilities = new List<AbilityProgressRecord>(),
                EquippedLoadout = new List<string> { "", "", "", "", "" }
            };

            // Spend points on Bone Sentinel and gain mastery XP
            CharacterSkillTree.TrySpendPoint("lich_lord_active_01", ref rec, out _);
            CharacterSkillTree.TrySpendPoint("lich_lord_active_01", ref rec, out _);
            Assert.AreEqual(2, rec.SpentSkillPoints);
            Assert.AreEqual(2, CharacterSkillTree.GetAbilityRank(rec, "lich_lord_active_01"));

            // Equip into slot 0
            rec.EquippedLoadout[0] = "lich_lord_active_01";

            // Award mastery XP
            CharacterSkillTree.AwardMasteryXp("lich_lord_active_01", ref rec, xpAmount: 75);
            var progBefore = CharacterSkillTree.GetAbilityProgress(rec, "lich_lord_active_01");
            Assert.AreEqual(75, progBefore.MasteryXp);

            // Execute Respec
            CharacterSkillTree.Respec(ref rec);

            // Verify points refunded
            Assert.AreEqual(0, rec.SpentSkillPoints, "Spent skill points must reset to 0");
            Assert.AreEqual(19, rec.AvailableSkillPoints, "All 19 points must be available again");

            // Verify rank reset to 0
            Assert.AreEqual(0, CharacterSkillTree.GetAbilityRank(rec, "lich_lord_active_01"), "Ability rank resets to 0");

            // Verify Mastery XP preserved!
            var progAfter = CharacterSkillTree.GetAbilityProgress(rec, "lich_lord_active_01");
            Assert.AreEqual(75, progAfter.MasteryXp, "Mastery XP MUST be preserved across respec");

            // Verify equipped loadout cleared of locked ability
            Assert.AreEqual(string.Empty, rec.EquippedLoadout[0], "Equipped loadout clears newly locked ability");
        }

        [Test]
        public void RankMilestoneMutations_AlterCooldown_Cost_Damage_Radius_AndSummons()
        {
            var def = AbilityRegistry.Get("lich_lord_active_01");
            Assert.IsNotNull(def);

            // Base stats (Rank 1)
            float baseCd = AbilityProgressionMath.CalculateEffectiveCooldown(def, 1);
            float baseCost = AbilityProgressionMath.CalculateEffectiveResourceCost(def, 1);
            int baseSummons = AbilityProgressionMath.CalculateEffectiveSummonCount(def, 1);
            Assert.AreEqual(8.0f, baseCd, 0.01f);
            Assert.AreEqual(20.0f, baseCost, 0.01f);
            Assert.AreEqual(1, baseSummons);

            // Rank 5: Cooldown reduced by 15% (8.0 * 0.85 = 6.8s)
            float r5Cd = AbilityProgressionMath.CalculateEffectiveCooldown(def, 5);
            Assert.AreEqual(8.0f * 0.85f, r5Cd, 0.01f);

            // Rank 10: Resource cost reduced by 25% (20.0 * 0.75 = 15.0)
            float r10Cost = AbilityProgressionMath.CalculateEffectiveResourceCost(def, 10);
            Assert.AreEqual(20.0f * 0.75f, r10Cost, 0.01f);

            // Rank 20: Master Mutation: Extra summon count (+1 = 2 sentinels)
            int r20Summons = AbilityProgressionMath.CalculateEffectiveSummonCount(def, 20);
            Assert.AreEqual(2, r20Summons);

            // Check Frost Nova radius milestone (Rank 5: +20% radius)
            var frostDef = AbilityRegistry.Get("lich_lord_active_11");
            Assert.IsNotNull(frostDef);
            float r1Radius = AbilityProgressionMath.CalculateEffectiveRadius(frostDef, 1);
            float r5Radius = AbilityProgressionMath.CalculateEffectiveRadius(frostDef, 5);
            Assert.AreEqual(frostDef.BaseRadius * 1.20f, r5Radius, 0.01f);
            Assert.Greater(r5Radius, r1Radius);
        }

        [Test]
        public void AntiSpamMasteryXp_AwardsXpOnlyWhenUsefulActionOccurs()
        {
            var noImpactResult = new AbilityExecutionResult
            {
                Success = true,
                DamageDealt = 0f,
                TargetsHit = 0,
                HealingDone = 0f,
                SummonsCreated = 0,
                BarrierGranted = 0f
            };
            Assert.IsFalse(noImpactResult.IsUseful, "Casting with zero impact is not useful and must not award mastery XP");

            var usefulResult = new AbilityExecutionResult
            {
                Success = true,
                DamageDealt = 45f,
                TargetsHit = 2
            };
            Assert.IsTrue(usefulResult.IsUseful, "Dealing damage is useful and qualifies for mastery XP");

            var usefulSummonResult = new AbilityExecutionResult
            {
                Success = true,
                SummonsCreated = 1
            };
            Assert.IsTrue(usefulSummonResult.IsUseful, "Summoning an ally is useful and qualifies for mastery XP");
        }

        [Test]
        public void AscensionPostRank20_ProvidesBoundedBonus()
        {
            var def = AbilityRegistry.Get("lich_lord_active_01");
            Assert.IsNotNull(def);

            float r20Dmg = AbilityProgressionMath.CalculateEffectiveDamage(def, 20, ascensionRank: 0);
            float asc1Dmg = AbilityProgressionMath.CalculateEffectiveDamage(def, 20, ascensionRank: 1);
            float asc5Dmg = AbilityProgressionMath.CalculateEffectiveDamage(def, 20, ascensionRank: 5);
            float asc10Dmg = AbilityProgressionMath.CalculateEffectiveDamage(def, 20, ascensionRank: 10);
            float asc20Dmg = AbilityProgressionMath.CalculateEffectiveDamage(def, 20, ascensionRank: 20);

            // +2% damage per ascension rank up to cap of 10
            Assert.Greater(asc1Dmg, r20Dmg);
            Assert.AreEqual(r20Dmg * 1.10f, asc5Dmg, 0.05f);
            Assert.AreEqual(r20Dmg * 1.20f, asc10Dmg, 0.05f);
            // Cap at ascension rank 10
            Assert.AreEqual(asc10Dmg, asc20Dmg, "Ascension beyond cap of 10 must remain bounded");
        }

        [Test]
        public void Persistence_SerializesAndRestoresLoadoutAndTreeState()
        {
            var profile = new PlayerSaveProfile();
            var charRec = profile.GetOrAddCharacter(CharacterCatalog.DemonMonarchId);
            charRec.Level = 15;
            charRec.SpentSkillPoints = 3;
            charRec.UnlockedAbilities = new List<AbilityProgressRecord>
            {
                new AbilityProgressRecord("demon_monarch_active_01", rank: 3, masteryXp: 120, ascensionRank: 0),
                new AbilityProgressRecord("demon_monarch_active_02", rank: 1, masteryXp: 40, ascensionRank: 0)
            };
            charRec.EquippedLoadout = new List<string>
            {
                "demon_monarch_active_01",
                "demon_monarch_active_02",
                "",
                "",
                ""
            };
            charRec.AutocastSettings = new List<bool> { true, false, false, false, false };
            profile.SaveCharacter(charRec);

            // Serialize
            string json = UnityEngine.JsonUtility.ToJson(profile, true);
            Assert.IsFalse(string.IsNullOrEmpty(json));

            // Deserialize
            var restored = UnityEngine.JsonUtility.FromJson<PlayerSaveProfile>(json);
            Assert.IsNotNull(restored);

            var restoredRec = restored.GetOrAddCharacter(CharacterCatalog.DemonMonarchId);
            Assert.AreEqual(15, restoredRec.Level);
            Assert.AreEqual(3, restoredRec.SpentSkillPoints);
            Assert.AreEqual(2, restoredRec.UnlockedAbilities.Count);
            Assert.AreEqual("demon_monarch_active_01", restoredRec.UnlockedAbilities[0].AbilityId);
            Assert.AreEqual(3, restoredRec.UnlockedAbilities[0].Rank);
            Assert.AreEqual(120, restoredRec.UnlockedAbilities[0].MasteryXp);

            Assert.AreEqual(5, restoredRec.EquippedLoadout.Count);
            Assert.AreEqual("demon_monarch_active_01", restoredRec.EquippedLoadout[0]);
            Assert.AreEqual("demon_monarch_active_02", restoredRec.EquippedLoadout[1]);
            Assert.AreEqual(true, restoredRec.AutocastSettings[0]);
            Assert.AreEqual(false, restoredRec.AutocastSettings[1]);
        }
    }
}
