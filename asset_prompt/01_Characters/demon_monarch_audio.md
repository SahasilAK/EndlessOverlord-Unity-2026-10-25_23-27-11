# Demon Monarch (`demon_monarch`) — Complete Audio & SFX Prompt Specification

> **Asset Category:** Playable Boss Overlord Audio (`01_Characters`)
> **Catalog ID:** `CharacterCatalog.DemonMonarchId` (`"demon_monarch"`)
> **Sonic Identity:** Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts.
> **Target Bus:** `AudioCuePresenter` via `CueBus` (`PoolAndCues.cs`)

---

## 1. Base Sound Identity & Acoustic Palette

- **Primary Timbre Profile:** Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts.
- **Technical Export Standard:**
  - **Format:** Uncompressed `.wav` (PCM 16-bit, `44100 Hz`, Mono for 2D/spatialized combat cues).
  - **Loudness & Peak:** Normalized to `-1.5 dBFS` peak, `-14 LUFS` integrated short-term, zero DC offset, 5ms fade-out to prevent clipping clicks.
  - **Polyphony & Pitch Variance:** `AudioCuePresenter` applies random pitch modulation (`0.92f` to `1.08f`) and volume jitter (`0.90f` to `1.00f`) on rapid consecutive triggers.

---

## 2. Step-by-Step Core Combat Audio Variations

### Basic Attack — Infernal Greatsword Magma Cleave (`CueIds.SpellCastFire` -> `"spell_cast_fire"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Demon Monarch - Basic Attack — Infernal Greatsword Magma Cleave. Timbre: Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Signature — Hellfire Earthshaker Slam (`CueIds.BossAbilitySlam` -> `"boss_ability_slam"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Demon Monarch - Signature — Hellfire Earthshaker Slam. Timbre: Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Hellfire Immolation Aura Loop (`CueIds.StatusBurn` -> `"status_burn"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Demon Monarch - Hellfire Immolation Aura Loop. Timbre: Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Brimstone Stampede Dash (`CueIds.PlayerDash` -> `"player_dash"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Demon Monarch - Brimstone Stampede Dash. Timbre: Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Obsidian Plate & Demonic Roar Hit (`CueIds.PlayerHit` -> `"player_hit"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Demon Monarch - Obsidian Plate & Demonic Roar Hit. Timbre: Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Volcanic Core Implosion Death (`CueIds.PlayerDeath` -> `"player_death"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Demon Monarch - Volcanic Core Implosion Death. Timbre: Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.

---

## 3. Step-by-Step Skill Tree Audio Prompts (55 Class Abilities)

To maintain sonic consistency across all **55 abilities** in `Demon Monarch`'s skill tree (`AbilityRegistry.cs`), generate 4 branch-archetype stems and layer per-tier intensity:

### 3.1 Branch A / Summon & Command Skills
> `Dark fantasy summoning sound effect for Demon Monarch, Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts., deep subterranean rumble followed by crisp Demon Monarch energy materialization chime, 0.65 seconds, mono 44.1kHz WAV.`

### 3.2 Branch B / Direct Offensive & Projectile Barrage Skills
> `Punchy arcane/martial projectile launch and impact sound for Demon Monarch, Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts., fast 10ms attack snap with doppler whoosh, 0.40 seconds, mono 44.1kHz WAV.`

### 3.3 Branch C / Defensive Ward, Mobility & Aura Skills
> `Resonant dark-fantasy barrier activation and phase-shift whoosh for Demon Monarch, Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts., shimmering harmonic drone with sub-bass lock, 0.55 seconds, mono 44.1kHz WAV.`

### 3.4 Ultimate / Tier 5–6 Cataclysm Skills (`AbilityEffectType.Ultimate`)
> `Massive boss ultimate activation sound effect for Demon Monarch, Sub-octave brass horn growls + volcanic basalt rock crunch + roaring furnace flame sweeps + heavy anvil sword impacts., 0.2s rising charging vacuum followed by a devastating 1.2s stereo-wide cataclysmic explosion, 44.1kHz 16-bit WAV.`

---

## 4. Unity `CueBus` & `AudioClip` Integration Table

| `CueIds` Constant | Cue String ID | Target Unity `.wav` Path | Duration | Runtime Settings |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastFire` | `"spell_cast_fire"` | `Assets/_Game/Audio/SFX/Characters/demon_monarch/sfx_demon_monarch_spell_cast_fire.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.BossAbilitySlam` | `"boss_ability_slam"` | `Assets/_Game/Audio/SFX/Characters/demon_monarch/sfx_demon_monarch_boss_ability_slam.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.StatusBurn` | `"status_burn"` | `Assets/_Game/Audio/SFX/Characters/demon_monarch/sfx_demon_monarch_status_burn.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerDash` | `"player_dash"` | `Assets/_Game/Audio/SFX/Characters/demon_monarch/sfx_demon_monarch_player_dash.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerHit` | `"player_hit"` | `Assets/_Game/Audio/SFX/Characters/demon_monarch/sfx_demon_monarch_player_hit.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerDeath` | `"player_death"` | `Assets/_Game/Audio/SFX/Characters/demon_monarch/sfx_demon_monarch_player_death.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
