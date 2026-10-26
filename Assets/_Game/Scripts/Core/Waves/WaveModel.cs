using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public enum InvaderArchetype
    {
        Swordsman = 1,
        Archer = 2,
        Cleric = 10,
        RoyalChampion = 28
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
                        archetype = InvaderArchetype.Cleric;
                        cost = 3;
                    }
                    else if (waveNumber >= 2 && roll < 0.60 && budget >= 2)
                    {
                        archetype = InvaderArchetype.Archer;
                        cost = 2;
                    }
                    else
                    {
                        archetype = InvaderArchetype.Swordsman;
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
    }
}
