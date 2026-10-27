# Corpse Collector (`corpse_collector`) — Summon Audio & SFX Prompt Specification

> **Asset Category:** Player Summon Audio (`02_Summons`)
> **Archetype Role:** Support Harvester Construct
> **Acoustic Identity:** Dragging rusted iron chains + eerie vacuum soul-siphon hum + wet bone thud.

---

## 1. Base Sound Identity & Mix Rules
- **Timbre Profile:** Dragging rusted iron chains + eerie vacuum soul-siphon hum + wet bone thud.
- **Mix Priority:** Secondary to Player Overlord (`-4 dB` relative attenuation so 5+ active minions never mask player/boss telegraphs).
- **Format:** `44100 Hz`, 16-bit Mono `.wav`, pitch randomized `0.90f–1.10f`.

---

## 2. Step-by-Step Audio Variations

1. **Summon Rise (`sfx_sum_corpse_collector_spawn.wav` — Mapped to `CueIds.PlayerSummon` / `"player_summon"`):**
   > `Dark fantasy undead minion summoning sound for Corpse Collector: cracking dungeon stone, rattling bone assembly (Dragging rusted iron chains + eerie vacuum soul-siphon hum + wet bone thud.), and a resonant emerald soul-fire ignition whoosh, 0.55s, mono 44.1kHz WAV.`
2. **Minion Attack (`sfx_sum_corpse_collector_attack.wav`):**
   > `Compact punchy combat attack sound for Corpse Collector (Support Harvester Construct): Dragging rusted iron chains + eerie vacuum soul-siphon hum + wet bone thud., fast transient, low-clutter 0.30s decay tail, mono WAV.`
3. **Minion Hit (`sfx_sum_corpse_collector_hit.wav`):**
   > `Hollow bone impact clatter with brief necrotic spark fizzle, 0.20s, mono WAV.`
4. **Minion Collapse / Expiration (`sfx_sum_corpse_collector_death.wav`):**
   > `Dry xylophone-like skeleton bone collapse onto stone floor with a soft exhaling soul wisp, 0.45s, mono WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | Cue ID | Target `.wav` Path | Attenuation | Pitch Variance |
| :--- | :--- | :--- | :--- | :--- |
| **Spawn** | `CueIds.PlayerSummon` (`"player_summon"`) | `Assets/_Game/Audio/SFX/Summons/sfx_sum_corpse_collector_spawn.wav` | `-3 dB` | `0.92 - 1.08` |
| **Attack** | `"summon_attack_corpse_collector"` | `Assets/_Game/Audio/SFX/Summons/sfx_sum_corpse_collector_attack.wav` | `-5 dB` | `0.90 - 1.12` |
| **Death** | `"summon_death_corpse_collector"` | `Assets/_Game/Audio/SFX/Summons/sfx_sum_corpse_collector_death.wav` | `-4 dB` | `0.88 - 1.10` |
