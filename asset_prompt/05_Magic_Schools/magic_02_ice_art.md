# Ice School — Cryomancy (`MagicSchoolType.Ice`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #02 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Ice` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Ice` | **School Hex:** `#00E5FF`
> **School Passive:** Chill: Freezes foes, reducing movement and attack speed by 25%.
> **Cross-School Synergy:** **Thermal Shock** (with `MagicSchoolType.Fire`) — *Striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#7FF2FF` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#00E5FF` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#00727F` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_ice.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Ice (Cryomancy): "Frigid control spells that slow enemy movement and crystallize armor.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #00E5FF and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Ice School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_frost_shard` | **Frost Shard** | Tier 1 | `DirectProjectile` | `13` | `0.6s` | `8` | `9` | Fires a sharp icicle that pierces one target. |
| `spell_glacial_spike` | **Glacial Spike** | Tier 2 | `GroundHazard` | `24` | `3.0s` | `22` | `7` | Erupts sharp glacier spikes from the permafrost. |
| `spell_blizzard_storm` | **Blizzard Storm** | Tier 3 | `RadialAoENova` | `48` | `5.5s` | `40` | `8` | Summons a violent sub-zero blizzard around the caster. |
| `spell_absolute_zero` | **Absolute Zero** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `90` | `4.5s` | `45` | `10` | Flash-freezes all surrounding existence to absolute zero. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Frost Shard (`spell_frost_shard` — `DirectProjectile`)
- **Description:** Fires a sharp icicle that pierces one target. (Damage `13`, Cooldown `0.6s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_frost_shard.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Frost Shard" (Ice / Cryomancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #00E5FF primary energy and #00727F background depth, depicting fires a sharp icicle that pierces one target., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_frost_shard_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Frost Shard" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #7FF2FF and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — fires a sharp icicle that pierces one target., intense #FFFFFF center stepping through #00E5FF to #00727F trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #00E5FF particles and status application visual (Chill).`
### Tier 2: Glacial Spike (`spell_glacial_spike` — `GroundHazard`)
- **Description:** Erupts sharp glacier spikes from the permafrost. (Damage `24`, Cooldown `3.0s`, Range `7`).
- **Spell Book / HUD Icon Prompt (`icon_spell_glacial_spike.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Glacial Spike" (Ice / Cryomancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #00E5FF primary energy and #00727F background depth, depicting erupts sharp glacier spikes from the permafrost., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_glacial_spike_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Glacial Spike" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #7FF2FF and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (GroundHazard) — erupts sharp glacier spikes from the permafrost., intense #FFFFFF center stepping through #00E5FF to #00727F trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #00E5FF particles and status application visual (Chill).`
### Tier 3: Blizzard Storm (`spell_blizzard_storm` — `RadialAoENova`)
- **Description:** Summons a violent sub-zero blizzard around the caster. (Damage `48`, Cooldown `5.5s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_blizzard_storm.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Blizzard Storm" (Ice / Cryomancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #00E5FF primary energy and #00727F background depth, depicting summons a violent sub-zero blizzard around the caster., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_blizzard_storm_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Blizzard Storm" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #7FF2FF and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (RadialAoENova) — summons a violent sub-zero blizzard around the caster., intense #FFFFFF center stepping through #00E5FF to #00727F trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #00E5FF particles and status application visual (Chill).`
### Tier 4 (Evolved Cataclysm): Absolute Zero (`spell_absolute_zero` — `RadialAoENova`)
- **Description:** Flash-freezes all surrounding existence to absolute zero. (Damage `90`, Cooldown `4.5s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_absolute_zero.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Absolute Zero" (Ice / Cryomancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #00E5FF primary energy and #00727F background depth, depicting flash-freezes all surrounding existence to absolute zero., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_absolute_zero_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Absolute Zero" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #7FF2FF and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — flash-freezes all surrounding existence to absolute zero., intense #FFFFFF center stepping through #00E5FF to #00727F trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #00E5FF particles and status application visual (Chill).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_ice.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Chill: Freezes foes, reducing movement and attack speed by 25%.": small orbiting/rising #00E5FF and #7FF2FF elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_ice.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Thermal Shock" (Ice + Fire): dual-color shockwave combining #00E5FF with Fire energy (striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_ice.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/ice/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/ice/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#00E5FF`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/ice/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/ice/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
