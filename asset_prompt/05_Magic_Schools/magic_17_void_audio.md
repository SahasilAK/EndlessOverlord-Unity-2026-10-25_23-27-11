# Void School — Void Magic (`MagicSchoolType.Void`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #17 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Void` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastVoid` (`"spell_cast_void"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Dimensional rips erasing physical matter from existence.
- **Acoustic Signature:** Layered elemental foley (`Void`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Void Rift (`spell_void_rift` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_void_rift_cast.wav` — Mapped to `CueIds.SpellCastVoid`):**
  > `Dark fantasy void magic spell sound effect for "Void Rift" (GroundHazard): Rips open a localized dimensional tear. Crisp 10ms elemental ignition transient, textured void body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_void_rift_hit.wav`):**
  > `Punchy void spell impact sound applying Null Shield, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Null Lance (`spell_null_lance` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_null_lance_cast.wav` — Mapped to `CueIds.SpellCastVoid`):**
  > `Dark fantasy void magic spell sound effect for "Null Lance" (DirectProjectile): Fires a beam ignoring 100% of armor and barriers. Crisp 10ms elemental ignition transient, textured void body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_null_lance_hit.wav`):**
  > `Punchy void spell impact sound applying Null Shield, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Dimensional Collapse (`spell_dimensional_collapse` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_dimensional_collapse_cast.wav` — Mapped to `CueIds.SpellCastVoid`):**
  > `Dark fantasy void magic spell sound effect for "Dimensional Collapse" (FallingImpact): Collapses spatial dimensions onto the impact zone. Crisp 10ms elemental ignition transient, textured void body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_dimensional_collapse_hit.wav`):**
  > `Punchy void spell impact sound applying Null Shield, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Zero-Point Cascade (`spell_zero_point_cascade` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_zero_point_cascade_cast.wav` — Mapped to `CueIds.SpellCastVoid`):**
  > `Dark fantasy void magic spell sound effect for "Zero-Point Cascade" (FallingImpact): Zero-point collapse obliterating everything in range. Crisp 10ms elemental ignition transient, textured void body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_zero_point_cascade_hit.wav`):**
  > `Punchy void spell impact sound applying Null Shield, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Entropy Singularity (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Void and Chaos magic (Critical strikes rip mini-rifts pulling enemies in for 30 damage.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastVoid` | `"spell_cast_void"` | `Assets/_Game/Audio/SFX/Magic/void/sfx_spell_cast_void.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/void/sfx_synergy_void.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
