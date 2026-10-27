# Water School — Hydromancy (`MagicSchoolType.Water`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #04 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Water` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Water` | **School Hex:** `#1E90FF`
> **School Passive:** Drenched: Foes become soaked, increasing susceptibility to lightning and frost by 20%.
> **Cross-School Synergy:** **Electrified Deluge** (with `MagicSchoolType.Lightning`) — *Lightning spells chain to +2 additional targets in wet areas.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#8EC7FF` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#1E90FF` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#0F487F` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_water.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Water (Hydromancy): "Fluid kinetic magic that knocks back foes and soaks the arena.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #1E90FF and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Water School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_water_jet` | **Water Jet** | Tier 1 | `ContinuousBeam` | `15` | `0.8s` | `12` | `8` | Channels a focused high-pressure beam of water. |
| `spell_tidal_surge` | **Tidal Surge** | Tier 2 | `WaveCone` | `25` | `2.8s` | `22` | `7` | Pushes enemies back with a rushing wave. |
| `spell_maelstrom_vortex` | **Maelstrom Vortex** | Tier 3 | `RadialAoENova` | `52` | `5.8s` | `38` | `8` | Forms a roaring oceanic whirlpool dragging enemies inward. |
| `spell_deluge_abyss` | **Deluge of the Abyss** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `95` | `4.8s` | `44` | `10` | Floods the arena with crushing abyssal water pressures. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Water Jet (`spell_water_jet` — `ContinuousBeam`)
- **Description:** Channels a focused high-pressure beam of water. (Damage `15`, Cooldown `0.8s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_water_jet.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Water Jet" (Water / Hydromancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #1E90FF primary energy and #0F487F background depth, depicting channels a focused high-pressure beam of water., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_water_jet_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Water Jet" (ContinuousBeam), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #8EC7FF and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (ContinuousBeam) — channels a focused high-pressure beam of water., intense #FFFFFF center stepping through #1E90FF to #0F487F trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #1E90FF particles and status application visual (Drenched).`
### Tier 2: Tidal Surge (`spell_tidal_surge` — `WaveCone`)
- **Description:** Pushes enemies back with a rushing wave. (Damage `25`, Cooldown `2.8s`, Range `7`).
- **Spell Book / HUD Icon Prompt (`icon_spell_tidal_surge.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Tidal Surge" (Water / Hydromancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #1E90FF primary energy and #0F487F background depth, depicting pushes enemies back with a rushing wave., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_tidal_surge_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Tidal Surge" (WaveCone), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #8EC7FF and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (WaveCone) — pushes enemies back with a rushing wave., intense #FFFFFF center stepping through #1E90FF to #0F487F trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #1E90FF particles and status application visual (Drenched).`
### Tier 3: Maelstrom Vortex (`spell_maelstrom_vortex` — `RadialAoENova`)
- **Description:** Forms a roaring oceanic whirlpool dragging enemies inward. (Damage `52`, Cooldown `5.8s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_maelstrom_vortex.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Maelstrom Vortex" (Water / Hydromancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #1E90FF primary energy and #0F487F background depth, depicting forms a roaring oceanic whirlpool dragging enemies inward., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_maelstrom_vortex_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Maelstrom Vortex" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #8EC7FF and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (RadialAoENova) — forms a roaring oceanic whirlpool dragging enemies inward., intense #FFFFFF center stepping through #1E90FF to #0F487F trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #1E90FF particles and status application visual (Drenched).`
### Tier 4 (Evolved Cataclysm): Deluge of the Abyss (`spell_deluge_abyss` — `RadialAoENova`)
- **Description:** Floods the arena with crushing abyssal water pressures. (Damage `95`, Cooldown `4.8s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_deluge_abyss.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Deluge of the Abyss" (Water / Hydromancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #1E90FF primary energy and #0F487F background depth, depicting floods the arena with crushing abyssal water pressures., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_deluge_abyss_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Deluge of the Abyss" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #8EC7FF and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — floods the arena with crushing abyssal water pressures., intense #FFFFFF center stepping through #1E90FF to #0F487F trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #1E90FF particles and status application visual (Drenched).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_water.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Drenched: Foes become soaked, increasing susceptibility to lightning and frost by 20%.": small orbiting/rising #1E90FF and #8EC7FF elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_water.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Electrified Deluge" (Water + Lightning): dual-color shockwave combining #1E90FF with Lightning energy (lightning spells chain to +2 additional targets in wet areas.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_water.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/water/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/water/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#1E90FF`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/water/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/water/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
