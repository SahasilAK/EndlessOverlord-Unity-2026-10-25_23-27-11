using System;
using System.Collections.Generic;
using NUnit.Framework;
using FinalBoss.Core;

namespace FinalBoss.Tests.EditMode
{
    [TestFixture]
    public sealed class BalanceAndRegressionTests
    {
        [Test]
        public void ExtremeWaveScaling_Wave1To100_NoNumericOverflowOrNaN()
        {
            long testSeed = 987654321L;
            var generator = new WaveGenerator(testSeed);

            int[] testWaves = { 1, 5, 10, 20, 25, 50, 75, 100 };

            foreach (int wave in testWaves)
            {
                var spec = generator.Generate(wave);
                Assert.IsNotNull(spec, $"Wave {wave} spec must generate.");
                Assert.AreEqual(wave, spec.WaveNumber);
                Assert.Greater(spec.Spawns.Count, 0, $"Wave {wave} must have spawns.");

                // Validate each spawn
                for (int i = 0; i < spec.Spawns.Count; i++)
                {
                    var spawn = spec.Spawns[i];
                    Assert.GreaterOrEqual(spawn.DelayBeforeSpawn, 0f);
                    Assert.IsFalse(float.IsNaN(spawn.DelayBeforeSpawn));
                    Assert.IsFalse(float.IsInfinity(spawn.DelayBeforeSpawn));

                    // Verify boss definition if boss
                    if (!string.IsNullOrEmpty(spawn.BossId))
                    {
                        var boss = LegendaryBossCatalog.Get(spawn.BossId);
                        Assert.IsNotNull(boss, $"Boss {spawn.BossId} in wave {wave} must exist in catalog.");
                        Assert.Greater(boss.BaseHealth, 0f);
                        Assert.IsFalse(float.IsNaN(boss.BaseHealth));
                        Assert.IsFalse(float.IsInfinity(boss.BaseHealth));
                    }
                }
            }
        }

        [Test]
        public void AllFiveRosterClasses_BalanceSanity_PositiveStatsAndUniqueResources()
        {
            string[] characterIds =
            {
                CharacterCatalog.LichLordId,
                CharacterCatalog.VampireQueenId,
                CharacterCatalog.CursedGolemId,
                CharacterCatalog.ShadowReaperId,
                CharacterCatalog.DemonMonarchId
            };

            var resourceNames = new HashSet<string>();

            foreach (var charId in characterIds)
            {
                var info = CharacterCatalog.Get(charId);
                Assert.IsNotNull(info, $"Character {charId} must exist in catalog.");
                Assert.Greater(info.BaseMaxHealth, 10f, $"Character {charId} must have base HP > 10.");
                Assert.Greater(info.BaseMoveSpeed, 2f, $"Character {charId} must have base speed > 2.");
                Assert.Greater(info.BaseMaxResource, 0f, $"Character {charId} must have base resource > 0.");
                Assert.GreaterOrEqual(info.StartingResource, 0f, $"Character {charId} starting resource must be non-negative.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(info.ResourceName), $"Character {charId} must have a resource name.");
                Assert.IsFalse(string.IsNullOrWhiteSpace(info.SignatureAbilityId), $"Character {charId} must have a signature ability.");

                resourceNames.Add(info.ResourceName);

                // Verify signature ability exists in registry
                var sigDef = AbilityRegistry.Get(info.SignatureAbilityId);
                Assert.IsNotNull(sigDef, $"Signature ability {info.SignatureAbilityId} must exist in AbilityRegistry.");
            }

            // Each class has its own resource identity
            Assert.AreEqual(5, resourceNames.Count, "All 5 classes must have unique resource identities.");
        }

        [Test]
        public void EconomyBalance_ShopPricingAndNoInfiniteGoldExploits()
        {
            var weapons = WeaponCatalog.All;
            var armors = ArmorCatalog.All;

            Assert.Greater(weapons.Count, 0);
            Assert.Greater(armors.Count, 0);

            // Buy price must strictly exceed sell price for every single equipment item
            foreach (var w in weapons)
            {
                Assert.Greater(w.BaseBuyPrice, 0, $"Weapon {w.Id} buy price must be positive.");
                Assert.Greater(w.BaseSellPrice, 0, $"Weapon {w.Id} sell price must be positive.");
                Assert.Greater(w.BaseBuyPrice, w.BaseSellPrice, $"Weapon {w.Id} buy price must exceed sell price to prevent infinite gold loop.");
            }

            foreach (var a in armors)
            {
                Assert.Greater(a.BaseBuyPrice, 0, $"Armor {a.Id} buy price must be positive.");
                Assert.Greater(a.BaseSellPrice, 0, $"Armor {a.Id} sell price must be positive.");
                Assert.Greater(a.BaseBuyPrice, a.BaseSellPrice, $"Armor {a.Id} buy price must exceed sell price to prevent infinite gold loop.");
            }
        }

        [Test]
        public void ObjectPool_ResetsStateCleanly_NoStateLeakOnReusedObjects()
        {
            int factoryCalls = 0;
            var pool = new ObjectPool<Health>(
                factory: () =>
                {
                    factoryCalls++;
                    return new Health(100f);
                },
                onGet: h => h.Reset(100f),
                onRelease: null
            );

            // Get first object, damage it, kill it
            var h1 = pool.Get();
            Assert.AreEqual(1, factoryCalls);
            h1.TakeDamage(100f);
            Assert.IsTrue(h1.IsDead);

            // Release back to pool
            pool.Release(h1);
            Assert.AreEqual(1, pool.CountInactive);

            // Get reused object
            var h2 = pool.Get();
            Assert.AreSame(h1, h2, "Pool should reuse the existing object.");
            Assert.AreEqual(1, factoryCalls, "Factory should not be called when inactive instance exists.");
            Assert.IsFalse(h2.IsDead, "Reused health must not carry dead state.");
            Assert.AreEqual(100f, h2.Current, "Reused health must be restored to full.");
        }

        [Test]
        public void AbilityLoadout_Max5Slots_RejectsSixthSlotAndLockedAbilities()
        {
            var loadout = new AbilityLoadout();
            Assert.AreEqual(5, AbilityLoadout.SlotCount);

            // Trying to equip slot index 5 (sixth slot) must fail
            bool sixthSlotEquipped = loadout.TryEquip(5, "some_ability", null, out string rejectReason);
            Assert.IsFalse(sixthSlotEquipped);
            StringAssert.Contains("bounds", rejectReason.ToLower());

            // Equipping locked ability must fail when validator rejects it
            bool lockedEquipped = loadout.TryEquip(0, "locked_skill", id => false, out string lockReason);
            Assert.IsFalse(lockedEquipped);
            StringAssert.Contains("locked", lockReason.ToLower());
        }

        [Test]
        public void WaveLedger_StrictInvariant_ClearedOnlyWhenSpawnsAndLivingZero()
        {
            var ledger = new WaveLedger();
            ledger.StartWave(1, 5);

            Assert.IsFalse(ledger.IsAllCleared);
            Assert.IsFalse(ledger.IsCompleted);

            // Record 5 spawns
            for (int i = 0; i < 5; i++)
            {
                Assert.IsTrue(ledger.TryRecordSpawn());
            }

            Assert.AreEqual(0, ledger.SpawnsRemaining);
            Assert.AreEqual(5, ledger.LivingEnemies);
            Assert.IsFalse(ledger.IsAllCleared, "Cannot be cleared while living enemies remain.");

            // Record 4 deaths
            for (int i = 0; i < 4; i++)
            {
                Assert.IsTrue(ledger.TryRecordDeath());
            }
            Assert.AreEqual(1, ledger.LivingEnemies);
            Assert.IsFalse(ledger.IsAllCleared);

            // Final death
            int clearEvents = 0;
            ledger.WaveCleared += wave => clearEvents++;

            Assert.IsTrue(ledger.TryRecordDeath());
            Assert.AreEqual(0, ledger.LivingEnemies);
            Assert.IsTrue(ledger.IsAllCleared);
            Assert.IsTrue(ledger.IsCompleted);
            Assert.AreEqual(1, clearEvents, "WaveCleared must fire strictly once.");

            // Extra deaths ignored
            Assert.IsFalse(ledger.TryRecordDeath());
            Assert.AreEqual(1, clearEvents);
        }
    }
}
