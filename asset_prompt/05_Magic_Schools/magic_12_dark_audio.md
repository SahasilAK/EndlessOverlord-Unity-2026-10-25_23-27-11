# Dark School — Noctumancy (`MagicSchoolType.Dark`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #12 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Dark` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastDark` (`"spell_cast_dark"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Abyssal entropy that corrodes willpower and pulls matter into nothingness.
- **Acoustic Signature:** Layered elemental foley (`Dark`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Dark Matter (`spell_dark_matter` — `HomingSeeker`)
- **Cast & Release Prompt (`sfx_spell_dark_matter_cast.wav` — Mapped to `CueIds.SpellCastDark`):**
  > `Dark fantasy dark magic spell sound effect for "Dark Matter" (HomingSeeker): Launches seeking globules of heavy dark matter. Crisp 10ms elemental ignition transient, textured dark body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_dark_matter_hit.wav`):**
  > `Punchy dark spell impact sound applying Despair, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Abyssal Grasp (`spell_abyssal_grasp` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_abyssal_grasp_cast.wav` — Mapped to `CueIds.SpellCastDark`):**
  > `Dark fantasy dark magic spell sound effect for "Abyssal Grasp" (GroundHazard): Summons grasping shadows rooting foes to the floor. Crisp 10ms elemental ignition transient, textured dark body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_abyssal_grasp_hit.wav`):**
  > `Punchy dark spell impact sound applying Despair, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Singularity Tear (`spell_singularity_tear` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_singularity_tear_cast.wav` — Mapped to `CueIds.SpellCastDark`):**
  > `Dark fantasy dark magic spell sound effect for "Singularity Tear" (FallingImpact): Tears open a pocket singularity swallowing reality. Crisp 10ms elemental ignition transient, textured dark body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_singularity_tear_hit.wav`):**
  > `Punchy dark spell impact sound applying Despair, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Oblivion Core (`spell_void_oblivion_core` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_void_oblivion_core_cast.wav` — Mapped to `CueIds.SpellCastDark`):**
  > `Dark fantasy dark magic spell sound effect for "Oblivion Core" (FallingImpact): An inescapable micro-black hole devouring target zone. Crisp 10ms elemental ignition transient, textured dark body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_void_oblivion_core_hit.wav`):**
  > `Punchy dark spell impact sound applying Despair, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Abyssal Eclipse (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Dark and Shadow magic (Dark and shadow spells have 15% chance to instantly execute non-bosses below 15% HP.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastDark` | `"spell_cast_dark"` | `Assets/_Game/Audio/SFX/Magic/dark/sfx_spell_cast_dark.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/dark/sfx_synergy_dark.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
