# Light School — Photomancy (`MagicSchoolType.Light`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #10 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Light` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Light` | **School Hex:** `#FEF08A`
> **School Passive:** Luminescence: Illuminates hit targets, taking 15% increased damage from all sources.
> **Cross-School Synergy:** **Celestial Radiance** (with `MagicSchoolType.Holy`) — *Radiant and holy spells cleanse slow and heal for 5% max HP on cast.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#FEF7C4` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#FEF08A` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#7F7845` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_light.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Light (Photomancy): "Blinding beams and illuminating surges that disintegrate corruption.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #FEF08A and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Light School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_radiant_beam` | **Radiant Beam** | Tier 1 | `ContinuousBeam` | `16` | `0.75s` | `11` | `8.5` | Focuses a piercing ray of pure daylight. |
| `spell_prismatic_lance` | **Prismatic Lance** | Tier 2 | `DirectProjectile` | `27` | `2.6s` | `24` | `10` | Projects a refracting spear of multi-spectrum light. |
| `spell_solar_flare` | **Solar Flare** | Tier 3 | `RadialAoENova` | `56` | `5.4s` | `42` | `8` | Erupts a blinding solar nova igniting all foes. |
| `spell_supernova_burst` | **Supernova Burst** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `102` | `4.5s` | `48` | `11` | Unleashes the blinding ignition of a newborn star. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Radiant Beam (`spell_radiant_beam` — `ContinuousBeam`)
- **Description:** Focuses a piercing ray of pure daylight. (Damage `16`, Cooldown `0.75s`, Range `8.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_radiant_beam.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Radiant Beam" (Light / Photomancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FEF08A primary energy and #7F7845 background depth, depicting focuses a piercing ray of pure daylight., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_radiant_beam_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Radiant Beam" (ContinuousBeam), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #FEF7C4 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (ContinuousBeam) — focuses a piercing ray of pure daylight., intense #FFFFFF center stepping through #FEF08A to #7F7845 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #FEF08A particles and status application visual (Luminescence).`
### Tier 2: Prismatic Lance (`spell_prismatic_lance` — `DirectProjectile`)
- **Description:** Projects a refracting spear of multi-spectrum light. (Damage `27`, Cooldown `2.6s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_prismatic_lance.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Prismatic Lance" (Light / Photomancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FEF08A primary energy and #7F7845 background depth, depicting projects a refracting spear of multi-spectrum light., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_prismatic_lance_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Prismatic Lance" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #FEF7C4 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — projects a refracting spear of multi-spectrum light., intense #FFFFFF center stepping through #FEF08A to #7F7845 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #FEF08A particles and status application visual (Luminescence).`
### Tier 3: Solar Flare (`spell_solar_flare` — `RadialAoENova`)
- **Description:** Erupts a blinding solar nova igniting all foes. (Damage `56`, Cooldown `5.4s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_solar_flare.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Solar Flare" (Light / Photomancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FEF08A primary energy and #7F7845 background depth, depicting erupts a blinding solar nova igniting all foes., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_solar_flare_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Solar Flare" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #FEF7C4 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (RadialAoENova) — erupts a blinding solar nova igniting all foes., intense #FFFFFF center stepping through #FEF08A to #7F7845 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #FEF08A particles and status application visual (Luminescence).`
### Tier 4 (Evolved Cataclysm): Supernova Burst (`spell_supernova_burst` — `RadialAoENova`)
- **Description:** Unleashes the blinding ignition of a newborn star. (Damage `102`, Cooldown `4.5s`, Range `11`).
- **Spell Book / HUD Icon Prompt (`icon_spell_supernova_burst.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Supernova Burst" (Light / Photomancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FEF08A primary energy and #7F7845 background depth, depicting unleashes the blinding ignition of a newborn star., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_supernova_burst_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Supernova Burst" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #FEF7C4 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — unleashes the blinding ignition of a newborn star., intense #FFFFFF center stepping through #FEF08A to #7F7845 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #FEF08A particles and status application visual (Luminescence).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_light.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Luminescence: Illuminates hit targets, taking 15% increased damage from all sources.": small orbiting/rising #FEF08A and #FEF7C4 elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_light.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Celestial Radiance" (Light + Holy): dual-color shockwave combining #FEF08A with Holy energy (radiant and holy spells cleanse slow and heal for 5% max hp on cast.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_light.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/light/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/light/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#FEF08A`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/light/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/light/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
