# Lightning School — Electromancy (`MagicSchoolType.Lightning`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #03 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Lightning` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastLightning` (`"spell_cast_lightning"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** High-voltage electricity that arcs rapidly between clustered targets.
- **Acoustic Signature:** Layered elemental foley (`Lightning`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Spark Arc (`spell_spark_arc` — `ChainingArc`)
- **Cast & Release Prompt (`sfx_spell_spark_arc_cast.wav` — Mapped to `CueIds.SpellCastLightning`):**
  > `Dark fantasy lightning magic spell sound effect for "Spark Arc" (ChainingArc): Discharges an arc of electricity jumping between targets. Crisp 10ms elemental ignition transient, textured lightning body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_spark_arc_hit.wav`):**
  > `Punchy lightning spell impact sound applying Static Charge, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Ball Lightning (`spell_ball_lightning` — `HomingSeeker`)
- **Cast & Release Prompt (`sfx_spell_ball_lightning_cast.wav` — Mapped to `CueIds.SpellCastLightning`):**
  > `Dark fantasy lightning magic spell sound effect for "Ball Lightning" (HomingSeeker): Releases slow-moving homing globes of concentrated plasma. Crisp 10ms elemental ignition transient, textured lightning body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_ball_lightning_hit.wav`):**
  > `Punchy lightning spell impact sound applying Static Charge, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Thunder Strike (`spell_thunder_strike` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_thunder_strike_cast.wav` — Mapped to `CueIds.SpellCastLightning`):**
  > `Dark fantasy lightning magic spell sound effect for "Thunder Strike" (FallingImpact): Strikes the battlefield with a deafening bolt from the heavens. Crisp 10ms elemental ignition transient, textured lightning body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_thunder_strike_hit.wav`):**
  > `Punchy lightning spell impact sound applying Static Charge, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Cataclysm Tempest (`spell_cataclysm_tempest` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_cataclysm_tempest_cast.wav` — Mapped to `CueIds.SpellCastLightning`):**
  > `Dark fantasy lightning magic spell sound effect for "Cataclysm Tempest" (FallingImpact): A divine tempest unleashing relentless lightning cascades. Crisp 10ms elemental ignition transient, textured lightning body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_cataclysm_tempest_hit.wav`):**
  > `Punchy lightning spell impact sound applying Static Charge, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Electrified Deluge (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Lightning and Water magic (Lightning spells chain to +2 additional targets in wet areas.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastLightning` | `"spell_cast_lightning"` | `Assets/_Game/Audio/SFX/Magic/lightning/sfx_spell_cast_lightning.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/lightning/sfx_synergy_lightning.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
