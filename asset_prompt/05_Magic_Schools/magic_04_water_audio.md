# Water School — Hydromancy (`MagicSchoolType.Water`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #04 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Water` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastWater` (`"spell_cast_water"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Fluid kinetic magic that knocks back foes and soaks the arena.
- **Acoustic Signature:** Layered elemental foley (`Water`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Water Jet (`spell_water_jet` — `ContinuousBeam`)
- **Cast & Release Prompt (`sfx_spell_water_jet_cast.wav` — Mapped to `CueIds.SpellCastWater`):**
  > `Dark fantasy water magic spell sound effect for "Water Jet" (ContinuousBeam): Channels a focused high-pressure beam of water. Crisp 10ms elemental ignition transient, textured water body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_water_jet_hit.wav`):**
  > `Punchy water spell impact sound applying Drenched, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Tidal Surge (`spell_tidal_surge` — `WaveCone`)
- **Cast & Release Prompt (`sfx_spell_tidal_surge_cast.wav` — Mapped to `CueIds.SpellCastWater`):**
  > `Dark fantasy water magic spell sound effect for "Tidal Surge" (WaveCone): Pushes enemies back with a rushing wave. Crisp 10ms elemental ignition transient, textured water body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_tidal_surge_hit.wav`):**
  > `Punchy water spell impact sound applying Drenched, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Maelstrom Vortex (`spell_maelstrom_vortex` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_maelstrom_vortex_cast.wav` — Mapped to `CueIds.SpellCastWater`):**
  > `Dark fantasy water magic spell sound effect for "Maelstrom Vortex" (RadialAoENova): Forms a roaring oceanic whirlpool dragging enemies inward. Crisp 10ms elemental ignition transient, textured water body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_maelstrom_vortex_hit.wav`):**
  > `Punchy water spell impact sound applying Drenched, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Deluge of the Abyss (`spell_deluge_abyss` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_deluge_abyss_cast.wav` — Mapped to `CueIds.SpellCastWater`):**
  > `Dark fantasy water magic spell sound effect for "Deluge of the Abyss" (RadialAoENova): Floods the arena with crushing abyssal water pressures. Crisp 10ms elemental ignition transient, textured water body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_deluge_abyss_hit.wav`):**
  > `Punchy water spell impact sound applying Drenched, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Electrified Deluge (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Water and Lightning magic (Lightning spells chain to +2 additional targets in wet areas.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastWater` | `"spell_cast_water"` | `Assets/_Game/Audio/SFX/Magic/water/sfx_spell_cast_water.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/water/sfx_synergy_water.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
