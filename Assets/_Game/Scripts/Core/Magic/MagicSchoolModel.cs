using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public enum MagicSchoolType
    {
        Fire,
        Ice,
        Lightning,
        Water,
        Wind,
        Earth,
        Poison,
        Blood,
        Shadow,
        Light,
        Holy,
        Dark,
        Necromancy,
        Spirit,
        Arcane,
        Nature,
        Void,
        Chaos,
        Time,
        Metal
    }

    public enum DamageType
    {
        Physical,
        Fire,
        Ice,
        Lightning,
        Water,
        Wind,
        Earth,
        Poison,
        Blood,
        Shadow,
        Light,
        Holy,
        Dark,
        Necromancy,
        Spirit,
        Arcane,
        Nature,
        Void,
        Chaos,
        Time,
        Metal
    }

    public enum CastBehaviorType
    {
        DirectProjectile,
        RadialAoENova,
        ContinuousBeam,
        OrbitingShield,
        ChainingArc,
        GroundHazard,
        HomingSeeker,
        WaveCone,
        AuraPulse,
        FallingImpact
    }

    /// <summary>
    /// Pure C# immutable definition for a spell belonging to a Magic School.
    /// Supports tiered progression, distinct cast behaviors, status effects, and evolutions.
    /// </summary>
    public sealed class SpellDefinition
    {
        public string Id { get; }
        public string Name { get; }
        public MagicSchoolType School { get; }
        public int Tier { get; }
        public CastBehaviorType Behavior { get; }
        public DamageType DamageType { get; }
        public float BaseDamage { get; }
        public float Cooldown { get; }
        public float ResourceCost { get; }
        public float Range { get; }
        public float Radius { get; }
        public float Duration { get; }
        public float ProjectileSpeed { get; }
        public int ProjectileCount { get; }
        public int Pierces { get; }
        public int Bounces { get; }
        public string CueId { get; }
        public string Description { get; }
        public string EvolutionSpellId { get; }
        public string StatusEffectName { get; }
        public float StatusEffectDuration { get; }
        public float StatusEffectValue { get; }

        public SpellDefinition(
            string id,
            string name,
            MagicSchoolType school,
            int tier,
            CastBehaviorType behavior,
            DamageType damageType,
            float baseDamage,
            float cooldown,
            float resourceCost,
            float range,
            float radius = 0f,
            float duration = 0f,
            float projectileSpeed = 10f,
            int projectileCount = 1,
            int pierces = 0,
            int bounces = 0,
            string cueId = null,
            string description = "",
            string evolutionSpellId = null,
            string statusEffectName = null,
            float statusEffectDuration = 0f,
            float statusEffectValue = 0f)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Name = name ?? id;
            School = school;
            Tier = tier;
            Behavior = behavior;
            DamageType = damageType;
            BaseDamage = baseDamage;
            Cooldown = Math.Max(0.1f, cooldown);
            ResourceCost = Math.Max(0f, resourceCost);
            Range = Math.Max(0f, range);
            Radius = Math.Max(0f, radius);
            Duration = Math.Max(0f, duration);
            ProjectileSpeed = Math.Max(0f, projectileSpeed);
            ProjectileCount = Math.Max(1, projectileCount);
            Pierces = Math.Max(0, pierces);
            Bounces = Math.Max(0, bounces);
            CueId = cueId ?? CueIds.BoltFire;
            Description = description ?? "";
            EvolutionSpellId = evolutionSpellId;
            StatusEffectName = statusEffectName ?? "";
            StatusEffectDuration = Math.Max(0f, statusEffectDuration);
            StatusEffectValue = Math.Max(0f, statusEffectValue);
        }
    }

    /// <summary>
    /// Pure C# immutable definition for one of the 20 Magic Schools.
    /// </summary>
    public sealed class MagicSchoolDefinition
    {
        public MagicSchoolType School { get; }
        public string Name { get; }
        public string Title { get; }
        public string Description { get; }
        public DamageType PrimaryDamageType { get; }
        public string ColorHex { get; }
        public string PassivePerkDescription { get; }
        public IReadOnlyList<SpellDefinition> Spells { get; }
        public float BonusDamagePerMasteryRank { get; }
        public float CooldownReductionPerMasteryRank { get; }
        public MagicSchoolType MultiElementSynergyPartner { get; }
        public string SynergyName { get; }
        public string SynergyEffectDescription { get; }

        public MagicSchoolDefinition(
            MagicSchoolType school,
            string name,
            string title,
            string description,
            DamageType primaryDamageType,
            string colorHex,
            string passivePerkDescription,
            IReadOnlyList<SpellDefinition> spells,
            float bonusDamagePerMasteryRank = 0.10f,
            float cooldownReductionPerMasteryRank = 0.05f,
            MagicSchoolType synergyPartner = MagicSchoolType.Fire,
            string synergyName = "",
            string synergyEffectDescription = "")
        {
            School = school;
            Name = name ?? school.ToString();
            Title = title ?? name;
            Description = description ?? "";
            PrimaryDamageType = primaryDamageType;
            ColorHex = colorHex ?? "#FFFFFF";
            PassivePerkDescription = passivePerkDescription ?? "";
            Spells = spells ?? Array.Empty<SpellDefinition>();
            BonusDamagePerMasteryRank = bonusDamagePerMasteryRank;
            CooldownReductionPerMasteryRank = cooldownReductionPerMasteryRank;
            MultiElementSynergyPartner = synergyPartner;
            SynergyName = synergyName ?? "";
            SynergyEffectDescription = synergyEffectDescription ?? "";
        }
    }

    /// <summary>
    /// Leveled instance of an individual spell in a run.
    /// Tracks mastery XP, rank (1..5), evolution status, and cooldown.
    /// </summary>
    public sealed class SpellInstance
    {
        public const int MaxSpellLevel = 5;
        public const float XpPerLevel = 100f;

        public SpellDefinition Definition { get; private set; }
        public int Level { get; private set; }
        public float MasteryXp { get; private set; }
        public bool IsEvolved { get; private set; }
        public float CooldownRemaining { get; private set; }
        public bool AutocastEnabled { get; set; }

        public event Action<SpellInstance> LeveledUp;
        public event Action<SpellInstance> Evolved;

        public SpellInstance(SpellDefinition definition, int initialLevel = 1, bool autocast = true)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            Level = Math.Clamp(initialLevel, 1, MaxSpellLevel);
            AutocastEnabled = autocast;
        }

        public void AddXp(float amount)
        {
            if (amount <= 0f || Level >= MaxSpellLevel) return;
            MasteryXp += amount;
            float req = Level * XpPerLevel;
            while (MasteryXp >= req && Level < MaxSpellLevel)
            {
                MasteryXp -= req;
                Level++;
                LeveledUp?.Invoke(this);
                req = Level * XpPerLevel;
            }
        }

        public bool TryEvolve(SpellDefinition evolvedDef)
        {
            if (IsEvolved || evolvedDef == null || Level < MaxSpellLevel)
                return false;

            Definition = evolvedDef;
            IsEvolved = true;
            Evolved?.Invoke(this);
            CueBus.Raise(CueIds.SpellEvolved, 0f, 0f, (float)Definition.School);
            return true;
        }

        public float GetEffectiveDamage(float schoolMasteryBonus = 0f, float characterSpellPower = 0f)
        {
            float levelMultiplier = 1f + (Level - 1) * 0.12f;
            float totalMultiplier = levelMultiplier + schoolMasteryBonus + characterSpellPower;
            return Definition.BaseDamage * Math.Max(0.1f, totalMultiplier);
        }

        public float GetEffectiveCooldown(float schoolCdrBonus = 0f)
        {
            float levelCdr = (Level - 1) * 0.05f;
            float totalCdr = Math.Clamp(levelCdr + schoolCdrBonus, 0f, 0.75f);
            return Math.Max(0.1f, Definition.Cooldown * (1f - totalCdr));
        }

        public bool IsReady => CooldownRemaining <= 0f;

        public void TriggerCooldown(float schoolCdrBonus = 0f)
        {
            CooldownRemaining = GetEffectiveCooldown(schoolCdrBonus);
        }

        public void ResetCooldown()
        {
            CooldownRemaining = 0f;
        }

        public void TickCooldown(float dt)
        {
            if (dt <= 0f) return;
            if (CooldownRemaining > 0f)
            {
                CooldownRemaining -= dt;
                if (CooldownRemaining < 0f) CooldownRemaining = 0f;
            }
        }
    }

    /// <summary>
    /// Dual-school synergy representation granting enhanced combat bonuses.
    /// </summary>
    public sealed class MultiElementSynergy
    {
        public MagicSchoolType SchoolA { get; }
        public MagicSchoolType SchoolB { get; }
        public string SynergyName { get; }
        public string Description { get; }
        public float BonusDamagePercent { get; }
        public float BonusCooldownReductionPercent { get; }

        public MultiElementSynergy(
            MagicSchoolType schoolA,
            MagicSchoolType schoolB,
            string synergyName,
            string description,
            float bonusDamagePercent = 0.15f,
            float bonusCooldownReductionPercent = 0.05f)
        {
            SchoolA = schoolA;
            SchoolB = schoolB;
            SynergyName = synergyName;
            Description = description;
            BonusDamagePercent = bonusDamagePercent;
            BonusCooldownReductionPercent = bonusCooldownReductionPercent;
        }

        public bool Matches(MagicSchoolType a, MagicSchoolType b)
        {
            return (SchoolA == a && SchoolB == b) || (SchoolA == b && SchoolB == a);
        }
    }

    /// <summary>
    /// Manages player magic school masteries, spell leveling, synergies, and active spell slots.
    /// </summary>
    public sealed class MagicSchoolProgression
    {
        public const int MaxMasteryLevel = 5;
        public const int MaxEquippedSpells = 4;

        private readonly Dictionary<MagicSchoolType, int> _schoolMasteryLevels = new Dictionary<MagicSchoolType, int>();
        private readonly Dictionary<MagicSchoolType, float> _schoolMasteryXp = new Dictionary<MagicSchoolType, float>();
        private readonly List<SpellInstance> _equippedSpells = new List<SpellInstance>();
        private readonly HashSet<string> _unlockedSpellIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public event Action<MagicSchoolType, int> SchoolMasteryLevelUp;
        public event Action<SpellInstance> SpellEquipped;
        public event Action<SpellInstance> SpellUnequipped;

        public IReadOnlyList<SpellInstance> EquippedSpells => _equippedSpells;

        public MagicSchoolProgression()
        {
            foreach (MagicSchoolType school in Enum.GetValues(typeof(MagicSchoolType)))
            {
                _schoolMasteryLevels[school] = 1;
                _schoolMasteryXp[school] = 0f;
            }
        }

        public int GetMasteryLevel(MagicSchoolType school)
        {
            return _schoolMasteryLevels.TryGetValue(school, out int lvl) ? lvl : 1;
        }

        public float GetMasteryXp(MagicSchoolType school)
        {
            return _schoolMasteryXp.TryGetValue(school, out float xp) ? xp : 0f;
        }

        public static float GetMasteryXpRequired(int currentLevel)
        {
            return currentLevel switch
            {
                1 => 100f,
                2 => 250f,
                3 => 500f,
                4 => 1000f,
                _ => 2000f
            };
        }

        public void AddSchoolXp(MagicSchoolType school, float amount)
        {
            if (amount <= 0f) return;
            int curLevel = GetMasteryLevel(school);
            if (curLevel >= MaxMasteryLevel) return;

            float xp = GetMasteryXp(school) + amount;
            float req = GetMasteryXpRequired(curLevel);

            while (xp >= req && curLevel < MaxMasteryLevel)
            {
                xp -= req;
                curLevel++;
                _schoolMasteryLevels[school] = curLevel;
                SchoolMasteryLevelUp?.Invoke(school, curLevel);
                CueBus.Raise(CueIds.MagicMasteryLevelUp, 0f, 0f, (float)school);
                req = GetMasteryXpRequired(curLevel);
            }

            _schoolMasteryXp[school] = xp;
        }

        public float GetSchoolDamageMultiplier(MagicSchoolType school)
        {
            int level = GetMasteryLevel(school);
            return (level - 1) * 0.10f; // +10% damage bonus per mastery level above 1
        }

        public float GetSchoolCooldownReduction(MagicSchoolType school)
        {
            int level = GetMasteryLevel(school);
            return (level - 1) * 0.05f; // +5% CDR per mastery level above 1
        }

        public bool IsSpellUnlocked(string spellId)
        {
            return !string.IsNullOrEmpty(spellId) && _unlockedSpellIds.Contains(spellId);
        }

        public void UnlockSpell(string spellId)
        {
            if (!string.IsNullOrEmpty(spellId))
            {
                _unlockedSpellIds.Add(spellId);
            }
        }

        public bool TryEquipSpell(SpellDefinition def, out SpellInstance instance)
        {
            instance = null;
            if (def == null) return false;

            // Check if already equipped
            for (int i = 0; i < _equippedSpells.Count; i++)
            {
                if (string.Equals(_equippedSpells[i].Definition.Id, def.Id, StringComparison.OrdinalIgnoreCase))
                {
                    instance = _equippedSpells[i];
                    return true;
                }
            }

            if (_equippedSpells.Count >= MaxEquippedSpells)
                return false;

            UnlockSpell(def.Id);
            instance = new SpellInstance(def);
            _equippedSpells.Add(instance);
            SpellEquipped?.Invoke(instance);
            return true;
        }

        public bool TryUnequipSpell(string spellId)
        {
            for (int i = 0; i < _equippedSpells.Count; i++)
            {
                if (string.Equals(_equippedSpells[i].Definition.Id, spellId, StringComparison.OrdinalIgnoreCase))
                {
                    var inst = _equippedSpells[i];
                    _equippedSpells.RemoveAt(i);
                    SpellUnequipped?.Invoke(inst);
                    return true;
                }
            }
            return false;
        }

        public bool HasActiveSynergy(MultiElementSynergy synergy)
        {
            if (synergy == null) return false;
            bool hasA = false;
            bool hasB = false;
            for (int i = 0; i < _equippedSpells.Count; i++)
            {
                var s = _equippedSpells[i].Definition.School;
                if (s == synergy.SchoolA) hasA = true;
                if (s == synergy.SchoolB) hasB = true;
            }
            return hasA && hasB;
        }

        public void Tick(float dt)
        {
            for (int i = 0; i < _equippedSpells.Count; i++)
            {
                _equippedSpells[i].TickCooldown(dt);
            }
        }
    }

    public struct SpellCastResult
    {
        public bool Success;
        public string SpellId;
        public float DamageDealt;
        public int TargetsHit;
        public string Reason;

        public static SpellCastResult Fail(string spellId, string reason) => new SpellCastResult
        {
            Success = false,
            SpellId = spellId,
            DamageDealt = 0f,
            TargetsHit = 0,
            Reason = reason
        };

        public static SpellCastResult Succeeded(string spellId, float damage, int targets) => new SpellCastResult
        {
            Success = true,
            SpellId = spellId,
            DamageDealt = damage,
            TargetsHit = targets,
            Reason = string.Empty
        };
    }

    /// <summary>
    /// Simulates spell casting and execution logic in pure C#.
    /// Validates resources, triggers cooldowns, applies mastery damage scaling and synergies, and fires cues.
    /// </summary>
    public static class SpellCastSimulator
    {
        public static SpellCastResult ExecuteSpell(
            SpellInstance spell,
            MagicSchoolProgression progression,
            float casterX,
            float casterY,
            float targetDistance,
            int availableTargets,
            float availableResource = float.MaxValue,
            float characterSpellPower = 0f)
        {
            if (spell == null)
                return SpellCastResult.Fail("", "Null spell instance.");

            if (!spell.IsReady)
                return SpellCastResult.Fail(spell.Definition.Id, $"Spell on cooldown ({spell.CooldownRemaining:F1}s).");

            if (availableResource < spell.Definition.ResourceCost)
                return SpellCastResult.Fail(spell.Definition.Id, "Insufficient resource.");

            if (targetDistance > spell.Definition.Range && spell.Definition.Range > 0f)
                return SpellCastResult.Fail(spell.Definition.Id, "Target out of range.");

            // Calculate effective damage
            float masteryBonus = progression != null ? progression.GetSchoolDamageMultiplier(spell.Definition.School) : 0f;
            float cdrBonus = progression != null ? progression.GetSchoolCooldownReduction(spell.Definition.School) : 0f;
            float damage = spell.GetEffectiveDamage(masteryBonus, characterSpellPower);

            // Factor in targets hit based on behavior
            int targetsHit = 1;
            switch (spell.Definition.Behavior)
            {
                case CastBehaviorType.DirectProjectile:
                    targetsHit = Math.Min(availableTargets, 1 + spell.Definition.Pierces);
                    break;
                case CastBehaviorType.RadialAoENova:
                case CastBehaviorType.WaveCone:
                case CastBehaviorType.GroundHazard:
                case CastBehaviorType.FallingImpact:
                    targetsHit = Math.Min(availableTargets, Math.Max(1, (int)(spell.Definition.Radius * 2.5f)));
                    break;
                case CastBehaviorType.ChainingArc:
                    targetsHit = Math.Min(availableTargets, 1 + spell.Definition.Bounces);
                    break;
                case CastBehaviorType.HomingSeeker:
                    targetsHit = Math.Min(availableTargets, spell.Definition.ProjectileCount);
                    break;
                case CastBehaviorType.ContinuousBeam:
                    targetsHit = Math.Min(availableTargets, 3);
                    break;
                default:
                    targetsHit = Math.Min(availableTargets, 1);
                    break;
            }

            float totalDamage = damage * targetsHit;

            // Trigger cooldown
            spell.TriggerCooldown(cdrBonus);

            // Award XP to spell and school
            spell.AddXp(15f);
            if (progression != null)
            {
                progression.AddSchoolXp(spell.Definition.School, 20f);
            }

            // Fire presentation cue
            CueBus.Raise(spell.Definition.CueId, casterX, casterY, totalDamage);

            return SpellCastResult.Succeeded(spell.Definition.Id, totalDamage, targetsHit);
        }
    }
}
