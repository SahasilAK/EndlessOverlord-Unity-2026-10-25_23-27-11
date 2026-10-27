# Shadow School — Umbromancy (`MagicSchoolType.Shadow`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #09 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Shadow` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Shadow` | **School Hex:** `#312E81`
> **School Passive:** Shadowstep: Dodging leaves behind a shadow decoy distracting foes for 2s.
> **Cross-School Synergy:** **Abyssal Eclipse** (with `MagicSchoolType.Dark`) — *Dark and shadow spells have 15% chance to instantly execute non-bosses below 15% HP.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#9896C0` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#312E81` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#181740` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_shadow.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Shadow (Umbromancy): "Elusive occult arts weakening foes from the darkness.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #312E81 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Shadow School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_shadow_tendril` | **Shadow Tendril** | Tier 1 | `DirectProjectile` | `14` | `0.6s` | `9` | `9` | Lashes out with an ethereal tendril of darkness. |
| `spell_umbral_veil` | **Umbral Veil** | Tier 2 | `AuraPulse` | `22` | `3.0s` | `22` | `4` | Pulses shadows dealing harm and concealing caster. |
| `spell_black_sun` | **Black Sun** | Tier 3 | `RadialAoENova` | `54` | `5.2s` | `40` | `8` | Manifests an eclipsing black star that consumes light. |
| `spell_event_horizon_umbra` | **Umbral Event Horizon** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `96` | `4.4s` | `45` | `10` | Complete shadow collapse sucking light and life. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Shadow Tendril (`spell_shadow_tendril` — `DirectProjectile`)
- **Description:** Lashes out with an ethereal tendril of darkness. (Damage `14`, Cooldown `0.6s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_shadow_tendril.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Shadow Tendril" (Shadow / Umbromancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #312E81 primary energy and #181740 background depth, depicting lashes out with an ethereal tendril of darkness., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_shadow_tendril_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Shadow Tendril" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #9896C0 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — lashes out with an ethereal tendril of darkness., intense #FFFFFF center stepping through #312E81 to #181740 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #312E81 particles and status application visual (Shadowstep).`
### Tier 2: Umbral Veil (`spell_umbral_veil` — `AuraPulse`)
- **Description:** Pulses shadows dealing harm and concealing caster. (Damage `22`, Cooldown `3.0s`, Range `4`).
- **Spell Book / HUD Icon Prompt (`icon_spell_umbral_veil.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Umbral Veil" (Shadow / Umbromancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #312E81 primary energy and #181740 background depth, depicting pulses shadows dealing harm and concealing caster., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_umbral_veil_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Umbral Veil" (AuraPulse), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #9896C0 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (AuraPulse) — pulses shadows dealing harm and concealing caster., intense #FFFFFF center stepping through #312E81 to #181740 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #312E81 particles and status application visual (Shadowstep).`
### Tier 3: Black Sun (`spell_black_sun` — `RadialAoENova`)
- **Description:** Manifests an eclipsing black star that consumes light. (Damage `54`, Cooldown `5.2s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_black_sun.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Black Sun" (Shadow / Umbromancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #312E81 primary energy and #181740 background depth, depicting manifests an eclipsing black star that consumes light., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_black_sun_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Black Sun" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #9896C0 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (RadialAoENova) — manifests an eclipsing black star that consumes light., intense #FFFFFF center stepping through #312E81 to #181740 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #312E81 particles and status application visual (Shadowstep).`
### Tier 4 (Evolved Cataclysm): Umbral Event Horizon (`spell_event_horizon_umbra` — `RadialAoENova`)
- **Description:** Complete shadow collapse sucking light and life. (Damage `96`, Cooldown `4.4s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_event_horizon_umbra.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Umbral Event Horizon" (Shadow / Umbromancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #312E81 primary energy and #181740 background depth, depicting complete shadow collapse sucking light and life., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_event_horizon_umbra_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Umbral Event Horizon" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #9896C0 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — complete shadow collapse sucking light and life., intense #FFFFFF center stepping through #312E81 to #181740 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #312E81 particles and status application visual (Shadowstep).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_shadow.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Shadowstep: Dodging leaves behind a shadow decoy distracting foes for 2s.": small orbiting/rising #312E81 and #9896C0 elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_shadow.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Abyssal Eclipse" (Shadow + Dark): dual-color shockwave combining #312E81 with Dark energy (dark and shadow spells have 15% chance to instantly execute non-bosses below 15% hp.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_shadow.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/shadow/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/shadow/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#312E81`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/shadow/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/shadow/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
