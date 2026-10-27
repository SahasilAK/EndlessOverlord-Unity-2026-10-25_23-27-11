# Shrine of Frenzy (`shrine_frenzy`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.ShrineActivated` (`"shrine_activated"`)
> **Acoustic Identity:** Feral war-drum ignite + roaring furnace flare + accelerating heartbeat pulse.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_shrine_frenzy_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Shrine of Frenzy": Feral war-drum ignite + roaring furnace flare + accelerating heartbeat pulse., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_shrine_frenzy_activate.wav` — `CueIds.ShrineActivated` / `"shrine_activated"`):**
   > `Satisfying interactive activation sound for "Shrine of Frenzy" (Jagged iron war-totem crowned with blazing berserker horns and roaring orange-gold flames (`#FF5722` / `#FFD54F`). Grants +25% Attack Speed & +15% Move Speed, but -10 Armor.): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.ShrineActivated` | `"shrine_activated"` | `Assets/_Game/Audio/SFX/Events/sfx_shrine_frenzy_shrine_activated.wav` |
