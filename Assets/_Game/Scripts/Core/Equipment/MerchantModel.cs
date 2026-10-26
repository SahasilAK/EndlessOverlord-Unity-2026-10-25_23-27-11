using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Pure C# Merchant shop simulation.
    /// Manages deterministic wave-based stock generation, transaction validation (gold + capacity),
    /// atomic purchasing, item selling with instant gold crediting, and rerolling.
    /// </summary>
    public sealed class MerchantSystem
    {
        public const int DefaultStockSize = 3;
        public const int RerollCost = 10;

        private readonly List<ItemInstance> _stock = new List<ItemInstance>();

        public IReadOnlyList<ItemInstance> Stock => _stock;
        public int CurrentWave { get; private set; }
        public int RerollCount { get; private set; }

        public event Action StockChanged;

        public void GenerateStock(int waveNumber, long seed, int count = DefaultStockSize)
        {
            CurrentWave = waveNumber;
            RerollCount = 0;
            PopulateStock(waveNumber, seed, 0, count);
        }

        private void PopulateStock(int waveNumber, long seed, int rerollIteration, int count)
        {
            _stock.Clear();

            long stockSeed = seed ^ ((long)(waveNumber + 100 * rerollIteration) * 0x3C6EF35FL) ^ 0xFEEDC0DE;
            var rng = new SplitMix64Rng(stockSeed);

            var pool = new List<ItemDefinition>(ItemCatalog.All);
            if (pool.Count == 0)
                return;

            for (int i = 0; i < count; i++)
            {
                if (pool.Count == 0)
                    pool.AddRange(ItemCatalog.All);

                int pickIndex = (int)(rng.NextDouble() * pool.Count);
                if (pickIndex >= pool.Count) pickIndex = pool.Count - 1;

                var def = pool[pickIndex];
                pool.RemoveAt(pickIndex); // Disallow duplicates in single stock presentation where possible

                _stock.Add(new ItemInstance(def));
            }

            StockChanged?.Invoke();
        }

        public bool TryReroll(long seed, HudModel hud)
        {
            if (hud == null || !hud.TrySpendGold(RerollCost))
                return false;

            RerollCount++;
            PopulateStock(CurrentWave, seed, RerollCount, DefaultStockSize);
            return true;
        }

        public bool TryBuy(string instanceId, PlayerInventory inventory, HudModel hud, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(instanceId))
            {
                error = "Invalid item.";
                return false;
            }

            ItemInstance target = null;
            for (int i = 0; i < _stock.Count; i++)
            {
                if (_stock[i].InstanceId == instanceId)
                {
                    target = _stock[i];
                    break;
                }
            }

            if (target == null)
            {
                error = "Item not found in merchant stock.";
                return false;
            }

            if (hud == null || hud.Gold < target.BuyPrice)
            {
                error = "Insufficient gold.";
                return false;
            }

            if (inventory == null || !inventory.CanAddItem)
            {
                error = "Inventory is full.";
                return false;
            }

            // Atomic commit
            hud.TrySpendGold(target.BuyPrice);
            _stock.Remove(target);
            inventory.TryAddItem(target);

            StockChanged?.Invoke();
            return true;
        }

        public bool TrySell(string instanceId, PlayerInventory inventory, HudModel hud, out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(instanceId) || inventory == null || hud == null)
            {
                error = "Invalid transaction.";
                return false;
            }

            int goldGained;
            if (!inventory.TrySell(instanceId, out goldGained))
            {
                error = "Item not found in player inventory or equipment.";
                return false;
            }

            hud.AddGold(goldGained);
            return true;
        }
    }
}
