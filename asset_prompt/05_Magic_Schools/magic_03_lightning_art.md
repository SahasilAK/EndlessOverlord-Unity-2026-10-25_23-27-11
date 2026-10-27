# Lightning School — Electromancy (`MagicSchoolType.Lightning`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #03 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Lightning` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Lightning` | **School Hex:** `#FFD700`
> **School Passive:** Static Charge: Hits build static charges that discharge for 20% chain damage.
> **Cross-School Synergy:** **Electrified Deluge** (with `MagicSchoolType.Water`) — *Lightning spells chain to +2 additional targets in wet areas.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#FFEB7F` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#FFD700` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#7F6B00` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_lightning.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Lightning (Electromancy): "High-voltage electricity that arcs rapidly between clustered targets.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #FFD700 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Lightning School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_spark_arc` | **Spark Arc** | Tier 1 | `ChainingArc` | `14` | `0.65s` | `10` | `8` | Discharges an arc of electricity jumping between targets. |
| `spell_ball_lightning` | **Ball Lightning** | Tier 2 | `HomingSeeker` | `26` | `2.2s` | `24` | `9` | Releases slow-moving homing globes of concentrated plasma. |
| `spell_thunder_strike` | **Thunder Strike** | Tier 3 | `FallingImpact` | `58` | `5.0s` | `42` | `10` | Strikes the battlefield with a deafening bolt from the heavens. |
| `spell_cataclysm_tempest` | **Cataclysm Tempest** | Tier 4 (Evolved Cataclysm) | `FallingImpact` | `105` | `4.2s` | `48` | `12` | A divine tempest unleashing relentless lightning cascades. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Spark Arc (`spell_spark_arc` — `ChainingArc`)
- **Description:** Discharges an arc of electricity jumping between targets. (Damage `14`, Cooldown `0.65s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_spark_arc.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Spark Arc" (Lightning / Electromancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FFD700 primary energy and #7F6B00 background depth, depicting discharges an arc of electricity jumping between targets., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_spark_arc_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Spark Arc" (ChainingArc), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #FFEB7F and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (ChainingArc) — discharges an arc of electricity jumping between targets., intense #FFFFFF center stepping through #FFD700 to #7F6B00 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #FFD700 particles and status application visual (Static Charge).`
### Tier 2: Ball Lightning (`spell_ball_lightning` — `HomingSeeker`)
- **Description:** Releases slow-moving homing globes of concentrated plasma. (Damage `26`, Cooldown `2.2s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_ball_lightning.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Ball Lightning" (Lightning / Electromancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FFD700 primary energy and #7F6B00 background depth, depicting releases slow-moving homing globes of concentrated plasma., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_ball_lightning_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Ball Lightning" (HomingSeeker), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #FFEB7F and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (HomingSeeker) — releases slow-moving homing globes of concentrated plasma., intense #FFFFFF center stepping through #FFD700 to #7F6B00 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #FFD700 particles and status application visual (Static Charge).`
### Tier 3: Thunder Strike (`spell_thunder_strike` — `FallingImpact`)
- **Description:** Strikes the battlefield with a deafening bolt from the heavens. (Damage `58`, Cooldown `5.0s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_thunder_strike.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Thunder Strike" (Lightning / Electromancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FFD700 primary energy and #7F6B00 background depth, depicting strikes the battlefield with a deafening bolt from the heavens., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_thunder_strike_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Thunder Strike" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #FFEB7F and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (FallingImpact) — strikes the battlefield with a deafening bolt from the heavens., intense #FFFFFF center stepping through #FFD700 to #7F6B00 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #FFD700 particles and status application visual (Static Charge).`
### Tier 4 (Evolved Cataclysm): Cataclysm Tempest (`spell_cataclysm_tempest` — `FallingImpact`)
- **Description:** A divine tempest unleashing relentless lightning cascades. (Damage `105`, Cooldown `4.2s`, Range `12`).
- **Spell Book / HUD Icon Prompt (`icon_spell_cataclysm_tempest.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Cataclysm Tempest" (Lightning / Electromancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FFD700 primary energy and #7F6B00 background depth, depicting a divine tempest unleashing relentless lightning cascades., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_cataclysm_tempest_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Cataclysm Tempest" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #FFEB7F and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (FallingImpact) — a divine tempest unleashing relentless lightning cascades., intense #FFFFFF center stepping through #FFD700 to #7F6B00 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #FFD700 particles and status application visual (Static Charge).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_lightning.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Static Charge: Hits build static charges that discharge for 20% chain damage.": small orbiting/rising #FFD700 and #FFEB7F elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_lightning.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Electrified Deluge" (Lightning + Water): dual-color shockwave combining #FFD700 with Water energy (lightning spells chain to +2 additional targets in wet areas.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_lightning.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/lightning/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/lightning/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#FFD700`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/lightning/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/lightning/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
