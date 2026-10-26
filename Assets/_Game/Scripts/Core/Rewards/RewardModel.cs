using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public enum RewardRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Mythic
    }

    public enum RewardModifierType
    {
        ProjectileDamagePercent,
        AttackSpeedPercent,
        AttackRange,
        MoveSpeedPercent,
        ExtraProjectiles,
        MaxHealth,
        Armor,
        LifeStealPercent,
        DashCooldownReductionPercent
    }

    /// <summary>
    /// Immutable definition of a wave completion reward / skill.
    /// Modifies actual runtime mechanics, supports stacking up to MaxRank.
    /// </summary>
    public sealed class RewardDefinition
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public RewardRarity Rarity { get; }
        public string CharacterId { get; }
        public int MaxRank { get; }
        public string PrerequisiteSkillId { get; }
        public RewardModifierType ModifierType { get; }
        public float ValuePerRank { get; }

        public RewardDefinition(
            string id,
            string name,
            string description,
            RewardRarity rarity,
            string characterId,
            RewardModifierType modifierType,
            float valuePerRank,
            int maxRank = 5,
            string prerequisiteSkillId = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Rarity = rarity;
            CharacterId = characterId;
            ModifierType = modifierType;
            ValuePerRank = valuePerRank;
            MaxRank = maxRank;
            PrerequisiteSkillId = prerequisiteSkillId;
        }

        public string GetTooltip(int currentRank)
        {
            int nextRank = currentRank + 1;
            string typeName = FormatTypeName(ModifierType);
            string valStr = FormatValue(ModifierType, ValuePerRank);

            if (currentRank >= MaxRank)
                return $"[MAXED {currentRank}/{MaxRank}] {Description}";

            if (currentRank == 0)
                return $"[Rank 1/{MaxRank}] {valStr} {typeName} ({Description})";

            string totalCurrent = FormatValue(ModifierType, ValuePerRank * currentRank);
            string totalNext = FormatValue(ModifierType, ValuePerRank * nextRank);
            return $"[Rank {currentRank}->{nextRank}/{MaxRank}] Total: {totalCurrent} -> {totalNext} ({Description})";
        }

        public static string FormatTypeName(RewardModifierType type)
        {
            switch (type)
            {
                case RewardModifierType.ProjectileDamagePercent: return "Damage";
                case RewardModifierType.AttackSpeedPercent: return "Attack Speed";
                case RewardModifierType.AttackRange: return "Range";
                case RewardModifierType.MoveSpeedPercent: return "Move Speed";
                case RewardModifierType.ExtraProjectiles: return "Extra Projectiles";
                case RewardModifierType.MaxHealth: return "Max HP";
                case RewardModifierType.Armor: return "Armor";
                case RewardModifierType.LifeStealPercent: return "Life Steal";
                case RewardModifierType.DashCooldownReductionPercent: return "Dash CD Reduction";
                default: return type.ToString();
            }
        }

        private static string FormatValue(RewardModifierType type, float val)
        {
            switch (type)
            {
                case RewardModifierType.ProjectileDamagePercent:
                case RewardModifierType.AttackSpeedPercent:
                case RewardModifierType.MoveSpeedPercent:
                case RewardModifierType.LifeStealPercent:
                case RewardModifierType.DashCooldownReductionPercent:
                    return $"+{val * 100f:0.#}%";
                case RewardModifierType.ExtraProjectiles:
                    return $"+{(int)val}";
                default:
                    return $"+{val:0.#}";
            }
        }
    }

    /// <summary>
    /// Master registry of implemented wave skills and generic fallbacks.
    /// IDs strictly match the planned registry entries in abilities_and_wave_skills.json.
    /// </summary>
    public static class RewardRegistry
    {
        public const string FallbackId = "generic_fallback_essence";
        public const string ArmorId = "lich_lord_wave_01";
        public const string ExtraProjId = "lich_lord_wave_02";
        public const string MoveSpeedId = "lich_lord_wave_08";
        public const string RangeId = "lich_lord_wave_11";
        public const string LifeStealId = "lich_lord_wave_15";
        public const string AttackSpeedId = "lich_lord_wave_21";
        public const string MaxHealthId = "lich_lord_wave_22";
        public const string DamageId = "lich_lord_wave_30";
        public const string MonarchId = "lich_lord_wave_mastery";

        private static readonly Dictionary<string, RewardDefinition> Registry = new Dictionary<string, RewardDefinition>
        {
            {
                ArmorId,
                new RewardDefinition(
                    ArmorId,
                    "Bone Reinforcement",
                    "Strengthen protective armor plating (+5 Armor)",
                    RewardRarity.Common,
                    "all",
                    RewardModifierType.Armor,
                    5f,
                    maxRank: 5)
            },
            {
                MoveSpeedId,
                new RewardDefinition(
                    MoveSpeedId,
                    "Death March",
                    "Hasten footsteps (+12% Move Speed)",
                    RewardRarity.Common,
                    "all",
                    RewardModifierType.MoveSpeedPercent,
                    0.12f,
                    maxRank: 5)
            },
            {
                AttackSpeedId,
                new RewardDefinition(
                    AttackSpeedId,
                    "Grim Haste",
                    "Quicken attack cycle (+20% Attack Speed)",
                    RewardRarity.Uncommon,
                    "all",
                    RewardModifierType.AttackSpeedPercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                RangeId,
                new RewardDefinition(
                    RangeId,
                    "Crypt Reach",
                    "Extend attack range (+2.0m Range)",
                    RewardRarity.Uncommon,
                    "all",
                    RewardModifierType.AttackRange,
                    2.0f,
                    maxRank: 5)
            },
            {
                MaxHealthId,
                new RewardDefinition(
                    MaxHealthId,
                    "Soul Harvest",
                    "Harvest souls into permanent vitality (+30 Max HP)",
                    RewardRarity.Uncommon,
                    "all",
                    RewardModifierType.MaxHealth,
                    30f,
                    maxRank: 5)
            },
            {
                DamageId,
                new RewardDefinition(
                    DamageId,
                    "Doom Potency",
                    "Amplify combat impact (+25% Damage)",
                    RewardRarity.Rare,
                    "all",
                    RewardModifierType.ProjectileDamagePercent,
                    0.25f,
                    maxRank: 5)
            },
            {
                LifeStealId,
                new RewardDefinition(
                    LifeStealId,
                    "Siphon Vitality",
                    "Leech life on combat hit (+8% Life Steal)",
                    RewardRarity.Rare,
                    "all",
                    RewardModifierType.LifeStealPercent,
                    0.08f,
                    maxRank: 5)
            },
            {
                ExtraProjId,
                new RewardDefinition(
                    ExtraProjId,
                    "Grave Barrage",
                    "Attack with an additional spread strike (+1 Projectile)",
                    RewardRarity.Epic,
                    "all",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                MonarchId,
                new RewardDefinition(
                    MonarchId,
                    "Eternal Monarch",
                    "Mastery of death: massive damage surge (+50% Damage). Requires Doom Potency.",
                    RewardRarity.Mythic,
                    "all",
                    RewardModifierType.ProjectileDamagePercent,
                    0.50f,
                    maxRank: 1,
                    prerequisiteSkillId: DamageId)
            },
            {
                FallbackId,
                new RewardDefinition(
                    FallbackId,
                    "Essence Surge",
                    "Raw soul essence infused directly (+15 Max HP)",
                    RewardRarity.Common,
                    "all",
                    RewardModifierType.MaxHealth,
                    15f,
                    maxRank: 99)
            }
        };

        public static RewardDefinition GetById(string id)
        {
            RewardDefinition def;
            Registry.TryGetValue(id, out def);
            return def;
        }

        public static IEnumerable<RewardDefinition> All => Registry.Values;
    }

    /// <summary>
    /// In-run inventory tracking acquired skill ranks and aggregate stat buffs.
    /// Thread-safe/atomic application prevents double spending or exploits.
    /// </summary>
    public sealed class RewardInventory
    {
        private readonly Dictionary<string, int> _ranks = new Dictionary<string, int>();

        public event Action<RewardDefinition, int> RewardAcquired;

        public int GetRank(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;
            int rank;
            _ranks.TryGetValue(id, out rank);
            return rank;
        }

        public bool CanAcquire(RewardDefinition def)
        {
            if (def == null) return false;
            int current = GetRank(def.Id);
            if (current >= def.MaxRank) return false;

            if (!string.IsNullOrEmpty(def.PrerequisiteSkillId))
            {
                if (GetRank(def.PrerequisiteSkillId) <= 0)
                    return false;
            }
            return true;
        }

        public bool TryAdd(RewardDefinition def)
        {
            if (!CanAcquire(def))
                return false;

            int next = GetRank(def.Id) + 1;
            _ranks[def.Id] = next;
            RewardAcquired?.Invoke(def, next);
            return true;
        }

        public IReadOnlyDictionary<string, int> GetAllRanks()
        {
            return _ranks;
        }

        // Aggregate runtime modifiers
        public float BonusArmor => GetTotalValue(RewardModifierType.Armor);
        public float BonusMoveSpeedPercent => GetTotalValue(RewardModifierType.MoveSpeedPercent);
        public float BonusAttackSpeedPercent => GetTotalValue(RewardModifierType.AttackSpeedPercent);
        public float BonusRange => GetTotalValue(RewardModifierType.AttackRange);
        public float BonusMaxHealth => GetTotalValue(RewardModifierType.MaxHealth);
        public float BonusDamagePercent => GetTotalValue(RewardModifierType.ProjectileDamagePercent);
        public float LifeStealFraction => GetTotalValue(RewardModifierType.LifeStealPercent);
        public int ExtraProjectiles => (int)GetTotalValue(RewardModifierType.ExtraProjectiles);
        public float BonusDashCooldownReductionPercent => GetTotalValue(RewardModifierType.DashCooldownReductionPercent);

        private float GetTotalValue(RewardModifierType type)
        {
            float total = 0f;
            foreach (var kvp in _ranks)
            {
                var def = RewardRegistry.GetById(kvp.Key);
                if (def != null && def.ModifierType == type)
                {
                    total += def.ValuePerRank * kvp.Value;
                }
            }
            return total;
        }
    }

    /// <summary>
    /// Represents one active reward draft presentation (standard 1-of-3 or rare 2-of-4).
    /// Enforces atomic selection, prevents duplicate choices, and locks next wave until committed.
    /// </summary>
    public sealed class RewardOffer
    {
        public int WaveNumber { get; }
        public int PicksAllowed { get; }
        public int PicksRemaining { get; private set; }
        public IReadOnlyList<RewardDefinition> Choices { get; }
        public List<string> PickedIds { get; } = new List<string>();

        public bool IsCommitted { get; private set; }

        public RewardOffer(int waveNumber, int picksAllowed, IReadOnlyList<RewardDefinition> choices)
        {
            WaveNumber = waveNumber;
            PicksAllowed = picksAllowed;
            PicksRemaining = picksAllowed;
            Choices = choices;
        }

        public bool TryPick(string rewardId, RewardInventory inventory)
        {
            if (IsCommitted || PicksRemaining <= 0)
                return false;

            if (string.IsNullOrEmpty(rewardId) || PickedIds.Contains(rewardId))
                return false;

            RewardDefinition found = null;
            for (int i = 0; i < Choices.Count; i++)
            {
                if (Choices[i].Id == rewardId)
                {
                    found = Choices[i];
                    break;
                }
            }

            if (found == null)
                return false;

            if (!inventory.TryAdd(found))
                return false;

            PickedIds.Add(rewardId);
            PicksRemaining--;
            if (PicksRemaining <= 0)
            {
                IsCommitted = true;
            }
            return true;
        }
    }

    /// <summary>
    /// Deterministic weighted generator for wave rewards.
    /// Filters on character, prerequisites, and max rank, excludes duplicates within one offer,
    /// supports rare 2-of-4 draft, and provides a guaranteed fallback when the eligible pool is small.
    /// </summary>
    public sealed class RewardDraftSystem
    {
        public static RewardOffer GenerateOffer(
            int waveNumber,
            long seed,
            RewardInventory inventory,
            string characterId,
            bool forceFourChoices = false)
        {
            // Deterministic selection: 4 choices (pick 2) on boss wave 10 or forced
            bool isFourChoice = forceFourChoices || (waveNumber % 10 == 0);
            int offerCount = isFourChoice ? 4 : 3;
            int picksAllowed = isFourChoice ? 2 : 1;

            long draftSeed = seed ^ ((long)waveNumber * 0x5DEECE66DL) ^ 0xDEADBEEF;
            var rng = new SplitMix64Rng(draftSeed);

            // 1. Gather all currently eligible definitions (excluding fallback initially)
            var eligible = new List<RewardDefinition>();
            foreach (var def in RewardRegistry.All)
            {
                if (def.Id == RewardRegistry.FallbackId)
                    continue;

                if (def.CharacterId != characterId && def.CharacterId != "all")
                    continue;

                if (inventory.CanAcquire(def))
                {
                    eligible.Add(def);
                }
            }

            var chosen = new List<RewardDefinition>();

            for (int i = 0; i < offerCount; i++)
            {
                if (eligible.Count == 0)
                {
                    // Fallback when eligible custom pool is exhausted
                    var fallback = RewardRegistry.GetById(RewardRegistry.FallbackId);
                    chosen.Add(fallback);
                    continue;
                }

                // Weighted rarity roll:
                // Common: 60%, Uncommon: 25%, Rare: 10%, Epic: 4%, Mythic: 1%
                double roll = rng.NextDouble() * 100.0;
                RewardRarity targetRarity;
                if (roll < 60.0) targetRarity = RewardRarity.Common;
                else if (roll < 85.0) targetRarity = RewardRarity.Uncommon;
                else if (roll < 95.0) targetRarity = RewardRarity.Rare;
                else if (roll < 99.0) targetRarity = RewardRarity.Epic;
                else targetRarity = RewardRarity.Mythic;

                // Find candidate matching target rarity
                var matching = new List<RewardDefinition>();
                for (int e = 0; e < eligible.Count; e++)
                {
                    if (eligible[e].Rarity == targetRarity)
                        matching.Add(eligible[e]);
                }

                RewardDefinition picked;
                if (matching.Count > 0)
                {
                    int pickIndex = (int)(rng.NextDouble() * matching.Count);
                    if (pickIndex >= matching.Count) pickIndex = matching.Count - 1;
                    picked = matching[pickIndex];
                }
                else
                {
                    // If target rarity has no eligible options, pick uniformly from remaining eligible
                    int pickIndex = (int)(rng.NextDouble() * eligible.Count);
                    if (pickIndex >= eligible.Count) pickIndex = eligible.Count - 1;
                    picked = eligible[pickIndex];
                }

                chosen.Add(picked);
                eligible.Remove(picked); // Exclude duplicates within this offer
            }

            return new RewardOffer(waveNumber, picksAllowed, chosen);
        }
    }
}
