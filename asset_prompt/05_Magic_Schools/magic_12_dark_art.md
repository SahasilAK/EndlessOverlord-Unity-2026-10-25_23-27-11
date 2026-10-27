# Dark School — Noctumancy (`MagicSchoolType.Dark`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #12 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Dark` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Dark` | **School Hex:** `#1E1B4B`
> **School Passive:** Despair: Enemies afflicted deal 20% reduced damage.
> **Cross-School Synergy:** **Abyssal Eclipse** (with `MagicSchoolType.Shadow`) — *Dark and shadow spells have 15% chance to instantly execute non-bosses below 15% HP.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#8E8DA5` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#1E1B4B` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#0F0D25` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_dark.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Dark (Noctumancy): "Abyssal entropy that corrodes willpower and pulls matter into nothingness.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #1E1B4B and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Dark School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_dark_matter` | **Dark Matter** | Tier 1 | `HomingSeeker` | `15` | `0.7s` | `10` | `9` | Launches seeking globules of heavy dark matter. |
| `spell_abyssal_grasp` | **Abyssal Grasp** | Tier 2 | `GroundHazard` | `26` | `3.2s` | `25` | `7` | Summons grasping shadows rooting foes to the floor. |
| `spell_singularity_tear` | **Singularity Tear** | Tier 3 | `FallingImpact` | `60` | `5.6s` | `44` | `9` | Tears open a pocket singularity swallowing reality. |
| `spell_void_oblivion_core` | **Oblivion Core** | Tier 4 (Evolved Cataclysm) | `FallingImpact` | `108` | `4.6s` | `50` | `11` | An inescapable micro-black hole devouring target zone. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Dark Matter (`spell_dark_matter` — `HomingSeeker`)
- **Description:** Launches seeking globules of heavy dark matter. (Damage `15`, Cooldown `0.7s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_dark_matter.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Dark Matter" (Dark / Noctumancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #1E1B4B primary energy and #0F0D25 background depth, depicting launches seeking globules of heavy dark matter., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_dark_matter_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Dark Matter" (HomingSeeker), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #8E8DA5 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (HomingSeeker) — launches seeking globules of heavy dark matter., intense #FFFFFF center stepping through #1E1B4B to #0F0D25 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #1E1B4B particles and status application visual (Despair).`
### Tier 2: Abyssal Grasp (`spell_abyssal_grasp` — `GroundHazard`)
- **Description:** Summons grasping shadows rooting foes to the floor. (Damage `26`, Cooldown `3.2s`, Range `7`).
- **Spell Book / HUD Icon Prompt (`icon_spell_abyssal_grasp.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Abyssal Grasp" (Dark / Noctumancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #1E1B4B primary energy and #0F0D25 background depth, depicting summons grasping shadows rooting foes to the floor., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_abyssal_grasp_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Abyssal Grasp" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #8E8DA5 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (GroundHazard) — summons grasping shadows rooting foes to the floor., intense #FFFFFF center stepping through #1E1B4B to #0F0D25 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #1E1B4B particles and status application visual (Despair).`
### Tier 3: Singularity Tear (`spell_singularity_tear` — `FallingImpact`)
- **Description:** Tears open a pocket singularity swallowing reality. (Damage `60`, Cooldown `5.6s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_singularity_tear.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Singularity Tear" (Dark / Noctumancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #1E1B4B primary energy and #0F0D25 background depth, depicting tears open a pocket singularity swallowing reality., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_singularity_tear_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Singularity Tear" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #8E8DA5 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (FallingImpact) — tears open a pocket singularity swallowing reality., intense #FFFFFF center stepping through #1E1B4B to #0F0D25 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #1E1B4B particles and status application visual (Despair).`
### Tier 4 (Evolved Cataclysm): Oblivion Core (`spell_void_oblivion_core` — `FallingImpact`)
- **Description:** An inescapable micro-black hole devouring target zone. (Damage `108`, Cooldown `4.6s`, Range `11`).
- **Spell Book / HUD Icon Prompt (`icon_spell_void_oblivion_core.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Oblivion Core" (Dark / Noctumancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #1E1B4B primary energy and #0F0D25 background depth, depicting an inescapable micro-black hole devouring target zone., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_void_oblivion_core_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Oblivion Core" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #8E8DA5 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (FallingImpact) — an inescapable micro-black hole devouring target zone., intense #FFFFFF center stepping through #1E1B4B to #0F0D25 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #1E1B4B particles and status application visual (Despair).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_dark.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Despair: Enemies afflicted deal 20% reduced damage.": small orbiting/rising #1E1B4B and #8E8DA5 elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_dark.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Abyssal Eclipse" (Dark + Shadow): dual-color shockwave combining #1E1B4B with Shadow energy (dark and shadow spells have 15% chance to instantly execute non-bosses below 15% hp.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_dark.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/dark/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/dark/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#1E1B4B`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/dark/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/dark/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
