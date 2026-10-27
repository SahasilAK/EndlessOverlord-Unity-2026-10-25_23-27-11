# Metal School — Ferromancy (`MagicSchoolType.Metal`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #20 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Metal` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Metal` | **School Hex:** `#94A3B8`
> **School Passive:** Iron Skin: Increases total armor by 25% and reflects 10% melee damage.
> **Cross-School Synergy:** **Tectonic Forge** (with `MagicSchoolType.Earth`) — *Increases armor by 15% and physical/earth spell damage by 20%.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#C9D1DB` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#94A3B8` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#4A515C` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_metal.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Metal (Ferromancy): "Magnetic telekinesis flinging deadly blades and crushing iron maidens.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #94A3B8 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Metal School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_shrapnel_shot` | **Shrapnel Shot** | Tier 1 | `WaveCone` | `16` | `0.65s` | `10` | `7` | Blasts a conical cone of iron flechettes. |
| `spell_blade_orbit` | **Blade Orbit** | Tier 2 | `OrbitingShield` | `25` | `3.2s` | `25` | `3.5` | Spins whirling steel blades orbiting around caster. |
| `spell_iron_maiden_crush` | **Iron Maiden Crush** | Tier 3 | `FallingImpact` | `62` | `5.8s` | `44` | `9` | Calls down a colossal iron maiden slamming shut on enemies. |
| `spell_colossus_anvil` | **Colossus God Anvil** | Tier 4 (Evolved Cataclysm) | `FallingImpact` | `110` | `4.6s` | `50` | `11` | An astronomical forge anvil crushing all foes below. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Shrapnel Shot (`spell_shrapnel_shot` — `WaveCone`)
- **Description:** Blasts a conical cone of iron flechettes. (Damage `16`, Cooldown `0.65s`, Range `7`).
- **Spell Book / HUD Icon Prompt (`icon_spell_shrapnel_shot.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Shrapnel Shot" (Metal / Ferromancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #94A3B8 primary energy and #4A515C background depth, depicting blasts a conical cone of iron flechettes., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_shrapnel_shot_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Shrapnel Shot" (WaveCone), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #C9D1DB and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (WaveCone) — blasts a conical cone of iron flechettes., intense #FFFFFF center stepping through #94A3B8 to #4A515C trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #94A3B8 particles and status application visual (Iron Skin).`
### Tier 2: Blade Orbit (`spell_blade_orbit` — `OrbitingShield`)
- **Description:** Spins whirling steel blades orbiting around caster. (Damage `25`, Cooldown `3.2s`, Range `3.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_blade_orbit.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Blade Orbit" (Metal / Ferromancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #94A3B8 primary energy and #4A515C background depth, depicting spins whirling steel blades orbiting around caster., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_blade_orbit_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Blade Orbit" (OrbitingShield), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #C9D1DB and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (OrbitingShield) — spins whirling steel blades orbiting around caster., intense #FFFFFF center stepping through #94A3B8 to #4A515C trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #94A3B8 particles and status application visual (Iron Skin).`
### Tier 3: Iron Maiden Crush (`spell_iron_maiden_crush` — `FallingImpact`)
- **Description:** Calls down a colossal iron maiden slamming shut on enemies. (Damage `62`, Cooldown `5.8s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_iron_maiden_crush.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Iron Maiden Crush" (Metal / Ferromancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #94A3B8 primary energy and #4A515C background depth, depicting calls down a colossal iron maiden slamming shut on enemies., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_iron_maiden_crush_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Iron Maiden Crush" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #C9D1DB and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (FallingImpact) — calls down a colossal iron maiden slamming shut on enemies., intense #FFFFFF center stepping through #94A3B8 to #4A515C trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #94A3B8 particles and status application visual (Iron Skin).`
### Tier 4 (Evolved Cataclysm): Colossus God Anvil (`spell_colossus_anvil` — `FallingImpact`)
- **Description:** An astronomical forge anvil crushing all foes below. (Damage `110`, Cooldown `4.6s`, Range `11`).
- **Spell Book / HUD Icon Prompt (`icon_spell_colossus_anvil.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Colossus God Anvil" (Metal / Ferromancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #94A3B8 primary energy and #4A515C background depth, depicting an astronomical forge anvil crushing all foes below., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_colossus_anvil_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Colossus God Anvil" (FallingImpact), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #C9D1DB and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (FallingImpact) — an astronomical forge anvil crushing all foes below., intense #FFFFFF center stepping through #94A3B8 to #4A515C trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #94A3B8 particles and status application visual (Iron Skin).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_metal.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Iron Skin: Increases total armor by 25% and reflects 10% melee damage.": small orbiting/rising #94A3B8 and #C9D1DB elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_metal.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Tectonic Forge" (Metal + Earth): dual-color shockwave combining #94A3B8 with Earth energy (increases armor by 15% and physical/earth spell damage by 20%.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_metal.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/metal/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/metal/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#94A3B8`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/metal/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/metal/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
