# Holy School — Theomancy (`MagicSchoolType.Holy`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #11 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Holy` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastHoly` (`"spell_cast_holy"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Sacred divine judgment that purges darkness and protects allies.
- **Acoustic Signature:** Layered elemental foley (`Holy`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Holy Smite (`spell_holy_smite` — `FallingImpact`)
- **Cast & Release Prompt (`sfx_spell_holy_smite_cast.wav` — Mapped to `CueIds.SpellCastHoly`):**
  > `Dark fantasy holy magic spell sound effect for "Holy Smite" (FallingImpact): Strikes a foe with sacred judgment from above. Crisp 10ms elemental ignition transient, textured holy body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_holy_smite_hit.wav`):**
  > `Punchy holy spell impact sound applying Divine Ward, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Consecrated Aura (`spell_consecrated_aura` — `AuraPulse`)
- **Cast & Release Prompt (`sfx_spell_consecrated_aura_cast.wav` — Mapped to `CueIds.SpellCastHoly`):**
  > `Dark fantasy holy magic spell sound effect for "Consecrated Aura" (AuraPulse): Sanctifies ground around caster, burning impurities. Crisp 10ms elemental ignition transient, textured holy body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_consecrated_aura_hit.wav`):**
  > `Punchy holy spell impact sound applying Divine Ward, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Divine Wrath (`spell_divine_wrath` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_divine_wrath_cast.wav` — Mapped to `CueIds.SpellCastHoly`):**
  > `Dark fantasy holy magic spell sound effect for "Divine Wrath" (RadialAoENova): Heavens descend in a brilliant blast of righteous fury. Crisp 10ms elemental ignition transient, textured holy body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_divine_wrath_hit.wav`):**
  > `Punchy holy spell impact sound applying Divine Ward, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Judgment of the Archons (`spell_judgment_of_archons` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_judgment_of_archons_cast.wav` — Mapped to `CueIds.SpellCastHoly`):**
  > `Dark fantasy holy magic spell sound effect for "Judgment of the Archons" (RadialAoENova): Archangels' wrath obliterating evil across the sanctuary. Crisp 10ms elemental ignition transient, textured holy body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_judgment_of_archons_hit.wav`):**
  > `Punchy holy spell impact sound applying Divine Ward, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Celestial Radiance (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Holy and Light magic (Radiant and holy spells cleanse slow and heal for 5% max HP on cast.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastHoly` | `"spell_cast_holy"` | `Assets/_Game/Audio/SFX/Magic/holy/sfx_spell_cast_holy.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/holy/sfx_synergy_holy.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
