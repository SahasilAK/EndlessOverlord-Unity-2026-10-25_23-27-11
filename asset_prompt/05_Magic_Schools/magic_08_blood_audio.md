# Blood School — Haemomancy (`MagicSchoolType.Blood`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #08 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Blood` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastBlood` (`"spell_cast_blood"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** Forbidden vitality magic draining life force from enemies to heal the caster.
- **Acoustic Signature:** Layered elemental foley (`Blood`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Blood Lance (`spell_blood_lance` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_blood_lance_cast.wav` — Mapped to `CueIds.SpellCastBlood`):**
  > `Dark fantasy blood magic spell sound effect for "Blood Lance" (DirectProjectile): Fires a coagulated needle of vampiric blood. Crisp 10ms elemental ignition transient, textured blood body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_blood_lance_hit.wav`):**
  > `Punchy blood spell impact sound applying Sanguine Thirst, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Crimson Siphon (`spell_crimson_siphon` — `ContinuousBeam`)
- **Cast & Release Prompt (`sfx_spell_crimson_siphon_cast.wav` — Mapped to `CueIds.SpellCastBlood`):**
  > `Dark fantasy blood magic spell sound effect for "Crimson Siphon" (ContinuousBeam): Extracts enemy blood directly into caster's essence. Crisp 10ms elemental ignition transient, textured blood body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_crimson_siphon_hit.wav`):**
  > `Punchy blood spell impact sound applying Sanguine Thirst, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Exsanguinate Burst (`spell_exsanguinate_burst` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_exsanguinate_burst_cast.wav` — Mapped to `CueIds.SpellCastBlood`):**
  > `Dark fantasy blood magic spell sound effect for "Exsanguinate Burst" (RadialAoENova): Detonates the blood vessels of surrounding invaders. Crisp 10ms elemental ignition transient, textured blood body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_exsanguinate_burst_hit.wav`):**
  > `Punchy blood spell impact sound applying Sanguine Thirst, 0.25s, mono 44.1kHz WAV.`

### Tier 4: Sanguine Ascendance (`spell_sanguine_ascendance` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_sanguine_ascendance_cast.wav` — Mapped to `CueIds.SpellCastBlood`):**
  > `Dark fantasy blood magic spell sound effect for "Sanguine Ascendance" (RadialAoENova): Ascends into a blood avatar draining the battlefield. Crisp 10ms elemental ignition transient, textured blood body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_sanguine_ascendance_hit.wav`):**
  > `Punchy blood spell impact sound applying Sanguine Thirst, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Sanguine Lichdom (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Blood and Necromancy magic (Minions and soul attacks heal the caster for 12% of damage.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastBlood` | `"spell_cast_blood"` | `Assets/_Game/Audio/SFX/Magic/blood/sfx_spell_cast_blood.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/blood/sfx_synergy_blood.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
