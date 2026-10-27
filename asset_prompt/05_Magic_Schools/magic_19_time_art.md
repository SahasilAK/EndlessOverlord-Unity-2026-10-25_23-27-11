# Time School — Chronomancy (`MagicSchoolType.Time`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #19 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Time` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Time` | **School Hex:** `#38BDF8`
> **School Passive:** Chronos Ward: +15% Cooldown Reduction across all skills and spells.
> **Cross-School Synergy:** **Temporal Acceleration** (with `MagicSchoolType.Wind`) — *Movement speed and cast speed increased by 15%.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#9BDEFB` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#38BDF8` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#1C5E7C` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_time.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Time (Chronomancy): "Temporal manipulation accelerating allies and freezing foes in stasis.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #38BDF8 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Time School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_chrono_bolt` | **Chrono Bolt** | Tier 1 | `DirectProjectile` | `14` | `0.6s` | `9` | `9` | Fires a temporal distortion slowing target's timeline. |
| `spell_temporal_stutter` | **Temporal Stutter** | Tier 2 | `AuraPulse` | `22` | `3.2s` | `22` | `4` | Dilates time locally, hindering approaching enemy strikes. |
| `spell_time_dilation_field` | **Time Dilation Field** | Tier 3 | `GroundHazard` | `48` | `5.5s` | `40` | `8.5` | Creates a chronosphere slowing all invaders by 70%. |
| `spell_chronos_shatter` | **Chronos Temporal Shatter** | Tier 4 (Evolved Cataclysm) | `GroundHazard` | `92` | `4.5s` | `45` | `10.5` | Freezes enemies in stasis then shatters their timeline. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Chrono Bolt (`spell_chrono_bolt` — `DirectProjectile`)
- **Description:** Fires a temporal distortion slowing target's timeline. (Damage `14`, Cooldown `0.6s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_chrono_bolt.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Chrono Bolt" (Time / Chronomancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #38BDF8 primary energy and #1C5E7C background depth, depicting fires a temporal distortion slowing target's timeline., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_chrono_bolt_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Chrono Bolt" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #9BDEFB and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — fires a temporal distortion slowing target's timeline., intense #FFFFFF center stepping through #38BDF8 to #1C5E7C trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #38BDF8 particles and status application visual (Chronos Ward).`
### Tier 2: Temporal Stutter (`spell_temporal_stutter` — `AuraPulse`)
- **Description:** Dilates time locally, hindering approaching enemy strikes. (Damage `22`, Cooldown `3.2s`, Range `4`).
- **Spell Book / HUD Icon Prompt (`icon_spell_temporal_stutter.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Temporal Stutter" (Time / Chronomancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #38BDF8 primary energy and #1C5E7C background depth, depicting dilates time locally, hindering approaching enemy strikes., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_temporal_stutter_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Temporal Stutter" (AuraPulse), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #9BDEFB and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (AuraPulse) — dilates time locally, hindering approaching enemy strikes., intense #FFFFFF center stepping through #38BDF8 to #1C5E7C trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #38BDF8 particles and status application visual (Chronos Ward).`
### Tier 3: Time Dilation Field (`spell_time_dilation_field` — `GroundHazard`)
- **Description:** Creates a chronosphere slowing all invaders by 70%. (Damage `48`, Cooldown `5.5s`, Range `8.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_time_dilation_field.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Time Dilation Field" (Time / Chronomancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #38BDF8 primary energy and #1C5E7C background depth, depicting creates a chronosphere slowing all invaders by 70%., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_time_dilation_field_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Time Dilation Field" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #9BDEFB and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (GroundHazard) — creates a chronosphere slowing all invaders by 70%., intense #FFFFFF center stepping through #38BDF8 to #1C5E7C trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #38BDF8 particles and status application visual (Chronos Ward).`
### Tier 4 (Evolved Cataclysm): Chronos Temporal Shatter (`spell_chronos_shatter` — `GroundHazard`)
- **Description:** Freezes enemies in stasis then shatters their timeline. (Damage `92`, Cooldown `4.5s`, Range `10.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_chronos_shatter.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Chronos Temporal Shatter" (Time / Chronomancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #38BDF8 primary energy and #1C5E7C background depth, depicting freezes enemies in stasis then shatters their timeline., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_chronos_shatter_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Chronos Temporal Shatter" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #9BDEFB and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (GroundHazard) — freezes enemies in stasis then shatters their timeline., intense #FFFFFF center stepping through #38BDF8 to #1C5E7C trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #38BDF8 particles and status application visual (Chronos Ward).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_time.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Chronos Ward: +15% Cooldown Reduction across all skills and spells.": small orbiting/rising #38BDF8 and #9BDEFB elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_time.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Temporal Acceleration" (Time + Wind): dual-color shockwave combining #38BDF8 with Wind energy (movement speed and cast speed increased by 15%.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_time.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/time/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/time/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#38BDF8`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/time/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/time/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
