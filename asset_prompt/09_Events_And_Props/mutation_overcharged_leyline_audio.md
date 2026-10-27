# Overcharged Leyline Mutation (`mutation_overcharged_leyline`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.DungeonMutationShift` (`"dungeon_mutation_shift"`)
> **Acoustic Identity:** High-voltage arcane transformer hum + harmonic crystal resonance + electric arc snap.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_mutation_overcharged_leyline_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Overcharged Leyline Mutation": High-voltage arcane transformer hum + harmonic crystal resonance + electric arc snap., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_mutation_overcharged_leyline_activate.wav` — `CueIds.DungeonMutationShift` / `"dungeon_mutation_shift"`):**
   > `Satisfying interactive activation sound for "Overcharged Leyline Mutation" (Glowing geometric arcane circuit lines coursing across dungeon stone with discharging emerald-cyan sparks (`+50%` Resource Regen, periodic static bursts).): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.DungeonMutationShift` | `"dungeon_mutation_shift"` | `Assets/_Game/Audio/SFX/Events/sfx_mutation_overcharged_leyline_dungeon_mutation_shift.wav` |
