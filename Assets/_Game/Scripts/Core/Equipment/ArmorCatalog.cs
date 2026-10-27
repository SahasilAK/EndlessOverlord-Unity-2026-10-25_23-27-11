using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public static class ArmorCatalog
    {
        private static readonly List<ItemDefinition> Armors = new List<ItemDefinition>();
        private static readonly Dictionary<string, ItemDefinition> ById = new Dictionary<string, ItemDefinition>(StringComparer.OrdinalIgnoreCase);

        static ArmorCatalog()
        {
            RegisterAll();
        }

        public static IReadOnlyList<ItemDefinition> All => Armors;

        public static ItemDefinition Get(string id)
        {
            if (ById.TryGetValue(id, out var def))
                return def;
            return null;
        }

        private static void Add(
            string id,
            string name,
            string description,
            ItemRarity rarity,
            ItemStats stats,
            int buyPrice,
            int sellPrice)
        {
            var def = new ItemDefinition(id, name, description, ItemSlot.Armor, rarity, stats, buyPrice, sellPrice);
            Armors.Add(def);
            ById[id] = def;
        }

        private static void RegisterAll()
        {
            if (Armors.Count > 0) return;

            // 1. Crypt Robes (Common)
            Add("item_crypt_robes", "Crypt Robes", "Ancient burial shrouds reinforced with bone splints.", ItemRarity.Common, new ItemStats { FlatArmor = 6f, FlatMaxHealth = 25f }, 20, 10);

            // 2. Necrotic Shroud (Rare)
            Add("item_necrotic_shroud", "Necrotic Shroud", "A heavy mantle steeped in dark sorcery, shielding the wearer from harm.", ItemRarity.Rare, new ItemStats { FlatArmor = 14f, FlatMaxHealth = 60f }, 50, 25);

            // 3. Infernal Plate (Uncommon)
            Add("arm_infernal_plate", "Infernal Plate", "Forged in sulfurous magma pits, burning any invader bold enough to strike.", ItemRarity.Uncommon, new ItemStats { FlatArmor = 10f, FlatMaxHealth = 40f, FlatDamage = 3f }, 45, 22);

            // 4. Dragon Bone Mail (Rare)
            Add("arm_dragon_bone_mail", "Dragon Bone Mail", "Interlocking scales harvested from ancient elder drakes.", ItemRarity.Rare, new ItemStats { FlatArmor = 16f, FlatMaxHealth = 65f }, 80, 40);

            // 5. Shadow Cloak (Uncommon)
            Add("arm_shadow_cloak", "Shadow Cloak", "Woven from distilled twilight, muffling steps and evading detection.", ItemRarity.Uncommon, new ItemStats { FlatArmor = 7f, FlatMaxHealth = 30f, MoveSpeedPercent = 0.10f }, 50, 25);

            // 6. Ancient Royal Aegis (Epic)
            Add("arm_ancient_royal_aegis", "Ancient Royal Aegis", "Ceremonial plate of forgotten kings blessed with indestructible wards.", ItemRarity.Epic, new ItemStats { FlatArmor = 22f, FlatMaxHealth = 90f, CooldownReductionPercent = 0.08f }, 140, 70);

            // 7. Lich Garments (Epic)
            Add("arm_lich_garments", "Lich Garments", "Empowered vestments radiating perpetual cold and death magic.", ItemRarity.Epic, new ItemStats { FlatArmor = 12f, FlatMaxHealth = 70f, DamagePercent = 0.15f, CooldownReductionPercent = 0.10f }, 145, 72);

            // 8. Spectral Cuirass (Rare)
            Add("arm_spectral_cuirass", "Spectral Cuirass", "Semi-tangible armor that deflects kinetic blows into the ether.", ItemRarity.Rare, new ItemStats { FlatArmor = 13f, FlatMaxHealth = 50f, MoveSpeedPercent = 0.08f }, 75, 37);

            // 9. Bloodforged Carapace (Rare)
            Add("arm_bloodforged_carapace", "Bloodforged Carapace", "Living armor feeding on spilled blood to knit defensive plates.", ItemRarity.Rare, new ItemStats { FlatArmor = 15f, FlatMaxHealth = 55f, LifeStealPercent = 0.05f }, 85, 42);

            // 10. Void Mantle (Epic)
            Add("arm_void_mantle", "Void Mantle", "Torn from the void boundary, swallowing incoming projectile energy.", ItemRarity.Epic, new ItemStats { FlatArmor = 18f, FlatMaxHealth = 80f, CooldownReductionPercent = 0.12f }, 150, 75);

            // 11. Frostbound Hauberk (Uncommon)
            Add("arm_frostbound_hauberk", "Frostbound Hauberk", "Rings of frozen steel numbing enemy impacts.", ItemRarity.Uncommon, new ItemStats { FlatArmor = 11f, FlatMaxHealth = 35f }, 45, 22);

            // 12. Plague Doctor Vestments (Uncommon)
            Add("arm_plague_doctor_vestments", "Plague Doctor Vestments", "Waxed leathers steeped in aromatic anti-toxins.", ItemRarity.Uncommon, new ItemStats { FlatArmor = 8f, FlatMaxHealth = 45f, MoveSpeedPercent = 0.05f }, 50, 25);

            // 13. Titan Granite Bulwark (Epic)
            Add("arm_titan_granite_bulwark", "Titan Granite Bulwark", "Solid stone armor turning the wearer into a walking fortress.", ItemRarity.Epic, new ItemStats { FlatArmor = 26f, FlatMaxHealth = 110f, MoveSpeedPercent = -0.06f }, 160, 80);

            // 14. Sunforged Radiant Mail (Rare)
            Add("arm_sunforged_radiant_mail", "Sunforged Radiant Mail", "Gleams with dawn light that disorients attackers.", ItemRarity.Rare, new ItemStats { FlatArmor = 17f, FlatMaxHealth = 60f }, 85, 42);

            // 15. Abyssal Dreadplate (Epic)
            Add("arm_abyssal_dreadplate", "Abyssal Dreadplate", "Heavy armor forged in crushing deep trenches, terrifying foes.", ItemRarity.Epic, new ItemStats { FlatArmor = 24f, FlatMaxHealth = 95f, FlatDamage = 6f }, 150, 75);

            // 16. Tempest Windrunner Leathers (Common)
            Add("arm_tempest_windrunner_leathers", "Tempest Windrunner Leathers", "Light boiled leather built for extreme mobility.", ItemRarity.Common, new ItemStats { FlatArmor = 5f, FlatMaxHealth = 20f, MoveSpeedPercent = 0.08f }, 20, 10);

            // 17. Obsidian Ward Coat (Uncommon)
            Add("arm_obsidian_ward_coat", "Obsidian Ward Coat", "Volcanic obsidian plates sewn into thick hide.", ItemRarity.Uncommon, new ItemStats { FlatArmor = 10f, FlatMaxHealth = 40f }, 45, 22);

            // 18. Celestial Silk Tunic (Rare)
            Add("arm_celestial_silk_tunic", "Celestial Silk Tunic", "Woven from fallen stars, accelerating spellcasting.", ItemRarity.Rare, new ItemStats { FlatArmor = 11f, FlatMaxHealth = 50f, CooldownReductionPercent = 0.10f }, 80, 40);

            // 19. Chitinous Husk (Common)
            Add("arm_chitinous_husk", "Chitinous Husk", "Harvested from giant cave burrowers, light and durable.", ItemRarity.Common, new ItemStats { FlatArmor = 7f, FlatMaxHealth = 25f }, 20, 10);

            // 20. Soulweave Vesture (Uncommon)
            Add("arm_soulweave_vesture", "Soulweave Vesture", "Pliant robes woven with spirit threads humming softly.", ItemRarity.Uncommon, new ItemStats { FlatArmor = 8f, FlatMaxHealth = 35f, CooldownReductionPercent = 0.06f }, 45, 22);

            // 21. Iron Vanguard Harness (Common)
            Add("arm_iron_vanguard_harness", "Iron Vanguard Harness", "Standard heavy harness of the frontline shock troops.", ItemRarity.Common, new ItemStats { FlatArmor = 8f, FlatMaxHealth = 30f }, 25, 12);

            // 22. Rune Inscribed Plate (Rare)
            Add("arm_rune_inscribed_plate", "Rune Inscribed Plate", "Glows with defensive runic inscriptions absorbing shocks.", ItemRarity.Rare, new ItemStats { FlatArmor = 15f, FlatMaxHealth = 55f, CooldownReductionPercent = 0.08f }, 80, 40);

            // 23. Ashen Cinder Robes (Common)
            Add("arm_ashen_cinder_robes", "Ashen Cinder Robes", "Charred robes radiating faint embers.", ItemRarity.Common, new ItemStats { FlatArmor = 6f, FlatMaxHealth = 25f, FlatDamage = 2f }, 25, 12);

            // 24. Wyrmscale Brigandine (Rare)
            Add("arm_wyrmscale_brigandine", "Wyrmscale Brigandine", "Supple leather lined with fire-drake scales.", ItemRarity.Rare, new ItemStats { FlatArmor = 16f, FlatMaxHealth = 60f, FlatDamage = 4f }, 85, 42);

            // 25. Netherfang Harness (Uncommon)
            Add("arm_netherfang_harness", "Netherfang Harness", "Brimstone-cured harness leaching life from attacker strikes.", ItemRarity.Uncommon, new ItemStats { FlatArmor = 9f, FlatMaxHealth = 40f, LifeStealPercent = 0.04f }, 50, 25);

            // 26. Glacial Ward Coat (Rare)
            Add("arm_glacial_ward_coat", "Glacial Ward Coat", "Chilled pelts and ice runes deflecting searing blows.", ItemRarity.Rare, new ItemStats { FlatArmor = 14f, FlatMaxHealth = 55f, CooldownReductionPercent = 0.06f }, 80, 40);

            // 27. Blightward Jerkin (Common)
            Add("arm_blightward_jerkin", "Blightward Jerkin", "Treated jerkin resisting acid and dungeon toxins.", ItemRarity.Common, new ItemStats { FlatArmor = 6f, FlatMaxHealth = 22f }, 20, 10);

            // 28. Dreadlord Pauldrons (Epic)
            Add("arm_dreadlord_pauldrons", "Dreadlord Pauldrons & Plate", "Imposing horned armor commanding dark obedience.", ItemRarity.Epic, new ItemStats { FlatArmor = 23f, FlatMaxHealth = 100f, LifeStealPercent = 0.06f }, 155, 77);

            // 29. Voidstrider Shroud (Epic)
            Add("arm_voidstrider_shroud", "Voidstrider Shroud", "Phases the wearer between physical dimensions during sprints.", ItemRarity.Epic, new ItemStats { FlatArmor = 16f, FlatMaxHealth = 75f, MoveSpeedPercent = 0.12f, CooldownReductionPercent = 0.10f }, 150, 75);

            // 30. Sovereign Eternal Regalia (Legendary)
            Add("arm_sovereign_eternal_regalia", "Sovereign Eternal Regalia", "The imperial battle-regalia of the undying dungeon lord.", ItemRarity.Legendary, new ItemStats { FlatArmor = 32f, FlatMaxHealth = 150f, DamagePercent = 0.20f, CooldownReductionPercent = 0.15f, MoveSpeedPercent = 0.10f }, 320, 160);
        }
    }
}
