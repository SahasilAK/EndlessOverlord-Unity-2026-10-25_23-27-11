# High Magus Corvin, The Glacial Warden (`boss_corvin`) — Legendary Boss Audio & SFX Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss Audio #23 (`04_Bosses`)
> **Catalog ID:** `"boss_corvin"` (`LegendaryBossCatalog.cs`)
> **Primary Ability Cue:** `CueIds.BossAbilityGlacial` (`"boss_ability_glacial"`)
> **Secondary Ability Cue:** `CueIds.BossAbilityMolten` (`"boss_ability_molten"`)

---

## 1. Base Sound Identity & Mix Priority
- **Sonic Authority:** Milestone bosses command **top mix priority (`-1.0 dBFS`)** alongside the Player Overlord. Every boss spawn triggers a dramatic brass/choir sting (`CueIds.BossAppearance`) and ducks regular invader SFX by `-4 dB` for 1.5s.
- **Character Timbre:** Heroic low-brass motif + heavy masterwork `IceMage` foley + signature `GlacialPrison` / `MoltenEruption` elemental layers.

---

## 2. Step-by-Step Audio Variations

1. **Boss Arena Entrance Fanfare (`sfx_boss_corvin_appearance.wav` — `CueIds.BossAppearance` / `"boss_appearance"`):**
   > `Dramatic dark-fantasy milestone boss entrance sting for High Magus Corvin (The Glacial Warden): booming war-drum hit, ominous brass horn chord, and metallic IceMage weapon unsheathing ring, 1.4s, stereo 44.1kHz 16-bit WAV.`
2. **Primary Signature Ability (`sfx_boss_corvin_boss_ability_glacial.wav` — `CueIds.BossAbilityGlacial` / `"boss_ability_glacial"`):**
   > `High-impact boss ability sound for High Magus Corvin's GlacialPrison (Ring of jagged permafrost ice spikes locking the target (`64x64 px`)): 0.6s rising warning charge followed by a massive GlacialPrison detonation, 0.85s, mono 44.1kHz WAV.`
3. **Secondary Signature Ability (`sfx_boss_corvin_boss_ability_molten.wav` — `CueIds.BossAbilityMolten` / `"boss_ability_molten"`):**
   > `Distinct secondary boss ability sound for High Magus Corvin's MoltenEruption (Volcanic magma geyser pillars erupting from floor fissures (`48x48 px`)): crisp telegraph transient and heavy release, 0.75s, mono 44.1kHz WAV.`
4. **Enrage Threshold Trigger (`sfx_boss_corvin_enrage.wav` — `CueIds.BossEnrage` / `"boss_enrage"`):**
   > `Furious heroic battle roar layered with a blazing furnace-horn swell and metallic armor resonance, 0.90s, mono 44.1kHz WAV.`
5. **Boss Defeat & Chest Spawn (`sfx_boss_corvin_defeat.wav` — `CueIds.BossDefeated` / `"boss_defeated"`):**
   > `Epic slow-motion armor collapse, shattering weapon ring, and triumphant dark-chord resolution into a golden chest chime, 1.5s, stereo 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Entrance** | `CueIds.BossAppearance` | `"boss_appearance"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_corvin_appearance.wav` |
| **Primary (`GlacialPrison`)** | `CueIds.BossAbilityGlacial` | `"boss_ability_glacial"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_corvin_boss_ability_glacial.wav` |
| **Secondary (`MoltenEruption`)** | `CueIds.BossAbilityMolten` | `"boss_ability_molten"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_corvin_boss_ability_molten.wav` |
| **Enrage** | `CueIds.BossEnrage` | `"boss_enrage"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_corvin_enrage.wav` |
| **Defeat** | `CueIds.BossDefeated` | `"boss_defeated"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_corvin_defeat.wav` |
