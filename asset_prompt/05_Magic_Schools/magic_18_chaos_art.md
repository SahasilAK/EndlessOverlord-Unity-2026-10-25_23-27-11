# Chaos School — Pandemonium (`MagicSchoolType.Chaos`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #18 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Chaos` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Chaos` | **School Hex:** `#F43F5E`
> **School Passive:** Wild Surge: Spells have 20% chance to cast a second random spell free.
> **Cross-School Synergy:** **Entropy Singularity** (with `MagicSchoolType.Void`) — *Critical strikes rip mini-rifts pulling enemies in for 30 damage.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#F99FAE` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#F43F5E` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#7A1F2F` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_chaos.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Chaos (Pandemonium): "Wild unchained entropic magic with variable and explosive results.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #F43F5E and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Chaos School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_chaos_bolt` | **Chaos Bolt** | Tier 1 | `DirectProjectile` | `17` | `0.65s` | `10` | `9` | Fires an erratic bolt surging with unpredictable fury. |
| `spell_entropic_flux` | **Entropic Flux** | Tier 2 | `AuraPulse` | `26` | `2.9s` | `24` | `5` | Pulses random elemental damage waves around caster. |
| `spell_cosmic_anarchy` | **Cosmic Anarchy** | Tier 3 | `RadialAoENova` | `60` | `5.4s` | `42` | `8.5` | Unleashes multi-elemental explosive devastation. |
| `spell_entropy_unbound` | **Entropy Unbound** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `110` | `4.4s` | `48` | `11` | Catastrophic breakdown of natural laws in target zone. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Chaos Bolt (`spell_chaos_bolt` — `DirectProjectile`)
- **Description:** Fires an erratic bolt surging with unpredictable fury. (Damage `17`, Cooldown `0.65s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_chaos_bolt.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Chaos Bolt" (Chaos / Pandemonium, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #F43F5E primary energy and #7A1F2F background depth, depicting fires an erratic bolt surging with unpredictable fury., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_chaos_bolt_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Chaos Bolt" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #F99FAE and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — fires an erratic bolt surging with unpredictable fury., intense #FFFFFF center stepping through #F43F5E to #7A1F2F trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #F43F5E particles and status application visual (Wild Surge).`
### Tier 2: Entropic Flux (`spell_entropic_flux` — `AuraPulse`)
- **Description:** Pulses random elemental damage waves around caster. (Damage `26`, Cooldown `2.9s`, Range `5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_entropic_flux.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Entropic Flux" (Chaos / Pandemonium, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #F43F5E primary energy and #7A1F2F background depth, depicting pulses random elemental damage waves around caster., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_entropic_flux_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Entropic Flux" (AuraPulse), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #F99FAE and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (AuraPulse) — pulses random elemental damage waves around caster., intense #FFFFFF center stepping through #F43F5E to #7A1F2F trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #F43F5E particles and status application visual (Wild Surge).`
### Tier 3: Cosmic Anarchy (`spell_cosmic_anarchy` — `RadialAoENova`)
- **Description:** Unleashes multi-elemental explosive devastation. (Damage `60`, Cooldown `5.4s`, Range `8.5`).
- **Spell Book / HUD Icon Prompt (`icon_spell_cosmic_anarchy.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Cosmic Anarchy" (Chaos / Pandemonium, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #F43F5E primary energy and #7A1F2F background depth, depicting unleashes multi-elemental explosive devastation., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_cosmic_anarchy_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Cosmic Anarchy" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #F99FAE and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (RadialAoENova) — unleashes multi-elemental explosive devastation., intense #FFFFFF center stepping through #F43F5E to #7A1F2F trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #F43F5E particles and status application visual (Wild Surge).`
### Tier 4 (Evolved Cataclysm): Entropy Unbound (`spell_entropy_unbound` — `RadialAoENova`)
- **Description:** Catastrophic breakdown of natural laws in target zone. (Damage `110`, Cooldown `4.4s`, Range `11`).
- **Spell Book / HUD Icon Prompt (`icon_spell_entropy_unbound.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Entropy Unbound" (Chaos / Pandemonium, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #F43F5E primary energy and #7A1F2F background depth, depicting catastrophic breakdown of natural laws in target zone., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_entropy_unbound_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "Entropy Unbound" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #F99FAE and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — catastrophic breakdown of natural laws in target zone., intense #FFFFFF center stepping through #F43F5E to #7A1F2F trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #F43F5E particles and status application visual (Wild Surge).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_chaos.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Wild Surge: Spells have 20% chance to cast a second random spell free.": small orbiting/rising #F43F5E and #F99FAE elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_chaos.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Entropy Singularity" (Chaos + Void): dual-color shockwave combining #F43F5E with Void energy (critical strikes rip mini-rifts pulling enemies in for 30 damage.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_chaos.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/chaos/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/chaos/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#F43F5E`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/chaos/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/chaos/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
