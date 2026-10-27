# Necromancy School — Mortis (`MagicSchoolType.Necromancy`) — Audio & SFX Prompt Specification

> **Asset Category:** Magic School Audio #13 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Necromancy` (`MagicSchoolCatalog.cs`)
> **Primary Cue ID:** `CueIds.SpellCastNecromancy` (`"spell_cast_necromancy"`)
> **Synergy Cue ID:** `CueIds.SpellSynergyTrigger` (`"spell_synergy_trigger"`)
> **Evolution Cue ID:** `CueIds.SpellEvolutionUnlock` (`"spell_evolution_unlock"`)

---

## 1. Base Elemental Timbre Identity
- **School Concept:** The dread domain of death, bone shards, and corpse consumption.
- **Acoustic Signature:** Layered elemental foley (`Necromancy`) + FM harmonic crystal/synth transient + sub-bass impact weight scaled by Spell Tier (Tier 1: `0.25s` light snap → Tier 4 Evolved: `1.10s` full-spectrum cataclysm).

---

## 2. Step-by-Step Audio Prompts per Spell Tier

### Tier 1: Bone Spear (`spell_bone_spear` — `DirectProjectile`)
- **Cast & Release Prompt (`sfx_spell_bone_spear_cast.wav` — Mapped to `CueIds.SpellCastNecromancy`):**
  > `Dark fantasy necromancy magic spell sound effect for "Bone Spear" (DirectProjectile): Hurls a calcified bone lance piercing enemy lines. Crisp 10ms elemental ignition transient, textured necromancy body, clean decay, duration 0.30s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_bone_spear_hit.wav`):**
  > `Punchy necromancy spell impact sound applying Soul Feast, 0.25s, mono 44.1kHz WAV.`

### Tier 2: Corpse Explosion (`spell_corpse_explosion` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_corpse_explosion_cast.wav` — Mapped to `CueIds.SpellCastNecromancy`):**
  > `Dark fantasy necromancy magic spell sound effect for "Corpse Explosion" (RadialAoENova): Detonates fallen remains in a lethal shrapnel burst. Crisp 10ms elemental ignition transient, textured necromancy body, clean decay, duration 0.45s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_corpse_explosion_hit.wav`):**
  > `Punchy necromancy spell impact sound applying Soul Feast, 0.25s, mono 44.1kHz WAV.`

### Tier 3: Death Coil (`spell_death_coil` — `HomingSeeker`)
- **Cast & Release Prompt (`sfx_spell_death_coil_cast.wav` — Mapped to `CueIds.SpellCastNecromancy`):**
  > `Dark fantasy necromancy magic spell sound effect for "Death Coil" (HomingSeeker): Unleashes screaming death coils seeking souls. Crisp 10ms elemental ignition transient, textured necromancy body, clean decay, duration 0.70s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_death_coil_hit.wav`):**
  > `Punchy necromancy spell impact sound applying Soul Feast, 0.25s, mono 44.1kHz WAV.`

### Tier 4: The Final Harvest (`spell_reaper_final_harvest` — `RadialAoENova`)
- **Cast & Release Prompt (`sfx_spell_reaper_final_harvest_cast.wav` — Mapped to `CueIds.SpellCastNecromancy`):**
  > `Dark fantasy necromancy magic spell sound effect for "The Final Harvest" (RadialAoENova): Reaps all souls within vicinity into eternal servitude. Crisp 10ms elemental ignition transient, textured necromancy body, clean decay, duration 1.15s, mono 44.1kHz 16-bit WAV.`
- **Impact / Status Proc Prompt (`sfx_spell_reaper_final_harvest_hit.wav`):**
  > `Punchy necromancy spell impact sound applying Soul Feast, 0.25s, mono 44.1kHz WAV.`

### Cross-School Synergy Trigger: Sanguine Lichdom (`CueIds.SpellSynergyTrigger`)
> `Explosive elemental combo chime combining Necromancy and Blood magic (Minions and soul attacks heal the caster for 12% of damage.): resonant harmonic chord flash with heavy elemental burst, 0.55s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Cue Constant | Cue String | Target `.wav` Path | Duration | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastNecromancy` | `"spell_cast_necromancy"` | `Assets/_Game/Audio/SFX/Magic/necromancy/sfx_spell_cast_necromancy.wav` | `0.30s - 1.15s` | `0.92 - 1.08` |
| `CueIds.SpellSynergyTrigger` | `"spell_synergy_trigger"` | `Assets/_Game/Audio/SFX/Magic/necromancy/sfx_synergy_necromancy.wav` | `0.55s` | `0.95 - 1.05` |
| `CueIds.SpellEvolutionUnlock` | `"spell_evolution_unlock"` | `Assets/_Game/Audio/SFX/Magic/sfx_spell_evolution_unlock.wav` | `1.20s` | `1.00` |
