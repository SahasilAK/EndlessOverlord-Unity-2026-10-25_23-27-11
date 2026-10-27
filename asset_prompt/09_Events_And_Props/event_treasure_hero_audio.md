# Treasure Hero In-Run Event (`event_treasure_hero`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.EventTreasureHero` (`"event_treasure_hero"`)
> **Acoustic Identity:** Frantic jingling coin pouch sprint + comical panicked whistle + jackpot burst on defeat.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_event_treasure_hero_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Treasure Hero In-Run Event": Frantic jingling coin pouch sprint + comical panicked whistle + jackpot burst on defeat., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_event_treasure_hero_activate.wav` — `CueIds.EventTreasureHero` / `"event_treasure_hero"`):**
   > `Satisfying interactive activation sound for "Treasure Hero In-Run Event" (Fleeing greedy adventurer burdened with an overflowing golden backpack dropping trails of glittering coins and gems (`32x32` 6-frame sprint & drop strip).): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.EventTreasureHero` | `"event_treasure_hero"` | `Assets/_Game/Audio/SFX/Events/sfx_event_treasure_hero_event_treasure_hero.wav` |
