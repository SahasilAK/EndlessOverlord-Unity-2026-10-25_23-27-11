# Metal School — Ferromancy (`MagicSchoolType.Metal`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #20 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Metal` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastMetal` (`"spell_cast_metal"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Magnetic telekinesis flinging deadly blades and crushing iron maidens.
- **Acoustic Signature:** Layered elemental foley (`Metal`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Shrapnel Shot (`spell_shrapnel_shot` — `WaveCone`)
- **Cast & Release Prompt (`sfx_spell_shrapnel_shot_cast.wav` — Mapped to `CueIds.SpellCastMetal`):**
  > `Dark fantasy metal magic spell sound effect for "Shrapnel Shot" (WaveCone): Blasts a conical cone of iron flechettes. Crisp 10ms elemental ignition transient, textured metal body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_shrapnel_shot_hit.wav`):**
  > `Punchy metal spell impact sound applying Iron Skin, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Blade Orbit (`spell_blade_orbit` — `OrbitingShield`)
- **Cast & Release Prompt (`sfx_spell_blade_orbit_cast.wav` — Mapped to `CueIds.SpellCastMetal`):**
  > `Dark fantasy metal magic spell sound effect for "Blade Orbit" (OrbitingShield): Spins whirling steel blades orbiting around caster. Crisp 10ms elemental ignition transient, textured metal body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_blade_orbit_hit.wav`):**
  > `Punchy metal spell impact sound applying Iron Skin, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Iron Maiden Crush (`spell_iron_maiden_crush` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_iron_maiden_crush_cast.wav` — Mapped to `CueIds.SpellCastMetal`):**
  > `Dark fantasy metal magic spell sound effect for "Iron Maiden Crush" (FallingImpact): Calls down a colossal iron maiden slamming shut on enemies. Crisp 10ms elemental ignition transient, textured metal body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_iron_maiden_crush_hit.wav`):**
  > `Punchy metal spell impact sound applying Iron Skin, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Colossus God Anvil (`spell_colossus_anvil` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_colossus_anvil_cast.wav` — Mapped to `CueIds.SpellCastMetal`):**
  > `Dark fantasy metal magic spell sound effect for "Colossus God Anvil" (FallingImpact): An astronomical forge anvil crushing all foes below. Crisp 10ms elemental ignition transient, textured metal body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_colossus_anvil_hit.wav`):**
  > `Punchy metal spell impact sound applying Iron Skin, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Tectonic Forge (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Metal and Earth magic (Increases armor by 15% and physical/earth spell damage by 20%.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastMetal` | `"spell_cast_metal"` | `Assets/_Game/Audio/SFX/Magic/metal/sfx_spell_cast_metal.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/metal/sfx_synergy_metal.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
