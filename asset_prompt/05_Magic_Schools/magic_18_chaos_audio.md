# Chaos School — Pandemonium (`MagicSchoolType.Chaos`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #18 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Chaos` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastChaos` (`"spell_cast_chaos"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Wild unchained entropic magic with variable and explosive results.
- **Acoustic Signature:** Layered elemental foley (`Chaos`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Chaos Bolt (`spell_chaos_bolt` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_chaos_bolt_cast.wav` — Mapped to `CueIds.SpellCastChaos`):**
  > `Dark fantasy chaos magic spell sound effect for "Chaos Bolt" (DirectProjectile): Fires an erratic bolt surging with unpredictable fury. Crisp 10ms elemental ignition transient, textured chaos body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_chaos_bolt_hit.wav`):**
  > `Punchy chaos spell impact sound applying Wild Surge, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Entropic Flux (`spell_entropic_flux` — `AuraPulse`)
- **Cast & Release Prompt (`sfx_spell_entropic_flux_cast.wav` — Mapped to `CueIds.SpellCastChaos`):**
  > `Dark fantasy chaos magic spell sound effect for "Entropic Flux" (AuraPulse): Pulses random elemental damage waves around caster. Crisp 10ms elemental ignition transient, textured chaos body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_entropic_flux_hit.wav`):**
  > `Punchy chaos spell impact sound applying Wild Surge, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Cosmic Anarchy (`spell_cosmic_anarchy` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_cosmic_anarchy_cast.wav` — Mapped to `CueIds.SpellCastChaos`):**
  > `Dark fantasy chaos magic spell sound effect for "Cosmic Anarchy" (RadialAoENova): Unleashes multi-elemental explosive devastation. Crisp 10ms elemental ignition transient, textured chaos body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_cosmic_anarchy_hit.wav`):**
  > `Punchy chaos spell impact sound applying Wild Surge, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Entropy Unbound (`spell_entropy_unbound` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_entropy_unbound_cast.wav` — Mapped to `CueIds.SpellCastChaos`):**
  > `Dark fantasy chaos magic spell sound effect for "Entropy Unbound" (RadialAoENova): Catastrophic breakdown of natural laws in target zone. Crisp 10ms elemental ignition transient, textured chaos body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_entropy_unbound_hit.wav`):**
  > `Punchy chaos spell impact sound applying Wild Surge, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Entropy Singularity (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Chaos and Void magic (Critical strikes rip mini-rifts pulling enemies in for 30 damage.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastChaos` | `"spell_cast_chaos"` | `Assets/_Game/Audio/SFX/Magic/chaos/sfx_spell_cast_chaos.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/chaos/sfx_synergy_chaos.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
