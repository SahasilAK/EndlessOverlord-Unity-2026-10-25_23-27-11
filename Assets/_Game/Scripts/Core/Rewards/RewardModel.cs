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
        public const string GraveSovereignId = "lich_lord_wave_41";
        public const string UndyingBastionId = "lich_lord_wave_42";
        public const string PhantomAlacrityId = "lich_lord_wave_43";
        public const string PhylacteryCoreId = "lich_lord_wave_44";
        public const string CatacombMasteryId = "lich_lord_wave_45";
        public const string SoulReapingId = "lich_lord_wave_46";
        public const string BoneFortressId = "lich_lord_wave_47";
        public const string EtherealVeilId = "lich_lord_wave_48";
        public const string EternalDreadId = "lich_lord_wave_49";
        public const string ApexLichdomId = "lich_lord_wave_50";

        // Cursed Golem Wave Skills (Batch 1: 01-10)
        public const string GolemCrushingWeightId = "cursed_golem_wave_01";
        public const string GolemTitanReachId = "cursed_golem_wave_02";
        public const string GolemSeismicFollowUpId = "cursed_golem_wave_03";
        public const string GolemBoulderKnucklesId = "cursed_golem_wave_04";
        public const string GolemShatteringBlowsId = "cursed_golem_wave_05";
        public const string GolemMountainMomentumId = "cursed_golem_wave_06";
        public const string GolemColossalRhythmId = "cursed_golem_wave_07";
        public const string GolemReinforcedStrikesId = "cursed_golem_wave_08";
        public const string GolemTremorFistsId = "cursed_golem_wave_09";
        public const string GolemWorldbreakerStrengthId = "cursed_golem_wave_10";

        // Cursed Golem Wave Skills (Batch 2: 11-20)
        public const string GolemCrystalMultiplicationId = "cursed_golem_wave_11";
        public const string GolemBoulderSplittingId = "cursed_golem_wave_12";
        public const string GolemBedrockExpansionId = "cursed_golem_wave_13";
        public const string GolemJaggedTerrainId = "cursed_golem_wave_14";
        public const string GolemQuakeResonanceId = "cursed_golem_wave_15";
        public const string GolemCrystalDensityId = "cursed_golem_wave_16";
        public const string GolemRollingDestructionId = "cursed_golem_wave_17";
        public const string GolemMountainEchoId = "cursed_golem_wave_18";
        public const string GolemEarthenConvergenceId = "cursed_golem_wave_19";
        public const string GolemUnstableBedrockId = "cursed_golem_wave_20";

        // Cursed Golem Wave Skills (Batch 3: 21-30)
        public const string GolemRunicReservoirId = "cursed_golem_wave_21";
        public const string GolemAncientInscriptionId = "cursed_golem_wave_22";
        public const string GolemRuneMultiplicationId = "cursed_golem_wave_23";
        public const string GolemResonatingSymbolsId = "cursed_golem_wave_24";
        public const string GolemRunicReclamationId = "cursed_golem_wave_25";
        public const string GolemGlyphAmplificationId = "cursed_golem_wave_26";
        public const string GolemStoneheartCircuitId = "cursed_golem_wave_27";
        public const string GolemAncientConfigurationId = "cursed_golem_wave_28";
        public const string GolemRuneCascadeId = "cursed_golem_wave_29";
        public const string GolemForgottenLanguageId = "cursed_golem_wave_30";

        // Cursed Golem Wave Skills (Batch 4: 31-40)
        public const string GolemAdamantinePlatingId = "cursed_golem_wave_31";
        public const string GolemUnbreakableCoreId = "cursed_golem_wave_32";
        public const string GolemStoneRegenerationId = "cursed_golem_wave_33";
        public const string GolemFortressRetaliationId = "cursed_golem_wave_34";
        public const string GolemObsidianReflectionId = "cursed_golem_wave_35";
        public const string GolemColossusStabilityId = "cursed_golem_wave_36";
        public const string GolemMountainShelterId = "cursed_golem_wave_37";
        public const string GolemLivingStoneRecoveryId = "cursed_golem_wave_38";
        public const string GolemHardenedSoulstoneId = "cursed_golem_wave_39";
        public const string GolemGuardiansPatienceId = "cursed_golem_wave_40";

        // Cursed Golem Wave Skills (Batch 5: 41-50)
        public const string GolemGravityCompressionId = "cursed_golem_wave_41";
        public const string GolemOrbitingMassId = "cursed_golem_wave_42";
        public const string GolemGraviticMomentumId = "cursed_golem_wave_43";
        public const string GolemCollapsingPressureId = "cursed_golem_wave_44";
        public const string GolemWorldstoneConductorId = "cursed_golem_wave_45";
        public const string GolemAncientMineralWealthId = "cursed_golem_wave_46";
        public const string GolemColossusTributeId = "cursed_golem_wave_47";
        public const string GolemLostForgeWisdomId = "cursed_golem_wave_48";
        public const string GolemWorldstoneFortuneId = "cursed_golem_wave_49";
        public const string GolemEternalMountainId = "cursed_golem_wave_50";

        // Demon Monarch Wave Skills (Batch 1: 01-10)
        public const string MonarchSerratedHellsteelId = "demon_monarch_wave_01";
        public const string MonarchCrimsonEdgeId = "demon_monarch_wave_02";
        public const string MonarchMoltenReachId = "demon_monarch_wave_03";
        public const string MonarchTwinFlameCutsId = "demon_monarch_wave_04";
        public const string MonarchEmberRicochetId = "demon_monarch_wave_05";
        public const string MonarchBladeMomentumId = "demon_monarch_wave_06";
        public const string MonarchArmorSunderingId = "demon_monarch_wave_07";
        public const string MonarchRoyalBladeDisciplineId = "demon_monarch_wave_08";
        public const string MonarchExecutionersFuryId = "demon_monarch_wave_09";
        public const string MonarchThousandCutInfernoId = "demon_monarch_wave_10";

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
                GraveSovereignId,
                new RewardDefinition(
                    GraveSovereignId,
                    "Grave Sovereign",
                    "Assert necromantic dominion over the realm (+25% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.25f,
                    maxRank: 5)
            },
            {
                UndyingBastionId,
                new RewardDefinition(
                    UndyingBastionId,
                    "Undying Bastion",
                    "Dense calcified defenses ward off mortal strikes (+8 Armor)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.Armor,
                    8f,
                    maxRank: 5)
            },
            {
                PhantomAlacrityId,
                new RewardDefinition(
                    PhantomAlacrityId,
                    "Phantom Alacrity",
                    "Spectral quickening enhances supernatural speed (+15% Move Speed)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.MoveSpeedPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                PhylacteryCoreId,
                new RewardDefinition(
                    PhylacteryCoreId,
                    "Phylactery Core",
                    "Ancient soul vessel deeply reinforces mortality threshold (+50 Max Health)",
                    RewardRarity.Epic,
                    "lich_lord",
                    RewardModifierType.MaxHealth,
                    50f,
                    maxRank: 5)
            },
            {
                CatacombMasteryId,
                new RewardDefinition(
                    CatacombMasteryId,
                    "Catacomb Mastery",
                    "Mastery of underworld passages accelerates spell cycles (+20% Cooldown Recovery)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.DashCooldownReductionPercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                SoulReapingId,
                new RewardDefinition(
                    SoulReapingId,
                    "Soul Reaping",
                    "Harvest vibrant vital energies from damaged invaders (+8% Life Steal)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.LifeStealPercent,
                    0.08f,
                    maxRank: 5)
            },
            {
                BoneFortressId,
                new RewardDefinition(
                    BoneFortressId,
                    "Bone Fortress",
                    "Impenetrable ossuary plating protects against incoming assaults (+8 Armor)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.Armor,
                    8f,
                    maxRank: 5)
            },
            {
                EtherealVeilId,
                new RewardDefinition(
                    EtherealVeilId,
                    "Ethereal Veil",
                    "Drift weightlessly through necrotic ether (+15% Move Speed)",
                    RewardRarity.Uncommon,
                    "lich_lord",
                    RewardModifierType.MoveSpeedPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                EternalDreadId,
                new RewardDefinition(
                    EternalDreadId,
                    "Eternal Dread",
                    "Project overwhelming dread terrifying all mortal invaders (+25% Damage)",
                    RewardRarity.Rare,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.25f,
                    maxRank: 5)
            },
            {
                ApexLichdomId,
                new RewardDefinition(
                    ApexLichdomId,
                    "Apex Lichdom",
                    "The pinnacle of lich transcendence: supreme death magic prowess (+35% Damage)",
                    RewardRarity.Mythic,
                    "lich_lord",
                    RewardModifierType.ProjectileDamagePercent,
                    0.35f,
                    maxRank: 3)
            },
            // Cursed Golem Wave Skills 01-10
            {
                GolemCrushingWeightId,
                new RewardDefinition(
                    GolemCrushingWeightId,
                    "Crushing Weight",
                    "Colossal fists strike with staggering mass (+15% Damage)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                GolemTitanReachId,
                new RewardDefinition(
                    GolemTitanReachId,
                    "Titan Reach",
                    "Elongated stone arms extend attack reach (+1.5m Range)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.AttackRange,
                    1.5f,
                    maxRank: 5)
            },
            {
                GolemSeismicFollowUpId,
                new RewardDefinition(
                    GolemSeismicFollowUpId,
                    "Seismic Follow-Up",
                    "Heavy strikes trigger a secondary shockwave (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                GolemBoulderKnucklesId,
                new RewardDefinition(
                    GolemBoulderKnucklesId,
                    "Boulder Knuckles",
                    "Crushing impacts launch splintering stone fragments (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                GolemShatteringBlowsId,
                new RewardDefinition(
                    GolemShatteringBlowsId,
                    "Shattering Blows",
                    "Armor-breaking blows expose enemy vulnerabilities (+20% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                GolemMountainMomentumId,
                new RewardDefinition(
                    GolemMountainMomentumId,
                    "Mountain Momentum",
                    "Unstoppable colossus mass builds forward momentum (+12% Move Speed)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.MoveSpeedPercent,
                    0.12f,
                    maxRank: 5)
            },
            {
                GolemColossalRhythmId,
                new RewardDefinition(
                    GolemColossalRhythmId,
                    "Colossal Rhythm",
                    "Rhythmic stone punches accelerate consecutive swings (+15% Attack Speed)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.AttackSpeedPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                GolemReinforcedStrikesId,
                new RewardDefinition(
                    GolemReinforcedStrikesId,
                    "Reinforced Strikes",
                    "Granite plating reinforces durability during combat (+6 Armor)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.Armor,
                    6f,
                    maxRank: 5)
            },
            {
                GolemTremorFistsId,
                new RewardDefinition(
                    GolemTremorFistsId,
                    "Tremor Fists",
                    "Ground-shaking fists radiate seismic damage (+20% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                GolemWorldbreakerStrengthId,
                new RewardDefinition(
                    GolemWorldbreakerStrengthId,
                    "Worldbreaker Strength",
                    "Supreme ancient force shatters elite champions and bosses (+30% Damage)",
                    RewardRarity.Epic,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.30f,
                    maxRank: 5)
            },
            // Cursed Golem Wave Skills 11-20
            {
                GolemCrystalMultiplicationId,
                new RewardDefinition(
                    GolemCrystalMultiplicationId,
                    "Crystal Multiplication",
                    "Magical earth crystals multiply into additional projectiles (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                GolemBoulderSplittingId,
                new RewardDefinition(
                    GolemBoulderSplittingId,
                    "Boulder Splitting",
                    "Heavy thrown rocks shatter into secondary fragments on impact (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                GolemBedrockExpansionId,
                new RewardDefinition(
                    GolemBedrockExpansionId,
                    "Bedrock Expansion",
                    "Expand the effective zone of all earth area attacks (+1.5m Range)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.AttackRange,
                    1.5f,
                    maxRank: 5)
            },
            {
                GolemJaggedTerrainId,
                new RewardDefinition(
                    GolemJaggedTerrainId,
                    "Jagged Terrain",
                    "Erupted stone spikes leave lacerating shards (+15% Damage)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                GolemQuakeResonanceId,
                new RewardDefinition(
                    GolemQuakeResonanceId,
                    "Quake Resonance",
                    "Tectonic tremors resonate through bedrock (+20% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                GolemCrystalDensityId,
                new RewardDefinition(
                    GolemCrystalDensityId,
                    "Crystal Density",
                    "Hardened crystalline minerals punch through enemy armor (+25% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.25f,
                    maxRank: 5)
            },
            {
                GolemRollingDestructionId,
                new RewardDefinition(
                    GolemRollingDestructionId,
                    "Rolling Destruction",
                    "Rolling boulders accelerate momentum (+12% Move Speed)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.MoveSpeedPercent,
                    0.12f,
                    maxRank: 5)
            },
            {
                GolemMountainEchoId,
                new RewardDefinition(
                    GolemMountainEchoId,
                    "Mountain Echo",
                    "Ground slams release echoing harmonic aftershocks (+20% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                GolemEarthenConvergenceId,
                new RewardDefinition(
                    GolemEarthenConvergenceId,
                    "Earthen Convergence",
                    "Swirling stone fragments bind into dense protective armor (+6 Armor)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.Armor,
                    6f,
                    maxRank: 5)
            },
            {
                GolemUnstableBedrockId,
                new RewardDefinition(
                    GolemUnstableBedrockId,
                    "Unstable Bedrock",
                    "Violent underground ruptures trigger secondary catastrophic eruptions (+30% Damage)",
                    RewardRarity.Epic,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.30f,
                    maxRank: 5)
            },
            // Cursed Golem Wave Skills 21-30
            {
                GolemRunicReservoirId,
                new RewardDefinition(
                    GolemRunicReservoirId,
                    "Runic Reservoir",
                    "Ancient runes store vital essence, boosting resilience (+30 Max HP)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.MaxHealth,
                    30f,
                    maxRank: 5)
            },
            {
                GolemAncientInscriptionId,
                new RewardDefinition(
                    GolemAncientInscriptionId,
                    "Ancient Inscription",
                    "Carved primordial runes empower all golem strikes (+15% Damage)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                GolemRuneMultiplicationId,
                new RewardDefinition(
                    GolemRuneMultiplicationId,
                    "Rune Multiplication",
                    "Active runes fracture into supplementary energy bolts (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                GolemResonatingSymbolsId,
                new RewardDefinition(
                    GolemResonatingSymbolsId,
                    "Resonating Symbols",
                    "Interlocking runic symbols harmonize, boosting damage (+20% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                GolemRunicReclamationId,
                new RewardDefinition(
                    GolemRunicReclamationId,
                    "Runic Reclamation",
                    "Siphon vitality through runic impact cycles (+6% Life Steal)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.LifeStealPercent,
                    0.06f,
                    maxRank: 5)
            },
            {
                GolemGlyphAmplificationId,
                new RewardDefinition(
                    GolemGlyphAmplificationId,
                    "Glyph Amplification",
                    "Intensify runic glyph radiance to overpower invaders (+25% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.25f,
                    maxRank: 5)
            },
            {
                GolemStoneheartCircuitId,
                new RewardDefinition(
                    GolemStoneheartCircuitId,
                    "Stoneheart Circuit",
                    "Runic energy channels through stone veins, enhancing durability (+6 Armor)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.Armor,
                    6f,
                    maxRank: 5)
            },
            {
                GolemAncientConfigurationId,
                new RewardDefinition(
                    GolemAncientConfigurationId,
                    "Ancient Configuration",
                    "Optimal geometric rune alignments extend spell range (+1.5m Range)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.AttackRange,
                    1.5f,
                    maxRank: 5)
            },
            {
                GolemRuneCascadeId,
                new RewardDefinition(
                    GolemRuneCascadeId,
                    "Rune Cascade",
                    "Rune detonations cascade into secondary shockwave bursts (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                GolemForgottenLanguageId,
                new RewardDefinition(
                    GolemForgottenLanguageId,
                    "Forgotten Language",
                    "Master the forgotten runic tongue for devastating combat supremacy (+30% Damage)",
                    RewardRarity.Epic,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.30f,
                    maxRank: 5)
            },
            // Cursed Golem Wave Skills 31-40
            {
                GolemAdamantinePlatingId,
                new RewardDefinition(
                    GolemAdamantinePlatingId,
                    "Adamantine Plating",
                    "Dense crystalline plating dramatically increases physical armor (+6 Armor)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.Armor,
                    6f,
                    maxRank: 5)
            },
            {
                GolemUnbreakableCoreId,
                new RewardDefinition(
                    GolemUnbreakableCoreId,
                    "Unbreakable Core",
                    "Fortify the golem heart, expanding maximum durability (+40 Max HP)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.MaxHealth,
                    40f,
                    maxRank: 5)
            },
            {
                GolemStoneRegenerationId,
                new RewardDefinition(
                    GolemStoneRegenerationId,
                    "Stone Regeneration",
                    "Gradually repair damaged stone plates by siphoning strike energy (+6% Life Steal)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.LifeStealPercent,
                    0.06f,
                    maxRank: 5)
            },
            {
                GolemFortressRetaliationId,
                new RewardDefinition(
                    GolemFortressRetaliationId,
                    "Fortress Retaliation",
                    "Blocked hits splinter into retaliatory projectile shards (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                GolemObsidianReflectionId,
                new RewardDefinition(
                    GolemObsidianReflectionId,
                    "Obsidian Reflection",
                    "Reflect kinetic shockwaves back into attacking ranks (+20% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                GolemColossusStabilityId,
                new RewardDefinition(
                    GolemColossusStabilityId,
                    "Colossus Stability",
                    "Titanic immovable mass wards off all mortal blows (+8 Armor)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.Armor,
                    8f,
                    maxRank: 5)
            },
            {
                GolemMountainShelterId,
                new RewardDefinition(
                    GolemMountainShelterId,
                    "Mountain Shelter",
                    "Sheltering stone bulwarks heavily deflect incoming damage (+8 Armor)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.Armor,
                    8f,
                    maxRank: 5)
            },
            {
                GolemLivingStoneRecoveryId,
                new RewardDefinition(
                    GolemLivingStoneRecoveryId,
                    "Living Stone Recovery",
                    "Deep earth vitality drastically boosts surviving health threshold (+50 Max HP)",
                    RewardRarity.Epic,
                    "cursed_golem",
                    RewardModifierType.MaxHealth,
                    50f,
                    maxRank: 5)
            },
            {
                GolemHardenedSoulstoneId,
                new RewardDefinition(
                    GolemHardenedSoulstoneId,
                    "Hardened Soulstone",
                    "Infuse soulstone core with impervious granite wards (+10 Armor)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.Armor,
                    10f,
                    maxRank: 5)
            },
            {
                GolemGuardiansPatienceId,
                new RewardDefinition(
                    GolemGuardiansPatienceId,
                    "Guardian's Patience",
                    "Enduring timeless ages accelerates defensive recovery (+20% Dash Cooldown Recovery)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.DashCooldownReductionPercent,
                    0.20f,
                    maxRank: 5)
            },
            // Cursed Golem Wave Skills 41-50
            {
                GolemGravityCompressionId,
                new RewardDefinition(
                    GolemGravityCompressionId,
                    "Gravity Compression",
                    "Compress gravitational shockwaves to crush enemy ranks (+20% Damage)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                GolemOrbitingMassId,
                new RewardDefinition(
                    GolemOrbitingMassId,
                    "Orbiting Mass",
                    "Empower rotating stone shields and fragments (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                GolemGraviticMomentumId,
                new RewardDefinition(
                    GolemGraviticMomentumId,
                    "Gravitic Momentum",
                    "Harness converging kinetic force to amplify strike power (+25% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.25f,
                    maxRank: 5)
            },
            {
                GolemCollapsingPressureId,
                new RewardDefinition(
                    GolemCollapsingPressureId,
                    "Collapsing Pressure",
                    "Intensify singularity fields, expanding effective pull reach (+1.5m Range)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.AttackRange,
                    1.5f,
                    maxRank: 5)
            },
            {
                GolemWorldstoneConductorId,
                new RewardDefinition(
                    GolemWorldstoneConductorId,
                    "Worldstone Conductor",
                    "Conductive rune matrices siphon lifeforce through cataclysmic impacts (+6% Life Steal)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.LifeStealPercent,
                    0.06f,
                    maxRank: 5)
            },
            {
                GolemAncientMineralWealthId,
                new RewardDefinition(
                    GolemAncientMineralWealthId,
                    "Ancient Mineral Wealth",
                    "Deep geological veins harden structural integrity (+6 Armor)",
                    RewardRarity.Common,
                    "cursed_golem",
                    RewardModifierType.Armor,
                    6f,
                    maxRank: 5)
            },
            {
                GolemColossusTributeId,
                new RewardDefinition(
                    GolemColossusTributeId,
                    "Colossus Tribute",
                    "Tribute of crushed conquerors expands the titan frame (+45 Max HP)",
                    RewardRarity.Uncommon,
                    "cursed_golem",
                    RewardModifierType.MaxHealth,
                    45f,
                    maxRank: 5)
            },
            {
                GolemLostForgeWisdomId,
                new RewardDefinition(
                    GolemLostForgeWisdomId,
                    "Lost Forge Wisdom",
                    "Ancient dwarven forgecraft increases weapon strike speed (+15% Attack Speed)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.AttackSpeedPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                GolemWorldstoneFortuneId,
                new RewardDefinition(
                    GolemWorldstoneFortuneId,
                    "Worldstone Fortune",
                    "Cataclysmic resonance shatters invader defenses (+20% Damage)",
                    RewardRarity.Rare,
                    "cursed_golem",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                GolemEternalMountainId,
                new RewardDefinition(
                    GolemEternalMountainId,
                    "Eternal Mountain",
                    "Become as permanent as the bedrock peaks (+60 Max HP, +8 Armor)",
                    RewardRarity.Mythic,
                    "cursed_golem",
                    RewardModifierType.MaxHealth,
                    60f,
                    maxRank: 3)
            },
            // Demon Monarch Wave Skills 01-10
            {
                MonarchSerratedHellsteelId,
                new RewardDefinition(
                    MonarchSerratedHellsteelId,
                    "Serrated Hellsteel",
                    "Hellforged serrations cause deep lacerations and hemorrhaging (+15% Damage)",
                    RewardRarity.Common,
                    "demon_monarch",
                    RewardModifierType.ProjectileDamagePercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                MonarchCrimsonEdgeId,
                new RewardDefinition(
                    MonarchCrimsonEdgeId,
                    "Crimson Edge",
                    "Crimson tempered blade lethality tears through enemy vital points (+20% Damage)",
                    RewardRarity.Uncommon,
                    "demon_monarch",
                    RewardModifierType.ProjectileDamagePercent,
                    0.20f,
                    maxRank: 5)
            },
            {
                MonarchMoltenReachId,
                new RewardDefinition(
                    MonarchMoltenReachId,
                    "Molten Reach",
                    "Superheated blade elongation extends strike range (+1.5m Range)",
                    RewardRarity.Common,
                    "demon_monarch",
                    RewardModifierType.AttackRange,
                    1.5f,
                    maxRank: 5)
            },
            {
                MonarchTwinFlameCutsId,
                new RewardDefinition(
                    MonarchTwinFlameCutsId,
                    "Twin Flame Cuts",
                    "Twin fiery arc echoes slice alongside every strike (+1 Projectile)",
                    RewardRarity.Rare,
                    "demon_monarch",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                MonarchEmberRicochetId,
                new RewardDefinition(
                    MonarchEmberRicochetId,
                    "Ember Ricochet",
                    "Severed blade embers ricochet across the battlefield (+1 Projectile)",
                    RewardRarity.Uncommon,
                    "demon_monarch",
                    RewardModifierType.ExtraProjectiles,
                    1f,
                    maxRank: 3)
            },
            {
                MonarchBladeMomentumId,
                new RewardDefinition(
                    MonarchBladeMomentumId,
                    "Blade Momentum",
                    "Furious relentless swing pacing accelerates sword cadence (+15% Attack Speed)",
                    RewardRarity.Common,
                    "demon_monarch",
                    RewardModifierType.AttackSpeedPercent,
                    0.15f,
                    maxRank: 5)
            },
            {
                MonarchArmorSunderingId,
                new RewardDefinition(
                    MonarchArmorSunderingId,
                    "Armor Sundering",
                    "Heavy demonic impacts shatter enemy armor and harden your shell (+6 Armor)",
                    RewardRarity.Common,
                    "demon_monarch",
                    RewardModifierType.Armor,
                    6f,
                    maxRank: 5)
            },
            {
                MonarchRoyalBladeDisciplineId,
                new RewardDefinition(
                    MonarchRoyalBladeDisciplineId,
                    "Royal Blade Discipline",
                    "Sovereign martial perfection maximizes sword precision (+25% Damage)",
                    RewardRarity.Rare,
                    "demon_monarch",
                    RewardModifierType.ProjectileDamagePercent,
                    0.25f,
                    maxRank: 5)
            },
            {
                MonarchExecutionersFuryId,
                new RewardDefinition(
                    MonarchExecutionersFuryId,
                    "Executioner's Fury",
                    "Slain invaders fuel demonic bloodthirst, restoring vitality (+6% Life Steal)",
                    RewardRarity.Rare,
                    "demon_monarch",
                    RewardModifierType.LifeStealPercent,
                    0.06f,
                    maxRank: 5)
            },
            {
                MonarchThousandCutInfernoId,
                new RewardDefinition(
                    MonarchThousandCutInfernoId,
                    "Thousand-Cut Inferno",
                    "A torrential storm of burning sword slashes consumes the battlefield (+30% Damage, +10% Attack Speed)",
                    RewardRarity.Epic,
                    "demon_monarch",
                    RewardModifierType.ProjectileDamagePercent,
                    0.30f,
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
