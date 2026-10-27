# Wind School — Aeromancy (`MagicSchoolType.Wind`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #05 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Wind` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastWind` (`"spell_cast_wind"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** High-speed vacuum blades and defensive squalls enhancing mobility.
- **Acoustic Signature:** Layered elemental foley (`Wind`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Gale Blade (`spell_gale_blade` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_gale_blade_cast.wav` — Mapped to `CueIds.SpellCastWind`):**
  > `Dark fantasy wind magic spell sound effect for "Gale Blade" (DirectProjectile): Fling a slicing blade of razor-sharp wind. Crisp 10ms elemental ignition transient, textured wind body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_gale_blade_hit.wav`):**
  > `Punchy wind spell impact sound applying Tailwind, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Cyclone Barrier (`spell_cyclone_barrier` — `OrbitingShield`)
- **Cast & Release Prompt (`sfx_spell_cyclone_barrier_cast.wav` — Mapped to `CueIds.SpellCastWind`):**
  > `Dark fantasy wind magic spell sound effect for "Cyclone Barrier" (OrbitingShield): Spins defensive air currents that shred close foes. Crisp 10ms elemental ignition transient, textured wind body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_cyclone_barrier_hit.wav`):**
  > `Punchy wind spell impact sound applying Tailwind, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Twister Squall (`spell_twister_squall` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_twister_squall_cast.wav` — Mapped to `CueIds.SpellCastWind`):**
  > `Dark fantasy wind magic spell sound effect for "Twister Squall" (GroundHazard): Summons a roaming tornado tossing foes into chaos. Crisp 10ms elemental ignition transient, textured wind body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_twister_squall_hit.wav`):**
  > `Punchy wind spell impact sound applying Tailwind, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Tempest Monsoon (`spell_tempest_monsoon` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_tempest_monsoon_cast.wav` — Mapped to `CueIds.SpellCastWind`):**
  > `Dark fantasy wind magic spell sound effect for "Tempest Monsoon" (GroundHazard): A raging gale cyclone covering massive ground. Crisp 10ms elemental ignition transient, textured wind body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_tempest_monsoon_hit.wav`):**
  > `Punchy wind spell impact sound applying Tailwind, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Temporal Acceleration (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Wind and Time magic (Movement speed and cast speed increased by 15%.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastWind` | `"spell_cast_wind"` | `Assets/_Game/Audio/SFX/Magic/wind/sfx_spell_cast_wind.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/wind/sfx_synergy_wind.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
