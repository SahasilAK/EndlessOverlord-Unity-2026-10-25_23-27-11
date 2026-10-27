using System;
using NUnit.Framework;
using FinalBoss.Core;

namespace FinalBoss.Tests.EditMode
{
    [TestFixture]
    public sealed class InRunEventTests
    {
        [Test]
        public void AllInRunEvents_AreRegistered_WithValidData()
        {
            var events = InRunEventCatalog.GetAllEvents();
            Assert.AreEqual(6, events.Count, "Exactly 6 major In-Run event types must be registered.");

            var types = new[]
            {
                InRunEventType.HolyCrusade,
                InRunEventType.TreasureHero,
                InRunEventType.MagicalShrine,
                InRunEventType.RareMerchant,
                InRunEventType.DungeonMutation,
                InRunEventType.DoubleBoss
            };

            foreach (var type in types)
            {
                var def = InRunEventCatalog.GetEvent(type);
                Assert.IsNotNull(def, $"Event {type} must be registered.");
                Assert.AreEqual(type, def.Type);
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Name), $"Event {type} name cannot be empty.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Description), $"Event {type} description cannot be empty.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.CueId), $"Event {type} cue ID cannot be empty.");
                Assert.Greater(def.MinWave, 0, $"Event {type} must have positive min wave.");
            }
        }

        [Test]
        public void All5ShrineTypes_AreRegistered_WithValidBuffs()
        {
            var shrines = InRunEventCatalog.GetAllShrines();
            Assert.AreEqual(5, shrines.Count, "Exactly 5 Shrine types must be registered.");

            var allShrineTypes = (ShrineType[])Enum.GetValues(typeof(ShrineType));
            Assert.AreEqual(5, allShrineTypes.Length);

            foreach (var sType in allShrineTypes)
            {
                var def = InRunEventCatalog.GetShrine(sType);
                Assert.IsNotNull(def, $"Shrine {sType} must be registered.");
                Assert.AreEqual(sType, def.Type);
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Name));
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Description));
                Assert.IsTrue(def.ColorHex.StartsWith("#"), "Shrine color hex must start with '#'.");
                Assert.Greater(def.BuffValue, 0f, "Shrine buff value must be positive.");
            }
        }

        [Test]
        public void All4DungeonMutations_AreRegistered_WithValidModifiers()
        {
            var mutations = InRunEventCatalog.GetAllMutations();
            Assert.AreEqual(4, mutations.Count, "Exactly 4 Dungeon Mutation types must be registered.");

            var mutationTypes = new[]
            {
                DungeonMutationType.VolcanicSurge,
                DungeonMutationType.GlacialCold,
                DungeonMutationType.VoidDistortion,
                DungeonMutationType.OverchargedLeyline
            };

            foreach (var mType in mutationTypes)
            {
                var def = InRunEventCatalog.GetMutation(mType);
                Assert.IsNotNull(def, $"Mutation {mType} must be registered.");
                Assert.AreEqual(mType, def.Type);
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Name));
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Description));
                Assert.Greater(def.ResourceGainMultiplier, 0.5f, "Resource gain multiplier must be reasonable.");
            }
        }

        [Test]
        public void InRunEventDirector_MilestoneWaves_YieldPredictableEvents()
        {
            long testSeed = 54321L;

            Assert.AreEqual(InRunEventType.None, InRunEventDirector.EvaluateWaveEvent(testSeed, 1));
            Assert.AreEqual(InRunEventType.MagicalShrine, InRunEventDirector.EvaluateWaveEvent(testSeed, 3));
            Assert.AreEqual(InRunEventType.HolyCrusade, InRunEventDirector.EvaluateWaveEvent(testSeed, 7));
            Assert.AreEqual(InRunEventType.TreasureHero, InRunEventDirector.EvaluateWaveEvent(testSeed, 12));
            Assert.AreEqual(InRunEventType.DungeonMutation, InRunEventDirector.EvaluateWaveEvent(testSeed, 15));
            Assert.AreEqual(InRunEventType.RareMerchant, InRunEventDirector.EvaluateWaveEvent(testSeed, 18));
            Assert.AreEqual(InRunEventType.DoubleBoss, InRunEventDirector.EvaluateWaveEvent(testSeed, 20));
        }

        [Test]
        public void ShrineInstance_Activation_ConsumesShrine_GrantsAppropriateBuff_AndCanOnlyActivateOnce()
        {
            var protDef = InRunEventCatalog.GetShrine(ShrineType.Protection);
            var inst = new ShrineInstance(protDef, 0f, 0f);

            Assert.IsFalse(inst.IsUsed);

            bool activated = inst.TryActivate(100f, out float hpCost, out float barrierGranted, out int goldGranted);
            Assert.IsTrue(activated);
            Assert.IsTrue(inst.IsUsed);
            Assert.AreEqual(0f, hpCost);
            Assert.AreEqual(75f, barrierGranted);
            Assert.AreEqual(0, goldGranted);

            // Activating again fails
            bool secondActivated = inst.TryActivate(100f, out _, out _, out _);
            Assert.IsFalse(secondActivated);

            // Test Greed shrine
            var greedDef = InRunEventCatalog.GetShrine(ShrineType.Greed);
            var greedInst = new ShrineInstance(greedDef, 1f, 1f);
            Assert.IsTrue(greedInst.TryActivate(100f, out _, out _, out int gold));
            Assert.AreEqual(150, gold);

            // Test Blood shrine
            var bloodDef = InRunEventCatalog.GetShrine(ShrineType.Blood);
            var bloodInst = new ShrineInstance(bloodDef, 2f, 2f);
            Assert.IsTrue(bloodInst.TryActivate(80f, out float bloodCost, out _, out _));
            Assert.AreEqual(16f, bloodCost, 0.01f); // 20% of 80 = 16
        }
    }
}
