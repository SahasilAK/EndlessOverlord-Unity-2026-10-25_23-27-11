# Blood School — Haemomancy (`MagicSchoolType.Blood`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #08 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Blood` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Blood` | **School Hex:** `#991B1B`
> **School Passive:** Sanguine Thirst: 12% of all damage dealt is converted into health.
> **Cross-School Synergy:** **Sanguine Lichdom** (with `MagicSchoolType.Necromancy`) — *Minions and soul attacks heal the caster for 12% of damage.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#CC8D8D` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#991B1B` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#4C0D0D` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_blood.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Blood (Haemomancy): "Forbidden vitality magic draining life force from enemies to heal the caster.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #991B1B and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Blood School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_blood_lance` | **Blood Lance** | Tier 1 | `DirectProjectile` | `15` | `0.7s` | `10` | `9` | Fires a coagulated needle of vampiric blood. |
| `spell_crimson_siphon` | **Crimson Siphon** | Tier 2 | `ContinuousBeam` | `26` | `2.8s` | `24` | `7` | Extracts enemy blood directly into caster's essence. |
| `spell_exsanguinate_burst` | **Exsanguinate Burst** | Tier 3 | `RadialAoENova` | `55` | `5.5s` | `42` | `8` | Detonates the blood vessels of surrounding invaders. |
| `spell_sanguine_ascendance` | **Sanguine Ascendance** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `100` | `4.5s` | `48` | `10` | Ascends into a blood avatar draining the battlefield. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Blood Lance (`spell_blood_lance` — `DirectProjectile`)
- **Description:** Fires a coagulated needle of vampiric blood. (Damage `15`, Cooldown `0.7s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_blood_lance.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Blood Lance" (Blood / Haemomancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #991B1B primary energy and #4C0D0D background depth, depicting fires a coagulated needle of vampiric blood., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_blood_lance_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Blood Lance" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #CC8D8D and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — fires a coagulated needle of vampiric blood., intense #FFFFFF center stepping through #991B1B to #4C0D0D trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #991B1B particles and status application visual (Sanguine Thirst).`
### Tier 2: Crimson Siphon (`spell_crimson_siphon` — `ContinuousBeam`)
- **Description:** Extracts enemy blood directly into caster's essence. (Damage `26`, Cooldown `2.8s`, Range `7`).
- **Spell Book / HUD Icon Prompt (`icon_spell_crimson_siphon.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Crimson Siphon" (Blood / Haemomancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #991B1B primary energy and #4C0D0D background depth, depicting extracts enemy blood directly into caster's essence., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_crimson_siphon_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Crimson Siphon" (ContinuousBeam), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #CC8D8D and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (ContinuousBeam) — extracts enemy blood directly into caster's essence., intense #FFFFFF center stepping through #991B1B to #4C0D0D trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #991B1B particles and status application visual (Sanguine Thirst).`
### Tier 3: Exsanguinate Burst (`spell_exsanguinate_burst` — `RadialAoENova`)
- **Description:** Detonates the blood vessels of surrounding invaders. (Damage `55`, Cooldown `5.5s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_exsanguinate_burst.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Exsanguinate Burst" (Blood / Haemomancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #991B1B primary energy and #4C0D0D background depth, depicting detonates the blood vessels of surrounding invaders., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_exsanguinate_burst_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Exsanguinate Burst" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #CC8D8D and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (RadialAoENova) — detonates the blood vessels of surrounding invaders., intense #FFFFFF center stepping through #991B1B to #4C0D0D trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #991B1B particles and status application visual (Sanguine Thirst).`
### Tier 4 (Evolved Cataclysm): Sanguine Ascendance (`spell_sanguine_ascendance` — `RadialAoENova`)
- **Description:** Ascends into a blood avatar draining the battlefield. (Damage `100`, Cooldown `4.5s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_sanguine_ascendance.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Sanguine Ascendance" (Blood / Haemomancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #991B1B primary energy and #4C0D0D background depth, depicting ascends into a blood avatar draining the battlefield., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_sanguine_ascendance_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Sanguine Ascendance" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #CC8D8D and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — ascends into a blood avatar draining the battlefield., intense #FFFFFF center stepping through #991B1B to #4C0D0D trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #991B1B particles and status application visual (Sanguine Thirst).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_blood.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Sanguine Thirst: 12% of all damage dealt is converted into health.": small orbiting/rising #991B1B and #CC8D8D elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_blood.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Sanguine Lichdom" (Blood + Necromancy): dual-color shockwave combining #991B1B with Necromancy energy (minions and soul attacks heal the caster for 12% of damage.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_blood.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/blood/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/blood/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#991B1B`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/blood/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/blood/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
