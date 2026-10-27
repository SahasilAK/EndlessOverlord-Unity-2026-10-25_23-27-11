# Holy School — Theomancy (`MagicSchoolType.Holy`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #11 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Holy` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Holy` | **School Hex:** `#FACC15`
> **School Passive:** Divine Ward: Periodically grants a barrier absorbing up to 20 damage.
> **Cross-School Synergy:** **Celestial Radiance** (with `MagicSchoolType.Light`) — *Radiant and holy spells cleanse slow and heal for 5% max HP on cast.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#FCE58A` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#FACC15` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#7D660A` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_holy.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Holy (Theomancy): "Sacred divine judgment that purges darkness and protects allies.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #FACC15 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Holy School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_holy_smite` | **Holy Smite** | Tier 1 | `FallingImpact` | `16` | `0.8s` | `10` | `8` | Strikes a foe with sacred judgment from above. |
| `spell_consecrated_aura` | **Consecrated Aura** | Tier 2 | `AuraPulse` | `24` | `3.2s` | `24` | `5` | Sanctifies ground around caster, burning impurities. |
| `spell_divine_wrath` | **Divine Wrath** | Tier 3 | `RadialAoENova` | `58` | `5.5s` | `40` | `9` | Heavens descend in a brilliant blast of righteous fury. |
| `spell_judgment_of_archons` | **Judgment of the Archons** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `106` | `4.6s` | `46` | `11` | Archangels' wrath obliterating evil across the sanctuary. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Holy Smite (`spell_holy_smite` — `FallingImpact`)
- **Description:** Strikes a foe with sacred judgment from above. (Damage `16`, Cooldown `0.8s`, Range `8`).
- **Spell Book / HUD Icon Prompt (`icon_spell_holy_smite.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Holy Smite" (Holy / Theomancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FACC15 primary energy and #7D660A background depth, depicting strikes a foe with sacred judgment from above., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_holy_smite_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Holy Smite" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #FCE58A and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (FallingImpact) — strikes a foe with sacred judgment from above., intense #FFFFFF center stepping through #FACC15 to #7D660A trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #FACC15 particles and status application visual (Divine Ward).`
### Tier 2: Consecrated Aura (`spell_consecrated_aura` — `AuraPulse`)
- **Description:** Sanctifies ground around caster, burning impurities. (Damage `24`, Cooldown `3.2s`, Range `5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_consecrated_aura.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Consecrated Aura" (Holy / Theomancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FACC15 primary energy and #7D660A background depth, depicting sanctifies ground around caster, burning impurities., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_consecrated_aura_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Consecrated Aura" (AuraPulse), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #FCE58A and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (AuraPulse) — sanctifies ground around caster, burning impurities., intense #FFFFFF center stepping through #FACC15 to #7D660A trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #FACC15 particles and status application visual (Divine Ward).`
### Tier 3: Divine Wrath (`spell_divine_wrath` — `RadialAoENova`)
- **Description:** Heavens descend in a brilliant blast of righteous fury. (Damage `58`, Cooldown `5.5s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_divine_wrath.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Divine Wrath" (Holy / Theomancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FACC15 primary energy and #7D660A background depth, depicting heavens descend in a brilliant blast of righteous fury., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_divine_wrath_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Divine Wrath" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #FCE58A and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (RadialAoENova) — heavens descend in a brilliant blast of righteous fury., intense #FFFFFF center stepping through #FACC15 to #7D660A trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #FACC15 particles and status application visual (Divine Ward).`
### Tier 4 (Evolved Cataclysm): Judgment of the Archons (`spell_judgment_of_archons` — `RadialAoENova`)
- **Description:** Archangels' wrath obliterating evil across the sanctuary. (Damage `106`, Cooldown `4.6s`, Range `11`).
- **Spell Book / HUD Icon Prompt (`icon_spell_judgment_of_archons.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Judgment of the Archons" (Holy / Theomancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #FACC15 primary energy and #7D660A background depth, depicting archangels' wrath obliterating evil across the sanctuary., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_judgment_of_archons_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Judgment of the Archons" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #FCE58A and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — archangels' wrath obliterating evil across the sanctuary., intense #FFFFFF center stepping through #FACC15 to #7D660A trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #FACC15 particles and status application visual (Divine Ward).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_holy.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Divine Ward: Periodically grants a barrier absorbing up to 20 damage.": small orbiting/rising #FACC15 and #FCE58A elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_holy.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Celestial Radiance" (Holy + Light): dual-color shockwave combining #FACC15 with Light energy (radiant and holy spells cleanse slow and heal for 5% max hp on cast.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_holy.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/holy/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/holy/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#FACC15`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/holy/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/holy/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
