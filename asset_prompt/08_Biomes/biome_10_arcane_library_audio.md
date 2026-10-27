# Arcane Library (Biome #10) — Ambient Soundscape, Music & Hazard Audio Specification

> **Asset Category:** Dungeon Arena Biome Audio #10/20 (`08_Biomes`)
> **Catalog Entry:** `BiomeCatalog.GetById(10)` — `"Arcane Library"`
> **Atmospheric Theme:** Enchanted bookshelves, floating books, magical books, crystal lanterns, runic floors, arcane laboratories, and mysterious magical devices.

---

## 1. Base Soundscape & Musical Identity
- **Ambient Bed Loop (`amb_biome_10_arcane_library_loop.wav`):** Seamless `30.0s` stereo ambient loop (`-18 LUFS`) capturing *enchanted bookshelves, floating books, magical books, crystal lanterns, runic floors, arcane laboratories, and mysterious magical devices.*, high-passed at `80 Hz` and dipped `-4 dB` at `1–3 kHz` to leave headroom for combat cues.
- **Combat Music Layer (`mus_biome_10_arcane_library_combat.wav`):** Driving dark-synth / orchestral / 16-bit hybrid combat loop (`128 BPM`, seamless loop points).

---

## 2. Step-by-Step Audio Prompts

1. **Seamless Ambient Environment Loop (`amb_biome_10_arcane_library_loop.wav`):**
   > `Seamless 30-second dark fantasy dungeon ambient background loop for "Arcane Library" (Enchanted bookshelves, floating books, magical books, crystal lanterns, runic floors, arcane laboratories, and mysterious magical devices.): immersive spatial textures, subtle echoing drips/winds/machinery/whispers, stereo 44.1kHz 16-bit WAV.`
2. **Biome Hazard / Prop Trigger (`sfx_biome_10_arcane_library_hazard.wav` — `CueIds.BiomeHazardTrigger` / `"biome_hazard_trigger"`):**
   > `Distinct environmental hazard activation and impact sound for "Arcane Library" (Enchanted bookshelves, floating books, magical books, crystal lanterns, runic floors, arcane laboratories, and mysterious magical devices.): 0.55s warning crackle followed by thematic elemental/physical burst, mono 44.1kHz WAV.`
3. **Portal Breach Pulse (`sfx_biome_10_arcane_library_portal.wav` — `CueIds.InvaderSpawn`):**
   > `Resonant dimensional rift pulse themed to "Arcane Library", 0.40s, mono 44.1kHz WAV.`

---

## 3. Unity Audio Integration Table

| Audio Role | Cue / Bus | Target `.wav` Path | Loop / Format |
| :--- | :--- | :--- | :--- |
| **Ambient Loop** | `AmbientAudioPresenter` | `Assets/_Game/Audio/Ambience/amb_biome_10_arcane_library_loop.wav` | Seamless Loop, Stereo |
| **Hazard Trigger** | `CueIds.BiomeHazardTrigger` | `Assets/_Game/Audio/SFX/Biomes/sfx_biome_10_arcane_library_hazard.wav` | One-Shot, Mono |
