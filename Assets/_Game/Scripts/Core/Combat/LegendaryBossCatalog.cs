using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    [Flags]
    public enum BossModifier
    {
        None = 0,
        Enrage = 1 << 0,
        Regeneration = 1 << 1,
        Barrier = 1 << 2,
        Reinforcements = 1 << 3,
        ElementalOvercharge = 1 << 4
    }

    public enum BossAbilityType
    {
        ColossalCleave = 1,
        ShockwaveSlam = 2,
        MultishotBarrage = 3,
        PhantomBlinkStrike = 4,
        MoltenEruption = 5,
        GlacialPrison = 6,
        ThunderStrike = 7,
        SanctuaryWard = 8,
        WhirlwindCharge = 9,
        SummonVanguard = 10
    }

    public sealed class LegendaryBossDefinition
    {
        public string Id { get; }
        public string Name { get; }
        public string Title { get; }
        public InvaderArchetype Archetype { get; }
        public float BaseHealth { get; }
        public float BaseArmor { get; }
        public float BaseMoveSpeed { get; }
        public float Radius { get; }
        public float AttackRange { get; }
        public float AttackDamage { get; }
        public float AttackWindup { get; }
        public float AttackCooldown { get; }
        public BossAbilityType PrimaryAbility { get; }
        public BossAbilityType SecondaryAbility { get; }
        public float ColorR { get; }
        public float ColorG { get; }
        public float ColorB { get; }
        public string Description { get; }

        public LegendaryBossDefinition(
            string id,
            string name,
            string title,
            InvaderArchetype archetype,
            float baseHealth,
            float baseArmor,
            float baseMoveSpeed,
            float radius,
            float attackRange,
            float attackDamage,
            float attackWindup,
            float attackCooldown,
            BossAbilityType primaryAbility,
            BossAbilityType secondaryAbility,
            float colorR,
            float colorG,
            float colorB,
            string description)
        {
            Id = id;
            Name = name;
            Title = title;
            Archetype = archetype;
            BaseHealth = baseHealth;
            BaseArmor = baseArmor;
            BaseMoveSpeed = baseMoveSpeed;
            Radius = radius;
            AttackRange = attackRange;
            AttackDamage = attackDamage;
            AttackWindup = attackWindup;
            AttackCooldown = attackCooldown;
            PrimaryAbility = primaryAbility;
            SecondaryAbility = secondaryAbility;
            ColorR = colorR;
            ColorG = colorG;
            ColorB = colorB;
            Description = description;
        }
    }

    public static class LegendaryBossCatalog
    {
        private static readonly Dictionary<string, LegendaryBossDefinition> Definitions = new(StringComparer.OrdinalIgnoreCase);
        private static readonly List<LegendaryBossDefinition> OrderedList = new();

        static LegendaryBossCatalog()
        {
            RegisterAll();
        }

        public static LegendaryBossDefinition Get(string id)
        {
            if (Definitions.Count == 0)
                RegisterAll();

            if (!string.IsNullOrEmpty(id) && Definitions.TryGetValue(id, out var def))
                return def;

            return OrderedList.Count > 0 ? OrderedList[0] : null;
        }

        public static LegendaryBossDefinition GetByIndex(int index)
        {
            if (OrderedList.Count == 0)
                RegisterAll();

            int clamped = Math.Clamp(index, 0, OrderedList.Count - 1);
            return OrderedList[clamped];
        }

        public static IReadOnlyList<LegendaryBossDefinition> All
        {
            get
            {
                if (OrderedList.Count == 0)
                    RegisterAll();
                return OrderedList;
            }
        }

        public static LegendaryBossDefinition PickForWave(int waveNumber, long seed)
        {
            if (OrderedList.Count == 0)
                RegisterAll();

            // Deterministic selection based on seed and wave number
            int hash = unchecked((int)(seed * 397 ^ (waveNumber * 17)));
            int idx = Math.Abs(hash) % OrderedList.Count;
            return OrderedList[idx];
        }

        public static BossModifier RollModifiersForWave(int waveNumber, long seed)
        {
            var mods = BossModifier.Enrage; // Baseline modifier guaranteed on all milestone bosses

            // Higher waves roll additional modifiers
            int count = 1;
            if (waveNumber >= 30) count = 3;
            else if (waveNumber >= 20) count = 2;

            if (count >= 2)
            {
                // Deterministic choice of second modifier
                int roll = unchecked((int)(seed + waveNumber * 31)) % 4;
                switch (roll)
                {
                    case 0: mods |= BossModifier.Regeneration; break;
                    case 1: mods |= BossModifier.Barrier; break;
                    case 2: mods |= BossModifier.Reinforcements; break;
                    default: mods |= BossModifier.ElementalOvercharge; break;
                }
            }

            if (count >= 3)
            {
                mods |= BossModifier.Barrier;
                mods |= BossModifier.ElementalOvercharge;
            }

            return mods;
        }

        private static void Register(LegendaryBossDefinition def)
        {
            Definitions[def.Id] = def;
            OrderedList.Add(def);
        }

        private static void RegisterAll()
        {
            if (Definitions.Count > 0) return;

            // 1. Sir Valerius the Radiant Dawn
            Register(new LegendaryBossDefinition(
                "boss_valerius", "Sir Valerius", "The Radiant Dawn", InvaderArchetype.Paladin,
                baseHealth: 280f, baseArmor: 20f, baseMoveSpeed: 2.6f, radius: 0.85f,
                attackRange: 2.5f, attackDamage: 24f, attackWindup: 0.6f, attackCooldown: 1.8f,
                primaryAbility: BossAbilityType.SanctuaryWard, secondaryAbility: BossAbilityType.ColossalCleave,
                colorR: 1.0f, colorG: 0.88f, colorB: 0.4f,
                description: "Vanguard of the solar order; projects impenetrable light wards and smites with blazing steel."));

            // 2. Lady Cassandra the Whispering Phantom
            Register(new LegendaryBossDefinition(
                "boss_cassandra", "Lady Cassandra", "The Whispering Phantom", InvaderArchetype.Assassin,
                baseHealth: 230f, baseArmor: 10f, baseMoveSpeed: 3.8f, radius: 0.7f,
                attackRange: 1.8f, attackDamage: 28f, attackWindup: 0.35f, attackCooldown: 1.2f,
                primaryAbility: BossAbilityType.PhantomBlinkStrike, secondaryAbility: BossAbilityType.MultishotBarrage,
                colorR: 0.35f, colorG: 0.15f, colorB: 0.5f,
                description: "Phantom assassin whose blades phase between spectral planes, blinking behind victims."));

            // 3. Archmage Ignis the Pyre Sovereign
            Register(new LegendaryBossDefinition(
                "boss_ignis", "Archmage Ignis", "The Pyre Sovereign", InvaderArchetype.FireMage,
                baseHealth: 250f, baseArmor: 12f, baseMoveSpeed: 2.5f, radius: 0.8f,
                attackRange: 7.5f, attackDamage: 26f, attackWindup: 0.7f, attackCooldown: 2.0f,
                primaryAbility: BossAbilityType.MoltenEruption, secondaryAbility: BossAbilityType.ShockwaveSlam,
                colorR: 1.0f, colorG: 0.3f, colorB: 0.1f,
                description: "Pyromancer lord commanding magma pillars that incinerate the arena."));

            // 4. Grand Inquisitor Malakor
            Register(new LegendaryBossDefinition(
                "boss_malakor", "Grand Inquisitor Malakor", "The Cleanser", InvaderArchetype.WitchHunter,
                baseHealth: 260f, baseArmor: 16f, baseMoveSpeed: 2.9f, radius: 0.78f,
                attackRange: 6.5f, attackDamage: 25f, attackWindup: 0.5f, attackCooldown: 1.6f,
                primaryAbility: BossAbilityType.MultishotBarrage, secondaryAbility: BossAbilityType.ThunderStrike,
                colorR: 0.8f, colorG: 0.2f, colorB: 0.25f,
                description: "Relentless inquisitor firing consecrated silver barrages and calling judging lightning."));

            // 5. Warlord Kraag the Mountain Breaker
            Register(new LegendaryBossDefinition(
                "boss_kraag", "Warlord Kraag", "The Mountain Breaker", InvaderArchetype.Berserker,
                baseHealth: 340f, baseArmor: 18f, baseMoveSpeed: 2.7f, radius: 0.95f,
                attackRange: 2.6f, attackDamage: 32f, attackWindup: 0.8f, attackCooldown: 2.2f,
                primaryAbility: BossAbilityType.ShockwaveSlam, secondaryAbility: BossAbilityType.WhirlwindCharge,
                colorR: 0.9f, colorG: 0.25f, colorB: 0.15f,
                description: "Colossal barbarian chieftain whose earth-shattering slams fissure stone floors."));

            // 6. High Priestess Elara
            Register(new LegendaryBossDefinition(
                "boss_elara", "High Priestess Elara", "Of the Sunlit Font", InvaderArchetype.Priest,
                baseHealth: 240f, baseArmor: 14f, baseMoveSpeed: 2.6f, radius: 0.75f,
                attackRange: 5.5f, attackDamage: 20f, attackWindup: 0.6f, attackCooldown: 1.7f,
                primaryAbility: BossAbilityType.SanctuaryWard, secondaryAbility: BossAbilityType.SummonVanguard,
                colorR: 0.95f, colorG: 0.92f, colorB: 0.6f,
                description: "High priestess weaving radiant healing fonts and protective sanctuaries."));

            // 7. Sylas the Gale Ranger
            Register(new LegendaryBossDefinition(
                "boss_sylas", "Sylas", "The Gale Ranger", InvaderArchetype.Ranger,
                baseHealth: 240f, baseArmor: 12f, baseMoveSpeed: 3.4f, radius: 0.72f,
                attackRange: 8.5f, attackDamage: 24f, attackWindup: 0.45f, attackCooldown: 1.4f,
                primaryAbility: BossAbilityType.MultishotBarrage, secondaryAbility: BossAbilityType.GlacialPrison,
                colorR: 0.3f, colorG: 0.8f, colorB: 0.5f,
                description: "Master archer loosing tempest volleys that pin foes with chilling winds."));

            // 8. Baron von Eisen the Iron Bastion
            Register(new LegendaryBossDefinition(
                "boss_eisen", "Baron von Eisen", "The Iron Bastion", InvaderArchetype.ShieldGuardian,
                baseHealth: 380f, baseArmor: 28f, baseMoveSpeed: 2.2f, radius: 1.0f,
                attackRange: 2.0f, attackDamage: 22f, attackWindup: 0.7f, attackCooldown: 2.0f,
                primaryAbility: BossAbilityType.ColossalCleave, secondaryAbility: BossAbilityType.SanctuaryWard,
                colorR: 0.65f, colorG: 0.7f, colorB: 0.78f,
                description: "Immovable armored lord deflecting assaults with colossal adamantine plating."));

            // 9. Vaelin the Storm Weaver
            Register(new LegendaryBossDefinition(
                "boss_vaelin", "Vaelin", "The Storm Weaver", InvaderArchetype.LightningMage,
                baseHealth: 250f, baseArmor: 12f, baseMoveSpeed: 2.8f, radius: 0.78f,
                attackRange: 7.0f, attackDamage: 27f, attackWindup: 0.55f, attackCooldown: 1.6f,
                primaryAbility: BossAbilityType.ThunderStrike, secondaryAbility: BossAbilityType.ShockwaveSlam,
                colorR: 0.4f, colorG: 0.65f, colorB: 1.0f,
                description: "Stormcaller channeling continuous lightning strikes and static shockwaves."));

            // 10. Lord Alden the Royal Champion
            Register(new LegendaryBossDefinition(
                "boss_alden", "Lord Alden", "The Royal Champion", InvaderArchetype.RoyalChampion,
                baseHealth: CombatTuning.BossHealth, baseArmor: 22f, baseMoveSpeed: 2.5f, radius: 0.95f,
                attackRange: 2.6f, attackDamage: 30f, attackWindup: 0.65f, attackCooldown: 1.9f,
                primaryAbility: BossAbilityType.WhirlwindCharge, secondaryAbility: BossAbilityType.ColossalCleave,
                colorR: 0.7f, colorG: 0.2f, colorB: 0.85f,
                description: "Crown commander wielding an ancestral greatsword in sweeping whirlwind sweeps."));

            // 11. Lady Vespera the Twilight Huntress
            Register(new LegendaryBossDefinition(
                "boss_vespera", "Lady Vespera", "The Twilight Huntress", InvaderArchetype.Crossbowman,
                baseHealth: 250f, baseArmor: 14f, baseMoveSpeed: 3.1f, radius: 0.75f,
                attackRange: 8.0f, attackDamage: 25f, attackWindup: 0.5f, attackCooldown: 1.5f,
                primaryAbility: BossAbilityType.MultishotBarrage, secondaryAbility: BossAbilityType.PhantomBlinkStrike,
                colorR: 0.5f, colorG: 0.25f, colorB: 0.65f,
                description: "Silent marksman dual-wielding heavy repeating crossbows with shadow bolts."));

            // 12. Brother Kaelen the Iron Palm
            Register(new LegendaryBossDefinition(
                "boss_kaelen_monk", "Brother Kaelen", "The Iron Palm", InvaderArchetype.BattleMonk,
                baseHealth: 300f, baseArmor: 18f, baseMoveSpeed: 3.3f, radius: 0.8f,
                attackRange: 1.9f, attackDamage: 26f, attackWindup: 0.4f, attackCooldown: 1.3f,
                primaryAbility: BossAbilityType.ShockwaveSlam, secondaryAbility: BossAbilityType.SanctuaryWard,
                colorR: 0.85f, colorG: 0.65f, colorB: 0.35f,
                description: "Ascetic martial grandmaster breaking dungeon masonry with concussive ki blasts."));

            // 13. Morvath the Soul Harvester
            Register(new LegendaryBossDefinition(
                "boss_morvath", "Morvath", "The Soul Harvester", InvaderArchetype.ArcaneWizard,
                baseHealth: 260f, baseArmor: 14f, baseMoveSpeed: 2.6f, radius: 0.8f,
                attackRange: 7.2f, attackDamage: 28f, attackWindup: 0.65f, attackCooldown: 1.8f,
                primaryAbility: BossAbilityType.GlacialPrison, secondaryAbility: BossAbilityType.ThunderStrike,
                colorR: 0.6f, colorG: 0.3f, colorB: 0.9f,
                description: "Forbidden arcanist freezing the lifeforce of bosses within ethereal prisons."));

            // 14. Commander Gerald of the Silver Watch
            Register(new LegendaryBossDefinition(
                "boss_gerald", "Commander Gerald", "Of the Silver Watch", InvaderArchetype.Knight,
                baseHealth: 310f, baseArmor: 24f, baseMoveSpeed: 2.6f, radius: 0.9f,
                attackRange: 2.4f, attackDamage: 26f, attackWindup: 0.6f, attackCooldown: 1.8f,
                primaryAbility: BossAbilityType.SummonVanguard, secondaryAbility: BossAbilityType.ColossalCleave,
                colorR: 0.8f, colorG: 0.85f, colorB: 0.95f,
                description: "Disciplined legionary commander rallying disciplined vanguard phalanxes."));

            // 15. Theron the Dragonbane Dragoon
            Register(new LegendaryBossDefinition(
                "boss_theron", "Theron", "The Dragonbane Dragoon", InvaderArchetype.Spearman,
                baseHealth: 290f, baseArmor: 18f, baseMoveSpeed: 3.2f, radius: 0.82f,
                attackRange: 3.2f, attackDamage: 28f, attackWindup: 0.5f, attackCooldown: 1.5f,
                primaryAbility: BossAbilityType.WhirlwindCharge, secondaryAbility: BossAbilityType.ShockwaveSlam,
                colorR: 0.9f, colorG: 0.5f, colorB: 0.2f,
                description: "Sky-leaping dragoon plunging wyrmtooth pikes straight through defensive wards."));

            // 16. Arch-Druid Bran the Primal Maw
            Register(new LegendaryBossDefinition(
                "boss_bran", "Arch-Druid Bran", "The Primal Maw", InvaderArchetype.Druid,
                baseHealth: 330f, baseArmor: 16f, baseMoveSpeed: 2.7f, radius: 0.92f,
                attackRange: 4.0f, attackDamage: 27f, attackWindup: 0.6f, attackCooldown: 1.8f,
                primaryAbility: BossAbilityType.ShockwaveSlam, secondaryAbility: BossAbilityType.SummonVanguard,
                colorR: 0.25f, colorG: 0.65f, colorB: 0.3f,
                description: "Ancient shapeshifter shaking the foundations with primeval nature earthshakers."));

            // 17. Lord Zephyr the Swiftblade
            Register(new LegendaryBossDefinition(
                "boss_zephyr", "Lord Zephyr", "The Swiftblade", InvaderArchetype.Spellblade,
                baseHealth: 270f, baseArmor: 15f, baseMoveSpeed: 3.6f, radius: 0.78f,
                attackRange: 2.2f, attackDamage: 27f, attackWindup: 0.38f, attackCooldown: 1.25f,
                primaryAbility: BossAbilityType.PhantomBlinkStrike, secondaryAbility: BossAbilityType.WhirlwindCharge,
                colorR: 0.45f, colorG: 0.75f, colorB: 0.9f,
                description: "Wind duelist cutting through air molecules with tempest-infused twin swords."));

            // 18. Grand Alchemist Aurelius
            Register(new LegendaryBossDefinition(
                "boss_aurelius", "Grand Alchemist Aurelius", "The Toxifier", InvaderArchetype.Alchemist,
                baseHealth: 260f, baseArmor: 14f, baseMoveSpeed: 2.8f, radius: 0.78f,
                attackRange: 6.8f, attackDamage: 26f, attackWindup: 0.55f, attackCooldown: 1.7f,
                primaryAbility: BossAbilityType.MoltenEruption, secondaryAbility: BossAbilityType.MultishotBarrage,
                colorR: 0.6f, colorG: 0.85f, colorB: 0.2f,
                description: "Master poisoner showering combat chambers with volatile corrosive chemical flasks."));

            // 19. Ragnar the Frost Jarl
            Register(new LegendaryBossDefinition(
                "boss_ragnar", "Ragnar", "The Frost Jarl", InvaderArchetype.Berserker,
                baseHealth: 350f, baseArmor: 20f, baseMoveSpeed: 2.6f, radius: 0.98f,
                attackRange: 2.5f, attackDamage: 32f, attackWindup: 0.75f, attackCooldown: 2.1f,
                primaryAbility: BossAbilityType.GlacialPrison, secondaryAbility: BossAbilityType.ColossalCleave,
                colorR: 0.35f, colorG: 0.7f, colorB: 0.95f,
                description: "Northern warlord swinging glacier reavers that encase dungeon defenders in ice."));

            // 20. Lady Seraphina the Celestial Beacon
            Register(new LegendaryBossDefinition(
                "boss_seraphina", "Lady Seraphina", "The Celestial Beacon", InvaderArchetype.HolyTemplar,
                baseHealth: 300f, baseArmor: 24f, baseMoveSpeed: 2.7f, radius: 0.88f,
                attackRange: 2.5f, attackDamage: 27f, attackWindup: 0.55f, attackCooldown: 1.7f,
                primaryAbility: BossAbilityType.SanctuaryWard, secondaryAbility: BossAbilityType.ThunderStrike,
                colorR: 0.95f, colorG: 0.9f, colorB: 0.5f,
                description: "Templar saint surrounding adventurers with radiant light barriers and holy reckoning."));

            // 21. Darius the Voidstalker
            Register(new LegendaryBossDefinition(
                "boss_darius", "Darius", "The Voidstalker", InvaderArchetype.ShadowDuelist,
                baseHealth: 250f, baseArmor: 12f, baseMoveSpeed: 3.9f, radius: 0.72f,
                attackRange: 1.8f, attackDamage: 29f, attackWindup: 0.32f, attackCooldown: 1.15f,
                primaryAbility: BossAbilityType.PhantomBlinkStrike, secondaryAbility: BossAbilityType.ShockwaveSlam,
                colorR: 0.2f, colorG: 0.1f, colorB: 0.35f,
                description: "Nether-touched stalker emerging from pitch-black rift tears to execute precision cuts."));

            // 22. Kallista the Beast Sovereign
            Register(new LegendaryBossDefinition(
                "boss_kallista", "Kallista", "The Beast Sovereign", InvaderArchetype.Beastmaster,
                baseHealth: 280f, baseArmor: 15f, baseMoveSpeed: 3.0f, radius: 0.8f,
                attackRange: 5.0f, attackDamage: 25f, attackWindup: 0.5f, attackCooldown: 1.6f,
                primaryAbility: BossAbilityType.SummonVanguard, secondaryAbility: BossAbilityType.WhirlwindCharge,
                colorR: 0.7f, colorG: 0.45f, colorB: 0.25f,
                description: "Apex huntress commanding packs of fierce predatory beasts to swarm the boss."));

            // 23. High Magus Corvin
            Register(new LegendaryBossDefinition(
                "boss_corvin", "High Magus Corvin", "The Glacial Warden", InvaderArchetype.IceMage,
                baseHealth: 260f, baseArmor: 14f, baseMoveSpeed: 2.5f, radius: 0.8f,
                attackRange: 7.2f, attackDamage: 26f, attackWindup: 0.6f, attackCooldown: 1.8f,
                primaryAbility: BossAbilityType.GlacialPrison, secondaryAbility: BossAbilityType.MoltenEruption,
                colorR: 0.45f, colorG: 0.8f, colorB: 0.95f,
                description: "Frost archmage summoning creeping blizzards that lock the arena in permafrost."));

            // 24. Thorgar the Rune Forger
            Register(new LegendaryBossDefinition(
                "boss_thorgar", "Thorgar", "The Rune Forger", InvaderArchetype.RuneKnight,
                baseHealth: 320f, baseArmor: 22f, baseMoveSpeed: 2.7f, radius: 0.9f,
                attackRange: 2.4f, attackDamage: 29f, attackWindup: 0.6f, attackCooldown: 1.8f,
                primaryAbility: BossAbilityType.ThunderStrike, secondaryAbility: BossAbilityType.ColossalCleave,
                colorR: 0.35f, colorG: 0.8f, colorB: 0.85f,
                description: "Dwarven runic artisan hammering ancient power glyphs into the stone floors."));

            // 25. Matron Alyssa the Puppet Empress
            Register(new LegendaryBossDefinition(
                "boss_alyssa", "Matron Alyssa", "The Puppet Empress", InvaderArchetype.Summoner,
                baseHealth: 270f, baseArmor: 14f, baseMoveSpeed: 2.6f, radius: 0.8f,
                attackRange: 6.5f, attackDamage: 24f, attackWindup: 0.55f, attackCooldown: 1.7f,
                primaryAbility: BossAbilityType.SummonVanguard, secondaryAbility: BossAbilityType.SanctuaryWard,
                colorR: 0.85f, colorG: 0.35f, colorB: 0.65f,
                description: "Strings mistress animating steel marionette retainers to overwhelm defenders."));

            // 26. Garrick the Behemoth Slayer
            Register(new LegendaryBossDefinition(
                "boss_garrick", "Garrick", "The Behemoth Slayer", InvaderArchetype.MonsterHunter,
                baseHealth: 290f, baseArmor: 18f, baseMoveSpeed: 3.0f, radius: 0.85f,
                attackRange: 7.8f, attackDamage: 30f, attackWindup: 0.7f, attackCooldown: 2.0f,
                primaryAbility: BossAbilityType.MultishotBarrage, secondaryAbility: BossAbilityType.ShockwaveSlam,
                colorR: 0.75f, colorG: 0.4f, colorB: 0.2f,
                description: "Veteran colossus hunter deploying heavy siege stakes designed to topple giant beasts."));

            // 27. Roland the Unyielding Bulwark
            Register(new LegendaryBossDefinition(
                "boss_roland", "Roland", "The Unyielding Bulwark", InvaderArchetype.Paladin,
                baseHealth: 360f, baseArmor: 26f, baseMoveSpeed: 2.4f, radius: 0.96f,
                attackRange: 2.2f, attackDamage: 25f, attackWindup: 0.65f, attackCooldown: 1.9f,
                primaryAbility: BossAbilityType.ColossalCleave, secondaryAbility: BossAbilityType.SanctuaryWard,
                colorR: 0.9f, colorG: 0.85f, colorB: 0.7f,
                description: "Unbroken knight whose tower shield withstands devastating boss onslaughts."));

            // 28. Kaelen the Bloodhound
            Register(new LegendaryBossDefinition(
                "boss_witchhunter_kaelen", "Kaelen", "The Bloodhound", InvaderArchetype.WitchHunter,
                baseHealth: 270f, baseArmor: 15f, baseMoveSpeed: 3.1f, radius: 0.78f,
                attackRange: 6.8f, attackDamage: 27f, attackWindup: 0.5f, attackCooldown: 1.55f,
                primaryAbility: BossAbilityType.ThunderStrike, secondaryAbility: BossAbilityType.PhantomBlinkStrike,
                colorR: 0.65f, colorG: 0.2f, colorB: 0.2f,
                description: "Trackdown specialist relentlessly hunting dungeon horrors with incendiary shotguns."));

            // 29. Lady Isabelle the Cinder Knight
            Register(new LegendaryBossDefinition(
                "boss_isabelle", "Lady Isabelle", "The Cinder Knight", InvaderArchetype.Spellblade,
                baseHealth: 290f, baseArmor: 17f, baseMoveSpeed: 3.3f, radius: 0.82f,
                attackRange: 2.5f, attackDamage: 28f, attackWindup: 0.45f, attackCooldown: 1.4f,
                primaryAbility: BossAbilityType.MoltenEruption, secondaryAbility: BossAbilityType.WhirlwindCharge,
                colorR: 0.95f, colorG: 0.4f, colorB: 0.15f,
                description: "Molten duelist leaving burning magma trails with every fluid blade cut."));

            // 30. Lord Cassian the Sovereign Duelist
            Register(new LegendaryBossDefinition(
                "boss_cassian", "Lord Cassian", "The Sovereign Duelist", InvaderArchetype.Rogue,
                baseHealth: 260f, baseArmor: 13f, baseMoveSpeed: 3.8f, radius: 0.72f,
                attackRange: 2.0f, attackDamage: 30f, attackWindup: 0.35f, attackCooldown: 1.15f,
                primaryAbility: BossAbilityType.PhantomBlinkStrike, secondaryAbility: BossAbilityType.ColossalCleave,
                colorR: 0.3f, colorG: 0.4f, colorB: 0.75f,
                description: "Peerless fencing champion executing lethal parry ripostes and shadowy vanishes."));
        }
    }
}
