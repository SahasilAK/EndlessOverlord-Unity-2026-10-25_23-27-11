# Nature School — Vitamancy (`MagicSchoolType.Nature`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #16 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Nature` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastNature` (`"spell_cast_nature"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Vigorous organic growth using brambles, thorn whips, and regenerative sap.
- **Acoustic Signature:** Layered elemental foley (`Nature`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Thorn Whip (`spell_thorn_whip` — `WaveCone`)
- **Cast & Release Prompt (`sfx_spell_thorn_whip_cast.wav` — Mapped to `CueIds.SpellCastNature`):**
  > `Dark fantasy nature magic spell sound effect for "Thorn Whip" (WaveCone): Lashes out with a barbed thorny vine. Crisp 10ms elemental ignition transient, textured nature body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_thorn_whip_hit.wav`):**
  > `Punchy nature spell impact sound applying Regeneration, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Entangling Roots (`spell_entangling_roots` — `GroundHazard`)
- **Cast & Release Prompt (`sfx_spell_entangling_roots_cast.wav` — Mapped to `CueIds.SpellCastNature`):**
  > `Dark fantasy nature magic spell sound effect for "Entangling Roots" (GroundHazard): Roots foes to the spot while thorny vines crush them. Crisp 10ms elemental ignition transient, textured nature body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_entangling_roots_hit.wav`):**
  > `Punchy nature spell impact sound applying Regeneration, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Wrath of the Ancients (`spell_wrath_of_ancients` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_wrath_of_ancients_cast.wav` — Mapped to `CueIds.SpellCastNature`):**
  > `Dark fantasy nature magic spell sound effect for "Wrath of the Ancients" (RadialAoENova): Awakens primeval flora sweeping all intruders away. Crisp 10ms elemental ignition transient, textured nature body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_wrath_of_ancients_hit.wav`):**
  > `Punchy nature spell impact sound applying Regeneration, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Primordial Gaia (`spell_primordial_gaia` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_primordial_gaia_cast.wav` — Mapped to `CueIds.SpellCastNature`):**
  > `Dark fantasy nature magic spell sound effect for "Primordial Gaia" (RadialAoENova): Transforms the arena into a lethal primeval jungle. Crisp 10ms elemental ignition transient, textured nature body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_primordial_gaia_hit.wav`):**
  > `Punchy nature spell impact sound applying Regeneration, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Virulent Spores (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Nature and Poison magic (Poison clouds spawn stinging thorn vines dealing 20% bonus DPS.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastNature` | `"spell_cast_nature"` | `Assets/_Game/Audio/SFX/Magic/nature/sfx_spell_cast_nature.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/nature/sfx_synergy_nature.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
