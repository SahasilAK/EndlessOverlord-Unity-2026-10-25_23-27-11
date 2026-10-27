# Void Distortion Mutation (`mutation_void_distortion`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.DungeonMutationShift` (`"dungeon_mutation_shift"`)
> **Acoustic Identity:** Warped pitch-bending synth drone + dimensional glass fracture + vacuum pulse.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_mutation_void_distortion_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Void Distortion Mutation": Warped pitch-bending synth drone + dimensional glass fracture + vacuum pulse., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_mutation_void_distortion_activate.wav` — `CueIds.DungeonMutationShift` / `"dungeon_mutation_shift"`):**
   > `Satisfying interactive activation sound for "Void Distortion Mutation" (Gravitational purple rift anomalies, warped floor tiles, and floating reality shards (`+20%` Cooldown speed, random gravity wells).): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.DungeonMutationShift` | `"dungeon_mutation_shift"` | `Assets/_Game/Audio/SFX/Events/sfx_mutation_void_distortion_dungeon_mutation_shift.wav` |
