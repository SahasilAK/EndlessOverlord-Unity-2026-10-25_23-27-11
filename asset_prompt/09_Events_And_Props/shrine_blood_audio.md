# Shrine of Blood (`shrine_blood`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.ShrineActivated` (`"shrine_activated"`)
> **Acoustic Identity:** Bubbling sanguine liquid surge + sacrificial blade slice + dark resonant power-up chord.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_shrine_blood_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Shrine of Blood": Bubbling sanguine liquid surge + sacrificial blade slice + dark resonant power-up chord., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_shrine_blood_activate.wav` — `CueIds.ShrineActivated` / `"shrine_activated"`):**
   > `Satisfying interactive activation sound for "Shrine of Blood" (Gothic sacrificial blood chalice altar carved from dark obsidian with overflowing crimson liquid blood (`#D91A4D`) and levitating ruby droplets (`#FF6680`). Sacrifice 20% current HP for +25% Permanent Damage.): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.ShrineActivated` | `"shrine_activated"` | `Assets/_Game/Audio/SFX/Events/sfx_shrine_blood_shrine_activated.wav` |
