using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public enum InRunEventType
    {
        None = 0,
        HolyCrusade = 1,
        TreasureHero = 2,
        MagicalShrine = 3,
        RareMerchant = 4,
        DungeonMutation = 5,
        DoubleBoss = 6
    }

    public enum ShrineType
    {
        Blood,
        Frenzy,
        Greed,
        Protection,
        Void
    }

    public enum DungeonMutationType
    {
        None = 0,
        VolcanicSurge = 1,
        GlacialCold = 2,
        VoidDistortion = 3,
        OverchargedLeyline = 4
    }

    public sealed class ShrineDefinition
    {
        public ShrineType Type { get; }
        public string Name { get; }
        public string Description { get; }
        public string ColorHex { get; }
        public float Duration { get; }
        public float BuffValue { get; }

        public ShrineDefinition(
            ShrineType type,
            string name,
            string description,
            string colorHex,
            float duration,
            float buffValue)
        {
            Type = type;
            Name = name ?? type.ToString();
            Description = description ?? "";
            ColorHex = colorHex ?? "#FFFFFF";
            Duration = Math.Max(0f, duration);
            BuffValue = buffValue;
        }
    }

    public sealed class ShrineInstance
    {
        public ShrineDefinition Definition { get; }
        public float X { get; }
        public float Y { get; }
        public bool IsUsed { get; private set; }

        public event Action<ShrineInstance> Activated;

        public ShrineInstance(ShrineDefinition definition, float x, float y)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            X = x;
            Y = y;
            IsUsed = false;
        }

        public bool TryActivate(float playerCurrentHp, out float hpCost, out float barrierGranted, out int goldGranted)
        {
            hpCost = 0f;
            barrierGranted = 0f;
            goldGranted = 0;

            if (IsUsed) return false;
            IsUsed = true;

            switch (Definition.Type)
            {
                case ShrineType.Blood:
                    hpCost = playerCurrentHp * Definition.BuffValue; // e.g. 20% current HP
                    break;
                case ShrineType.Protection:
                    barrierGranted = Definition.BuffValue; // e.g. 75 barrier
                    break;
                case ShrineType.Greed:
                    goldGranted = (int)Definition.BuffValue; // e.g. 150 gold
                    break;
            }

            Activated?.Invoke(this);
            CueBus.Raise(CueIds.ShrineActivated, X, Y, (float)Definition.Type);
            return true;
        }
    }

    public sealed class DungeonMutationDefinition
    {
        public DungeonMutationType Type { get; }
        public string Name { get; }
        public string Description { get; }
        public float DamageMultiplierBonus { get; }
        public float MoveSpeedMultiplierBonus { get; }
        public float CooldownReductionBonus { get; }
        public float ResourceGainMultiplier { get; }

        public DungeonMutationDefinition(
            DungeonMutationType type,
            string name,
            string description,
            float damageBonus = 0f,
            float speedBonus = 0f,
            float cdrBonus = 0f,
            float resourceGainMultiplier = 1f)
        {
            Type = type;
            Name = name ?? type.ToString();
            Description = description ?? "";
            DamageMultiplierBonus = damageBonus;
            MoveSpeedMultiplierBonus = speedBonus;
            CooldownReductionBonus = cdrBonus;
            ResourceGainMultiplier = resourceGainMultiplier;
        }
    }

    public sealed class InRunEventDefinition
    {
        public InRunEventType Type { get; }
        public string Name { get; }
        public string Description { get; }
        public string CueId { get; }
        public int MinWave { get; }

        public InRunEventDefinition(
            InRunEventType type,
            string name,
            string description,
            string cueId,
            int minWave = 1)
        {
            Type = type;
            Name = name ?? type.ToString();
            Description = description ?? "";
            CueId = cueId ?? "";
            MinWave = minWave;
        }
    }

    public static class InRunEventCatalog
    {
        private static readonly Dictionary<InRunEventType, InRunEventDefinition> _events = new Dictionary<InRunEventType, InRunEventDefinition>();
        private static readonly Dictionary<ShrineType, ShrineDefinition> _shrines = new Dictionary<ShrineType, ShrineDefinition>();
        private static readonly Dictionary<DungeonMutationType, DungeonMutationDefinition> _mutations = new Dictionary<DungeonMutationType, DungeonMutationDefinition>();

        static InRunEventCatalog()
        {
            RegisterAll();
        }

        public static InRunEventDefinition GetEvent(InRunEventType type) => _events.TryGetValue(type, out var def) ? def : null;
        public static ShrineDefinition GetShrine(ShrineType type) => _shrines.TryGetValue(type, out var def) ? def : null;
        public static DungeonMutationDefinition GetMutation(DungeonMutationType type) => _mutations.TryGetValue(type, out var def) ? def : null;

        public static IReadOnlyCollection<InRunEventDefinition> GetAllEvents() => _events.Values;
        public static IReadOnlyCollection<ShrineDefinition> GetAllShrines() => _shrines.Values;
        public static IReadOnlyCollection<DungeonMutationDefinition> GetAllMutations() => _mutations.Values;

        private static void RegisterAll()
        {
            _events.Clear();
            _shrines.Clear();
            _mutations.Clear();

            // 1. Events
            _events[InRunEventType.HolyCrusade] = new InRunEventDefinition(
                InRunEventType.HolyCrusade,
                "Holy Crusade",
                "A holy army surges forth in rigid phalanx formation, heavily shielded and granting triple gold upon defeat.",
                CueIds.CrusadeEvent,
                minWave: 5
            );

            _events[InRunEventType.TreasureHero] = new InRunEventDefinition(
                InRunEventType.TreasureHero,
                "Treasure-Bearing Hero",
                "An elusive adventurer laden with legendary sacks attempts to sprint across the arena. Slay them before escape for massive riches!",
                CueIds.TreasureHeroSpawn,
                minWave: 3
            );

            _events[InRunEventType.MagicalShrine] = new InRunEventDefinition(
                InRunEventType.MagicalShrine,
                "Magical Shrine",
                "An ancient monument appears in the dungeon, offering supernatural boons at significant risk or reward.",
                CueIds.ShrineActivated,
                minWave: 2
            );

            _events[InRunEventType.RareMerchant] = new InRunEventDefinition(
                InRunEventType.RareMerchant,
                "Black Market Smuggler",
                "An enigmatic underworld smuggler arrives with deep discounts on high-tier Epic and Legendary armaments.",
                CueIds.ShieldBlock,
                minWave: 6
            );

            _events[InRunEventType.DungeonMutation] = new InRunEventDefinition(
                InRunEventType.DungeonMutation,
                "Dungeon Mutation",
                "Leyline instabilities alter atmospheric and elemental properties across the arena floor.",
                CueIds.DungeonMutationActive,
                minWave: 8
            );

            _events[InRunEventType.DoubleBoss] = new InRunEventDefinition(
                InRunEventType.DoubleBoss,
                "Double-Boss Encounter",
                "Two legendary invader champions join forces simultaneously, demanding mastery over positioning and burst control.",
                CueIds.DoubleBossSpawn,
                minWave: 15
            );

            // 2. Shrines (5 distinct shrines)
            _shrines[ShrineType.Blood] = new ShrineDefinition(
                ShrineType.Blood,
                "Shrine of Blood",
                "Sacrifices 20% of current HP in exchange for +35% damage for 45 seconds.",
                "#DC2626",
                duration: 45f,
                buffValue: 0.20f
            );

            _shrines[ShrineType.Frenzy] = new ShrineDefinition(
                ShrineType.Frenzy,
                "Shrine of Frenzy",
                "Bestows wild feral zeal: +40% movement speed and +30% cooldown reduction for 35 seconds.",
                "#F59E0B",
                duration: 35f,
                buffValue: 0.40f
            );

            _shrines[ShrineType.Greed] = new ShrineDefinition(
                ShrineType.Greed,
                "Shrine of Greed",
                "Instantly showers the floor with 150 bonus gold coins and triples score rewards.",
                "#EAB308",
                duration: 0f,
                buffValue: 150f
            );

            _shrines[ShrineType.Protection] = new ShrineDefinition(
                ShrineType.Protection,
                "Shrine of Protection",
                "Encases the Overlord in a massive runic shield absorbing up to 75 damage.",
                "#3B82F6",
                duration: 60f,
                buffValue: 75f
            );

            _shrines[ShrineType.Void] = new ShrineDefinition(
                ShrineType.Void,
                "Shrine of the Void",
                "Triggers an explosive singularity dealing 120 void damage to all surrounding invaders.",
                "#8B5CF6",
                duration: 0f,
                buffValue: 120f
            );

            // 3. Dungeon Mutations (4 distinct mutations)
            _mutations[DungeonMutationType.VolcanicSurge] = new DungeonMutationDefinition(
                DungeonMutationType.VolcanicSurge,
                "Volcanic Surge",
                "Superheated arena floor: +25% player damage, +10% enemy move speed.",
                damageBonus: 0.25f,
                speedBonus: 0.05f,
                cdrBonus: 0f,
                resourceGainMultiplier: 1.15f
            );

            _mutations[DungeonMutationType.GlacialCold] = new DungeonMutationDefinition(
                DungeonMutationType.GlacialCold,
                "Glacial Cold",
                "Sub-zero drafts: +20% cooldown reduction, -10% movement speed.",
                damageBonus: 0f,
                speedBonus: -0.10f,
                cdrBonus: 0.20f,
                resourceGainMultiplier: 1.0f
            );

            _mutations[DungeonMutationType.VoidDistortion] = new DungeonMutationDefinition(
                DungeonMutationType.VoidDistortion,
                "Void Distortion",
                "Spatial fractures: +30% critical and spell damage, +15% enemy aggression.",
                damageBonus: 0.30f,
                speedBonus: 0f,
                cdrBonus: 0.10f,
                resourceGainMultiplier: 1.25f
            );

            _mutations[DungeonMutationType.OverchargedLeyline] = new DungeonMutationDefinition(
                DungeonMutationType.OverchargedLeyline,
                "Overcharged Leyline",
                "Overflowing mana currents: +50% resource gain and +25% cooldown reduction.",
                damageBonus: 0.15f,
                speedBonus: 0.10f,
                cdrBonus: 0.25f,
                resourceGainMultiplier: 1.50f
            );
        }
    }

    /// <summary>
    /// Deterministic controller for evaluating in-run events based on seed and wave progression.
    /// </summary>
    public static class InRunEventDirector
    {
        public static InRunEventType EvaluateWaveEvent(long seed, int waveNumber)
        {
            if (waveNumber < 2) return InRunEventType.None;

            // Guaranteed milestone encounters
            if (waveNumber == 3) return InRunEventType.MagicalShrine;
            if (waveNumber == 7) return InRunEventType.HolyCrusade;
            if (waveNumber == 12) return InRunEventType.TreasureHero;
            if (waveNumber == 15) return InRunEventType.DungeonMutation;
            if (waveNumber == 18) return InRunEventType.RareMerchant;
            if (waveNumber == 20) return InRunEventType.DoubleBoss;

            // Deterministic sub-rng roll for other waves (18% chance)
            var rng = new SplitMix64Rng(seed ^ ((long)waveNumber * 0x7F4A7C159A3B280DL));
            if (rng.NextDouble() < 0.18)
            {
                int pick = rng.NextInt(1, 7);
                return (InRunEventType)pick;
            }

            return InRunEventType.None;
        }

        public static ShrineType PickShrineType(long seed, int waveNumber)
        {
            var rng = new SplitMix64Rng(seed ^ ((long)waveNumber * 0x3C6EF35F2522FDL));
            var types = (ShrineType[])Enum.GetValues(typeof(ShrineType));
            int idx = rng.NextInt(0, types.Length);
            return types[idx];
        }

        public static DungeonMutationType PickMutationType(long seed, int waveNumber)
        {
            var rng = new SplitMix64Rng(seed ^ ((long)waveNumber * unchecked((long)0x9E3779B97F4A7C15UL)));
            int roll = rng.NextInt(1, 5);
            return (DungeonMutationType)roll;
        }
    }
}
