# Arcane School — Arcanomancy (`MagicSchoolType.Arcane`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #15 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Arcane` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastArcane` (`"spell_cast_arcane"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Pure primordial mana manipulated into disintegrating energy.
- **Acoustic Signature:** Layered elemental foley (`Arcane`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Magic Missile (`spell_magic_missile` — `HomingSeeker`)
- **Cast & Release Prompt (`sfx_spell_magic_missile_cast.wav` — Mapped to `CueIds.SpellCastArcane`):**
  > `Dark fantasy arcane magic spell sound effect for "Magic Missile" (HomingSeeker): Fires tri-force seeker darts of concentrated mana. Crisp 10ms elemental ignition transient, textured arcane body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_magic_missile_hit.wav`):**
  > `Punchy arcane spell impact sound applying Arcane Surge, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Arcane Orb (`spell_arcane_orb` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_arcane_orb_cast.wav` — Mapped to `CueIds.SpellCastArcane`):**
  > `Dark fantasy arcane magic spell sound effect for "Arcane Orb" (DirectProjectile): Hurls a slow pulsating globe emitting arcane pulses. Crisp 10ms elemental ignition transient, textured arcane body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_arcane_orb_hit.wav`):**
  > `Punchy arcane spell impact sound applying Arcane Surge, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Disintegration Ray (`spell_disintegration_ray` — `ContinuousBeam`)
- **Cast & Release Prompt (`sfx_spell_disintegration_ray_cast.wav` — Mapped to `CueIds.SpellCastArcane`):**
  > `Dark fantasy arcane magic spell sound effect for "Disintegration Ray" (ContinuousBeam): Channels a devastating beam vaporizing matter. Crisp 10ms elemental ignition transient, textured arcane body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_disintegration_ray_hit.wav`):**
  > `Punchy arcane spell impact sound applying Arcane Surge, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Nether Singularity (`spell_nether_singularity` — `ContinuousBeam`)
- **Cast & Release Prompt (`sfx_spell_nether_singularity_cast.wav` — Mapped to `CueIds.SpellCastArcane`):**
  > `Dark fantasy arcane magic spell sound effect for "Nether Singularity" (ContinuousBeam): Supreme arcane beam piercing all barriers and shields. Crisp 10ms elemental ignition transient, textured arcane body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_nether_singularity_hit.wav`):**
  > `Punchy arcane spell impact sound applying Arcane Surge, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Aetheric Resonance (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Arcane and Spirit magic (Cooldown reduction increased by 10% and mana costs reduced by 15%.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastArcane` | `"spell_cast_arcane"` | `Assets/_Game/Audio/SFX/Magic/arcane/sfx_spell_cast_arcane.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/arcane/sfx_synergy_arcane.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
