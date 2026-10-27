# Lady Seraphina, The Celestial Beacon (`boss_seraphina`) — Legendary Boss Audio & SFX Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss Audio #20 (`04_Bosses`)
> **Catalog ID:** `"boss_seraphina"` (`LegendaryBossCatalog.cs`)
> **Primary Ability Cue:** `CueIds.BossAbilityWard` (`"boss_ability_ward"`)
> **Secondary Ability Cue:** `CueIds.BossAbilityThunder` (`"boss_ability_thunder"`)

---

## 1. Base Sound Identity & Mix Priority
- **Sonic Authority:** Milestone bosses command **top mix priority (`-1.0 dBFS`)** alongside the Player Overlord. Every boss spawn triggers a dramatic brass/choir sting (`CueIds.BossAppearance`) and ducks regular invader SFX by `-4 dB` for 1.5s.
- **Character Timbre:** Heroic low-brass motif + heavy masterwork `HolyTemplar` foley + signature `SanctuaryWard` / `ThunderStrike` elemental layers.

---

## 2. Step-by-Step Audio Variations

1. **Boss Arena Entrance Fanfare (`sfx_boss_seraphina_appearance.wav` — `CueIds.BossAppearance` / `"boss_appearance"`):**
   > `Dramatic dark-fantasy milestone boss entrance sting for Lady Seraphina (The Celestial Beacon): booming war-drum hit, ominous brass horn chord, and metallic HolyTemplar weapon unsheathing ring, 1.4s, stereo 44.1kHz 16-bit WAV.`
2. **Primary Signature Ability (`sfx_boss_seraphina_boss_ability_ward.wav` — `CueIds.BossAbilityWard` / `"boss_ability_ward"`):**
   > `High-impact boss ability sound for Lady Seraphina's SanctuaryWard (Sacred golden dome barrier & floor sigil (`64x64 px`)): 0.55s rising warning charge followed by a massive SanctuaryWard detonation, 0.85s, mono 44.1kHz WAV.`
3. **Secondary Signature Ability (`sfx_boss_seraphina_boss_ability_thunder.wav` — `CueIds.BossAbilityThunder` / `"boss_ability_thunder"`):**
   > `Distinct secondary boss ability sound for Lady Seraphina's ThunderStrike (Heaven-piercing lightning pillar impact (`32x64 px`)): crisp telegraph transient and heavy release, 0.75s, mono 44.1kHz WAV.`
4. **Enrage Threshold Trigger (`sfx_boss_seraphina_enrage.wav` — `CueIds.BossEnrage` / `"boss_enrage"`):**
   > `Furious heroic battle roar layered with a blazing furnace-horn swell and metallic armor resonance, 0.90s, mono 44.1kHz WAV.`
5. **Boss Defeat & Chest Spawn (`sfx_boss_seraphina_defeat.wav` — `CueIds.BossDefeated` / `"boss_defeated"`):**
   > `Epic slow-motion armor collapse, shattering weapon ring, and triumphant dark-chord resolution into a golden chest chime, 1.5s, stereo 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Entrance** | `CueIds.BossAppearance` | `"boss_appearance"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_seraphina_appearance.wav` |
| **Primary (`SanctuaryWard`)** | `CueIds.BossAbilityWard` | `"boss_ability_ward"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_seraphina_boss_ability_ward.wav` |
| **Secondary (`ThunderStrike`)** | `CueIds.BossAbilityThunder` | `"boss_ability_thunder"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_seraphina_boss_ability_thunder.wav` |
| **Enrage** | `CueIds.BossEnrage` | `"boss_enrage"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_seraphina_enrage.wav` |
| **Defeat** | `CueIds.BossDefeated` | `"boss_defeated"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_seraphina_defeat.wav` |
