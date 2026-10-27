# Void School — Void Magic (`MagicSchoolType.Void`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #17 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Void` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Void` | **School Hex:** `#581C87`
> **School Passive:** Null Shield: Completely nullifies 1 out of every 5 incoming hits.
> **Cross-School Synergy:** **Entropy Singularity** (with `MagicSchoolType.Chaos`) — *Critical strikes rip mini-rifts pulling enemies in for 30 damage.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#AB8DC3` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#581C87` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#2C0E43` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_void.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Void (Void Magic): "Dimensional rips erasing physical matter from existence.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #581C87 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Void School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_void_rift` | **Void Rift** | Tier 1 | `GroundHazard` | `15` | `0.75s` | `10` | `8` | Rips open a localized dimensional tear. |
| `spell_null_lance` | **Null Lance** | Tier 2 | `DirectProjectile` | `28` | `2.6s` | `24` | `10` | Fires a beam ignoring 100% of armor and barriers. |
| `spell_dimensional_collapse` | **Dimensional Collapse** | Tier 3 | `FallingImpact` | `62` | `5.8s` | `44` | `9.5` | Collapses spatial dimensions onto the impact zone. |
| `spell_zero_point_cascade` | **Zero-Point Cascade** | Tier 4 (Evolved Cataclysm) | `FallingImpact` | `114` | `4.6s` | `50` | `11.5` | Zero-point collapse obliterating everything in range. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Void Rift (`spell_void_rift` — `GroundHazard`)
- **Description:** Rips open a localized dimensional tear. (Damage `15`, Cooldown `0.75s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_void_rift.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Void Rift" (Void / Void Magic, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #581C87 primary energy and #2C0E43 background depth, depicting rips open a localized dimensional tear., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_void_rift_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Void Rift" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #AB8DC3 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (GroundHazard) — rips open a localized dimensional tear., intense #FFFFFF center stepping through #581C87 to #2C0E43 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #581C87 particles and status application visual (Null Shield).`
### Tier 2: Null Lance (`spell_null_lance` — `DirectProjectile`)
- **Description:** Fires a beam ignoring 100% of armor and barriers. (Damage `28`, Cooldown `2.6s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_null_lance.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Null Lance" (Void / Void Magic, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #581C87 primary energy and #2C0E43 background depth, depicting fires a beam ignoring 100% of armor and barriers., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_null_lance_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Null Lance" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #AB8DC3 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — fires a beam ignoring 100% of armor and barriers., intense #FFFFFF center stepping through #581C87 to #2C0E43 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #581C87 particles and status application visual (Null Shield).`
### Tier 3: Dimensional Collapse (`spell_dimensional_collapse` — `FallingImpact`)
- **Description:** Collapses spatial dimensions onto the impact zone. (Damage `62`, Cooldown `5.8s`, Range `9.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_dimensional_collapse.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Dimensional Collapse" (Void / Void Magic, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #581C87 primary energy and #2C0E43 background depth, depicting collapses spatial dimensions onto the impact zone., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_dimensional_collapse_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Dimensional Collapse" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #AB8DC3 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (FallingImpact) — collapses spatial dimensions onto the impact zone., intense #FFFFFF center stepping through #581C87 to #2C0E43 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #581C87 particles and status application visual (Null Shield).`
### Tier 4 (Evolved Cataclysm): Zero-Point Cascade (`spell_zero_point_cascade` — `FallingImpact`)
- **Description:** Zero-point collapse obliterating everything in range. (Damage `114`, Cooldown `4.6s`, Range `11.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_zero_point_cascade.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Zero-Point Cascade" (Void / Void Magic, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #581C87 primary energy and #2C0E43 background depth, depicting zero-point collapse obliterating everything in range., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_zero_point_cascade_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Zero-Point Cascade" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #AB8DC3 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (FallingImpact) — zero-point collapse obliterating everything in range., intense #FFFFFF center stepping through #581C87 to #2C0E43 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #581C87 particles and status application visual (Null Shield).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_void.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Null Shield: Completely nullifies 1 out of every 5 incoming hits.": small orbiting/rising #581C87 and #AB8DC3 elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_void.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Entropy Singularity" (Void + Chaos): dual-color shockwave combining #581C87 with Chaos energy (critical strikes rip mini-rifts pulling enemies in for 30 damage.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_void.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/void/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/void/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#581C87`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/void/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/void/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
