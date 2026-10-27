# Wind School — Aeromancy (`MagicSchoolType.Wind`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #05 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Wind` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Wind` | **School Hex:** `#A7F3D0`
> **School Passive:** Tailwind: Grants +12% movement speed and +10% projectile velocity.
> **Cross-School Synergy:** **Temporal Acceleration** (with `MagicSchoolType.Time`) — *Movement speed and cast speed increased by 15%.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#D3F9E7` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#A7F3D0` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#537968` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_wind.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Wind (Aeromancy): "High-speed vacuum blades and defensive squalls enhancing mobility.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #A7F3D0 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Wind School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_gale_blade` | **Gale Blade** | Tier 1 | `DirectProjectile` | `14` | `0.5s` | `8` | `10` | Fling a slicing blade of razor-sharp wind. |
| `spell_cyclone_barrier` | **Cyclone Barrier** | Tier 2 | `OrbitingShield` | `22` | `3.5s` | `25` | `3` | Spins defensive air currents that shred close foes. |
| `spell_twister_squall` | **Twister Squall** | Tier 3 | `GroundHazard` | `46` | `5.2s` | `36` | `9` | Summons a roaming tornado tossing foes into chaos. |
| `spell_tempest_monsoon` | **Tempest Monsoon** | Tier 4 (Evolved Cataclysm) | `GroundHazard` | `88` | `4.4s` | `42` | `11` | A raging gale cyclone covering massive ground. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Gale Blade (`spell_gale_blade` — `DirectProjectile`)
- **Description:** Fling a slicing blade of razor-sharp wind. (Damage `14`, Cooldown `0.5s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_gale_blade.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Gale Blade" (Wind / Aeromancy, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #A7F3D0 primary energy and #537968 background depth, depicting fling a slicing blade of razor-sharp wind., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_gale_blade_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Gale Blade" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #D3F9E7 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — fling a slicing blade of razor-sharp wind., intense #FFFFFF center stepping through #A7F3D0 to #537968 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #A7F3D0 particles and status application visual (Tailwind).`
### Tier 2: Cyclone Barrier (`spell_cyclone_barrier` — `OrbitingShield`)
- **Description:** Spins defensive air currents that shred close foes. (Damage `22`, Cooldown `3.5s`, Range `3`).
- **Spell Book / HUD Icon Prompt (`icon_spell_cyclone_barrier.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Cyclone Barrier" (Wind / Aeromancy, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #A7F3D0 primary energy and #537968 background depth, depicting spins defensive air currents that shred close foes., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_cyclone_barrier_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Cyclone Barrier" (OrbitingShield), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #D3F9E7 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (OrbitingShield) — spins defensive air currents that shred close foes., intense #FFFFFF center stepping through #A7F3D0 to #537968 trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #A7F3D0 particles and status application visual (Tailwind).`
### Tier 3: Twister Squall (`spell_twister_squall` — `GroundHazard`)
- **Description:** Summons a roaming tornado tossing foes into chaos. (Damage `46`, Cooldown `5.2s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_twister_squall.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Twister Squall" (Wind / Aeromancy, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #A7F3D0 primary energy and #537968 background depth, depicting summons a roaming tornado tossing foes into chaos., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_twister_squall_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Twister Squall" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #D3F9E7 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (GroundHazard) — summons a roaming tornado tossing foes into chaos., intense #FFFFFF center stepping through #A7F3D0 to #537968 trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #A7F3D0 particles and status application visual (Tailwind).`
### Tier 4 (Evolved Cataclysm): Tempest Monsoon (`spell_tempest_monsoon` — `GroundHazard`)
- **Description:** A raging gale cyclone covering massive ground. (Damage `88`, Cooldown `4.4s`, Range `11`).
- **Spell Book / HUD Icon Prompt (`icon_spell_tempest_monsoon.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Tempest Monsoon" (Wind / Aeromancy, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #A7F3D0 primary energy and #537968 background depth, depicting a raging gale cyclone covering massive ground., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_tempest_monsoon_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Tempest Monsoon" (GroundHazard), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #D3F9E7 and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (GroundHazard) — a raging gale cyclone covering massive ground., intense #FFFFFF center stepping through #A7F3D0 to #537968 trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #A7F3D0 particles and status application visual (Tailwind).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_wind.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Tailwind: Grants +12% movement speed and +10% projectile velocity.": small orbiting/rising #A7F3D0 and #D3F9E7 elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_wind.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Temporal Acceleration" (Wind + Time): dual-color shockwave combining #A7F3D0 with Time energy (movement speed and cast speed increased by 15%.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_wind.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/wind/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/wind/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#A7F3D0`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/wind/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/wind/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
