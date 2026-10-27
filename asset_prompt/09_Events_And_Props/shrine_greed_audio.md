# Shrine of Greed (`shrine_greed`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.ShrineActivated` (`"shrine_activated"`)
> **Acoustic Identity:** Cascading gold coin jackpot jingle + sinister metallic gargoyle laugh + war-horn warning.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_shrine_greed_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Shrine of Greed": Cascading gold coin jackpot jingle + sinister metallic gargoyle laugh + war-horn warning., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_shrine_greed_activate.wav` — `CueIds.ShrineActivated` / `"shrine_activated"`):**
   > `Satisfying interactive activation sound for "Shrine of Greed" (Gilded demonic coffer pedestal overflowing with cursed gold coins (`#F1C40F`) and a grinning golden gargoyle maw (`#FFF59D`). Grants +60 Gold immediately, spawns extra elite wave.): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.ShrineActivated` | `"shrine_activated"` | `Assets/_Game/Audio/SFX/Events/sfx_shrine_greed_shrine_activated.wav` |
