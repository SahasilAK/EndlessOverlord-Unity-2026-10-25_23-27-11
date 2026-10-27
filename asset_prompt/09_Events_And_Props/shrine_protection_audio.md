# Shrine of Protection (`shrine_protection`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.ShrineActivated` (`"shrine_activated"`)
> **Acoustic Identity:** Sanctified crystal bell chime + heavy iron shield lock + warm restorative harmonic swell.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_shrine_protection_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Shrine of Protection": Sanctified crystal bell chime + heavy iron shield lock + warm restorative harmonic swell., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_shrine_protection_activate.wav` — `CueIds.ShrineActivated` / `"shrine_activated"`):**
   > `Satisfying interactive activation sound for "Shrine of Protection" (Ancient runic bastion monolith projecting a floating azure-silver shield crest (`#3498DB` / `#85C1E9`). Restores 35% Max HP and grants +12 Armor.): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.ShrineActivated` | `"shrine_activated"` | `Assets/_Game/Audio/SFX/Events/sfx_shrine_protection_shrine_activated.wav` |
