# Earth School — Geomancy (`MagicSchoolType.Earth`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #06 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Earth` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Earth` | **School Hex:** `#B45309`
> **School Passive:** Stoneform: Hardens skin, reducing incoming damage by flat 8 armor.
> **Cross-School Synergy:** **Tectonic Forge** (with `MagicSchoolType.Metal`) — *Increases armor by 15% and physical/earth spell damage by 20%.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#D9A984` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#B45309` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#5A2904` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_earth.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Earth (Geomancy): "Crushing tectonic magic utilizing boulders, tremors, and granite armor.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #B45309 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Earth School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_stone_barrage` | **Stone Barrage** | Tier 1 | `DirectProjectile` | `15` | `0.75s` | `10` | `8` | Hurls a cluster of dense jagged rocks. |
| `spell_tremor_quake` | **Tremor Quake** | Tier 2 | `RadialAoENova` | `27` | `3.2s` | `26` | `6` | Slams the ground sending seismic shockwaves. |
| `spell_boulder_catapult` | **Boulder Catapult** | Tier 3 | `FallingImpact` | `62` | `5.8s` | `44` | `9` | Drops a massive granite monolith onto enemy ranks. |
| `spell_tectonic_shatter` | **Tectonic Shatter** | Tier 4 (Evolved Cataclysm) | `FallingImpact` | `112` | `4.8s` | `50` | `11` | Shatters tectonic plates under the entire arena. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Stone Barrage (`spell_stone_barrage` — `DirectProjectile`)
- **Description:** Hurls a cluster of dense jagged rocks. (Damage `15`, Cooldown `0.75s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_stone_barrage.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Stone Barrage" (Earth / Geomancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #B45309 primary energy and #5A2904 background depth, depicting hurls a cluster of dense jagged rocks., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_stone_barrage_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Stone Barrage" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #D9A984 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — hurls a cluster of dense jagged rocks., intense #FFFFFF center stepping through #B45309 to #5A2904 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #B45309 particles and status application visual (Stoneform).`
### Tier 2: Tremor Quake (`spell_tremor_quake` — `RadialAoENova`)
- **Description:** Slams the ground sending seismic shockwaves. (Damage `27`, Cooldown `3.2s`, Range `6`).
- **Spell Book / HUD Icon Prompt (`icon_spell_tremor_quake.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Tremor Quake" (Earth / Geomancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #B45309 primary energy and #5A2904 background depth, depicting slams the ground sending seismic shockwaves., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_tremor_quake_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Tremor Quake" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #D9A984 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (RadialAoENova) — slams the ground sending seismic shockwaves., intense #FFFFFF center stepping through #B45309 to #5A2904 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #B45309 particles and status application visual (Stoneform).`
### Tier 3: Boulder Catapult (`spell_boulder_catapult` — `FallingImpact`)
- **Description:** Drops a massive granite monolith onto enemy ranks. (Damage `62`, Cooldown `5.8s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_boulder_catapult.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Boulder Catapult" (Earth / Geomancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #B45309 primary energy and #5A2904 background depth, depicting drops a massive granite monolith onto enemy ranks., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_boulder_catapult_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Boulder Catapult" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #D9A984 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (FallingImpact) — drops a massive granite monolith onto enemy ranks., intense #FFFFFF center stepping through #B45309 to #5A2904 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #B45309 particles and status application visual (Stoneform).`
### Tier 4 (Evolved Cataclysm): Tectonic Shatter (`spell_tectonic_shatter` — `FallingImpact`)
- **Description:** Shatters tectonic plates under the entire arena. (Damage `112`, Cooldown `4.8s`, Range `11`).
- **Spell Book / HUD Icon Prompt (`icon_spell_tectonic_shatter.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Tectonic Shatter" (Earth / Geomancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #B45309 primary energy and #5A2904 background depth, depicting shatters tectonic plates under the entire arena., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_tectonic_shatter_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Tectonic Shatter" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #D9A984 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (FallingImpact) — shatters tectonic plates under the entire arena., intense #FFFFFF center stepping through #B45309 to #5A2904 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #B45309 particles and status application visual (Stoneform).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_earth.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Stoneform: Hardens skin, reducing incoming damage by flat 8 armor.": small orbiting/rising #B45309 and #D9A984 elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_earth.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Tectonic Forge" (Earth + Metal): dual-color shockwave combining #B45309 with Metal energy (increases armor by 15% and physical/earth spell damage by 20%.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_earth.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/earth/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/earth/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#B45309`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/earth/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/earth/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
