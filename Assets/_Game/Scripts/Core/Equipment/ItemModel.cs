using System;
using System.Collections.Generic;
using System.Text;

namespace FinalBoss.Core
{
    public enum ItemSlot
    {
        Weapon,
        Armor,
        Accessory
    }

    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public enum WeaponCategory
    {
        None = 0,
        OneHandedSword = 1,
        TwoHandedSword = 2,
        Dagger = 3,
        Spear = 4,
        Axe = 5,
        Hammer = 6,
        Bow = 7,
        Crossbow = 8,
        Staff = 9,
        Wand = 10,
        Spellbook = 11,
        MagicalOrb = 12,
        Scythe = 13,
        Claws = 14,
        CursedRelic = 15
    }

    public enum ItemEnchantment
    {
        None = 0,
        Flaming = 1,
        Freezing = 2,
        Vampiric = 3,
        Thunderous = 4,
        Warded = 5,
        VoidTouched = 6
    }

    [Serializable]
    public struct ItemStats
    {
        public float FlatDamage;
        public float DamagePercent;
        public float AttackSpeedPercent;
        public float FlatArmor;
        public float FlatMaxHealth;
        public float MoveSpeedPercent;
        public float LifeStealPercent;
        public float CooldownReductionPercent;

        public void Add(ItemStats other)
        {
            FlatDamage += other.FlatDamage;
            DamagePercent += other.DamagePercent;
            AttackSpeedPercent += other.AttackSpeedPercent;
            FlatArmor += other.FlatArmor;
            FlatMaxHealth += other.FlatMaxHealth;
            MoveSpeedPercent += other.MoveSpeedPercent;
            LifeStealPercent += other.LifeStealPercent;
            CooldownReductionPercent += other.CooldownReductionPercent;
        }

        public string GetSummary()
        {
            var sb = new StringBuilder();
            if (FlatDamage > 0f) sb.Append($"+{FlatDamage:0.#} Dmg ");
            if (DamagePercent > 0f) sb.Append($"+{DamagePercent * 100f:0.#}% Dmg ");
            if (AttackSpeedPercent > 0f) sb.Append($"+{AttackSpeedPercent * 100f:0.#}% AtkSpd ");
            if (FlatArmor > 0f) sb.Append($"+{FlatArmor:0.#} Armor ");
            if (FlatMaxHealth > 0f) sb.Append($"+{FlatMaxHealth:0.#} HP ");
            if (MoveSpeedPercent > 0f) sb.Append($"+{MoveSpeedPercent * 100f:0.#}% Spd ");
            if (LifeStealPercent > 0f) sb.Append($"+{LifeStealPercent * 100f:0.#}% Leech ");
            if (CooldownReductionPercent > 0f) sb.Append($"+{CooldownReductionPercent * 100f:0.#}% CDR ");

            string res = sb.ToString().Trim();
            return string.IsNullOrEmpty(res) ? "No stat modifiers" : res;
        }
    }

    /// <summary>Immutable base definition for an equipment item.</summary>
    public sealed class ItemDefinition
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public ItemSlot Slot { get; }
        public ItemRarity Rarity { get; }
        public ItemStats Stats { get; }
        public int BaseBuyPrice { get; }
        public int BaseSellPrice { get; }
        public WeaponCategory WeaponCategory { get; }

        public ItemDefinition(
            string id,
            string name,
            string description,
            ItemSlot slot,
            ItemRarity rarity,
            ItemStats stats,
            int baseBuyPrice,
            int baseSellPrice,
            WeaponCategory weaponCategory = WeaponCategory.None)
        {
            Id = id;
            Name = name;
            Description = description;
            Slot = slot;
            Rarity = rarity;
            Stats = stats;
            BaseBuyPrice = baseBuyPrice;
            BaseSellPrice = baseSellPrice;
            WeaponCategory = weaponCategory;
        }
    }

    /// <summary>Concrete instance of an item in bag or equipped slot, with a unique instance ID.</summary>
    public sealed class ItemInstance
    {
        private static long _nextId = 1;

        public string InstanceId { get; }
        public ItemDefinition Definition { get; }
        public int BuyPrice { get; }
        public int SellPrice { get; }

        public int Level { get; private set; } = 1;
        public int CurrentXp { get; private set; } = 0;
        public int MaxLevel => 5;
        public int XpToNextLevel => Level >= MaxLevel ? 0 : Level * 100;
        public ItemEnchantment Enchantment { get; private set; } = ItemEnchantment.None;

        public ItemInstance(ItemDefinition definition, string instanceId = null, int? customBuyPrice = null, int? customSellPrice = null)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            InstanceId = !string.IsNullOrEmpty(instanceId) ? instanceId : $"item_inst_{_nextId++}";
            BuyPrice = customBuyPrice ?? definition.BaseBuyPrice;
            SellPrice = customSellPrice ?? definition.BaseSellPrice;
        }

        public bool AddMasteryXp(int xp)
        {
            if (Level >= MaxLevel || xp <= 0) return false;
            CurrentXp += xp;
            bool leveledUp = false;
            while (Level < MaxLevel && CurrentXp >= XpToNextLevel)
            {
                CurrentXp -= XpToNextLevel;
                Level++;
                leveledUp = true;
            }
            return leveledUp;
        }

        public void ApplyEnchantment(ItemEnchantment enchantment)
        {
            Enchantment = enchantment;
        }

        public ItemStats GetEffectiveStats()
        {
            var baseStats = Definition.Stats;
            float mult = 1f + (Level - 1) * 0.15f; // +15% stats per level
            var result = new ItemStats
            {
                FlatDamage = baseStats.FlatDamage * mult,
                DamagePercent = baseStats.DamagePercent * mult,
                AttackSpeedPercent = baseStats.AttackSpeedPercent * mult,
                FlatArmor = baseStats.FlatArmor * mult,
                FlatMaxHealth = baseStats.FlatMaxHealth * mult,
                MoveSpeedPercent = baseStats.MoveSpeedPercent * mult,
                LifeStealPercent = baseStats.LifeStealPercent * mult,
                CooldownReductionPercent = baseStats.CooldownReductionPercent * mult
            };

            switch (Enchantment)
            {
                case ItemEnchantment.Flaming:
                    result.FlatDamage += 4f * Level;
                    break;
                case ItemEnchantment.Freezing:
                    result.AttackSpeedPercent += 0.08f;
                    break;
                case ItemEnchantment.Vampiric:
                    result.LifeStealPercent += 0.05f;
                    break;
                case ItemEnchantment.Thunderous:
                    result.DamagePercent += 0.10f;
                    break;
                case ItemEnchantment.Warded:
                    result.FlatArmor += 5f * Level;
                    break;
                case ItemEnchantment.VoidTouched:
                    result.CooldownReductionPercent += 0.08f;
                    break;
            }

            return result;
        }
    }

    /// <summary>Authoritative catalog of verified base equipment items.</summary>
    public static class ItemCatalog
    {
        public const string BoneWandId = "item_bone_wand";
        public const string DeathScytheId = "item_death_scythe";
        public const string CryptRobesId = "item_crypt_robes";
        public const string NecroticShroudId = "item_necrotic_shroud";
        public const string SkullRingId = "item_skull_ring";
        public const string AmuletOfTormentId = "item_amulet_of_torment";

        private static readonly Dictionary<string, ItemDefinition> Items = new Dictionary<string, ItemDefinition>(StringComparer.OrdinalIgnoreCase);

        static ItemCatalog()
        {
            Initialize();
        }

        private static void Initialize()
        {
            if (Items.Count > 0) return;

            // Baseline Accessories
            Register(new ItemDefinition(
                SkullRingId,
                "Skull Ring",
                "A bone band enchanted to quicken feet and reflexes.",
                ItemSlot.Accessory,
                ItemRarity.Common,
                new ItemStats { MoveSpeedPercent = 0.12f, CooldownReductionPercent = 0.10f },
                baseBuyPrice: 20,
                baseSellPrice: 10));

            Register(new ItemDefinition(
                AmuletOfTormentId,
                "Amulet of Torment",
                "Gems charged with trapped agony elevate vitality and necrotic spell potency.",
                ItemSlot.Accessory,
                ItemRarity.Rare,
                new ItemStats { FlatMaxHealth = 30f, DamagePercent = 0.15f },
                baseBuyPrice: 50,
                baseSellPrice: 25));

            Register(new ItemDefinition(
                "item_bloodstone_pendant",
                "Bloodstone Pendant",
                "Pulsing ruby pendant granting parasitic life drain.",
                ItemSlot.Accessory,
                ItemRarity.Uncommon,
                new ItemStats { LifeStealPercent = 0.08f, FlatMaxHealth = 20f },
                baseBuyPrice: 40,
                baseSellPrice: 20));

            Register(new ItemDefinition(
                "item_crown_of_damnation",
                "Crown of Damnation",
                "Black spiked circlet amplifying dark magic catastrophically.",
                ItemSlot.Accessory,
                ItemRarity.Legendary,
                new ItemStats { DamagePercent = 0.30f, CooldownReductionPercent = 0.20f, FlatMaxHealth = 50f },
                baseBuyPrice: 300,
                baseSellPrice: 150));

            // Weapons from WeaponCatalog (150 items across 15 categories)
            foreach (var wpn in WeaponCatalog.All)
            {
                Register(wpn);
            }

            // Armors from ArmorCatalog (30 armor families)
            foreach (var arm in ArmorCatalog.All)
            {
                Register(arm);
            }
        }

        public static void Register(ItemDefinition def)
        {
            if (def != null)
                Items[def.Id] = def;
        }

        public static ItemDefinition GetById(string id)
        {
            if (Items.Count == 0) Initialize();
            if (string.IsNullOrEmpty(id)) return null;
            Items.TryGetValue(id, out var def);
            return def;
        }

        public static IEnumerable<ItemDefinition> All
        {
            get
            {
                if (Items.Count == 0) Initialize();
                return Items.Values;
            }
        }
    }

    /// <summary>
    /// Player's runtime inventory and equipped slots.
    /// Manages bag capacity, equipping/unequipping, selling, and aggregate equipped stats.
    /// </summary>
    public sealed class PlayerInventory
    {
        public const int DefaultCapacity = 12;

        private readonly List<ItemInstance> _bag = new List<ItemInstance>();

        public int Capacity { get; }
        public IReadOnlyList<ItemInstance> Bag => _bag;

        public ItemInstance EquippedWeapon { get; private set; }
        public ItemInstance EquippedArmor { get; private set; }
        public ItemInstance EquippedAccessory { get; private set; }

        public event Action Changed;
        public event Action<ItemInstance, ItemSlot> Equipped;
        public event Action<ItemInstance, ItemSlot> Unequipped;

        public bool CanAddItem => _bag.Count < Capacity;

        public PlayerInventory(int capacity = DefaultCapacity)
        {
            Capacity = capacity > 0 ? capacity : DefaultCapacity;
        }

        public bool TryAddItem(ItemInstance item)
        {
            if (item == null || !CanAddItem)
                return false;

            _bag.Add(item);
            Changed?.Invoke();
            return true;
        }

        public bool TryRemoveItem(string instanceId, out ItemInstance removed)
        {
            removed = null;
            if (string.IsNullOrEmpty(instanceId)) return false;

            for (int i = 0; i < _bag.Count; i++)
            {
                if (_bag[i].InstanceId == instanceId)
                {
                    removed = _bag[i];
                    _bag.RemoveAt(i);
                    Changed?.Invoke();
                    return true;
                }
            }
            return false;
        }

        public bool TryEquip(string instanceId)
        {
            if (string.IsNullOrEmpty(instanceId)) return false;

            int bagIndex = -1;
            ItemInstance target = null;
            for (int i = 0; i < _bag.Count; i++)
            {
                if (_bag[i].InstanceId == instanceId)
                {
                    bagIndex = i;
                    target = _bag[i];
                    break;
                }
            }

            if (target == null)
                return false;

            ItemSlot slot = target.Definition.Slot;
            ItemInstance currentlyEquipped = GetEquippedInSlot(slot);

            if (currentlyEquipped != null)
            {
                // Swap currently equipped into the bag at the same slot
                _bag[bagIndex] = currentlyEquipped;
                SetEquippedInSlot(slot, target);
                Unequipped?.Invoke(currentlyEquipped, slot);
                Equipped?.Invoke(target, slot);
            }
            else
            {
                _bag.RemoveAt(bagIndex);
                SetEquippedInSlot(slot, target);
                Equipped?.Invoke(target, slot);
            }

            Changed?.Invoke();
            return true;
        }

        public bool TryUnequip(ItemSlot slot)
        {
            ItemInstance current = GetEquippedInSlot(slot);
            if (current == null)
                return false;

            if (!CanAddItem)
                return false; // Cannot unequip when bag is full

            SetEquippedInSlot(slot, null);
            _bag.Add(current);
            Unequipped?.Invoke(current, slot);
            Changed?.Invoke();
            return true;
        }

        /// <summary>
        /// Sells an item from either bag or equipped slot.
        /// If equipped, atomically unequips the item first before removing and returning gold gained.
        /// </summary>
        public bool TrySell(string instanceId, out int goldGained)
        {
            goldGained = 0;
            if (string.IsNullOrEmpty(instanceId)) return false;

            // 1. Check Bag
            for (int i = 0; i < _bag.Count; i++)
            {
                if (_bag[i].InstanceId == instanceId)
                {
                    goldGained = _bag[i].SellPrice;
                    _bag.RemoveAt(i);
                    Changed?.Invoke();
                    return true;
                }
            }

            // 2. Check Equipped Slots
            if (EquippedWeapon != null && EquippedWeapon.InstanceId == instanceId)
            {
                goldGained = EquippedWeapon.SellPrice;
                var old = EquippedWeapon;
                EquippedWeapon = null;
                Unequipped?.Invoke(old, ItemSlot.Weapon);
                Changed?.Invoke();
                return true;
            }

            if (EquippedArmor != null && EquippedArmor.InstanceId == instanceId)
            {
                goldGained = EquippedArmor.SellPrice;
                var old = EquippedArmor;
                EquippedArmor = null;
                Unequipped?.Invoke(old, ItemSlot.Armor);
                Changed?.Invoke();
                return true;
            }

            if (EquippedAccessory != null && EquippedAccessory.InstanceId == instanceId)
            {
                goldGained = EquippedAccessory.SellPrice;
                var old = EquippedAccessory;
                EquippedAccessory = null;
                Unequipped?.Invoke(old, ItemSlot.Accessory);
                Changed?.Invoke();
                return true;
            }

            return false;
        }

        public ItemInstance GetEquippedInSlot(ItemSlot slot)
        {
            switch (slot)
            {
                case ItemSlot.Weapon: return EquippedWeapon;
                case ItemSlot.Armor: return EquippedArmor;
                case ItemSlot.Accessory: return EquippedAccessory;
                default: return null;
            }
        }

        private void SetEquippedInSlot(ItemSlot slot, ItemInstance item)
        {
            switch (slot)
            {
                case ItemSlot.Weapon: EquippedWeapon = item; break;
                case ItemSlot.Armor: EquippedArmor = item; break;
                case ItemSlot.Accessory: EquippedAccessory = item; break;
            }
        }

        public ItemStats GetEquippedStats()
        {
            ItemStats total = default(ItemStats);
            if (EquippedWeapon != null) total.Add(EquippedWeapon.GetEffectiveStats());
            if (EquippedArmor != null) total.Add(EquippedArmor.GetEffectiveStats());
            if (EquippedAccessory != null) total.Add(EquippedAccessory.GetEffectiveStats());
            return total;
        }
    }
}
