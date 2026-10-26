using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public enum InvaderArchetype
    {
        Swordsman = 1,
        Archer = 2,
        Crossbowman = 3,
        Spearman = 4,
        Berserker = 5,
        Rogue = 6,
        Assassin = 7,
        Knight = 8,
        Paladin = 9,
        Cleric = 10,
        Priest = 11,
        FireMage = 12,
        IceMage = 13,
        LightningMage = 14,
        ArcaneWizard = 15,
        WitchHunter = 16,
        MonsterHunter = 17,
        Ranger = 18,
        Druid = 19,
        Alchemist = 20,
        Beastmaster = 21,
        Summoner = 22,
        ShieldGuardian = 23,
        BattleMonk = 24,
        HolyTemplar = 25,
        RuneKnight = 26,
        Spellblade = 27,
        RoyalChampion = 28,
        DragonHunter = 29,
        ShadowDuelist = 30
    }

    /// <summary>
    /// Definition for a single enemy spawn inside a wave.
    /// </summary>
    public struct InvaderSpawnDef
    {
        public InvaderArchetype Archetype;
        public bool IsElite;
        public float DelayBeforeSpawn;

        public InvaderSpawnDef(InvaderArchetype archetype, bool isElite, float delayBeforeSpawn)
        {
            Archetype = archetype;
            IsElite = isElite;
            DelayBeforeSpawn = delayBeforeSpawn;
        }
    }

    /// <summary>
    /// Specification of a discrete wave generated from seed and wave number.
    /// </summary>
    public sealed class WaveSpec
    {
        public int WaveNumber { get; }
        public IReadOnlyList<InvaderSpawnDef> Spawns { get; }
        public bool IsBossWave { get; }

        public WaveSpec(int waveNumber, IReadOnlyList<InvaderSpawnDef> spawns, bool isBossWave)
        {
            WaveNumber = waveNumber;
            Spawns = spawns ?? Array.Empty<InvaderSpawnDef>();
            IsBossWave = isBossWave;
        }
    }

    /// <summary>
    /// Authoritative wave ledger. Invariant: A wave is ONLY cleared when SpawnsRemaining == 0
    /// AND LivingEnemies == 0. WaveCleared event fires strictly once per wave.
    /// </summary>
    public sealed class WaveLedger
    {
        public event Action<int> WaveCleared;
        public event Action Changed;

        public int WaveNumber { get; private set; }
        public int TotalEnemies { get; private set; }
        public int SpawnsRemaining { get; private set; }
        public int LivingEnemies { get; private set; }
        public bool IsCompleted { get; private set; }

        public bool IsAllCleared => SpawnsRemaining == 0 && LivingEnemies == 0;

        public void StartWave(int waveNumber, int totalEnemies)
        {
            WaveNumber = waveNumber;
            TotalEnemies = Math.Max(0, totalEnemies);
            SpawnsRemaining = TotalEnemies;
            LivingEnemies = 0;
            IsCompleted = false;
            Changed?.Invoke();

            // Edge case: wave with 0 enemies immediately clears
            if (TotalEnemies == 0)
            {
                IsCompleted = true;
                WaveCleared?.Invoke(WaveNumber);
            }
        }

        public bool TryRecordSpawn()
        {
            if (SpawnsRemaining <= 0 || IsCompleted)
                return false;

            SpawnsRemaining--;
            LivingEnemies++;
            Changed?.Invoke();
            return true;
        }

        public bool TryRecordDeath()
        {
            if (LivingEnemies <= 0)
                return false;

            LivingEnemies--;
            Changed?.Invoke();

            if (IsAllCleared && !IsCompleted)
            {
                IsCompleted = true;
                WaveCleared?.Invoke(WaveNumber);
            }
            return true;
        }

        public void Reset()
        {
            SpawnsRemaining = 0;
            LivingEnemies = 0;
            TotalEnemies = 0;
            IsCompleted = false;
            Changed?.Invoke();
        }
    }

    /// <summary>
    /// Deterministic wave generator producing balanced, scaling wave budgets.
    /// Every 5th wave: heightened elite rate.
    /// Every 10th wave: guaranteed Champion / Boss tier (Royal Champion).
    /// </summary>
    public sealed class WaveGenerator
    {
        private readonly long _baseSeed;

        public WaveGenerator(long baseSeed)
        {
            _baseSeed = baseSeed;
        }

        public WaveSpec Generate(int waveNumber)
        {
            if (waveNumber < 1) waveNumber = 1;

            // Deterministic sub-rng stream per wave
            var rng = new SplitMix64Rng(_baseSeed ^ ((long)waveNumber * 0x517CC1B727220A95L));

            bool isBossWave = (waveNumber % 10 == 0);
            bool isEliteWave = (waveNumber % 5 == 0 && !isBossWave);

            var spawns = new List<InvaderSpawnDef>();

            if (isBossWave)
            {
                // Boss spawns first
                spawns.Add(new InvaderSpawnDef(InvaderArchetype.RoyalChampion, false, 0.2f));

                // Escort minions scaling with wave (wave 10: 4, wave 20: 8...)
                int minionCount = 3 + (waveNumber / 10) * 2;
                for (int i = 0; i < minionCount; i++)
                {
                    var archetype = rng.NextDouble() < 0.4 ? InvaderArchetype.Archer : InvaderArchetype.Swordsman;
                    bool isElite = rng.NextDouble() < 0.25;
                    spawns.Add(new InvaderSpawnDef(archetype, isElite, 0.8f + (float)rng.NextDouble() * 0.6f));
                }
            }
            else
            {
                // Normal or Elite wave
                // Budget scales: Wave 1 = 4 budget, Wave 2 = 6, Wave 5 = 14...
                int budget = 3 + waveNumber * 2;
                float eliteChance = isEliteWave ? 0.60f : Math.Min(0.35f, 0.05f + waveNumber * 0.02f);

                while (budget > 0)
                {
                    // Available archetypes unlock as waves advance
                    InvaderArchetype archetype;
                    int cost;

                    double roll = rng.NextDouble();
                    if (waveNumber >= 3 && roll < 0.25 && budget >= 3)
                    {
                        archetype = SelectSupportOrCasterArchetype(waveNumber, rng);
                        cost = 3;
                    }
                    else if (waveNumber >= 2 && roll < 0.60 && budget >= 2)
                    {
                        archetype = SelectRangedArchetype(waveNumber, rng);
                        cost = 2;
                    }
                    else
                    {
                        archetype = SelectMeleeArchetype(waveNumber, rng);
                        cost = 1;
                    }

                    budget -= cost;
                    bool isElite = rng.NextDouble() < eliteChance;
                    float stagger = 0.4f + (float)rng.NextDouble() * 0.8f;

                    spawns.Add(new InvaderSpawnDef(archetype, isElite, stagger));
                }
            }

            return new WaveSpec(waveNumber, spawns, isBossWave);
        }

        private static InvaderArchetype SelectSupportOrCasterArchetype(int waveNumber, SplitMix64Rng rng)
        {
            if (waveNumber < 7)
                return InvaderArchetype.Cleric;

            var pool = new List<InvaderArchetype> { InvaderArchetype.Cleric, InvaderArchetype.Priest, InvaderArchetype.FireMage, InvaderArchetype.IceMage };
            if (waveNumber >= 11)
            {
                pool.Add(InvaderArchetype.LightningMage);
                pool.Add(InvaderArchetype.ArcaneWizard);
                pool.Add(InvaderArchetype.Druid);
                pool.Add(InvaderArchetype.Alchemist);
            }
            if (waveNumber >= 16)
            {
                pool.Add(InvaderArchetype.Beastmaster);
                pool.Add(InvaderArchetype.Summoner);
            }
            int idx = (int)(rng.NextDouble() * pool.Count);
            return pool[Math.Clamp(idx, 0, pool.Count - 1)];
        }

        private static InvaderArchetype SelectRangedArchetype(int waveNumber, SplitMix64Rng rng)
        {
            if (waveNumber < 6)
                return InvaderArchetype.Archer;

            var pool = new List<InvaderArchetype> { InvaderArchetype.Archer, InvaderArchetype.Crossbowman };
            if (waveNumber >= 11)
            {
                pool.Add(InvaderArchetype.WitchHunter);
                pool.Add(InvaderArchetype.MonsterHunter);
                pool.Add(InvaderArchetype.Ranger);
            }
            if (waveNumber >= 20)
            {
                pool.Add(InvaderArchetype.DragonHunter);
            }
            int idx = (int)(rng.NextDouble() * pool.Count);
            return pool[Math.Clamp(idx, 0, pool.Count - 1)];
        }

        private static InvaderArchetype SelectMeleeArchetype(int waveNumber, SplitMix64Rng rng)
        {
            if (waveNumber < 4)
                return InvaderArchetype.Swordsman;

            var pool = new List<InvaderArchetype> { InvaderArchetype.Swordsman, InvaderArchetype.Spearman, InvaderArchetype.Berserker, InvaderArchetype.Rogue };
            if (waveNumber >= 8)
            {
                pool.Add(InvaderArchetype.Assassin);
                pool.Add(InvaderArchetype.Knight);
                pool.Add(InvaderArchetype.Paladin);
            }
            if (waveNumber >= 15)
            {
                pool.Add(InvaderArchetype.ShieldGuardian);
                pool.Add(InvaderArchetype.BattleMonk);
                pool.Add(InvaderArchetype.HolyTemplar);
            }
            if (waveNumber >= 20)
            {
                pool.Add(InvaderArchetype.RuneKnight);
                pool.Add(InvaderArchetype.Spellblade);
                pool.Add(InvaderArchetype.ShadowDuelist);
            }
            int idx = (int)(rng.NextDouble() * pool.Count);
            return pool[Math.Clamp(idx, 0, pool.Count - 1)];
        }
    }
}
