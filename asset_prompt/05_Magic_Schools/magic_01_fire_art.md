# Fire School — Pyromancy (`MagicSchoolType.Fire`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #01 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Fire` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Fire` | **School Hex:** `#FF4500`
> **School Passive:** Ignite: All attacks inflict burning damage over 3 seconds (+15% DoT).
> **Cross-School Synergy:** **Thermal Shock** (with `MagicSchoolType.Ice`) — *Striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#FFA27F` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#FF4500` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#7F2200` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_fire.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Fire (Pyromancy): "Devastating incendiary magic that burns foes and spreads conflagration.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #FF4500 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Fire School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_fire_bolt` | **Fire Bolt** | Tier 1 | `DirectProjectile` | `16` | `0.7s` | `10` | `9` | Hurls a blazing bolt of fire. |
| `spell_flame_wave` | **Flame Wave** | Tier 2 | `WaveCone` | `28` | `2.5s` | `25` | `6` | Unleashes a sweeping crescent of flame. |
| `spell_meteor_strike` | **Meteor Strike** | Tier 3 | `FallingImpact` | `65` | `6.0s` | `45` | `10` | Calls down a colossal flaming meteor from the cosmos. |
| `spell_inferno_cataclysm` | **Inferno Cataclysm** | Tier 4 (Evolved Cataclysm) | `FallingImpact` | `110` | `5.0s` | `50` | `12` | Ascended cataclysm raining a cluster of superheated meteors. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Fire Bolt (`spell_fire_bolt` — `DirectProjectile`)
- **Description:** Hurls a blazing bolt of fire. (Damage `16`, Cooldown `0.7s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_fire_bolt.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Fire Bolt" (Fire / Pyromancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FF4500 primary energy and #7F2200 background depth, depicting hurls a blazing bolt of fire., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_fire_bolt_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Fire Bolt" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #FFA27F and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — hurls a blazing bolt of fire., intense #FFFFFF center stepping through #FF4500 to #7F2200 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #FF4500 particles and status application visual (Ignite).`
### Tier 2: Flame Wave (`spell_flame_wave` — `WaveCone`)
- **Description:** Unleashes a sweeping crescent of flame. (Damage `28`, Cooldown `2.5s`, Range `6`).
- **Spell Book / HUD Icon Prompt (`icon_spell_flame_wave.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Flame Wave" (Fire / Pyromancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FF4500 primary energy and #7F2200 background depth, depicting unleashes a sweeping crescent of flame., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_flame_wave_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Flame Wave" (WaveCone), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #FFA27F and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (WaveCone) — unleashes a sweeping crescent of flame., intense #FFFFFF center stepping through #FF4500 to #7F2200 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #FF4500 particles and status application visual (Ignite).`
### Tier 3: Meteor Strike (`spell_meteor_strike` — `FallingImpact`)
- **Description:** Calls down a colossal flaming meteor from the cosmos. (Damage `65`, Cooldown `6.0s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_meteor_strike.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Meteor Strike" (Fire / Pyromancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FF4500 primary energy and #7F2200 background depth, depicting calls down a colossal flaming meteor from the cosmos., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_meteor_strike_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Meteor Strike" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #FFA27F and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (FallingImpact) — calls down a colossal flaming meteor from the cosmos., intense #FFFFFF center stepping through #FF4500 to #7F2200 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #FF4500 particles and status application visual (Ignite).`
### Tier 4 (Evolved Cataclysm): Inferno Cataclysm (`spell_inferno_cataclysm` — `FallingImpact`)
- **Description:** Ascended cataclysm raining a cluster of superheated meteors. (Damage `110`, Cooldown `5.0s`, Range `12`).
- **Spell Book / HUD Icon Prompt (`icon_spell_inferno_cataclysm.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Inferno Cataclysm" (Fire / Pyromancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FF4500 primary energy and #7F2200 background depth, depicting ascended cataclysm raining a cluster of superheated meteors., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_inferno_cataclysm_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Inferno Cataclysm" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #FFA27F and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (FallingImpact) — ascended cataclysm raining a cluster of superheated meteors., intense #FFFFFF center stepping through #FF4500 to #7F2200 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #FF4500 particles and status application visual (Ignite).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_fire.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Ignite: All attacks inflict burning damage over 3 seconds (+15% DoT).": small orbiting/rising #FF4500 and #FFA27F elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_fire.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Thermal Shock" (Fire + Ice): dual-color shockwave combining #FF4500 with Ice energy (striking frozen enemies with fire triggers explosive shatter for +25% bonus damage.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_fire.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/fire/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/fire/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#FF4500`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/fire/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/fire/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
