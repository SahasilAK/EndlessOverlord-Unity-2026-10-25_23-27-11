# Shadow School — Umbromancy (`MagicSchoolType.Shadow`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #09 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Shadow` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastShadow` (`"spell_cast_shadow"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Elusive occult arts weakening foes from the darkness.
- **Acoustic Signature:** Layered elemental foley (`Shadow`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Shadow Tendril (`spell_shadow_tendril` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_shadow_tendril_cast.wav` — Mapped to `CueIds.SpellCastShadow`):**
  > `Dark fantasy shadow magic spell sound effect for "Shadow Tendril" (DirectProjectile): Lashes out with an ethereal tendril of darkness. Crisp 10ms elemental ignition transient, textured shadow body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_shadow_tendril_hit.wav`):**
  > `Punchy shadow spell impact sound applying Shadowstep, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Umbral Veil (`spell_umbral_veil` — `AuraPulse`)
- **Cast & Release Prompt (`sfx_spell_umbral_veil_cast.wav` — Mapped to `CueIds.SpellCastShadow`):**
  > `Dark fantasy shadow magic spell sound effect for "Umbral Veil" (AuraPulse): Pulses shadows dealing harm and concealing caster. Crisp 10ms elemental ignition transient, textured shadow body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_umbral_veil_hit.wav`):**
  > `Punchy shadow spell impact sound applying Shadowstep, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Black Sun (`spell_black_sun` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_black_sun_cast.wav` — Mapped to `CueIds.SpellCastShadow`):**
  > `Dark fantasy shadow magic spell sound effect for "Black Sun" (RadialAoENova): Manifests an eclipsing black star that consumes light. Crisp 10ms elemental ignition transient, textured shadow body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_black_sun_hit.wav`):**
  > `Punchy shadow spell impact sound applying Shadowstep, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Umbral Event Horizon (`spell_event_horizon_umbra` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_event_horizon_umbra_cast.wav` — Mapped to `CueIds.SpellCastShadow`):**
  > `Dark fantasy shadow magic spell sound effect for "Umbral Event Horizon" (RadialAoENova): Complete shadow collapse sucking light and life. Crisp 10ms elemental ignition transient, textured shadow body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_event_horizon_umbra_hit.wav`):**
  > `Punchy shadow spell impact sound applying Shadowstep, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Abyssal Eclipse (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Shadow and Dark magic (Dark and shadow spells have 15% chance to instantly execute non-bosses below 15% HP.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastShadow` | `"spell_cast_shadow"` | `Assets/_Game/Audio/SFX/Magic/shadow/sfx_spell_cast_shadow.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/shadow/sfx_synergy_shadow.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
