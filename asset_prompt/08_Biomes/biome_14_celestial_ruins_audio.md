# Celestial Ruins (Biome #14) — Ambient Soundscape, Music & Hazard Audio Specification

> **Asset Category:** Dungeon Arena Biome Audio #14/20 (`08_Biomes`)
> **Catalog Entry:** `BiomeCatalog.GetById(14)` — `"Celestial Ruins"`
> **Atmospheric Theme:** Floating stone pathways, ancient divine structures, glowing inscriptions, ethereal clouds, celestial gardens, and golden magical light.

---

## 1. Base Soundscape & Musical Identity
- **Ambient Bed Loop (`amb_biome_14_celestial_ruins_loop.wav`):** Seamless `30.0s` stereo ambient loop (`-18 LUFS`) capturing *floating stone pathways, ancient divine structures, glowing inscriptions, ethereal clouds, celestial gardens, and golden magical light.*, high-passed at `80 Hz` and dipped `-4 dB` at `1–3 kHz` to leave headroom for combat cues.
- **Combat Music Layer (`mus_biome_14_celestial_ruins_combat.wav`):** Driving dark-synth / orchestral / 16-bit hybrid combat loop (`128 BPM`, seamless loop points).

---

## 2. Step-by-Step Audio Prompts

1. **Seamless Ambient Environment Loop (`amb_biome_14_celestial_ruins_loop.wav`):**
   > `Seamless 30-second dark fantasy dungeon ambient background loop for "Celestial Ruins" (Floating stone pathways, ancient divine structures, glowing inscriptions, ethereal clouds, celestial gardens, and golden magical light.): immersive spatial textures, subtle echoing drips/winds/machinery/whispers, stereo 44.1kHz 16-bit WAV.`
2. **Biome Hazard / Prop Trigger (`sfx_biome_14_celestial_ruins_hazard.wav` — `CueIds.BiomeHazardTrigger` / `"biome_hazard_trigger"`):**
   > `Distinct environmental hazard activation and impact sound for "Celestial Ruins" (Floating stone pathways, ancient divine structures, glowing inscriptions, ethereal clouds, celestial gardens, and golden magical light.): 0.55s warning crackle followed by thematic elemental/physical burst, mono 44.1kHz WAV.`
3. **Portal Breach Pulse (`sfx_biome_14_celestial_ruins_portal.wav` — `CueIds.InvaderSpawn`):**
   > `Resonant dimensional rift pulse themed to "Celestial Ruins", 0.40s, mono 44.1kHz WAV.`

---

## 3. Unity Audio Integration Table

| Audio Role | Cue / Bus | Target `.wav` Path | Loop / Format |
| :--- | :--- | :--- | :--- |
| **Ambient Loop** | `AmbientAudioPresenter` | `Assets/_Game/Audio/Ambience/amb_biome_14_celestial_ruins_loop.wav` | Seamless Loop, Stereo |
| **Hazard Trigger** | `CueIds.BiomeHazardTrigger` | `Assets/_Game/Audio/SFX/Biomes/sfx_biome_14_celestial_ruins_hazard.wav` | One-Shot, Mono |
