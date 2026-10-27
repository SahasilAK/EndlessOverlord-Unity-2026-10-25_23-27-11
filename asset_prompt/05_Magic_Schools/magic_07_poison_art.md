# Poison School — Toximancy (`MagicSchoolType.Poison`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #07 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Poison` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Poison` | **School Hex:** `#10B981`
> **School Passive:** Corrosion: Reduces enemy armor by 30% while afflicted with poison.
> **Cross-School Synergy:** **Virulent Spores** (with `MagicSchoolType.Nature`) — *Poison clouds spawn stinging thorn vines dealing 20% bonus DPS.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#87DCC0` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#10B981` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#085C40` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_poison.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Poison (Toximancy): "Virulent corrosive bile that eats through armor and inflicts stacking toxins.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #10B981 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Poison School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_toxic_spitter` | **Toxic Spitter** | Tier 1 | `DirectProjectile` | `12` | `0.55s` | `9` | `8.5` | Spits caustic acid burning flesh on contact. |
| `spell_venom_cloud` | **Venom Cloud** | Tier 2 | `GroundHazard` | `24` | `3.2s` | `24` | `7` | Creates a toxic miasma choking all who enter. |
| `spell_plague_infestation` | **Plague Infestation** | Tier 3 | `ChainingArc` | `50` | `5.0s` | `38` | `9` | Injects virulent disease leaping violently between hosts. |
| `spell_nox_extinction` | **Noxious Extinction** | Tier 4 (Evolved Cataclysm) | `GroundHazard` | `92` | `4.2s` | `46` | `10` | Spreads an apocalyptic airborne neurotoxin. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Toxic Spitter (`spell_toxic_spitter` — `DirectProjectile`)
- **Description:** Spits caustic acid burning flesh on contact. (Damage `12`, Cooldown `0.55s`, Range `8.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_toxic_spitter.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Toxic Spitter" (Poison / Toximancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #10B981 primary energy and #085C40 background depth, depicting spits caustic acid burning flesh on contact., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_toxic_spitter_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Toxic Spitter" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #87DCC0 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — spits caustic acid burning flesh on contact., intense #FFFFFF center stepping through #10B981 to #085C40 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #10B981 particles and status application visual (Corrosion).`
### Tier 2: Venom Cloud (`spell_venom_cloud` — `GroundHazard`)
- **Description:** Creates a toxic miasma choking all who enter. (Damage `24`, Cooldown `3.2s`, Range `7`).
- **Spell Book / HUD Icon Prompt (`icon_spell_venom_cloud.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Venom Cloud" (Poison / Toximancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #10B981 primary energy and #085C40 background depth, depicting creates a toxic miasma choking all who enter., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_venom_cloud_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Venom Cloud" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #87DCC0 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (GroundHazard) — creates a toxic miasma choking all who enter., intense #FFFFFF center stepping through #10B981 to #085C40 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #10B981 particles and status application visual (Corrosion).`
### Tier 3: Plague Infestation (`spell_plague_infestation` — `ChainingArc`)
- **Description:** Injects virulent disease leaping violently between hosts. (Damage `50`, Cooldown `5.0s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_plague_infestation.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Plague Infestation" (Poison / Toximancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #10B981 primary energy and #085C40 background depth, depicting injects virulent disease leaping violently between hosts., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_plague_infestation_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Plague Infestation" (ChainingArc), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #87DCC0 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (ChainingArc) — injects virulent disease leaping violently between hosts., intense #FFFFFF center stepping through #10B981 to #085C40 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #10B981 particles and status application visual (Corrosion).`
### Tier 4 (Evolved Cataclysm): Noxious Extinction (`spell_nox_extinction` — `GroundHazard`)
- **Description:** Spreads an apocalyptic airborne neurotoxin. (Damage `92`, Cooldown `4.2s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_nox_extinction.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Noxious Extinction" (Poison / Toximancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #10B981 primary energy and #085C40 background depth, depicting spreads an apocalyptic airborne neurotoxin., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_nox_extinction_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Noxious Extinction" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #87DCC0 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (GroundHazard) — spreads an apocalyptic airborne neurotoxin., intense #FFFFFF center stepping through #10B981 to #085C40 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #10B981 particles and status application visual (Corrosion).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_poison.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Corrosion: Reduces enemy armor by 30% while afflicted with poison.": small orbiting/rising #10B981 and #87DCC0 elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_poison.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Virulent Spores" (Poison + Nature): dual-color shockwave combining #10B981 with Nature energy (poison clouds spawn stinging thorn vines dealing 20% bonus dps.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_poison.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/poison/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/poison/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#10B981`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/poison/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/poison/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
