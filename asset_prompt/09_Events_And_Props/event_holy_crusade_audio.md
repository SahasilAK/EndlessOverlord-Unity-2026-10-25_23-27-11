# Holy Crusade In-Run Event (`event_holy_crusade`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.EventBannerWarning` (`"event_banner_warning"`)
> **Acoustic Identity:** Blaring cathedral brass fanfare + choir warning sting + descending radiant beam hum.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_event_holy_crusade_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Holy Crusade In-Run Event": Blaring cathedral brass fanfare + choir warning sting + descending radiant beam hum., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_event_holy_crusade_activate.wav` — `CueIds.EventBannerWarning` / `"event_banner_warning"`):**
   > `Satisfying interactive activation sound for "Holy Crusade In-Run Event" (Descending golden sunbeam pillars, sanctified war-banners planted in the arena, and radiant crusader spawn herald VFX.): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.EventBannerWarning` | `"event_banner_warning"` | `Assets/_Game/Audio/SFX/Events/sfx_event_holy_crusade_event_banner_warning.wav` |
