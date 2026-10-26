using System;
using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class EquipmentAndMerchantTests
    {
        [Test]
        public void ItemCatalog_AllRegisteredItemsValid()
        {
            var ids = new[]
            {
                ItemCatalog.BoneWandId,
                ItemCatalog.DeathScytheId,
                ItemCatalog.CryptRobesId,
                ItemCatalog.NecroticShroudId,
                ItemCatalog.SkullRingId,
                ItemCatalog.AmuletOfTormentId
            };

            foreach (var id in ids)
            {
                var def = ItemCatalog.GetById(id);
                Assert.IsNotNull(def, $"Item {id} must exist in catalog");
                Assert.IsNotEmpty(def.Name);
                Assert.IsNotEmpty(def.Description);
                Assert.IsTrue(def.BaseBuyPrice > 0, "Buy price must be positive");
                Assert.IsTrue(def.BaseSellPrice > 0, "Sell price must be positive");
                Assert.IsTrue(def.BaseBuyPrice >= def.BaseSellPrice, "Buy price should be >= sell price");
            }
        }

        [Test]
        public void Inventory_CapacityEnforced_RejectsWhenFull()
        {
            var inv = new PlayerInventory(capacity: 3);
            var wandDef = ItemCatalog.GetById(ItemCatalog.BoneWandId);

            Assert.IsTrue(inv.TryAddItem(new ItemInstance(wandDef)));
            Assert.IsTrue(inv.TryAddItem(new ItemInstance(wandDef)));
            Assert.IsTrue(inv.TryAddItem(new ItemInstance(wandDef)));
            Assert.AreEqual(3, inv.Bag.Count);
            Assert.IsFalse(inv.CanAddItem);

            // 4th item rejected
            Assert.IsFalse(inv.TryAddItem(new ItemInstance(wandDef)));
            Assert.AreEqual(3, inv.Bag.Count);
        }

        [Test]
        public void Inventory_EquipAndUnequip_ModifiesEquippedSlotsAndBag()
        {
            var inv = new PlayerInventory(capacity: 4);
            var wandDef = ItemCatalog.GetById(ItemCatalog.BoneWandId);
            var scytheDef = ItemCatalog.GetById(ItemCatalog.DeathScytheId);

            var wand = new ItemInstance(wandDef);
            var scythe = new ItemInstance(scytheDef);

            inv.TryAddItem(wand);
            inv.TryAddItem(scythe);
            Assert.AreEqual(2, inv.Bag.Count);

            // Equip wand into empty weapon slot
            Assert.IsTrue(inv.TryEquip(wand.InstanceId));
            Assert.AreEqual(wand, inv.EquippedWeapon);
            Assert.AreEqual(1, inv.Bag.Count);
            Assert.AreEqual(scythe.InstanceId, inv.Bag[0].InstanceId);

            // Equip scythe -> swaps wand back into bag
            Assert.IsTrue(inv.TryEquip(scythe.InstanceId));
            Assert.AreEqual(scythe, inv.EquippedWeapon);
            Assert.AreEqual(1, inv.Bag.Count);
            Assert.AreEqual(wand.InstanceId, inv.Bag[0].InstanceId);

            // Unequip scythe back to bag
            Assert.IsTrue(inv.TryUnequip(ItemSlot.Weapon));
            Assert.IsNull(inv.EquippedWeapon);
            Assert.AreEqual(2, inv.Bag.Count);
        }

        [Test]
        public void Inventory_UnequipFailsWhenBagIsFull()
        {
            var inv = new PlayerInventory(capacity: 1);
            var wandDef = ItemCatalog.GetById(ItemCatalog.BoneWandId);
            var scytheDef = ItemCatalog.GetById(ItemCatalog.DeathScytheId);

            var wand = new ItemInstance(wandDef);
            inv.TryAddItem(wand);
            inv.TryEquip(wand.InstanceId);
            Assert.AreEqual(0, inv.Bag.Count);
            Assert.IsNotNull(inv.EquippedWeapon);

            // Fill the bag
            inv.TryAddItem(new ItemInstance(scytheDef));
            Assert.AreEqual(1, inv.Bag.Count);
            Assert.IsFalse(inv.CanAddItem);

            // Unequip must fail because capacity is full
            Assert.IsFalse(inv.TryUnequip(ItemSlot.Weapon));
            Assert.IsNotNull(inv.EquippedWeapon);
        }

        [Test]
        public void Inventory_GetEquippedStats_AggregatesAllSlots()
        {
            var inv = new PlayerInventory();
            var wand = new ItemInstance(ItemCatalog.GetById(ItemCatalog.BoneWandId)); // +4 Dmg, +10% AtkSpd
            var robes = new ItemInstance(ItemCatalog.GetById(ItemCatalog.CryptRobesId)); // +6 Armor, +25 HP
            var ring = new ItemInstance(ItemCatalog.GetById(ItemCatalog.SkullRingId)); // +12% Spd, +10% CDR

            inv.TryAddItem(wand);
            inv.TryAddItem(robes);
            inv.TryAddItem(ring);

            inv.TryEquip(wand.InstanceId);
            inv.TryEquip(robes.InstanceId);
            inv.TryEquip(ring.InstanceId);

            var stats = inv.GetEquippedStats();
            Assert.AreEqual(4f, stats.FlatDamage);
            Assert.AreEqual(0.10f, stats.AttackSpeedPercent, 1e-4f);
            Assert.AreEqual(6f, stats.FlatArmor);
            Assert.AreEqual(25f, stats.FlatMaxHealth);
            Assert.AreEqual(0.12f, stats.MoveSpeedPercent, 1e-4f);
            Assert.AreEqual(0.10f, stats.CooldownReductionPercent, 1e-4f);
        }

        [Test]
        public void Inventory_SellEquippedItem_AtomicallyUnequipsAndCreditsGold()
        {
            var inv = new PlayerInventory();
            var scythe = new ItemInstance(ItemCatalog.GetById(ItemCatalog.DeathScytheId)); // Sell 25

            inv.TryAddItem(scythe);
            inv.TryEquip(scythe.InstanceId);
            Assert.AreEqual(scythe, inv.EquippedWeapon);
            Assert.AreEqual(10f, inv.GetEquippedStats().FlatDamage);

            // Sell equipped scythe
            int goldGained;
            Assert.IsTrue(inv.TrySell(scythe.InstanceId, out goldGained));
            Assert.AreEqual(scythe.SellPrice, goldGained);
            Assert.IsNull(inv.EquippedWeapon);
            Assert.AreEqual(0f, inv.GetEquippedStats().FlatDamage);

            // Cannot sell again
            Assert.IsFalse(inv.TrySell(scythe.InstanceId, out goldGained));
        }

        [Test]
        public void Merchant_GeneratesStockDeterministically_FromSeedAndWave()
        {
            var merchantA = new MerchantSystem();
            var merchantB = new MerchantSystem();

            merchantA.GenerateStock(1, 987654321L);
            merchantB.GenerateStock(1, 987654321L);

            Assert.AreEqual(merchantA.Stock.Count, merchantB.Stock.Count);
            for (int i = 0; i < merchantA.Stock.Count; i++)
            {
                Assert.AreEqual(merchantA.Stock[i].Definition.Id, merchantB.Stock[i].Definition.Id);
                Assert.AreEqual(merchantA.Stock[i].BuyPrice, merchantB.Stock[i].BuyPrice);
            }
        }

        [Test]
        public void Merchant_TryBuy_ExactGold_DeductsGoldRemovesStockAddsToBag()
        {
            var merchant = new MerchantSystem();
            var inv = new PlayerInventory();
            var hud = new HudModel(100f, "Souls", 100f, 1);

            merchant.GenerateStock(1, 42L);
            var target = merchant.Stock[0];
            hud.AddGold(target.BuyPrice);

            string error;
            Assert.IsTrue(merchant.TryBuy(target.InstanceId, inv, hud, out error));
            Assert.IsNull(error);
            Assert.AreEqual(0, hud.Gold);
            Assert.AreEqual(1, inv.Bag.Count);
            Assert.AreEqual(target.InstanceId, inv.Bag[0].InstanceId);

            // Item removed from merchant stock
            for (int i = 0; i < merchant.Stock.Count; i++)
            {
                Assert.AreNotEqual(target.InstanceId, merchant.Stock[i].InstanceId);
            }
        }

        [Test]
        public void Merchant_TryBuy_InsufficientFunds_RejectsAndLeavesStateUntouched()
        {
            var merchant = new MerchantSystem();
            var inv = new PlayerInventory();
            var hud = new HudModel(100f, "Souls", 100f, 1);

            merchant.GenerateStock(1, 42L);
            var target = merchant.Stock[0];
            hud.AddGold(target.BuyPrice - 1); // 1 gold short

            string error;
            Assert.IsFalse(merchant.TryBuy(target.InstanceId, inv, hud, out error));
            StringAssert.Contains("Insufficient gold", error);
            Assert.AreEqual(target.BuyPrice - 1, hud.Gold);
            Assert.AreEqual(0, inv.Bag.Count);
        }

        [Test]
        public void Merchant_TryBuy_InventoryFull_RejectsAndLeavesStateUntouched()
        {
            var merchant = new MerchantSystem();
            var inv = new PlayerInventory(capacity: 1);
            var hud = new HudModel(100f, "Souls", 100f, 1);

            // Fill inventory
            inv.TryAddItem(new ItemInstance(ItemCatalog.GetById(ItemCatalog.BoneWandId)));

            merchant.GenerateStock(1, 42L);
            var target = merchant.Stock[0];
            hud.AddGold(100);

            string error;
            Assert.IsFalse(merchant.TryBuy(target.InstanceId, inv, hud, out error));
            StringAssert.Contains("Inventory is full", error);
            Assert.AreEqual(100, hud.Gold);
        }

        [Test]
        public void Merchant_Reroll_DeductsGoldAndRegeneratesStock()
        {
            var merchant = new MerchantSystem();
            var hud = new HudModel(100f, "Souls", 100f, 1);

            merchant.GenerateStock(1, 100L);
            hud.AddGold(MerchantSystem.RerollCost);

            Assert.AreEqual(0, merchant.RerollCount);
            Assert.IsTrue(merchant.TryReroll(100L, hud));
            Assert.AreEqual(1, merchant.RerollCount);
            Assert.AreEqual(0, hud.Gold);

            // 2nd reroll fails due to lack of funds
            Assert.IsFalse(merchant.TryReroll(100L, hud));
            Assert.AreEqual(1, merchant.RerollCount);
        }

        [Test]
        public void RunStateMachine_TransitionsThroughMerchantState()
        {
            var sm = new RunStateMachine(RunState.PlayingWave);

            Assert.IsTrue(sm.TryTransition(RunState.ChoosingReward));
            Assert.IsTrue(sm.TryTransition(RunState.Intermission));

            // Intermission -> Merchant
            Assert.IsTrue(sm.TryTransition(RunState.Merchant));
            Assert.AreEqual(RunState.Merchant, sm.Current);

            // Pause & Resume from Merchant
            Assert.IsTrue(sm.TryPause());
            Assert.AreEqual(RunState.Paused, sm.Current);
            Assert.IsTrue(sm.TryResume());
            Assert.AreEqual(RunState.Merchant, sm.Current);

            // Merchant -> Intermission
            Assert.IsTrue(sm.TryTransition(RunState.Intermission));
            Assert.AreEqual(RunState.Intermission, sm.Current);

            // Death priority from Merchant
            Assert.IsTrue(sm.TryTransition(RunState.Merchant));
            Assert.IsTrue(sm.TryTransition(RunState.Death));
            Assert.AreEqual(RunState.Death, sm.Current);
        }
    }
}
