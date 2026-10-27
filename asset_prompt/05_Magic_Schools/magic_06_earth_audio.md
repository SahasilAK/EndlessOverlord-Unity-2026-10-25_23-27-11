# Earth School — Geomancy (`MagicSchoolType.Earth`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #06 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Earth` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastEarth` (`"spell_cast_earth"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Crushing tectonic magic utilizing boulders, tremors, and granite armor.
- **Acoustic Signature:** Layered elemental foley (`Earth`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Stone Barrage (`spell_stone_barrage` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_stone_barrage_cast.wav` — Mapped to `CueIds.SpellCastEarth`):**
  > `Dark fantasy earth magic spell sound effect for "Stone Barrage" (DirectProjectile): Hurls a cluster of dense jagged rocks. Crisp 10ms elemental ignition transient, textured earth body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_stone_barrage_hit.wav`):**
  > `Punchy earth spell impact sound applying Stoneform, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Tremor Quake (`spell_tremor_quake` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_tremor_quake_cast.wav` — Mapped to `CueIds.SpellCastEarth`):**
  > `Dark fantasy earth magic spell sound effect for "Tremor Quake" (RadialAoENova): Slams the ground sending seismic shockwaves. Crisp 10ms elemental ignition transient, textured earth body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_tremor_quake_hit.wav`):**
  > `Punchy earth spell impact sound applying Stoneform, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Boulder Catapult (`spell_boulder_catapult` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_boulder_catapult_cast.wav` — Mapped to `CueIds.SpellCastEarth`):**
  > `Dark fantasy earth magic spell sound effect for "Boulder Catapult" (FallingImpact): Drops a massive granite monolith onto enemy ranks. Crisp 10ms elemental ignition transient, textured earth body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_boulder_catapult_hit.wav`):**
  > `Punchy earth spell impact sound applying Stoneform, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Tectonic Shatter (`spell_tectonic_shatter` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_tectonic_shatter_cast.wav` — Mapped to `CueIds.SpellCastEarth`):**
  > `Dark fantasy earth magic spell sound effect for "Tectonic Shatter" (FallingImpact): Shatters tectonic plates under the entire arena. Crisp 10ms elemental ignition transient, textured earth body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_tectonic_shatter_hit.wav`):**
  > `Punchy earth spell impact sound applying Stoneform, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Tectonic Forge (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Earth and Metal magic (Increases armor by 15% and physical/earth spell damage by 20%.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastEarth` | `"spell_cast_earth"` | `Assets/_Game/Audio/SFX/Magic/earth/sfx_spell_cast_earth.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/earth/sfx_synergy_earth.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
