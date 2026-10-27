# Shadow Reaper (`shadow_reaper`) — Complete Art & Animation Prompt Specification

> **Asset Category:** Playable Boss Overlord (`01_Characters`)
> **Catalog ID:** `CharacterCatalog.ShadowReaperId` (`"shadow_reaper"`)
> **Class Title:** *Executioner of the Umbral Void*
> **Combat Profile:** HP: 90 | Armor: 4 | MoveSpeed: 5.5 | DashSpeed: 16.0 (0.18s, 1.8s CD) | **Resource:** Shadow Energy (Max 100, +8/s regen, +14 per kill)
> **Starting Weapon:** Umbral Scythe (`wpn_scy_reaper_scythe` — sweeping crescent execution, Range 2.6, Damage 20.0)
> **Signature Ability:** Phantom Execution (`shadow_reaper_active_01`) & Umbral Nova (Radius 2.8, Damage 42.0)

---

## 1. Base Character Design & Master Visual Prompt

### 1.1 Color Palette & Pixel Rules
| Swatch Role | Hex Code | RGB Float (`CharacterCatalog`) | Usage |
| :--- | :--- | :--- | :--- |
| **Outer Rim Outline** | `#110E1B` | `(0.067, 0.055, 0.106)` | Mandatory 1px dark silhouette border around all opaque pixels |
| **Primary Class Color** | `#402666` | Matches `CharacterCatalog` RGB | Primary robes/armor/mantle body mass (60% of sprite area) |
| **Secondary Energy / Glow** | `#B366FF` | High-value emissive | Eyes, weapon core, spell casting hands, signature aura (20% of sprite area) |
| **Trim / Bone / Metal** | `#0D0A14` | Neutral contrast | Crown, pauldrons, weapon shaft, structural trim (15% of sprite area) |
| **Deep Shadow** | `#07050A` | recessed folds | Interior cloak folds, under-chin shadow, lower hem (5% of sprite area) |

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle Reference)
```text
Masterpiece 2D top-down 3/4 view pixel art character sprite of Shadow Reaper (Executioner of the Umbral Void), playable final boss in a dark fantasy survival roguelite.
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, zero motion blur, 1px solid dark outline (#110E1B).
Silhouette & Design: Cowled wraith-assassin hunched forward in a jagged asymmetrical cloak (`#402666`), face Pitch-Black (`#0D0A14`) save for a single diagonal violet eye-slit (`#B366FF`), trailing smoke ribbons from shoulders, wielding a massive curved Umbral Crescent Scythe larger than its body.
Color Palette: strictly constrained to 5-6 ramps centered on Primary #402666, Emissive #B366FF, Trim #0D0A14, and Shadow #07050A.
Lighting: zenithal top-left light source; head, crown, shoulders, and weapon tip receive brightest value ramp; lower body drops into #07050A shadow.
Foot Anchor: character feet/lower robe hem centered at X=16, Y=3 from bottom edge (Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step Character Sprite Sheet Prompts (`char_shadow_reaper_sheet.png`)

To ensure **100% visual consistency** across all states, generate or assemble a **12-Frame Master Character Strip (`384x32 px` total, 12 frames of `32x32 px`)** or **2 rows of 6 frames (`192x64 px`)** using Frame 1 as the locked style/character reference.

### Row 1: Core Locomotion & Combat Strip (`192x32 px` — Required by `PIXEL_ART_STYLE_GUIDE.md`)

- **Frame 01 (`X: 0..31, Y: 0..31`) — `Idle_01` (Base Stance):**
  > `Same 32x32 pixel art character Shadow Reaper from Base Prompt. Neutral commanding boss stance, facing South-East (3/4 top-down), weapon held at ready on right side (X:22..29), left hand glowing with 2px #B366FF energy ember, silhouette fits within 26x28 px centered box.`
- **Frame 02 (`X: 32..63, Y: 0..31`) — `Walk_01` (Left Stride / Hover Up):**
  > `Consistent 32x32 pixel art of Shadow Reaper. Low-profile phantom skitter: leans 30° into movement direction, cloak shreds into 3 trailing shadow afterimages (`#402666`). Torso shifted 1px up (Y+1), left shoulder forward 1px, weapon trails 1px back, mantle/cloak flares 2px to the left.`
- **Frame 03 (`X: 64..95, Y: 0..31`) — `Walk_02` (Right Stride / Hover Down):**
  > `Consistent 32x32 pixel art of Shadow Reaper. Opposite phase of walk cycle: torso settles 1px down, right weapon shoulder surges forward 1px, lower cloak ripples right, 2px #B366FF particle detaches from hem.`
- **Frame 04 (`X: 96..127, Y: 0..31`) — `Attack_Cast_01` (Primary Strike / Spell Release):**
  > `Consistent 32x32 pixel art of Shadow Reaper. 270° spinning crescent scythe reap; blade edge leaves a neon-violet dimensional rift tear (`48x48 px`). Weapon and casting hand thrust forward toward right edge (X:24..31), eyes and weapon core flare pure white #FFFFFF with #B366FF 2px halo, dynamic forward lean.`
- **Frame 05 (`X: 128..159, Y: 0..31`) — `Hit_React_01` (Damaged Recoil):**
  > `Consistent 32x32 pixel art of Shadow Reaper. Knocked backward 2px to the left, torso arched back, arms flung wide defensively, 40% of primary #402666 pixels shifted toward high-contrast white/red hit flash (#FF5555 / #FFFFFF), 3 sharp impact sparks around chest.`
- **Frame 06 (`X: 160..191, Y: 0..31`) — `Death_01` (Defeat Collapse / Dissolve):**
  > `Consistent 32x32 pixel art of Shadow Reaper. Weapon dropped horizontally at Y:2..5, crown/helm cracked on ground, body collapsing into a swirling vortex of #402666 ash and ascending #B366FF soul embers.`

### Row 2: Extended Boss Action States (`192x32 px` — Optional Polish Strip)

- **Frame 07 — `Idle_02` (Breathing / Aura Pulse):** Chest/shoulders rise 1px, `#B366FF` eye/core glow expands by 1px.
- **Frame 08 — `Dash_Phase` (High-Speed Evasion):** Compressed horizontal silhouette tilted 25° forward, trailing 3 semi-transparent `#402666` motion lines.
- **Frame 09 — `Signature_Windup` (Channeling Ultimate):** Both arms raised overhead, levitating 3px off floor, surrounded by converging `#B366FF` runes.
- **Frame 10 — `Signature_Release` (Nova Eruption):** Vanishes into a 1px floor shadow puddle, teleports through targets leaving crossed violet X-slash marks (`32x32 px`).
- **Frame 11 — `Summon_Command` (Raising Minions / Totems):** Left hand extended palm-down over a glowing ground sigil (`#B366FF`).
- **Frame 12 — `Death_02` (Final Remnant):** Collapsed pile of `#0D0A14` armor/robes with a single flickering `#B366FF` ember at center.

---

## 3. Basic Attack, Starting Weapon & Signature VFX Prompts

### 3.1 Basic Attack Projectile / Swing Strip (`vfx_shadow_reaper_basic_attack.png` — `48x16 px`, 3 frames of `16x16`)
```text
3-frame horizontal pixel art sprite strip (48x16 px total, three 16x16 frames), transparent background, 1px dark rim (#110E1B) on opaque cores, FilterMode.Point.
Subject: Shadow Reaper basic attack effect (Umbral Scythe (`wpn_scy_reaper_scythe` — sweeping crescent execution, Range 2.6, Damage 20.0)).
- Frame 1 (0..15): Muzzle/hand cast flash — 10x10 px star-burst in #FFFFFF core and #B366FF outer spikes.
- Frame 2 (16..31): Active traveling projectile or melee slash arc — 14x12 px directional crescent/bolt facing Right (+X), #FFFFFF hot core, #B366FF mid-body, #402666 trailing tail.
- Frame 3 (32..47): Impact shatter — 15x15 px radial burst of 6 pixel shards in #B366FF and #402666.
```

### 3.2 Signature Nova / Ultimate Eruption (`vfx_shadow_reaper_signature_nova.png` — `256x64 px`, 4 frames of `64x64`)
```text
4-frame horizontal pixel art AoE explosion strip (256x64 px total, four 64x64 frames), top-down perspective, transparent background, crisp pixel art.
Subject: Phantom Execution (`shadow_reaper_active_01`) & Umbral Nova (Radius 2.8, Damage 42.0) (Vanishes into a 1px floor shadow puddle, teleports through targets leaving crossed violet X-slash marks (`32x32 px`).).
- Frame 1 (64x64): Ground telegraph ring (radius 28px) inscribed with glowing #B366FF runes.
- Frame 2 (64x64): Initial eruption — dense central pillar/wave of #FFFFFF and #B366FF expanding to radius 18px.
- Frame 3 (64x64): Peak shockwave — outer jagged ring at radius 30px in #402666 and #B366FF, hollow center with ground cracks.
- Frame 4 (64x64): Dissipating embers and smoke rings fading into #07050A particles.
```

---

## 4. Complete Class Skill Tree Assets (56 Abilities from `AbilityRegistry.cs`)

Below are all **56 class abilities** registered for **Shadow Reaper** across its 4 skill tree branches, with exact icon and VFX generation prompts.

| Ability ID | Name | Branch & Tier | Effect / Targeting | Cost | Cooldown | Base Dmg | Radius |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `shadow_reaper_active_01` | **Crescent Reap** | Branch A — Scythe Mastery (T1) | AbilityEffectType.MeleeArc / AimDirection | Cost: 3 | CD: 3.0s | Dmg: 42 | Rad: 3.2 |
| `shadow_reaper_active_02` | **Soul Sever** | Branch A — Scythe Mastery (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 2 | CD: 3.5s | Dmg: 48 | Rad: 2.8 |
| `shadow_reaper_active_03` | **Grim Spin** | Branch A — Scythe Mastery (T2) | AbilityEffectType.RadialAoE / Self | Cost: 3 | CD: 4.5s | Dmg: 52 | Rad: 4.2 |
| `shadow_reaper_active_04` | **Phantom Slice** | Branch A — Scythe Mastery (T2) | AbilityEffectType.Projectile | AbilityEffectType.MeleeArc / AimDirection | Cost: 2 | CD: 4.0s | Dmg: 46 | Rad: 1.5 |
| `shadow_reaper_active_05` | **Executioner's Hook** | Branch A — Scythe Mastery (T2) | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl / AimDirection | Cost: 3 | CD: 5.0s | Dmg: 50 | Rad: 2.5 |
| `shadow_reaper_active_06` | **Twin Crescent** | Branch A — Scythe Mastery (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc / AimDirection | Cost: 3 | CD: 5.5s | Dmg: 60 | Rad: 3.5 |
| `shadow_reaper_active_07` | **Gravewheel** | Branch A — Scythe Mastery (T3) | AbilityEffectType.Projectile | AbilityEffectType.Hazard / AimDirection | Cost: 3 | CD: 6.0s | Dmg: 64 | Rad: 3.0 |
| `shadow_reaper_active_08` | **Silent Decapitation** | Branch A — Scythe Mastery (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / NearestInvader | Cost: 3 | CD: 6.5s | Dmg: 72 | Rad: 3.2 |
| `shadow_reaper_active_09` | **Harvesting Dance** | Branch A — Scythe Mastery (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc / Self | Cost: 4 | CD: 7.5s | Dmg: 84 | Rad: 4.8 |
| `shadow_reaper_active_10` | **Reaper's Guillotine** | Branch A — Scythe Mastery (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 5 | CD: 9.5s | Dmg: 105 | Rad: 5.5 |
| `shadow_reaper_active_11` | **Shadowstep** | Branch B — Shadow Movement (T1) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE / AimDirection | Cost: 2 | CD: 3.5s | Dmg: 38 | Rad: 3.5 |
| `shadow_reaper_active_12` | **Nightfall Dash** | Branch B — Shadow Movement (T1) | AbilityEffectType.Dash | AbilityEffectType.Hazard / AimDirection | Cost: 2 | CD: 4.0s | Dmg: 40 | Rad: 3.0 |
| `shadow_reaper_active_13` | **Phantom Crossing** | Branch B — Shadow Movement (T2) | AbilityEffectType.Dash | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / AimDirection | Cost: 2 | CD: 4.5s | Dmg: 44 | Rad: 3.5 |
| `shadow_reaper_active_14` | **Shadow Exchange** | Branch B — Shadow Movement (T2) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE / AimDirection | Cost: 3 | CD: 5.0s | Dmg: 48 | Rad: 3.2 |
| `shadow_reaper_active_15` | **Midnight Leap** | Branch B — Shadow Movement (T2) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 3 | CD: 5.5s | Dmg: 54 | Rad: 4.0 |
| `shadow_reaper_active_16` | **Veil Slide** | Branch B — Shadow Movement (T3) | AbilityEffectType.Dash | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 3 | CD: 5.0s | Dmg: 56 | Rad: 3.5 |
| `shadow_reaper_active_17` | **Black Corridor** | Branch B — Shadow Movement (T3) | AbilityEffectType.Hazard | AbilityEffectType.RadialAoE / AimDirection | Cost: 3 | CD: 6.0s | Dmg: 62 | Rad: 3.5 |
| `shadow_reaper_active_18` | **Silent Pursuit** | Branch B — Shadow Movement (T3) | AbilityEffectType.Dash | AbilityEffectType.MeleeArc / NearestInvader | Cost: 3 | CD: 6.5s | Dmg: 70 | Rad: 3.0 |
| `shadow_reaper_active_19` | **Spectral Blink** | Branch B — Shadow Movement (T4) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE / AimDirection | Cost: 4 | CD: 7.0s | Dmg: 78 | Rad: 3.8 |
| `shadow_reaper_active_20` | **Shadow Horizon** | Branch B — Shadow Movement (T4) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / AreaCursorOrAim | Cost: 5 | CD: 9.0s | Dmg: 96 | Rad: 5.5 |
| `shadow_reaper_active_21` | **Whispering Shade** | Branch C — Haunting & Shadow Clones (T1) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AreaCursorOrAim | Cost: 2 | CD: 5.0s | Dmg: 45 | Rad: 3.5 |
| `shadow_reaper_active_22` | **Phantom Double** | Branch C — Haunting & Shadow Clones (T1) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / AreaCursorOrAim | Cost: 3 | CD: 6.0s | Dmg: 52 | Rad: 4.0 |
| `shadow_reaper_active_23` | **Haunting Presence** | Branch C — Haunting & Shadow Clones (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 2 | CD: 5.5s | Dmg: 48 | Rad: 4.5 |
| `shadow_reaper_active_24` | **Shadow Trap** | Branch C — Haunting & Shadow Clones (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 3 | CD: 6.0s | Dmg: 60 | Rad: 3.8 |
| `shadow_reaper_active_25` | **Wraith Ambush** | Branch C — Haunting & Shadow Clones (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / NearestInvader | Cost: 3 | CD: 6.5s | Dmg: 66 | Rad: 3.2 |
| `shadow_reaper_active_26` | **Nightblade Echo** | Branch C — Haunting & Shadow Clones (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / AimDirection | Cost: 3 | CD: 5.5s | Dmg: 68 | Rad: 4.0 |
| `shadow_reaper_active_27` | **Unseen Hunter** | Branch C — Haunting & Shadow Clones (T3) | AbilityEffectType.Shield / Self | Cost: 3 | CD: 7.0s | Dmg: 55 | Rad: 4.0 |
| `shadow_reaper_active_28` | **Umbral Pursuer** | Branch C — Haunting & Shadow Clones (T3) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / NearestInvader | Cost: 4 | CD: 7.5s | Dmg: 72 | Rad: 3.8 |
| `shadow_reaper_active_29` | **Phantom Circle** | Branch C — Haunting & Shadow Clones (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Summon / AreaCursorOrAim | Cost: 4 | CD: 8.0s | Dmg: 84 | Rad: 5.0 |
| `shadow_reaper_active_30` | **Council of Shadows** | Branch C — Haunting & Shadow Clones (T4) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 5 | CD: 9.5s | Dmg: 98 | Rad: 6.0 |
| `shadow_reaper_active_31` | **Soul Mark** | Branch D — Soul Harvesting (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / NearestInvader | Cost: 2 | CD: 4.5s | Dmg: 45 | Rad: 3.5 |
| `shadow_reaper_active_32` | **Essence Rend** | Branch D — Soul Harvesting (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.Heal / AimDirection | Cost: 2 | CD: 5.0s | Dmg: 52 | Rad: 3.8 |
| `shadow_reaper_active_33` | **Death Whisper** | Branch D — Soul Harvesting (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 3 | CD: 5.5s | Dmg: 58 | Rad: 4.5 |
| `shadow_reaper_active_34` | **Last Breath** | Branch D — Soul Harvesting (T2) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / AimDirection | Cost: 3 | CD: 6.0s | Dmg: 66 | Rad: 3.5 |
| `shadow_reaper_active_35` | **Reaper's Contract** | Branch D — Soul Harvesting (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / NearestInvader | Cost: 3 | CD: 6.5s | Dmg: 62 | Rad: 4.0 |
| `shadow_reaper_active_36` | **Spirit Chains** | Branch D — Soul Harvesting (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / NearestInvader | Cost: 3 | CD: 6.0s | Dmg: 68 | Rad: 3.8 |
| `shadow_reaper_active_37` | **Echoing Demise** | Branch D — Soul Harvesting (T3) | AbilityEffectType.RadialAoE / Self | Cost: 3 | CD: 6.5s | Dmg: 72 | Rad: 4.8 |
| `shadow_reaper_active_38` | **Soul Detonation** | Branch D — Soul Harvesting (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 4 | CD: 7.5s | Dmg: 80 | Rad: 4.5 |
| `shadow_reaper_active_39` | **Marked for Oblivion** | Branch D — Soul Harvesting (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 4 | CD: 8.0s | Dmg: 86 | Rad: 5.2 |
| `shadow_reaper_active_40` | **Harvest Eclipse** | Branch D — Soul Harvesting (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 5 | CD: 9.0s | Dmg: 96 | Rad: 5.8 |
| `shadow_reaper_active_41` | **Death's Embrace** | Branch E — Forbidden Reaper Arts (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 2 | CD: 5.0s | Dmg: 48 | Rad: 4.5 |
| `shadow_reaper_active_42` | **Scythe of Silence** | Branch E — Forbidden Reaper Arts (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 3 | CD: 5.5s | Dmg: 56 | Rad: 4.0 |
| `shadow_reaper_active_43` | **Shadow Hourglass** | Branch E — Forbidden Reaper Arts (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 3 | CD: 6.0s | Dmg: 60 | Rad: 5.0 |
| `shadow_reaper_active_44` | **Nightfall Domain** | Branch E — Forbidden Reaper Arts (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / AreaCursorOrAim | Cost: 3 | CD: 6.5s | Dmg: 65 | Rad: 4.8 |
| `shadow_reaper_active_45` | **Phantom Reversal** | Branch E — Forbidden Reaper Arts (T2) | AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Shield / AimDirection | Cost: 3 | CD: 6.0s | Dmg: 70 | Rad: 3.8 |
| `shadow_reaper_active_46` | **Reaper's Judgment** | Branch E — Forbidden Reaper Arts (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / NearestInvader | Cost: 4 | CD: 7.0s | Dmg: 78 | Rad: 4.2 |
| `shadow_reaper_active_47` | **Endless Footsteps** | Branch E — Forbidden Reaper Arts (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Dash / AreaCursorOrAim | Cost: 4 | CD: 7.5s | Dmg: 82 | Rad: 5.0 |
| `shadow_reaper_active_48` | **Spectral Overdrive** | Branch E — Forbidden Reaper Arts (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 4 | CD: 8.0s | Dmg: 88 | Rad: 5.2 |
| `shadow_reaper_active_49` | **Crown of Oblivion** | Branch E — Forbidden Reaper Arts (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.Transform / Self | Cost: 5 | CD: 9.0s | Dmg: 96 | Rad: 5.5 |
| `shadow_reaper_active_50` | **Final Silence** | Branch E — Forbidden Reaper Arts (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 5 | CD: 10.0s | Dmg: 110 | Rad: 6.5 |
| `shadow_reaper_active_shadowstep` | **Shadowstep** | Branch B — Shadow Movement (T1) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 2 | CD: 4s | Dmg: 40 | Rad: 3.5 |
| `shadow_reaper_ultimate_50` | **Dance of Ten Thousand Cuts** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Dash / AimDirection | Cost: 5 | CD: 20s | Dmg: 110 | Rad: 6.5 |
| `shadow_reaper_ultimate_100` | **Night Without Footsteps** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 5 | CD: 25s | Dmg: 135 | Rad: 7.0 |
| `shadow_reaper_ultimate_150` | **Umbral Labyrinth** | Milestone Ultimates (T5) | AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 5 | CD: 30s | Dmg: 160 | Rad: 7.5 |
| `shadow_reaper_ultimate_200` | **Harvest of Names** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 6 | CD: 35s | Dmg: 190 | Rad: 8.0 |
| `shadow_reaper_ultimate_250` | **Eclipse Reaper** | Milestone Ultimates (T5) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 6 | CD: 40s | Dmg: 240 | Rad: 9.0 |

### Step-by-Step Prompts for Every Shadow Reaper Skill
#### Skill 01: Crescent Reap (`shadow_reaper_active_01` — Branch A — Scythe Mastery, Tier 1)
- **Gameplay Context:** Sweep a wide spectral scythe arc in front of the Reaper, cleaving through invaders and dealing bonus damage to wounded targets. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc`, Radius: `3.2`, Duration: `0.25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crescent Reap", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting sweep a wide spectral scythe arc in front of the reaper, cleaving through invaders and dealing bonus damage to wounded targets., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crescent Reap" (AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 02: Soul Sever (`shadow_reaper_active_02` — Branch A — Scythe Mastery, Tier 1)
- **Gameplay Context:** Execute a heavy vertical scythe strike that severs the soul of a single target, dealing massive armor-piercing damage. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `2.8`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Soul Sever", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting execute a heavy vertical scythe strike that severs the soul of a single target, dealing massive armor-piercing damage., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Soul Sever" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 03: Grim Spin (`shadow_reaper_active_03` — Branch A — Scythe Mastery, Tier 2)
- **Gameplay Context:** Spin the scythe in a full 360-degree whirlwind of spectral steel, shredding all surrounding invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE`, Radius: `4.2`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Grim Spin", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting spin the scythe in a full 360-degree whirlwind of spectral steel, shredding all surrounding invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Grim Spin" (AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 04: Phantom Slice (`shadow_reaper_active_04` — Branch A — Scythe Mastery, Tier 2)
- **Gameplay Context:** Launch a flying crescent projectile of dark energy that pierces through a line of advancing invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.MeleeArc`, Radius: `1.5`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Phantom Slice", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting launch a flying crescent projectile of dark energy that pierces through a line of advancing invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Phantom Slice" (AbilityEffectType.Projectile | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 05: Executioner's Hook (`shadow_reaper_active_05` — Branch A — Scythe Mastery, Tier 2)
- **Gameplay Context:** Hurl a hooked spectral blade that snags an enemy, dragging them into scythe range and stunning them. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.CrowdControl`, Radius: `2.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Executioner's Hook", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting hurl a hooked spectral blade that snags an enemy, dragging them into scythe range and stunning them., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Executioner's Hook" (AbilityEffectType.Projectile | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 06: Twin Crescent (`shadow_reaper_active_06` — Branch A — Scythe Mastery, Tier 3)
- **Gameplay Context:** Release dual overlapping scythe shockwaves in an X-pattern that cleaves enemies caught in the intersection. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc`, Radius: `3.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Twin Crescent", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting release dual overlapping scythe shockwaves in an x-pattern that cleaves enemies caught in the intersection., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Twin Crescent" (AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 07: Gravewheel (`shadow_reaper_active_07` — Branch A — Scythe Mastery, Tier 3)
- **Gameplay Context:** Hurl a spinning scythe like a wheel of doom that travels outward, carving through enemies before returning to the Reaper. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.Hazard`, Radius: `3.0`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Gravewheel", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting hurl a spinning scythe like a wheel of doom that travels outward, carving through enemies before returning to the reaper., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Gravewheel" (AbilityEffectType.Projectile | AbilityEffectType.Hazard), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 08: Silent Decapitation (`shadow_reaper_active_08` — Branch A — Scythe Mastery, Tier 3)
- **Gameplay Context:** A ruthless finishing slice from the shadows that inflicts catastrophic damage against wounded foes. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `3.2`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Silent Decapitation", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting a ruthless finishing slice from the shadows that inflicts catastrophic damage against wounded foes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Silent Decapitation" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (NearestInvader) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 09: Harvesting Dance (`shadow_reaper_active_09` — Branch A — Scythe Mastery, Tier 4)
- **Gameplay Context:** Perform a rapid four-stage sequence of scythe pirouettes, slashing all nearby enemies and boosting movement speed. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc`, Radius: `4.8`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Harvesting Dance", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting perform a rapid four-stage sequence of scythe pirouettes, slashing all nearby enemies and boosting movement speed., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Harvesting Dance" (AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 10: Reaper's Guillotine (`shadow_reaper_active_10` — Branch A — Scythe Mastery, Tier 4)
- **Gameplay Context:** Summon an immense spectral guillotine scythe that crashes down from the heavens, splitting the earth and crushing all invaders. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.5`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Reaper's Guillotine", Shadow Reaper ability (Branch A — Scythe Mastery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting summon an immense spectral guillotine scythe that crashes down from the heavens, splitting the earth and crushing all invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Reaper's Guillotine" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 11: Shadowstep (`shadow_reaper_active_11` — Branch B — Shadow Movement, Tier 1)
- **Gameplay Context:** Teleport a short distance through the shadow realm, reappearing with a devastating 360-degree shadow whirlwind. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Shadowstep", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting teleport a short distance through the shadow realm, reappearing with a devastating 360-degree shadow whirlwind., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Shadowstep" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 12: Nightfall Dash (`shadow_reaper_active_12` — Branch B — Shadow Movement, Tier 1)
- **Gameplay Context:** Dash rapidly through enemy ranks, leaving a lingering trail of dark shadow flames that burns invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.Hazard`, Radius: `3.0`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Nightfall Dash", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting dash rapidly through enemy ranks, leaving a lingering trail of dark shadow flames that burns invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Nightfall Dash" (AbilityEffectType.Dash | AbilityEffectType.Hazard), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 13: Phantom Crossing (`shadow_reaper_active_13` — Branch B — Shadow Movement, Tier 2)
- **Gameplay Context:** Step across the veil of reality, becoming intangible while moving and releasing a phase burst upon rematerializing. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Phantom Crossing", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting step across the veil of reality, becoming intangible while moving and releasing a phase burst upon rematerializing., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Phantom Crossing" (AbilityEffectType.Dash | AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 14: Shadow Exchange (`shadow_reaper_active_14` — Branch B — Shadow Movement, Tier 2)
- **Gameplay Context:** Project a shadow shade forward and instantly swap places with it, triggering explosive spatial shockwaves at both points. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE`, Radius: `3.2`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Shadow Exchange", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting project a shadow shade forward and instantly swap places with it, triggering explosive spatial shockwaves at both points., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Shadow Exchange" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 15: Midnight Leap (`shadow_reaper_active_15` — Branch B — Shadow Movement, Tier 2)
- **Gameplay Context:** Vault high into the air on wings of midnight, crashing down at the target location in an earth-shattering impact. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Midnight Leap", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting vault high into the air on wings of midnight, crashing down at the target location in an earth-shattering impact., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Midnight Leap" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 16: Veil Slide (`shadow_reaper_active_16` — Branch B — Shadow Movement, Tier 3)
- **Gameplay Context:** Slide fluidly beneath oncoming attacks while leaving behind an explosive shadow decoy to draw enemy aggression. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Veil Slide", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting slide fluidly beneath oncoming attacks while leaving behind an explosive shadow decoy to draw enemy aggression., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Veil Slide" (AbilityEffectType.Dash | AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 17: Black Corridor (`shadow_reaper_active_17` — Branch B — Shadow Movement, Tier 3)
- **Gameplay Context:** Rip open a dimensional corridor of pure umbral energy that damages invaders and propels the Reaper forward. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Hazard | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Black Corridor", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting rip open a dimensional corridor of pure umbral energy that damages invaders and propels the reaper forward., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Black Corridor" (AbilityEffectType.Hazard | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 18: Silent Pursuit (`shadow_reaper_active_18` — Branch B — Shadow Movement, Tier 3)
- **Gameplay Context:** Instantly lock onto and blink behind the nearest invader, delivering a guaranteed critical backstab. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.Dash | AbilityEffectType.MeleeArc`, Radius: `3.0`, Duration: `0.25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Silent Pursuit", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting instantly lock onto and blink behind the nearest invader, delivering a guaranteed critical backstab., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Silent Pursuit" (AbilityEffectType.Dash | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (NearestInvader) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 19: Spectral Blink (`shadow_reaper_active_19` — Branch B — Shadow Movement, Tier 4)
- **Gameplay Context:** Perform a rapid three-stage sequence of micro-teleports, weaving through combat while slicing in every direction. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE`, Radius: `3.8`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Spectral Blink", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting perform a rapid three-stage sequence of micro-teleports, weaving through combat while slicing in every direction., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Spectral Blink" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 20: Shadow Horizon (`shadow_reaper_active_20` — Branch B — Shadow Movement, Tier 4)
- **Gameplay Context:** Disperse across the horizon, sequentially teleporting between and executing multiple invaders in a cascade of darkness. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `5.5`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Shadow Horizon", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting disperse across the horizon, sequentially teleporting between and executing multiple invaders in a cascade of darkness., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Shadow Horizon" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 21: Whispering Shade (`shadow_reaper_active_21` — Branch C — Haunting & Shadow Clones, Tier 1)
- **Gameplay Context:** Summon a phantasmal shade at target area that repeatedly mimics scythe sweeps, cleaving nearby enemies. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Whispering Shade", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting summon a phantasmal shade at target area that repeatedly mimics scythe sweeps, cleaving nearby enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Whispering Shade" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 22: Phantom Double (`shadow_reaper_active_22` — Branch C — Haunting & Shadow Clones, Tier 1)
- **Gameplay Context:** Conjure an agile combat clone that draws invader aggression and retaliates with whirling shadow blades. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.0`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Phantom Double", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting conjure an agile combat clone that draws invader aggression and retaliates with whirling shadow blades., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Phantom Double" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 23: Haunting Presence (`shadow_reaper_active_23` — Branch C — Haunting & Shadow Clones, Tier 2)
- **Gameplay Context:** Radiate an overpowering spectral terror, slowing and inflicting fear upon all nearby invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Haunting Presence", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting radiate an overpowering spectral terror, slowing and inflicting fear upon all nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Haunting Presence" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 24: Shadow Trap (`shadow_reaper_active_24` — Branch C — Haunting & Shadow Clones, Tier 2)
- **Gameplay Context:** Inscribe a dormant spectral rune on the ground that detonates in a flurry of scythe claws when crossed. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.8`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Shadow Trap", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting inscribe a dormant spectral rune on the ground that detonates in a flurry of scythe claws when crossed., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Shadow Trap" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 25: Wraith Ambush (`shadow_reaper_active_25` — Branch C — Haunting & Shadow Clones, Tier 2)
- **Gameplay Context:** Summon a stealthy wraith behind the highest-threat enemy that executes a devastating ambush strike. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.2`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Wraith Ambush", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting summon a stealthy wraith behind the highest-threat enemy that executes a devastating ambush strike., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Wraith Ambush" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (NearestInvader) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 26: Nightblade Echo (`shadow_reaper_active_26` — Branch C — Haunting & Shadow Clones, Tier 3)
- **Gameplay Context:** Harness residual spectral vibrations to unleash a synchronized duplicate scythe cleave ahead. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `4.0`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Nightblade Echo", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting harness residual spectral vibrations to unleash a synchronized duplicate scythe cleave ahead., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Nightblade Echo" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 27: Unseen Hunter (`shadow_reaper_active_27` — Branch C — Haunting & Shadow Clones, Tier 3)
- **Gameplay Context:** Melt into the umbral realm, cloaking the Reaper in darkness while generating a protective shadow barrier. (Targeting: `Self`, Effect: `AbilityEffectType.Shield`, Radius: `4.0`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Unseen Hunter", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting melt into the umbral realm, cloaking the reaper in darkness while generating a protective shadow barrier., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Unseen Hunter" (AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 28: Umbral Pursuer (`shadow_reaper_active_28` — Branch C — Haunting & Shadow Clones, Tier 3)
- **Gameplay Context:** Manifest a persistent hound of gloom that tirelessly stalks and rends distant invaders. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `3.8`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Umbral Pursuer", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting manifest a persistent hound of gloom that tirelessly stalks and rends distant invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Umbral Pursuer" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (NearestInvader) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 29: Phantom Circle (`shadow_reaper_active_29` — Branch C — Haunting & Shadow Clones, Tier 4)
- **Gameplay Context:** Form an encircling ring of spectral mirages that converge inward, executing simultaneous scythe sweeps. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Summon`, Radius: `5.0`, Duration: `1.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Phantom Circle", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting form an encircling ring of spectral mirages that converge inward, executing simultaneous scythe sweeps., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Phantom Circle" (AbilityEffectType.RadialAoE | AbilityEffectType.Summon), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 30: Council of Shadows (`shadow_reaper_active_30` — Branch C — Haunting & Shadow Clones, Tier 4)
- **Gameplay Context:** Assemble the full ghostly council: summon a cohort of formidable spectral duplicates to dominate the battlefield. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `6.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Council of Shadows", Shadow Reaper ability (Branch C — Haunting & Shadow Clones), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting assemble the full ghostly council: summon a cohort of formidable spectral duplicates to dominate the battlefield., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Council of Shadows" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 31: Soul Mark (`shadow_reaper_active_31` — Branch D — Soul Harvesting, Tier 1)
- **Gameplay Context:** Afflict target with a death mark that renders them deeply vulnerable to lethal execution strikes. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Soul Mark", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting afflict target with a death mark that renders them deeply vulnerable to lethal execution strikes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Soul Mark" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (NearestInvader) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 32: Essence Rend (`shadow_reaper_active_32` — Branch D — Soul Harvesting, Tier 1)
- **Gameplay Context:** Tear a luminous spectral fragment from an enemy, healing the Reaper and fueling lethal energy. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Heal`, Radius: `3.8`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Essence Rend", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting tear a luminous spectral fragment from an enemy, healing the reaper and fueling lethal energy., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Essence Rend" (AbilityEffectType.MeleeArc | AbilityEffectType.Heal), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 33: Death Whisper (`shadow_reaper_active_33` — Branch D — Soul Harvesting, Tier 2)
- **Gameplay Context:** Channel a terrifying harbinger murmur that deals devastating damage to enemies below half health. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Death Whisper", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting channel a terrifying harbinger murmur that deals devastating damage to enemies below half health., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Death Whisper" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 34: Last Breath (`shadow_reaper_active_34` — Branch D — Soul Harvesting, Tier 2)
- **Gameplay Context:** Deliver a merciless execution thrust that crushes weakened foes with critical force. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Last Breath", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting deliver a merciless execution thrust that crushes weakened foes with critical force., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Last Breath" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 35: Reaper's Contract (`shadow_reaper_active_35` — Branch D — Soul Harvesting, Tier 2)
- **Gameplay Context:** Inscribe a solemn soul contract upon high-value targets, amplifying incoming damage and granting barrier on death. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Reaper's Contract", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting inscribe a solemn soul contract upon high-value targets, amplifying incoming damage and granting barrier on death., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Reaper's Contract" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (NearestInvader) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 36: Spirit Chains (`shadow_reaper_active_36` — Branch D — Soul Harvesting, Tier 3)
- **Gameplay Context:** Bind an enemy to an umbral anchor, restricting movement and draining vital essence. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.8`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Spirit Chains", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting bind an enemy to an umbral anchor, restricting movement and draining vital essence., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Spirit Chains" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (NearestInvader) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 37: Echoing Demise (`shadow_reaper_active_37` — Branch D — Soul Harvesting, Tier 3)
- **Gameplay Context:** Shatter harvested souls to release a concussive spectral explosion that lacerates surrounding enemies. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE`, Radius: `4.8`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Echoing Demise", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting shatter harvested souls to release a concussive spectral explosion that lacerates surrounding enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Echoing Demise" (AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 38: Soul Detonation (`shadow_reaper_active_38` — Branch D — Soul Harvesting, Tier 3)
- **Gameplay Context:** Detonate harvested soul essence in a violent cataclysm, obliterating invaders in a massive shockwave. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Soul Detonation", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting detonate harvested soul essence in a violent cataclysm, obliterating invaders in a massive shockwave., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Soul Detonation" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 39: Marked for Oblivion (`shadow_reaper_active_39` — Branch D — Soul Harvesting, Tier 4)
- **Gameplay Context:** Mark multiple foes simultaneously for eternal oblivion, massively increasing their susceptibility to executions. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.2`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Marked for Oblivion", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting mark multiple foes simultaneously for eternal oblivion, massively increasing their susceptibility to executions., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Marked for Oblivion" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 40: Harvest Eclipse (`shadow_reaper_active_40` — Branch D — Soul Harvesting, Tier 4)
- **Gameplay Context:** Unleash the full fury of harvested souls, triggering a cascading eclipse of chained scythe rifts. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.CrowdControl`, Radius: `5.8`, Duration: `1.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Harvest Eclipse", Shadow Reaper ability (Branch D — Soul Harvesting), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting unleash the full fury of harvested souls, triggering a cascading eclipse of chained scythe rifts., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Harvest Eclipse" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 41: Death's Embrace (`shadow_reaper_active_41` — Branch E — Forbidden Reaper Arts, Tier 1)
- **Gameplay Context:** Radiate an icy aura of mortality, debilitating and weakening all nearby enemies. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Death's Embrace", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting radiate an icy aura of mortality, debilitating and weakening all nearby enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Death's Embrace" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 42: Scythe of Silence (`shadow_reaper_active_42` — Branch E — Forbidden Reaper Arts, Tier 1)
- **Gameplay Context:** Sweep a massive nullifying scythe arc ahead, interrupting enemy actions and inflicting heavy damage. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Scythe of Silence", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting sweep a massive nullifying scythe arc ahead, interrupting enemy actions and inflicting heavy damage., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Scythe of Silence" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 43: Shadow Hourglass (`shadow_reaper_active_43` — Branch E — Forbidden Reaper Arts, Tier 2)
- **Gameplay Context:** Invert temporal shadow flow, trapping surrounding invaders in extreme temporal dilation. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Shadow Hourglass", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting invert temporal shadow flow, trapping surrounding invaders in extreme temporal dilation., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Shadow Hourglass" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 44: Nightfall Domain (`shadow_reaper_active_44` — Branch E — Forbidden Reaper Arts, Tier 2)
- **Gameplay Context:** Conceal the battlefield in an expansive nightfall zone that accelerates the Reaper and grants barrier. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.8`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Nightfall Domain", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting conceal the battlefield in an expansive nightfall zone that accelerates the reaper and grants barrier., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Nightfall Domain" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 45: Phantom Reversal (`shadow_reaper_active_45` — Branch E — Forbidden Reaper Arts, Tier 2)
- **Gameplay Context:** Deliver a devastating forward scythe slash and instantly snap backwards into shadow to dodge retaliation. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Shield`, Radius: `3.8`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Phantom Reversal", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting deliver a devastating forward scythe slash and instantly snap backwards into shadow to dodge retaliation., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Phantom Reversal" (AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 46: Reaper's Judgment (`shadow_reaper_active_46` — Branch E — Forbidden Reaper Arts, Tier 3)
- **Gameplay Context:** Pass solemn supernatural judgment upon marked invaders, executing weakened foes in rapid succession. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.2`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Reaper's Judgment", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting pass solemn supernatural judgment upon marked invaders, executing weakened foes in rapid succession., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Reaper's Judgment" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (NearestInvader) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 47: Endless Footsteps (`shadow_reaper_active_47` — Branch E — Forbidden Reaper Arts, Tier 3)
- **Gameplay Context:** Unleash a breathless chain of spectral afterimages crisscrossing the combat zone with lethal precision. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Dash`, Radius: `5.0`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Endless Footsteps", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting unleash a breathless chain of spectral afterimages crisscrossing the combat zone with lethal precision., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Endless Footsteps" (AbilityEffectType.RadialAoE | AbilityEffectType.Dash), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 48: Spectral Overdrive (`shadow_reaper_active_48` — Branch E — Forbidden Reaper Arts, Tier 3)
- **Gameplay Context:** Ignite forbidden spiritual power to supercharge combat speed and unleash continuous slicing shockwaves. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `5.2`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Spectral Overdrive", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting ignite forbidden spiritual power to supercharge combat speed and unleash continuous slicing shockwaves., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Spectral Overdrive" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 49: Crown of Oblivion (`shadow_reaper_active_49` — Branch E — Forbidden Reaper Arts, Tier 4)
- **Gameplay Context:** Don the forbidden dark crown: summon colossal execution blades and fortify the Reaper with impregnable barrier. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.Transform`, Radius: `5.5`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crown of Oblivion", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting don the forbidden dark crown: summon colossal execution blades and fortify the reaper with impregnable barrier., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crown of Oblivion" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.Transform), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 50: Final Silence (`shadow_reaper_active_50` — Branch E — Forbidden Reaper Arts, Tier 4)
- **Gameplay Context:** Unleash the ultimate forbidden art: an apocalyptic, screen-wide scythe execution bringing absolute silence to all enemies. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.CrowdControl`, Radius: `6.5`, Duration: `1.2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Final Silence", Shadow Reaper ability (Branch E — Forbidden Reaper Arts), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting unleash the ultimate forbidden art: an apocalyptic, screen-wide scythe execution bringing absolute silence to all enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Final Silence" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AreaCursorOrAim) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 51: Shadowstep (`shadow_reaper_active_shadowstep` — Branch B — Shadow Movement, Tier 1)
- **Gameplay Context:** Teleport behind invaders and execute a devastating 360-degree shadow whirlwind. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Shadowstep", Shadow Reaper ability (Branch B — Shadow Movement), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting teleport behind invaders and execute a devastating 360-degree shadow whirlwind., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Shadowstep" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 52: Dance of Ten Thousand Cuts (`shadow_reaper_ultimate_50` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Accelerate the Reaper into a catastrophic flurry of rapid, lethal scythe strikes across the arena. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Dash`, Radius: `6.5`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Dance of Ten Thousand Cuts", Shadow Reaper ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting accelerate the reaper into a catastrophic flurry of rapid, lethal scythe strikes across the arena., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Dance of Ten Thousand Cuts" (AbilityEffectType.RadialAoE | AbilityEffectType.Dash), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (AimDirection) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 53: Night Without Footsteps (`shadow_reaper_ultimate_100` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Enter an untargetable spectral state while multiple afterimages assault invaders across the dungeon. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `7.0`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Night Without Footsteps", Shadow Reaper ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting enter an untargetable spectral state while multiple afterimages assault invaders across the dungeon., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Night Without Footsteps" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 54: Umbral Labyrinth (`shadow_reaper_ultimate_150` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Create a supernatural shadow maze that slows enemies by 60% and unleashes chaining scythe detonations. (Targeting: `Self`, Effect: `AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `7.5`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Umbral Labyrinth", Shadow Reaper ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting create a supernatural shadow maze that slows enemies by 60% and unleashes chaining scythe detonations., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Umbral Labyrinth" (AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 55: Harvest of Names (`shadow_reaper_ultimate_200` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Mark every enemy on the battlefield and execute weakened targets in a devastating chain reaction. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `8.0`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Harvest of Names", Shadow Reaper ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting mark every enemy on the battlefield and execute weakened targets in a devastating chain reaction., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Harvest of Names" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 56: Eclipse Reaper (`shadow_reaper_ultimate_250` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Transform into a colossal spectral executioner, unleashing cataclysmic scythe waves, shadow clones, and total oblivion. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `9.0`, Duration: `8.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Eclipse Reaper", Shadow Reaper ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #402666 and glowing #B366FF highlights, depicting transform into a colossal spectral executioner, unleashing cataclysmic scythe waves, shadow clones, and total oblivion., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Eclipse Reaper" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Shadow Reaper hands in #B366FF, frame 2: active delivery (Self) with #402666 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`

---

## 5. Unity Import, Slicing & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Filter / Format | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Character Sheet** | `Assets/_Game/Art/Characters/shadow_reaper/char_shadow_reaper_sheet.png` | `192x32` (or `384x32`) | `32` | `(0.5, 0.1)` | `Point`, `RGBA32` | `LichLordVisualPresenter._bodyRenderer` (`PlaceholderSprites.Circle` tinted `#402666`) |
| **Portrait Icon** | `Assets/_Game/Art/UI/Portraits/portrait_shadow_reaper.png` | `48x48` | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `CharacterSelectPresenter` class card color block |
| **Basic Attack VFX** | `Assets/_Game/Art/VFX/Characters/vfx_shadow_reaper_basic.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `ProjectileView` / `AttackTelegraphPresenter` (`PlaceholderSprites.Circle`) |
| **Signature VFX** | `Assets/_Game/Art/VFX/Characters/vfx_shadow_reaper_signature.png` | `256x64` (`4x 64x64`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `DeathWaveVfxPresenter` expanding ring placeholder |
| **Skill Icons (56x)** | `Assets/_Game/Art/UI/Skills/shadow_reaper/icon_<ability_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `SkillTreeScreenPresenter` & `HudPresenter` slot squares |
