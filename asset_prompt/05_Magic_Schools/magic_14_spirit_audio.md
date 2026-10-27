# Spirit School — Spiritism (`MagicSchoolType.Spirit`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #14 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Spirit` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastSpirit` (`"spell_cast_spirit"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Astral apparitions that bypass physical armor completely.
- **Acoustic Signature:** Layered elemental foley (`Spirit`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Ethereal Wisp (`spell_ethereal_wisp` — `HomingSeeker`)
- **Cast & Release Prompt (`sfx_spell_ethereal_wisp_cast.wav` — Mapped to `CueIds.SpellCastSpirit`):**
  > `Dark fantasy spirit magic spell sound effect for "Ethereal Wisp" (HomingSeeker): Releases glowing spirit wisps that phase through obstacles. Crisp 10ms elemental ignition transient, textured spirit body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_ethereal_wisp_hit.wav`):**
  > `Punchy spirit spell impact sound applying Ethereal Form, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Spectral Wail (`spell_spectral_wail` — `WaveCone`)
- **Cast & Release Prompt (`sfx_spell_spectral_wail_cast.wav` — Mapped to `CueIds.SpellCastSpirit`):**
  > `Dark fantasy spirit magic spell sound effect for "Spectral Wail" (WaveCone): A chilling ghostly shriek that terrifies foes. Crisp 10ms elemental ignition transient, textured spirit body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_spectral_wail_hit.wav`):**
  > `Punchy spirit spell impact sound applying Ethereal Form, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Astral Projection (`spell_astral_projection` — `OrbitingShield`)
- **Cast & Release Prompt (`sfx_spell_astral_projection_cast.wav` — Mapped to `CueIds.SpellCastSpirit`):**
  > `Dark fantasy spirit magic spell sound effect for "Astral Projection" (OrbitingShield): Summons spectral avatars rotating in defense. Crisp 10ms elemental ignition transient, textured spirit body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_astral_projection_hit.wav`):**
  > `Punchy spirit spell impact sound applying Ethereal Form, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Astral Pandemonium (`spell_spirit_pandemonium` — `OrbitingShield`)
- **Cast & Release Prompt (`sfx_spell_spirit_pandemonium_cast.wav` — Mapped to `CueIds.SpellCastSpirit`):**
  > `Dark fantasy spirit magic spell sound effect for "Astral Pandemonium" (OrbitingShield): A chaotic storm of ancestors rending enemies to bits. Crisp 10ms elemental ignition transient, textured spirit body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_spirit_pandemonium_hit.wav`):**
  > `Punchy spirit spell impact sound applying Ethereal Form, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Aetheric Resonance (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Spirit and Arcane magic (Cooldown reduction increased by 10% and mana costs reduced by 15%.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastSpirit` | `"spell_cast_spirit"` | `Assets/_Game/Audio/SFX/Magic/spirit/sfx_spell_cast_spirit.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/spirit/sfx_synergy_spirit.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
