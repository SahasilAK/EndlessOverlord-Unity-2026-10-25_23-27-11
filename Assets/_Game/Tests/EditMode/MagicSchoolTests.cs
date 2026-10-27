using System;
using System.Collections.Generic;
using NUnit.Framework;
using FinalBoss.Core;

namespace FinalBoss.Tests.EditMode
{
    [TestFixture]
    public sealed class MagicSchoolTests
    {
        [Test]
        public void All20MagicSchools_AreRegistered_WithValidData()
        {
            var schools = MagicSchoolCatalog.GetAllSchools();
            Assert.AreEqual(20, schools.Count, "Exactly 20 Magic Schools must be registered.");

            var allEnums = (MagicSchoolType[])Enum.GetValues(typeof(MagicSchoolType));
            Assert.AreEqual(20, allEnums.Length, "MagicSchoolType enum must have exactly 20 values.");

            foreach (var schoolType in allEnums)
            {
                var def = MagicSchoolCatalog.GetSchool(schoolType);
                Assert.IsNotNull(def, $"School {schoolType} must be registered in catalog.");
                Assert.AreEqual(schoolType, def.School);
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Name), $"School {schoolType} must have a name.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Title), $"School {schoolType} must have a title.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.Description), $"School {schoolType} must have a description.");
                Assert.IsTrue(def.ColorHex.StartsWith("#"), $"School {schoolType} color hex must start with '#'.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.PassivePerkDescription), $"School {schoolType} must have a passive perk.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(def.SynergyName), $"School {schoolType} must have a synergy name.");
                Assert.GreaterOrEqual(def.Spells.Count, 3, $"School {schoolType} must have at least 3 tier spells.");
            }
        }

        [Test]
        public void All60SpellsAnd20Evolutions_AreRegistered_WithValidTiersAndBehaviors()
        {
            var spells = MagicSchoolCatalog.GetAllSpells();
            // 20 schools * 3 base spells + 20 evolved spells = 80 total
            Assert.AreEqual(80, spells.Count, "Catalog must have exactly 80 total spells (60 base + 20 evolved).");

            var behaviorsPresent = new HashSet<CastBehaviorType>();

            foreach (var spell in spells)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(spell.Id), "Spell ID cannot be empty.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(spell.Name), "Spell name cannot be empty.");
                Assert.Greater(spell.BaseDamage, 0f, $"Spell {spell.Id} must have positive damage.");
                Assert.Greater(spell.Cooldown, 0f, $"Spell {spell.Id} must have positive cooldown.");
                Assert.Greater(spell.Range, 0f, $"Spell {spell.Id} must have positive range.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(spell.CueId), $"Spell {spell.Id} must have a cue ID.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(spell.Description), $"Spell {spell.Id} must have a description.");

                behaviorsPresent.Add(spell.Behavior);
            }

            // Verify all 10 cast behavior types are represented
            var allBehaviors = (CastBehaviorType[])Enum.GetValues(typeof(CastBehaviorType));
            Assert.AreEqual(10, allBehaviors.Length, "CastBehaviorType enum must have 10 values.");
            foreach (var behavior in allBehaviors)
            {
                Assert.IsTrue(behaviorsPresent.Contains(behavior), $"Behavior {behavior} must be utilized by spells.");
            }
        }

        [Test]
        public void MultiElementSynergies_All10Pairs_AreRegistered_AndMatchBothWays()
        {
            var synergies = MagicSchoolCatalog.GetAllSynergies();
            Assert.AreEqual(10, synergies.Count, "Exactly 10 Multi-Element synergies must be registered.");

            foreach (var syn in synergies)
            {
                Assert.AreNotEqual(syn.SchoolA, syn.SchoolB, "Synergy must pair distinct schools.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(syn.SynergyName), "Synergy name cannot be empty.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(syn.Description), "Synergy description cannot be empty.");
                Assert.Greater(syn.BonusDamagePercent, 0f, "Synergy must grant positive bonus damage.");
                Assert.Greater(syn.BonusCooldownReductionPercent, 0f, "Synergy must grant positive CDR.");

                // Both directions match
                Assert.IsTrue(syn.Matches(syn.SchoolA, syn.SchoolB));
                Assert.IsTrue(syn.Matches(syn.SchoolB, syn.SchoolA));
            }

            // Verify lookup method
            var fireIce = MagicSchoolCatalog.FindSynergy(MagicSchoolType.Fire, MagicSchoolType.Ice);
            Assert.IsNotNull(fireIce);
            Assert.AreEqual("Thermal Shock", fireIce.SynergyName);

            var iceFire = MagicSchoolCatalog.FindSynergy(MagicSchoolType.Ice, MagicSchoolType.Fire);
            Assert.AreSame(fireIce, iceFire);
        }

        [Test]
        public void MagicSchoolProgression_TracksMasteryLevels_AwardsBonusesAndFiresEvents()
        {
            var prog = new MagicSchoolProgression();
            Assert.AreEqual(1, prog.GetMasteryLevel(MagicSchoolType.Fire));
            Assert.AreEqual(0f, prog.GetSchoolDamageMultiplier(MagicSchoolType.Fire));
            Assert.AreEqual(0f, prog.GetSchoolCooldownReduction(MagicSchoolType.Fire));

            int levelUpsFired = 0;
            MagicSchoolType levelUpSchool = MagicSchoolType.Fire;
            int newLevelReported = 0;

            prog.SchoolMasteryLevelUp += (school, lvl) =>
            {
                levelUpsFired++;
                levelUpSchool = school;
                newLevelReported = lvl;
            };

            // Add 100 XP -> level 2
            prog.AddSchoolXp(MagicSchoolType.Fire, 100f);
            Assert.AreEqual(2, prog.GetMasteryLevel(MagicSchoolType.Fire));
            Assert.AreEqual(1, levelUpsFired);
            Assert.AreEqual(MagicSchoolType.Fire, levelUpSchool);
            Assert.AreEqual(2, newLevelReported);

            // Level 2 grants +10% damage, +5% CDR
            Assert.AreEqual(0.10f, prog.GetSchoolDamageMultiplier(MagicSchoolType.Fire), 0.001f);
            Assert.AreEqual(0.05f, prog.GetSchoolCooldownReduction(MagicSchoolType.Fire), 0.001f);

            // Add 250 XP -> level 3 (+20% damage, +10% CDR)
            prog.AddSchoolXp(MagicSchoolType.Fire, 250f);
            Assert.AreEqual(3, prog.GetMasteryLevel(MagicSchoolType.Fire));
            Assert.AreEqual(0.20f, prog.GetSchoolDamageMultiplier(MagicSchoolType.Fire), 0.001f);
            Assert.AreEqual(0.10f, prog.GetSchoolCooldownReduction(MagicSchoolType.Fire), 0.001f);
        }

        [Test]
        public void SpellInstance_LevelingAndEvolution_ScalesStatsAndEvolvesAtMaxLevel()
        {
            var meteorDef = MagicSchoolCatalog.GetSpell("spell_meteor_strike");
            Assert.IsNotNull(meteorDef);
            var evolvedDef = MagicSchoolCatalog.GetSpell(meteorDef.EvolutionSpellId);
            Assert.IsNotNull(evolvedDef, "Evolved spell definition must exist.");

            var spell = new SpellInstance(meteorDef);
            Assert.AreEqual(1, spell.Level);
            Assert.IsFalse(spell.IsEvolved);

            float baseEffectiveDmg = spell.GetEffectiveDamage();
            Assert.AreEqual(meteorDef.BaseDamage, baseEffectiveDmg, 0.01f);

            // Attempt evolve before level 5 fails
            Assert.IsFalse(spell.TryEvolve(evolvedDef));
            Assert.IsFalse(spell.IsEvolved);

            // Level up to level 5 (requires 400 XP total: 100 + 200 + 300 + 400)
            spell.AddXp(1000f);
            Assert.AreEqual(5, spell.Level);

            // Damage at level 5 has increased (+48% base multiplier)
            float lvl5Dmg = spell.GetEffectiveDamage();
            Assert.Greater(lvl5Dmg, baseEffectiveDmg * 1.4f);

            // Now evolution succeeds
            bool evolved = spell.TryEvolve(evolvedDef);
            Assert.IsTrue(evolved);
            Assert.IsTrue(spell.IsEvolved);
            Assert.AreEqual(evolvedDef.Id, spell.Definition.Id);
            Assert.AreEqual("Inferno Cataclysm", spell.Definition.Name);

            // Evolving again fails
            Assert.IsFalse(spell.TryEvolve(evolvedDef));
        }

        [Test]
        public void SpellCastSimulator_ExecutesValidSpell_ConsumesCooldown_AwardsXp_AndRaisesCue()
        {
            var prog = new MagicSchoolProgression();
            var spellDef = MagicSchoolCatalog.GetSpell("spell_fire_bolt");
            Assert.IsNotNull(spellDef);

            var spell = new SpellInstance(spellDef);

            string lastCue = null;
            float lastCueValue = 0f;
            Action<CueRequest> cueHandler = req =>
            {
                lastCue = req.Id;
                lastCueValue = req.Value;
            };
            CueBus.Requested += cueHandler;

            try
            {
                // Execute cast with target in range
                var result = SpellCastSimulator.ExecuteSpell(
                    spell,
                    prog,
                    casterX: 0f,
                    casterY: 0f,
                    targetDistance: 5f,
                    availableTargets: 1,
                    availableResource: 50f
                );

                Assert.IsTrue(result.Success, $"Cast failed: {result.Reason}");
                Assert.AreEqual("spell_fire_bolt", result.SpellId);
                Assert.Greater(result.DamageDealt, 0f);
                Assert.AreEqual(1, result.TargetsHit);

                // Cue was raised
                Assert.AreEqual(CueIds.SpellCastFire, lastCue);
                Assert.AreEqual(result.DamageDealt, lastCueValue);

                // Spell cooldown is active
                Assert.IsFalse(spell.IsReady);
                Assert.Greater(spell.CooldownRemaining, 0f);

                // Casting again immediately fails due to cooldown
                var cdResult = SpellCastSimulator.ExecuteSpell(
                    spell,
                    prog,
                    casterX: 0f,
                    casterY: 0f,
                    targetDistance: 5f,
                    availableTargets: 1
                );
                Assert.IsFalse(cdResult.Success);
                StringAssert.Contains("cooldown", cdResult.Reason.ToLower());

                // Ticking cooldown restores readiness
                spell.TickCooldown(spellDef.Cooldown + 0.1f);
                Assert.IsTrue(spell.IsReady);

                // School mastery gained XP
                Assert.Greater(prog.GetMasteryXp(MagicSchoolType.Fire), 0f);
            }
            finally
            {
                CueBus.Requested -= cueHandler;
            }
        }
    }
}
