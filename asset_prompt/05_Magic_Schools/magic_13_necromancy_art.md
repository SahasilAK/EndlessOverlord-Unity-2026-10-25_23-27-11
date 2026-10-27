# Necromancy School — Mortis (`MagicSchoolType.Necromancy`) — Art & VFX Prompt Specification

> **Asset Category:** Magic School #13 (`05_Magic_Schools`)
> **Catalog Enum:** `MagicSchoolType.Necromancy` (`MagicSchoolCatalog.cs`)
> **Damage Type:** `DamageType.Necromancy` | **School Hex:** `#4C1D95`
> **School Passive:** Soul Feast: Defeating enemies restores 5 resource and charges soul power.
> **Cross-School Synergy:** **Sanguine Lichdom** (with `MagicSchoolType.Blood`) — *Minions and soul attacks heal the caster for 12% of damage.*

---

## 1. Base Elemental Visual Identity & Palette

| Ramp Layer | Hex Code | Role in Spell VFX & Icons |
| :--- | :--- | :--- |
| **Core Incandescence** | `#FFFFFF` → `#A58ECA` | Innermost 20% of projectile cores, lightning arcs, and beam centers |
| **Primary Elemental Hue** | `#4C1D95` | Main body (55%) of flames/shards/waves (`MagicSchoolCatalog` primary hex) |
| **Outer Dissipation / Smoke** | `#260E4A` | Trailing embers, smoke edges, ground scorch decals |
| **Contrast Rim** | `#110E1B` | 1px outer silhouette border ensuring readability over any biome floor |

### 1.1 School Emblem / Mastery Icon Prompt (`icon_school_necromancy.png` — `24x24 px`)
```text
24x24 pixel art magic school crest icon for Necromancy (Mortis): "The dread domain of death, bone shards, and corpse consumption.".
1px dark outline (#110E1B), ornate dark-iron runic frame containing a glowing #4C1D95 and #FFFFFF elemental sigil, crisp pixel art, no anti-aliasing, transparent background (RGBA32).
```

---

## 2. All 4 Spells in Necromancy School (`MagicSchoolCatalog.cs`)

| Spell ID | Spell Name | Tier | Cast Behavior | Base Dmg | Cooldown | Cost | Range | Lore / Visual Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `spell_bone_spear` | **Bone Spear** | Tier 1 | `DirectProjectile` | `16` | `0.65s` | `10` | `10` | Hurls a calcified bone lance piercing enemy lines. |
| `spell_corpse_explosion` | **Corpse Explosion** | Tier 2 | `RadialAoENova` | `30` | `2.8s` | `26` | `7` | Detonates fallen remains in a lethal shrapnel burst. |
| `spell_death_coil` | **Death Coil** | Tier 3 | `HomingSeeker` | `56` | `5.0s` | `40` | `9` | Unleashes screaming death coils seeking souls. |
| `spell_reaper_final_harvest` | **The Final Harvest** | Tier 4 (Evolved Cataclysm) | `RadialAoENova` | `104` | `4.4s` | `48` | `11` | Reaps all souls within vicinity into eternal servitude. |

---

## 3. Step-by-Step Icon & 3-Frame VFX Strip Prompts per Spell

### Tier 1: Bone Spear (`spell_bone_spear` — `DirectProjectile`)
- **Description:** Hurls a calcified bone lance piercing enemy lines. (Damage `16`, Cooldown `0.65s`, Range `10`).
- **Spell Book / HUD Icon Prompt (`icon_spell_bone_spear.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Bone Spear" (Necromancy / Mortis, Tier 1), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #4C1D95 primary energy and #260E4A background depth, depicting hurls a calcified bone lance piercing enemy lines., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_bone_spear_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Bone Spear" (DirectProjectile), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #A58ECA and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (DirectProjectile) — hurls a calcified bone lance piercing enemy lines., intense #FFFFFF center stepping through #4C1D95 to #260E4A trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #4C1D95 particles and status application visual (Soul Feast).`
### Tier 2: Corpse Explosion (`spell_corpse_explosion` — `RadialAoENova`)
- **Description:** Detonates fallen remains in a lethal shrapnel burst. (Damage `30`, Cooldown `2.8s`, Range `7`).
- **Spell Book / HUD Icon Prompt (`icon_spell_corpse_explosion.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Corpse Explosion" (Necromancy / Mortis, Tier 2), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #4C1D95 primary energy and #260E4A background depth, depicting detonates fallen remains in a lethal shrapnel burst., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_corpse_explosion_strip.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame horizontal pixel art spell VFX strip (48x16 px, three 16x16 frames) for "Corpse Explosion" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (16x16): Cast Ignition — #A58ECA and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (16x16): Active Delivery (RadialAoENova) — detonates fallen remains in a lethal shrapnel burst., intense #FFFFFF center stepping through #4C1D95 to #260E4A trailing edges.`
  > `- Frame 3 (16x16): Target Impact / Detonation — radial shatter of #4C1D95 particles and status application visual (Soul Feast).`
### Tier 3: Death Coil (`spell_death_coil` — `HomingSeeker`)
- **Description:** Unleashes screaming death coils seeking souls. (Damage `56`, Cooldown `5.0s`, Range `9`).
- **Spell Book / HUD Icon Prompt (`icon_spell_death_coil.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "Death Coil" (Necromancy / Mortis, Tier 3), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #4C1D95 primary energy and #260E4A background depth, depicting unleashes screaming death coils seeking souls., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_death_coil_strip.png` — `96x32 px`, 3 frames of `32x32`):**
  > `3-frame horizontal pixel art spell VFX strip (96x32 px, three 32x32 frames) for "Death Coil" (HomingSeeker), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (32x32): Cast Ignition — #A58ECA and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (32x32): Active Delivery (HomingSeeker) — unleashes screaming death coils seeking souls., intense #FFFFFF center stepping through #4C1D95 to #260E4A trailing edges.`
  > `- Frame 3 (32x32): Target Impact / Detonation — radial shatter of #4C1D95 particles and status application visual (Soul Feast).`
### Tier 4 (Evolved Cataclysm): The Final Harvest (`spell_reaper_final_harvest` — `RadialAoENova`)
- **Description:** Reaps all souls within vicinity into eternal servitude. (Damage `104`, Cooldown `4.4s`, Range `11`).
- **Spell Book / HUD Icon Prompt (`icon_spell_reaper_final_harvest.png` — `16x16 px`, `16 PPU`):**
  > `16x16 pixel art spell icon for "The Final Harvest" (Necromancy / Mortis, Tier 4 (Evolved Cataclysm)), dark fantasy roguelite UI, 1px #110E1B border, glowing #FFFFFF core with #4C1D95 primary energy and #260E4A background depth, depicting reaps all souls within vicinity into eternal servitude., FilterMode.Point.`
- **3-Frame Spell VFX Strip Prompt (`vfx_spell_reaper_final_harvest_strip.png` — `192x64 px`, 3 frames of `64x64`):**
  > `3-frame horizontal pixel art spell VFX strip (192x64 px, three 64x64 frames) for "The Final Harvest" (RadialAoENova), transparent background, 1px #110E1B outer contrast border on solid silhouettes:`
  > `- Frame 1 (64x64): Cast Ignition — #A58ECA and #FFFFFF runic burst at caster hand.`
  > `- Frame 2 (64x64): Active Delivery (RadialAoENova) — reaps all souls within vicinity into eternal servitude., intense #FFFFFF center stepping through #4C1D95 to #260E4A trailing edges.`
  > `- Frame 3 (64x64): Target Impact / Detonation — radial shatter of #4C1D95 particles and status application visual (Soul Feast).`

---

## 4. Status Effect & Synergy Overlay Prompts

1. **Status Effect Overlay (`vfx_status_necromancy.png` — `32x16 px`, 2 frames of `16x16`):**
   > `2-frame looping 16x16 pixel art status effect overlay for "Soul Feast: Defeating enemies restores 5 resource and charges soul power.": small orbiting/rising #4C1D95 and #A58ECA elemental particles that attach to afflicted invader sprites.`
2. **Synergy Burst VFX (`vfx_synergy_necromancy.png` — `96x32 px`, 3 frames of `32x32`):**
   > `3-frame 32x32 pixel art elemental combo explosion for "Sanguine Lichdom" (Necromancy + Blood): dual-color shockwave combining #4C1D95 with Blood energy (minions and soul attacks heal the caster for 12% of damage.).`

---

## 5. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **School Emblem** | `Assets/_Game/Art/UI/Magic/icon_school_necromancy.png` | `24x24` | `16` | `(0.5, 0.5)` | `SkillTreeScreenPresenter` / reward card badge |
| **Spell Icons (4x)** | `Assets/_Game/Art/UI/Magic/necromancy/icon_<spell_id>.png` | `16x16` | `16` | `(0.5, 0.5)` | `HudPresenter` & `WaveRewardModalPresenter` icon |
| **Tier 1–2 VFX** | `Assets/_Game/Art/VFX/Magic/necromancy/vfx_<spell_id>_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` (`PlaceholderSprites.Circle` tinted `#4C1D95`) |
| **Tier 3 VFX** | `Assets/_Game/Art/VFX/Magic/necromancy/vfx_<spell_id>_strip.png` | `96x32` (`3x 32x32`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` AoE ring |
| **Tier 4 Evolved VFX** | `Assets/_Game/Art/VFX/Magic/necromancy/vfx_<spell_id>_strip.png` | `192x64` (`3x 64x64`) | `16` | `(0.5, 0.5)` | `DeathWaveVfxPresenter` cataclysm burst |
