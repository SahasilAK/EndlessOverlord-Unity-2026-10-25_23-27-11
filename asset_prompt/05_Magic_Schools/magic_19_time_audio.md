# Time School — Chronomancy (`MagicSchoolType.Time`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #19 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Time` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastTime` (`"spell_cast_time"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Temporal manipulation accelerating allies and freezing foes in stasis.
- **Acoustic Signature:** Layered elemental foley (`Time`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Chrono Bolt (`spell_chrono_bolt` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_chrono_bolt_cast.wav` — Mapped to `CueIds.SpellCastTime`):**
  > `Dark fantasy time magic spell sound effect for "Chrono Bolt" (DirectProjectile): Fires a temporal distortion slowing target's timeline. Crisp 10ms elemental ignition transient, textured time body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_chrono_bolt_hit.wav`):**
  > `Punchy time spell impact sound applying Chronos Ward, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Temporal Stutter (`spell_temporal_stutter` — `AuraPulse`)
- **Cast & Release Prompt (`sfx_spell_temporal_stutter_cast.wav` — Mapped to `CueIds.SpellCastTime`):**
  > `Dark fantasy time magic spell sound effect for "Temporal Stutter" (AuraPulse): Dilates time locally, hindering approaching enemy strikes. Crisp 10ms elemental ignition transient, textured time body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_temporal_stutter_hit.wav`):**
  > `Punchy time spell impact sound applying Chronos Ward, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Time Dilation Field (`spell_time_dilation_field` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_time_dilation_field_cast.wav` — Mapped to `CueIds.SpellCastTime`):**
  > `Dark fantasy time magic spell sound effect for "Time Dilation Field" (GroundHazard): Creates a chronosphere slowing all invaders by 70%. Crisp 10ms elemental ignition transient, textured time body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_time_dilation_field_hit.wav`):**
  > `Punchy time spell impact sound applying Chronos Ward, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Chronos Temporal Shatter (`spell_chronos_shatter` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_chronos_shatter_cast.wav` — Mapped to `CueIds.SpellCastTime`):**
  > `Dark fantasy time magic spell sound effect for "Chronos Temporal Shatter" (GroundHazard): Freezes enemies in stasis then shatters their timeline. Crisp 10ms elemental ignition transient, textured time body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_chronos_shatter_hit.wav`):**
  > `Punchy time spell impact sound applying Chronos Ward, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Temporal Acceleration (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Time and Wind magic (Movement speed and cast speed increased by 15%.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastTime` | `"spell_cast_time"` | `Assets/_Game/Audio/SFX/Magic/time/sfx_spell_cast_time.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/time/sfx_synergy_time.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
