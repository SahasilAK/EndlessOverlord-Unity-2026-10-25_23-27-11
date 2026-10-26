using System;
using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class InvaderArchetypeTests
    {
        [Test]
        public void InvaderCatalog_ContainsAllThirtyArchetypes_WithValidStatsAndRoles()
        {
            var archetypes = (InvaderArchetype[])Enum.GetValues(typeof(InvaderArchetype));
            Assert.AreEqual(30, archetypes.Length, "There must be exactly 30 registered InvaderArchetypes.");

            foreach (var arch in archetypes)
            {
                var def = InvaderCatalog.Get(arch);
                Assert.IsNotNull(def, $"Definition for {arch} must not be null.");
                Assert.AreEqual(arch, def.Archetype, $"Definition archetype must match {arch}.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Name), $"Definition for {arch} must have a name.");
                Assert.Greater(def.BaseHealth, 0f, $"{arch} BaseHealth must be > 0.");
                Assert.Greater(def.BaseMoveSpeed, 0f, $"{arch} BaseMoveSpeed must be > 0.");
                Assert.Greater(def.Radius, 0f, $"{arch} Radius must be > 0.");
                Assert.Greater(def.AttackRange, 0f, $"{arch} AttackRange must be > 0.");
                Assert.Greater(def.AttackDamage, 0f, $"{arch} AttackDamage must be > 0.");
                Assert.Greater(def.AttackWindup, 0f, $"{arch} AttackWindup must be > 0.");
                Assert.Greater(def.AttackCooldown, 0f, $"{arch} AttackCooldown must be > 0.");
                Assert.GreaterOrEqual(def.BaseArmor, 0f, $"{arch} BaseArmor must be >= 0.");
            }
        }

        [Test]
        public void InvaderCatalog_RoleDistribution_CoversAllSixRoles()
        {
            var rolesCovered = new HashSet<InvaderCombatRole>();
            foreach (var def in InvaderCatalog.All)
            {
                rolesCovered.Add(def.Role);
            }

            Assert.IsTrue(rolesCovered.Contains(InvaderCombatRole.Melee), "Melee role must be represented.");
            Assert.IsTrue(rolesCovered.Contains(InvaderCombatRole.Ranged), "Ranged role must be represented.");
            Assert.IsTrue(rolesCovered.Contains(InvaderCombatRole.Caster), "Caster role must be represented.");
            Assert.IsTrue(rolesCovered.Contains(InvaderCombatRole.HealerSupport), "HealerSupport role must be represented.");
            Assert.IsTrue(rolesCovered.Contains(InvaderCombatRole.Assassin), "Assassin role must be represented.");
            Assert.IsTrue(rolesCovered.Contains(InvaderCombatRole.Tank), "Tank role must be represented.");
        }

        [Test]
        public void WaveGenerator_HigherWaves_SpanAllArchetypeRoles()
        {
            var gen = new WaveGenerator(1337L);
            var seenRoles = new HashSet<InvaderCombatRole>();
            var seenArchetypes = new HashSet<InvaderArchetype>();

            for (int w = 1; w <= 30; w++)
            {
                var spec = gen.Generate(w);
                for (int i = 0; i < spec.Spawns.Count; i++)
                {
                    var arch = spec.Spawns[i].Archetype;
                    seenArchetypes.Add(arch);
                    seenRoles.Add(InvaderCatalog.Get(arch).Role);
                }
            }

            // Across 30 waves, multiple archetypes and all combat roles should be fielded
            Assert.GreaterOrEqual(seenArchetypes.Count, 15, "Across 30 waves, at least 15 unique archetypes should appear.");
            Assert.IsTrue(seenRoles.Contains(InvaderCombatRole.Melee));
            Assert.IsTrue(seenRoles.Contains(InvaderCombatRole.Ranged));
            Assert.IsTrue(seenRoles.Contains(InvaderCombatRole.Caster));
            Assert.IsTrue(seenRoles.Contains(InvaderCombatRole.HealerSupport));
            Assert.IsTrue(seenRoles.Contains(InvaderCombatRole.Tank));
        }

        [Test]
        public void WaveGenerator_EliteSpawns_ScaleProperly()
        {
            var gen = new WaveGenerator(42L);
            int eliteCountEarly = 0;
            int eliteCountLate = 0;

            for (int w = 1; w <= 3; w++)
            {
                var spec = gen.Generate(w);
                foreach (var s in spec.Spawns)
                    if (s.IsElite) eliteCountEarly++;
            }

            for (int w = 21; w <= 23; w++)
            {
                var spec = gen.Generate(w);
                foreach (var s in spec.Spawns)
                    if (s.IsElite) eliteCountLate++;
            }

            Assert.GreaterOrEqual(eliteCountLate, eliteCountEarly, "Later waves should spawn more or equal elites than early waves.");
        }
    }
}
