using System;
using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class LegendaryBossTests
    {
        [Test]
        public void LegendaryBossCatalog_ContainsThirtyUniqueBosses_WithCompleteAttributesAndAbilities()
        {
            var bosses = LegendaryBossCatalog.All;
            Assert.AreEqual(30, bosses.Count, "There must be exactly 30 uniquely named legendary bosses registered.");

            var seenIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            for (int i = 0; i < bosses.Count; i++)
            {
                var b = bosses[i];
                Assert.IsFalse(string.IsNullOrWhiteSpace(b.Id), $"Boss at index {i} must have an Id.");
                Assert.IsTrue(seenIds.Add(b.Id), $"Boss Id '{b.Id}' must be unique.");

                Assert.IsFalse(string.IsNullOrWhiteSpace(b.Name), $"Boss '{b.Id}' must have a Name.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(b.Title), $"Boss '{b.Id}' must have a Title.");
                Assert.IsTrue(seenNames.Add(b.Name), $"Boss Name '{b.Name}' must be unique.");

                Assert.GreaterOrEqual(b.BaseHealth, 200f, $"Boss '{b.Id}' BaseHealth must be at least 200.");
                Assert.Greater(b.BaseMoveSpeed, 0f, $"Boss '{b.Id}' BaseMoveSpeed must be > 0.");
                Assert.Greater(b.Radius, 0f, $"Boss '{b.Id}' Radius must be > 0.");
                Assert.Greater(b.AttackDamage, 0f, $"Boss '{b.Id}' AttackDamage must be > 0.");
                Assert.Greater(b.AttackWindup, 0f, $"Boss '{b.Id}' AttackWindup must be > 0.");
                Assert.Greater(b.AttackCooldown, 0f, $"Boss '{b.Id}' AttackCooldown must be > 0.");
                Assert.IsTrue(Enum.IsDefined(typeof(BossAbilityType), b.PrimaryAbility), $"Boss '{b.Id}' must have a valid PrimaryAbility.");
                Assert.IsTrue(Enum.IsDefined(typeof(BossAbilityType), b.SecondaryAbility), $"Boss '{b.Id}' must have a valid SecondaryAbility.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(b.Description), $"Boss '{b.Id}' must have a Description.");
            }
        }

        [Test]
        public void LegendaryBossCatalog_AbilityCoverage_AllTenBossAbilitiesCovered()
        {
            var abilitiesCovered = new HashSet<BossAbilityType>();
            foreach (var b in LegendaryBossCatalog.All)
            {
                abilitiesCovered.Add(b.PrimaryAbility);
                abilitiesCovered.Add(b.SecondaryAbility);
            }

            var allAbilities = (BossAbilityType[])Enum.GetValues(typeof(BossAbilityType));
            Assert.AreEqual(10, allAbilities.Length, "There must be 10 defined BossAbilityTypes.");

            foreach (var ability in allAbilities)
            {
                Assert.IsTrue(abilitiesCovered.Contains(ability), $"Boss ability {ability} must be covered across the 30 bosses.");
            }
        }

        [Test]
        public void LegendaryBossCatalog_ModifierRolls_EnrageAlwaysPresent_HigherWavesRollAdditionalModifiers()
        {
            long seed = 98765L;

            // Wave 10
            var mod10 = LegendaryBossCatalog.RollModifiersForWave(10, seed);
            Assert.IsTrue((mod10 & BossModifier.Enrage) != 0, "Wave 10 boss must have Enrage modifier.");

            // Wave 20
            var mod20 = LegendaryBossCatalog.RollModifiersForWave(20, seed);
            Assert.IsTrue((mod20 & BossModifier.Enrage) != 0, "Wave 20 boss must have Enrage modifier.");
            Assert.AreNotEqual(BossModifier.Enrage, mod20, "Wave 20 boss must have at least 2 modifiers.");

            // Wave 30
            var mod30 = LegendaryBossCatalog.RollModifiersForWave(30, seed);
            Assert.IsTrue((mod30 & BossModifier.Enrage) != 0, "Wave 30 boss must have Enrage modifier.");
            Assert.IsTrue((mod30 & BossModifier.Barrier) != 0, "Wave 30 boss must have Barrier modifier.");
            Assert.IsTrue((mod30 & BossModifier.ElementalOvercharge) != 0, "Wave 30 boss must have ElementalOvercharge modifier.");
        }

        [Test]
        public void WaveGenerator_BossWaves_GenerateLegendaryBossesWithModifiers()
        {
            var gen = new WaveGenerator(54321L);

            // Wave 10
            var w10 = gen.Generate(10);
            Assert.IsTrue(w10.IsBossWave);
            Assert.IsFalse(string.IsNullOrEmpty(w10.Spawns[0].BossId));
            Assert.AreNotEqual(BossModifier.None, w10.Spawns[0].Modifiers);

            // Wave 20 has double boss encounter!
            var w20 = gen.Generate(20);
            Assert.IsTrue(w20.IsBossWave);
            Assert.IsFalse(string.IsNullOrEmpty(w20.Spawns[0].BossId));
            Assert.IsFalse(string.IsNullOrEmpty(w20.Spawns[1].BossId));
            Assert.AreNotEqual(w20.Spawns[0].BossId, w20.Spawns[1].BossId, "Wave 20 dual bosses must have distinct identities.");
        }
    }
}
