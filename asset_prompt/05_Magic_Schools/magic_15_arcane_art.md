# Arcane School — Arcanomancy (`MagicSchoolType.Arcane`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #15 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Arcane` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Arcane` | **School Hex:** `#C084FC`
> **School Passive:** Arcane Surge: Spell critical hits grant +25% cast speed for 3 seconds.
> **Cross-School Synergy:** **Aetheric Resonance** (with `MagicSchoolType.Spirit`) — *Cooldown reduction increased by 10% and mana costs reduced by 15%.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#DFC1FD` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#C084FC` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#60427E` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_arcane.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Arcane (Arcanomancy): "Pure primordial mana manipulated into disintegrating energy.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #C084FC and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Arcane School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_magic_missile` | **Magic Missile** | Tier 1 | `HomingSeeker` | `13` | `0.55s` | `8` | `9` | Fires tri-force seeker darts of concentrated mana. |
| `spell_arcane_orb` | **Arcane Orb** | Tier 2 | `DirectProjectile` | `28` | `2.5s` | `24` | `9` | Hurls a slow pulsating globe emitting arcane pulses. |
| `spell_disintegration_ray` | **Disintegration Ray** | Tier 3 | `ContinuousBeam` | `64` | `5.0s` | `45` | `10` | Channels a devastating beam vaporizing matter. |
| `spell_nether_singularity` | **Nether Singularity** | Tier 4 (Evolved Cataclysm) | `ContinuousBeam` | `115` | `4.0s` | `50` | `12` | Supreme arcane beam piercing all barriers and shields. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Magic Missile (`spell_magic_missile` — `HomingSeeker`)
- **Description:** Fires tri-force seeker darts of concentrated mana. (Damage `13`, Cooldown `0.55s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_magic_missile.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Magic Missile" (Arcane / Arcanomancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #C084FC primary energy and #60427E background depth, depicting fires tri-force seeker darts of concentrated mana., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_magic_missile_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Magic Missile" (HomingSeeker), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #DFC1FD and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (HomingSeeker) — fires tri-force seeker darts of concentrated mana., intense #FFFFFF center stepping through #C084FC to #60427E trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #C084FC particles and status application visual (Arcane Surge).`
### Tier 2: Arcane Orb (`spell_arcane_orb` — `DirectProjectile`)
- **Description:** Hurls a slow pulsating globe emitting arcane pulses. (Damage `28`, Cooldown `2.5s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_arcane_orb.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Arcane Orb" (Arcane / Arcanomancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #C084FC primary energy and #60427E background depth, depicting hurls a slow pulsating globe emitting arcane pulses., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_arcane_orb_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Arcane Orb" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #DFC1FD and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — hurls a slow pulsating globe emitting arcane pulses., intense #FFFFFF center stepping through #C084FC to #60427E trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #C084FC particles and status application visual (Arcane Surge).`
### Tier 3: Disintegration Ray (`spell_disintegration_ray` — `ContinuousBeam`)
- **Description:** Channels a devastating beam vaporizing matter. (Damage `64`, Cooldown `5.0s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_disintegration_ray.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Disintegration Ray" (Arcane / Arcanomancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #C084FC primary energy and #60427E background depth, depicting channels a devastating beam vaporizing matter., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_disintegration_ray_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Disintegration Ray" (ContinuousBeam), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #DFC1FD and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (ContinuousBeam) — channels a devastating beam vaporizing matter., intense #FFFFFF center stepping through #C084FC to #60427E trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #C084FC particles and status application visual (Arcane Surge).`
### Tier 4 (Evolved Cataclysm): Nether Singularity (`spell_nether_singularity` — `ContinuousBeam`)
- **Description:** Supreme arcane beam piercing all barriers and shields. (Damage `115`, Cooldown `4.0s`, Range `12`).
- **Spell Book / HUD Icon Prompt (`icon_spell_nether_singularity.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Nether Singularity" (Arcane / Arcanomancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #C084FC primary energy and #60427E background depth, depicting supreme arcane beam piercing all barriers and shields., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_nether_singularity_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Nether Singularity" (ContinuousBeam), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #DFC1FD and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (ContinuousBeam) — supreme arcane beam piercing all barriers and shields., intense #FFFFFF center stepping through #C084FC to #60427E trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #C084FC particles and status application visual (Arcane Surge).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_arcane.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Arcane Surge: Spell critical hits grant +25% cast speed for 3 seconds.": small orbiting/rising #C084FC and #DFC1FD elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_arcane.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Aetheric Resonance" (Arcane + Spirit): dual-color shockwave combining #C084FC with Spirit energy (cooldown reduction increased by 10% and mana costs reduced by 15%.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_arcane.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/arcane/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/arcane/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#C084FC`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/arcane/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/arcane/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
