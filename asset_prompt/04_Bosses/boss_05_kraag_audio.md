# Warlord Kraag, The Mountain Breaker (`boss_kraag`) — Legendary Boss Audio & SFX Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss Audio #05 (`04_Bosses`)
> **Catalog ID:** `"boss_kraag"` (`LegendaryBossCatalog.cs`)
> **Primary Ability Cue:** `CueIds.BossAbilitySlam` (`"boss_ability_slam"`)
> **Secondary Ability Cue:** `CueIds.BossAbilityWhirlwind` (`"boss_ability_whirlwind"`)

---

## 1. Base Sound Identity & Mix Priority
- **Sonic Authority:** Milestone bosses command **top mix priority (`-1.0 dBFS`)** alongside the Player Overlord. Every boss spawn triggers a dramatic brass/choir sting (`CueIds.BossAppearance`) and ducks regular invader SFX by `-4 dB` for 1.5s.
- **Character Timbre:** Heroic low-brass motif + heavy masterwork `Berserker` foley + signature `ShockwaveSlam` / `WhirlwindCharge` elemental layers.

---

## 2. Step-by-Step Audio Variations

1. **Boss Arena Entrance Fanfare (`sfx_boss_kraag_appearance.wav` — `CueIds.BossAppearance` / `"boss_appearance"`):**
   > `Dramatic dark-fantasy milestone boss entrance sting for Warlord Kraag (The Mountain Breaker): booming war-drum hit, ominous brass horn chord, and metallic Berserker weapon unsheathing ring, 1.4s, stereo 44.1kHz 16-bit WAV.`
2. **Primary Signature Ability (`sfx_boss_kraag_boss_ability_slam.wav` — `CueIds.BossAbilitySlam` / `"boss_ability_slam"`):**
   > `High-impact boss ability sound for Warlord Kraag's ShockwaveSlam (Tectonic ground-shattering slam ring (`64x64 px`)): 0.8s rising warning charge followed by a massive ShockwaveSlam detonation, 0.85s, mono 44.1kHz WAV.`
3. **Secondary Signature Ability (`sfx_boss_kraag_boss_ability_whirlwind.wav` — `CueIds.BossAbilityWhirlwind` / `"boss_ability_whirlwind"`):**
   > `Distinct secondary boss ability sound for Warlord Kraag's WhirlwindCharge (360° spinning blade tornado rush (`48x48 px`)): crisp telegraph transient and heavy release, 0.75s, mono 44.1kHz WAV.`
4. **Enrage Threshold Trigger (`sfx_boss_kraag_enrage.wav` — `CueIds.BossEnrage` / `"boss_enrage"`):**
   > `Furious heroic battle roar layered with a blazing furnace-horn swell and metallic armor resonance, 0.90s, mono 44.1kHz WAV.`
5. **Boss Defeat & Chest Spawn (`sfx_boss_kraag_defeat.wav` — `CueIds.BossDefeated` / `"boss_defeated"`):**
   > `Epic slow-motion armor collapse, shattering weapon ring, and triumphant dark-chord resolution into a golden chest chime, 1.5s, stereo 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Entrance** | `CueIds.BossAppearance` | `"boss_appearance"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_kraag_appearance.wav` |
| **Primary (`ShockwaveSlam`)** | `CueIds.BossAbilitySlam` | `"boss_ability_slam"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_kraag_boss_ability_slam.wav` |
| **Secondary (`WhirlwindCharge`)** | `CueIds.BossAbilityWhirlwind` | `"boss_ability_whirlwind"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_kraag_boss_ability_whirlwind.wav` |
| **Enrage** | `CueIds.BossEnrage` | `"boss_enrage"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_kraag_enrage.wav` |
| **Defeat** | `CueIds.BossDefeated` | `"boss_defeated"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_kraag_defeat.wav` |
