# Lady Cassandra, The Whispering Phantom (`boss_cassandra`) — Legendary Boss Audio & SFX Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss Audio #02 (`04_Bosses`)
> **Catalog ID:** `"boss_cassandra"` (`LegendaryBossCatalog.cs`)
> **Primary Ability Cue:** `CueIds.BossAbilityBlink` (`"boss_ability_blink"`)
> **Secondary Ability Cue:** `CueIds.BossAbilityBarrage` (`"boss_ability_barrage"`)

---

## 1. Base Sound Identity & Mix Priority
- **Sonic Authority:** Milestone bosses command **top mix priority (`-1.0 dBFS`)** alongside the Player Overlord. Every boss spawn triggers a dramatic brass/choir sting (`CueIds.BossAppearance`) and ducks regular invader SFX by `-4 dB` for 1.5s.
- **Character Timbre:** Heroic low-brass motif + heavy masterwork `Assassin` foley + signature `PhantomBlinkStrike` / `MultishotBarrage` elemental layers.

---

## 2. Step-by-Step Audio Variations

1. **Boss Arena Entrance Fanfare (`sfx_boss_cassandra_appearance.wav` — `CueIds.BossAppearance` / `"boss_appearance"`):**
   > `Dramatic dark-fantasy milestone boss entrance sting for Lady Cassandra (The Whispering Phantom): booming war-drum hit, ominous brass horn chord, and metallic Assassin weapon unsheathing ring, 1.4s, stereo 44.1kHz 16-bit WAV.`
2. **Primary Signature Ability (`sfx_boss_cassandra_boss_ability_blink.wav` — `CueIds.BossAbilityBlink` / `"boss_ability_blink"`):**
   > `High-impact boss ability sound for Lady Cassandra's PhantomBlinkStrike (Dimensional shadow-step vanish & cross-slash execution (`48x48 px`)): 0.35s rising warning charge followed by a massive PhantomBlinkStrike detonation, 0.85s, mono 44.1kHz WAV.`
3. **Secondary Signature Ability (`sfx_boss_cassandra_boss_ability_barrage.wav` — `CueIds.BossAbilityBarrage` / `"boss_ability_barrage"`):**
   > `Distinct secondary boss ability sound for Lady Cassandra's MultishotBarrage (Fan-spread volley of 7 high-velocity heroic projectiles (`24x24 px` each)): crisp telegraph transient and heavy release, 0.75s, mono 44.1kHz WAV.`
4. **Enrage Threshold Trigger (`sfx_boss_cassandra_enrage.wav` — `CueIds.BossEnrage` / `"boss_enrage"`):**
   > `Furious heroic battle roar layered with a blazing furnace-horn swell and metallic armor resonance, 0.90s, mono 44.1kHz WAV.`
5. **Boss Defeat & Chest Spawn (`sfx_boss_cassandra_defeat.wav` — `CueIds.BossDefeated` / `"boss_defeated"`):**
   > `Epic slow-motion armor collapse, shattering weapon ring, and triumphant dark-chord resolution into a golden chest chime, 1.5s, stereo 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Entrance** | `CueIds.BossAppearance` | `"boss_appearance"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_cassandra_appearance.wav` |
| **Primary (`PhantomBlinkStrike`)** | `CueIds.BossAbilityBlink` | `"boss_ability_blink"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_cassandra_boss_ability_blink.wav` |
| **Secondary (`MultishotBarrage`)** | `CueIds.BossAbilityBarrage` | `"boss_ability_barrage"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_cassandra_boss_ability_barrage.wav` |
| **Enrage** | `CueIds.BossEnrage` | `"boss_enrage"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_cassandra_enrage.wav` |
| **Defeat** | `CueIds.BossDefeated` | `"boss_defeated"` | `Assets/_Game/Audio/SFX/Bosses/sfx_boss_cassandra_defeat.wav` |
