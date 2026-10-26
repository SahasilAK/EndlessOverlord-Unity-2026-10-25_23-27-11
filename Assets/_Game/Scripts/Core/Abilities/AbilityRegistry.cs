using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Central catalog of all implemented active abilities and ultimates across the 5 playable boss classes.
    /// Provides definition lookup, character-specific filtering, and tree traversal.
    /// Pure C# (no engine references).
    /// </summary>
    public static class AbilityRegistry
    {
        private static readonly Dictionary<string, AbilityDefinition> _definitions = new Dictionary<string, AbilityDefinition>(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, List<AbilityDefinition>> _byCharacter = new Dictionary<string, List<AbilityDefinition>>(StringComparer.OrdinalIgnoreCase);

        static AbilityRegistry()
        {
            RegisterAll();
        }

        public static IReadOnlyCollection<AbilityDefinition> AllAbilities => _definitions.Values;

        public static AbilityDefinition Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            _definitions.TryGetValue(id, out var def);
            return def;
        }

        public static bool TryGet(string id, out AbilityDefinition def)
        {
            if (string.IsNullOrEmpty(id))
            {
                def = null;
                return false;
            }
            return _definitions.TryGetValue(id, out def);
        }

        public static IReadOnlyList<AbilityDefinition> GetForCharacter(string characterId)
        {
            if (string.IsNullOrEmpty(characterId)) return Array.Empty<AbilityDefinition>();
            if (_byCharacter.TryGetValue(characterId, out var list))
            {
                return list;
            }
            return Array.Empty<AbilityDefinition>();
        }

        private static void Register(AbilityDefinition def)
        {
            if (def == null || string.IsNullOrEmpty(def.Id)) return;
            _definitions[def.Id] = def;

            if (!_byCharacter.TryGetValue(def.CharacterId, out var list))
            {
                list = new List<AbilityDefinition>();
                _byCharacter[def.CharacterId] = list;
            }
            list.Add(def);
        }

        private static void RegisterAll()
        {
            _definitions.Clear();
            _byCharacter.Clear();

            // -------------------------------------------------------------
            // LICH LORD
            // -------------------------------------------------------------
            Register(new AbilityDefinition(
                id: "lich_lord_active_01",
                characterId: CharacterCatalog.LichLordId,
                name: "Bone Sentinel",
                description: "Summon a defensive skeleton swordsman that guards you and engages nearby invaders.",
                branchName: "Branch A — Undead Summoning",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 25f,
                cooldownDuration: 8f,
                range: 8f,
                effects: AbilityEffectType.Summon,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 14f,
                baseRadius: 1f,
                duration: 20f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown reduced by 15%", cooldownMultiplier: 0.85f),
                    new AbilityRankMilestone(10, "Soul Essence cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(15, "Sentinel speed and armor +25%", bonusUtility: 0.25f),
                    new AbilityRankMilestone(20, "Mastery: Summon an additional Bone Sentinel on cast (+1 summon)", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_02",
                characterId: CharacterCatalog.LichLordId,
                name: "Crypt Archer",
                description: "Summon an undead ranged archer that lofts piercing bone arrows at distant targets.",
                branchName: "Branch A — Undead Summoning",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_01",
                resourceCost: 25f,
                cooldownDuration: 10f,
                range: 9f,
                effects: AbilityEffectType.Summon | AbilityEffectType.Projectile,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 12f,
                baseRadius: 1f,
                duration: 18f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown reduced by 15%", cooldownMultiplier: 0.85f),
                    new AbilityRankMilestone(10, "Archer arrow damage +25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(15, "Arrows pierce 1 additional enemy", extraSummonsOrProjectiles: 1),
                    new AbilityRankMilestone(20, "Mastery: Summon 2 Crypt Archers simultaneously", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_03",
                characterId: CharacterCatalog.LichLordId,
                name: "Grave Spearman",
                description: "Summon a piercing melee defender that impales invaders in a line and knocks them back.",
                branchName: "Branch A — Undead Summoning",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_01",
                resourceCost: 30f,
                cooldownDuration: 9f,
                range: 8f,
                effects: AbilityEffectType.Summon | AbilityEffectType.MeleeArc,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 18f,
                baseRadius: 1.5f,
                duration: 20f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Spear thrust reach +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Spear thrust damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Thrust knockback force doubled", bonusUtility: 1.0f),
                    new AbilityRankMilestone(20, "Mastery: Spearman gains sweeping 180-degree counter-strike", damageMultiplier: 1.50f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_04",
                characterId: CharacterCatalog.LichLordId,
                name: "Grave Hound",
                description: "Summon a rapid undead hunter that sprints swiftly toward distant threats and tears into invaders.",
                branchName: "Branch A — Undead Summoning",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_03",
                resourceCost: 30f,
                cooldownDuration: 7f,
                range: 9f,
                effects: AbilityEffectType.Summon,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 10f,
                baseRadius: 0.8f,
                duration: 18f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Hound movement speed +25%", bonusUtility: 0.25f),
                    new AbilityRankMilestone(10, "Bite attack damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Pounce slows targets by 40% for 2s", bonusUtility: 0.40f),
                    new AbilityRankMilestone(20, "Mastery: Spawns a pack of 2 Grave Hounds (+1 summon)", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_05",
                characterId: CharacterCatalog.LichLordId,
                name: "Bone Mage",
                description: "Summon an elemental skeleton caster that casts concentrated bolts of dark elemental magic.",
                branchName: "Branch A — Undead Summoning",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_02",
                resourceCost: 35f,
                cooldownDuration: 10f,
                range: 10f,
                effects: AbilityEffectType.Summon | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 16f,
                baseRadius: 1.5f,
                duration: 20f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Spell cast cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Spell damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Spells burst in 2.5m AoE on impact", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Bone Mage casts dual elemental bolts (frost + fire)", damageMultiplier: 1.50f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_06",
                characterId: CharacterCatalog.LichLordId,
                name: "Ossuary Guardian",
                description: "Summon a colossal armored skeleton defender that absorbs damage and intercepts enemy strikes.",
                branchName: "Branch A — Undead Summoning",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_03",
                resourceCost: 40f,
                cooldownDuration: 12f,
                range: 8f,
                effects: AbilityEffectType.Summon | AbilityEffectType.Shield,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 22f,
                baseRadius: 1.2f,
                duration: 24f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Guardian armor +4 (12 total armor)", bonusUtility: 4f),
                    new AbilityRankMilestone(10, "Guardian Max HP +40% (140 HP)", bonusUtility: 40f),
                    new AbilityRankMilestone(15, "Ground slam taunts and knocks back nearby enemies", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(20, "Mastery: Guardian reflects 30% of incoming damage back to attackers", bonusUtility: 0.30f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_07",
                characterId: CharacterCatalog.LichLordId,
                name: "Wight Captain",
                description: "Summon a dread undead commander whose authoritative presence buffs all nearby allied summons.",
                branchName: "Branch A — Undead Summoning",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_05",
                resourceCost: 45f,
                cooldownDuration: 14f,
                range: 8f,
                effects: AbilityEffectType.Summon,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 20f,
                baseRadius: 4.5f,
                duration: 25f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Commander aura radius +50%", radiusMultiplier: 1.50f),
                    new AbilityRankMilestone(10, "Aura damage buff increased to +50%", damageMultiplier: 1.50f),
                    new AbilityRankMilestone(15, "Aura grants 10% lifesteal to summons", bonusUtility: 0.10f),
                    new AbilityRankMilestone(20, "Mastery: Captain summons 2 skeleton honor guards on arrival", extraSummonsOrProjectiles: 2)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_08",
                characterId: CharacterCatalog.LichLordId,
                name: "Corpse Collector",
                description: "Summon a scavenger minion that gathers soul fragments and channels extra Soul Essence directly to the Lich.",
                branchName: "Branch A — Undead Summoning",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_04",
                resourceCost: 30f,
                cooldownDuration: 11f,
                range: 8f,
                effects: AbilityEffectType.Summon,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 8f,
                baseRadius: 5f,
                duration: 25f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Collector movement speed +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Soul harvest bonus increased to +8 Essence", bonusUtility: 3f),
                    new AbilityRankMilestone(15, "Harvest magnet radius increased to 6m", radiusMultiplier: 1.50f),
                    new AbilityRankMilestone(20, "Mastery: Collector detonates corpses when full, dealing 30 damage", damageMultiplier: 1.40f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_09",
                characterId: CharacterCatalog.LichLordId,
                name: "Dread Abomination",
                description: "Summon a massive stitched monstrosity that crushes enemies with devastating area slams.",
                branchName: "Branch A — Undead Summoning",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_06",
                resourceCost: 55f,
                cooldownDuration: 18f,
                range: 8f,
                effects: AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 35f,
                baseRadius: 3.5f,
                duration: 28f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Smash radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Abomination Max HP +50% (240 HP)", bonusUtility: 80f),
                    new AbilityRankMilestone(15, "Smash damage +40% (49 damage)", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(20, "Mastery: Abomination unleashes toxic rot cloud dealing 20 DoT", damageMultiplier: 1.50f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_10",
                characterId: CharacterCatalog.LichLordId,
                name: "Skeletal Dragon",
                description: "Summon a colossal undead dragon that glides above the arena and blankets invaders in dark fire.",
                branchName: "Branch A — Undead Summoning",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_07",
                resourceCost: 70f,
                cooldownDuration: 22f,
                range: 9f,
                effects: AbilityEffectType.Summon | AbilityEffectType.MeleeArc,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 45f,
                baseRadius: 5f,
                duration: 30f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Dragon breath cone range +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Dragon breath damage +35% (60 damage)", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Skeletal Dragon unleashes dual necrotic strafing runs", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_11",
                characterId: CharacterCatalog.LichLordId,
                name: "Grave Frost",
                description: "Release an expanding ring of glacial frost, dealing damage and slowing all nearby invaders.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 15f,
                cooldownDuration: 5f,
                range: 4.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 22f,
                baseRadius: 4.5f,
                duration: 3f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Blast radius +20%", radiusMultiplier: 1.20f),
                    new AbilityRankMilestone(10, "Frost damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Chill duration increased by 50%", bonusUtility: 0.50f),
                    new AbilityRankMilestone(20, "Mastery: Grave Frost triggers a secondary shatter explosion after 1s", damageMultiplier: 1.50f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_12",
                characterId: CharacterCatalog.LichLordId,
                name: "Permafrost Sigil",
                description: "Etch a persistent freezing rune on the ground that deals continuous damage and chills passing invaders.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_11",
                resourceCost: 25f,
                cooldownDuration: 8f,
                range: 7f,
                effects: AbilityEffectType.Hazard | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 12f,
                baseRadius: 3f,
                duration: 6f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Sigil duration +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Sigil frost damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Sigil radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Enemies inside sigil take +30% damage from all attacks", damageMultiplier: 1.30f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_13",
                characterId: CharacterCatalog.LichLordId,
                name: "Chill Lance",
                description: "Hurl a high-velocity piercing ice spear that impales and chills multiple invaders in a line.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_11",
                resourceCost: 20f,
                cooldownDuration: 4f,
                range: 14f,
                effects: AbilityEffectType.Projectile | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 32f,
                baseRadius: 0.25f,
                duration: 1.2f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Spear projectile speed +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Spear damage +30% (41.6 dmg)", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Pierces 2 additional invaders", extraSummonsOrProjectiles: 2),
                    new AbilityRankMilestone(20, "Mastery: Cooldown reduced by 25%", cooldownMultiplier: 0.75f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_14",
                characterId: CharacterCatalog.LichLordId,
                name: "Wailing Blizzard",
                description: "Conjure a moving howling blizzard storm that sweeps forward, freezing and shredding invaders.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_12",
                resourceCost: 35f,
                cooldownDuration: 9f,
                range: 8f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 18f,
                baseRadius: 3.5f,
                duration: 5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Blizzard travel distance +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Blizzard tick damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Blizzard radius +30%", radiusMultiplier: 1.30f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_15",
                characterId: CharacterCatalog.LichLordId,
                name: "Glacial Tomb",
                description: "Entomb the nearest targeted invader in solid ice, stunning them and dealing area damage.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_13",
                resourceCost: 30f,
                cooldownDuration: 8f,
                range: 8f,
                effects: AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 28f,
                baseRadius: 2f,
                duration: 2.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Freeze duration +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Impact damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cast range +35%", radiusMultiplier: 1.35f),
                    new AbilityRankMilestone(20, "Mastery: Ice shatter damage doubled", damageMultiplier: 1.50f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_16",
                characterId: CharacterCatalog.LichLordId,
                name: "Shiver Pulse",
                description: "Unleash a sudden shockwave of absolute zero, violently knocking back and chilling surrounding invaders.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_14",
                resourceCost: 25f,
                cooldownDuration: 6f,
                range: 6f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 26f,
                baseRadius: 6f,
                duration: 3f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Knockback force doubled", bonusUtility: 1.0f),
                    new AbilityRankMilestone(10, "Pulse damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Pulse radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Grants frost barrier absorbing 30 damage", bonusUtility: 30f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_17",
                characterId: CharacterCatalog.LichLordId,
                name: "Rime Chain",
                description: "Cast arcing bands of frost that tether and jump between up to 4 nearby invaders, dealing damage and slowing.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_15",
                resourceCost: 35f,
                cooldownDuration: 7f,
                range: 10f,
                effects: AbilityEffectType.Projectile | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 24f,
                baseRadius: 4f,
                duration: 2f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Jump range between targets +35%", radiusMultiplier: 1.35f),
                    new AbilityRankMilestone(10, "Chain damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Chains to 2 additional targets", extraSummonsOrProjectiles: 2),
                    new AbilityRankMilestone(20, "Mastery: Cooldown reduced by 25%", cooldownMultiplier: 0.75f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_18",
                characterId: CharacterCatalog.LichLordId,
                name: "Frozen Lantern",
                description: "Summon an orbiting frost lantern that shields the Lich Lord and fires frost bolts at nearby invaders.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_14",
                resourceCost: 30f,
                cooldownDuration: 12f,
                range: 9f,
                effects: AbilityEffectType.Summon | AbilityEffectType.Shield | AbilityEffectType.Projectile,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 14f,
                baseRadius: 1.6f,
                duration: 15f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Lantern firing rate +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Lantern bolt damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Duration extended by 5 seconds", bonusUtility: 5f),
                    new AbilityRankMilestone(20, "Mastery: Summon 2 orbiting lanterns simultaneously", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_19",
                characterId: CharacterCatalog.LichLordId,
                name: "Hail Crown",
                description: "Call down a barrage of jagged hail and ice spikes in a wide area around the Lich Lord.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_16",
                resourceCost: 50f,
                cooldownDuration: 14f,
                range: 6f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 40f,
                baseRadius: 6f,
                duration: 4f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Storm radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Hail impact damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Hail strikes leave freezing ground patches", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_20",
                characterId: CharacterCatalog.LichLordId,
                name: "Winter of the Dead",
                description: "Engulf the entire battlefield in an ancient sub-zero freeze, slowing invaders and empowering all player summons.",
                branchName: "Branch B — Frost and Death Magic",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_18",
                resourceCost: 65f,
                cooldownDuration: 20f,
                range: 12f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Summon,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 50f,
                baseRadius: 12f,
                duration: 10f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Battlefield chill slow increased to 75%", bonusUtility: 0.25f),
                    new AbilityRankMilestone(10, "Frost blast damage +40% (70 dmg)", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Duration extended to 15 seconds", bonusUtility: 5f),
                    new AbilityRankMilestone(20, "Mastery: Cooldown reduced by 25%", cooldownMultiplier: 0.75f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_21",
                characterId: CharacterCatalog.LichLordId,
                name: "Witchfire Orb",
                description: "Hurl a crackling orb of green witchfire that detonates on contact, scorching nearby invaders.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 15f,
                cooldownDuration: 2.2f,
                range: 8.5f,
                effects: AbilityEffectType.Projectile | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 22f,
                baseRadius: 1.5f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Orb velocity +35%", bonusUtility: 0.35f),
                    new AbilityRankMilestone(10, "Impact blast radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Witchfire burn damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(20, "Mastery: Launches 2 parallel Witchfire Orbs", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_22",
                characterId: CharacterCatalog.LichLordId,
                name: "Graveflame Pillar",
                description: "Erupt a column of violent dark flames at the target location, dealing heavy damage and leaving a burning ground zone.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_21",
                resourceCost: 24f,
                cooldownDuration: 4.5f,
                range: 7.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 38f,
                baseRadius: 2.2f,
                duration: 2.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Pillar eruption damage +25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Eruption radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Leaves intense burning ground patch", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_23",
                characterId: CharacterCatalog.LichLordId,
                name: "Ashen Gust",
                description: "Unleash a fierce cone of ash and wind, dealing damage and violently repelling advancing invaders.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_21",
                resourceCost: 18f,
                cooldownDuration: 4.0f,
                range: 5.5f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 20f,
                baseRadius: 5.5f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Knockback distance +50%", bonusUtility: 0.50f),
                    new AbilityRankMilestone(10, "Gust reach +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Gust damages and disorients targets", damageMultiplier: 1.30f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_24",
                characterId: CharacterCatalog.LichLordId,
                name: "Static Cadaver",
                description: "Electrify a targeted area or fallen remnant into a high-voltage trap that repeatedly zaps passing invaders.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_22",
                resourceCost: 26f,
                cooldownDuration: 6.0f,
                range: 6.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 32f,
                baseRadius: 2.8f,
                duration: 4.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Discharge rate +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Static shock damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Trap radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Discharges a massive lightning burst upon expiry", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_25",
                characterId: CharacterCatalog.LichLordId,
                name: "Deathbolt",
                description: "Fire a concentrated bolt of lethal lightning that pierces through multiple enemies in a straight line.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_23",
                resourceCost: 25f,
                cooldownDuration: 3.5f,
                range: 10.0f,
                effects: AbilityEffectType.Projectile | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 45f,
                baseRadius: 0.35f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Pierce up to 3 targets", bonusUtility: 3f),
                    new AbilityRankMilestone(10, "Deathbolt damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Generates lightning splash on every pierced target", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_26",
                characterId: CharacterCatalog.LichLordId,
                name: "Corpse Spark",
                description: "Cast an arcing spark of electrical necrosis that jumps rapidly between up to 4 nearby invaders.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_24",
                resourceCost: 30f,
                cooldownDuration: 7.0f,
                range: 8.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Projectile,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 34f,
                baseRadius: 1.5f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Jump chain jumps +1 (5 targets)", extraSummonsOrProjectiles: 1),
                    new AbilityRankMilestone(10, "Electrical arc damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Stuns struck invaders for 1 second", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_27",
                characterId: CharacterCatalog.LichLordId,
                name: "Storm of Ash",
                description: "Conjure a turbulent burning windstorm that engulfs invaders in swirling embers and damaging heat.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_23",
                resourceCost: 36f,
                cooldownDuration: 8.5f,
                range: 6.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 40f,
                baseRadius: 3.5f,
                duration: 5.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Ash cloud velocity +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Continuous burn damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Storm radius enlarged +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Blinds and slows invaders inside the ash storm", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_28",
                characterId: CharacterCatalog.LichLordId,
                name: "Phantom Cyclone",
                description: "Unleash a spirit-filled tornado that drags nearby invaders toward its swirling core while ripping flesh.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_25",
                resourceCost: 38f,
                cooldownDuration: 10.0f,
                range: 7.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Gravity | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 42f,
                baseRadius: 3.2f,
                duration: 4.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Vortex pull force +40%", bonusUtility: 0.40f),
                    new AbilityRankMilestone(10, "Cyclone shred damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Duration extended by 1.5 seconds", bonusUtility: 1.5f),
                    new AbilityRankMilestone(20, "Mastery: Cyclone launches phantom blades outward", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_29",
                characterId: CharacterCatalog.LichLordId,
                name: "Ember Ossuary",
                description: "Form an explosive circular barrier of blazing bone runes that detonate when invaders breach them.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_27",
                resourceCost: 52f,
                cooldownDuration: 13.0f,
                range: 6.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 55f,
                baseRadius: 4.5f,
                duration: 3.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Rune ring diameter +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Bone detonation damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Detonations ignite ground for 4 seconds", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_30",
                characterId: CharacterCatalog.LichLordId,
                name: "Thunder Coffin",
                description: "Trap an enemy commander in an electric lightning prison before detonating in a catastrophic thunderclap.",
                branchName: "Branch C — Fire, Wind and Thunder Sorcery",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_28",
                resourceCost: 65f,
                cooldownDuration: 16.0f,
                range: 8.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 70f,
                baseRadius: 3.5f,
                duration: 2.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cage immobilization duration +1.0s", bonusUtility: 1.0f),
                    new AbilityRankMilestone(10, "Thunderclap detonation damage +40% (98 dmg)", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Thunderclap paralyzes all invaders within 6m", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_31",
                characterId: CharacterCatalog.LichLordId,
                name: "Soul Siphon",
                description: "Tear vital soul essence from nearby invaders, dealing necrotic damage and siphoning health back to the Lich Lord.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 16f,
                cooldownDuration: 3.0f,
                range: 6.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Heal,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 22f,
                baseRadius: 2.5f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Life siphon healing ratio +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Siphon damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Restores 15 Soul Essence on hit", bonusUtility: 15f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_32",
                characterId: CharacterCatalog.LichLordId,
                name: "Hex of Frailty",
                description: "Cast an ancient enervating hex over an area, dealing damage and crippling enemy defenses for 4 seconds.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_31",
                resourceCost: 20f,
                cooldownDuration: 4.5f,
                range: 7.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 24f,
                baseRadius: 3.0f,
                duration: 4.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Hex vulnerability duration +2s", bonusUtility: 2f),
                    new AbilityRankMilestone(10, "Initial hex damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Hex area radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Cursed enemies take 30% increased damage from all sources", bonusUtility: 0.30f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_33",
                characterId: CharacterCatalog.LichLordId,
                name: "Withering Field",
                description: "Infect the battlefield ground with a decaying necrotic zone that continuously withers and saps invader vitality.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_31",
                resourceCost: 24f,
                cooldownDuration: 5.0f,
                range: 6.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 30f,
                baseRadius: 3.0f,
                duration: 4.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Decay tick frequency +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Withering damage per tick +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Enemies in field are slowed by 40%", bonusUtility: 0.40f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_34",
                characterId: CharacterCatalog.LichLordId,
                name: "Black Sun",
                description: "Summon a dense sphere of gravitational darkness that drags approaching invaders inward while crushing them.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_33",
                resourceCost: 30f,
                cooldownDuration: 7.0f,
                range: 6.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Gravity | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 38f,
                baseRadius: 3.5f,
                duration: 4.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Singularity gravitational pull +40%", bonusUtility: 0.40f),
                    new AbilityRankMilestone(10, "Orb crush damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Orb expansion radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Black Sun implodes in a massive void shockwave", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_35",
                characterId: CharacterCatalog.LichLordId,
                name: "Spirit Leech",
                description: "Launch a parasitic shadowy wraith that latches onto the target, continuously draining health back to the Lich.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_32",
                resourceCost: 25f,
                cooldownDuration: 5.5f,
                range: 8.0f,
                effects: AbilityEffectType.Projectile | AbilityEffectType.Heal,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 35f,
                baseRadius: 0.3f,
                duration: 3.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Leech healing ratio +35%", bonusUtility: 0.35f),
                    new AbilityRankMilestone(10, "Spirit drain damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Spawns 2 leeching spirits simultaneously", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_36",
                characterId: CharacterCatalog.LichLordId,
                name: "Doom Bind",
                description: "Burst shadowy tendrils from the earth, dealing crushing damage and locking all enemies in place with root bonds.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_35",
                resourceCost: 32f,
                cooldownDuration: 7.5f,
                range: 7.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 36f,
                baseRadius: 3.2f,
                duration: 3.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Root bind duration increased to 4.0s", bonusUtility: 1.0f),
                    new AbilityRankMilestone(10, "Shadow crush damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Bind area +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Bound invaders are disarmed and cannot strike for 2s", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_37",
                characterId: CharacterCatalog.LichLordId,
                name: "Ebon Grasp",
                description: "Summon clawing skeletal limbs from subterranean graves, tearing through invaders and heavily slowing movement.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_33",
                resourceCost: 34f,
                cooldownDuration: 8.0f,
                range: 6.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 40f,
                baseRadius: 3.5f,
                duration: 4.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Skeletal hand snare slow increased to 70%", bonusUtility: 0.20f),
                    new AbilityRankMilestone(10, "Clawing grasp damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Grasp area +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Slain enemies in area spawn temporary bone sentinels", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_38",
                characterId: CharacterCatalog.LichLordId,
                name: "Curse Spread",
                description: "Transmit a virulent wave of darkness that damages a target and leaps to infect neighboring enemies with active curses.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_32",
                resourceCost: 30f,
                cooldownDuration: 6.5f,
                range: 8.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Projectile,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 32f,
                baseRadius: 2.0f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Contagion jump chain targets +1 (5 targets)", extraSummonsOrProjectiles: 1),
                    new AbilityRankMilestone(10, "Curse damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Spreads all active debuffs to nearby targets", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_39",
                characterId: CharacterCatalog.LichLordId,
                name: "Death Sentence",
                description: "Brand a powerful invader with an ominous execution glyph, delivering massive necrotic damage when the timer expires.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_36",
                resourceCost: 48f,
                cooldownDuration: 12.0f,
                range: 8.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 65f,
                baseRadius: 2.5f,
                duration: 2.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Sentence mark damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Execution deals double damage below 50% HP", bonusUtility: 1.0f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Slain target transfers Death Sentence to nearest survivor", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_40",
                characterId: CharacterCatalog.LichLordId,
                name: "Umbral Nova",
                description: "Release an apocalyptic shockwave of pure void darkness across the arena, obliterating invaders in a massive blast radius.",
                branchName: "Branch D — Forbidden Dark Magic",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_34",
                resourceCost: 60f,
                cooldownDuration: 15.0f,
                range: 10.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 75f,
                baseRadius: 8.0f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Nova radius +25% (10m area)", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Cataclysmic void damage +40% (105 dmg)", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Slain enemies restore 10 HP and 10 Soul Essence each", bonusUtility: 10f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_41",
                characterId: CharacterCatalog.LichLordId,
                name: "Reanimate Fallen",
                description: "Perform an unholy ritual raising fallen foes as temporary skeletal revenants to fight at the Lich Lord's side.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 20f,
                cooldownDuration: 4.5f,
                range: 5.0f,
                effects: AbilityEffectType.Summon | AbilityEffectType.Heal,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 20f,
                baseRadius: 1.5f,
                duration: 8.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Reanimated summon attack damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Revenant lifespan extended by 4 seconds", bonusUtility: 4.0f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Reanimates 2 skeletal revenants simultaneously", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_42",
                characterId: CharacterCatalog.LichLordId,
                name: "Tomb Relay",
                description: "Instantly teleport all active undead minions to the target location with a violent necrotic impact shockwave.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_41",
                resourceCost: 18f,
                cooldownDuration: 5.0f,
                range: 8.0f,
                effects: AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 25f,
                baseRadius: 2.5f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Relay arrival blast radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Arrival shockwave damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Relayed minions gain 100 bonus armor and +50% haste for 5s", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_43",
                characterId: CharacterCatalog.LichLordId,
                name: "Legion Formation",
                description: "Command all active undead into an aggressive combat formation, granting massive attack speed and armor for 6 seconds.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 2,
                prerequisiteAbilityId: "lich_lord_active_41",
                resourceCost: 22f,
                cooldownDuration: 7.0f,
                range: 6.0f,
                effects: AbilityEffectType.Heal,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 0f,
                baseRadius: 6.0f,
                duration: 6.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Legion formation duration +3s (9s total)", bonusUtility: 3.0f),
                    new AbilityRankMilestone(10, "Minion haste bonus increased to +60%", bonusUtility: 0.60f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Undead minions heal 5 HP per second while formation is active", bonusUtility: 5.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_44",
                characterId: CharacterCatalog.LichLordId,
                name: "Soul Furnace",
                description: "Convert stored Soul Essence into a volcanic eruption of necrotic flame, incinerating all nearby invaders.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_43",
                resourceCost: 35f,
                cooldownDuration: 6.5f,
                range: 7.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 45f,
                baseRadius: 3.5f,
                duration: 3.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Furnace eruption blast radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Necrotic furnace blast damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Enemies below 25% health are incinerated instantly", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_45",
                characterId: CharacterCatalog.LichLordId,
                name: "Boneshield",
                description: "Conjure a whirling barrier of dense bone plates around the Lich Lord, granting armor and absorbing incoming damage.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_42",
                resourceCost: 28f,
                cooldownDuration: 8.0f,
                range: 0f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 18f,
                baseRadius: 2.2f,
                duration: 8.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Boneshield absorption capacity +40% (70 HP shield)", bonusUtility: 20f),
                    new AbilityRankMilestone(10, "Orbiting bone fragments damage nearby enemies +50%", damageMultiplier: 1.50f),
                    new AbilityRankMilestone(15, "Duration extended to 12 seconds", bonusUtility: 4.0f),
                    new AbilityRankMilestone(20, "Mastery: When Boneshield breaks or expires, detonates in a bone shrapnel burst", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_46",
                characterId: CharacterCatalog.LichLordId,
                name: "Corpse Explosion",
                description: "Detonate fallen remains or marked targets in the targeted area, unleashing a catastrophic concussive blast of bone and gore.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 3,
                prerequisiteAbilityId: "lich_lord_active_41",
                resourceCost: 30f,
                cooldownDuration: 5.5f,
                range: 8.5f,
                effects: AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 52f,
                baseRadius: 3.8f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Corpse blast radius +30% (4.9m blast)", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Detonation burst damage +35% (70 dmg)", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Slain enemies trigger secondary chain explosions", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_47",
                characterId: CharacterCatalog.LichLordId,
                name: "Soul Anchor",
                description: "Plant an unholy obelisk at targeted location that pulses protective wards, healing the Lich Lord and all nearby summons.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_45",
                resourceCost: 36f,
                cooldownDuration: 9.0f,
                range: 6.0f,
                effects: AbilityEffectType.Heal | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 15f,
                baseRadius: 3.2f,
                duration: 6.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Anchor pulse healing increased by +40%", bonusUtility: 0.40f),
                    new AbilityRankMilestone(10, "Anchor aura pulses slow enemy movement by 40%", bonusUtility: 0.40f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: If player takes fatal damage near anchor, consume anchor to cheat death with 40% HP", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_48",
                characterId: CharacterCatalog.LichLordId,
                name: "Death Pact",
                description: "Sacrifice active undead minions to instantly restore health and Soul Essence to the Lich Lord.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_43",
                resourceCost: 15f,
                cooldownDuration: 6.0f,
                range: 0f,
                effects: AbilityEffectType.Heal | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 30f,
                baseRadius: 3.5f,
                duration: 0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Life restored increased by +35%", bonusUtility: 0.35f),
                    new AbilityRankMilestone(10, "Restores 35 Soul Essence upon sacrifice", bonusUtility: 35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Sacrificing summons creates a radial necrotic burst dealing 60 damage", damageMultiplier: 2.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_49",
                characterId: CharacterCatalog.LichLordId,
                name: "Necropolis Gate",
                description: "Tear open a portal to the underworld, periodically summoning undead reinforcements over 6 seconds.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 4,
                prerequisiteAbilityId: "lich_lord_active_44",
                resourceCost: 45f,
                cooldownDuration: 11.0f,
                range: 6.5f,
                effects: AbilityEffectType.Summon | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 22f,
                baseRadius: 3.0f,
                duration: 6.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Gate portal spawns +1 additional sentinel", extraSummonsOrProjectiles: 1),
                    new AbilityRankMilestone(10, "Portal necrotic ground aura deals +40% damage", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Gate duration extended to 8 seconds", bonusUtility: 2.0f),
                    new AbilityRankMilestone(20, "Mastery: Final gate surge summons a Wight Captain before collapsing", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_active_50",
                characterId: CharacterCatalog.LichLordId,
                name: "Crown of Silence",
                description: "Unleash a deafening necrotic silence across a 7.5m radius, interrupting and silencing all enemies for 3.5 seconds.",
                branchName: "Branch E — Necromantic Rituals",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_47",
                resourceCost: 55f,
                cooldownDuration: 14.0f,
                range: 8.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 68f,
                baseRadius: 7.5f,
                duration: 3.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Crown radius increased to 9.5m area", radiusMultiplier: 1.27f),
                    new AbilityRankMilestone(10, "Crown psychic burst damage +40% (95 dmg)", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Silenced enemies suffer 40% increased damage from all sources", bonusUtility: 0.40f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_ultimate_50",
                characterId: CharacterCatalog.LichLordId,
                name: "Army of the Damned",
                description: "Summon a temporary army of skeleton defenders accompanied by a catastrophic necrotic blast.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_active_02",
                resourceCost: 60f,
                cooldownDuration: 25f,
                range: 10f,
                effects: AbilityEffectType.Summon | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 60f,
                baseRadius: 6f,
                duration: 25f,
                isUltimate: true,
                requiredLevel: 50,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Necrotic blast damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Blast radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(20, "Mastery: Army summon count doubled (+2 sentinels)", extraSummonsOrProjectiles: 2)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_ultimate_100",
                characterId: CharacterCatalog.LichLordId,
                name: "Ossuary Dragon",
                description: "Summon a colossal ancient undead dragon that swoops across the arena bathed in dark flames.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_ultimate_50",
                resourceCost: 75f,
                cooldownDuration: 30f,
                range: 10f,
                effects: AbilityEffectType.Summon | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 80f,
                baseRadius: 6.5f,
                duration: 30f,
                isUltimate: true,
                requiredLevel: 100,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown reduced by 15%", cooldownMultiplier: 0.85f),
                    new AbilityRankMilestone(10, "Dragon flame damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Dragon breath radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(20, "Mastery: Summon an additional Frost Drake companion", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_ultimate_150",
                characterId: CharacterCatalog.LichLordId,
                name: "Eternal Winter Court",
                description: "Unleash an absolute glacial domain that deep-freezes invaders and summons 2 frost guardians.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_ultimate_100",
                resourceCost: 75f,
                cooldownDuration: 30f,
                range: 10f,
                effects: AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 70f,
                baseRadius: 7.5f,
                duration: 25f,
                isUltimate: true,
                requiredLevel: 150,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Freeze slow intensity +20%", bonusUtility: 0.20f),
                    new AbilityRankMilestone(10, "Glacial burst damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Glacial radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Frost Guardians gain frost-nova counter attack on death", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_ultimate_200",
                characterId: CharacterCatalog.LichLordId,
                name: "Black Star Eclipse",
                description: "Conjure an apocalyptic dying dark star that bombards the arena and harvests 40 Soul Essence.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_ultimate_150",
                resourceCost: 80f,
                cooldownDuration: 32f,
                range: 10f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 110f,
                baseRadius: 8.5f,
                duration: 25f,
                isUltimate: true,
                requiredLevel: 200,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Dark star impact damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Harvested Soul Essence doubled (+80 Essence)", bonusUtility: 40f),
                    new AbilityRankMilestone(20, "Mastery: Eclipse inflicts continuous gravitational collapse dragging invaders to center", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "lich_lord_ultimate_250",
                characterId: CharacterCatalog.LichLordId,
                name: "World Grave Awakening",
                description: "Awaken all dead across the arena in a necrotic apocalypse, reviving 4 sentinels, 2 archers, and 1 wight captain.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "lich_lord_ultimate_200",
                resourceCost: 90f,
                cooldownDuration: 35f,
                range: 12f,
                effects: AbilityEffectType.Summon | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 90f,
                baseRadius: 9f,
                duration: 35f,
                isUltimate: true,
                requiredLevel: 250,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Necrotic shockwave damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Summoned minion duration increased by 50%", bonusUtility: 0.50f),
                    new AbilityRankMilestone(20, "Mastery: World Grave also summons an ancient Dread Abomination", extraSummonsOrProjectiles: 1)
                }
            ));

            // -------------------------------------------------------------
            // DEMON MONARCH
            // -------------------------------------------------------------
            Register(new AbilityDefinition(
                id: "demon_monarch_active_22",
                characterId: CharacterCatalog.DemonMonarchId,
                name: "Hellstep",
                description: "Teleport swiftly across the battlefield, unleashing infernal explosions at departure and arrival.",
                branchName: "Branch C — Demonic Mobility",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 25f,
                cooldownDuration: 4f,
                range: 5f,
                effects: AbilityEffectType.Dash | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 30f,
                baseRadius: 3f,
                duration: 0.3f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Fury cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(15, "Explosion radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Hellstep deals 50% more damage and leaves an incinerating trail", damageMultiplier: 1.50f)
                }
            ));
            // Alias for active_01
            Register(new AbilityDefinition(
                id: "demon_monarch_active_01",
                characterId: CharacterCatalog.DemonMonarchId,
                name: "Hellstep",
                description: "Teleport swiftly across the battlefield, unleashing infernal explosions at departure and arrival.",
                branchName: "Branch C — Demonic Mobility",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 25f,
                cooldownDuration: 4f,
                range: 5f,
                effects: AbilityEffectType.Dash | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 30f,
                baseRadius: 3f,
                duration: 0.3f
            ));

            Register(new AbilityDefinition(
                id: "demon_monarch_active_02",
                characterId: CharacterCatalog.DemonMonarchId,
                name: "Infernal Cleave",
                description: "Unleash a devastating 140-degree flaming sword slash, sundering enemies and inflicting burn.",
                branchName: "Branch A — Infernal Swordsmanship",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 15f,
                cooldownDuration: 2.5f,
                range: 3.2f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 35f,
                baseRadius: 3.2f,
                duration: 0.2f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Slash damage +25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(15, "Cleave reach +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Cleave unleashes an infernal flame wave erupting forward", damageMultiplier: 1.40f)
                }
            ));

            Register(new AbilityDefinition(
                id: "demon_monarch_active_11",
                characterId: CharacterCatalog.DemonMonarchId,
                name: "Blazing Eruption",
                description: "Ignite the ground beneath foes, creating a fiery hazard that burns all targets inside it.",
                branchName: "Branch B — Fire Sorcery",
                tier: 2,
                prerequisiteAbilityId: "demon_monarch_active_02",
                resourceCost: 30f,
                cooldownDuration: 6f,
                range: 5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AreaCursorOrAim,
                baseDamage: 25f,
                baseRadius: 4.5f,
                duration: 4f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Hazard radius +20%", radiusMultiplier: 1.20f),
                    new AbilityRankMilestone(10, "Hazard damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Hazard duration +2 seconds", bonusUtility: 2f),
                    new AbilityRankMilestone(20, "Mastery: Eruption drags enemies toward the vortex center", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "demon_monarch_ultimate_50",
                characterId: CharacterCatalog.DemonMonarchId,
                name: "Hellblade Judgment",
                description: "Summon a colossal flaming sword that crashes into the arena with apocalyptic devastation.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "demon_monarch_active_01",
                resourceCost: 70f,
                cooldownDuration: 25f,
                range: 8f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 80f,
                baseRadius: 7f,
                duration: 0.5f,
                isUltimate: true,
                requiredLevel: 50,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Impact damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Fury cost -25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: The judgment blade creates a secondary volcanic aftershock", damageMultiplier: 1.50f)
                }
            ));

            // -------------------------------------------------------------
            // VAMPIRE QUEEN
            // -------------------------------------------------------------
            Register(new AbilityDefinition(
                id: "vampire_queen_active_02",
                characterId: CharacterCatalog.VampireQueenId,
                name: "Sanguine Burst",
                description: "Detonate a crimson blood nova that damages all nearby enemies and siphons health back to the Queen.",
                branchName: "Branch A — Blood Sorcery",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 25f,
                cooldownDuration: 5f,
                range: 5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Heal,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 28f,
                baseRadius: 5f,
                duration: 0.35f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -15%", cooldownMultiplier: 0.85f),
                    new AbilityRankMilestone(10, "Lifesteal siphon increased from 35% to 50%", bonusUtility: 0.15f),
                    new AbilityRankMilestone(15, "Nova damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(20, "Mastery: Burst radius +40% and heals for 70% of damage dealt", radiusMultiplier: 1.40f)
                }
            ));

            // Alias for active_01
            Register(new AbilityDefinition(
                id: "vampire_queen_active_01",
                characterId: CharacterCatalog.VampireQueenId,
                name: "Sanguine Burst",
                description: "Detonate a crimson blood nova that damages all nearby enemies and siphons health back to the Queen.",
                branchName: "Branch A — Blood Sorcery",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 25f,
                cooldownDuration: 5f,
                range: 5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Heal,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 28f,
                baseRadius: 5f,
                duration: 0.35f
            ));

            Register(new AbilityDefinition(
                id: "vampire_queen_active_needle",
                characterId: CharacterCatalog.VampireQueenId,
                name: "Crimson Needle",
                description: "Cast rapid piercing crystal blood needles that tear through lines of invaders.",
                branchName: "Branch B — Precision & Rapier",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 15f,
                cooldownDuration: 3f,
                range: 10f,
                effects: AbilityEffectType.Projectile,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 20f,
                baseRadius: 0.5f,
                duration: 1f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Fires +1 extra needle", extraSummonsOrProjectiles: 1),
                    new AbilityRankMilestone(10, "Needle damage +25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(15, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Fires a 5-needle fan tearing through all targets", extraSummonsOrProjectiles: 3)
                }
            ));

            Register(new AbilityDefinition(
                id: "vampire_queen_active_11",
                characterId: CharacterCatalog.VampireQueenId,
                name: "Bat Swarm",
                description: "Summon a ferocious swarm of spectral bats that harass enemies and hinder their movement.",
                branchName: "Branch C — Swarm & Dominance",
                tier: 2,
                prerequisiteAbilityId: "vampire_queen_active_01",
                resourceCost: 30f,
                cooldownDuration: 7f,
                range: 8f,
                effects: AbilityEffectType.Summon | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.NearestInvader,
                baseDamage: 18f,
                baseRadius: 3f,
                duration: 5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Bat swarm size +2", extraSummonsOrProjectiles: 2),
                    new AbilityRankMilestone(10, "Slow potency +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(15, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Bats return 100% of damage as Blood Essence", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "vampire_queen_ultimate_50",
                characterId: CharacterCatalog.VampireQueenId,
                name: "Crimson Masquerade",
                description: "Unleash an arena-wide vampiric siphon that drains blood from all invading heroes.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "vampire_queen_active_01",
                resourceCost: 70f,
                cooldownDuration: 25f,
                range: 12f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 60f,
                baseRadius: 8f,
                duration: 1f,
                isUltimate: true,
                requiredLevel: 50,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Siphon damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Blood Essence cost -25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Siphons 100% damage as health and resets active cooldowns", bonusUtility: 1f)
                }
            ));

            // -------------------------------------------------------------
            // SHADOW REAPER
            // -------------------------------------------------------------
            Register(new AbilityDefinition(
                id: "shadow_reaper_active_01",
                characterId: CharacterCatalog.ShadowReaperId,
                name: "Shadowstep",
                description: "Teleport behind invaders and execute a devastating 360-degree shadow whirlwind.",
                branchName: "Branch A — Umbral Mobility",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 2f,
                cooldownDuration: 4f,
                range: 6f,
                effects: AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 40f,
                baseRadius: 3.5f,
                duration: 0.3f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Whirlwind radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(15, "Shadow damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(20, "Mastery: Refunds 1 Mark on hitting 3 or more enemies", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "shadow_reaper_active_02",
                characterId: CharacterCatalog.ShadowReaperId,
                name: "Crescent Reap",
                description: "Perform a sweeping 150-degree scythe execution, dealing amplified damage against wounded foes.",
                branchName: "Branch B — Scythe Execution",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 3f,
                cooldownDuration: 3f,
                range: 2.8f,
                effects: AbilityEffectType.MeleeArc,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 45f,
                baseRadius: 2.8f,
                duration: 0.25f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Reap damage +25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Mark cost reduced to 2", resourceCostMultiplier: 0.67f),
                    new AbilityRankMilestone(15, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Slaying an enemy with Crescent Reap instantly refreshes its cooldown", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "shadow_reaper_active_11",
                characterId: CharacterCatalog.ShadowReaperId,
                name: "Umbral Shroud",
                description: "Summon a patch of suffocating shadows that cripples enemy mobility and shrouds the Reaper.",
                branchName: "Branch C — Shadow Veil",
                tier: 2,
                prerequisiteAbilityId: "shadow_reaper_active_01",
                resourceCost: 2f,
                cooldownDuration: 6f,
                range: 4f,
                effects: AbilityEffectType.CrowdControl | AbilityEffectType.Shield,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 15f,
                baseRadius: 4f,
                duration: 4f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Shroud radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Shroud duration +2 seconds", bonusUtility: 2f),
                    new AbilityRankMilestone(15, "Slow potency +25%", bonusUtility: 0.25f),
                    new AbilityRankMilestone(20, "Mastery: Shroud detonates on expiration dealing 40 shadow damage", damageMultiplier: 1.50f)
                }
            ));

            Register(new AbilityDefinition(
                id: "shadow_reaper_ultimate_50",
                characterId: CharacterCatalog.ShadowReaperId,
                name: "Dance of Ten Thousand Cuts",
                description: "Accelerate the Reaper into a catastrophic flurry of rapid, lethal scythe strikes across the arena.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "shadow_reaper_active_01",
                resourceCost: 5f,
                cooldownDuration: 20f,
                range: 8f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Dash,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 75f,
                baseRadius: 6f,
                duration: 0.6f,
                isUltimate: true,
                requiredLevel: 50,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Flurry damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Mark cost reduced to 4", resourceCostMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Spawns shadow phantoms continuing the flurry for 3 seconds", bonusUtility: 1f)
                }
            ));

            // -------------------------------------------------------------
            // CURSED GOLEM
            // -------------------------------------------------------------
            // Branch A — Colossal Physical Combat (Actives 01-10)
            Register(new AbilityDefinition(
                id: "cursed_golem_active_01",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Titan Punch",
                description: "Deliver an extremely powerful forward punch that staggers enemies and knocks them back.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 20f,
                cooldownDuration: 3.5f,
                range: 3.5f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 32f,
                baseRadius: 2.2f,
                duration: 0.3f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Punch cooldown reduced by 15%", cooldownMultiplier: 0.85f),
                    new AbilityRankMilestone(10, "Punch impact damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Titan Punch evolves into a two-stage seismic strike with a secondary shockwave", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_02",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Stonebreaker",
                description: "Deliver a heavy armor-crushing strike that shatters enemy defenses.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 1,
                prerequisiteAbilityId: "cursed_golem_active_01",
                resourceCost: 25f,
                cooldownDuration: 4.5f,
                range: 3.5f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 38f,
                baseRadius: 2.5f,
                duration: 0.4f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Stonebreaker strike damage +25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Runic Charge cost reduced by 20%", resourceCostMultiplier: 0.80f),
                    new AbilityRankMilestone(15, "Strike impact area +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(20, "Mastery: Stonebreaker shatters enemy defenses, inflicting heavy vulnerability", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_03",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Boulder Uppercut",
                description: "Launch enemies violently into the air with a rising stone fist.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_01",
                resourceCost: 25f,
                cooldownDuration: 5.0f,
                range: 3.0f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 36f,
                baseRadius: 2.0f,
                duration: 0.4f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Launch knockback force +35%", bonusUtility: 0.35f),
                    new AbilityRankMilestone(10, "Uppercut damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Impact launches rocks in all directions, dealing 25 area damage", extraSummonsOrProjectiles: 4)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_04",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Mountain Slam",
                description: "Pound the ground with titanic force, releasing a radial shockwave that staggers invaders.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_02",
                resourceCost: 30f,
                cooldownDuration: 6.0f,
                range: 4.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 45f,
                baseRadius: 4.5f,
                duration: 0.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Slam radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Slam shockwave damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Mountain Slam creates cracked earth, slowing invaders by 40%", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_05",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Granite Sweep",
                description: "Perform a wide sweeping stone-arm attack that cleaves invaders across a broad frontal arc.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_03",
                resourceCost: 25f,
                cooldownDuration: 5.0f,
                range: 4.0f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 40f,
                baseRadius: 3.8f,
                duration: 0.45f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Sweep arc width and range +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Sweep damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Knockback distance doubled", bonusUtility: 1.0f),
                    new AbilityRankMilestone(20, "Mastery: Granite Sweep strikes twice in rapid succession", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_06",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Crushing Charge",
                description: "Rush forward unstoppably through nearby enemies, trampling all in your path.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_04",
                resourceCost: 30f,
                cooldownDuration: 6.5f,
                range: 7.0f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 42f,
                baseRadius: 2.5f,
                duration: 0.4f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Charge rush distance +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Charge impact damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Grants 40 barrier while rushing and stuns impacted foes for 1.5 seconds", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_07",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Bedrock Hammer",
                description: "Transform one arm into a giant stone hammer and smash down with devastating impact.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_05",
                resourceCost: 35f,
                cooldownDuration: 7.0f,
                range: 4.0f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 52f,
                baseRadius: 3.2f,
                duration: 0.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Hammer smash damage +25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Hammer smash radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Bedrock Hammer shatters rock shards outward dealing 25 damage", extraSummonsOrProjectiles: 3)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_08",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Colossus Grip",
                description: "Grab distant enemies with ancient gravitational stone power, pulling them inward before slamming them.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_06",
                resourceCost: 35f,
                cooldownDuration: 7.5f,
                range: 6.0f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.Gravity | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 48f,
                baseRadius: 2.8f,
                duration: 0.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Grip reach +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Smash impact damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Pulls all nearby enemies into the throw impact point", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_09",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Double Fist Impact",
                description: "Strike the ground with both colossal fists simultaneously, creating an overwhelming earth tremor.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 4,
                prerequisiteAbilityId: "cursed_golem_active_07",
                resourceCost: 40f,
                cooldownDuration: 8.0f,
                range: 5.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 58f,
                baseRadius: 5.0f,
                duration: 0.6f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Double impact shockwave radius +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Impact damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Double Fist Impact triggers a delayed secondary tremor", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_10",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Worldbreaker Punch",
                description: "Release a catastrophic shockwave from an empowered stone punch that pierces through armies.",
                branchName: "Branch A — Colossal Physical Combat",
                tier: 4,
                prerequisiteAbilityId: "cursed_golem_active_08",
                resourceCost: 45f,
                cooldownDuration: 9.0f,
                range: 7.0f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 70f,
                baseRadius: 4.5f,
                duration: 0.65f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Shockwave distance +30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Punch damage +35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Massive shockwave pierces all enemies and creates a destructive shockwave line", extraSummonsOrProjectiles: 1)
                }
            ));

            // Branch B — Earth and Stone Magic (Actives 11-20)
            Register(new AbilityDefinition(
                id: "cursed_golem_active_11",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Stone Spike",
                description: "Summon sharp stone spikes bursting from beneath enemies, impaling and slowing them.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 20f,
                cooldownDuration: 4.0f,
                range: 6.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 34f,
                baseRadius: 2.2f,
                duration: 2.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Stone Spike cooldown reduced by 15%", cooldownMultiplier: 0.85f),
                    new AbilityRankMilestone(10, "Spike impale damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Spike eruption radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(20, "Mastery: Generates additional spike formations beneath hit enemies", extraSummonsOrProjectiles: 2)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_12",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Boulder Throw",
                description: "Launch a colossal rock projectile that crushes invaders in a line and knocks them back.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 1,
                prerequisiteAbilityId: "cursed_golem_active_11",
                resourceCost: 25f,
                cooldownDuration: 4.5f,
                range: 9.0f,
                effects: AbilityEffectType.Projectile | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 40f,
                baseRadius: 1.5f,
                duration: 1.2f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Boulder throw damage increased by 25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Runic Charge cost reduced by 20%", resourceCostMultiplier: 0.80f),
                    new AbilityRankMilestone(15, "Pierces 1 additional enemy and increases knockback", extraSummonsOrProjectiles: 1),
                    new AbilityRankMilestone(20, "Mastery: Boulder shatters on impact, releasing splitting rock fragments", extraSummonsOrProjectiles: 3)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_13",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rockfall",
                description: "Call down a relentless rain of heavy rocks over an area, crushing all invaders beneath.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_11",
                resourceCost: 30f,
                cooldownDuration: 6.0f,
                range: 6.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 44f,
                baseRadius: 3.5f,
                duration: 2.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Rockfall bombardment area increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Rockfall impact damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Rockfall leaves a lingering hazard zone of tumbling stone debris", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_14",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Stone Wall",
                description: "Raise an impenetrable stone bulwark granting an absorption barrier and emitting concussive shock.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_12",
                resourceCost: 25f,
                cooldownDuration: 6.5f,
                range: 3.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 20f,
                baseRadius: 3.0f,
                duration: 6.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Stone Wall barrier absorption increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(15, "Wall duration extended by 2 seconds", bonusUtility: 2.0f),
                    new AbilityRankMilestone(20, "Mastery: Stone Wall gains retaliatory rune effects, damaging attacking invaders", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_15",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Earth Pillar",
                description: "Erupt a massive stone monolith violently from the earth, launching enemies into the air.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_13",
                resourceCost: 30f,
                cooldownDuration: 5.5f,
                range: 5.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 46f,
                baseRadius: 2.5f,
                duration: 0.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Pillar stagger duration increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Pillar eruption damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Raises twin pillars, crushing enemies between them", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_16",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rolling Boulder",
                description: "Hurl an enormous rolling boulder that plows forward continuously, bowling through ranks of invaders.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_14",
                resourceCost: 35f,
                cooldownDuration: 7.0f,
                range: 10.0f,
                effects: AbilityEffectType.Projectile | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 48f,
                baseRadius: 1.8f,
                duration: 1.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Rolling boulder speed and distance increased by 30%", radiusMultiplier: 1.30f),
                    new AbilityRankMilestone(10, "Crushing impact damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Rolling boulder leaves a trail of crushing tremors", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_17",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Crystal Eruption",
                description: "Detonate sharp crystalline formations that erupt outwards in all directions, shredding invaders.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_15",
                resourceCost: 35f,
                cooldownDuration: 7.0f,
                range: 5.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Projectile,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 50f,
                baseRadius: 4.0f,
                duration: 0.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Crystal eruption radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Crystal shard damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Fires 6 piercing crystal shards outward in all directions", extraSummonsOrProjectiles: 6)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_18",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Tremor Field",
                description: "Create a persistent zone of violent seismic vibrations that repeatedly staggers and slows enemies.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_16",
                resourceCost: 40f,
                cooldownDuration: 8.0f,
                range: 5.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 52f,
                baseRadius: 5.0f,
                duration: 4.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Tremor field radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Seismic pulse damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Tremor slow intensity and duration increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(20, "Mastery: Tremors pulse continuously, pulling invaders toward epicenter", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_19",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Living Stone",
                description: "Awaken fractured stone fragments as mobile animated constructs that batter nearby enemies.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 4,
                prerequisiteAbilityId: "cursed_golem_active_17",
                resourceCost: 45f,
                cooldownDuration: 9.0f,
                range: 6.0f,
                effects: AbilityEffectType.Summon | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 25f,
                baseRadius: 2.0f,
                duration: 15.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Living Stone construct health and duration increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Construct smash damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Summons an additional stone fragment guardian (+1 summon)", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_20",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Mountain's Wrath",
                description: "Unleash the catastrophic fury of tectonic plates, causing a chain of violent ground eruptions.",
                branchName: "Branch B — Earth and Stone Magic",
                tier: 4,
                prerequisiteAbilityId: "cursed_golem_active_18",
                resourceCost: 45f,
                cooldownDuration: 10.0f,
                range: 7.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 75f,
                baseRadius: 6.0f,
                duration: 0.8f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Eruption chain radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Mountain's Wrath eruption damage increased by 35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Triple sequential eruptions tearing the surrounding earth", extraSummonsOrProjectiles: 2)
                }
            ));

            // Branch C — Ancient Runic Magic (Actives 21-30)
            Register(new AbilityDefinition(
                id: "cursed_golem_active_21",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Crushing",
                description: "Inscribe a crushing rune on your stone fists that empowers physical impacts and releases concussive bursts.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 20f,
                cooldownDuration: 4.0f,
                range: 3.5f,
                effects: AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 36f,
                baseRadius: 2.5f,
                duration: 0.4f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Rune of Crushing cooldown reduced by 15%", cooldownMultiplier: 0.85f),
                    new AbilityRankMilestone(10, "Crushing impact damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Releases a secondary crushing shockwave dealing 20 damage", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_22",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Protection",
                description: "Channel a runic barrier that absorbs incoming damage and emits a tremor shockwave upon impact.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 25f,
                cooldownDuration: 6.0f,
                range: 3.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 15f,
                baseRadius: 3.5f,
                duration: 8.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Barrier absorption +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(15, "Tremor shockwave damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(20, "Mastery: Barrier absorbs 100 HP and emits seismic shockwave on expiration", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_23",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Binding",
                description: "Inscribe an ancient binding rune that locks invaders in place with impenetrable runic chains.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_21",
                resourceCost: 25f,
                cooldownDuration: 5.0f,
                range: 5.5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 38f,
                baseRadius: 3.0f,
                duration: 2.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Immobilize duration increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Binding rune damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Binding rune shatters upon ending, damaging all anchored foes", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_24",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Reflection",
                description: "Erect an arcane runic mirror that grants an absorption barrier and reflects kinetic force back at attackers.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_22",
                resourceCost: 30f,
                cooldownDuration: 6.0f,
                range: 4.0f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 30f,
                baseRadius: 3.5f,
                duration: 6.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Reflection barrier absorption increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Retaliatory shockwave damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Reflection triggers a retaliatory concussive nova", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_25",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Detonation",
                description: "Plant a volatile runic glyph that violently detonates when invaders step near, tearing the earth.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_23",
                resourceCost: 30f,
                cooldownDuration: 5.5f,
                range: 6.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 48f,
                baseRadius: 3.5f,
                duration: 3.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Detonation radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Detonation damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 20%", resourceCostMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Detonates into 3 secondary cluster runic explosions", extraSummonsOrProjectiles: 3)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_26",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Reinforcement",
                description: "Etch protective fortification runes into stone armor, surging defense and pulsing restorative wards.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_24",
                resourceCost: 35f,
                cooldownDuration: 7.0f,
                range: 3.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.Heal | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 25f,
                baseRadius: 3.5f,
                duration: 8.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Fortification barrier absorption increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(15, "Restores 30 health upon activation", bonusUtility: 30.0f),
                    new AbilityRankMilestone(20, "Mastery: Doubled barrier capacity and continuously reflects damage", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_27",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Reversal",
                description: "Unleash an inverted gravitic rune that reverses incoming momentum, blasting invaders away.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_25",
                resourceCost: 35f,
                cooldownDuration: 7.0f,
                range: 5.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 50f,
                baseRadius: 4.5f,
                duration: 0.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Reversal knockback force doubled", bonusUtility: 1.0f),
                    new AbilityRankMilestone(10, "Reversal damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Blasts a ring of reversed kinetic projectiles outward", extraSummonsOrProjectiles: 6)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_28",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Resonance",
                description: "Link runic frequencies across the battlefield, triggering a harmonic chain reaction between invaders.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_26",
                resourceCost: 40f,
                cooldownDuration: 7.5f,
                range: 6.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 54f,
                baseRadius: 4.0f,
                duration: 0.6f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Resonance chain radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Resonance damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Chained targets explode, dealing 25 damage to adjacent enemies", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_29",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Ancient Rune Circle",
                description: "Inscribe an enormous runic circle on the floor, enhancing the golem while burning and crippling all intruders.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 4,
                prerequisiteAbilityId: "cursed_golem_active_27",
                resourceCost: 45f,
                cooldownDuration: 9.0f,
                range: 6.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 60f,
                baseRadius: 5.5f,
                duration: 5.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Rune circle radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Circle burn damage increased by 35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Grants 60 barrier upon entering and pulls invaders toward center", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_30",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Runeheart Awakening",
                description: "Awaken the primordial runic core within your chest, supercharging all runic magic and triggering catastrophic surges.",
                branchName: "Branch C — Ancient Runic Magic",
                tier: 4,
                prerequisiteAbilityId: "cursed_golem_active_28",
                resourceCost: 50f,
                cooldownDuration: 11.0f,
                range: 7.0f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 80f,
                baseRadius: 6.5f,
                duration: 6.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Awakening radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Surge damage increased by 35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Grants an additional 60 barrier", bonusUtility: 60.0f),
                    new AbilityRankMilestone(20, "Mastery: Awakened runeheart permanently radiates tectonic pulses for the duration", bonusUtility: 1.0f)
                }
            ));

            // Branch D — Fortress and Defense (Actives 31-40)
            Register(new AbilityDefinition(
                id: "cursed_golem_active_31",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Granite Skin",
                description: "Harden your exterior stone plates into unyielding granite, gaining a barrier and releasing a concussive shock.",
                branchName: "Branch D — Fortress and Defense",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 20f,
                cooldownDuration: 4.5f,
                range: 3.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 25f,
                baseRadius: 3.0f,
                duration: 5.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Granite Skin barrier increased by 25%", bonusUtility: 0.25f),
                    new AbilityRankMilestone(10, "Shock damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Barrier capacity doubled to 120", bonusUtility: 60f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_32",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Living Fortress",
                description: "Root your titanic bulk into the dungeon floor, gaining an impenetrable defense shield and staggering nearby foes.",
                branchName: "Branch D — Fortress and Defense",
                tier: 1,
                prerequisiteAbilityId: "cursed_golem_active_31",
                resourceCost: 25f,
                cooldownDuration: 5.5f,
                range: 4.0f,
                effects: AbilityEffectType.Shield | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 30f,
                baseRadius: 3.5f,
                duration: 6.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Fortress shield absorption increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Stagger damage increased by 35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 20%", resourceCostMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Emits a continuous concussive pulse every 2 seconds", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_33",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Cursed Regeneration",
                description: "Channel dark earthen energy into your stone core to rapidly repair physical integrity and erect stone barriers.",
                branchName: "Branch D — Fortress and Defense",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_31",
                resourceCost: 30f,
                cooldownDuration: 6.5f,
                range: 3.0f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 20f,
                baseRadius: 3.0f,
                duration: 5.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Health restoration increased by 35%", bonusUtility: 0.35f),
                    new AbilityRankMilestone(10, "Barrier granted increased to 50", bonusUtility: 20f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Cleanses negative status effects and restores additional health", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_34",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Obsidian Shell",
                description: "Encase your outer chassis in sharp volcanic obsidian, absorbing damage and blasting razor obsidian shards outward.",
                branchName: "Branch D — Fortress and Defense",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_32",
                resourceCost: 30f,
                cooldownDuration: 6.0f,
                range: 4.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 40f,
                baseRadius: 4.0f,
                duration: 5.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Obsidian Shell barrier capacity increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Razor shard explosion damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 20%", resourceCostMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Shards cause bleeding lacerations dealing lingering damage", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_35",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Retaliation Quake",
                description: "Store kinetic energy from incoming impacts, releasing a devastating seismic shockwave that knocks back all nearby invaders.",
                branchName: "Branch D — Fortress and Defense",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_33",
                resourceCost: 35f,
                cooldownDuration: 6.5f,
                range: 5.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 48f,
                baseRadius: 4.5f,
                duration: 0.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Retaliation shockwave radius increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Shockwave damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Double shockwave detonation with catastrophic knockback", extraSummonsOrProjectiles: 1)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_36",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Stoneheart Guard",
                description: "Surround your soulstone with impenetrable layered bedrock shields, slowing attackers and absorbing heavy damage.",
                branchName: "Branch D — Fortress and Defense",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_34",
                resourceCost: 40f,
                cooldownDuration: 7.5f,
                range: 5.0f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 42f,
                baseRadius: 4.0f,
                duration: 6.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Stoneheart barrier absorption increased by 30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Guard aura damage increased by 35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Enemies struck within the aura have movement slowed by 60%", bonusUtility: 0.60f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_37",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Immovable Titan",
                description: "Assume the posture of an ancient mountain colossus, gaining massive barrier and crushing the earth underfoot.",
                branchName: "Branch D — Fortress and Defense",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_35",
                resourceCost: 40f,
                cooldownDuration: 8.0f,
                range: 5.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 52f,
                baseRadius: 5.0f,
                duration: 6.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Titan barrier capacity increased by 35%", bonusUtility: 0.35f),
                    new AbilityRankMilestone(10, "Earth crush impact damage increased by 30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Runic Charge cost reduced by 25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Grants complete knockback immunity and barrier pulses", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_38",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Fortress Roar",
                description: "Emit a resonating seismic bellow from your stone throat, crippling all nearby invaders and shattering their armor.",
                branchName: "Branch D — Fortress and Defense",
                tier: 3,
                prerequisiteAbilityId: "cursed_golem_active_36",
                resourceCost: 35f,
                cooldownDuration: 7.0f,
                range: 6.0f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 46f,
                baseRadius: 5.5f,
                duration: 3.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Roar range increased by 25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Fortress Roar damage increased by 35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(20, "Mastery: Stuns affected invaders for 1.5 seconds", bonusUtility: 1.5f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_39",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Ancient Reassembly",
                description: "Re-knit splintered masonry and broken runes, instantly repairing health and generating a massive protective bulwark.",
                branchName: "Branch D — Fortress and Defense",
                tier: 4,
                prerequisiteAbilityId: "cursed_golem_active_37",
                resourceCost: 50f,
                cooldownDuration: 10.0f,
                range: 5.0f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 35f,
                baseRadius: 4.5f,
                duration: 7.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Health reassembly increased by 40%", bonusUtility: 0.40f),
                    new AbilityRankMilestone(10, "Bulwark barrier increased to 120", bonusUtility: 40f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Reassembly triggers a retaliatory stone shrapnel blast", bonusUtility: 1.0f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_40",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Adamantine Form",
                description: "Transmute your stone frame into unbreakable adamantine, granting supreme barrier defenses and releasing catastrophic seismic tremors.",
                branchName: "Branch D — Fortress and Defense",
                tier: 4,
                prerequisiteAbilityId: "cursed_golem_active_38",
                resourceCost: 55f,
                cooldownDuration: 12.0f,
                range: 7.0f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 85f,
                baseRadius: 6.5f,
                duration: 8.0f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Adamantine barrier capacity increased to 200", bonusUtility: 50f),
                    new AbilityRankMilestone(10, "Seismic tremor shockwave damage increased by 35%", damageMultiplier: 1.35f),
                    new AbilityRankMilestone(15, "Cooldown reduced by 25%", cooldownMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Adamantine Form continuously retaliates with crushing waves upon taking hits", bonusUtility: 1.0f)
                }
            ));

            // Milestone Ultimates (Levels 50 through 250)
            Register(new AbilityDefinition(
                id: "cursed_golem_ultimate_50",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Colossus Awakening",
                description: "Awaken the ancient colossus within, gaining an enormous barrier and triggering massive seismic eruptions.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "cursed_golem_active_01",
                resourceCost: 50f,
                cooldownDuration: 25f,
                range: 7f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 70f,
                baseRadius: 7f,
                duration: 10f,
                isUltimate: true,
                requiredLevel: 50,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Earthquake damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Runic Charge cost -25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Colossus Awakening doubles armor and reflects 50% damage back", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_ultimate_100",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Faultline Dominion",
                description: "Split the battlefield with powerful earthquakes, stone eruptions, and falling boulders.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "cursed_golem_ultimate_50",
                resourceCost: 60f,
                cooldownDuration: 30f,
                range: 8f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 85f,
                baseRadius: 8f,
                duration: 12f,
                isUltimate: true,
                requiredLevel: 100,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Faultline earthquake damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Runic Charge cost -25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Faultline leaves burning tectonic rifts stunning invaders for 2 seconds", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_ultimate_150",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Heart of the Mountain",
                description: "Enter a legendary defensive state that absorbs damage and releases stored energy through counterattacks.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "cursed_golem_ultimate_100",
                resourceCost: 60f,
                cooldownDuration: 32f,
                range: 6.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Heal,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 65f,
                baseRadius: 6.5f,
                duration: 12f,
                isUltimate: true,
                requiredLevel: 150,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Counterattack blast damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Barrier absorption increased by 50 HP", bonusUtility: 0.5f),
                    new AbilityRankMilestone(20, "Mastery: Restores 80 HP and permanently pulses tremors for the duration", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_ultimate_200",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Gravitic Cataclysm",
                description: "Create a massive gravity field that drags enemies together before detonating in a devastating explosion.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "cursed_golem_ultimate_150",
                resourceCost: 70f,
                cooldownDuration: 35f,
                range: 9f,
                effects: AbilityEffectType.Gravity | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 110f,
                baseRadius: 9f,
                duration: 8f,
                isUltimate: true,
                requiredLevel: 200,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Gravitic explosion damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Runic Charge cost -25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Gravitic pull crushes targets, dealing continuous gravitational damage", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_ultimate_250",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Worldstone Avatar",
                description: "Transform into an enormous awakened worldstone titan combining colossal physical attacks, gravity powers, and runic magic.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "cursed_golem_ultimate_200",
                resourceCost: 80f,
                cooldownDuration: 40f,
                range: 10f,
                effects: AbilityEffectType.Transform | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Gravity,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 135f,
                baseRadius: 10f,
                duration: 15f,
                isUltimate: true,
                requiredLevel: 250,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(10, "Worldstone titan damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(15, "Runic Charge cost -25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(20, "Mastery: Grants 300 barrier and repeatedly summons meteoric rockfalls", bonusUtility: 1f)
                }
            ));
        }
    }
}
