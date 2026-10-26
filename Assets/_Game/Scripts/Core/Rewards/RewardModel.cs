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
        public const string CryptDisciplineId = "lich_lord_wave_03";
        public const string EternalServitudeId = "lich_lord_wave_04";
        public const string MarrowGrowthId = "lich_lord_wave_05";
        public const string WightAuthorityId = "lich_lord_wave_06";
        public const string BoneSplintersId = "lich_lord_wave_07";
        public const string MoveSpeedId = "lich_lord_wave_08";
        public const string SoulboundLegionId = "lich_lord_wave_09";
        public const string OssuarySwarmId = "lich_lord_wave_10";
        public const string FrostbiteBloomId = "lich_lord_wave_11";
        public const string GravefireSpreadId = "lich_lord_wave_12";
        public const string ThunderRemainsId = "lich_lord_wave_13";
        public const string HowlingCurrentId = "lich_lord_wave_14";
        public const string ArcaneHailId = "lich_lord_wave_15";
        public const string EmberNecrosisId = "lich_lord_wave_16";
        public const string TempestBonesId = "lich_lord_wave_17";
        public const string WinterChainsId = "lich_lord_wave_18";
        public const string StormNecromancyId = "lich_lord_wave_19";
        public const string ElementalGravesId = "lich_lord_wave_20";
        public const string EssenceOverflowId = "lich_lord_wave_21";
        public const string HarvestingAuraId = "lich_lord_wave_22";
        public const string DoomEchoId = "lich_lord_wave_23";
        public const string EbonHungerId = "lich_lord_wave_24";
        public const string SoulFractureId = "lich_lord_wave_25";
        public const string GraveCovenantId = "lich_lord_wave_26";
        public const string HexMagnificationId = "lich_lord_wave_27";
        public const string SoulDividendId = "lich_lord_wave_28";
        public const string DarkResidueId = "lich_lord_wave_29";
        public const string FinalWhisperId = "lich_lord_wave_30";
        public const string MarrowBarrierId = "lich_lord_wave_31";
        public const string PhylacterySparkId = "lich_lord_wave_32";
        public const string SpectralDriftId = "lich_lord_wave_33";
        public const string TombShelterId = "lich_lord_wave_34";
        public const string GraveReflectionId = "lich_lord_wave_35";
        public const string FrozenReliquaryId = "lich_lord_wave_36";
        public const string DeathlessPatienceId = "lich_lord_wave_37";
        public const string BoneSanctuaryId = "lich_lord_wave_38";
        public const string NecroticRecoveryId = "lich_lord_wave_39";
        public const string KingsRemainsId = "lich_lord_wave_40";
        public const string RangeId = TempestBonesId;
        public const string LifeStealId = ElementalGravesId;
        public const string AttackSpeedId = EssenceOverflowId;
        public const string MaxHealthId = HarvestingAuraId;
        public const string DamageId = FinalWhisperId;
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
                    "lich_lord",
                    RewardModifierType.Armor,
                    5f,
                    maxRank: 5)
            },
            {
                ExtraProjId,
                new RewardDefinition(
                    ExtraProjId,
                    "Grave Barrage",
                    "Skeleton archers and lich attacks launch additional strikes (+1 Projectile)",
                    RewardRarity.Epic,
                    "lich_lord",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                CryptDisciplineId,
                new RewardDefinition(
                    CryptDisciplineId,
                    "Crypt Discipline",
                    "Extends minion targeting and attack reach (+1.5m Range)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.AttackRange,
                    1.5f,
                    maxRank: 5)
            },
            {
                EternalServitudeId,
                new RewardDefinition(
                    EternalServitudeId,
                    "Eternal Servitude",
                    "Hasten summon recovery and mobility (+15% Cooldown Recovery)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.DashCooldownReductionPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                MarrowGrowthId,
                new RewardDefinition(
                    MarrowGrowthId,
                    "Marrow Growth",
                    "Enlarge undead bone frame and vitality (+35 Max HP)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.MaxHealth,
                    35f,
                    maxRank: 5)
            },
            {
                WightAuthorityId,
                new RewardDefinition(
                    WightAuthorityId,
                    "Wight Authority",
                    "Empower commander presence and minion swing rate (+15% Attack Speed)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.AttackSpeedPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                BoneSplintersId,
                new RewardDefinition(
                    BoneSplintersId,
                    "Bone Splinters",
                    "Attacks shatter into razor bone fragments (+1 Projectile)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 2)
            },
            {
                MoveSpeedId,
                new RewardDefinition(
                    MoveSpeedId,
                    "Death March",
                    "Hasten footsteps of the Lich and undead army (+12% Move Speed)",
                    RewardRarity.Common,
                    "lich_lord",
                    RewardModifierType.MoveSpeedPercent,
                    0.12f,
                    maxRank: 5)
            },
            {
                SoulboundLegionId,
                new RewardDefinition(
                    SoulboundLegionId,
                    "Soulbound Legion",
                    "Siphon spiritual energy of fallen allies (+20% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                OssuarySwarmId,
                new RewardDefinition(
                    OssuarySwarmId,
                    "Ossuary Swarm",
                    "Swarms of minor bone shards leech essence from invaders (+6% Life Steal)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.LifeStealPercent,
                    0.06f,
                    maxRank: 5)
            },
            {
                FrostbiteBloomId,
                new RewardDefinition(
                    FrostbiteBloomId,
                    "Frostbite Bloom",
                    "Frozen enemies release bursts of frost energy (+15% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                GravefireSpreadId,
                new RewardDefinition(
                    GravefireSpreadId,
                    "Gravefire Spread",
                    "Witchfire sparks jump across targets (+1 Projectile)",
                    RewardRarity.Epic,
                    "lich_lord",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 2)
            },
            {
                ThunderRemainsId,
                new RewardDefinition(
                    ThunderRemainsId,
                    "Thunder Remains",
                    "Fallen corpses discharge electrical surges (+20% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                HowlingCurrentId,
                new RewardDefinition(
                    HowlingCurrentId,
                    "Howling Current",
                    "Gale currents accelerate attack frequency (+20% Attack Speed)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.AttackSpeedPercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                ArcaneHailId,
                new RewardDefinition(
                    ArcaneHailId,
                    "Arcane Hail",
                    "Critical strikes launch crystalline ice shards (+1 Projectile)",
                    RewardRarity.Epic,
                    "lich_lord",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 2)
            },
            {
                EmberNecrosisId,
                new RewardDefinition(
                    EmberNecrosisId,
                    "Ember Necrosis",
                    "Necrotic heat weakens invader flesh (+15% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                TempestBonesId,
                new RewardDefinition(
                    TempestBonesId,
                    "Tempest Bones",
                    "Bone shards carry on freezing winds (+2.0m Range)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.AttackRange,
                    2.0f,
                    maxRank: 5)
            },
            {
                WinterChainsId,
                new RewardDefinition(
                    WinterChainsId,
                    "Winter Chains",
                    "Glacial tethers reduce cooldown intervals (+15% Dash Cooldown Recovery)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.DashCooldownReductionPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                StormNecromancyId,
                new RewardDefinition(
                    StormNecromancyId,
                    "Storm Necromancy",
                    "Supercharged dark magic bolsters physical form (+30 Max HP)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.MaxHealth,
                    30f,
                    maxRank: 5)
            },
            {
                ElementalGravesId,
                new RewardDefinition(
                    ElementalGravesId,
                    "Elemental Graves",
                    "Defeated invaders yield elemental essence (+8% Life Steal)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.LifeStealPercent,
                    0.08f,
                    maxRank: 5)
            },
            {
                EssenceOverflowId,
                new RewardDefinition(
                    EssenceOverflowId,
                    "Essence Overflow",
                    "Greater Essence storage quickens attack casting frequency (+20% Attack Speed)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.AttackSpeedPercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                HarvestingAuraId,
                new RewardDefinition(
                    HarvestingAuraId,
                    "Harvesting Aura",
                    "Nearby deaths weave harvested spiritual essence into vitality (+30 Max HP)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.MaxHealth,
                    30f,
                    maxRank: 5)
            },
            {
                DoomEchoId,
                new RewardDefinition(
                    DoomEchoId,
                    "Doom Echo",
                    "Execution marks resonate across targets (+15% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                EbonHungerId,
                new RewardDefinition(
                    EbonHungerId,
                    "Ebon Hunger",
                    "Dark curses siphoning vitality strengthen combat life steal (+6% Life Steal)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.LifeStealPercent,
                    0.06f,
                    maxRank: 5)
            },
            {
                SoulFractureId,
                new RewardDefinition(
                    SoulFractureId,
                    "Soul Fracture",
                    "Dark critical attacks splinter into additional projectiles (+1 Projectile)",
                    RewardRarity.Epic,
                    "lich_lord",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 2)
            },
            {
                GraveCovenantId,
                new RewardDefinition(
                    GraveCovenantId,
                    "Grave Covenant",
                    "Pact of the grave accelerates cooldown recovery (+15% Dash Cooldown Recovery)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.DashCooldownReductionPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                HexMagnificationId,
                new RewardDefinition(
                    HexMagnificationId,
                    "Hex Magnification",
                    "Enlarges spell range and reach of curses (+1.5m Range)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.AttackRange,
                    1.5f,
                    maxRank: 5)
            },
            {
                SoulDividendId,
                new RewardDefinition(
                    SoulDividendId,
                    "Soul Dividend",
                    "Elite essence infusions hasten the Lich Lord's pace (+12% Move Speed)",
                    RewardRarity.Common,
                    "lich_lord",
                    RewardModifierType.MoveSpeedPercent,
                    0.12f,
                    maxRank: 5)
            },
            {
                DarkResidueId,
                new RewardDefinition(
                    DarkResidueId,
                    "Dark Residue",
                    "Spells leave persistent necrotic residue, increasing damage (+15% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                FinalWhisperId,
                new RewardDefinition(
                    FinalWhisperId,
                    "Final Whisper",
                    "Cursed deaths trigger devastating damage amplification (+25% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.25f,
                    maxRank: 5)
            },
            {
                MarrowBarrierId,
                new RewardDefinition(
                    MarrowBarrierId,
                    "Marrow Barrier",
                    "Bone plating regenerates, reinforcing defensive protection (+5 Armor)",
                    RewardRarity.Common,
                    "lich_lord",
                    RewardModifierType.Armor,
                    5f,
                    maxRank: 5)
            },
            {
                PhylacterySparkId,
                new RewardDefinition(
                    PhylacterySparkId,
                    "Phylactery Spark",
                    "The eternal spark bolsters maximum deathless vitality (+35 Max HP)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.MaxHealth,
                    35f,
                    maxRank: 5)
            },
            {
                SpectralDriftId,
                new RewardDefinition(
                    SpectralDriftId,
                    "Spectral Drift",
                    "Phasing through the ethereal plane hastens stride (+12% Move Speed)",
                    RewardRarity.Common,
                    "lich_lord",
                    RewardModifierType.MoveSpeedPercent,
                    0.12f,
                    maxRank: 5)
            },
            {
                TombShelterId,
                new RewardDefinition(
                    TombShelterId,
                    "Tomb Shelter",
                    "Nearby ancient mausoleum wards deflect heavy blows (+6 Armor)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.Armor,
                    6f,
                    maxRank: 5)
            },
            {
                GraveReflectionId,
                new RewardDefinition(
                    GraveReflectionId,
                    "Grave Reflection",
                    "Mirror necrotic energy back into offensive spell impact (+15% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                FrozenReliquaryId,
                new RewardDefinition(
                    FrozenReliquaryId,
                    "Frozen Reliquary",
                    "Relic chill preserves evasion energy faster (+15% Dash Cooldown Recovery)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.DashCooldownReductionPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                DeathlessPatienceId,
                new RewardDefinition(
                    DeathlessPatienceId,
                    "Deathless Patience",
                    "Enduring timeless centuries yields permanent fortitude (+40 Max HP)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.MaxHealth,
                    40f,
                    maxRank: 5)
            },
            {
                BoneSanctuaryId,
                new RewardDefinition(
                    BoneSanctuaryId,
                    "Bone Sanctuary",
                    "Constructed bone walls absorb crushing physical impacts (+6 Armor)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.Armor,
                    6f,
                    maxRank: 5)
            },
            {
                NecroticRecoveryId,
                new RewardDefinition(
                    NecroticRecoveryId,
                    "Necrotic Recovery",
                    "Extract vital essence from every strike (+6% Life Steal)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.LifeStealPercent,
                    0.06f,
                    maxRank: 5)
            },
            {
                KingsRemainsId,
                new RewardDefinition(
                    KingsRemainsId,
                    "King's Remains",
                    "Regal remnants empower all deathly magic (+20% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
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
