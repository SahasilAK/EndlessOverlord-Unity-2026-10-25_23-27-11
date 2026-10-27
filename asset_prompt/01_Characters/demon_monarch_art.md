# Demon Monarch (`demon_monarch`) — Complete Art & Animation Prompt Specification

> **Asset Category:** Playable Boss Overlord (`01_Characters`)
> **Catalog ID:** `CharacterCatalog.DemonMonarchId` (`"demon_monarch"`)
> **Class Title:** *Sovereign of the Infernal Citadel*
> **Combat Profile:** HP: 145 | Armor: 14 | MoveSpeed: 4.2 | DashSpeed: 13.0 (0.22s, 2.8s CD) | **Resource:** Hellfire Fury (Max 100, +3/s regen, +15 per kill)
> **Starting Weapon:** Infernal Greatsword (`wpn_2h_crimson_cleaver` — heavy melee cleave, Range 2.8, Damage 24.0)
> **Signature Ability:** Hellfire Cleave (`demon_monarch_active_01`) & Molten Earthshaker (Radius 3.6, Damage 48.0)

---

## 1. Base Character Design & Master Visual Prompt

### 1.1 Color Palette & Pixel Rules
| Swatch Role | Hex Code | RGB Float (`CharacterCatalog`) | Usage |
| :--- | :--- | :--- | :--- |
| **Outer Rim Outline** | `#110E1B` | `(0.067, 0.055, 0.106)` | Mandatory 1px dark silhouette border around all opaque pixels |
| **Primary Class Color** | `#EB331F` | Matches `CharacterCatalog` RGB | Primary robes/armor/mantle body mass (60% of sprite area) |
| **Secondary Energy / Glow** | `#FF9E00` | High-value emissive | Eyes, weapon core, spell casting hands, signature aura (20% of sprite area) |
| **Trim / Bone / Metal** | `#2B1D1D` | Neutral contrast | Crown, pauldrons, weapon shaft, structural trim (15% of sprite area) |
| **Deep Shadow** | `#1A0B08` | recessed folds | Interior cloak folds, under-chin shadow, lower hem (5% of sprite area) |

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle Reference)
```text
Masterpiece 2D top-down 3/4 view pixel art character sprite of Demon Monarch (Sovereign of the Infernal Citadel), playable final boss in a dark fantasy survival roguelite.
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, zero motion blur, 1px solid dark outline (#110E1B).
Silhouette & Design: Broad-shouldered demonic warlord with swept-back obsidian horns (`#2B1D1D`), glowing magma chest fissures (`#FF9E00`), heavy blackened iron pauldrons, crimson-scarlet war mantle (`#EB331F`), and a colossal two-handed Infernal Greatsword resting across the right shoulder dripping molten embers.
Color Palette: strictly constrained to 5-6 ramps centered on Primary #EB331F, Emissive #FF9E00, Trim #2B1D1D, and Shadow #1A0B08.
Lighting: zenithal top-left light source; head, crown, shoulders, and weapon tip receive brightest value ramp; lower body drops into #1A0B08 shadow.
Foot Anchor: character feet/lower robe hem centered at X=16, Y=3 from bottom edge (Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step Character Sprite Sheet Prompts (`char_demon_monarch_sheet.png`)

To ensure **100% visual consistency** across all states, generate or assemble a **12-Frame Master Character Strip (`384x32 px` total, 12 frames of `32x32 px`)** or **2 rows of 6 frames (`192x64 px`)** using Frame 1 as the locked style/character reference.

### Row 1: Core Locomotion & Combat Strip (`192x32 px` — Required by `PIXEL_ART_STYLE_GUIDE.md`)

- **Frame 01 (`X: 0..31, Y: 0..31`) — `Idle_01` (Base Stance):**
  > `Same 32x32 pixel art character Demon Monarch from Base Prompt. Neutral commanding boss stance, facing South-East (3/4 top-down), weapon held at ready on right side (X:22..29), left hand glowing with 2px #FF9E00 energy ember, silhouette fits within 26x28 px centered box.`
- **Frame 02 (`X: 32..63, Y: 0..31`) — `Walk_01` (Left Stride / Hover Up):**
  > `Consistent 32x32 pixel art of Demon Monarch. Heavy volcanic stride: horned head tilts menacingly, greatsword drags sparks (`#FF9E00`), hooves leave 2px glowing cinder footprints. Torso shifted 1px up (Y+1), left shoulder forward 1px, weapon trails 1px back, mantle/cloak flares 2px to the left.`
- **Frame 03 (`X: 64..95, Y: 0..31`) — `Walk_02` (Right Stride / Hover Down):**
  > `Consistent 32x32 pixel art of Demon Monarch. Opposite phase of walk cycle: torso settles 1px down, right weapon shoulder surges forward 1px, lower cloak ripples right, 2px #FF9E00 particle detaches from hem.`
- **Frame 04 (`X: 96..127, Y: 0..31`) — `Attack_Cast_01` (Primary Strike / Spell Release):**
  > `Consistent 32x32 pixel art of Demon Monarch. 180° wide horizontal greatsword cleave from right to left, leaving a 4-frame molten magma arc (`48x48 px`). Weapon and casting hand thrust forward toward right edge (X:24..31), eyes and weapon core flare pure white #FFFFFF with #FF9E00 2px halo, dynamic forward lean.`
- **Frame 05 (`X: 128..159, Y: 0..31`) — `Hit_React_01` (Damaged Recoil):**
  > `Consistent 32x32 pixel art of Demon Monarch. Knocked backward 2px to the left, torso arched back, arms flung wide defensively, 40% of primary #EB331F pixels shifted toward high-contrast white/red hit flash (#FF5555 / #FFFFFF), 3 sharp impact sparks around chest.`
- **Frame 06 (`X: 160..191, Y: 0..31`) — `Death_01` (Defeat Collapse / Dissolve):**
  > `Consistent 32x32 pixel art of Demon Monarch. Weapon dropped horizontally at Y:2..5, crown/helm cracked on ground, body collapsing into a swirling vortex of #EB331F ash and ascending #FF9E00 soul embers.`

### Row 2: Extended Boss Action States (`192x32 px` — Optional Polish Strip)

- **Frame 07 — `Idle_02` (Breathing / Aura Pulse):** Chest/shoulders rise 1px, `#FF9E00` eye/core glow expands by 1px.
- **Frame 08 — `Dash_Phase` (High-Speed Evasion):** Compressed horizontal silhouette tilted 25° forward, trailing 3 semi-transparent `#EB331F` motion lines.
- **Frame 09 — `Signature_Windup` (Channeling Ultimate):** Both arms raised overhead, levitating 3px off floor, surrounded by converging `#FF9E00` runes.
- **Frame 10 — `Signature_Release` (Nova Eruption):** Leaps 4px upward and plunges Infernal Greatsword into the stone floor, erupting a cross-shaped lava fissure (`64x64 px`).
- **Frame 11 — `Summon_Command` (Raising Minions / Totems):** Left hand extended palm-down over a glowing ground sigil (`#FF9E00`).
- **Frame 12 — `Death_02` (Final Remnant):** Collapsed pile of `#2B1D1D` armor/robes with a single flickering `#FF9E00` ember at center.

---

## 3. Basic Attack, Starting Weapon & Signature VFX Prompts

### 3.1 Basic Attack Projectile / Swing Strip (`vfx_demon_monarch_basic_attack.png` — `48x16 px`, 3 frames of `16x16`)
```text
3-frame horizontal pixel art sprite strip (48x16 px total, three 16x16 frames), transparent background, 1px dark rim (#110E1B) on opaque cores, FilterMode.Point.
Subject: Demon Monarch basic attack effect (Infernal Greatsword (`wpn_2h_crimson_cleaver` — heavy melee cleave, Range 2.8, Damage 24.0)).
- Frame 1 (0..15): Muzzle/hand cast flash — 10x10 px star-burst in #FFFFFF core and #FF9E00 outer spikes.
- Frame 2 (16..31): Active traveling projectile or melee slash arc — 14x12 px directional crescent/bolt facing Right (+X), #FFFFFF hot core, #FF9E00 mid-body, #EB331F trailing tail.
- Frame 3 (32..47): Impact shatter — 15x15 px radial burst of 6 pixel shards in #FF9E00 and #EB331F.
```

### 3.2 Signature Nova / Ultimate Eruption (`vfx_demon_monarch_signature_nova.png` — `256x64 px`, 4 frames of `64x64`)
```text
4-frame horizontal pixel art AoE explosion strip (256x64 px total, four 64x64 frames), top-down perspective, transparent background, crisp pixel art.
Subject: Hellfire Cleave (`demon_monarch_active_01`) & Molten Earthshaker (Radius 3.6, Damage 48.0) (Leaps 4px upward and plunges Infernal Greatsword into the stone floor, erupting a cross-shaped lava fissure (`64x64 px`).).
- Frame 1 (64x64): Ground telegraph ring (radius 28px) inscribed with glowing #FF9E00 runes.
- Frame 2 (64x64): Initial eruption — dense central pillar/wave of #FFFFFF and #FF9E00 expanding to radius 18px.
- Frame 3 (64x64): Peak shockwave — outer jagged ring at radius 30px in #EB331F and #FF9E00, hollow center with ground cracks.
- Frame 4 (64x64): Dissipating embers and smoke rings fading into #1A0B08 particles.
```

---

## 4. Complete Class Skill Tree Assets (55 Abilities from `AbilityRegistry.cs`)

Below are all **55 class abilities** registered for **Demon Monarch** across its 4 skill tree branches, with exact icon and VFX generation prompts.

| Ability ID | Name | Branch & Tier | Effect / Targeting | Cost | Cooldown | Base Dmg | Radius |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `demon_monarch_active_01` | **Infernal Cleave** | Branch A — Infernal Swordsmanship (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.Hazard / AimDirection | Cost: 15 | CD: 2.2s | Dmg: 32 | Rad: 3.5 |
| `demon_monarch_active_02` | **Hellbrand Thrust** | Branch A — Infernal Swordsmanship (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.Projectile / AimDirection | Cost: 18 | CD: 3.0s | Dmg: 38 | Rad: 2.0 |
| `demon_monarch_active_03` | **Flaming Arc** | Branch A — Infernal Swordsmanship (T2) | AbilityEffectType.Projectile | AbilityEffectType.MeleeArc / AimDirection | Cost: 25 | CD: 4.0s | Dmg: 40 | Rad: 3.0 |
| `demon_monarch_active_04` | **Ember Uppercut** | Branch A — Infernal Swordsmanship (T2) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 22 | CD: 3.5s | Dmg: 42 | Rad: 3.0 |
| `demon_monarch_active_05` | **Blazing Riposte** | Branch A — Infernal Swordsmanship (T2) | AbilityEffectType.Shield | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / Self | Cost: 25 | CD: 5.0s | Dmg: 45 | Rad: 3.5 |
| `demon_monarch_active_06` | **Molten Lunge** | Branch A — Infernal Swordsmanship (T3) | AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Hazard / AimDirection | Cost: 30 | CD: 5.5s | Dmg: 48 | Rad: 3.0 |
| `demon_monarch_active_07` | **Burning Whirl** | Branch A — Infernal Swordsmanship (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 35 | CD: 6.0s | Dmg: 52 | Rad: 4.5 |
| `demon_monarch_active_08` | **Volcanic Slash** | Branch A — Infernal Swordsmanship (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.Hazard / AimDirection | Cost: 35 | CD: 6.5s | Dmg: 56 | Rad: 3.5 |
| `demon_monarch_active_09` | **Sundering Edge** | Branch A — Infernal Swordsmanship (T4) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 40 | CD: 7.5s | Dmg: 65 | Rad: 3.5 |
| `demon_monarch_active_10` | **Royal Execution** | Branch A — Infernal Swordsmanship (T4) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / AimDirection | Cost: 45 | CD: 8.5s | Dmg: 80 | Rad: 4.0 |
| `demon_monarch_active_11` | **Hellfire Wave** | Branch B — Hellfire Magic (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 20 | CD: 4.0s | Dmg: 34 | Rad: 4.0 |
| `demon_monarch_active_12` | **Magma Fissure** | Branch B — Hellfire Magic (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 25 | CD: 5.0s | Dmg: 38 | Rad: 3.5 |
| `demon_monarch_active_13` | **Cinder Rain** | Branch B — Hellfire Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AreaCursorOrAim | Cost: 30 | CD: 5.5s | Dmg: 44 | Rad: 4.5 |
| `demon_monarch_active_14` | **Inferno Spiral** | Branch B — Hellfire Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 30 | CD: 6.0s | Dmg: 46 | Rad: 4.0 |
| `demon_monarch_active_15` | **Pyre Ring** | Branch B — Hellfire Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 35 | CD: 6.5s | Dmg: 50 | Rad: 5.0 |
| `demon_monarch_active_16` | **Flame Vortex** | Branch B — Hellfire Magic (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard / AimDirection | Cost: 40 | CD: 7.0s | Dmg: 54 | Rad: 4.5 |
| `demon_monarch_active_17` | **Molten Chains** | Branch B — Hellfire Magic (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 35 | CD: 6.5s | Dmg: 52 | Rad: 3.5 |
| `demon_monarch_active_18` | **Ember Lance** | Branch B — Hellfire Magic (T3) | AbilityEffectType.Projectile | AbilityEffectType.MeleeArc / AimDirection | Cost: 35 | CD: 5.5s | Dmg: 60 | Rad: 2.0 |
| `demon_monarch_active_19` | **Searing Brand** | Branch B — Hellfire Magic (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 40 | CD: 7.5s | Dmg: 65 | Rad: 4.0 |
| `demon_monarch_active_20` | **Cataclysm Fire** | Branch B — Hellfire Magic (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 50 | CD: 9.5s | Dmg: 85 | Rad: 6.0 |
| `demon_monarch_active_21` | **Throne of Embers** | Branch C — Demonic Powers (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield / Self | Cost: 20 | CD: 4.5s | Dmg: 32 | Rad: 3.5 |
| `demon_monarch_active_22` | **Hellstep** | Branch C — Demonic Powers (T1) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE / AimDirection | Cost: 25 | CD: 4.0s | Dmg: 30 | Rad: 3.0 |
| `demon_monarch_active_23` | **Ash Doppelganger** | Branch C — Demonic Powers (T2) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 30 | CD: 6.0s | Dmg: 38 | Rad: 3.0 |
| `demon_monarch_active_24` | **Bloodforged Blade** | Branch C — Demonic Powers (T2) | AbilityEffectType.MeleeArc | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / AimDirection | Cost: 20 | CD: 5.0s | Dmg: 48 | Rad: 3.5 |
| `demon_monarch_active_25` | **Furnace Heart** | Branch C — Demonic Powers (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 35 | CD: 6.5s | Dmg: 42 | Rad: 4.5 |
| `demon_monarch_active_26` | **Terror Roar** | Branch C — Demonic Powers (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 30 | CD: 6.0s | Dmg: 40 | Rad: 5.0 |
| `demon_monarch_active_27` | **Infernal Grasp** | Branch C — Demonic Powers (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 35 | CD: 7.0s | Dmg: 54 | Rad: 4.0 |
| `demon_monarch_active_28` | **Blasphemous Banner** | Branch C — Demonic Powers (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield / AreaCursorOrAim | Cost: 40 | CD: 8.0s | Dmg: 50 | Rad: 4.5 |
| `demon_monarch_active_29` | **Wrath Magnet** | Branch C — Demonic Powers (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 45 | CD: 7.5s | Dmg: 62 | Rad: 6.0 |
| `demon_monarch_active_30` | **Demonic Standard** | Branch C — Demonic Powers (T4) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 55 | CD: 10.0s | Dmg: 85 | Rad: 5.5 |
| `demon_monarch_active_31` | **Brutal Smash** | Branch D — Devastating Physical Attacks (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 15 | CD: 2.2s | Dmg: 34 | Rad: 3.5 |
| `demon_monarch_active_32` | **Titanic Swing** | Branch D — Devastating Physical Attacks (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 20 | CD: 3.0s | Dmg: 40 | Rad: 4.0 |
| `demon_monarch_active_33` | **Rupture Stomp** | Branch D — Devastating Physical Attacks (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / Self | Cost: 25 | CD: 4.5s | Dmg: 42 | Rad: 4.0 |
| `demon_monarch_active_34` | **Colossal Crush** | Branch D — Devastating Physical Attacks (T2) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 30 | CD: 4.8s | Dmg: 52 | Rad: 3.5 |
| `demon_monarch_active_35` | **Iron Sledge** | Branch D — Devastating Physical Attacks (T2) | AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Shield / AimDirection | Cost: 30 | CD: 5.5s | Dmg: 46 | Rad: 3.0 |
| `demon_monarch_active_36` | **Devastating Quake** | Branch D — Devastating Physical Attacks (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 35 | CD: 6.5s | Dmg: 58 | Rad: 5.0 |
| `demon_monarch_active_37` | **Bone Pulverizer** | Branch D — Devastating Physical Attacks (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.Hazard / AimDirection | Cost: 35 | CD: 6.0s | Dmg: 64 | Rad: 3.5 |
| `demon_monarch_active_38` | **Brutal Cleaver** | Branch D — Devastating Physical Attacks (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / AimDirection | Cost: 40 | CD: 7.0s | Dmg: 66 | Rad: 4.0 |
| `demon_monarch_active_39` | **Sunder the Earth** | Branch D — Devastating Physical Attacks (T4) | AbilityEffectType.MeleeArc | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / AimDirection | Cost: 45 | CD: 8.0s | Dmg: 78 | Rad: 4.5 |
| `demon_monarch_active_40` | **Wrathful Guillotine** | Branch D — Devastating Physical Attacks (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 55 | CD: 10.0s | Dmg: 92 | Rad: 6.0 |
| `demon_monarch_active_41` | **Crown of Flames** | Branch E — Infernal Ascension (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 20 | CD: 4.0s | Dmg: 36 | Rad: 3.8 |
| `demon_monarch_active_42` | **Wing Buffet** | Branch E — Infernal Ascension (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 25 | CD: 3.5s | Dmg: 42 | Rad: 4.2 |
| `demon_monarch_active_43` | **Demonic Ascendance** | Branch E — Infernal Ascension (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 30 | CD: 5.0s | Dmg: 48 | Rad: 4.5 |
| `demon_monarch_active_44` | **Molten Halo** | Branch E — Infernal Ascension (T2) | AbilityEffectType.Projectile | AbilityEffectType.RadialAoE / AimDirection | Cost: 30 | CD: 5.5s | Dmg: 54 | Rad: 3.5 |
| `demon_monarch_active_45` | **Sovereign Glare** | Branch E — Infernal Ascension (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 35 | CD: 6.0s | Dmg: 56 | Rad: 3.5 |
| `demon_monarch_active_46` | **Overlord's Wrath** | Branch E — Infernal Ascension (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / AimDirection | Cost: 40 | CD: 7.0s | Dmg: 68 | Rad: 4.5 |
| `demon_monarch_active_47` | **Hellfire Nova** | Branch E — Infernal Ascension (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 45 | CD: 7.5s | Dmg: 72 | Rad: 5.5 |
| `demon_monarch_active_48` | **Pillar of Damnation** | Branch E — Infernal Ascension (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AreaCursorOrAim | Cost: 45 | CD: 8.0s | Dmg: 76 | Rad: 4.5 |
| `demon_monarch_active_49` | **Crown of the Abyss** | Branch E — Infernal Ascension (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 55 | CD: 9.0s | Dmg: 84 | Rad: 5.5 |
| `demon_monarch_active_50` | **Infernal Ascension** | Branch E — Infernal Ascension (T4) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield / Self | Cost: 65 | CD: 12.0s | Dmg: 100 | Rad: 7.0 |
| `demon_monarch_ultimate_50` | **Hellblade Judgment** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc / AimDirection | Cost: 70 | CD: 25s | Dmg: 80 | Rad: 7 |
| `demon_monarch_ultimate_100` | **Demon King's Awakening** | Milestone Ultimates (T5) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform / Self | Cost: 65 | CD: 30s | Dmg: 90 | Rad: 6.5 |
| `demon_monarch_ultimate_150` | **Rain of a Thousand Blades** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AreaCursorOrAim | Cost: 75 | CD: 35s | Dmg: 100 | Rad: 8.0 |
| `demon_monarch_ultimate_200` | **Infernal Apocalypse** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / Self | Cost: 80 | CD: 40s | Dmg: 120 | Rad: 8.5 |
| `demon_monarch_ultimate_250` | **Throne of Eternal Flame** | Milestone Ultimates (T5) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.Hazard / Self | Cost: 90 | CD: 45s | Dmg: 150 | Rad: 9.0 |

### Step-by-Step Prompts for Every Demon Monarch Skill
#### Skill 01: Infernal Cleave (`demon_monarch_active_01` — Branch A — Infernal Swordsmanship, Tier 1)
- **Gameplay Context:** Unleash a devastating wide flaming sword strike, sundering enemies and inflicting burn. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `0.25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Infernal Cleave", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting unleash a devastating wide flaming sword strike, sundering enemies and inflicting burn., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Infernal Cleave" (AbilityEffectType.MeleeArc | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 02: Hellbrand Thrust (`demon_monarch_active_02` — Branch A — Infernal Swordsmanship, Tier 1)
- **Gameplay Context:** Deliver a lightning-fast piercing forward stab that impales invaders and ignites them with hellfire. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Projectile`, Radius: `2.0`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hellbrand Thrust", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting deliver a lightning-fast piercing forward stab that impales invaders and ignites them with hellfire., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hellbrand Thrust" (AbilityEffectType.MeleeArc | AbilityEffectType.Projectile), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 03: Flaming Arc (`demon_monarch_active_03` — Branch A — Infernal Swordsmanship, Tier 2)
- **Gameplay Context:** Sweep your blade in a wide arc, launching a ranged crescent-shaped wave of demonic fire forward. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.MeleeArc`, Radius: `3.0`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Flaming Arc", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting sweep your blade in a wide arc, launching a ranged crescent-shaped wave of demonic fire forward., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Flaming Arc" (AbilityEffectType.Projectile | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 04: Ember Uppercut (`demon_monarch_active_04` — Branch A — Infernal Swordsmanship, Tier 2)
- **Gameplay Context:** Execute a rising fiery blade strike that launches invaders into the air and leaves a trail of burning embers. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `3.0`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ember Uppercut", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting execute a rising fiery blade strike that launches invaders into the air and leaves a trail of burning embers., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ember Uppercut" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 05: Blazing Riposte (`demon_monarch_active_05` — Branch A — Infernal Swordsmanship, Tier 2)
- **Gameplay Context:** Assume an infernal parry stance, gaining a barrier and violently counterattacking with an eruptive fiery slash. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blazing Riposte", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting assume an infernal parry stance, gaining a barrier and violently counterattacking with an eruptive fiery slash., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blazing Riposte" (AbilityEffectType.Shield | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 06: Molten Lunge (`demon_monarch_active_06` — Branch A — Infernal Swordsmanship, Tier 3)
- **Gameplay Context:** Dash forward in a streak of molten flame, thrusting your hellblade through all invaders in your path. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Hazard`, Radius: `3.0`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Molten Lunge", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting dash forward in a streak of molten flame, thrusting your hellblade through all invaders in your path., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Molten Lunge" (AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 07: Burning Whirl (`demon_monarch_active_07` — Branch A — Infernal Swordsmanship, Tier 3)
- **Gameplay Context:** Spin rapidly with your blazing sword, slicing all surrounding enemies in a fiery vortex. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Burning Whirl", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting spin rapidly with your blazing sword, slicing all surrounding enemies in a fiery vortex., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Burning Whirl" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 08: Volcanic Slash (`demon_monarch_active_08` — Branch A — Infernal Swordsmanship, Tier 3)
- **Gameplay Context:** Slam your greatsword into the dungeon stone, carving a trench of erupting molten lava. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Volcanic Slash", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting slam your greatsword into the dungeon stone, carving a trench of erupting molten lava., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Volcanic Slash" (AbilityEffectType.MeleeArc | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 09: Sundering Edge (`demon_monarch_active_09` — Branch A — Infernal Swordsmanship, Tier 4)
- **Gameplay Context:** A titanic downward greatsword cleave that sunders physical defenses and shatters enemy armor. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Sundering Edge", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting a titanic downward greatsword cleave that sunders physical defenses and shatters enemy armor., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Sundering Edge" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 10: Royal Execution (`demon_monarch_active_10` — Branch A — Infernal Swordsmanship, Tier 4)
- **Gameplay Context:** Execute a sovereign lethal finishing strike against weakened invaders, detonating them into infernal shrapnel. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `4.0`, Duration: `0.45s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Royal Execution", Demon Monarch ability (Branch A — Infernal Swordsmanship), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting execute a sovereign lethal finishing strike against weakened invaders, detonating them into infernal shrapnel., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Royal Execution" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 11: Hellfire Wave (`demon_monarch_active_11` — Branch B — Hellfire Magic, Tier 1)
- **Gameplay Context:** Summon a rolling tsunami of demonic hellfire that sweeps forward across the battlefield. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `4.0`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hellfire Wave", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting summon a rolling tsunami of demonic hellfire that sweeps forward across the battlefield., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hellfire Wave" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 12: Magma Fissure (`demon_monarch_active_12` — Branch B — Hellfire Magic, Tier 1)
- **Gameplay Context:** Shatter the dungeon stones with volcanic force, opening molten fissures that spew liquid fire. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Magma Fissure", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting shatter the dungeon stones with volcanic force, opening molten fissures that spew liquid fire., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Magma Fissure" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 13: Cinder Rain (`demon_monarch_active_13` — Branch B — Hellfire Magic, Tier 2)
- **Gameplay Context:** Call down a blistering rain of burning cinders and brimstone shards over a designated area. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `4.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Cinder Rain", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting call down a blistering rain of burning cinders and brimstone shards over a designated area., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Cinder Rain" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AreaCursorOrAim) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 14: Inferno Spiral (`demon_monarch_active_14` — Branch B — Hellfire Magic, Tier 2)
- **Gameplay Context:** Ignite a vortex of spiraling firestorms revolving around your avatar, scorching all encroaching invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.0`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Inferno Spiral", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting ignite a vortex of spiraling firestorms revolving around your avatar, scorching all encroaching invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Inferno Spiral" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 15: Pyre Ring (`demon_monarch_active_15` — Branch B — Hellfire Magic, Tier 2)
- **Gameplay Context:** Detonate an expanding circular blast of demonic fire that knocks away enemies and detonates explosive cinders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Pyre Ring", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting detonate an expanding circular blast of demonic fire that knocks away enemies and detonates explosive cinders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Pyre Ring" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 16: Flame Vortex (`demon_monarch_active_16` — Branch B — Hellfire Magic, Tier 3)
- **Gameplay Context:** Conjure a raging fiery cyclone that drags enemies inward while shredding them with molten heat. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard`, Radius: `4.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Flame Vortex", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting conjure a raging fiery cyclone that drags enemies inward while shredding them with molten heat., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Flame Vortex" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 17: Molten Chains (`demon_monarch_active_17` — Branch B — Hellfire Magic, Tier 3)
- **Gameplay Context:** Lash out with superheated chains of molten iron, restraining invaders and scorching their flesh. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Molten Chains", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting lash out with superheated chains of molten iron, restraining invaders and scorching their flesh., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Molten Chains" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 18: Ember Lance (`demon_monarch_active_18` — Branch B — Hellfire Magic, Tier 3)
- **Gameplay Context:** Hurl a hyper-dense lance of condensed hellfire that pierces through all enemies in its path. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.MeleeArc`, Radius: `2.0`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ember Lance", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting hurl a hyper-dense lance of condensed hellfire that pierces through all enemies in its path., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ember Lance" (AbilityEffectType.Projectile | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 19: Searing Brand (`demon_monarch_active_19` — Branch B — Hellfire Magic, Tier 4)
- **Gameplay Context:** Mark targeted invaders with an abyssal brand that erupts into violent firestorms upon sustained damage. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Searing Brand", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting mark targeted invaders with an abyssal brand that erupts into violent firestorms upon sustained damage., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Searing Brand" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 20: Cataclysm Fire (`demon_monarch_active_20` — Branch B — Hellfire Magic, Tier 4)
- **Gameplay Context:** Channel the full destructive potential of the Underworld, igniting an enormous delayed infernal explosion. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `6.0`, Duration: `4.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Cataclysm Fire", Demon Monarch ability (Branch B — Hellfire Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting channel the full destructive potential of the underworld, igniting an enormous delayed infernal explosion., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Cataclysm Fire" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AreaCursorOrAim) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 21: Throne of Embers (`demon_monarch_active_21` — Branch C — Demonic Powers, Tier 1)
- **Gameplay Context:** Ignite an empowering demonic floor zone beneath your feet, enhancing fury regeneration and immolating surrounding enemies. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield`, Radius: `3.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Throne of Embers", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting ignite an empowering demonic floor zone beneath your feet, enhancing fury regeneration and immolating surrounding enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Throne of Embers" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 22: Hellstep (`demon_monarch_active_22` — Branch C — Demonic Powers, Tier 1)
- **Gameplay Context:** Teleport swiftly across the battlefield, unleashing infernal explosions at departure and arrival. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE`, Radius: `3.0`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hellstep", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting teleport swiftly across the battlefield, unleashing infernal explosions at departure and arrival., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hellstep" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 23: Ash Doppelganger (`demon_monarch_active_23` — Branch C — Demonic Powers, Tier 2)
- **Gameplay Context:** Forge a fiery shadow doppelganger out of volcanic ash to hunt down invaders and cleave them with molten steel. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `3.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ash Doppelganger", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting forge a fiery shadow doppelganger out of volcanic ash to hunt down invaders and cleave them with molten steel., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ash Doppelganger" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 24: Bloodforged Blade (`demon_monarch_active_24` — Branch C — Demonic Powers, Tier 2)
- **Gameplay Context:** Sacrifice a portion of health to imbue your weapon with demonic thirst, dealing heavy cleave damage and granting life steal. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Bloodforged Blade", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting sacrifice a portion of health to imbue your weapon with demonic thirst, dealing heavy cleave damage and granting life steal., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Bloodforged Blade" (AbilityEffectType.MeleeArc | AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 25: Furnace Heart (`demon_monarch_active_25` — Branch C — Demonic Powers, Tier 2)
- **Gameplay Context:** Overcharge the demonic furnace within your chest, pulsing intense heat waves and reinforcing your demonic barrier. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Furnace Heart", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting overcharge the demonic furnace within your chest, pulsing intense heat waves and reinforcing your demonic barrier., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Furnace Heart" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 26: Terror Roar (`demon_monarch_active_26` — Branch C — Demonic Powers, Tier 3)
- **Gameplay Context:** Release a terrifying abyssal roar that repels encroaching invaders, inflicting heavy slow and demoralizing their ranks. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Terror Roar", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting release a terrifying abyssal roar that repels encroaching invaders, inflicting heavy slow and demoralizing their ranks., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Terror Roar" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 27: Infernal Grasp (`demon_monarch_active_27` — Branch C — Demonic Powers, Tier 3)
- **Gameplay Context:** Conjure a massive demonic claw that erupts from the stone floor, pulling enemies inward and crushing them in a fiery grip. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `1.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Infernal Grasp", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting conjure a massive demonic claw that erupts from the stone floor, pulling enemies inward and crushing them in a fiery grip., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Infernal Grasp" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 28: Blasphemous Banner (`demon_monarch_active_28` — Branch C — Demonic Powers, Tier 3)
- **Gameplay Context:** Plant a terrifying demonic war banner into the ground, radiating continuous brimstone burn and inspiring relentless fury. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield`, Radius: `4.5`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blasphemous Banner", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting plant a terrifying demonic war banner into the ground, radiating continuous brimstone burn and inspiring relentless fury., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blasphemous Banner" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AreaCursorOrAim) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 29: Wrath Magnet (`demon_monarch_active_29` — Branch C — Demonic Powers, Tier 4)
- **Gameplay Context:** Generate a gravitational vortex of pure demonic fury that pulls all nearby invaders inward towards your blade. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `6.0`, Duration: `1.2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Wrath Magnet", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting generate a gravitational vortex of pure demonic fury that pulls all nearby invaders inward towards your blade., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Wrath Magnet" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 30: Demonic Standard (`demon_monarch_active_30` — Branch C — Demonic Powers, Tier 4)
- **Gameplay Context:** Summon the monarch's elite demonic guard and unleash a cataclysmic radial firestorm across the battlefield. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `5.5`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Demonic Standard", Demon Monarch ability (Branch C — Demonic Powers), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting summon the monarch's elite demonic guard and unleash a cataclysmic radial firestorm across the battlefield., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Demonic Standard" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 31: Brutal Smash (`demon_monarch_active_31` — Branch D — Devastating Physical Attacks, Tier 1)
- **Gameplay Context:** Slam your titanic fist into the earth, sending a concussive shockwave through surrounding invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `0.2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Brutal Smash", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting slam your titanic fist into the earth, sending a concussive shockwave through surrounding invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Brutal Smash" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 32: Titanic Swing (`demon_monarch_active_32` — Branch D — Devastating Physical Attacks, Tier 1)
- **Gameplay Context:** Swing your heavy greatsword in a wide sweeping arc, sending shattered enemies flying backward. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Titanic Swing", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting swing your heavy greatsword in a wide sweeping arc, sending shattered enemies flying backward., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Titanic Swing" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 33: Rupture Stomp (`demon_monarch_active_33` — Branch D — Devastating Physical Attacks, Tier 2)
- **Gameplay Context:** Stomp violently on the dungeon floor, shattering stone tiles and knocking down encroaching foes. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rupture Stomp", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting stomp violently on the dungeon floor, shattering stone tiles and knocking down encroaching foes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rupture Stomp" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 34: Colossal Crush (`demon_monarch_active_34` — Branch D — Devastating Physical Attacks, Tier 2)
- **Gameplay Context:** Bring your clenched fists down in a crushing hammer strike that pulverizes invaders directly in front. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Colossal Crush", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting bring your clenched fists down in a crushing hammer strike that pulverizes invaders directly in front., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Colossal Crush" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 35: Iron Sledge (`demon_monarch_active_35` — Branch D — Devastating Physical Attacks, Tier 2)
- **Gameplay Context:** Drive your shoulder forward in a brutal battering-ram rush, plowing through the enemy line and absorbing impacts. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Shield`, Radius: `3.0`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Iron Sledge", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting drive your shoulder forward in a brutal battering-ram rush, plowing through the enemy line and absorbing impacts., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Iron Sledge" (AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 36: Devastating Quake (`demon_monarch_active_36` — Branch D — Devastating Physical Attacks, Tier 3)
- **Gameplay Context:** Leap and smash the arena with catastrophic force, unleashing a violent seismic shockwave across the room. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Devastating Quake", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting leap and smash the arena with catastrophic force, unleashing a violent seismic shockwave across the room., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Devastating Quake" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 37: Bone Pulverizer (`demon_monarch_active_37` — Branch D — Devastating Physical Attacks, Tier 3)
- **Gameplay Context:** Unleash a ferocious two-handed overhead chop that shatters bones and fractures the ground. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Bone Pulverizer", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting unleash a ferocious two-handed overhead chop that shatters bones and fractures the ground., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Bone Pulverizer" (AbilityEffectType.MeleeArc | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 38: Brutal Cleaver (`demon_monarch_active_38` — Branch D — Devastating Physical Attacks, Tier 3)
- **Gameplay Context:** Perform a rapid succession of brutal cleaves, tearing through frontline enemy ranks with relentless ferocity. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `4.0`, Duration: `0.45s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Brutal Cleaver", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting perform a rapid succession of brutal cleaves, tearing through frontline enemy ranks with relentless ferocity., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Brutal Cleaver" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 39: Sunder the Earth (`demon_monarch_active_39` — Branch D — Devastating Physical Attacks, Tier 4)
- **Gameplay Context:** Drive your greatsword into the floor with world-splitting force, tearing open a massive fracture. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Sunder the Earth", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting drive your greatsword into the floor with world-splitting force, tearing open a massive fracture., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Sunder the Earth" (AbilityEffectType.MeleeArc | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 40: Wrathful Guillotine (`demon_monarch_active_40` — Branch D — Devastating Physical Attacks, Tier 4)
- **Gameplay Context:** Leap high into the air and deliver the ultimate execution slam, cleaving the earth and crushing all in its radius. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `6.0`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Wrathful Guillotine", Demon Monarch ability (Branch D — Devastating Physical Attacks), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting leap high into the air and deliver the ultimate execution slam, cleaving the earth and crushing all in its radius., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Wrathful Guillotine" (AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AreaCursorOrAim) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 41: Crown of Flames (`demon_monarch_active_41` — Branch E — Infernal Ascension, Tier 1)
- **Gameplay Context:** Ignite a halo of crown-shaped demonic flames around your head, burning encroaching invaders and empowering nearby attacks. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `3.8`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crown of Flames", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting ignite a halo of crown-shaped demonic flames around your head, burning encroaching invaders and empowering nearby attacks., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crown of Flames" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 42: Wing Buffet (`demon_monarch_active_42` — Branch E — Infernal Ascension, Tier 1)
- **Gameplay Context:** Unfurl massive wings of molten hellfire, buffeting all invaders with blistering heat and knocking them backward. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.2`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Wing Buffet", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting unfurl massive wings of molten hellfire, buffeting all invaders with blistering heat and knocking them backward., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Wing Buffet" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 43: Demonic Ascendance (`demon_monarch_active_43` — Branch E — Infernal Ascension, Tier 2)
- **Gameplay Context:** Levitate above the stones and gather brimstone energy, blasting the ground with an expanding fiery ring. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Demonic Ascendance", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting levitate above the stones and gather brimstone energy, blasting the ground with an expanding fiery ring., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Demonic Ascendance" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 44: Molten Halo (`demon_monarch_active_44` — Branch E — Infernal Ascension, Tier 2)
- **Gameplay Context:** Hurl a spinning halo of incandescent magma that slices through multiple ranks of invaders before returning. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Molten Halo", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting hurl a spinning halo of incandescent magma that slices through multiple ranks of invaders before returning., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Molten Halo" (AbilityEffectType.Projectile | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 45: Sovereign Glare (`demon_monarch_active_45` — Branch E — Infernal Ascension, Tier 2)
- **Gameplay Context:** Fix a piercing demonic glare upon targeted invaders, crushing their will and burning them with abyssal eyebeams. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Sovereign Glare", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting fix a piercing demonic glare upon targeted invaders, crushing their will and burning them with abyssal eyebeams., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Sovereign Glare" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 46: Overlord's Wrath (`demon_monarch_active_46` — Branch E — Infernal Ascension, Tier 3)
- **Gameplay Context:** Channel pure sovereign fury into a wide forward devastation cone, obliterating encroaching waves. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `4.5`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Overlord's Wrath", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting channel pure sovereign fury into a wide forward devastation cone, obliterating encroaching waves., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Overlord's Wrath" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 47: Hellfire Nova (`demon_monarch_active_47` — Branch E — Infernal Ascension, Tier 3)
- **Gameplay Context:** Unleash a massive expanding supernova of pure hellfire centered on your avatar, consuming everything in reach. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.5`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hellfire Nova", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting unleash a massive expanding supernova of pure hellfire centered on your avatar, consuming everything in reach., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hellfire Nova" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 48: Pillar of Damnation (`demon_monarch_active_48` — Branch E — Infernal Ascension, Tier 3)
- **Gameplay Context:** Call down a gargantuan pillar of abyssal damnation at targeted coordinates, obliterating invaders upon impact. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `4.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Pillar of Damnation", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting call down a gargantuan pillar of abyssal damnation at targeted coordinates, obliterating invaders upon impact., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Pillar of Damnation" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AreaCursorOrAim) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 49: Crown of the Abyss (`demon_monarch_active_49` — Branch E — Infernal Ascension, Tier 4)
- **Gameplay Context:** Assume the mantle of true Underworld sovereignty, pulsing devastating abyssal shockwaves while gaining massive barrier. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `5.5`, Duration: `4.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crown of the Abyss", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting assume the mantle of true underworld sovereignty, pulsing devastating abyssal shockwaves while gaining massive barrier., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crown of the Abyss" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 50: Infernal Ascension (`demon_monarch_active_50` — Branch E — Infernal Ascension, Tier 4)
- **Gameplay Context:** Undergo complete apotheosis into the Demon God Emperor, summoning elite bodyguards and unleashing an all-consuming apocalypse. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield`, Radius: `7.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Infernal Ascension", Demon Monarch ability (Branch E — Infernal Ascension), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting undergo complete apotheosis into the demon god emperor, summoning elite bodyguards and unleashing an all-consuming apocalypse., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Infernal Ascension" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Shield), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 51: Hellblade Judgment (`demon_monarch_ultimate_50` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Summon a colossal flaming sword that crashes into the arena with apocalyptic devastation. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc`, Radius: `7`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hellblade Judgment", Demon Monarch ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting summon a colossal flaming sword that crashes into the arena with apocalyptic devastation., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hellblade Judgment" (AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AimDirection) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 52: Demon King's Awakening (`demon_monarch_ultimate_100` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Transform into a towering armored demon, gaining immense barrier and unleashing devastating fiery sword waves. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform`, Radius: `6.5`, Duration: `10s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Demon King's Awakening", Demon Monarch ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting transform into a towering armored demon, gaining immense barrier and unleashing devastating fiery sword waves., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Demon King's Awakening" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 53: Rain of a Thousand Blades (`demon_monarch_ultimate_150` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Unleash countless burning demonic swords from the sky, bombarding invaders across the entire arena. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `8.0`, Duration: `4.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rain of a Thousand Blades", Demon Monarch ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting unleash countless burning demonic swords from the sky, bombarding invaders across the entire arena., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rain of a Thousand Blades" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (AreaCursorOrAim) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 54: Infernal Apocalypse (`demon_monarch_ultimate_200` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Fracture the earth to trigger devastating volcanic eruptions across the arena, immolating all invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `8.5`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Infernal Apocalypse", Demon Monarch ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting fracture the earth to trigger devastating volcanic eruptions across the arena, immolating all invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Infernal Apocalypse" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 55: Throne of Eternal Flame (`demon_monarch_ultimate_250` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Assume the true Monarch form, combining supreme demonic swordsmanship, aerial meteor strikes, and infernal flames. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.Hazard`, Radius: `9.0`, Duration: `12s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Throne of Eternal Flame", Demon Monarch ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #EB331F and glowing #FF9E00 highlights, depicting assume the true monarch form, combining supreme demonic swordsmanship, aerial meteor strikes, and infernal flames., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Throne of Eternal Flame" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.Hazard), frame 1: arcane cast flash on Demon Monarch hands in #FF9E00, frame 2: active delivery (Self) with #EB331F core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`

---

## 5. Unity Import, Slicing & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Filter / Format | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Character Sheet** | `Assets/_Game/Art/Characters/demon_monarch/char_demon_monarch_sheet.png` | `192x32` (or `384x32`) | `32` | `(0.5, 0.1)` | `Point`, `RGBA32` | `LichLordVisualPresenter._bodyRenderer` (`PlaceholderSprites.Circle` tinted `#EB331F`) |
| **Portrait Icon** | `Assets/_Game/Art/UI/Portraits/portrait_demon_monarch.png` | `48x48` | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `CharacterSelectPresenter` class card color block |
| **Basic Attack VFX** | `Assets/_Game/Art/VFX/Characters/vfx_demon_monarch_basic.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `ProjectileView` / `AttackTelegraphPresenter` (`PlaceholderSprites.Circle`) |
| **Signature VFX** | `Assets/_Game/Art/VFX/Characters/vfx_demon_monarch_signature.png` | `256x64` (`4x 64x64`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `DeathWaveVfxPresenter` expanding ring placeholder |
| **Skill Icons (55x)** | `Assets/_Game/Art/UI/Skills/demon_monarch/icon_<ability_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `SkillTreeScreenPresenter` & `HudPresenter` slot squares |
