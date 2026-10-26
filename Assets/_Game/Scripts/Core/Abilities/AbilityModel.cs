using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public struct AbilityExecutionResult
    {
        public bool Success;
        public string Message;
        public float DamageDealt;
        public int TargetsHit;
        public float HealingDone;
        public int SummonsCreated;
        public float BarrierGranted;

        public bool IsUseful => DamageDealt > 0f || TargetsHit > 0 || HealingDone > 0f || SummonsCreated > 0 || BarrierGranted > 0f;
    }

    [Flags]
    public enum AbilityEffectType
    {
        None = 0,
        Summon = 1 << 0,
        Projectile = 1 << 1,
        MeleeArc = 1 << 2,
        RadialAoE = 1 << 3,
        Dash = 1 << 4,
        Shield = 1 << 5,
        CrowdControl = 1 << 6,
        Heal = 1 << 7,
        Hazard = 1 << 8,
        Gravity = 1 << 9,
        Transform = 1 << 10
    }

    public enum AbilityTargetingRule
    {
        Self = 0,
        AimDirection = 1,
        NearestInvader = 2,
        AreaCursorOrAim = 3
    }

    [Serializable]
    public sealed class AbilityRankMilestone
    {
        public int Rank { get; }
        public string Description { get; }
        public float CooldownMultiplier { get; }
        public float ResourceCostMultiplier { get; }
        public float DamageMultiplier { get; }
        public float RadiusMultiplier { get; }
        public int ExtraSummonsOrProjectiles { get; }
        public float BonusUtility { get; }

        public AbilityRankMilestone(
            int rank,
            string description,
            float cooldownMultiplier = 1f,
            float resourceCostMultiplier = 1f,
            float damageMultiplier = 1f,
            float radiusMultiplier = 1f,
            int extraSummonsOrProjectiles = 0,
            float bonusUtility = 0f)
        {
            Rank = rank;
            Description = description;
            CooldownMultiplier = cooldownMultiplier;
            ResourceCostMultiplier = resourceCostMultiplier;
            DamageMultiplier = damageMultiplier;
            RadiusMultiplier = radiusMultiplier;
            ExtraSummonsOrProjectiles = extraSummonsOrProjectiles;
            BonusUtility = bonusUtility;
        }
    }

    /// <summary>
    /// Pure C# immutable specification for an active ability or ultimate.
    /// Definition-driven per 09_SKILL_TREES_ABILITIES_AND_MASTERY.md.
    /// </summary>
    public sealed class AbilityDefinition
    {
        public string Id { get; }
        public string CharacterId { get; }
        public string Name { get; }
        public string Description { get; }
        public string BranchName { get; }
        public int Tier { get; }
        public string PrerequisiteAbilityId { get; }
        public float ResourceCost { get; }
        public float CooldownDuration { get; }
        public float Range { get; }
        public AbilityEffectType Effects { get; }
        public AbilityTargetingRule Targeting { get; }
        public float BaseDamage { get; }
        public float BaseRadius { get; }
        public float Duration { get; }
        public bool IsUltimate { get; }
        public int RequiredLevel { get; }
        public bool EligibleForAutocast { get; }
        public int MaxRank { get; }
        public IReadOnlyList<AbilityRankMilestone> Milestones { get; }

        public AbilityDefinition(
            string id,
            string characterId,
            string name,
            string description,
            string branchName,
            int tier,
            string prerequisiteAbilityId,
            float resourceCost,
            float cooldownDuration,
            float range,
            AbilityEffectType effects,
            AbilityTargetingRule targeting,
            float baseDamage,
            float baseRadius,
            float duration = 0f,
            bool isUltimate = false,
            int requiredLevel = 1,
            bool eligibleForAutocast = true,
            int maxRank = 20,
            IReadOnlyList<AbilityRankMilestone> milestones = null)
        {
            Id = id;
            CharacterId = characterId;
            Name = name;
            Description = description;
            BranchName = branchName;
            Tier = tier;
            PrerequisiteAbilityId = prerequisiteAbilityId;
            ResourceCost = resourceCost;
            CooldownDuration = cooldownDuration;
            Range = range;
            Effects = effects;
            Targeting = targeting;
            BaseDamage = baseDamage;
            BaseRadius = baseRadius;
            Duration = duration;
            IsUltimate = isUltimate;
            RequiredLevel = requiredLevel;
            EligibleForAutocast = eligibleForAutocast;
            MaxRank = maxRank;
            Milestones = milestones ?? Array.Empty<AbilityRankMilestone>();
        }
    }

    /// <summary>
    /// Evaluates ability rank stats, XP thresholds, mutations, and post-20 ascension.
    /// </summary>
    public static class AbilityProgressionMath
    {
        public const int MaxStandardRank = 20;
        public const int MaxAscensionRank = 10;
        public const int BaseXpPerRank = 100;
        public const int XpGrowthPerRank = 50;

        public static int GetXpRequiredForNextRank(int currentRank)
        {
            if (currentRank < 1) return BaseXpPerRank;
            if (currentRank >= MaxStandardRank)
            {
                // Ascension XP
                return 1000 + (currentRank - MaxStandardRank) * 500;
            }
            return BaseXpPerRank + (currentRank - 1) * XpGrowthPerRank;
        }

        public static float CalculateEffectiveCooldown(AbilityDefinition def, int rank, int ascensionRank = 0)
        {
            if (def == null) return 0f;
            float cd = def.CooldownDuration;

            // Milestones at rank 5, 10, 15, 20
            if (def.Milestones != null)
            {
                for (int i = 0; i < def.Milestones.Count; i++)
                {
                    var m = def.Milestones[i];
                    if (rank >= m.Rank)
                    {
                        cd *= m.CooldownMultiplier;
                    }
                }
            }

            // Post-20 ascension: bounded small bonus (1% per ascension rank, capped at 10%)
            if (ascensionRank > 0)
            {
                float ascReduction = Math.Min(ascensionRank, MaxAscensionRank) * 0.01f;
                cd *= (1f - ascReduction);
            }

            return Math.Max(0.2f, cd);
        }

        public static float CalculateEffectiveResourceCost(AbilityDefinition def, int rank, int ascensionRank = 0)
        {
            if (def == null) return 0f;
            float cost = def.ResourceCost;

            if (def.Milestones != null)
            {
                for (int i = 0; i < def.Milestones.Count; i++)
                {
                    var m = def.Milestones[i];
                    if (rank >= m.Rank)
                    {
                        cost *= m.ResourceCostMultiplier;
                    }
                }
            }

            if (ascensionRank > 0)
            {
                float ascReduction = Math.Min(ascensionRank, MaxAscensionRank) * 0.01f;
                cost *= (1f - ascReduction);
            }

            return Math.Max(0f, cost);
        }

        public static float CalculateEffectiveDamage(AbilityDefinition def, int rank, int ascensionRank = 0)
        {
            if (def == null) return 0f;
            float dmg = def.BaseDamage;

            // Base progression: +5% damage per rank above 1
            if (rank > 1)
            {
                dmg += def.BaseDamage * (rank - 1) * 0.05f;
            }

            // Milestones
            if (def.Milestones != null)
            {
                for (int i = 0; i < def.Milestones.Count; i++)
                {
                    var m = def.Milestones[i];
                    if (rank >= m.Rank)
                    {
                        dmg *= m.DamageMultiplier;
                    }
                }
            }

            // Post-20 ascension: bounded +2% per ascension rank
            if (ascensionRank > 0)
            {
                float ascBonus = Math.Min(ascensionRank, MaxAscensionRank) * 0.02f;
                dmg *= (1f + ascBonus);
            }

            return dmg;
        }

        public static float CalculateEffectiveRadius(AbilityDefinition def, int rank)
        {
            if (def == null) return 0f;
            float rad = def.BaseRadius;

            if (def.Milestones != null)
            {
                for (int i = 0; i < def.Milestones.Count; i++)
                {
                    var m = def.Milestones[i];
                    if (rank >= m.Rank)
                    {
                        rad *= m.RadiusMultiplier;
                    }
                }
            }

            return rad;
        }

        public static int CalculateEffectiveSummonCount(AbilityDefinition def, int rank)
        {
            if (def == null) return 1;
            int count = 1;

            if (def.Milestones != null)
            {
                for (int i = 0; i < def.Milestones.Count; i++)
                {
                    var m = def.Milestones[i];
                    if (rank >= m.Rank)
                    {
                        count += m.ExtraSummonsOrProjectiles;
                    }
                }
            }

            return count;
        }
    }

    /// <summary>
    /// Pure C# 5-slot loadout container.
    /// Slots 0..4 (max 5 slots, keys 1-5).
    /// Enforces:
    /// - Max 5 equipped slots.
    /// - Ultimates consume 1 of the 5 slots.
    /// - Basic attack remains outside slots.
    /// - Cannot equip locked or duplicate abilities.
    /// </summary>
    public sealed class AbilityLoadout
    {
        public const int SlotCount = 5;

        private readonly string[] _slots = new string[SlotCount];
        private readonly bool[] _autocast = new bool[SlotCount];

        public IReadOnlyList<string> Slots => _slots;
        public IReadOnlyList<bool> AutocastSettings => _autocast;

        public AbilityLoadout()
        {
            for (int i = 0; i < SlotCount; i++)
            {
                _slots[i] = string.Empty;
                _autocast[i] = false;
            }
        }

        public AbilityLoadout(IList<string> initialSlots, IList<bool> initialAutocast = null) : this()
        {
            if (initialSlots != null)
            {
                for (int i = 0; i < Math.Min(SlotCount, initialSlots.Count); i++)
                {
                    _slots[i] = initialSlots[i] ?? string.Empty;
                }
            }

            if (initialAutocast != null)
            {
                for (int i = 0; i < Math.Min(SlotCount, initialAutocast.Count); i++)
                {
                    _autocast[i] = initialAutocast[i];
                }
            }
        }

        public string GetSlot(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= SlotCount) return string.Empty;
            return _slots[slotIndex] ?? string.Empty;
        }

        public bool IsAutocastEnabled(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= SlotCount) return false;
            return _autocast[slotIndex];
        }

        public void SetAutocast(int slotIndex, bool enabled)
        {
            if (slotIndex >= 0 && slotIndex < SlotCount)
            {
                _autocast[slotIndex] = enabled;
            }
        }

        public void ToggleAutocast(int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < SlotCount)
            {
                _autocast[slotIndex] = !_autocast[slotIndex];
            }
        }

        public bool TryEquip(
            int slotIndex,
            string abilityId,
            Func<string, bool> isUnlockedValidator,
            out string rejectionReason)
        {
            rejectionReason = string.Empty;

            if (slotIndex < 0 || slotIndex >= SlotCount)
            {
                rejectionReason = $"Slot index {slotIndex} out of bounds (0..{SlotCount - 1}). Maximum {SlotCount} slots allowed.";
                return false;
            }

            if (string.IsNullOrEmpty(abilityId))
            {
                _slots[slotIndex] = string.Empty;
                return true;
            }

            if (isUnlockedValidator != null && !isUnlockedValidator(abilityId))
            {
                rejectionReason = $"Ability '{abilityId}' is locked. Unlock it in the skill tree first.";
                return false;
            }

            // Check if already equipped in another slot
            for (int i = 0; i < SlotCount; i++)
            {
                if (i != slotIndex && _slots[i] == abilityId)
                {
                    rejectionReason = $"Ability '{abilityId}' is already equipped in slot {i + 1}.";
                    return false;
                }
            }

            _slots[slotIndex] = abilityId;
            return true;
        }

        public bool TryUnequip(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= SlotCount) return false;
            _slots[slotIndex] = string.Empty;
            _autocast[slotIndex] = false;
            return true;
        }

        public int FindSlot(string abilityId)
        {
            if (string.IsNullOrEmpty(abilityId)) return -1;
            for (int i = 0; i < SlotCount; i++)
            {
                if (_slots[i] == abilityId) return i;
            }
            return -1;
        }
    }
}
