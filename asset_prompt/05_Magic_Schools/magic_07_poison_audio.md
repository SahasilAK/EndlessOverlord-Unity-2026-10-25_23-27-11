# Poison School — Toximancy (`MagicSchoolType.Poison`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #07 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Poison` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastPoison` (`"spell_cast_poison"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Virulent corrosive bile that eats through armor and inflicts stacking toxins.
- **Acoustic Signature:** Layered elemental foley (`Poison`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Toxic Spitter (`spell_toxic_spitter` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_toxic_spitter_cast.wav` — Mapped to `CueIds.SpellCastPoison`):**
  > `Dark fantasy poison magic spell sound effect for "Toxic Spitter" (DirectProjectile): Spits caustic acid burning flesh on contact. Crisp 10ms elemental ignition transient, textured poison body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_toxic_spitter_hit.wav`):**
  > `Punchy poison spell impact sound applying Corrosion, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Venom Cloud (`spell_venom_cloud` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_venom_cloud_cast.wav` — Mapped to `CueIds.SpellCastPoison`):**
  > `Dark fantasy poison magic spell sound effect for "Venom Cloud" (GroundHazard): Creates a toxic miasma choking all who enter. Crisp 10ms elemental ignition transient, textured poison body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_venom_cloud_hit.wav`):**
  > `Punchy poison spell impact sound applying Corrosion, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Plague Infestation (`spell_plague_infestation` — `ChainingArc`)
- **Cast & Release Prompt (`sfx_spell_plague_infestation_cast.wav` — Mapped to `CueIds.SpellCastPoison`):**
  > `Dark fantasy poison magic spell sound effect for "Plague Infestation" (ChainingArc): Injects virulent disease leaping violently between hosts. Crisp 10ms elemental ignition transient, textured poison body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_plague_infestation_hit.wav`):**
  > `Punchy poison spell impact sound applying Corrosion, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Noxious Extinction (`spell_nox_extinction` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_nox_extinction_cast.wav` — Mapped to `CueIds.SpellCastPoison`):**
  > `Dark fantasy poison magic spell sound effect for "Noxious Extinction" (GroundHazard): Spreads an apocalyptic airborne neurotoxin. Crisp 10ms elemental ignition transient, textured poison body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_nox_extinction_hit.wav`):**
  > `Punchy poison spell impact sound applying Corrosion, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Virulent Spores (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Poison and Nature magic (Poison clouds spawn stinging thorn vines dealing 20% bonus DPS.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastPoison` | `"spell_cast_poison"` | `Assets/_Game/Audio/SFX/Magic/poison/sfx_spell_cast_poison.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/poison/sfx_synergy_poison.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
