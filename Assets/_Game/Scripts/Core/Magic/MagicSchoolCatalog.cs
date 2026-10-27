using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Pure C# immutable catalog registering all 20 Magic Schools, 60 Tiered Spells,
    /// 20 Legendary Evolved Spells, and 10 Multi-Element Synergies.
    /// Meets all requirements of 11_LEGENDARY_BOSSES_EVENTS_AND_CONTENT.md and world.md (lines 420-438).
    /// </summary>
    public static class MagicSchoolCatalog
    {
        private static readonly Dictionary<MagicSchoolType, MagicSchoolDefinition> _schools = new Dictionary<MagicSchoolType, MagicSchoolDefinition>();
        private static readonly Dictionary<string, SpellDefinition> _spells = new Dictionary<string, SpellDefinition>(StringComparer.OrdinalIgnoreCase);
        private static readonly List<MultiElementSynergy> _synergies = new List<MultiElementSynergy>();

        static MagicSchoolCatalog()
        {
            RegisterAll();
        }

        public static MagicSchoolDefinition GetSchool(MagicSchoolType school)
        {
            return _schools.TryGetValue(school, out var def) ? def : null;
        }

        public static SpellDefinition GetSpell(string spellId)
        {
            if (string.IsNullOrEmpty(spellId)) return null;
            return _spells.TryGetValue(spellId, out var def) ? def : null;
        }

        public static bool TryGetSpell(string spellId, out SpellDefinition spell)
        {
            if (string.IsNullOrEmpty(spellId))
            {
                spell = null;
                return false;
            }
            return _spells.TryGetValue(spellId, out spell);
        }

        public static IReadOnlyCollection<MagicSchoolDefinition> GetAllSchools() => _schools.Values;
        public static IReadOnlyCollection<SpellDefinition> GetAllSpells() => _spells.Values;
        public static IReadOnlyList<MultiElementSynergy> GetAllSynergies() => _synergies;

        public static MultiElementSynergy FindSynergy(MagicSchoolType a, MagicSchoolType b)
        {
            for (int i = 0; i < _synergies.Count; i++)
            {
                if (_synergies[i].Matches(a, b)) return _synergies[i];
            }
            return null;
        }

        private static void RegisterAll()
        {
            _schools.Clear();
            _spells.Clear();
            _synergies.Clear();

            // 1. FIRE (Pyromancy)
            RegisterSchool(
                MagicSchoolType.Fire,
                "Fire",
                "Pyromancy",
                "Devastating incendiary magic that burns foes and spreads conflagration.",
                DamageType.Fire,
                "#FF4500",
                "Ignite: All attacks inflict burning damage over 3 seconds (+15% DoT).",
                MagicSchoolType.Ice,
                "Thermal Shock",
                "Striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.",
                spells: new[]
                {
                    new SpellDefinition("spell_fire_bolt", "Fire Bolt", MagicSchoolType.Fire, 1, CastBehaviorType.DirectProjectile, DamageType.Fire, 16f, 0.7f, 10f, 9f, projectileSpeed: 15f, cueId: CueIds.SpellCastFire, description: "Hurls a blazing bolt of fire.", evolutionSpellId: null, statusEffectName: "Burn", statusEffectDuration: 3f, statusEffectValue: 4f),
                    new SpellDefinition("spell_flame_wave", "Flame Wave", MagicSchoolType.Fire, 2, CastBehaviorType.WaveCone, DamageType.Fire, 28f, 2.5f, 25f, 6f, radius: 3.5f, cueId: CueIds.SpellCastFire, description: "Unleashes a sweeping crescent of flame.", evolutionSpellId: null, statusEffectName: "Burn", statusEffectDuration: 4f, statusEffectValue: 8f),
                    new SpellDefinition("spell_meteor_strike", "Meteor Strike", MagicSchoolType.Fire, 3, CastBehaviorType.FallingImpact, DamageType.Fire, 65f, 6.0f, 45f, 10f, radius: 4.5f, cueId: CueIds.SpellCastFire, description: "Calls down a colossal flaming meteor from the cosmos.", evolutionSpellId: "spell_inferno_cataclysm", statusEffectName: "Burn", statusEffectDuration: 5f, statusEffectValue: 15f)
                },
                evolvedSpell: new SpellDefinition("spell_inferno_cataclysm", "Inferno Cataclysm", MagicSchoolType.Fire, 4, CastBehaviorType.FallingImpact, DamageType.Fire, 110f, 5.0f, 50f, 12f, radius: 6.0f, cueId: CueIds.SpellCastFire, description: "Ascended cataclysm raining a cluster of superheated meteors.", evolutionSpellId: null, statusEffectName: "Incinerate", statusEffectDuration: 6f, statusEffectValue: 30f)
            );

            // 2. ICE (Cryomancy)
            RegisterSchool(
                MagicSchoolType.Ice,
                "Ice",
                "Cryomancy",
                "Frigid control spells that slow enemy movement and crystallize armor.",
                DamageType.Ice,
                "#00E5FF",
                "Chill: Freezes foes, reducing movement and attack speed by 25%.",
                MagicSchoolType.Fire,
                "Thermal Shock",
                "Striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.",
                spells: new[]
                {
                    new SpellDefinition("spell_frost_shard", "Frost Shard", MagicSchoolType.Ice, 1, CastBehaviorType.DirectProjectile, DamageType.Ice, 13f, 0.6f, 8f, 9f, projectileSpeed: 16f, pierces: 1, cueId: CueIds.SpellCastIce, description: "Fires a sharp icicle that pierces one target.", statusEffectName: "Chill", statusEffectDuration: 3f, statusEffectValue: 0.25f),
                    new SpellDefinition("spell_glacial_spike", "Glacial Spike", MagicSchoolType.Ice, 2, CastBehaviorType.GroundHazard, DamageType.Ice, 24f, 3.0f, 22f, 7f, radius: 2.5f, duration: 4f, cueId: CueIds.SpellCastIce, description: "Erupts sharp glacier spikes from the permafrost.", statusEffectName: "Freeze", statusEffectDuration: 1.5f, statusEffectValue: 0.5f),
                    new SpellDefinition("spell_blizzard_storm", "Blizzard Storm", MagicSchoolType.Ice, 3, CastBehaviorType.RadialAoENova, DamageType.Ice, 48f, 5.5f, 40f, 8f, radius: 4.5f, cueId: CueIds.SpellCastIce, description: "Summons a violent sub-zero blizzard around the caster.", evolutionSpellId: "spell_absolute_zero", statusEffectName: "Permafrost", statusEffectDuration: 4f, statusEffectValue: 0.4f)
                },
                evolvedSpell: new SpellDefinition("spell_absolute_zero", "Absolute Zero", MagicSchoolType.Ice, 4, CastBehaviorType.RadialAoENova, DamageType.Ice, 90f, 4.5f, 45f, 10f, radius: 6.0f, cueId: CueIds.SpellCastIce, description: "Flash-freezes all surrounding existence to absolute zero.", statusEffectName: "DeepFreeze", statusEffectDuration: 5f, statusEffectValue: 0.7f)
            );

            // 3. LIGHTNING (Electromancy)
            RegisterSchool(
                MagicSchoolType.Lightning,
                "Lightning",
                "Electromancy",
                "High-voltage electricity that arcs rapidly between clustered targets.",
                DamageType.Lightning,
                "#FFD700",
                "Static Charge: Hits build static charges that discharge for 20% chain damage.",
                MagicSchoolType.Water,
                "Electrified Deluge",
                "Lightning spells chain to +2 additional targets in wet areas.",
                spells: new[]
                {
                    new SpellDefinition("spell_spark_arc", "Spark Arc", MagicSchoolType.Lightning, 1, CastBehaviorType.ChainingArc, DamageType.Lightning, 14f, 0.65f, 10f, 8f, bounces: 2, cueId: CueIds.SpellCastLightning, description: "Discharges an arc of electricity jumping between targets.", statusEffectName: "Shock", statusEffectDuration: 2f, statusEffectValue: 0.15f),
                    new SpellDefinition("spell_ball_lightning", "Ball Lightning", MagicSchoolType.Lightning, 2, CastBehaviorType.HomingSeeker, DamageType.Lightning, 26f, 2.2f, 24f, 9f, projectileSpeed: 7f, projectileCount: 2, cueId: CueIds.SpellCastLightning, description: "Releases slow-moving homing globes of concentrated plasma.", statusEffectName: "Shock", statusEffectDuration: 3f, statusEffectValue: 0.2f),
                    new SpellDefinition("spell_thunder_strike", "Thunder Strike", MagicSchoolType.Lightning, 3, CastBehaviorType.FallingImpact, DamageType.Lightning, 58f, 5.0f, 42f, 10f, radius: 3.5f, cueId: CueIds.SpellCastLightning, description: "Strikes the battlefield with a deafening bolt from the heavens.", evolutionSpellId: "spell_cataclysm_tempest", statusEffectName: "Stun", statusEffectDuration: 1.0f, statusEffectValue: 1f)
                },
                evolvedSpell: new SpellDefinition("spell_cataclysm_tempest", "Cataclysm Tempest", MagicSchoolType.Lightning, 4, CastBehaviorType.FallingImpact, DamageType.Lightning, 105f, 4.2f, 48f, 12f, radius: 5.5f, bounces: 4, cueId: CueIds.SpellCastLightning, description: "A divine tempest unleashing relentless lightning cascades.", statusEffectName: "Overload", statusEffectDuration: 4f, statusEffectValue: 0.35f)
            );

            // 4. WATER (Hydromancy)
            RegisterSchool(
                MagicSchoolType.Water,
                "Water",
                "Hydromancy",
                "Fluid kinetic magic that knocks back foes and soaks the arena.",
                DamageType.Water,
                "#1E90FF",
                "Drenched: Foes become soaked, increasing susceptibility to lightning and frost by 20%.",
                MagicSchoolType.Lightning,
                "Electrified Deluge",
                "Lightning spells chain to +2 additional targets in wet areas.",
                spells: new[]
                {
                    new SpellDefinition("spell_water_jet", "Water Jet", MagicSchoolType.Water, 1, CastBehaviorType.ContinuousBeam, DamageType.Water, 15f, 0.8f, 12f, 8f, cueId: CueIds.SpellCastWater, description: "Channels a focused high-pressure beam of water.", statusEffectName: "Wet", statusEffectDuration: 4f, statusEffectValue: 0.2f),
                    new SpellDefinition("spell_tidal_surge", "Tidal Surge", MagicSchoolType.Water, 2, CastBehaviorType.WaveCone, DamageType.Water, 25f, 2.8f, 22f, 7f, radius: 4f, cueId: CueIds.SpellCastWater, description: "Pushes enemies back with a rushing wave.", statusEffectName: "Knockback", statusEffectDuration: 0.5f, statusEffectValue: 3f),
                    new SpellDefinition("spell_maelstrom_vortex", "Maelstrom Vortex", MagicSchoolType.Water, 3, CastBehaviorType.RadialAoENova, DamageType.Water, 52f, 5.8f, 38f, 8f, radius: 4.2f, cueId: CueIds.SpellCastWater, description: "Forms a roaring oceanic whirlpool dragging enemies inward.", evolutionSpellId: "spell_deluge_abyss", statusEffectName: "VortexPull", statusEffectDuration: 3f, statusEffectValue: 2f)
                },
                evolvedSpell: new SpellDefinition("spell_deluge_abyss", "Deluge of the Abyss", MagicSchoolType.Water, 4, CastBehaviorType.RadialAoENova, DamageType.Water, 95f, 4.8f, 44f, 10f, radius: 5.8f, cueId: CueIds.SpellCastWater, description: "Floods the arena with crushing abyssal water pressures.", statusEffectName: "Drown", statusEffectDuration: 5f, statusEffectValue: 20f)
            );

            // 5. WIND (Aeromancy)
            RegisterSchool(
                MagicSchoolType.Wind,
                "Wind",
                "Aeromancy",
                "High-speed vacuum blades and defensive squalls enhancing mobility.",
                DamageType.Wind,
                "#A7F3D0",
                "Tailwind: Grants +12% movement speed and +10% projectile velocity.",
                MagicSchoolType.Time,
                "Temporal Acceleration",
                "Movement speed and cast speed increased by 15%.",
                spells: new[]
                {
                    new SpellDefinition("spell_gale_blade", "Gale Blade", MagicSchoolType.Wind, 1, CastBehaviorType.DirectProjectile, DamageType.Wind, 14f, 0.5f, 8f, 10f, projectileSpeed: 18f, pierces: 2, cueId: CueIds.SpellCastWind, description: "Fling a slicing blade of razor-sharp wind.", statusEffectName: "Bleed", statusEffectDuration: 2f, statusEffectValue: 3f),
                    new SpellDefinition("spell_cyclone_barrier", "Cyclone Barrier", MagicSchoolType.Wind, 2, CastBehaviorType.OrbitingShield, DamageType.Wind, 22f, 3.5f, 25f, 3f, radius: 2.2f, duration: 5f, cueId: CueIds.SpellCastWind, description: "Spins defensive air currents that shred close foes.", statusEffectName: "Deflect", statusEffectDuration: 5f, statusEffectValue: 0.2f),
                    new SpellDefinition("spell_twister_squall", "Twister Squall", MagicSchoolType.Wind, 3, CastBehaviorType.GroundHazard, DamageType.Wind, 46f, 5.2f, 36f, 9f, radius: 3.8f, duration: 4.5f, cueId: CueIds.SpellCastWind, description: "Summons a roaming tornado tossing foes into chaos.", evolutionSpellId: "spell_tempest_monsoon", statusEffectName: "Levitate", statusEffectDuration: 1.5f, statusEffectValue: 1f)
                },
                evolvedSpell: new SpellDefinition("spell_tempest_monsoon", "Tempest Monsoon", MagicSchoolType.Wind, 4, CastBehaviorType.GroundHazard, DamageType.Wind, 88f, 4.4f, 42f, 11f, radius: 5.4f, duration: 6f, cueId: CueIds.SpellCastWind, description: "A raging gale cyclone covering massive ground.", statusEffectName: "Windshear", statusEffectDuration: 4f, statusEffectValue: 0.3f)
            );

            // 6. EARTH (Geomancy)
            RegisterSchool(
                MagicSchoolType.Earth,
                "Earth",
                "Geomancy",
                "Crushing tectonic magic utilizing boulders, tremors, and granite armor.",
                DamageType.Earth,
                "#B45309",
                "Stoneform: Hardens skin, reducing incoming damage by flat 8 armor.",
                MagicSchoolType.Metal,
                "Tectonic Forge",
                "Increases armor by 15% and physical/earth spell damage by 20%.",
                spells: new[]
                {
                    new SpellDefinition("spell_stone_barrage", "Stone Barrage", MagicSchoolType.Earth, 1, CastBehaviorType.DirectProjectile, DamageType.Earth, 15f, 0.75f, 10f, 8f, projectileSpeed: 12f, projectileCount: 3, cueId: CueIds.SpellCastEarth, description: "Hurls a cluster of dense jagged rocks.", statusEffectName: "Crush", statusEffectDuration: 1f, statusEffectValue: 2f),
                    new SpellDefinition("spell_tremor_quake", "Tremor Quake", MagicSchoolType.Earth, 2, CastBehaviorType.RadialAoENova, DamageType.Earth, 27f, 3.2f, 26f, 6f, radius: 3.5f, cueId: CueIds.SpellCastEarth, description: "Slams the ground sending seismic shockwaves.", statusEffectName: "Stagger", statusEffectDuration: 1.2f, statusEffectValue: 0.4f),
                    new SpellDefinition("spell_boulder_catapult", "Boulder Catapult", MagicSchoolType.Earth, 3, CastBehaviorType.FallingImpact, DamageType.Earth, 62f, 5.8f, 44f, 9f, radius: 4.2f, cueId: CueIds.SpellCastEarth, description: "Drops a massive granite monolith onto enemy ranks.", evolutionSpellId: "spell_tectonic_shatter", statusEffectName: "Stun", statusEffectDuration: 1.5f, statusEffectValue: 1f)
                },
                evolvedSpell: new SpellDefinition("spell_tectonic_shatter", "Tectonic Shatter", MagicSchoolType.Earth, 4, CastBehaviorType.FallingImpact, DamageType.Earth, 112f, 4.8f, 50f, 11f, radius: 6.0f, cueId: CueIds.SpellCastEarth, description: "Shatters tectonic plates under the entire arena.", statusEffectName: "Faultline", statusEffectDuration: 4f, statusEffectValue: 0.5f)
            );

            // 7. POISON (Toximancy)
            RegisterSchool(
                MagicSchoolType.Poison,
                "Poison",
                "Toximancy",
                "Virulent corrosive bile that eats through armor and inflicts stacking toxins.",
                DamageType.Poison,
                "#10B981",
                "Corrosion: Reduces enemy armor by 30% while afflicted with poison.",
                MagicSchoolType.Nature,
                "Virulent Spores",
                "Poison clouds spawn stinging thorn vines dealing 20% bonus DPS.",
                spells: new[]
                {
                    new SpellDefinition("spell_toxic_spitter", "Toxic Spitter", MagicSchoolType.Poison, 1, CastBehaviorType.DirectProjectile, DamageType.Poison, 12f, 0.55f, 9f, 8.5f, projectileSpeed: 14f, cueId: CueIds.SpellCastPoison, description: "Spits caustic acid burning flesh on contact.", statusEffectName: "Poison", statusEffectDuration: 4f, statusEffectValue: 6f),
                    new SpellDefinition("spell_venom_cloud", "Venom Cloud", MagicSchoolType.Poison, 2, CastBehaviorType.GroundHazard, DamageType.Poison, 24f, 3.2f, 24f, 7f, radius: 3.5f, duration: 5f, cueId: CueIds.SpellCastPoison, description: "Creates a toxic miasma choking all who enter.", statusEffectName: "Toxin", statusEffectDuration: 5f, statusEffectValue: 10f),
                    new SpellDefinition("spell_plague_infestation", "Plague Infestation", MagicSchoolType.Poison, 3, CastBehaviorType.ChainingArc, DamageType.Poison, 50f, 5.0f, 38f, 9f, bounces: 4, cueId: CueIds.SpellCastPoison, description: "Injects virulent disease leaping violently between hosts.", evolutionSpellId: "spell_nox_extinction", statusEffectName: "Plague", statusEffectDuration: 6f, statusEffectValue: 15f)
                },
                evolvedSpell: new SpellDefinition("spell_nox_extinction", "Noxious Extinction", MagicSchoolType.Poison, 4, CastBehaviorType.GroundHazard, DamageType.Poison, 92f, 4.2f, 46f, 10f, radius: 5.5f, duration: 7f, cueId: CueIds.SpellCastPoison, description: "Spreads an apocalyptic airborne neurotoxin.", statusEffectName: "Necrosis", statusEffectDuration: 6f, statusEffectValue: 30f)
            );

            // 8. BLOOD (Haemomancy)
            RegisterSchool(
                MagicSchoolType.Blood,
                "Blood",
                "Haemomancy",
                "Forbidden vitality magic draining life force from enemies to heal the caster.",
                DamageType.Blood,
                "#991B1B",
                "Sanguine Thirst: 12% of all damage dealt is converted into health.",
                MagicSchoolType.Necromancy,
                "Sanguine Lichdom",
                "Minions and soul attacks heal the caster for 12% of damage.",
                spells: new[]
                {
                    new SpellDefinition("spell_blood_lance", "Blood Lance", MagicSchoolType.Blood, 1, CastBehaviorType.DirectProjectile, DamageType.Blood, 15f, 0.7f, 10f, 9f, projectileSpeed: 15f, cueId: CueIds.SpellCastBlood, description: "Fires a coagulated needle of vampiric blood.", statusEffectName: "Lifesteal", statusEffectDuration: 1f, statusEffectValue: 0.15f),
                    new SpellDefinition("spell_crimson_siphon", "Crimson Siphon", MagicSchoolType.Blood, 2, CastBehaviorType.ContinuousBeam, DamageType.Blood, 26f, 2.8f, 24f, 7f, cueId: CueIds.SpellCastBlood, description: "Extracts enemy blood directly into caster's essence.", statusEffectName: "Leech", statusEffectDuration: 3f, statusEffectValue: 0.25f),
                    new SpellDefinition("spell_exsanguinate_burst", "Exsanguinate Burst", MagicSchoolType.Blood, 3, CastBehaviorType.RadialAoENova, DamageType.Blood, 55f, 5.5f, 42f, 8f, radius: 4.2f, cueId: CueIds.SpellCastBlood, description: "Detonates the blood vessels of surrounding invaders.", evolutionSpellId: "spell_sanguine_ascendance", statusEffectName: "Hemorrhage", statusEffectDuration: 4f, statusEffectValue: 12f)
                },
                evolvedSpell: new SpellDefinition("spell_sanguine_ascendance", "Sanguine Ascendance", MagicSchoolType.Blood, 4, CastBehaviorType.RadialAoENova, DamageType.Blood, 100f, 4.5f, 48f, 10f, radius: 5.8f, cueId: CueIds.SpellCastBlood, description: "Ascends into a blood avatar draining the battlefield.", statusEffectName: "BloodGod", statusEffectDuration: 5f, statusEffectValue: 0.35f)
            );

            // 9. SHADOW (Umbromancy)
            RegisterSchool(
                MagicSchoolType.Shadow,
                "Shadow",
                "Umbromancy",
                "Elusive occult arts weakening foes from the darkness.",
                DamageType.Shadow,
                "#312E81",
                "Shadowstep: Dodging leaves behind a shadow decoy distracting foes for 2s.",
                MagicSchoolType.Dark,
                "Abyssal Eclipse",
                "Dark and shadow spells have 15% chance to instantly execute non-bosses below 15% HP.",
                spells: new[]
                {
                    new SpellDefinition("spell_shadow_tendril", "Shadow Tendril", MagicSchoolType.Shadow, 1, CastBehaviorType.DirectProjectile, DamageType.Shadow, 14f, 0.6f, 9f, 9f, projectileSpeed: 16f, cueId: CueIds.SpellCastShadow, description: "Lashes out with an ethereal tendril of darkness.", statusEffectName: "Blind", statusEffectDuration: 2f, statusEffectValue: 0.2f),
                    new SpellDefinition("spell_umbral_veil", "Umbral Veil", MagicSchoolType.Shadow, 2, CastBehaviorType.AuraPulse, DamageType.Shadow, 22f, 3.0f, 22f, 4f, radius: 3.5f, duration: 4f, cueId: CueIds.SpellCastShadow, description: "Pulses shadows dealing harm and concealing caster.", statusEffectName: "Veil", statusEffectDuration: 3f, statusEffectValue: 0.3f),
                    new SpellDefinition("spell_black_sun", "Black Sun", MagicSchoolType.Shadow, 3, CastBehaviorType.RadialAoENova, DamageType.Shadow, 54f, 5.2f, 40f, 8f, radius: 4.5f, cueId: CueIds.SpellCastShadow, description: "Manifests an eclipsing black star that consumes light.", evolutionSpellId: "spell_event_horizon_umbra", statusEffectName: "UmbralCurse", statusEffectDuration: 4f, statusEffectValue: 0.25f)
                },
                evolvedSpell: new SpellDefinition("spell_event_horizon_umbra", "Umbral Event Horizon", MagicSchoolType.Shadow, 4, CastBehaviorType.RadialAoENova, DamageType.Shadow, 96f, 4.4f, 45f, 10f, radius: 6.2f, cueId: CueIds.SpellCastShadow, description: "Complete shadow collapse sucking light and life.", statusEffectName: "TrueDarkness", statusEffectDuration: 5f, statusEffectValue: 0.5f)
            );

            // 10. LIGHT (Photomancy)
            RegisterSchool(
                MagicSchoolType.Light,
                "Light",
                "Photomancy",
                "Blinding beams and illuminating surges that disintegrate corruption.",
                DamageType.Light,
                "#FEF08A",
                "Luminescence: Illuminates hit targets, taking 15% increased damage from all sources.",
                MagicSchoolType.Holy,
                "Celestial Radiance",
                "Radiant and holy spells cleanse slow and heal for 5% max HP on cast.",
                spells: new[]
                {
                    new SpellDefinition("spell_radiant_beam", "Radiant Beam", MagicSchoolType.Light, 1, CastBehaviorType.ContinuousBeam, DamageType.Light, 16f, 0.75f, 11f, 8.5f, cueId: CueIds.SpellCastLight, description: "Focuses a piercing ray of pure daylight.", statusEffectName: "Glow", statusEffectDuration: 3f, statusEffectValue: 0.15f),
                    new SpellDefinition("spell_prismatic_lance", "Prismatic Lance", MagicSchoolType.Light, 2, CastBehaviorType.DirectProjectile, DamageType.Light, 27f, 2.6f, 24f, 10f, projectileSpeed: 20f, pierces: 3, cueId: CueIds.SpellCastLight, description: "Projects a refracting spear of multi-spectrum light.", statusEffectName: "Blind", statusEffectDuration: 2.5f, statusEffectValue: 0.3f),
                    new SpellDefinition("spell_solar_flare", "Solar Flare", MagicSchoolType.Light, 3, CastBehaviorType.RadialAoENova, DamageType.Light, 56f, 5.4f, 42f, 8f, radius: 4.6f, cueId: CueIds.SpellCastLight, description: "Erupts a blinding solar nova igniting all foes.", evolutionSpellId: "spell_supernova_burst", statusEffectName: "SolarBurn", statusEffectDuration: 4f, statusEffectValue: 12f)
                },
                evolvedSpell: new SpellDefinition("spell_supernova_burst", "Supernova Burst", MagicSchoolType.Light, 4, CastBehaviorType.RadialAoENova, DamageType.Light, 102f, 4.5f, 48f, 11f, radius: 6.0f, cueId: CueIds.SpellCastLight, description: "Unleashes the blinding ignition of a newborn star.", statusEffectName: "Disintegrate", statusEffectDuration: 5f, statusEffectValue: 25f)
            );

            // 11. HOLY (Theomancy)
            RegisterSchool(
                MagicSchoolType.Holy,
                "Holy",
                "Theomancy",
                "Sacred divine judgment that purges darkness and protects allies.",
                DamageType.Holy,
                "#FACC15",
                "Divine Ward: Periodically grants a barrier absorbing up to 20 damage.",
                MagicSchoolType.Light,
                "Celestial Radiance",
                "Radiant and holy spells cleanse slow and heal for 5% max HP on cast.",
                spells: new[]
                {
                    new SpellDefinition("spell_holy_smite", "Holy Smite", MagicSchoolType.Holy, 1, CastBehaviorType.FallingImpact, DamageType.Holy, 16f, 0.8f, 10f, 8f, radius: 1.5f, cueId: CueIds.SpellCastHoly, description: "Strikes a foe with sacred judgment from above.", statusEffectName: "Smite", statusEffectDuration: 2f, statusEffectValue: 5f),
                    new SpellDefinition("spell_consecrated_aura", "Consecrated Aura", MagicSchoolType.Holy, 2, CastBehaviorType.AuraPulse, DamageType.Holy, 24f, 3.2f, 24f, 5f, radius: 3.5f, duration: 4.5f, cueId: CueIds.SpellCastHoly, description: "Sanctifies ground around caster, burning impurities.", statusEffectName: "Sanctified", statusEffectDuration: 3f, statusEffectValue: 8f),
                    new SpellDefinition("spell_divine_wrath", "Divine Wrath", MagicSchoolType.Holy, 3, CastBehaviorType.RadialAoENova, DamageType.Holy, 58f, 5.5f, 40f, 9f, radius: 4.8f, cueId: CueIds.SpellCastHoly, description: "Heavens descend in a brilliant blast of righteous fury.", evolutionSpellId: "spell_judgment_of_archons", statusEffectName: "Purified", statusEffectDuration: 3.5f, statusEffectValue: 15f)
                },
                evolvedSpell: new SpellDefinition("spell_judgment_of_archons", "Judgment of the Archons", MagicSchoolType.Holy, 4, CastBehaviorType.RadialAoENova, DamageType.Holy, 106f, 4.6f, 46f, 11f, radius: 6.4f, cueId: CueIds.SpellCastHoly, description: "Archangels' wrath obliterating evil across the sanctuary.", statusEffectName: "HeavenlyRetribution", statusEffectDuration: 5f, statusEffectValue: 30f)
            );

            // 12. DARK (Noctumancy)
            RegisterSchool(
                MagicSchoolType.Dark,
                "Dark",
                "Noctumancy",
                "Abyssal entropy that corrodes willpower and pulls matter into nothingness.",
                DamageType.Dark,
                "#1E1B4B",
                "Despair: Enemies afflicted deal 20% reduced damage.",
                MagicSchoolType.Shadow,
                "Abyssal Eclipse",
                "Dark and shadow spells have 15% chance to instantly execute non-bosses below 15% HP.",
                spells: new[]
                {
                    new SpellDefinition("spell_dark_matter", "Dark Matter", MagicSchoolType.Dark, 1, CastBehaviorType.HomingSeeker, DamageType.Dark, 15f, 0.7f, 10f, 9f, projectileSpeed: 10f, cueId: CueIds.SpellCastDark, description: "Launches seeking globules of heavy dark matter.", statusEffectName: "Curse", statusEffectDuration: 3f, statusEffectValue: 0.15f),
                    new SpellDefinition("spell_abyssal_grasp", "Abyssal Grasp", MagicSchoolType.Dark, 2, CastBehaviorType.GroundHazard, DamageType.Dark, 26f, 3.2f, 25f, 7f, radius: 3.2f, duration: 4f, cueId: CueIds.SpellCastDark, description: "Summons grasping shadows rooting foes to the floor.", statusEffectName: "Root", statusEffectDuration: 2f, statusEffectValue: 1f),
                    new SpellDefinition("spell_singularity_tear", "Singularity Tear", MagicSchoolType.Dark, 3, CastBehaviorType.FallingImpact, DamageType.Dark, 60f, 5.6f, 44f, 9f, radius: 4.2f, cueId: CueIds.SpellCastDark, description: "Tears open a pocket singularity swallowing reality.", evolutionSpellId: "spell_void_oblivion_core", statusEffectName: "GravityWell", statusEffectDuration: 3f, statusEffectValue: 2f)
                },
                evolvedSpell: new SpellDefinition("spell_void_oblivion_core", "Oblivion Core", MagicSchoolType.Dark, 4, CastBehaviorType.FallingImpact, DamageType.Dark, 108f, 4.6f, 50f, 11f, radius: 6.0f, cueId: CueIds.SpellCastDark, description: "An inescapable micro-black hole devouring target zone.", statusEffectName: "Nullify", statusEffectDuration: 5f, statusEffectValue: 0.4f)
            );

            // 13. NECROMANCY (Mortis)
            RegisterSchool(
                MagicSchoolType.Necromancy,
                "Necromancy",
                "Mortis",
                "The dread domain of death, bone shards, and corpse consumption.",
                DamageType.Necromancy,
                "#4C1D95",
                "Soul Feast: Defeating enemies restores 5 resource and charges soul power.",
                MagicSchoolType.Blood,
                "Sanguine Lichdom",
                "Minions and soul attacks heal the caster for 12% of damage.",
                spells: new[]
                {
                    new SpellDefinition("spell_bone_spear", "Bone Spear", MagicSchoolType.Necromancy, 1, CastBehaviorType.DirectProjectile, DamageType.Necromancy, 16f, 0.65f, 10f, 10f, projectileSpeed: 17f, pierces: 2, cueId: CueIds.SpellCastNecromancy, description: "Hurls a calcified bone lance piercing enemy lines.", statusEffectName: "Fracture", statusEffectDuration: 3f, statusEffectValue: 0.2f),
                    new SpellDefinition("spell_corpse_explosion", "Corpse Explosion", MagicSchoolType.Necromancy, 2, CastBehaviorType.RadialAoENova, DamageType.Necromancy, 30f, 2.8f, 26f, 7f, radius: 3.8f, cueId: CueIds.SpellCastNecromancy, description: "Detonates fallen remains in a lethal shrapnel burst.", statusEffectName: "Rot", statusEffectDuration: 4f, statusEffectValue: 10f),
                    new SpellDefinition("spell_death_coil", "Death Coil", MagicSchoolType.Necromancy, 3, CastBehaviorType.HomingSeeker, DamageType.Necromancy, 56f, 5.0f, 40f, 9f, projectileCount: 3, cueId: CueIds.SpellCastNecromancy, description: "Unleashes screaming death coils seeking souls.", evolutionSpellId: "spell_reaper_final_harvest", statusEffectName: "SoulBleed", statusEffectDuration: 4f, statusEffectValue: 12f)
                },
                evolvedSpell: new SpellDefinition("spell_reaper_final_harvest", "The Final Harvest", MagicSchoolType.Necromancy, 4, CastBehaviorType.RadialAoENova, DamageType.Necromancy, 104f, 4.4f, 48f, 11f, radius: 6.2f, cueId: CueIds.SpellCastNecromancy, description: "Reaps all souls within vicinity into eternal servitude.", statusEffectName: "DoomHarvest", statusEffectDuration: 5f, statusEffectValue: 25f)
            );

            // 14. SPIRIT (Spiritism)
            RegisterSchool(
                MagicSchoolType.Spirit,
                "Spirit",
                "Spiritism",
                "Astral apparitions that bypass physical armor completely.",
                DamageType.Spirit,
                "#67E8F9",
                "Ethereal Form: 15% chance to ignore incoming projectile damage entirely.",
                MagicSchoolType.Arcane,
                "Aetheric Resonance",
                "Cooldown reduction increased by 10% and mana costs reduced by 15%.",
                spells: new[]
                {
                    new SpellDefinition("spell_ethereal_wisp", "Ethereal Wisp", MagicSchoolType.Spirit, 1, CastBehaviorType.HomingSeeker, DamageType.Spirit, 14f, 0.6f, 9f, 9f, projectileSpeed: 11f, projectileCount: 2, cueId: CueIds.SpellCastSpirit, description: "Releases glowing spirit wisps that phase through obstacles.", statusEffectName: "Haunt", statusEffectDuration: 3f, statusEffectValue: 0.15f),
                    new SpellDefinition("spell_spectral_wail", "Spectral Wail", MagicSchoolType.Spirit, 2, CastBehaviorType.WaveCone, DamageType.Spirit, 25f, 2.9f, 23f, 6.5f, radius: 4f, cueId: CueIds.SpellCastSpirit, description: "A chilling ghostly shriek that terrifies foes.", statusEffectName: "Fear", statusEffectDuration: 1.5f, statusEffectValue: 0.5f),
                    new SpellDefinition("spell_astral_projection", "Astral Projection", MagicSchoolType.Spirit, 3, CastBehaviorType.OrbitingShield, DamageType.Spirit, 48f, 5.2f, 38f, 4f, radius: 2.8f, duration: 6f, cueId: CueIds.SpellCastSpirit, description: "Summons spectral avatars rotating in defense.", evolutionSpellId: "spell_spirit_pandemonium", statusEffectName: "AstralWard", statusEffectDuration: 5f, statusEffectValue: 0.2f)
                },
                evolvedSpell: new SpellDefinition("spell_spirit_pandemonium", "Astral Pandemonium", MagicSchoolType.Spirit, 4, CastBehaviorType.OrbitingShield, DamageType.Spirit, 94f, 4.2f, 44f, 6f, radius: 4.5f, duration: 8f, cueId: CueIds.SpellCastSpirit, description: "A chaotic storm of ancestors rending enemies to bits.", statusEffectName: "Phantasm", statusEffectDuration: 6f, statusEffectValue: 0.35f)
            );

            // 15. ARCANE (Arcanomancy)
            RegisterSchool(
                MagicSchoolType.Arcane,
                "Arcane",
                "Arcanomancy",
                "Pure primordial mana manipulated into disintegrating energy.",
                DamageType.Arcane,
                "#C084FC",
                "Arcane Surge: Spell critical hits grant +25% cast speed for 3 seconds.",
                MagicSchoolType.Spirit,
                "Aetheric Resonance",
                "Cooldown reduction increased by 10% and mana costs reduced by 15%.",
                spells: new[]
                {
                    new SpellDefinition("spell_magic_missile", "Magic Missile", MagicSchoolType.Arcane, 1, CastBehaviorType.HomingSeeker, DamageType.Arcane, 13f, 0.55f, 8f, 9f, projectileSpeed: 14f, projectileCount: 3, cueId: CueIds.SpellCastArcane, description: "Fires tri-force seeker darts of concentrated mana.", statusEffectName: "ManaEcho", statusEffectDuration: 2f, statusEffectValue: 3f),
                    new SpellDefinition("spell_arcane_orb", "Arcane Orb", MagicSchoolType.Arcane, 2, CastBehaviorType.DirectProjectile, DamageType.Arcane, 28f, 2.5f, 24f, 9f, projectileSpeed: 8f, pierces: 5, cueId: CueIds.SpellCastArcane, description: "Hurls a slow pulsating globe emitting arcane pulses.", statusEffectName: "Disrupt", statusEffectDuration: 3f, statusEffectValue: 0.2f),
                    new SpellDefinition("spell_disintegration_ray", "Disintegration Ray", MagicSchoolType.Arcane, 3, CastBehaviorType.ContinuousBeam, DamageType.Arcane, 64f, 5.0f, 45f, 10f, cueId: CueIds.SpellCastArcane, description: "Channels a devastating beam vaporizing matter.", evolutionSpellId: "spell_nether_singularity", statusEffectName: "Evaporate", statusEffectDuration: 3.5f, statusEffectValue: 20f)
                },
                evolvedSpell: new SpellDefinition("spell_nether_singularity", "Nether Singularity", MagicSchoolType.Arcane, 4, CastBehaviorType.ContinuousBeam, DamageType.Arcane, 115f, 4.0f, 50f, 12f, cueId: CueIds.SpellCastArcane, description: "Supreme arcane beam piercing all barriers and shields.", statusEffectName: "MatterDecay", statusEffectDuration: 5f, statusEffectValue: 35f)
            );

            // 16. NATURE (Vitamancy)
            RegisterSchool(
                MagicSchoolType.Nature,
                "Nature",
                "Vitamancy",
                "Vigorous organic growth using brambles, thorn whips, and regenerative sap.",
                DamageType.Nature,
                "#22C55E",
                "Regeneration: Regenerates 2% of maximum health every 3 seconds.",
                MagicSchoolType.Poison,
                "Virulent Spores",
                "Poison clouds spawn stinging thorn vines dealing 20% bonus DPS.",
                spells: new[]
                {
                    new SpellDefinition("spell_thorn_whip", "Thorn Whip", MagicSchoolType.Nature, 1, CastBehaviorType.WaveCone, DamageType.Nature, 15f, 0.7f, 9f, 6.5f, radius: 3.5f, cueId: CueIds.SpellCastNature, description: "Lashes out with a barbed thorny vine.", statusEffectName: "Entangle", statusEffectDuration: 2f, statusEffectValue: 0.2f),
                    new SpellDefinition("spell_entangling_roots", "Entangling Roots", MagicSchoolType.Nature, 2, CastBehaviorType.GroundHazard, DamageType.Nature, 24f, 3.2f, 22f, 7.5f, radius: 3.5f, duration: 4.5f, cueId: CueIds.SpellCastNature, description: "Roots foes to the spot while thorny vines crush them.", statusEffectName: "Constrict", statusEffectDuration: 3f, statusEffectValue: 8f),
                    new SpellDefinition("spell_wrath_of_ancients", "Wrath of the Ancients", MagicSchoolType.Nature, 3, CastBehaviorType.RadialAoENova, DamageType.Nature, 52f, 5.5f, 38f, 8.5f, radius: 4.5f, cueId: CueIds.SpellCastNature, description: "Awakens primeval flora sweeping all intruders away.", evolutionSpellId: "spell_primordial_gaia", statusEffectName: "BrambleTrap", statusEffectDuration: 4f, statusEffectValue: 14f)
                },
                evolvedSpell: new SpellDefinition("spell_primordial_gaia", "Primordial Gaia", MagicSchoolType.Nature, 4, CastBehaviorType.RadialAoENova, DamageType.Nature, 98f, 4.5f, 45f, 10.5f, radius: 6.2f, cueId: CueIds.SpellCastNature, description: "Transforms the arena into a lethal primeval jungle.", statusEffectName: "NatureVengeance", statusEffectDuration: 5f, statusEffectValue: 28f)
            );

            // 17. VOID (Nihil)
            RegisterSchool(
                MagicSchoolType.Void,
                "Void",
                "Void Magic",
                "Dimensional rips erasing physical matter from existence.",
                DamageType.Void,
                "#581C87",
                "Null Shield: Completely nullifies 1 out of every 5 incoming hits.",
                MagicSchoolType.Chaos,
                "Entropy Singularity",
                "Critical strikes rip mini-rifts pulling enemies in for 30 damage.",
                spells: new[]
                {
                    new SpellDefinition("spell_void_rift", "Void Rift", MagicSchoolType.Void, 1, CastBehaviorType.GroundHazard, DamageType.Void, 15f, 0.75f, 10f, 8f, radius: 2.5f, duration: 3.5f, cueId: CueIds.SpellCastVoid, description: "Rips open a localized dimensional tear.", statusEffectName: "VoidBurn", statusEffectDuration: 3f, statusEffectValue: 5f),
                    new SpellDefinition("spell_null_lance", "Null Lance", MagicSchoolType.Void, 2, CastBehaviorType.DirectProjectile, DamageType.Void, 28f, 2.6f, 24f, 10f, projectileSpeed: 18f, pierces: 4, cueId: CueIds.SpellCastVoid, description: "Fires a beam ignoring 100% of armor and barriers.", statusEffectName: "ArmorShatter", statusEffectDuration: 3f, statusEffectValue: 1f),
                    new SpellDefinition("spell_dimensional_collapse", "Dimensional Collapse", MagicSchoolType.Void, 3, CastBehaviorType.FallingImpact, DamageType.Void, 62f, 5.8f, 44f, 9.5f, radius: 4.5f, cueId: CueIds.SpellCastVoid, description: "Collapses spatial dimensions onto the impact zone.", evolutionSpellId: "spell_zero_point_cascade", statusEffectName: "Erased", statusEffectDuration: 2f, statusEffectValue: 15f)
                },
                evolvedSpell: new SpellDefinition("spell_zero_point_cascade", "Zero-Point Cascade", MagicSchoolType.Void, 4, CastBehaviorType.FallingImpact, DamageType.Void, 114f, 4.6f, 50f, 11.5f, radius: 6.2f, cueId: CueIds.SpellCastVoid, description: "Zero-point collapse obliterating everything in range.", statusEffectName: "NonExistence", statusEffectDuration: 4f, statusEffectValue: 35f)
            );

            // 18. CHAOS (Pandemonium)
            RegisterSchool(
                MagicSchoolType.Chaos,
                "Chaos",
                "Pandemonium",
                "Wild unchained entropic magic with variable and explosive results.",
                DamageType.Chaos,
                "#F43F5E",
                "Wild Surge: Spells have 20% chance to cast a second random spell free.",
                MagicSchoolType.Void,
                "Entropy Singularity",
                "Critical strikes rip mini-rifts pulling enemies in for 30 damage.",
                spells: new[]
                {
                    new SpellDefinition("spell_chaos_bolt", "Chaos Bolt", MagicSchoolType.Chaos, 1, CastBehaviorType.DirectProjectile, DamageType.Chaos, 17f, 0.65f, 10f, 9f, projectileSpeed: 15f, pierces: 1, cueId: CueIds.SpellCastChaos, description: "Fires an erratic bolt surging with unpredictable fury.", statusEffectName: "Entropy", statusEffectDuration: 3f, statusEffectValue: 5f),
                    new SpellDefinition("spell_entropic_flux", "Entropic Flux", MagicSchoolType.Chaos, 2, CastBehaviorType.AuraPulse, DamageType.Chaos, 26f, 2.9f, 24f, 5f, radius: 3.8f, duration: 4f, cueId: CueIds.SpellCastChaos, description: "Pulses random elemental damage waves around caster.", statusEffectName: "Flux", statusEffectDuration: 3f, statusEffectValue: 8f),
                    new SpellDefinition("spell_cosmic_anarchy", "Cosmic Anarchy", MagicSchoolType.Chaos, 3, CastBehaviorType.RadialAoENova, DamageType.Chaos, 60f, 5.4f, 42f, 8.5f, radius: 4.8f, cueId: CueIds.SpellCastChaos, description: "Unleashes multi-elemental explosive devastation.", evolutionSpellId: "spell_entropy_unbound", statusEffectName: "ChaosRend", statusEffectDuration: 4f, statusEffectValue: 18f)
                },
                evolvedSpell: new SpellDefinition("spell_entropy_unbound", "Entropy Unbound", MagicSchoolType.Chaos, 4, CastBehaviorType.RadialAoENova, DamageType.Chaos, 110f, 4.4f, 48f, 11f, radius: 6.5f, cueId: CueIds.SpellCastChaos, description: "Catastrophic breakdown of natural laws in target zone.", statusEffectName: "TotalAnarchy", statusEffectDuration: 5f, statusEffectValue: 32f)
            );

            // 19. TIME (Chronomancy)
            RegisterSchool(
                MagicSchoolType.Time,
                "Time",
                "Chronomancy",
                "Temporal manipulation accelerating allies and freezing foes in stasis.",
                DamageType.Time,
                "#38BDF8",
                "Chronos Ward: +15% Cooldown Reduction across all skills and spells.",
                MagicSchoolType.Wind,
                "Temporal Acceleration",
                "Movement speed and cast speed increased by 15%.",
                spells: new[]
                {
                    new SpellDefinition("spell_chrono_bolt", "Chrono Bolt", MagicSchoolType.Time, 1, CastBehaviorType.DirectProjectile, DamageType.Time, 14f, 0.6f, 9f, 9f, projectileSpeed: 16f, cueId: CueIds.SpellCastTime, description: "Fires a temporal distortion slowing target's timeline.", statusEffectName: "Decelerate", statusEffectDuration: 3f, statusEffectValue: 0.35f),
                    new SpellDefinition("spell_temporal_stutter", "Temporal Stutter", MagicSchoolType.Time, 2, CastBehaviorType.AuraPulse, DamageType.Time, 22f, 3.2f, 22f, 4f, radius: 3.5f, duration: 4.5f, cueId: CueIds.SpellCastTime, description: "Dilates time locally, hindering approaching enemy strikes.", statusEffectName: "Stasis", statusEffectDuration: 2f, statusEffectValue: 0.5f),
                    new SpellDefinition("spell_time_dilation_field", "Time Dilation Field", MagicSchoolType.Time, 3, CastBehaviorType.GroundHazard, DamageType.Time, 48f, 5.5f, 40f, 8.5f, radius: 4.2f, duration: 5f, cueId: CueIds.SpellCastTime, description: "Creates a chronosphere slowing all invaders by 70%.", evolutionSpellId: "spell_chronos_shatter", statusEffectName: "TimeStop", statusEffectDuration: 3f, statusEffectValue: 0.7f)
                },
                evolvedSpell: new SpellDefinition("spell_chronos_shatter", "Chronos Temporal Shatter", MagicSchoolType.Time, 4, CastBehaviorType.GroundHazard, DamageType.Time, 92f, 4.5f, 45f, 10.5f, radius: 6.0f, duration: 6.5f, cueId: CueIds.SpellCastTime, description: "Freezes enemies in stasis then shatters their timeline.", statusEffectName: "TemporalCollapse", statusEffectDuration: 5f, statusEffectValue: 0.9f)
            );

            // 20. METAL (Ferromancy)
            RegisterSchool(
                MagicSchoolType.Metal,
                "Metal",
                "Ferromancy",
                "Magnetic telekinesis flinging deadly blades and crushing iron maidens.",
                DamageType.Metal,
                "#94A3B8",
                "Iron Skin: Increases total armor by 25% and reflects 10% melee damage.",
                MagicSchoolType.Earth,
                "Tectonic Forge",
                "Increases armor by 15% and physical/earth spell damage by 20%.",
                spells: new[]
                {
                    new SpellDefinition("spell_shrapnel_shot", "Shrapnel Shot", MagicSchoolType.Metal, 1, CastBehaviorType.WaveCone, DamageType.Metal, 16f, 0.65f, 10f, 7f, radius: 3.5f, projectileCount: 5, cueId: CueIds.SpellCastMetal, description: "Blasts a conical cone of iron flechettes.", statusEffectName: "Lacerate", statusEffectDuration: 3f, statusEffectValue: 4f),
                    new SpellDefinition("spell_blade_orbit", "Blade Orbit", MagicSchoolType.Metal, 2, CastBehaviorType.OrbitingShield, DamageType.Metal, 25f, 3.2f, 25f, 3.5f, radius: 2.5f, duration: 5f, cueId: CueIds.SpellCastMetal, description: "Spins whirling steel blades orbiting around caster.", statusEffectName: "Shred", statusEffectDuration: 4f, statusEffectValue: 0.25f),
                    new SpellDefinition("spell_iron_maiden_crush", "Iron Maiden Crush", MagicSchoolType.Metal, 3, CastBehaviorType.FallingImpact, DamageType.Metal, 62f, 5.8f, 44f, 9f, radius: 4.2f, cueId: CueIds.SpellCastMetal, description: "Calls down a colossal iron maiden slamming shut on enemies.", evolutionSpellId: "spell_colossus_anvil", statusEffectName: "Crushed", statusEffectDuration: 2f, statusEffectValue: 20f)
                },
                evolvedSpell: new SpellDefinition("spell_colossus_anvil", "Colossus God Anvil", MagicSchoolType.Metal, 4, CastBehaviorType.FallingImpact, DamageType.Metal, 110f, 4.6f, 50f, 11f, radius: 6.0f, cueId: CueIds.SpellCastMetal, description: "An astronomical forge anvil crushing all foes below.", statusEffectName: "Obliterated", statusEffectDuration: 4f, statusEffectValue: 35f)
            );

            // Register 10 Multi-Element Synergies
            RegisterSynergy(MagicSchoolType.Fire, MagicSchoolType.Ice, "Thermal Shock", "Striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.", 0.25f, 0.05f);
            RegisterSynergy(MagicSchoolType.Lightning, MagicSchoolType.Water, "Electrified Deluge", "Lightning spells chain to +2 additional targets in wet areas.", 0.20f, 0.10f);
            RegisterSynergy(MagicSchoolType.Blood, MagicSchoolType.Necromancy, "Sanguine Lichdom", "Minions and soul attacks heal the caster for 12% of damage.", 0.18f, 0.05f);
            RegisterSynergy(MagicSchoolType.Earth, MagicSchoolType.Metal, "Tectonic Forge", "Increases armor by 15% and physical/earth spell damage by 20%.", 0.20f, 0.05f);
            RegisterSynergy(MagicSchoolType.Shadow, MagicSchoolType.Dark, "Abyssal Eclipse", "Dark and shadow spells have 15% chance to instantly execute non-bosses below 15% HP.", 0.22f, 0.08f);
            RegisterSynergy(MagicSchoolType.Light, MagicSchoolType.Holy, "Celestial Radiance", "Radiant and holy spells cleanse slow and heal for 5% max HP on cast.", 0.18f, 0.08f);
            RegisterSynergy(MagicSchoolType.Nature, MagicSchoolType.Poison, "Virulent Spores", "Poison clouds spawn stinging thorn vines dealing 20% bonus DPS.", 0.20f, 0.05f);
            RegisterSynergy(MagicSchoolType.Void, MagicSchoolType.Chaos, "Entropy Singularity", "Critical strikes rip mini-rifts pulling enemies in for 30 damage.", 0.25f, 0.10f);
            RegisterSynergy(MagicSchoolType.Wind, MagicSchoolType.Time, "Temporal Acceleration", "Movement speed and cast speed increased by 15%.", 0.15f, 0.15f);
            RegisterSynergy(MagicSchoolType.Spirit, MagicSchoolType.Arcane, "Aetheric Resonance", "Cooldown reduction increased by 10% and mana costs reduced by 15%.", 0.18f, 0.12f);
        }

        private static void RegisterSchool(
            MagicSchoolType school,
            string name,
            string title,
            string description,
            DamageType primaryDamageType,
            string colorHex,
            string passivePerkDescription,
            MagicSchoolType synergyPartner,
            string synergyName,
            string synergyEffectDescription,
            SpellDefinition[] spells,
            SpellDefinition evolvedSpell)
        {
            var def = new MagicSchoolDefinition(
                school,
                name,
                title,
                description,
                primaryDamageType,
                colorHex,
                passivePerkDescription,
                spells,
                bonusDamagePerMasteryRank: 0.10f,
                cooldownReductionPerMasteryRank: 0.05f,
                synergyPartner: synergyPartner,
                synergyName: synergyName,
                synergyEffectDescription: synergyEffectDescription
            );

            _schools[school] = def;

            for (int i = 0; i < spells.Length; i++)
            {
                _spells[spells[i].Id] = spells[i];
            }

            if (evolvedSpell != null)
            {
                _spells[evolvedSpell.Id] = evolvedSpell;
            }
        }

        private static void RegisterSynergy(
            MagicSchoolType a,
            MagicSchoolType b,
            string name,
            string desc,
            float bonusDamage,
            float bonusCdr)
        {
            _synergies.Add(new MultiElementSynergy(a, b, name, desc, bonusDamage, bonusCdr));
        }
    }
}
