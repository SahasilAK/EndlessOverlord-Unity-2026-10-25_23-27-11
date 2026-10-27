# Fire School — Pyromancy (`MagicSchoolType.Fire`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #01 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Fire` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastFire` (`"spell_cast_fire"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Devastating incendiary magic that burns foes and spreads conflagration.
- **Acoustic Signature:** Layered elemental foley (`Fire`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Fire Bolt (`spell_fire_bolt` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_fire_bolt_cast.wav` — Mapped to `CueIds.SpellCastFire`):**
  > `Dark fantasy fire magic spell sound effect for "Fire Bolt" (DirectProjectile): Hurls a blazing bolt of fire. Crisp 10ms elemental ignition transient, textured fire body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_fire_bolt_hit.wav`):**
  > `Punchy fire spell impact sound applying Ignite, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Flame Wave (`spell_flame_wave` — `WaveCone`)
- **Cast & Release Prompt (`sfx_spell_flame_wave_cast.wav` — Mapped to `CueIds.SpellCastFire`):**
  > `Dark fantasy fire magic spell sound effect for "Flame Wave" (WaveCone): Unleashes a sweeping crescent of flame. Crisp 10ms elemental ignition transient, textured fire body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_flame_wave_hit.wav`):**
  > `Punchy fire spell impact sound applying Ignite, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Meteor Strike (`spell_meteor_strike` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_meteor_strike_cast.wav` — Mapped to `CueIds.SpellCastFire`):**
  > `Dark fantasy fire magic spell sound effect for "Meteor Strike" (FallingImpact): Calls down a colossal flaming meteor from the cosmos. Crisp 10ms elemental ignition transient, textured fire body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_meteor_strike_hit.wav`):**
  > `Punchy fire spell impact sound applying Ignite, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Inferno Cataclysm (`spell_inferno_cataclysm` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_inferno_cataclysm_cast.wav` — Mapped to `CueIds.SpellCastFire`):**
  > `Dark fantasy fire magic spell sound effect for "Inferno Cataclysm" (FallingImpact): Ascended cataclysm raining a cluster of superheated meteors. Crisp 10ms elemental ignition transient, textured fire body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_inferno_cataclysm_hit.wav`):**
  > `Punchy fire spell impact sound applying Ignite, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Thermal Shock (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Fire and Ice magic (Striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastFire` | `"spell_cast_fire"` | `Assets/_Game/Audio/SFX/Magic/fire/sfx_spell_cast_fire.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/fire/sfx_synergy_fire.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
