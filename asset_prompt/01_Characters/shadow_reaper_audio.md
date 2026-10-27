# Shadow Reaper (`shadow_reaper`) — Complete Audio & SFX Prompt Specification

> **Asset Category:** Playable Boss Overlord Audio (`01_Characters`)
> **Catalog ID:** `CharacterCatalog.ShadowReaperId` (`"shadow_reaper"`)
> **Sonic Identity:** Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes.
> **Target Bus:** `AudioCuePresenter` via `CueBus` (`PoolAndCues.cs`)

---

## 1. Base Sound Identity & Acoustic Palette

- **Primary Timbre Profile:** Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes.
- **Technical Export Standard:**
  - **Format:** Uncompressed `.wav` (PCM 16-bit, `44100 Hz`, Mono for 2D/spatialized combat cues).
  - **Loudness & Peak:** Normalized to `-1.5 dBFS` peak, `-14 LUFS` integrated short-term, zero DC offset, 5ms fade-out to prevent clipping clicks.
  - **Polyphony & Pitch Variance:** `AudioCuePresenter` applies random pitch modulation (`0.92f` to `1.08f`) and volume jitter (`0.90f` to `1.00f`) on rapid consecutive triggers.

---

## 2. Step-by-Step Core Combat Audio Variations

### Basic Attack — Umbral Scythe Dimensional Reap (`CueIds.SpellCastShadow` -> `"spell_cast_shadow"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Shadow Reaper - Basic Attack — Umbral Scythe Dimensional Reap. Timbre: Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Signature — Phantom Shadowstep Execution (`CueIds.BossAbilityBlink` -> `"boss_ability_blink"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Shadow Reaper - Signature — Phantom Shadowstep Execution. Timbre: Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Umbral Smoke Vanish Dash (`CueIds.PlayerDash` -> `"player_dash"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Shadow Reaper - Umbral Smoke Vanish Dash. Timbre: Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Shadow Shroud Tear Impact (`CueIds.PlayerHit` -> `"player_hit"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Shadow Reaper - Shadow Shroud Tear Impact. Timbre: Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Void Singularity Collapse Death (`CueIds.PlayerDeath` -> `"player_death"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Shadow Reaper - Void Singularity Collapse Death. Timbre: Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.

---

## 3. Step-by-Step Skill Tree Audio Prompts (56 Class Abilities)

To maintain sonic consistency across all **56 abilities** in `Shadow Reaper`'s skill tree (`AbilityRegistry.cs`), generate 4 branch-archetype stems and layer per-tier intensity:

### 3.1 Branch A / Summon & Command Skills
> `Dark fantasy summoning sound effect for Shadow Reaper, Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes., deep subterranean rumble followed by crisp Shadow Reaper energy materialization chime, 0.65 seconds, mono 44.1kHz WAV.`

### 3.2 Branch B / Direct Offensive & Projectile Barrage Skills
> `Punchy arcane/martial projectile launch and impact sound for Shadow Reaper, Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes., fast 10ms attack snap with doppler whoosh, 0.40 seconds, mono 44.1kHz WAV.`

### 3.3 Branch C / Defensive Ward, Mobility & Aura Skills
> `Resonant dark-fantasy barrier activation and phase-shift whoosh for Shadow Reaper, Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes., shimmering harmonic drone with sub-bass lock, 0.55 seconds, mono 44.1kHz WAV.`

### 3.4 Ultimate / Tier 5–6 Cataclysm Skills (`AbilityEffectType.Ultimate`)
> `Massive boss ultimate activation sound effect for Shadow Reaper, Sub-bass dimensional vacuum tears + metallic guillotine scythe rings + airy reversed white-noise shadow swooshes., 0.2s rising charging vacuum followed by a devastating 1.2s stereo-wide cataclysmic explosion, 44.1kHz 16-bit WAV.`

---

## 4. Unity `CueBus` & `AudioClip` Integration Table

| `CueIds` Constant | Cue String ID | Target Unity `.wav` Path | Duration | Runtime Settings |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.SpellCastShadow` | `"spell_cast_shadow"` | `Assets/_Game/Audio/SFX/Characters/shadow_reaper/sfx_shadow_reaper_spell_cast_shadow.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.BossAbilityBlink` | `"boss_ability_blink"` | `Assets/_Game/Audio/SFX/Characters/shadow_reaper/sfx_shadow_reaper_boss_ability_blink.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerDash` | `"player_dash"` | `Assets/_Game/Audio/SFX/Characters/shadow_reaper/sfx_shadow_reaper_player_dash.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerHit` | `"player_hit"` | `Assets/_Game/Audio/SFX/Characters/shadow_reaper/sfx_shadow_reaper_player_hit.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerDeath` | `"player_death"` | `Assets/_Game/Audio/SFX/Characters/shadow_reaper/sfx_shadow_reaper_player_death.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
