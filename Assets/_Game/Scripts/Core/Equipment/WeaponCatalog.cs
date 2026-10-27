using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public static class WeaponCatalog
    {
        private static readonly List<ItemDefinition> Weapons = new List<ItemDefinition>();
        private static readonly Dictionary<string, ItemDefinition> ById = new Dictionary<string, ItemDefinition>(StringComparer.OrdinalIgnoreCase);

        static WeaponCatalog()
        {
            RegisterAll();
        }

        public static IReadOnlyList<ItemDefinition> All => Weapons;

        public static ItemDefinition Get(string id)
        {
            if (ById.TryGetValue(id, out var def))
                return def;
            return null;
        }

        public static List<ItemDefinition> GetByCategory(WeaponCategory category)
        {
            var list = new List<ItemDefinition>();
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i].WeaponCategory == category)
                    list.Add(Weapons[i]);
            }
            return list;
        }

        private static void Add(
            string id,
            string name,
            string description,
            WeaponCategory category,
            ItemRarity rarity,
            ItemStats stats,
            int buyPrice,
            int sellPrice)
        {
            var def = new ItemDefinition(id, name, description, ItemSlot.Weapon, rarity, stats, buyPrice, sellPrice, category);
            Weapons.Add(def);
            ById[id] = def;
        }

        private static void RegisterAll()
        {
            if (Weapons.Count > 0) return;

            // 1. One-Handed Swords (10)
            Add("wpn_1h_iron_broadsword", "Iron Broadsword", "Standard reliable soldier blade.", WeaponCategory.OneHandedSword, ItemRarity.Common, new ItemStats { FlatDamage = 5f, AttackSpeedPercent = 0.05f }, 20, 10);
            Add("wpn_1h_steel_arming_sword", "Steel Arming Sword", "Balanced double-edged arming blade.", WeaponCategory.OneHandedSword, ItemRarity.Common, new ItemStats { FlatDamage = 7f, AttackSpeedPercent = 0.05f }, 25, 12);
            Add("wpn_1h_soldier_gladius", "Soldier Gladius", "Short thrusting gladius with quick recovery.", WeaponCategory.OneHandedSword, ItemRarity.Common, new ItemStats { FlatDamage = 6f, AttackSpeedPercent = 0.12f }, 25, 12);
            Add("wpn_1h_flame_brand_blade", "Flamebrand Blade", "Serrated sword pulsing with latent heat.", WeaponCategory.OneHandedSword, ItemRarity.Uncommon, new ItemStats { FlatDamage = 9f, DamagePercent = 0.08f }, 40, 20);
            Add("wpn_1h_tempest_cutlass", "Tempest Cutlass", "Curved blade whistling like coastal gales.", WeaponCategory.OneHandedSword, ItemRarity.Uncommon, new ItemStats { FlatDamage = 8f, MoveSpeedPercent = 0.08f, AttackSpeedPercent = 0.10f }, 45, 22);
            Add("wpn_1h_frostbite_saber", "Frostbite Saber", "Crystalline steel chilling flesh on contact.", WeaponCategory.OneHandedSword, ItemRarity.Rare, new ItemStats { FlatDamage = 13f, AttackSpeedPercent = 0.15f }, 70, 35);
            Add("wpn_1h_runed_broadsword", "Runed Broadsword", "Carved with ancient battle glyphs.", WeaponCategory.OneHandedSword, ItemRarity.Rare, new ItemStats { FlatDamage = 14f, DamagePercent = 0.12f, CooldownReductionPercent = 0.08f }, 75, 37);
            Add("wpn_1h_sunforged_rapier", "Sunforged Rapier", "Blinding solar needle piercing enemy armor.", WeaponCategory.OneHandedSword, ItemRarity.Epic, new ItemStats { FlatDamage = 19f, AttackSpeedPercent = 0.25f, LifeStealPercent = 0.08f }, 120, 60);
            Add("wpn_1h_dread_executioner", "Dread Executioner", "Heavy notched blade cleaving bone clean.", WeaponCategory.OneHandedSword, ItemRarity.Epic, new ItemStats { FlatDamage = 22f, DamagePercent = 0.18f }, 130, 65);
            Add("wpn_1h_excalibur_of_shadows", "Excalibur of Shadows", "Legendary umbral sword crowned with void energy.", WeaponCategory.OneHandedSword, ItemRarity.Legendary, new ItemStats { FlatDamage = 32f, DamagePercent = 0.28f, AttackSpeedPercent = 0.20f, LifeStealPercent = 0.12f }, 250, 125);

            // 2. Two-Handed Swords (10)
            Add("wpn_2h_heavy_claymore", "Heavy Claymore", "Long massive highlander greatsword.", WeaponCategory.TwoHandedSword, ItemRarity.Common, new ItemStats { FlatDamage = 9f }, 25, 12);
            Add("wpn_2h_bastard_sword", "Bastard Sword", "Versatile long steel blade.", WeaponCategory.TwoHandedSword, ItemRarity.Common, new ItemStats { FlatDamage = 10f, AttackSpeedPercent = -0.05f }, 25, 12);
            Add("wpn_2h_zweihander", "Zweihander", "Immense flamberge greatsword sweeping wide arcs.", WeaponCategory.TwoHandedSword, ItemRarity.Common, new ItemStats { FlatDamage = 12f, AttackSpeedPercent = -0.08f }, 30, 15);
            Add("wpn_2h_obsidian_greatsword", "Obsidian Greatsword", "Razor-sharp volcanic glass cleaver.", WeaponCategory.TwoHandedSword, ItemRarity.Uncommon, new ItemStats { FlatDamage = 15f, DamagePercent = 0.10f }, 50, 25);
            Add("wpn_2h_crimson_cleaver", "Crimson Cleaver", "Heavy slab stained with centuries of conqueror blood.", WeaponCategory.TwoHandedSword, ItemRarity.Uncommon, new ItemStats { FlatDamage = 16f, LifeStealPercent = 0.06f }, 55, 27);
            Add("wpn_2h_cataclysm_edge", "Cataclysm Edge", "Jagged volcanic greatsword shaking the arena.", WeaponCategory.TwoHandedSword, ItemRarity.Rare, new ItemStats { FlatDamage = 22f, DamagePercent = 0.15f }, 85, 42);
            Add("wpn_2h_soul_reaper_blade", "Soul Reaper Blade", "Ethereal greatsword carving through spiritual bonds.", WeaponCategory.TwoHandedSword, ItemRarity.Rare, new ItemStats { FlatDamage = 20f, LifeStealPercent = 0.10f, CooldownReductionPercent = 0.08f }, 90, 45);
            Add("wpn_2h_abyssal_colossus", "Abyssal Colossus", "Colossal sword forged in crushing deep ocean trenches.", WeaponCategory.TwoHandedSword, ItemRarity.Epic, new ItemStats { FlatDamage = 28f, DamagePercent = 0.22f, FlatArmor = 6f }, 140, 70);
            Add("wpn_2h_titan_slab", "Titan Slab", "Monolithic blunt blade flattening invaders.", WeaponCategory.TwoHandedSword, ItemRarity.Epic, new ItemStats { FlatDamage = 32f, DamagePercent = 0.25f, AttackSpeedPercent = -0.05f }, 150, 75);
            Add("wpn_2h_world_breaker_blade", "World Breaker Blade", "Mythic god-slaying greatsword shattering bedrock.", WeaponCategory.TwoHandedSword, ItemRarity.Legendary, new ItemStats { FlatDamage = 45f, DamagePercent = 0.35f, LifeStealPercent = 0.15f }, 300, 150);

            // 3. Daggers (10)
            Add("wpn_dag_rusty_shiv", "Rusty Shiv", "Pitted crude iron knife.", WeaponCategory.Dagger, ItemRarity.Common, new ItemStats { FlatDamage = 3f, AttackSpeedPercent = 0.15f }, 15, 7);
            Add("wpn_dag_iron_stiletto", "Iron Stiletto", "Needle point slipping through armor seams.", WeaponCategory.Dagger, ItemRarity.Common, new ItemStats { FlatDamage = 4f, AttackSpeedPercent = 0.20f }, 20, 10);
            Add("wpn_dag_assassin_knife", "Assassin Knife", "Darkened rogue weapon weighted for critical stabs.", WeaponCategory.Dagger, ItemRarity.Common, new ItemStats { FlatDamage = 5f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.05f }, 25, 12);
            Add("wpn_dag_venomous_fang", "Venomous Fang", "Dripping caustic serpent venom.", WeaponCategory.Dagger, ItemRarity.Uncommon, new ItemStats { FlatDamage = 7f, AttackSpeedPercent = 0.25f }, 45, 22);
            Add("wpn_dag_shadow_quill", "Shadow Quill", "Featherweight blade sliding between shadows.", WeaponCategory.Dagger, ItemRarity.Uncommon, new ItemStats { FlatDamage = 6f, MoveSpeedPercent = 0.10f, AttackSpeedPercent = 0.25f }, 50, 25);
            Add("wpn_dag_blood_letter", "Blood Letter", "Grooved parrying dagger feeding upon wounds.", WeaponCategory.Dagger, ItemRarity.Rare, new ItemStats { FlatDamage = 10f, AttackSpeedPercent = 0.30f, LifeStealPercent = 0.08f }, 75, 37);
            Add("wpn_dag_phantom_parry_dagger", "Phantom Parry Dagger", "Phases through steel to puncture hearts.", WeaponCategory.Dagger, ItemRarity.Rare, new ItemStats { FlatDamage = 11f, AttackSpeedPercent = 0.32f, CooldownReductionPercent = 0.10f }, 80, 40);
            Add("wpn_dag_void_needle", "Void Needle", "Condensed null-matter needle stitching reality shut.", WeaponCategory.Dagger, ItemRarity.Epic, new ItemStats { FlatDamage = 16f, AttackSpeedPercent = 0.40f, MoveSpeedPercent = 0.12f }, 130, 65);
            Add("wpn_dag_eclipsed_talon", "Eclipsed Talon", "Claw forged during a total lunar eclipse.", WeaponCategory.Dagger, ItemRarity.Epic, new ItemStats { FlatDamage = 18f, DamagePercent = 0.15f, AttackSpeedPercent = 0.38f }, 140, 70);
            Add("wpn_dag_whisper_of_oblivion", "Whisper of Oblivion", "Silent executioner dagger striking before sound arrives.", WeaponCategory.Dagger, ItemRarity.Legendary, new ItemStats { FlatDamage = 26f, AttackSpeedPercent = 0.50f, DamagePercent = 0.25f, LifeStealPercent = 0.12f }, 260, 130);

            // 4. Spears (10)
            Add("wpn_spr_ash_wood_spear", "Ash Wood Spear", "Long pointed spear maintaining standoff distance.", WeaponCategory.Spear, ItemRarity.Common, new ItemStats { FlatDamage = 6f, AttackSpeedPercent = 0.05f }, 20, 10);
            Add("wpn_spr_bronze_pike", "Bronze Pike", "Sturdy phalanx pike with flared spearhead.", WeaponCategory.Spear, ItemRarity.Common, new ItemStats { FlatDamage = 7f, AttackSpeedPercent = 0.06f }, 25, 12);
            Add("wpn_spr_soldier_halberd", "Soldier Halberd", "Versatile axe-spear hybrid for chopping and thrusting.", WeaponCategory.Spear, ItemRarity.Common, new ItemStats { FlatDamage = 8f, AttackSpeedPercent = 0.04f }, 30, 15);
            Add("wpn_spr_storm_lance", "Storm Lance", "Crackling lightning conducts down the steel shaft.", WeaponCategory.Spear, ItemRarity.Uncommon, new ItemStats { FlatDamage = 11f, AttackSpeedPercent = 0.08f }, 50, 25);
            Add("wpn_spr_glaive_of_reaping", "Glaive of Reaping", "Curved blade slicing through dense ranks.", WeaponCategory.Spear, ItemRarity.Uncommon, new ItemStats { FlatDamage = 12f, DamagePercent = 0.08f }, 55, 27);
            Add("wpn_spr_wyrm_piercer", "Wyrm Piercer", "Hardened dragon-bone tip punching scales.", WeaponCategory.Spear, ItemRarity.Rare, new ItemStats { FlatDamage = 16f, DamagePercent = 0.12f }, 80, 40);
            Add("wpn_spr_celestial_trident", "Celestial Trident", "Three-pronged sacred fork radiating starlight.", WeaponCategory.Spear, ItemRarity.Rare, new ItemStats { FlatDamage = 15f, AttackSpeedPercent = 0.15f, LifeStealPercent = 0.06f }, 85, 42);
            Add("wpn_spr_sun_piercer_lance", "Sun Piercer Lance", "Channeling brilliant sunbeams into piercing strikes.", WeaponCategory.Spear, ItemRarity.Epic, new ItemStats { FlatDamage = 22f, DamagePercent = 0.18f, AttackSpeedPercent = 0.15f }, 135, 67);
            Add("wpn_spr_dread_naginata", "Dread Naginata", "Cursed polearm sweeping bloody swaths.", WeaponCategory.Spear, ItemRarity.Epic, new ItemStats { FlatDamage = 24f, DamagePercent = 0.20f, LifeStealPercent = 0.08f }, 145, 72);
            Add("wpn_spr_dragoon_god_lance", "Dragoon God Lance", "Celestial dragonslayer spear leaping through thunderheads.", WeaponCategory.Spear, ItemRarity.Legendary, new ItemStats { FlatDamage = 35f, DamagePercent = 0.30f, AttackSpeedPercent = 0.25f }, 280, 140);

            // 5. Axes (10)
            Add("wpn_axe_woodchopper_hatchet", "Woodchopper Hatchet", "Simple hand axe with keen edge.", WeaponCategory.Axe, ItemRarity.Common, new ItemStats { FlatDamage = 6f }, 20, 10);
            Add("wpn_axe_iron_battleaxe", "Iron Battleaxe", "Double-bitted martial axe.", WeaponCategory.Axe, ItemRarity.Common, new ItemStats { FlatDamage = 8f }, 25, 12);
            Add("wpn_axe_berserker_tomahawk", "Berserker Tomahawk", "Fast swinging throwing and chopping tomahawk.", WeaponCategory.Axe, ItemRarity.Common, new ItemStats { FlatDamage = 7f, AttackSpeedPercent = 0.10f }, 25, 12);
            Add("wpn_axe_molten_chopper", "Molten Chopper", "Furnace-heated edge cauterizing deep wounds.", WeaponCategory.Axe, ItemRarity.Uncommon, new ItemStats { FlatDamage = 12f, DamagePercent = 0.08f }, 50, 25);
            Add("wpn_axe_frost_bearded_axe", "Frost Bearded Axe", "Norwegian bearded axe coated in biting frost.", WeaponCategory.Axe, ItemRarity.Uncommon, new ItemStats { FlatDamage = 11f, AttackSpeedPercent = 0.08f }, 50, 25);
            Add("wpn_axe_skull_splitter", "Skull Splitter", "Weighted wedge crushing helms and craniums.", WeaponCategory.Axe, ItemRarity.Rare, new ItemStats { FlatDamage = 17f, DamagePercent = 0.14f }, 80, 40);
            Add("wpn_axe_viking_crescent", "Viking Crescent", "Sweeping crescent axe cleaving multiple foes.", WeaponCategory.Axe, ItemRarity.Rare, new ItemStats { FlatDamage = 18f, AttackSpeedPercent = 0.12f }, 85, 42);
            Add("wpn_axe_infernal_waraxe", "Infernal Waraxe", "Demon-forged battleaxe hungry for violence.", WeaponCategory.Axe, ItemRarity.Epic, new ItemStats { FlatDamage = 25f, DamagePercent = 0.20f, LifeStealPercent = 0.08f }, 140, 70);
            Add("wpn_axe_frenzy_labrys", "Frenzy Labrys", "Double-bladed labrys accelerating with each kill.", WeaponCategory.Axe, ItemRarity.Epic, new ItemStats { FlatDamage = 26f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.08f }, 150, 75);
            Add("wpn_axe_ragnarok_cleaver", "Ragnarok Cleaver", "World-ending battleaxe sundering the sky.", WeaponCategory.Axe, ItemRarity.Legendary, new ItemStats { FlatDamage = 38f, DamagePercent = 0.32f, LifeStealPercent = 0.12f }, 290, 145);

            // 6. Hammers (10)
            Add("wpn_ham_blacksmith_mallet", "Blacksmith Mallet", "Heavy iron mallet delivering blunt trauma.", WeaponCategory.Hammer, ItemRarity.Common, new ItemStats { FlatDamage = 7f }, 20, 10);
            Add("wpn_ham_stone_warhammer", "Stone Warhammer", "Chiseled granite warhammer ignoring armor plates.", WeaponCategory.Hammer, ItemRarity.Common, new ItemStats { FlatDamage = 9f, FlatArmor = 2f }, 25, 12);
            Add("wpn_ham_heavy_mace", "Heavy Mace", "Flanged steel mace fracturing enemy limbs.", WeaponCategory.Hammer, ItemRarity.Common, new ItemStats { FlatDamage = 8f, DamagePercent = 0.05f }, 25, 12);
            Add("wpn_ham_granite_smasher", "Granite Smasher", "Enchanted bedrock hammer shattering stone.", WeaponCategory.Hammer, ItemRarity.Uncommon, new ItemStats { FlatDamage = 13f, FlatArmor = 4f }, 50, 25);
            Add("wpn_ham_thunder_mallet", "Thunder Mallet", "Discharges concussive shockwaves upon impact.", WeaponCategory.Hammer, ItemRarity.Uncommon, new ItemStats { FlatDamage = 12f, DamagePercent = 0.10f }, 55, 27);
            Add("wpn_ham_earthshaker_maul", "Earthshaker Maul", "Two-handed maul sending fissures through arena floors.", WeaponCategory.Hammer, ItemRarity.Rare, new ItemStats { FlatDamage = 19f, DamagePercent = 0.15f }, 85, 42);
            Add("wpn_ham_concussive_flail", "Concussive Flail", "Spiked iron sphere bypassing enemy parries.", WeaponCategory.Hammer, ItemRarity.Rare, new ItemStats { FlatDamage = 18f, AttackSpeedPercent = 0.10f }, 80, 40);
            Add("wpn_ham_titan_anvil_hammer", "Titan Anvil Hammer", "Ancient forge hammer forged to reshape continents.", WeaponCategory.Hammer, ItemRarity.Epic, new ItemStats { FlatDamage = 27f, DamagePercent = 0.22f, FlatArmor = 8f }, 145, 72);
            Add("wpn_ham_radiant_morningstar", "Radiant Morningstar", "Holy spiked morningstar purging demonic corruption.", WeaponCategory.Hammer, ItemRarity.Epic, new ItemStats { FlatDamage = 26f, AttackSpeedPercent = 0.18f, LifeStealPercent = 0.08f }, 140, 70);
            Add("wpn_ham_mjolnir_of_the_deep", "Mjolnir of the Deep", "Abyssal stormhammer unleashing tectonic crackles.", WeaponCategory.Hammer, ItemRarity.Legendary, new ItemStats { FlatDamage = 40f, DamagePercent = 0.35f, FlatArmor = 10f }, 300, 150);

            // 7. Bows (10)
            Add("wpn_bow_short_hunting_bow", "Short Hunting Bow", "Nimble recurve bow firing rapid arrows.", WeaponCategory.Bow, ItemRarity.Common, new ItemStats { FlatDamage = 4f, AttackSpeedPercent = 0.12f }, 20, 10);
            Add("wpn_bow_cedar_longbow", "Cedar Longbow", "High-draw longbow launching piercing shafts.", WeaponCategory.Bow, ItemRarity.Common, new ItemStats { FlatDamage = 6f, AttackSpeedPercent = 0.08f }, 25, 12);
            Add("wpn_bow_recurve_composite", "Recurve Composite Bow", "Laminated horn and wood launching arrows with venomous speed.", WeaponCategory.Bow, ItemRarity.Common, new ItemStats { FlatDamage = 5f, AttackSpeedPercent = 0.15f }, 25, 12);
            Add("wpn_bow_gale_whisper_bow", "Gale Whisper Bow", "Arrows propelled by trapped wind currents.", WeaponCategory.Bow, ItemRarity.Uncommon, new ItemStats { FlatDamage = 8f, AttackSpeedPercent = 0.18f, MoveSpeedPercent = 0.06f }, 50, 25);
            Add("wpn_bow_flame_quiver_bow", "Flame Quiver Bow", "Ignites arrows into fiery streaks.", WeaponCategory.Bow, ItemRarity.Uncommon, new ItemStats { FlatDamage = 9f, DamagePercent = 0.08f }, 50, 25);
            Add("wpn_bow_viper_sting_bow", "Viper Sting Bow", "Envenomed broadheads weakening targets.", WeaponCategory.Bow, ItemRarity.Rare, new ItemStats { FlatDamage = 13f, AttackSpeedPercent = 0.22f }, 80, 40);
            Add("wpn_bow_frost_feather_bow", "Frost Feather Bow", "Chilling volleys dampening enemy advance.", WeaponCategory.Bow, ItemRarity.Rare, new ItemStats { FlatDamage = 12f, AttackSpeedPercent = 0.20f, CooldownReductionPercent = 0.08f }, 85, 42);
            Add("wpn_bow_starfall_greatbow", "Starfall Greatbow", "Massive bow raining luminous starlight arrows.", WeaponCategory.Bow, ItemRarity.Epic, new ItemStats { FlatDamage = 19f, DamagePercent = 0.20f, AttackSpeedPercent = 0.25f }, 140, 70);
            Add("wpn_bow_shadow_composite_bow", "Shadow Composite Bow", "Ethereal arrows phasing through obstacles.", WeaponCategory.Bow, ItemRarity.Epic, new ItemStats { FlatDamage = 20f, AttackSpeedPercent = 0.28f, MoveSpeedPercent = 0.10f }, 145, 72);
            Add("wpn_bow_artemis_silver_bow", "Artemis Silver Bow", "Divine moonbow never missing its destined target.", WeaponCategory.Bow, ItemRarity.Legendary, new ItemStats { FlatDamage = 30f, DamagePercent = 0.30f, AttackSpeedPercent = 0.35f, LifeStealPercent = 0.10f }, 280, 140);

            // 8. Crossbows (10)
            Add("wpn_xbw_light_hand_crossbow", "Light Hand Crossbow", "Compact one-handed quarrel launcher.", WeaponCategory.Crossbow, ItemRarity.Common, new ItemStats { FlatDamage = 5f, AttackSpeedPercent = 0.10f }, 20, 10);
            Add("wpn_xbw_iron_arbalest", "Iron Arbalest", "Steel prod delivering brutal punch.", WeaponCategory.Crossbow, ItemRarity.Common, new ItemStats { FlatDamage = 8f }, 25, 12);
            Add("wpn_xbw_repeater_crossbow", "Repeater Crossbow", "Magazine-fed rapid-fire quarrel projector.", WeaponCategory.Crossbow, ItemRarity.Common, new ItemStats { FlatDamage = 6f, AttackSpeedPercent = 0.20f }, 30, 15);
            Add("wpn_xbw_heavy_siege_arbalest", "Heavy Siege Arbalest", "Winch-cranked arbalest punching iron shields.", WeaponCategory.Crossbow, ItemRarity.Uncommon, new ItemStats { FlatDamage = 13f, DamagePercent = 0.10f }, 50, 25);
            Add("wpn_xbw_barbed_bolt_caster", "Barbed Bolt Caster", "Barbed bolts tearing through flesh.", WeaponCategory.Crossbow, ItemRarity.Uncommon, new ItemStats { FlatDamage = 11f, AttackSpeedPercent = 0.15f }, 55, 27);
            Add("wpn_xbw_witch_hunter_repeater", "Witch Hunter Repeater", "Blessed repeater firing silver tipped quarrels.", WeaponCategory.Crossbow, ItemRarity.Rare, new ItemStats { FlatDamage = 16f, AttackSpeedPercent = 0.25f }, 85, 42);
            Add("wpn_xbw_consecrated_cross_caster", "Consecrated Cross Caster", "Holy symbol firing radiant darts.", WeaponCategory.Crossbow, ItemRarity.Rare, new ItemStats { FlatDamage = 17f, DamagePercent = 0.14f }, 90, 45);
            Add("wpn_xbw_twin_demon_arbalest", "Twin Demon Arbalest", "Dual-barrel monstrosity launching twin heavy quarrels.", WeaponCategory.Crossbow, ItemRarity.Epic, new ItemStats { FlatDamage = 24f, AttackSpeedPercent = 0.25f, LifeStealPercent = 0.08f }, 145, 72);
            Add("wpn_xbw_dread_ballista", "Dread Ballista", "Miniature portable ballista turning invaders to paste.", WeaponCategory.Crossbow, ItemRarity.Epic, new ItemStats { FlatDamage = 28f, DamagePercent = 0.22f }, 150, 75);
            Add("wpn_xbw_apocalypse_repeater", "Apocalypse Repeater", "Gatling mechanism cycling soul-charged bolts.", WeaponCategory.Crossbow, ItemRarity.Legendary, new ItemStats { FlatDamage = 38f, DamagePercent = 0.32f, AttackSpeedPercent = 0.35f }, 300, 150);

            // 9. Staffs (10)
            Add("wpn_stf_carved_oak_staff", "Carved Oak Staff", "Gnarled oak focusing novice incantations.", WeaponCategory.Staff, ItemRarity.Common, new ItemStats { FlatDamage = 5f, CooldownReductionPercent = 0.05f }, 20, 10);
            Add("wpn_stf_apprentice_cane", "Apprentice Cane", "Lightweight wizard cane boosting mana circulation.", WeaponCategory.Staff, ItemRarity.Common, new ItemStats { FlatDamage = 6f, CooldownReductionPercent = 0.06f }, 25, 12);
            Add("wpn_stf_iron_capped_quarterstaff", "Iron Capped Quarterstaff", "Martial staff doubling as focus and bludgeon.", WeaponCategory.Staff, ItemRarity.Common, new ItemStats { FlatDamage = 7f, AttackSpeedPercent = 0.05f }, 25, 12);
            Add("wpn_stf_flame_heart_stave", "Flame Heart Stave", "Ember-topped staff scorching enemies with every blast.", WeaponCategory.Staff, ItemRarity.Uncommon, new ItemStats { FlatDamage = 10f, DamagePercent = 0.10f }, 50, 25);
            Add("wpn_stf_glacier_staff", "Glacier Staff", "Carved from blue glacial ice that never melts.", WeaponCategory.Staff, ItemRarity.Uncommon, new ItemStats { FlatDamage = 9f, CooldownReductionPercent = 0.10f }, 50, 25);
            Add("wpn_stf_thunder_stave", "Thunder Stave", "Conducts atmospheric static into crackling bolts.", WeaponCategory.Staff, ItemRarity.Rare, new ItemStats { FlatDamage = 14f, AttackSpeedPercent = 0.15f, DamagePercent = 0.10f }, 80, 40);
            Add("wpn_stf_druidic_totem_staff", "Druidic Totem Staff", "Sprouts verdant vines restoring lifeforce.", WeaponCategory.Staff, ItemRarity.Rare, new ItemStats { FlatDamage = 13f, LifeStealPercent = 0.08f, FlatMaxHealth = 30f }, 85, 42);
            Add("wpn_stf_astral_archmage_staff", "Astral Archmage Staff", "Crowned with orbiting stellar rings.", WeaponCategory.Staff, ItemRarity.Epic, new ItemStats { FlatDamage = 21f, DamagePercent = 0.22f, CooldownReductionPercent = 0.15f }, 140, 70);
            Add("wpn_stf_dread_necromancer_staff", "Dread Necromancer Staff", "Carved with shrieking skulls fueling dark arts.", WeaponCategory.Staff, ItemRarity.Epic, new ItemStats { FlatDamage = 23f, DamagePercent = 0.20f, LifeStealPercent = 0.10f }, 150, 75);
            Add("wpn_stf_staff_of_creation", "Staff of Creation", "Primordial relic containing the genesis spark of worlds.", WeaponCategory.Staff, ItemRarity.Legendary, new ItemStats { FlatDamage = 35f, DamagePercent = 0.35f, CooldownReductionPercent = 0.20f, LifeStealPercent = 0.12f }, 300, 150);

            // 10. Wands (10)
            Add("item_bone_wand", "Bone Wand", "A carved skeletal wand channels focused necrotic blasts.", WeaponCategory.Wand, ItemRarity.Common, new ItemStats { FlatDamage = 4f, AttackSpeedPercent = 0.10f }, 20, 10);
            Add("wpn_wnd_willow_wand", "Willow Wand", "Pliant wand flicking precise magic darts.", WeaponCategory.Wand, ItemRarity.Common, new ItemStats { FlatDamage = 5f, AttackSpeedPercent = 0.12f }, 20, 10);
            Add("wpn_wnd_flint_spark_wand", "Flint Spark Wand", "Emits quick fiery sparks on flicking.", WeaponCategory.Wand, ItemRarity.Common, new ItemStats { FlatDamage = 6f, AttackSpeedPercent = 0.10f }, 25, 12);
            Add("wpn_wnd_crystal_channel_wand", "Crystal Channel Wand", "Quartz tip amplifying arcane projectile velocity.", WeaponCategory.Wand, ItemRarity.Uncommon, new ItemStats { FlatDamage = 8f, AttackSpeedPercent = 0.16f }, 45, 22);
            Add("wpn_wnd_pyroclast_wand", "Pyroclast Wand", "Spits miniature cinder spheres.", WeaponCategory.Wand, ItemRarity.Uncommon, new ItemStats { FlatDamage = 9f, DamagePercent = 0.08f }, 50, 25);
            Add("wpn_wnd_rime_wand", "Rime Wand", "Fires freezing splinters slowing invaders.", WeaponCategory.Wand, ItemRarity.Rare, new ItemStats { FlatDamage = 12f, AttackSpeedPercent = 0.20f }, 75, 37);
            Add("wpn_wnd_tempest_wand", "Tempest Wand", "Crackles with chain lightning arcs.", WeaponCategory.Wand, ItemRarity.Rare, new ItemStats { FlatDamage = 13f, AttackSpeedPercent = 0.22f, DamagePercent = 0.10f }, 80, 40);
            Add("wpn_wnd_sorcerer_fury_wand", "Sorcerer Fury Wand", "Accelerates projectile cadence to machine-like flurries.", WeaponCategory.Wand, ItemRarity.Epic, new ItemStats { FlatDamage = 18f, AttackSpeedPercent = 0.35f }, 135, 67);
            Add("wpn_wnd_nether_crystal_wand", "Nether Crystal Wand", "Channels dark void bolts through amethyst prisms.", WeaponCategory.Wand, ItemRarity.Epic, new ItemStats { FlatDamage = 20f, DamagePercent = 0.20f, LifeStealPercent = 0.08f }, 145, 72);
            Add("wpn_wnd_wand_of_endless_void", "Wand of Endless Void", "Infinite nullity streams from its obsidian tip.", WeaponCategory.Wand, ItemRarity.Legendary, new ItemStats { FlatDamage = 28f, AttackSpeedPercent = 0.45f, DamagePercent = 0.25f }, 270, 135);

            // 11. Spellbooks (10)
            Add("wpn_spb_novice_tome", "Novice Tome", "Leatherbound primer containing rudimentary cantrips.", WeaponCategory.Spellbook, ItemRarity.Common, new ItemStats { FlatDamage = 4f, DamagePercent = 0.05f }, 20, 10);
            Add("wpn_spb_elemental_scroll", "Elemental Scroll", "Parchment inscribed with basic elemental equations.", WeaponCategory.Spellbook, ItemRarity.Common, new ItemStats { FlatDamage = 5f, DamagePercent = 0.06f }, 25, 12);
            Add("wpn_spb_cantrip_grimoire", "Cantrip Grimoire", "Well-thumbed collection of minor hexes.", WeaponCategory.Spellbook, ItemRarity.Common, new ItemStats { FlatDamage = 6f, CooldownReductionPercent = 0.05f }, 25, 12);
            Add("wpn_spb_tome_of_embers", "Tome of Embers", "Pages scorched with self-igniting fire runes.", WeaponCategory.Spellbook, ItemRarity.Uncommon, new ItemStats { FlatDamage = 9f, DamagePercent = 0.10f }, 50, 25);
            Add("wpn_spb_pages_of_permafrost", "Pages of Permafrost", "Vellum cold to the touch containing winter litanies.", WeaponCategory.Spellbook, ItemRarity.Uncommon, new ItemStats { FlatDamage = 8f, CooldownReductionPercent = 0.08f }, 50, 25);
            Add("wpn_spb_grimoire_of_souls", "Grimoire of Souls", "Bound in flayed parchment humming with trapped whispers.", WeaponCategory.Spellbook, ItemRarity.Rare, new ItemStats { FlatDamage = 13f, LifeStealPercent = 0.08f, DamagePercent = 0.10f }, 80, 40);
            Add("wpn_spb_codex_of_arcane_might", "Codex of Arcane Might", "Encyclopedic mastery of destructive transmutation.", WeaponCategory.Spellbook, ItemRarity.Rare, new ItemStats { FlatDamage = 15f, DamagePercent = 0.15f }, 85, 42);
            Add("wpn_spb_book_of_forbidden_flesh", "Book of Forbidden Flesh", "Mutates outgoing spells into parasitic blood leeches.", WeaponCategory.Spellbook, ItemRarity.Epic, new ItemStats { FlatDamage = 21f, LifeStealPercent = 0.12f, FlatMaxHealth = 40f }, 140, 70);
            Add("wpn_spb_chronicles_of_storm", "Chronicles of Storm", "Living parchment summoning tempest cataclysms.", WeaponCategory.Spellbook, ItemRarity.Epic, new ItemStats { FlatDamage = 22f, DamagePercent = 0.22f, AttackSpeedPercent = 0.15f }, 145, 72);
            Add("wpn_spb_necronomicon_original", "Necronomicon Original", "The blasphemous tome of eternal dread and oblivion.", WeaponCategory.Spellbook, ItemRarity.Legendary, new ItemStats { FlatDamage = 34f, DamagePercent = 0.35f, CooldownReductionPercent = 0.20f, LifeStealPercent = 0.15f }, 300, 150);

            // 12. Magical Orbs (10)
            Add("wpn_orb_glass_sphere", "Glass Sphere", "Simple scrying globe focusing mental emanations.", WeaponCategory.MagicalOrb, ItemRarity.Common, new ItemStats { FlatDamage = 4f, MoveSpeedPercent = 0.05f }, 20, 10);
            Add("wpn_orb_mana_crystal", "Mana Crystal", "Faceted gem orbiting the caster automatically.", WeaponCategory.MagicalOrb, ItemRarity.Common, new ItemStats { FlatDamage = 5f, CooldownReductionPercent = 0.06f }, 25, 12);
            Add("wpn_orb_glowing_quartz", "Glowing Quartz", "Radiates a comforting yet lethal magical pulse.", WeaponCategory.MagicalOrb, ItemRarity.Common, new ItemStats { FlatDamage = 6f, DamagePercent = 0.05f }, 25, 12);
            Add("wpn_orb_pyre_orb", "Pyre Orb", "Swirling miniature sun orbiting and burning invaders.", WeaponCategory.MagicalOrb, ItemRarity.Uncommon, new ItemStats { FlatDamage = 9f, DamagePercent = 0.10f }, 50, 25);
            Add("wpn_orb_cryo_orb", "Cryo Orb", "Frost sphere releasing radial cold pulses.", WeaponCategory.MagicalOrb, ItemRarity.Uncommon, new ItemStats { FlatDamage = 8f, AttackSpeedPercent = 0.10f }, 50, 25);
            Add("wpn_orb_spark_core", "Spark Core", "Ionized plasma sphere zapping nearby intruders.", WeaponCategory.MagicalOrb, ItemRarity.Rare, new ItemStats { FlatDamage = 13f, AttackSpeedPercent = 0.15f, MoveSpeedPercent = 0.08f }, 80, 40);
            Add("wpn_orb_shadow_lens", "Shadow Lens", "Dark sphere absorbing light and reflecting void beams.", WeaponCategory.MagicalOrb, ItemRarity.Rare, new ItemStats { FlatDamage = 14f, DamagePercent = 0.12f, LifeStealPercent = 0.06f }, 85, 42);
            Add("wpn_orb_celestial_eye", "Celestial Eye", "Floating ocular orb observing and smiting foes.", WeaponCategory.MagicalOrb, ItemRarity.Epic, new ItemStats { FlatDamage = 20f, DamagePercent = 0.20f, CooldownReductionPercent = 0.12f }, 140, 70);
            Add("wpn_orb_pulsing_void_core", "Pulsing Void Core", "Micro black hole pulling enemy vitality inward.", WeaponCategory.MagicalOrb, ItemRarity.Epic, new ItemStats { FlatDamage = 22f, DamagePercent = 0.22f, LifeStealPercent = 0.10f }, 150, 75);
            Add("wpn_orb_eye_of_the_cosmos", "Eye of the Cosmos", "All-seeing singularity disintegrating matter on sight.", WeaponCategory.MagicalOrb, ItemRarity.Legendary, new ItemStats { FlatDamage = 32f, DamagePercent = 0.32f, CooldownReductionPercent = 0.18f, AttackSpeedPercent = 0.20f }, 290, 145);

            // 13. Scythes (10)
            Add("wpn_scy_farmer_scythe", "Farmer Scythe", "Curved agrarian blade repurposed for butchering invaders.", WeaponCategory.Scythe, ItemRarity.Common, new ItemStats { FlatDamage = 7f, AttackSpeedPercent = 0.04f }, 20, 10);
            Add("wpn_scy_reaper_sickle", "Reaper Sickle", "Hooked sickle ideal for swift execution sweeps.", WeaponCategory.Scythe, ItemRarity.Common, new ItemStats { FlatDamage = 6f, AttackSpeedPercent = 0.12f }, 25, 12);
            Add("wpn_scy_bone_sickle", "Bone Sickle", "Fashioned from hardened pelvis and rib bone.", WeaponCategory.Scythe, ItemRarity.Common, new ItemStats { FlatDamage = 8f, LifeStealPercent = 0.04f }, 25, 12);
            Add("wpn_scy_blood_crescent", "Blood Crescent", "Deep red crescent blade drinking spilled vitals.", WeaponCategory.Scythe, ItemRarity.Uncommon, new ItemStats { FlatDamage = 11f, LifeStealPercent = 0.08f }, 50, 25);
            Add("wpn_scy_grave_harvester", "Grave Harvester", "Heavy cemetery scythe cleaving broad arcs.", WeaponCategory.Scythe, ItemRarity.Uncommon, new ItemStats { FlatDamage = 12f, DamagePercent = 0.08f }, 55, 27);
            Add("item_death_scythe", "Death Scythe", "A cruel scythe blade that harvests the life essence of struck foes.", WeaponCategory.Scythe, ItemRarity.Rare, new ItemStats { FlatDamage = 10f, LifeStealPercent = 0.12f }, 50, 25);
            Add("wpn_scy_soul_collector", "Soul Collector", "Traps the screams of fallen heroes in misty trails.", WeaponCategory.Scythe, ItemRarity.Rare, new ItemStats { FlatDamage = 16f, LifeStealPercent = 0.10f, AttackSpeedPercent = 0.10f }, 85, 42);
            Add("wpn_scy_abyssal_war_scythe", "Abyssal War Scythe", "Forged from midnight ore to harvest entire armies.", WeaponCategory.Scythe, ItemRarity.Epic, new ItemStats { FlatDamage = 23f, DamagePercent = 0.20f, LifeStealPercent = 0.14f }, 145, 72);
            Add("wpn_scy_grim_judgment", "Grim Judgment", "The embodiment of unavoidable mortality.", WeaponCategory.Scythe, ItemRarity.Epic, new ItemStats { FlatDamage = 25f, DamagePercent = 0.22f, AttackSpeedPercent = 0.15f }, 155, 77);
            Add("wpn_scy_scythe_of_the_final_hour", "Scythe of the Final Hour", "Cuts the thread of fate itself.", WeaponCategory.Scythe, ItemRarity.Legendary, new ItemStats { FlatDamage = 38f, DamagePercent = 0.32f, LifeStealPercent = 0.18f, AttackSpeedPercent = 0.20f }, 300, 150);

            // 14. Claws (10)
            Add("wpn_clw_iron_knuckles", "Iron Knuckles", "Reinforced fist guards for brutal close-quarters beatdowns.", WeaponCategory.Claws, ItemRarity.Common, new ItemStats { FlatDamage = 4f, AttackSpeedPercent = 0.15f }, 20, 10);
            Add("wpn_clw_spiked_fists", "Spiked Fists", "Steel spikes piercing deep upon every punch.", WeaponCategory.Claws, ItemRarity.Common, new ItemStats { FlatDamage = 5f, AttackSpeedPercent = 0.18f }, 25, 12);
            Add("wpn_clw_wolf_fang_claws", "Wolf Fang Claws", "Serrated fangs strapped to leather gauntlets.", WeaponCategory.Claws, ItemRarity.Common, new ItemStats { FlatDamage = 6f, MoveSpeedPercent = 0.06f, AttackSpeedPercent = 0.15f }, 25, 12);
            Add("wpn_clw_panther_talons", "Panther Talons", "Stealthy curved talons ripping jugulars.", WeaponCategory.Claws, ItemRarity.Uncommon, new ItemStats { FlatDamage = 8f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.08f }, 50, 25);
            Add("wpn_clw_poison_spurs", "Poison Spurs", "Hollow needles injecting paralytic toxin.", WeaponCategory.Claws, ItemRarity.Uncommon, new ItemStats { FlatDamage = 9f, AttackSpeedPercent = 0.20f }, 50, 25);
            Add("wpn_clw_blood_ripper_claws", "Blood Ripper Claws", "Tears arterial wounds restoring user vitality.", WeaponCategory.Claws, ItemRarity.Rare, new ItemStats { FlatDamage = 12f, AttackSpeedPercent = 0.28f, LifeStealPercent = 0.08f }, 80, 40);
            Add("wpn_clw_demonic_talons", "Demonic Talons", "Brimstone talons rending flesh and armor alike.", WeaponCategory.Claws, ItemRarity.Rare, new ItemStats { FlatDamage = 14f, DamagePercent = 0.12f, AttackSpeedPercent = 0.25f }, 85, 42);
            Add("wpn_clw_dragon_rending_claws", "Dragon Rending Claws", "Claws of ancient drakes slicing diamond shields.", WeaponCategory.Claws, ItemRarity.Epic, new ItemStats { FlatDamage = 20f, DamagePercent = 0.20f, AttackSpeedPercent = 0.32f }, 140, 70);
            Add("wpn_clw_shadow_rend_fists", "Shadow Rend Fists", "Disappears into shadow between frantic tearing strikes.", WeaponCategory.Claws, ItemRarity.Epic, new ItemStats { FlatDamage = 21f, AttackSpeedPercent = 0.38f, MoveSpeedPercent = 0.12f }, 150, 75);
            Add("wpn_clw_claws_of_the_beast_god", "Claws of the Beast God", "Primal divine wrath condensed into rending claws.", WeaponCategory.Claws, ItemRarity.Legendary, new ItemStats { FlatDamage = 30f, DamagePercent = 0.28f, AttackSpeedPercent = 0.45f, LifeStealPercent = 0.12f }, 280, 140);

            // 15. Cursed Relics (10)
            Add("wpn_rlc_broken_fetish", "Broken Fetish", "Crude idol pulsing with sinister intent.", WeaponCategory.CursedRelic, ItemRarity.Common, new ItemStats { FlatDamage = 4f, DamagePercent = 0.06f }, 20, 10);
            Add("wpn_rlc_tarnished_chalice", "Tarnished Chalice", "Communion goblet filled with necrotic residue.", WeaponCategory.CursedRelic, ItemRarity.Common, new ItemStats { FlatDamage = 5f, LifeStealPercent = 0.04f }, 25, 12);
            Add("wpn_rlc_cracked_effigy", "Cracked Effigy", "Wax figure weeping black tears of agony.", WeaponCategory.CursedRelic, ItemRarity.Common, new ItemStats { FlatDamage = 6f, CooldownReductionPercent = 0.05f }, 25, 12);
            Add("wpn_rlc_blood_soaked_idol", "Blood Soaked Idol", "Sustained by perpetual sacrifice.", WeaponCategory.CursedRelic, ItemRarity.Uncommon, new ItemStats { FlatDamage = 9f, LifeStealPercent = 0.08f, DamagePercent = 0.06f }, 50, 25);
            Add("wpn_rlc_crying_mask", "Crying Mask", "Porcelain visage crying tears of boiling blood.", WeaponCategory.CursedRelic, ItemRarity.Uncommon, new ItemStats { FlatDamage = 10f, DamagePercent = 0.10f }, 55, 27);
            Add("wpn_rlc_demonic_skull_urn", "Demonic Skull Urn", "Urn containing the ashes of a betrayed archfiend.", WeaponCategory.CursedRelic, ItemRarity.Rare, new ItemStats { FlatDamage = 15f, DamagePercent = 0.14f }, 85, 42);
            Add("wpn_rlc_tormented_crucifix", "Tormented Crucifix", "Inverted cross burning with unholy blasphemy.", WeaponCategory.CursedRelic, ItemRarity.Rare, new ItemStats { FlatDamage = 16f, CooldownReductionPercent = 0.10f, LifeStealPercent = 0.06f }, 90, 45);
            Add("wpn_rlc_heart_of_the_fallen_angel", "Heart of the Fallen Angel", "Petrified angel heart still beating with forbidden rage.", WeaponCategory.CursedRelic, ItemRarity.Epic, new ItemStats { FlatDamage = 22f, DamagePercent = 0.22f, LifeStealPercent = 0.10f }, 145, 72);
            Add("wpn_rlc_void_singularity_shrine", "Void Singularity Shrine", "Miniature altar bending light into dark event horizons.", WeaponCategory.CursedRelic, ItemRarity.Epic, new ItemStats { FlatDamage = 24f, DamagePercent = 0.24f, CooldownReductionPercent = 0.12f }, 155, 77);
            Add("wpn_rlc_relic_of_eternal_damnation", "Relic of Eternal Damnation", "The apex relic sealing the final doom of the dungeon.", WeaponCategory.CursedRelic, ItemRarity.Legendary, new ItemStats { FlatDamage = 36f, DamagePercent = 0.35f, LifeStealPercent = 0.15f, CooldownReductionPercent = 0.15f }, 300, 150);
        }
    }
}
