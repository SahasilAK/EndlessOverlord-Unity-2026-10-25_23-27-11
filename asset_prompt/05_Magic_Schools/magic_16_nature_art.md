# Nature School — Vitamancy (`MagicSchoolType.Nature`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #16 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Nature` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Nature` | **School Hex:** `#22C55E`
> **School Passive:** Regeneration: Regenerates 2% of maximum health every 3 seconds.
> **Cross-School Synergy:** **Virulent Spores** (with `MagicSchoolType.Poison`) — *Poison clouds spawn stinging thorn vines dealing 20% bonus DPS.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#90E2AE` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#22C55E` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#11622F` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_nature.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Nature (Vitamancy): "Vigorous organic growth using brambles, thorn whips, and regenerative sap.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #22C55E and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Nature School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_thorn_whip` | **Thorn Whip** | Tier 1 | `WaveCone` | `15` | `0.7s` | `9` | `6.5` | Lashes out with a barbed thorny vine. |
| `spell_entangling_roots` | **Entangling Roots** | Tier 2 | `GroundHazard` | `24` | `3.2s` | `22` | `7.5` | Roots foes to the spot while thorny vines crush them. |
| `spell_wrath_of_ancients` | **Wrath of the Ancients** | Tier 3 | `RadialAoENova` | `52` | `5.5s` | `38` | `8.5` | Awakens primeval flora sweeping all intruders away. |
| `spell_primordial_gaia` | **Primordial Gaia** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `98` | `4.5s` | `45` | `10.5` | Transforms the arena into a lethal primeval jungle. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Thorn Whip (`spell_thorn_whip` — `WaveCone`)
- **Description:** Lashes out with a barbed thorny vine. (Damage `15`, Cooldown `0.7s`, Range `6.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_thorn_whip.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Thorn Whip" (Nature / Vitamancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #22C55E primary energy and #11622F background depth, depicting lashes out with a barbed thorny vine., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_thorn_whip_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Thorn Whip" (WaveCone), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #90E2AE and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (WaveCone) — lashes out with a barbed thorny vine., intense #FFFFFF center stepping through #22C55E to #11622F trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #22C55E particles and status application visual (Regeneration).`
### Tier 2: Entangling Roots (`spell_entangling_roots` — `GroundHazard`)
- **Description:** Roots foes to the spot while thorny vines crush them. (Damage `24`, Cooldown `3.2s`, Range `7.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_entangling_roots.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Entangling Roots" (Nature / Vitamancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #22C55E primary energy and #11622F background depth, depicting roots foes to the spot while thorny vines crush them., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_entangling_roots_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Entangling Roots" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #90E2AE and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (GroundHazard) — roots foes to the spot while thorny vines crush them., intense #FFFFFF center stepping through #22C55E to #11622F trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #22C55E particles and status application visual (Regeneration).`
### Tier 3: Wrath of the Ancients (`spell_wrath_of_ancients` — `RadialAoENova`)
- **Description:** Awakens primeval flora sweeping all intruders away. (Damage `52`, Cooldown `5.5s`, Range `8.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_wrath_of_ancients.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Wrath of the Ancients" (Nature / Vitamancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #22C55E primary energy and #11622F background depth, depicting awakens primeval flora sweeping all intruders away., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_wrath_of_ancients_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Wrath of the Ancients" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #90E2AE and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (RadialAoENova) — awakens primeval flora sweeping all intruders away., intense #FFFFFF center stepping through #22C55E to #11622F trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #22C55E particles and status application visual (Regeneration).`
### Tier 4 (Evolved Cataclysm): Primordial Gaia (`spell_primordial_gaia` — `RadialAoENova`)
- **Description:** Transforms the arena into a lethal primeval jungle. (Damage `98`, Cooldown `4.5s`, Range `10.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_primordial_gaia.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Primordial Gaia" (Nature / Vitamancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #22C55E primary energy and #11622F background depth, depicting transforms the arena into a lethal primeval jungle., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_primordial_gaia_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Primordial Gaia" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #90E2AE and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — transforms the arena into a lethal primeval jungle., intense #FFFFFF center stepping through #22C55E to #11622F trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #22C55E particles and status application visual (Regeneration).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_nature.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Regeneration: Regenerates 2% of maximum health every 3 seconds.": small orbiting/rising #22C55E and #90E2AE elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_nature.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Virulent Spores" (Nature + Poison): dual-color shockwave combining #22C55E with Poison energy (poison clouds spawn stinging thorn vines dealing 20% bonus dps.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_nature.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/nature/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/nature/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#22C55E`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/nature/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/nature/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
