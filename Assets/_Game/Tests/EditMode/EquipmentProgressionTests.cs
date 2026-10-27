using System;
using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class EquipmentProgressionTests
    {
        [Test]
        public void WeaponCatalog_ContainsAllFifteenCategories_TenWeaponsEach()
        {
            var categories = (WeaponCategory[])Enum.GetValues(typeof(WeaponCategory));
            int checkedCategories = 0;

            foreach (var cat in categories)
            {
                if (cat == WeaponCategory.None) continue;

                var weaponsInCat = WeaponCatalog.GetByCategory(cat);
                Assert.GreaterOrEqual(weaponsInCat.Count, 10, $"Category {cat} must contain at least 10 weapons.");
                checkedCategories++;

                foreach (var w in weaponsInCat)
                {
                    Assert.IsFalse(string.IsNullOrWhiteSpace(w.Id), "Weapon must have an Id.");
                    Assert.IsFalse(string.IsNullOrWhiteSpace(w.Name), "Weapon must have a Name.");
                    Assert.AreEqual(ItemSlot.Weapon, w.Slot, "Weapon must have Weapon slot.");
                    Assert.AreEqual(cat, w.WeaponCategory, "Weapon category must match.");
                    Assert.Greater(w.BaseBuyPrice, 0, $"{w.Name} must have a positive buy price.");
                    Assert.Greater(w.BaseSellPrice, 0, $"{w.Name} must have a positive sell price.");
                    Assert.Greater(w.Stats.FlatDamage, 0f, $"{w.Name} must provide flat damage.");
                }
            }

            Assert.AreEqual(15, checkedCategories, "There must be exactly 15 weapon categories tested.");
            Assert.GreaterOrEqual(WeaponCatalog.All.Count, 150, "WeaponCatalog must contain at least 150 weapons.");
        }

        [Test]
        public void ArmorCatalog_ContainsAtLeastThirtyArmorFamilies()
        {
            var armors = ArmorCatalog.All;
            Assert.GreaterOrEqual(armors.Count, 30, "ArmorCatalog must contain at least 30 armor families.");

            var seenIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var seenNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var arm in armors)
            {
                Assert.IsTrue(seenIds.Add(arm.Id), $"Duplicate armor ID: {arm.Id}");
                Assert.IsTrue(seenNames.Add(arm.Name), $"Duplicate armor Name: {arm.Name}");
                Assert.AreEqual(ItemSlot.Armor, arm.Slot);
                Assert.Greater(arm.Stats.FlatArmor, 0f, $"{arm.Name} must provide FlatArmor.");
                Assert.Greater(arm.Stats.FlatMaxHealth, 0f, $"{arm.Name} must provide FlatMaxHealth.");
                Assert.Greater(arm.BaseBuyPrice, 0);
                Assert.Greater(arm.BaseSellPrice, 0);
            }
        }

        [Test]
        public void ItemInstance_MasteryXpAndLevelProgression_ScalesEffectiveStats()
        {
            var def = WeaponCatalog.Get("wpn_1h_iron_broadsword");
            Assert.IsNotNull(def);

            var item = new ItemInstance(def);
            Assert.AreEqual(1, item.Level);
            Assert.AreEqual(0, item.CurrentXp);

            var baseStats = item.GetEffectiveStats();
            Assert.AreEqual(def.Stats.FlatDamage, baseStats.FlatDamage, 0.01f);

            // Add 100 XP -> Level 2
            bool leveled = item.AddMasteryXp(100);
            Assert.IsTrue(leveled);
            Assert.AreEqual(2, item.Level);
            Assert.AreEqual(0, item.CurrentXp);

            var lvl2Stats = item.GetEffectiveStats();
            // Level 2 should have +15% damage bonus
            Assert.AreEqual(def.Stats.FlatDamage * 1.15f, lvl2Stats.FlatDamage, 0.01f);

            // Add enough XP to reach max level (Level 5)
            item.AddMasteryXp(1000);
            Assert.AreEqual(item.MaxLevel, item.Level);
            Assert.AreEqual(0, item.XpToNextLevel);

            var maxStats = item.GetEffectiveStats();
            Assert.AreEqual(def.Stats.FlatDamage * 1.60f, maxStats.FlatDamage, 0.01f);
        }

        [Test]
        public void ItemInstance_Enchantments_ApplyDistinctCombatBonuses()
        {
            var def = WeaponCatalog.Get("wpn_2h_heavy_claymore");
            var item = new ItemInstance(def);

            // Flaming
            item.ApplyEnchantment(ItemEnchantment.Flaming);
            var flamingStats = item.GetEffectiveStats();
            Assert.Greater(flamingStats.FlatDamage, def.Stats.FlatDamage);

            // Vampiric
            item.ApplyEnchantment(ItemEnchantment.Vampiric);
            var vampStats = item.GetEffectiveStats();
            Assert.Greater(vampStats.LifeStealPercent, 0f);

            // Freezing
            item.ApplyEnchantment(ItemEnchantment.Freezing);
            var frostStats = item.GetEffectiveStats();
            Assert.Greater(frostStats.AttackSpeedPercent, 0f);

            // Thunderous
            item.ApplyEnchantment(ItemEnchantment.Thunderous);
            var thunderStats = item.GetEffectiveStats();
            Assert.Greater(thunderStats.DamagePercent, 0f);

            // VoidTouched
            item.ApplyEnchantment(ItemEnchantment.VoidTouched);
            var voidStats = item.GetEffectiveStats();
            Assert.Greater(voidStats.CooldownReductionPercent, 0f);
        }

        [Test]
        public void PlayerInventory_EquippedItemsWithLevelAndEnchantment_FeedIntoGetEquippedStats()
        {
            var inv = new PlayerInventory();
            var weapon = new ItemInstance(WeaponCatalog.Get("wpn_1h_sunforged_rapier"));
            var armor = new ItemInstance(ArmorCatalog.Get("arm_ancient_royal_aegis"));

            weapon.AddMasteryXp(100); // Level 2 (+15%)
            weapon.ApplyEnchantment(ItemEnchantment.Flaming);

            armor.AddMasteryXp(100); // Level 2 (+15%)
            armor.ApplyEnchantment(ItemEnchantment.Warded);

            inv.TryAddItem(weapon);
            inv.TryAddItem(armor);

            Assert.IsTrue(inv.TryEquip(weapon.InstanceId));
            Assert.IsTrue(inv.TryEquip(armor.InstanceId));

            var totalStats = inv.GetEquippedStats();

            // Check that weapon flat damage reflects level 2 and flaming enchantment
            Assert.Greater(totalStats.FlatDamage, weapon.Definition.Stats.FlatDamage);
            // Check that armor reflects level 2 and warded enchantment
            Assert.Greater(totalStats.FlatArmor, armor.Definition.Stats.FlatArmor);
            Assert.Greater(totalStats.FlatMaxHealth, armor.Definition.Stats.FlatMaxHealth);
        }
    }
}
