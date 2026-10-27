# Archmage Ignis, The Pyre Sovereign (`boss_ignis`) — Legendary Boss Audio & SFX Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss Audio #03 (`04_Bosses`)
> **Catalog ID:** `"boss_ignis"` (`LegendaryBossCatalog.cs`)
> **Primary Ability Cue:** `CueIds.BossAbilityMolten` (`"boss_ability_molten"`)
> **Secondary Ability Cue:** `CueIds.BossAbilitySlam` (`"boss_ability_slam"`)

---

## 1. Base Sound Identity & Mix Priority
- **Sonic Authority:** Milestone bosses command **top mix priority (`-1.0 dBFS`)** alongside the Player Overlord. Every boss spawn triggers a dramatic brass/choir sting (`CueIds.BossAppearance`) and ducks regular invader SFX by `-4 dB` for 1.5s.
- **Character Timbre:** Heroic low-brass motif + heavy masterwork `FireMage` foley + signature `MoltenEruption` / `ShockwaveSlam` elemental layers.

---

## 2. Step-by-Step Audio Variations

1. **Boss Arena Entrance Fanfare (`sfx_boss_ignis_appearance.wav` — `CueIds.BossAppearance` / `"boss_appearance"`):**
   > `Dramatic dark-fantasy milestone boss entrance sting for Archmage Ignis (The Pyre Sovereign): booming war-drum hit, ominous brass horn chord, and metallic FireMage weapon unsheathing ring, 1.4s, stereo 44.1kHz 16-bit WAV.`
2. **Primary Signature Ability (`sfx_boss_ignis_boss_ability_molten.wav` — `CueIds.BossAbilityMolten` / `"boss_ability_molten"`):**
   > `High-impact boss ability sound for Archmage Ignis's MoltenEruption (Volcanic magma geyser pillars erupting from floor fissures (`48x48 px`)): 0.7s rising warning charge followed by a massive MoltenEruption detonation, 0.85s, mono 44.1kHz WAV.`
3. **Secondary Signature Ability (`sfx_boss_ignis_boss_ability_slam.wav` — `CueIds.BossAbilitySlam` / `"boss_ability_slam"`):**
   > `Distinct secondary boss ability sound for Archmage Ignis's ShockwaveSlam (Tectonic ground-shattering slam ring (`64x64 px`)): crisp telegraph transient and heavy release, 0.75s, mono 44.1kHz WAV.`
4. **Enrage Threshold Trigger (`sfx_boss_ignis_enrage.wav` — `CueIds.BossEnrage` / `"boss_enrage"`):**
   > `Furious heroic battle roar layered with a blazing furnace-horn swell and metallic armor resonance, 0.90s, mono 44.1kHz WAV.`
5. **Boss Defeat & Chest Spawn (`sfx_boss_ignis_defeat.wav` — `CueIds.BossDefeated` / `"boss_defeated"`):**
   > `Epic slow-motion armor collapse, shattering weapon ring, and triumphant dark-chord resolution into a golden chest chime, 1.5s, stereo 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Entrance** | `CueIds.BossAppearance` | `"boss_appearance"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_ignis_appearance.wav` |
| **Primary (`MoltenEruption`)** | `CueIds.BossAbilityMolten` | `"boss_ability_molten"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_ignis_boss_ability_molten.wav` |
| **Secondary (`ShockwaveSlam`)** | `CueIds.BossAbilitySlam` | `"boss_ability_slam"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_ignis_boss_ability_slam.wav` |
| **Enrage** | `CueIds.BossEnrage` | `"boss_enrage"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_ignis_enrage.wav` |
| **Defeat** | `CueIds.BossDefeated` | `"boss_defeated"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_ignis_defeat.wav` |
