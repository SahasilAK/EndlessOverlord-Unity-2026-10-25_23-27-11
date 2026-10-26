using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public enum InvaderCombatRole
    {
        Melee = 0,
        Ranged = 1,
        Caster = 2,
        HealerSupport = 3,
        Assassin = 4,
        Tank = 5
    }

    public sealed class InvaderDefinition
    {
        public InvaderArchetype Archetype { get; }
        public string Name { get; }
        public InvaderCombatRole Role { get; }
        public float BaseHealth { get; }
        public float BaseArmor { get; }
        public float BaseMoveSpeed { get; }
        public float Radius { get; }
        public float AttackRange { get; }
        public float AttackDamage { get; }
        public float AttackWindup { get; }
        public float AttackCooldown { get; }
        public float ColorR { get; }
        public float ColorG { get; }
        public float ColorB { get; }
        public bool IsBoss { get; }
        public string Description { get; }

        public InvaderDefinition(
            InvaderArchetype archetype,
            string name,
            InvaderCombatRole role,
            float baseHealth,
            float baseArmor,
            float baseMoveSpeed,
            float radius,
            float attackRange,
            float attackDamage,
            float attackWindup,
            float attackCooldown,
            float colorR,
            float colorG,
            float colorB,
            bool isBoss = false,
            string description = "")
        {
            Archetype = archetype;
            Name = name;
            Role = role;
            BaseHealth = baseHealth;
            BaseArmor = baseArmor;
            BaseMoveSpeed = baseMoveSpeed;
            Radius = radius;
            AttackRange = attackRange;
            AttackDamage = attackDamage;
            AttackWindup = attackWindup;
            AttackCooldown = attackCooldown;
            ColorR = colorR;
            ColorG = colorG;
            ColorB = colorB;
            IsBoss = isBoss;
            Description = description;
        }
    }

    public static class InvaderCatalog
    {
        private static readonly Dictionary<InvaderArchetype, InvaderDefinition> Definitions = new();

        static InvaderCatalog()
        {
            RegisterAll();
        }

        public static InvaderDefinition Get(InvaderArchetype archetype)
        {
            if (Definitions.Count == 0)
                RegisterAll();

            if (Definitions.TryGetValue(archetype, out var def))
                return def;

            return Definitions[InvaderArchetype.Swordsman];
        }

        public static IReadOnlyCollection<InvaderDefinition> All => Definitions.Values;

        private static void Register(InvaderDefinition def)
        {
            Definitions[def.Archetype] = def;
        }

        private static void RegisterAll()
        {
            Definitions.Clear();

            // 1. Swordsman
            Register(new InvaderDefinition(
                InvaderArchetype.Swordsman,
                "Swordsman",
                InvaderCombatRole.Melee,
                baseHealth: 35f,
                baseArmor: 5f,
                baseMoveSpeed: 3.2f,
                radius: 0.5f,
                attackRange: 1.3f,
                attackDamage: 10f,
                attackWindup: 0.5f,
                attackCooldown: 1.4f,
                colorR: 0.9f, colorG: 0.35f, colorB: 0.3f,
                description: "Standard frontline melee infantry with balanced strikes."));

            // 2. Archer
            Register(new InvaderDefinition(
                InvaderArchetype.Archer,
                "Archer",
                InvaderCombatRole.Ranged,
                baseHealth: 25f,
                baseArmor: 0f,
                baseMoveSpeed: 2.8f,
                radius: 0.45f,
                attackRange: 7.5f,
                attackDamage: 8f,
                attackWindup: 0.65f,
                attackCooldown: 2.2f,
                colorR: 0.95f, colorG: 0.65f, colorB: 0.2f,
                description: "Nimble ranged bowman attacking from safe distance."));

            // 3. Crossbowman
            Register(new InvaderDefinition(
                InvaderArchetype.Crossbowman,
                "Crossbowman",
                InvaderCombatRole.Ranged,
                baseHealth: 30f,
                baseArmor: 4f,
                baseMoveSpeed: 2.4f,
                radius: 0.48f,
                attackRange: 8.5f,
                attackDamage: 16f,
                attackWindup: 0.9f,
                attackCooldown: 3.0f,
                colorR: 0.8f, colorG: 0.55f, colorB: 0.3f,
                description: "Heavy armor-piercing ranged marksman with powerful, slow bolts."));

            // 4. Spearman
            Register(new InvaderDefinition(
                InvaderArchetype.Spearman,
                "Spearman",
                InvaderCombatRole.Melee,
                baseHealth: 38f,
                baseArmor: 6f,
                baseMoveSpeed: 3.0f,
                radius: 0.5f,
                attackRange: 2.2f,
                attackDamage: 11f,
                attackWindup: 0.55f,
                attackCooldown: 1.5f,
                colorR: 0.85f, colorG: 0.4f, colorB: 0.25f,
                description: "Disciplined infantry with extended melee thrust reach."));

            // 5. Berserker
            Register(new InvaderDefinition(
                InvaderArchetype.Berserker,
                "Berserker",
                InvaderCombatRole.Melee,
                baseHealth: 45f,
                baseArmor: 2f,
                baseMoveSpeed: 3.8f,
                radius: 0.52f,
                attackRange: 1.4f,
                attackDamage: 15f,
                attackWindup: 0.4f,
                attackCooldown: 1.2f,
                colorR: 0.95f, colorG: 0.15f, colorB: 0.15f,
                description: "Frenzied combatant rushing forward with devastating attack speed."));

            // 6. Rogue
            Register(new InvaderDefinition(
                InvaderArchetype.Rogue,
                "Rogue",
                InvaderCombatRole.Assassin,
                baseHealth: 28f,
                baseArmor: 2f,
                baseMoveSpeed: 4.0f,
                radius: 0.42f,
                attackRange: 1.2f,
                attackDamage: 14f,
                attackWindup: 0.35f,
                attackCooldown: 1.1f,
                colorR: 0.5f, colorG: 0.5f, colorB: 0.55f,
                description: "Evasive skirmisher darting around the dungeon."));

            // 7. Assassin
            Register(new InvaderDefinition(
                InvaderArchetype.Assassin,
                "Assassin",
                InvaderCombatRole.Assassin,
                baseHealth: 26f,
                baseArmor: 1f,
                baseMoveSpeed: 4.2f,
                radius: 0.4f,
                attackRange: 1.2f,
                attackDamage: 20f,
                attackWindup: 0.3f,
                attackCooldown: 1.5f,
                colorR: 0.35f, colorG: 0.25f, colorB: 0.45f,
                description: "Lethal shadow operative executing high burst ambush strikes."));

            // 8. Knight
            Register(new InvaderDefinition(
                InvaderArchetype.Knight,
                "Knight",
                InvaderCombatRole.Tank,
                baseHealth: 65f,
                baseArmor: 15f,
                baseMoveSpeed: 2.3f,
                radius: 0.58f,
                attackRange: 1.4f,
                attackDamage: 12f,
                attackWindup: 0.6f,
                attackCooldown: 1.8f,
                colorR: 0.7f, colorG: 0.75f, colorB: 0.8f,
                description: "Heavy plate-armored guardian absorbing heavy punishment."));

            // 9. Paladin
            Register(new InvaderDefinition(
                InvaderArchetype.Paladin,
                "Paladin",
                InvaderCombatRole.Tank,
                baseHealth: 75f,
                baseArmor: 16f,
                baseMoveSpeed: 2.2f,
                radius: 0.6f,
                attackRange: 1.5f,
                attackDamage: 14f,
                attackWindup: 0.65f,
                attackCooldown: 2.0f,
                colorR: 0.9f, colorG: 0.85f, colorB: 0.45f,
                description: "Holy crusader reinforced with righteous armor and smiting strength."));

            // 10. Cleric
            Register(new InvaderDefinition(
                InvaderArchetype.Cleric,
                "Cleric",
                InvaderCombatRole.HealerSupport,
                baseHealth: 40f,
                baseArmor: 8f,
                baseMoveSpeed: 2.5f,
                radius: 0.5f,
                attackRange: 6.0f,
                attackDamage: 6f,
                attackWindup: 0.8f,
                attackCooldown: 3.5f,
                colorR: 0.95f, colorG: 0.9f, colorB: 0.3f,
                description: "Field medic casting restorative waves on injured comrades."));

            // 11. Priest
            Register(new InvaderDefinition(
                InvaderArchetype.Priest,
                "Priest",
                InvaderCombatRole.HealerSupport,
                baseHealth: 36f,
                baseArmor: 4f,
                baseMoveSpeed: 2.6f,
                radius: 0.48f,
                attackRange: 6.5f,
                attackDamage: 5f,
                attackWindup: 0.75f,
                attackCooldown: 3.0f,
                colorR: 0.95f, colorG: 0.95f, colorB: 0.85f,
                description: "Sanctuary chanter reinforcing allies with divine blessings."));

            // 12. Fire Mage
            Register(new InvaderDefinition(
                InvaderArchetype.FireMage,
                "Fire Mage",
                InvaderCombatRole.Caster,
                baseHealth: 32f,
                baseArmor: 2f,
                baseMoveSpeed: 2.5f,
                radius: 0.46f,
                attackRange: 7.0f,
                attackDamage: 18f,
                attackWindup: 0.8f,
                attackCooldown: 2.8f,
                colorR: 1.0f, colorG: 0.3f, colorB: 0.1f,
                description: "Destructive pyromancer hurling blazing explosive orbs."));

            // 13. Ice Mage
            Register(new InvaderDefinition(
                InvaderArchetype.IceMage,
                "Ice Mage",
                InvaderCombatRole.Caster,
                baseHealth: 34f,
                baseArmor: 4f,
                baseMoveSpeed: 2.5f,
                radius: 0.46f,
                attackRange: 7.0f,
                attackDamage: 12f,
                attackWindup: 0.7f,
                attackCooldown: 2.5f,
                colorR: 0.3f, colorG: 0.75f, colorB: 1.0f,
                description: "Frost spellcaster hurling chilling spikes that slow targets."));

            // 14. Lightning Mage
            Register(new InvaderDefinition(
                InvaderArchetype.LightningMage,
                "Lightning Mage",
                InvaderCombatRole.Caster,
                baseHealth: 30f,
                baseArmor: 1f,
                baseMoveSpeed: 2.7f,
                radius: 0.45f,
                attackRange: 7.5f,
                attackDamage: 15f,
                attackWindup: 0.5f,
                attackCooldown: 2.2f,
                colorR: 0.95f, colorG: 0.95f, colorB: 0.2f,
                description: "Storm evoker releasing rapid-discharge kinetic bolts."));

            // 15. Arcane Wizard
            Register(new InvaderDefinition(
                InvaderArchetype.ArcaneWizard,
                "Arcane Wizard",
                InvaderCombatRole.Caster,
                baseHealth: 35f,
                baseArmor: 3f,
                baseMoveSpeed: 2.6f,
                radius: 0.47f,
                attackRange: 8.0f,
                attackDamage: 20f,
                attackWindup: 0.85f,
                attackCooldown: 3.0f,
                colorR: 0.75f, colorG: 0.2f, colorB: 0.95f,
                description: "Grand academic conjuring piercing arcane missiles."));

            // 16. Witch Hunter
            Register(new InvaderDefinition(
                InvaderArchetype.WitchHunter,
                "Witch Hunter",
                InvaderCombatRole.Ranged,
                baseHealth: 42f,
                baseArmor: 6f,
                baseMoveSpeed: 3.2f,
                radius: 0.48f,
                attackRange: 7.0f,
                attackDamage: 13f,
                attackWindup: 0.55f,
                attackCooldown: 1.8f,
                colorR: 0.4f, colorG: 0.35f, colorB: 0.3f,
                description: "Grim inquisitor skilled at tracking and dispelling occult threats."));

            // 17. Monster Hunter
            Register(new InvaderDefinition(
                InvaderArchetype.MonsterHunter,
                "Monster Hunter",
                InvaderCombatRole.Ranged,
                baseHealth: 48f,
                baseArmor: 8f,
                baseMoveSpeed: 3.0f,
                radius: 0.5f,
                attackRange: 6.5f,
                attackDamage: 15f,
                attackWindup: 0.6f,
                attackCooldown: 2.0f,
                colorR: 0.6f, colorG: 0.45f, colorB: 0.25f,
                description: "Veteran beast trapper using heavy projectile traps and barbed darts."));

            // 18. Ranger
            Register(new InvaderDefinition(
                InvaderArchetype.Ranger,
                "Ranger",
                InvaderCombatRole.Ranged,
                baseHealth: 35f,
                baseArmor: 4f,
                baseMoveSpeed: 3.5f,
                radius: 0.46f,
                attackRange: 8.0f,
                attackDamage: 10f,
                attackWindup: 0.45f,
                attackCooldown: 1.5f,
                colorR: 0.3f, colorG: 0.7f, colorB: 0.35f,
                description: "Woodland sharpshooter with fluid mobile kiting tactics."));

            // 19. Druid
            Register(new InvaderDefinition(
                InvaderArchetype.Druid,
                "Druid",
                InvaderCombatRole.HealerSupport,
                baseHealth: 50f,
                baseArmor: 6f,
                baseMoveSpeed: 2.8f,
                radius: 0.52f,
                attackRange: 6.0f,
                attackDamage: 11f,
                attackWindup: 0.7f,
                attackCooldown: 2.6f,
                colorR: 0.25f, colorG: 0.6f, colorB: 0.2f,
                description: "Nature custodian casting regenerative mists and entangling thorns."));

            // 20. Alchemist
            Register(new InvaderDefinition(
                InvaderArchetype.Alchemist,
                "Alchemist",
                InvaderCombatRole.Caster,
                baseHealth: 38f,
                baseArmor: 4f,
                baseMoveSpeed: 2.9f,
                radius: 0.48f,
                attackRange: 6.5f,
                attackDamage: 14f,
                attackWindup: 0.65f,
                attackCooldown: 2.4f,
                colorR: 0.35f, colorG: 0.85f, colorB: 0.4f,
                description: "Volatile researcher hurling caustic chemical flasks."));

            // 21. Beastmaster
            Register(new InvaderDefinition(
                InvaderArchetype.Beastmaster,
                "Beastmaster",
                InvaderCombatRole.Caster,
                baseHealth: 55f,
                baseArmor: 8f,
                baseMoveSpeed: 3.1f,
                radius: 0.54f,
                attackRange: 5.5f,
                attackDamage: 13f,
                attackWindup: 0.6f,
                attackCooldown: 2.5f,
                colorR: 0.7f, colorG: 0.45f, colorB: 0.2f,
                description: "Primal commander coordinating attacks alongside trained hounds."));

            // 22. Summoner
            Register(new InvaderDefinition(
                InvaderArchetype.Summoner,
                "Summoner",
                InvaderCombatRole.HealerSupport,
                baseHealth: 42f,
                baseArmor: 3f,
                baseMoveSpeed: 2.6f,
                radius: 0.48f,
                attackRange: 7.0f,
                attackDamage: 10f,
                attackWindup: 0.9f,
                attackCooldown: 3.8f,
                colorR: 0.6f, colorG: 0.15f, colorB: 0.7f,
                description: "Occult summoner opening rifts to call forth auxiliary spirits."));

            // 23. Shield Guardian
            Register(new InvaderDefinition(
                InvaderArchetype.ShieldGuardian,
                "Shield Guardian",
                InvaderCombatRole.Tank,
                baseHealth: 85f,
                baseArmor: 22f,
                baseMoveSpeed: 2.0f,
                radius: 0.62f,
                attackRange: 1.3f,
                attackDamage: 10f,
                attackWindup: 0.7f,
                attackCooldown: 2.2f,
                colorR: 0.45f, colorG: 0.55f, colorB: 0.65f,
                description: "Impenetrable tower-shield fortress deflecting direct strikes."));

            // 24. Battle Monk
            Register(new InvaderDefinition(
                InvaderArchetype.BattleMonk,
                "Battle Monk",
                InvaderCombatRole.Melee,
                baseHealth: 52f,
                baseArmor: 7f,
                baseMoveSpeed: 3.6f,
                radius: 0.5f,
                attackRange: 1.3f,
                attackDamage: 12f,
                attackWindup: 0.35f,
                attackCooldown: 0.9f,
                colorR: 0.85f, colorG: 0.65f, colorB: 0.4f,
                description: "Disciplined martial artist delivering rapid consecutive strikes."));

            // 25. Holy Templar
            Register(new InvaderDefinition(
                InvaderArchetype.HolyTemplar,
                "Holy Templar",
                InvaderCombatRole.Tank,
                baseHealth: 80f,
                baseArmor: 18f,
                baseMoveSpeed: 2.3f,
                radius: 0.6f,
                attackRange: 1.5f,
                attackDamage: 16f,
                attackWindup: 0.6f,
                attackCooldown: 1.9f,
                colorR: 0.95f, colorG: 0.85f, colorB: 0.6f,
                description: "Fanatical champion wielding blessed steel and radiant protection."));

            // 26. Rune Knight
            Register(new InvaderDefinition(
                InvaderArchetype.RuneKnight,
                "Rune Knight",
                InvaderCombatRole.Melee,
                baseHealth: 68f,
                baseArmor: 14f,
                baseMoveSpeed: 2.8f,
                radius: 0.56f,
                attackRange: 1.6f,
                attackDamage: 16f,
                attackWindup: 0.55f,
                attackCooldown: 1.6f,
                colorR: 0.3f, colorG: 0.7f, colorB: 0.85f,
                description: "Armored vanguard channeling protective and destructive ancient runes."));

            // 27. Spellblade
            Register(new InvaderDefinition(
                InvaderArchetype.Spellblade,
                "Spellblade",
                InvaderCombatRole.Melee,
                baseHealth: 46f,
                baseArmor: 6f,
                baseMoveSpeed: 3.4f,
                radius: 0.5f,
                attackRange: 2.5f,
                attackDamage: 15f,
                attackWindup: 0.45f,
                attackCooldown: 1.3f,
                colorR: 0.65f, colorG: 0.35f, colorB: 0.9f,
                description: "Hybrid duelist weaving elemental blade waves into melee strikes."));

            // 28. Royal Champion (Boss)
            Register(new InvaderDefinition(
                InvaderArchetype.RoyalChampion,
                "Royal Champion",
                InvaderCombatRole.Tank,
                baseHealth: 220f,
                baseArmor: 15f,
                baseMoveSpeed: 2.4f,
                radius: 0.9f,
                attackRange: 2.4f,
                attackDamage: 20f,
                attackWindup: 0.55f,
                attackCooldown: 1.8f,
                colorR: 0.6f, colorG: 0.2f, colorB: 0.75f,
                isBoss: true,
                description: "Elite commander of the royal vanguard; cleaves foes and summons reinforcements upon enrage."));

            // 29. Dragon Hunter
            Register(new InvaderDefinition(
                InvaderArchetype.DragonHunter,
                "Dragon Hunter",
                InvaderCombatRole.Ranged,
                baseHealth: 60f,
                baseArmor: 12f,
                baseMoveSpeed: 2.9f,
                radius: 0.56f,
                attackRange: 8.0f,
                attackDamage: 22f,
                attackWindup: 0.75f,
                attackCooldown: 2.5f,
                colorR: 0.75f, colorG: 0.3f, colorB: 0.2f,
                description: "Colossus slayer armed with heavy harpoons and dragon-breaking javelins."));

            // 30. Shadow Duelist
            Register(new InvaderDefinition(
                InvaderArchetype.ShadowDuelist,
                "Shadow Duelist",
                InvaderCombatRole.Assassin,
                baseHealth: 44f,
                baseArmor: 5f,
                baseMoveSpeed: 4.1f,
                radius: 0.45f,
                attackRange: 1.5f,
                attackDamage: 18f,
                attackWindup: 0.3f,
                attackCooldown: 1.0f,
                colorR: 0.25f, colorG: 0.15f, colorB: 0.35f,
                description: "Master swordsman dancing through shadows with blinding afterimage thrusts."));
        }
    }
}
