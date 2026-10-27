# Volcanic Surge Mutation (`mutation_volcanic_surge`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.DungeonMutationShift` (`"dungeon_mutation_shift"`)
> **Acoustic Identity:** Deep tectonic rumble + hissing magma geyser eruption + crackling ember loop.

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_mutation_volcanic_surge_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Volcanic Surge Mutation": Deep tectonic rumble + hissing magma geyser eruption + crackling ember loop., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_mutation_volcanic_surge_activate.wav` — `CueIds.DungeonMutationShift` / `"dungeon_mutation_shift"`):**
   > `Satisfying interactive activation sound for "Volcanic Surge Mutation" (Periodic molten magma floor fissures, bubbling lava geysers, and rising ember screen-border overlay (`+15%` all damage dealt, periodic fire eruptions).): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.DungeonMutationShift` | `"dungeon_mutation_shift"` | `Assets/_Game/Audio/SFX/Events/sfx_mutation_volcanic_surge_dungeon_mutation_shift.wav` |
