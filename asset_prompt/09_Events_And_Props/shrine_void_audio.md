# Shrine of the Void (`shrine_void`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.ShrineActivated` (`"shrine_activated"`)
> **Acoustic Identity:** Sub-bass dimensional implosion + shimmering cosmic arpeggio + arcane time-warp hum.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_shrine_void_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Shrine of the Void": Sub-bass dimensional implosion + shimmering cosmic arpeggio + arcane time-warp hum., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_shrine_void_activate.wav` — `CueIds.ShrineActivated` / `"shrine_activated"`):**
   > `Satisfying interactive activation sound for "Shrine of the Void" (Fractured floating dark-matter obelisk orbiting a miniature purple-magenta singularity (`#9B59B6` / `#E056FD`). Grants +20% Cooldown Reduction & +25 Max Resource.): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.ShrineActivated` | `"shrine_activated"` | `Assets/_Game/Audio/SFX/Events/sfx_shrine_void_shrine_activated.wav` |
