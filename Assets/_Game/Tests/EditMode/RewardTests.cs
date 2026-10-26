using System;
using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class RewardTests
    {
        [Test]
        public void AllImplementedRewardIdsMatchPlannedRegistry()
        {
            var expectedIds = new[]
            {
                RewardRegistry.ArmorId,
                RewardRegistry.ExtraProjId,
                RewardRegistry.MoveSpeedId,
                RewardRegistry.RangeId,
                RewardRegistry.LifeStealId,
                RewardRegistry.AttackSpeedId,
                RewardRegistry.MaxHealthId,
                RewardRegistry.DamageId,
                RewardRegistry.MonarchId,
                RewardRegistry.FallbackId
            };

            foreach (var id in expectedIds)
            {
                var def = RewardRegistry.GetById(id);
                Assert.IsNotNull(def, $"Reward ID {id} must be registered.");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.IsTrue(def.ValuePerRank > 0f, $"Value per rank for {id} must be positive.");
                Assert.IsTrue(def.MaxRank >= 1, $"MaxRank for {id} must be at least 1.");
            }
        }

        [Test]
        public void RewardInventory_AppliesRankStackingAndComputesAggregates()
        {
            var inv = new RewardInventory();
            var armorDef = RewardRegistry.GetById(RewardRegistry.ArmorId);
            var speedDef = RewardRegistry.GetById(RewardRegistry.MoveSpeedId);
            var damageDef = RewardRegistry.GetById(RewardRegistry.DamageId);
            var extraProjDef = RewardRegistry.GetById(RewardRegistry.ExtraProjId);

            Assert.AreEqual(0, inv.GetRank(armorDef.Id));
            Assert.AreEqual(0f, inv.BonusArmor);

            Assert.IsTrue(inv.TryAdd(armorDef));
            Assert.AreEqual(1, inv.GetRank(armorDef.Id));
            Assert.AreEqual(5f, inv.BonusArmor);

            Assert.IsTrue(inv.TryAdd(armorDef));
            Assert.AreEqual(2, inv.GetRank(armorDef.Id));
            Assert.AreEqual(10f, inv.BonusArmor);

            Assert.IsTrue(inv.TryAdd(speedDef));
            Assert.AreEqual(0.12f, inv.BonusMoveSpeedPercent, 1e-4f);

            Assert.IsTrue(inv.TryAdd(damageDef));
            Assert.AreEqual(0.25f, inv.BonusDamagePercent, 1e-4f);

            Assert.IsTrue(inv.TryAdd(extraProjDef));
            Assert.AreEqual(1, inv.ExtraProjectiles);
        }

        [Test]
        public void RewardInventory_RejectsBeyondMaxRank()
        {
            var inv = new RewardInventory();
            var extraProjDef = RewardRegistry.GetById(RewardRegistry.ExtraProjId); // MaxRank 3

            Assert.IsTrue(inv.TryAdd(extraProjDef));
            Assert.IsTrue(inv.TryAdd(extraProjDef));
            Assert.IsTrue(inv.TryAdd(extraProjDef));
            Assert.AreEqual(3, inv.GetRank(extraProjDef.Id));
            Assert.IsFalse(inv.CanAcquire(extraProjDef));
            Assert.IsFalse(inv.TryAdd(extraProjDef));
            Assert.AreEqual(3, inv.GetRank(extraProjDef.Id));
        }

        [Test]
        public void PrerequisiteFiltering_RejectsUntilPrerequisiteRankAcquired()
        {
            var inv = new RewardInventory();
            var monarchDef = RewardRegistry.GetById(RewardRegistry.MonarchId);
            var damageDef = RewardRegistry.GetById(RewardRegistry.DamageId);

            Assert.IsFalse(inv.CanAcquire(monarchDef), "Monarch should require Doom Potency prerequisite");
            Assert.IsFalse(inv.TryAdd(monarchDef));

            // Acquire prerequisite
            Assert.IsTrue(inv.TryAdd(damageDef));
            Assert.IsTrue(inv.CanAcquire(monarchDef), "Monarch should be eligible after prerequisite rank > 0");
            Assert.IsTrue(inv.TryAdd(monarchDef));
            Assert.AreEqual(1, inv.GetRank(monarchDef.Id));
        }

        [Test]
        public void RewardOffer_NoDuplicateChoicesInSingleDraft()
        {
            var inv = new RewardInventory();
            for (int wave = 1; wave <= 20; wave++)
            {
                var offer = RewardDraftSystem.GenerateOffer(wave, 123456789L, inv, "lich_lord");
                int expectedCount = (wave % 10 == 0) ? 4 : 3;
                Assert.AreEqual(expectedCount, offer.Choices.Count);
                var seen = new HashSet<string>();
                for (int i = 0; i < offer.Choices.Count; i++)
                {
                    Assert.IsTrue(seen.Add(offer.Choices[i].Id), $"Duplicate choice {offer.Choices[i].Id} in wave {wave}");
                }
            }
        }

        [Test]
        public void RewardOffer_SinglePickEnforcesAtomicCommit()
        {
            var inv = new RewardInventory();
            var offer = RewardDraftSystem.GenerateOffer(1, 42L, inv, "lich_lord");

            Assert.AreEqual(1, offer.PicksAllowed);
            Assert.AreEqual(1, offer.PicksRemaining);
            Assert.IsFalse(offer.IsCommitted);

            var choice = offer.Choices[0];
            Assert.IsTrue(offer.TryPick(choice.Id, inv));
            Assert.AreEqual(0, offer.PicksRemaining);
            Assert.IsTrue(offer.IsCommitted);
            Assert.AreEqual(1, inv.GetRank(choice.Id));

            // Cannot pick again
            Assert.IsFalse(offer.TryPick(choice.Id, inv));
            Assert.IsFalse(offer.TryPick(offer.Choices[1].Id, inv));
            Assert.AreEqual(1, inv.GetRank(choice.Id));
        }

        [Test]
        public void RewardOffer_FourChoiceRequiresTwoPicksBeforeCommit()
        {
            var inv = new RewardInventory();
            var offer = RewardDraftSystem.GenerateOffer(10, 42L, inv, "lich_lord", forceFourChoices: true);

            Assert.AreEqual(4, offer.Choices.Count);
            Assert.AreEqual(2, offer.PicksAllowed);
            Assert.AreEqual(2, offer.PicksRemaining);
            Assert.IsFalse(offer.IsCommitted);

            // First pick
            var choice1 = offer.Choices[0];
            Assert.IsTrue(offer.TryPick(choice1.Id, inv));
            Assert.AreEqual(1, offer.PicksRemaining);
            Assert.IsFalse(offer.IsCommitted);

            // Duplicate of first pick rejected
            Assert.IsFalse(offer.TryPick(choice1.Id, inv));
            Assert.AreEqual(1, offer.PicksRemaining);

            // Second pick commits
            var choice2 = offer.Choices[1];
            Assert.IsTrue(offer.TryPick(choice2.Id, inv));
            Assert.AreEqual(0, offer.PicksRemaining);
            Assert.IsTrue(offer.IsCommitted);

            // Third pick rejected
            Assert.IsFalse(offer.TryPick(offer.Choices[2].Id, inv));
        }

        [Test]
        public void RewardDraft_FallbackWhenEligiblePoolExhausted()
        {
            var inv = new RewardInventory();
            // Max out every single custom reward
            foreach (var def in RewardRegistry.All)
            {
                if (def.Id == RewardRegistry.FallbackId) continue;
                for (int r = 0; r < def.MaxRank; r++)
                {
                    inv.TryAdd(def);
                }
            }

            // Pool exhausted: draft must provide fallback
            var offer = RewardDraftSystem.GenerateOffer(1, 999L, inv, "lich_lord");
            Assert.AreEqual(3, offer.Choices.Count);
            for (int i = 0; i < offer.Choices.Count; i++)
            {
                Assert.AreEqual(RewardRegistry.FallbackId, offer.Choices[i].Id);
            }

            Assert.IsTrue(offer.TryPick(RewardRegistry.FallbackId, inv));
            Assert.AreEqual(1, inv.GetRank(RewardRegistry.FallbackId));
        }

        [Test]
        public void DeterministicGeneration_SameSeedYieldsIdenticalOffer()
        {
            var invA = new RewardInventory();
            var invB = new RewardInventory();

            var offerA = RewardDraftSystem.GenerateOffer(3, 88888L, invA, "lich_lord");
            var offerB = RewardDraftSystem.GenerateOffer(3, 88888L, invB, "lich_lord");

            Assert.AreEqual(offerA.Choices.Count, offerB.Choices.Count);
            for (int i = 0; i < offerA.Choices.Count; i++)
            {
                Assert.AreEqual(offerA.Choices[i].Id, offerB.Choices[i].Id);
            }
        }

        [Test]
        public void RarityWeighting_ProducesExpectedDistribution()
        {
            // Verify across 1000 draws that common and uncommon are drawn more frequently than epic/mythic
            var inv = new RewardInventory();
            inv.TryAdd(RewardRegistry.GetById(RewardRegistry.DamageId)); // unlock monarch
            int common = 0, uncommon = 0, rare = 0, epic = 0, mythic = 0;

            for (int i = 0; i < 1000; i++)
            {
                var offer = RewardDraftSystem.GenerateOffer(i + 1, 777L + i, inv, "lich_lord");
                for (int c = 0; c < offer.Choices.Count; c++)
                {
                    switch (offer.Choices[c].Rarity)
                    {
                        case RewardRarity.Common: common++; break;
                        case RewardRarity.Uncommon: uncommon++; break;
                        case RewardRarity.Rare: rare++; break;
                        case RewardRarity.Epic: epic++; break;
                        case RewardRarity.Mythic: mythic++; break;
                    }
                }
            }

            Assert.IsTrue(common > rare, $"Common ({common}) should exceed Rare ({rare})");
            Assert.IsTrue(uncommon > epic, $"Uncommon ({uncommon}) should exceed Epic ({epic})");
            Assert.IsTrue(rare > mythic, $"Rare ({rare}) should exceed Mythic ({mythic})");
        }

        [Test]
        public void Tooltip_AccuratelyReflectsRankProgression()
        {
            var def = RewardRegistry.GetById(RewardRegistry.ArmorId);
            string tip0 = def.GetTooltip(0);
            StringAssert.Contains("Rank 1/5", tip0);
            StringAssert.Contains("+5 Armor", tip0);

            string tip1 = def.GetTooltip(1);
            StringAssert.Contains("Rank 1->2/5", tip1);
            StringAssert.Contains("+5 -> +10", tip1);

            string tipMax = def.GetTooltip(5);
            StringAssert.Contains("MAXED 5/5", tipMax);
        }

        [Test]
        public void RunStateMachine_TransitionsThroughRewardAndIntermissionFlow()
        {
            var sm = new RunStateMachine(RunState.PlayingWave);

            // Wave clear -> ChoosingReward
            Assert.IsTrue(sm.TryTransition(RunState.ChoosingReward));
            Assert.AreEqual(RunState.ChoosingReward, sm.Current);

            // Can pause and resume while choosing reward
            Assert.IsTrue(sm.TryPause());
            Assert.AreEqual(RunState.Paused, sm.Current);
            Assert.IsTrue(sm.TryResume());
            Assert.AreEqual(RunState.ChoosingReward, sm.Current);

            // Reward chosen -> Intermission
            Assert.IsTrue(sm.TryTransition(RunState.Intermission));
            Assert.AreEqual(RunState.Intermission, sm.Current);

            // Next wave -> PlayingWave
            Assert.IsTrue(sm.TryTransition(RunState.PlayingWave));
            Assert.AreEqual(RunState.PlayingWave, sm.Current);

            // Death has priority
            Assert.IsTrue(sm.TryTransition(RunState.ChoosingReward));
            Assert.IsTrue(sm.TryTransition(RunState.Death));
            Assert.AreEqual(RunState.Death, sm.Current);
        }
    }
}
