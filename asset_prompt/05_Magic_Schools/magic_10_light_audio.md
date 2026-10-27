# Light School — Photomancy (`MagicSchoolType.Light`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #10 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Light` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastLight` (`"spell_cast_light"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Blinding beams and illuminating surges that disintegrate corruption.
- **Acoustic Signature:** Layered elemental foley (`Light`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Radiant Beam (`spell_radiant_beam` — `ContinuousBeam`)
- **Cast & Release Prompt (`sfx_spell_radiant_beam_cast.wav` — Mapped to `CueIds.SpellCastLight`):**
  > `Dark fantasy light magic spell sound effect for "Radiant Beam" (ContinuousBeam): Focuses a piercing ray of pure daylight. Crisp 10ms elemental ignition transient, textured light body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_radiant_beam_hit.wav`):**
  > `Punchy light spell impact sound applying Luminescence, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Prismatic Lance (`spell_prismatic_lance` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_prismatic_lance_cast.wav` — Mapped to `CueIds.SpellCastLight`):**
  > `Dark fantasy light magic spell sound effect for "Prismatic Lance" (DirectProjectile): Projects a refracting spear of multi-spectrum light. Crisp 10ms elemental ignition transient, textured light body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_prismatic_lance_hit.wav`):**
  > `Punchy light spell impact sound applying Luminescence, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Solar Flare (`spell_solar_flare` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_solar_flare_cast.wav` — Mapped to `CueIds.SpellCastLight`):**
  > `Dark fantasy light magic spell sound effect for "Solar Flare" (RadialAoENova): Erupts a blinding solar nova igniting all foes. Crisp 10ms elemental ignition transient, textured light body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_solar_flare_hit.wav`):**
  > `Punchy light spell impact sound applying Luminescence, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Supernova Burst (`spell_supernova_burst` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_supernova_burst_cast.wav` — Mapped to `CueIds.SpellCastLight`):**
  > `Dark fantasy light magic spell sound effect for "Supernova Burst" (RadialAoENova): Unleashes the blinding ignition of a newborn star. Crisp 10ms elemental ignition transient, textured light body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_supernova_burst_hit.wav`):**
  > `Punchy light spell impact sound applying Luminescence, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Celestial Radiance (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Light and Holy magic (Radiant and holy spells cleanse slow and heal for 5% max HP on cast.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastLight` | `"spell_cast_light"` | `Assets/_Game/Audio/SFX/Magic/light/sfx_spell_cast_light.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/light/sfx_synergy_light.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
