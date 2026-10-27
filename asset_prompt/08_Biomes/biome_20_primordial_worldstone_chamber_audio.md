# Primordial Worldstone Chamber (Biome #20) — Ambient Soundscape, Music & Hazard Audio Specification

> **Asset Category:** Dungeon Arena Biome Audio #20/20 (`08_Biomes`)
> **Catalog Entry:** `BiomeCatalog.GetById(20)` — `"Primordial Worldstone Chamber"`
> **Atmospheric Theme:** Ancient monolithic stone structures, floating crystals, magical mineral formations, enormous runes, and shifting underground energy.

---

## 1. Base Soundscape & Musical Identity
- **Ambient Bed Loop (`amb_biome_20_primordial_worldstone_chamber_loop.wav`):** Seamless `30.0s` stereo ambient loop (`-18 LUFS`) capturing *ancient monolithic stone structures, floating crystals, magical mineral formations, enormous runes, and shifting underground energy.*, high-passed at `80 Hz` and dipped `-4 dB` at `1–3 kHz` to leave headroom for combat cues.
- **Combat Music Layer (`mus_biome_20_primordial_worldstone_chamber_combat.wav`):** Driving dark-synth / orchestral / 16-bit hybrid combat loop (`128 BPM`, seamless loop points).

---

## 2. Step-by-Step Audio Prompts

1. **Seamless Ambient Environment Loop (`amb_biome_20_primordial_worldstone_chamber_loop.wav`):**
   > `Seamless 30-second dark fantasy dungeon ambient background loop for "Primordial Worldstone Chamber" (Ancient monolithic stone structures, floating crystals, magical mineral formations, enormous runes, and shifting underground energy.): immersive spatial textures, subtle echoing drips/winds/machinery/whispers, stereo 44.1kHz 16-bit WAV.`
2. **Biome Hazard / Prop Trigger (`sfx_biome_20_primordial_worldstone_chamber_hazard.wav` — `CueIds.BiomeHazardTrigger` / `"biome_hazard_trigger"`):**
   > `Distinct environmental hazard activation and impact sound for "Primordial Worldstone Chamber" (Ancient monolithic stone structures, floating crystals, magical mineral formations, enormous runes, and shifting underground energy.): 0.55s warning crackle followed by thematic elemental/physical burst, mono 44.1kHz WAV.`
3. **Portal Breach Pulse (`sfx_biome_20_primordial_worldstone_chamber_portal.wav` — `CueIds.InvaderSpawn`):**
   > `Resonant dimensional rift pulse themed to "Primordial Worldstone Chamber", 0.40s, mono 44.1kHz WAV.`

---

## 3. Unity Audio Integration Table

| Audio Role | Cue / Bus | Target `.wav` Path | Loop / Format |
| :--- | :--- | :--- | :--- |
| **Ambient Loop** | `AmbientAudioPresenter` | `Assets/_Game/Audio/Ambience/amb_biome_20_primordial_worldstone_chamber_loop.wav` | Seamless Loop, Stereo |
| **Hazard Trigger** | `CueIds.BiomeHazardTrigger` | `Assets/_Game/Audio/SFX/Biomes/sfx_biome_20_primordial_worldstone_chamber_hazard.wav` | One-Shot, Mono |
