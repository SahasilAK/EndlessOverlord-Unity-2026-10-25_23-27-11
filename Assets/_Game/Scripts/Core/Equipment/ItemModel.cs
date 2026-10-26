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

        public ItemDefinition(
            string id,
            string name,
            string description,
            ItemSlot slot,
            ItemRarity rarity,
            ItemStats stats,
            int baseBuyPrice,
            int baseSellPrice)
        {
            Id = id;
            Name = name;
            Description = description;
            Slot = slot;
            Rarity = rarity;
            Stats = stats;
            BaseBuyPrice = baseBuyPrice;
            BaseSellPrice = baseSellPrice;
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

        public ItemInstance(ItemDefinition definition, string instanceId = null, int? customBuyPrice = null, int? customSellPrice = null)
        {
            Definition = definition ?? throw new ArgumentNullException(nameof(definition));
            InstanceId = !string.IsNullOrEmpty(instanceId) ? instanceId : $"item_inst_{_nextId++}";
            BuyPrice = customBuyPrice ?? definition.BaseBuyPrice;
            SellPrice = customSellPrice ?? definition.BaseSellPrice;
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

        private static readonly Dictionary<string, ItemDefinition> Items = new Dictionary<string, ItemDefinition>
        {
            {
                BoneWandId,
                new ItemDefinition(
                    BoneWandId,
                    "Bone Wand",
                    "A carved skeletal wand channels focused necrotic blasts.",
                    ItemSlot.Weapon,
                    ItemRarity.Common,
                    new ItemStats { FlatDamage = 4f, AttackSpeedPercent = 0.10f },
                    baseBuyPrice: 20,
                    baseSellPrice: 10)
            },
            {
                DeathScytheId,
                new ItemDefinition(
                    DeathScytheId,
                    "Death Scythe",
                    "A cruel scythe blade that harvests the life essence of struck foes.",
                    ItemSlot.Weapon,
                    ItemRarity.Rare,
                    new ItemStats { FlatDamage = 10f, LifeStealPercent = 0.12f },
                    baseBuyPrice: 50,
                    baseSellPrice: 25)
            },
            {
                CryptRobesId,
                new ItemDefinition(
                    CryptRobesId,
                    "Crypt Robes",
                    "Ancient burial shrouds reinforced with bone splints.",
                    ItemSlot.Armor,
                    ItemRarity.Common,
                    new ItemStats { FlatArmor = 6f, FlatMaxHealth = 25f },
                    baseBuyPrice: 20,
                    baseSellPrice: 10)
            },
            {
                NecroticShroudId,
                new ItemDefinition(
                    NecroticShroudId,
                    "Necrotic Shroud",
                    "A heavy mantle steeped in dark sorcery, shielding the wearer from harm.",
                    ItemSlot.Armor,
                    ItemRarity.Rare,
                    new ItemStats { FlatArmor = 14f, FlatMaxHealth = 60f },
                    baseBuyPrice: 50,
                    baseSellPrice: 25)
            },
            {
                SkullRingId,
                new ItemDefinition(
                    SkullRingId,
                    "Skull Ring",
                    "A bone band enchanted to quicken feet and reflexes.",
                    ItemSlot.Accessory,
                    ItemRarity.Common,
                    new ItemStats { MoveSpeedPercent = 0.12f, CooldownReductionPercent = 0.10f },
                    baseBuyPrice: 20,
                    baseSellPrice: 10)
            },
            {
                AmuletOfTormentId,
                new ItemDefinition(
                    AmuletOfTormentId,
                    "Amulet of Torment",
                    "Gems charged with trapped agony elevate vitality and necrotic spell potency.",
                    ItemSlot.Accessory,
                    ItemRarity.Rare,
                    new ItemStats { FlatMaxHealth = 30f, DamagePercent = 0.15f },
                    baseBuyPrice: 50,
                    baseSellPrice: 25)
            }
        };

        public static ItemDefinition GetById(string id)
        {
            ItemDefinition def;
            Items.TryGetValue(id, out def);
            return def;
        }

        public static IEnumerable<ItemDefinition> All => Items.Values;
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
            if (EquippedWeapon != null) total.Add(EquippedWeapon.Definition.Stats);
            if (EquippedArmor != null) total.Add(EquippedArmor.Definition.Stats);
            if (EquippedAccessory != null) total.Add(EquippedAccessory.Definition.Stats);
            return total;
        }
    }
}
