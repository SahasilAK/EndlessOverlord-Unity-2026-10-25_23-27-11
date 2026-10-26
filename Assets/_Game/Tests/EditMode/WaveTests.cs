using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class WaveLedgerTests
    {
        [Test]
        public void WaveWithZeroEnemiesClearsImmediatelyAndFiresOnce()
        {
            var ledger = new WaveLedger();
            int clears = 0;
            ledger.WaveCleared += w => clears++;

            ledger.StartWave(1, 0);

            Assert.IsTrue(ledger.IsAllCleared);
            Assert.IsTrue(ledger.IsCompleted);
            Assert.AreEqual(1, clears);

            // Subsequent operations do not re-fire
            Assert.IsFalse(ledger.TryRecordDeath());
            Assert.AreEqual(1, clears);
        }

        [Test]
        public void SpawnsAndDeathsAreAccuratelyTracked()
        {
            var ledger = new WaveLedger();
            int clears = 0;
            ledger.WaveCleared += w => clears++;

            ledger.StartWave(1, 3);
            Assert.AreEqual(3, ledger.SpawnsRemaining);
            Assert.AreEqual(0, ledger.LivingEnemies);
            Assert.IsFalse(ledger.IsAllCleared);

            // Spawn 1
            Assert.IsTrue(ledger.TryRecordSpawn());
            Assert.AreEqual(2, ledger.SpawnsRemaining);
            Assert.AreEqual(1, ledger.LivingEnemies);

            // Spawn 2 & 3
            Assert.IsTrue(ledger.TryRecordSpawn());
            Assert.IsTrue(ledger.TryRecordSpawn());
            Assert.IsFalse(ledger.TryRecordSpawn()); // No more spawns
            Assert.AreEqual(0, ledger.SpawnsRemaining);
            Assert.AreEqual(3, ledger.LivingEnemies);
            Assert.IsFalse(ledger.IsAllCleared);

            // Kill 1
            Assert.IsTrue(ledger.TryRecordDeath());
            Assert.AreEqual(2, ledger.LivingEnemies);
            Assert.IsFalse(ledger.IsAllCleared);
            Assert.AreEqual(0, clears);

            // Kill 2
            Assert.IsTrue(ledger.TryRecordDeath());
            Assert.AreEqual(1, ledger.LivingEnemies);
            Assert.AreEqual(0, clears);

            // Kill 3 -> Wave cleared!
            Assert.IsTrue(ledger.TryRecordDeath());
            Assert.AreEqual(0, ledger.LivingEnemies);
            Assert.IsTrue(ledger.IsAllCleared);
            Assert.IsTrue(ledger.IsCompleted);
            Assert.AreEqual(1, clears);

            // Extra kill rejected and doesn't re-fire event
            Assert.IsFalse(ledger.TryRecordDeath());
            Assert.AreEqual(1, clears);
        }

        [Test]
        public void Invariant_NeverClearsIfSpawnsRemainEvenIfLivingIsZero()
        {
            var ledger = new WaveLedger();
            int clears = 0;
            ledger.WaveCleared += w => clears++;

            ledger.StartWave(2, 5);
            // Spawn 1, kill 1
            ledger.TryRecordSpawn();
            ledger.TryRecordDeath();

            // Living is 0, but 4 spawns remain -> MUST NOT CLEAR
            Assert.AreEqual(0, ledger.LivingEnemies);
            Assert.AreEqual(4, ledger.SpawnsRemaining);
            Assert.IsFalse(ledger.IsAllCleared);
            Assert.IsFalse(ledger.IsCompleted);
            Assert.AreEqual(0, clears);
        }

        [Test]
        public void ResetSafelyClearsAllCounters()
        {
            var ledger = new WaveLedger();
            ledger.StartWave(1, 5);
            ledger.TryRecordSpawn();
            ledger.Reset();

            Assert.AreEqual(0, ledger.SpawnsRemaining);
            Assert.AreEqual(0, ledger.LivingEnemies);
            Assert.AreEqual(0, ledger.TotalEnemies);
            Assert.IsFalse(ledger.IsCompleted);
        }
    }

    public class WaveGeneratorTests
    {
        [Test]
        public void SameSeedProducesIdenticalWaveSpecs()
        {
            var genA = new WaveGenerator(12345L);
            var genB = new WaveGenerator(12345L);

            for (int w = 1; w <= 15; w++)
            {
                var specA = genA.Generate(w);
                var specB = genB.Generate(w);

                Assert.AreEqual(specA.WaveNumber, specB.WaveNumber);
                Assert.AreEqual(specA.IsBossWave, specB.IsBossWave);
                Assert.AreEqual(specA.Spawns.Count, specB.Spawns.Count);

                for (int i = 0; i < specA.Spawns.Count; i++)
                {
                    Assert.AreEqual(specA.Spawns[i].Archetype, specB.Spawns[i].Archetype);
                    Assert.AreEqual(specA.Spawns[i].IsElite, specB.Spawns[i].IsElite);
                    Assert.AreEqual(specA.Spawns[i].DelayBeforeSpawn, specB.Spawns[i].DelayBeforeSpawn, 1e-4f);
                }
            }
        }

        [Test]
        public void Wave10AndMultiplesGuaranteeBoss()
        {
            var gen = new WaveGenerator(99999L);

            var w9 = gen.Generate(9);
            Assert.IsFalse(w9.IsBossWave);

            var w10 = gen.Generate(10);
            Assert.IsTrue(w10.IsBossWave);
            Assert.AreEqual(InvaderArchetype.RoyalChampion, w10.Spawns[0].Archetype);

            var w20 = gen.Generate(20);
            Assert.IsTrue(w20.IsBossWave);
            Assert.AreEqual(InvaderArchetype.RoyalChampion, w20.Spawns[0].Archetype);
        }

        [Test]
        public void Wave5HasHeightenedEliteRate()
        {
            var gen = new WaveGenerator(777L);
            var w1 = gen.Generate(1);
            var w5 = gen.Generate(5);

            int eliteCountW5 = 0;
            for (int i = 0; i < w5.Spawns.Count; i++)
                if (w5.Spawns[i].IsElite) eliteCountW5++;

            // Wave 5 should have high elite density
            Assert.Greater(eliteCountW5, 0, "Wave 5 should feature elite enemies");
            Assert.Greater(w5.Spawns.Count, w1.Spawns.Count, "Wave 5 budget should scale higher than Wave 1");
        }

        [Test]
        public void HigherWavesUnlockDifferentiatedArchetypes()
        {
            var gen = new WaveGenerator(42L);

            // Sample across waves 1 to 6 to verify Swordsman, Archer, Cleric all appear
            var seen = new HashSet<InvaderArchetype>();
            for (int w = 1; w <= 6; w++)
            {
                var spec = gen.Generate(w);
                for (int i = 0; i < spec.Spawns.Count; i++)
                    seen.Add(spec.Spawns[i].Archetype);
            }

            Assert.IsTrue(seen.Contains(InvaderArchetype.Swordsman), "Swordsman must appear");
            Assert.IsTrue(seen.Contains(InvaderArchetype.Archer), "Archer must appear");
            Assert.IsTrue(seen.Contains(InvaderArchetype.Cleric), "Cleric must appear");
        }
    }
}
