# Spirit School — Spiritism (`MagicSchoolType.Spirit`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #14 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Spirit` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Spirit` | **School Hex:** `#67E8F9`
> **School Passive:** Ethereal Form: 15% chance to ignore incoming projectile damage entirely.
> **Cross-School Synergy:** **Aetheric Resonance** (with `MagicSchoolType.Arcane`) — *Cooldown reduction increased by 10% and mana costs reduced by 15%.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#B3F3FC` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#67E8F9` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#33747C` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_spirit.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Spirit (Spiritism): "Astral apparitions that bypass physical armor completely.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #67E8F9 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Spirit School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_ethereal_wisp` | **Ethereal Wisp** | Tier 1 | `HomingSeeker` | `14` | `0.6s` | `9` | `9` | Releases glowing spirit wisps that phase through obstacles. |
| `spell_spectral_wail` | **Spectral Wail** | Tier 2 | `WaveCone` | `25` | `2.9s` | `23` | `6.5` | A chilling ghostly shriek that terrifies foes. |
| `spell_astral_projection` | **Astral Projection** | Tier 3 | `OrbitingShield` | `48` | `5.2s` | `38` | `4` | Summons spectral avatars rotating in defense. |
| `spell_spirit_pandemonium` | **Astral Pandemonium** | Tier 4 (Evolved Cataclysm) | `OrbitingShield` | `94` | `4.2s` | `44` | `6` | A chaotic storm of ancestors rending enemies to bits. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Ethereal Wisp (`spell_ethereal_wisp` — `HomingSeeker`)
- **Description:** Releases glowing spirit wisps that phase through obstacles. (Damage `14`, Cooldown `0.6s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_ethereal_wisp.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Ethereal Wisp" (Spirit / Spiritism, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #67E8F9 primary energy and #33747C background depth, depicting releases glowing spirit wisps that phase through obstacles., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_ethereal_wisp_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Ethereal Wisp" (HomingSeeker), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #B3F3FC and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (HomingSeeker) — releases glowing spirit wisps that phase through obstacles., intense #FFFFFF center stepping through #67E8F9 to #33747C trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #67E8F9 particles and status application visual (Ethereal Form).`
### Tier 2: Spectral Wail (`spell_spectral_wail` — `WaveCone`)
- **Description:** A chilling ghostly shriek that terrifies foes. (Damage `25`, Cooldown `2.9s`, Range `6.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_spectral_wail.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Spectral Wail" (Spirit / Spiritism, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #67E8F9 primary energy and #33747C background depth, depicting a chilling ghostly shriek that terrifies foes., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_spectral_wail_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Spectral Wail" (WaveCone), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #B3F3FC and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (WaveCone) — a chilling ghostly shriek that terrifies foes., intense #FFFFFF center stepping through #67E8F9 to #33747C trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #67E8F9 particles and status application visual (Ethereal Form).`
### Tier 3: Astral Projection (`spell_astral_projection` — `OrbitingShield`)
- **Description:** Summons spectral avatars rotating in defense. (Damage `48`, Cooldown `5.2s`, Range `4`).
- **Spell Book / HUD Icon Prompt (`icon_spell_astral_projection.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Astral Projection" (Spirit / Spiritism, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #67E8F9 primary energy and #33747C background depth, depicting summons spectral avatars rotating in defense., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_astral_projection_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Astral Projection" (OrbitingShield), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #B3F3FC and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (OrbitingShield) — summons spectral avatars rotating in defense., intense #FFFFFF center stepping through #67E8F9 to #33747C trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #67E8F9 particles and status application visual (Ethereal Form).`
### Tier 4 (Evolved Cataclysm): Astral Pandemonium (`spell_spirit_pandemonium` — `OrbitingShield`)
- **Description:** A chaotic storm of ancestors rending enemies to bits. (Damage `94`, Cooldown `4.2s`, Range `6`).
- **Spell Book / HUD Icon Prompt (`icon_spell_spirit_pandemonium.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Astral Pandemonium" (Spirit / Spiritism, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #67E8F9 primary energy and #33747C background depth, depicting a chaotic storm of ancestors rending enemies to bits., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_spirit_pandemonium_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Astral Pandemonium" (OrbitingShield), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #B3F3FC and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (OrbitingShield) — a chaotic storm of ancestors rending enemies to bits., intense #FFFFFF center stepping through #67E8F9 to #33747C trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #67E8F9 particles and status application visual (Ethereal Form).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_spirit.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Ethereal Form: 15% chance to ignore incoming projectile damage entirely.": small orbiting/rising #67E8F9 and #B3F3FC elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_spirit.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Aetheric Resonance" (Spirit + Arcane): dual-color shockwave combining #67E8F9 with Arcane energy (cooldown reduction increased by 10% and mana costs reduced by 15%.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_spirit.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/spirit/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/spirit/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#67E8F9`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/spirit/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/spirit/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
