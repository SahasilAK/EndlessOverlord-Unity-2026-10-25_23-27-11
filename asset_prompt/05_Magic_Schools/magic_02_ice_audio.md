# Ice School — Cryomancy (`MagicSchoolType.Ice`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #02 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Ice` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastIce` (`"spell_cast_ice"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Frigid control spells that slow enemy movement and crystallize armor.
- **Acoustic Signature:** Layered elemental foley (`Ice`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Frost Shard (`spell_frost_shard` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_frost_shard_cast.wav` — Mapped to `CueIds.SpellCastIce`):**
  > `Dark fantasy ice magic spell sound effect for "Frost Shard" (DirectProjectile): Fires a sharp icicle that pierces one target. Crisp 10ms elemental ignition transient, textured ice body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_frost_shard_hit.wav`):**
  > `Punchy ice spell impact sound applying Chill, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Glacial Spike (`spell_glacial_spike` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_glacial_spike_cast.wav` — Mapped to `CueIds.SpellCastIce`):**
  > `Dark fantasy ice magic spell sound effect for "Glacial Spike" (GroundHazard): Erupts sharp glacier spikes from the permafrost. Crisp 10ms elemental ignition transient, textured ice body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_glacial_spike_hit.wav`):**
  > `Punchy ice spell impact sound applying Chill, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Blizzard Storm (`spell_blizzard_storm` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_blizzard_storm_cast.wav` — Mapped to `CueIds.SpellCastIce`):**
  > `Dark fantasy ice magic spell sound effect for "Blizzard Storm" (RadialAoENova): Summons a violent sub-zero blizzard around the caster. Crisp 10ms elemental ignition transient, textured ice body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_blizzard_storm_hit.wav`):**
  > `Punchy ice spell impact sound applying Chill, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Absolute Zero (`spell_absolute_zero` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_absolute_zero_cast.wav` — Mapped to `CueIds.SpellCastIce`):**
  > `Dark fantasy ice magic spell sound effect for "Absolute Zero" (RadialAoENova): Flash-freezes all surrounding existence to absolute zero. Crisp 10ms elemental ignition transient, textured ice body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_absolute_zero_hit.wav`):**
  > `Punchy ice spell impact sound applying Chill, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Thermal Shock (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Ice and Fire magic (Striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastIce` | `"spell_cast_ice"` | `Assets/_Game/Audio/SFX/Magic/ice/sfx_spell_cast_ice.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/ice/sfx_synergy_ice.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
