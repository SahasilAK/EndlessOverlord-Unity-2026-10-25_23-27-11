# Glacial Cold Mutation (`mutation_glacial_cold`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.DungeonMutationShift` (`"dungeon_mutation_shift"`)
> **Acoustic Identity:** Howling arctic wind gust + brittle crystalline ice cracking + sub-zero chime.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_mutation_glacial_cold_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Glacial Cold Mutation": Howling arctic wind gust + brittle crystalline ice cracking + sub-zero chime., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_mutation_glacial_cold_activate.wav` — `CueIds.DungeonMutationShift` / `"dungeon_mutation_shift"`):**
   > `Satisfying interactive activation sound for "Glacial Cold Mutation" (Creeping permafrost ice-crystal floor patches, drifting blizzard snowflakes, and frosted screen-corner vignette (`-15%` global move speed, ice shatter procs).): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.DungeonMutationShift` | `"dungeon_mutation_shift"` | `Assets/_Game/Audio/SFX/Events/sfx_mutation_glacial_cold_dungeon_mutation_shift.wav` |
