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
                id: "lich_lord_active_11",
                characterId: CharacterCatalog.LichLordId,
                name: "Frost Nova",
                description: "Release an expanding ring of glacial frost, dealing damage and slowing all nearby invaders.",
                branchName: "Branch B — Frost & Curses",
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
                    new AbilityRankMilestone(20, "Mastery: Frost Nova triggers a secondary shatter explosion after 1s", damageMultiplier: 1.50f)
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
            Register(new AbilityDefinition(
                id: "cursed_golem_active_01",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Protection",
                description: "Channel a runic barrier that absorbs incoming damage and emits a tremor shockwave upon impact.",
                branchName: "Branch A — Runic Defense",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 25f,
                cooldownDuration: 6f,
                range: 3.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 15f,
                baseRadius: 3.5f,
                duration: 8f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Barrier absorption +30%", bonusUtility: 0.30f),
                    new AbilityRankMilestone(10, "Cooldown -20%", cooldownMultiplier: 0.80f),
                    new AbilityRankMilestone(15, "Tremor shockwave damage +40%", damageMultiplier: 1.40f),
                    new AbilityRankMilestone(20, "Mastery: Barrier absorbs 100 HP and emits seismic shockwave on expiration", bonusUtility: 1f)
                }
            ));

            // Alias for active_22
            Register(new AbilityDefinition(
                id: "cursed_golem_active_22",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Rune of Protection",
                description: "Channel a runic barrier that absorbs incoming damage and emits a tremor shockwave upon impact.",
                branchName: "Branch A — Runic Defense",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 25f,
                cooldownDuration: 6f,
                range: 3.5f,
                effects: AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 15f,
                baseRadius: 3.5f,
                duration: 8f
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_02",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Boulder Toss",
                description: "Hurl a colossal boulder that crushes invaders in a line and knocks them backwards.",
                branchName: "Branch B — Earth & Stone",
                tier: 1,
                prerequisiteAbilityId: null,
                resourceCost: 20f,
                cooldownDuration: 4f,
                range: 9f,
                effects: AbilityEffectType.Projectile | AbilityEffectType.CrowdControl,
                targeting: AbilityTargetingRule.AimDirection,
                baseDamage: 35f,
                baseRadius: 1.5f,
                duration: 1.2f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Boulder damage +25%", damageMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Runic Charge cost -25%", resourceCostMultiplier: 0.75f),
                    new AbilityRankMilestone(15, "Boulder shatters into 2 fragments on hit", extraSummonsOrProjectiles: 2),
                    new AbilityRankMilestone(20, "Mastery: Shatter fragments stun hit enemies for 1.5 seconds", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_active_11",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Seismic Slam",
                description: "Slam the ground with colossal force, stunning invaders and dragging them together.",
                branchName: "Branch C — Tectonic Force",
                tier: 2,
                prerequisiteAbilityId: "cursed_golem_active_01",
                resourceCost: 35f,
                cooldownDuration: 7f,
                range: 5f,
                effects: AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Gravity,
                targeting: AbilityTargetingRule.Self,
                baseDamage: 40f,
                baseRadius: 5f,
                duration: 1.5f,
                milestones: new[]
                {
                    new AbilityRankMilestone(5, "Slam radius +25%", radiusMultiplier: 1.25f),
                    new AbilityRankMilestone(10, "Slam damage +30%", damageMultiplier: 1.30f),
                    new AbilityRankMilestone(15, "Stun duration +0.5 seconds", bonusUtility: 0.5f),
                    new AbilityRankMilestone(20, "Mastery: Creates a lingering gravity vortex for 3 seconds", bonusUtility: 1f)
                }
            ));

            Register(new AbilityDefinition(
                id: "cursed_golem_ultimate_50",
                characterId: CharacterCatalog.CursedGolemId,
                name: "Colossus Awakening",
                description: "Awaken the ancient colossus within, gaining an enormous barrier and triggering massive seismic eruptions.",
                branchName: "Milestone Ultimates",
                tier: 5,
                prerequisiteAbilityId: "cursed_golem_active_01",
                resourceCost: 70f,
                cooldownDuration: 25f,
                range: 8f,
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
        }
    }
}
