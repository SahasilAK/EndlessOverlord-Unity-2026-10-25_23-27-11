# Lich Lord (`lich_lord`) — Complete Audio & SFX Prompt Specification

> **Asset Category:** Playable Boss Overlord Audio (`01_Characters`)
> **Catalog ID:** `CharacterCatalog.LichLordId` (`"lich_lord"`)
> **Sonic Identity:** Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis.
> **Target Bus:** `AudioCuePresenter` via `CueBus` (`PoolAndCues.cs`)

---

## 1. Base Sound Identity & Acoustic Palette

- **Primary Timbre Profile:** Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis.
- **Technical Export Standard:**
  - **Format:** Uncompressed `.wav` (PCM 16-bit, `44100 Hz`, Mono for 2D/spatialized combat cues).
  - **Loudness & Peak:** Normalized to `-1.5 dBFS` peak, `-14 LUFS` integrated short-term, zero DC offset, 5ms fade-out to prevent clipping clicks.
  - **Polyphony & Pitch Variance:** `AudioCuePresenter` applies random pitch modulation (`0.92f` to `1.08f`) and volume jitter (`0.90f` to `1.00f`) on rapid consecutive triggers.

---

## 2. Step-by-Step Core Combat Audio Variations

### Basic Attack — Soul Bolt Launch (`CueIds.PlayerSoulBolt` -> `"player_soul_bolt"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Lich Lord - Basic Attack — Soul Bolt Launch. Timbre: Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Signature — Death Wave / Necrotic Nova (`CueIds.PlayerDeathWave` -> `"player_death_wave"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Lich Lord - Signature — Death Wave / Necrotic Nova. Timbre: Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Summon Undead Minion Activation (`CueIds.PlayerSummon` -> `"player_summon"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Lich Lord - Summon Undead Minion Activation. Timbre: Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Spectral Phase Dash (`CueIds.PlayerDash` -> `"player_dash"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Lich Lord - Spectral Phase Dash. Timbre: Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Bone-Rattle & Arcane Ward Impact (`CueIds.PlayerHit` -> `"player_hit"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Lich Lord - Bone-Rattle & Arcane Ward Impact. Timbre: Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.
### Phylactery Shatter & Soul Collapse (`CueIds.PlayerDeath` -> `"player_death"`)
- **AI Audio Generation Prompt:**
  > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Lich Lord - Phylactery Shatter & Soul Collapse. Timbre: Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis.. Crisp transient under 15ms, punchy mid-range body (250Hz-2.5kHz), zero muddiness, clean decay tail, mono 44.1kHz 16-bit WAV.`
- **Synthesis / Foley Layering Recipe:**
  1. **Transient Layer (0–40ms):** Sharp metallic/arcane attack click at `1.8kHz–4.2kHz` (`-3 dBFS`).
  2. **Core Body (40–280ms):** Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis. modulated with a downward pitch envelope (`+4 st` to `0 st`).
  3. **Tail Layer (280–650ms):** Dark dungeon reverb tail with high-cut filter at `3.5kHz`.

---

## 3. Step-by-Step Skill Tree Audio Prompts (55 Class Abilities)

To maintain sonic consistency across all **55 abilities** in `Lich Lord`'s skill tree (`AbilityRegistry.cs`), generate 4 branch-archetype stems and layer per-tier intensity:

### 3.1 Branch A / Summon & Command Skills
> `Dark fantasy summoning sound effect for Lich Lord, Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis., deep subterranean rumble followed by crisp Lich Lord energy materialization chime, 0.65 seconds, mono 44.1kHz WAV.`

### 3.2 Branch B / Direct Offensive & Projectile Barrage Skills
> `Punchy arcane/martial projectile launch and impact sound for Lich Lord, Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis., fast 10ms attack snap with doppler whoosh, 0.40 seconds, mono 44.1kHz WAV.`

### 3.3 Branch C / Defensive Ward, Mobility & Aura Skills
> `Resonant dark-fantasy barrier activation and phase-shift whoosh for Lich Lord, Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis., shimmering harmonic drone with sub-bass lock, 0.55 seconds, mono 44.1kHz WAV.`

### 3.4 Ultimate / Tier 5–6 Cataclysm Skills (`AbilityEffectType.Ultimate`)
> `Massive boss ultimate activation sound effect for Lich Lord, Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis., 0.2s rising charging vacuum followed by a devastating 1.2s stereo-wide cataclysmic explosion, 44.1kHz 16-bit WAV.`

---

## 4. Unity `CueBus` & `AudioClip` Integration Table

| `CueIds` Constant | Cue String ID | Target Unity `.wav` Path | Duration | Runtime Settings |
| :--- | :--- | :--- | :--- | :--- |
| `CueIds.PlayerSoulBolt` | `"player_soul_bolt"` | `Assets/_Game/Audio/SFX/Characters/lich_lord/sfx_lich_lord_player_soul_bolt.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerDeathWave` | `"player_death_wave"` | `Assets/_Game/Audio/SFX/Characters/lich_lord/sfx_lich_lord_player_death_wave.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerSummon` | `"player_summon"` | `Assets/_Game/Audio/SFX/Characters/lich_lord/sfx_lich_lord_player_summon.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerDash` | `"player_dash"` | `Assets/_Game/Audio/SFX/Characters/lich_lord/sfx_lich_lord_player_dash.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerHit` | `"player_hit"` | `Assets/_Game/Audio/SFX/Characters/lich_lord/sfx_lich_lord_player_hit.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
| `CueIds.PlayerDeath` | `"player_death"` | `Assets/_Game/Audio/SFX/Characters/lich_lord/sfx_lich_lord_player_death.wav` | `0.25s - 0.85s` | Pitch `0.92 - 1.08` |
