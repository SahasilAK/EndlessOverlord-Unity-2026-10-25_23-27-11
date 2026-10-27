# Cursed Golem (`cursed_golem`) — Complete Art & Animation Prompt Specification

> **Asset Category:** Playable Boss Overlord (`01_Characters`)
> **Catalog ID:** `CharacterCatalog.CursedGolemId` (`"cursed_golem"`)
> **Class Title:** *Runic Monolith of the Ancient Sanctuary*
> **Combat Profile:** HP: 185 | Armor: 22 | MoveSpeed: 3.6 | DashSpeed: 10.5 (0.25s, 3.4s CD) | **Resource:** Runic Core Charge (Max 100, +5/s regen, +10 per kill)
> **Starting Weapon:** Monolith Fist (`wpn_ham_titan_anvil_hammer` — tectonic shockwave punch, Range 2.4, Damage 26.0)
> **Signature Ability:** Seismic Slam (`cursed_golem_active_01`) & Runic Overload (Radius 4.2, Damage 52.0)

---

## 1. Base Character Design & Master Visual Prompt

### 1.1 Color Palette & Pixel Rules
| Swatch Role | Hex Code | RGB Float (`CharacterCatalog`) | Usage |
| :--- | :--- | :--- | :--- |
| **Outer Rim Outline** | `#110E1B` | `(0.067, 0.055, 0.106)` | Mandatory 1px dark silhouette border around all opaque pixels |
| **Primary Class Color** | `#4DB38C` | Matches `CharacterCatalog` RGB | Primary robes/armor/mantle body mass (60% of sprite area) |
| **Secondary Energy / Glow** | `#6EE7B7` | High-value emissive | Eyes, weapon core, spell casting hands, signature aura (20% of sprite area) |
| **Trim / Bone / Metal** | `#4A5559` | Neutral contrast | Crown, pauldrons, weapon shaft, structural trim (15% of sprite area) |
| **Deep Shadow** | `#1B2422` | recessed folds | Interior cloak folds, under-chin shadow, lower hem (5% of sprite area) |

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle Reference)
```text
Masterpiece 2D top-down 3/4 view pixel art character sprite of Cursed Golem (Runic Monolith of the Ancient Sanctuary), playable final boss in a dark fantasy survival roguelite.
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, zero motion blur, 1px solid dark outline (#110E1B).
Silhouette & Design: Hulking asymmetrical stone colossus (`#4A5559`) held together by levitating magnetic runic bands (`#4DB38C`), torso split open revealing a spinning turquoise-jade Worldstone core (`#6EE7B7`), oversized detached floating stone gauntlets with carved glowing glyphs, moss and cracked granite shoulders.
Color Palette: strictly constrained to 5-6 ramps centered on Primary #4DB38C, Emissive #6EE7B7, Trim #4A5559, and Shadow #1B2422.
Lighting: zenithal top-left light source; head, crown, shoulders, and weapon tip receive brightest value ramp; lower body drops into #1B2422 shadow.
Foot Anchor: character feet/lower robe hem centered at X=16, Y=3 from bottom edge (Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step Character Sprite Sheet Prompts (`char_cursed_golem_sheet.png`)

To ensure **100% visual consistency** across all states, generate or assemble a **12-Frame Master Character Strip (`384x32 px` total, 12 frames of `32x32 px`)** or **2 rows of 6 frames (`192x64 px`)** using Frame 1 as the locked style/character reference.

### Row 1: Core Locomotion & Combat Strip (`192x32 px` — Required by `PIXEL_ART_STYLE_GUIDE.md`)

- **Frame 01 (`X: 0..31, Y: 0..31`) — `Idle_01` (Base Stance):**
  > `Same 32x32 pixel art character Cursed Golem from Base Prompt. Neutral commanding boss stance, facing South-East (3/4 top-down), weapon held at ready on right side (X:22..29), left hand glowing with 2px #6EE7B7 energy ember, silhouette fits within 26x28 px centered box.`
- **Frame 02 (`X: 32..63, Y: 0..31`) — `Walk_01` (Left Stride / Hover Up):**
  > `Consistent 32x32 pixel art of Cursed Golem. Earth-shaking mechanical piston march: floating stone fists swing opposite to stubby pillar legs, core pulses brighter on each step. Torso shifted 1px up (Y+1), left shoulder forward 1px, weapon trails 1px back, mantle/cloak flares 2px to the left.`
- **Frame 03 (`X: 64..95, Y: 0..31`) — `Walk_02` (Right Stride / Hover Down):**
  > `Consistent 32x32 pixel art of Cursed Golem. Opposite phase of walk cycle: torso settles 1px down, right weapon shoulder surges forward 1px, lower cloak ripples right, 2px #6EE7B7 particle detaches from hem.`
- **Frame 04 (`X: 96..127, Y: 0..31`) — `Attack_Cast_01` (Primary Strike / Spell Release):**
  > `Consistent 32x32 pixel art of Cursed Golem. Launches right floating granite gauntlet forward 6px like a rocket-ram, creating concentric stone shockwave rings (`32x32 px`). Weapon and casting hand thrust forward toward right edge (X:24..31), eyes and weapon core flare pure white #FFFFFF with #6EE7B7 2px halo, dynamic forward lean.`
- **Frame 05 (`X: 128..159, Y: 0..31`) — `Hit_React_01` (Damaged Recoil):**
  > `Consistent 32x32 pixel art of Cursed Golem. Knocked backward 2px to the left, torso arched back, arms flung wide defensively, 40% of primary #4DB38C pixels shifted toward high-contrast white/red hit flash (#FF5555 / #FFFFFF), 3 sharp impact sparks around chest.`
- **Frame 06 (`X: 160..191, Y: 0..31`) — `Death_01` (Defeat Collapse / Dissolve):**
  > `Consistent 32x32 pixel art of Cursed Golem. Weapon dropped horizontally at Y:2..5, crown/helm cracked on ground, body collapsing into a swirling vortex of #4DB38C ash and ascending #6EE7B7 soul embers.`

### Row 2: Extended Boss Action States (`192x32 px` — Optional Polish Strip)

- **Frame 07 — `Idle_02` (Breathing / Aura Pulse):** Chest/shoulders rise 1px, `#6EE7B7` eye/core glow expands by 1px.
- **Frame 08 — `Dash_Phase` (High-Speed Evasion):** Compressed horizontal silhouette tilted 25° forward, trailing 3 semi-transparent `#4DB38C` motion lines.
- **Frame 09 — `Signature_Windup` (Channeling Ultimate):** Both arms raised overhead, levitating 3px off floor, surrounded by converging `#6EE7B7` runes.
- **Frame 10 — `Signature_Release` (Nova Eruption):** Slams both monolithic fists together then into the floor; jade runic circuit lines (`#6EE7B7`) spiderweb across `64x64 px` and erupt upward.
- **Frame 11 — `Summon_Command` (Raising Minions / Totems):** Left hand extended palm-down over a glowing ground sigil (`#6EE7B7`).
- **Frame 12 — `Death_02` (Final Remnant):** Collapsed pile of `#4A5559` armor/robes with a single flickering `#6EE7B7` ember at center.

---

## 3. Basic Attack, Starting Weapon & Signature VFX Prompts

### 3.1 Basic Attack Projectile / Swing Strip (`vfx_cursed_golem_basic_attack.png` — `48x16 px`, 3 frames of `16x16`)
```text
3-frame horizontal pixel art sprite strip (48x16 px total, three 16x16 frames), transparent background, 1px dark rim (#110E1B) on opaque cores, FilterMode.Point.
Subject: Cursed Golem basic attack effect (Monolith Fist (`wpn_ham_titan_anvil_hammer` — tectonic shockwave punch, Range 2.4, Damage 26.0)).
- Frame 1 (0..15): Muzzle/hand cast flash — 10x10 px star-burst in #FFFFFF core and #6EE7B7 outer spikes.
- Frame 2 (16..31): Active traveling projectile or melee slash arc — 14x12 px directional crescent/bolt facing Right (+X), #FFFFFF hot core, #6EE7B7 mid-body, #4DB38C trailing tail.
- Frame 3 (32..47): Impact shatter — 15x15 px radial burst of 6 pixel shards in #6EE7B7 and #4DB38C.
```

### 3.2 Signature Nova / Ultimate Eruption (`vfx_cursed_golem_signature_nova.png` — `256x64 px`, 4 frames of `64x64`)
```text
4-frame horizontal pixel art AoE explosion strip (256x64 px total, four 64x64 frames), top-down perspective, transparent background, crisp pixel art.
Subject: Seismic Slam (`cursed_golem_active_01`) & Runic Overload (Radius 4.2, Damage 52.0) (Slams both monolithic fists together then into the floor; jade runic circuit lines (`#6EE7B7`) spiderweb across `64x64 px` and erupt upward.).
- Frame 1 (64x64): Ground telegraph ring (radius 28px) inscribed with glowing #6EE7B7 runes.
- Frame 2 (64x64): Initial eruption — dense central pillar/wave of #FFFFFF and #6EE7B7 expanding to radius 18px.
- Frame 3 (64x64): Peak shockwave — outer jagged ring at radius 30px in #4DB38C and #6EE7B7, hollow center with ground cracks.
- Frame 4 (64x64): Dissipating embers and smoke rings fading into #1B2422 particles.
```

---

## 4. Complete Class Skill Tree Assets (55 Abilities from `AbilityRegistry.cs`)

Below are all **55 class abilities** registered for **Cursed Golem** across its 4 skill tree branches, with exact icon and VFX generation prompts.

| Ability ID | Name | Branch & Tier | Effect / Targeting | Cost | Cooldown | Base Dmg | Radius |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `cursed_golem_active_01` | **Titan Punch** | Branch A — Colossal Physical Combat (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 20 | CD: 3.5s | Dmg: 32 | Rad: 2.2 |
| `cursed_golem_active_02` | **Stonebreaker** | Branch A — Colossal Physical Combat (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / AimDirection | Cost: 25 | CD: 4.5s | Dmg: 38 | Rad: 2.5 |
| `cursed_golem_active_03` | **Boulder Uppercut** | Branch A — Colossal Physical Combat (T2) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 25 | CD: 5.0s | Dmg: 36 | Rad: 2.0 |
| `cursed_golem_active_04` | **Mountain Slam** | Branch A — Colossal Physical Combat (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 30 | CD: 6.0s | Dmg: 45 | Rad: 4.5 |
| `cursed_golem_active_05` | **Granite Sweep** | Branch A — Colossal Physical Combat (T2) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 25 | CD: 5.0s | Dmg: 40 | Rad: 3.8 |
| `cursed_golem_active_06` | **Crushing Charge** | Branch A — Colossal Physical Combat (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 30 | CD: 6.5s | Dmg: 42 | Rad: 2.5 |
| `cursed_golem_active_07` | **Bedrock Hammer** | Branch A — Colossal Physical Combat (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / AimDirection | Cost: 35 | CD: 7.0s | Dmg: 52 | Rad: 3.2 |
| `cursed_golem_active_08` | **Colossus Grip** | Branch A — Colossal Physical Combat (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.Gravity | AbilityEffectType.CrowdControl / AimDirection | Cost: 35 | CD: 7.5s | Dmg: 48 | Rad: 2.8 |
| `cursed_golem_active_09` | **Double Fist Impact** | Branch A — Colossal Physical Combat (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 40 | CD: 8.0s | Dmg: 58 | Rad: 5.0 |
| `cursed_golem_active_10` | **Worldbreaker Punch** | Branch A — Colossal Physical Combat (T4) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 45 | CD: 9.0s | Dmg: 70 | Rad: 4.5 |
| `cursed_golem_active_11` | **Stone Spike** | Branch B — Earth and Stone Magic (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / AimDirection | Cost: 20 | CD: 4.0s | Dmg: 34 | Rad: 2.2 |
| `cursed_golem_active_12` | **Boulder Throw** | Branch B — Earth and Stone Magic (T1) | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl / AimDirection | Cost: 25 | CD: 4.5s | Dmg: 40 | Rad: 1.5 |
| `cursed_golem_active_13` | **Rockfall** | Branch B — Earth and Stone Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 30 | CD: 6.0s | Dmg: 44 | Rad: 3.5 |
| `cursed_golem_active_14` | **Stone Wall** | Branch B — Earth and Stone Magic (T2) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / Self | Cost: 25 | CD: 6.5s | Dmg: 20 | Rad: 3.0 |
| `cursed_golem_active_15` | **Earth Pillar** | Branch B — Earth and Stone Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 30 | CD: 5.5s | Dmg: 46 | Rad: 2.5 |
| `cursed_golem_active_16` | **Rolling Boulder** | Branch B — Earth and Stone Magic (T3) | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl / AimDirection | Cost: 35 | CD: 7.0s | Dmg: 48 | Rad: 1.8 |
| `cursed_golem_active_17` | **Crystal Eruption** | Branch B — Earth and Stone Magic (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Projectile / Self | Cost: 35 | CD: 7.0s | Dmg: 50 | Rad: 4.0 |
| `cursed_golem_active_18` | **Tremor Field** | Branch B — Earth and Stone Magic (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / Self | Cost: 40 | CD: 8.0s | Dmg: 52 | Rad: 5.0 |
| `cursed_golem_active_19` | **Living Stone** | Branch B — Earth and Stone Magic (T4) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 45 | CD: 9.0s | Dmg: 25 | Rad: 2.0 |
| `cursed_golem_active_20` | **Mountain's Wrath** | Branch B — Earth and Stone Magic (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard / Self | Cost: 45 | CD: 10.0s | Dmg: 75 | Rad: 6.0 |
| `cursed_golem_active_21` | **Rune of Crushing** | Branch C — Ancient Runic Magic (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE / AimDirection | Cost: 20 | CD: 4.0s | Dmg: 36 | Rad: 2.5 |
| `cursed_golem_active_22` | **Rune of Protection** | Branch C — Ancient Runic Magic (T1) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 25 | CD: 6.0s | Dmg: 15 | Rad: 3.5 |
| `cursed_golem_active_23` | **Rune of Binding** | Branch C — Ancient Runic Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 25 | CD: 5.0s | Dmg: 38 | Rad: 3.0 |
| `cursed_golem_active_24` | **Rune of Reflection** | Branch C — Ancient Runic Magic (T2) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / Self | Cost: 30 | CD: 6.0s | Dmg: 30 | Rad: 3.5 |
| `cursed_golem_active_25` | **Rune of Detonation** | Branch C — Ancient Runic Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 30 | CD: 5.5s | Dmg: 48 | Rad: 3.5 |
| `cursed_golem_active_26` | **Rune of Reinforcement** | Branch C — Ancient Runic Magic (T3) | AbilityEffectType.Shield | AbilityEffectType.Heal | AbilityEffectType.RadialAoE / Self | Cost: 35 | CD: 7.0s | Dmg: 25 | Rad: 3.5 |
| `cursed_golem_active_27` | **Rune of Reversal** | Branch C — Ancient Runic Magic (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 35 | CD: 7.0s | Dmg: 50 | Rad: 4.5 |
| `cursed_golem_active_28` | **Rune of Resonance** | Branch C — Ancient Runic Magic (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 40 | CD: 7.5s | Dmg: 54 | Rad: 4.0 |
| `cursed_golem_active_29` | **Ancient Rune Circle** | Branch C — Ancient Runic Magic (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / Self | Cost: 45 | CD: 9.0s | Dmg: 60 | Rad: 5.5 |
| `cursed_golem_active_30` | **Runeheart Awakening** | Branch C — Ancient Runic Magic (T4) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform / Self | Cost: 50 | CD: 11.0s | Dmg: 80 | Rad: 6.5 |
| `cursed_golem_active_31` | **Granite Skin** | Branch D — Fortress and Defense (T1) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / Self | Cost: 20 | CD: 4.5s | Dmg: 25 | Rad: 3.0 |
| `cursed_golem_active_32` | **Living Fortress** | Branch D — Fortress and Defense (T1) | AbilityEffectType.Shield | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE / Self | Cost: 25 | CD: 5.5s | Dmg: 30 | Rad: 3.5 |
| `cursed_golem_active_33` | **Cursed Regeneration** | Branch D — Fortress and Defense (T2) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / Self | Cost: 30 | CD: 6.5s | Dmg: 20 | Rad: 3.0 |
| `cursed_golem_active_34` | **Obsidian Shell** | Branch D — Fortress and Defense (T2) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / Self | Cost: 30 | CD: 6.0s | Dmg: 40 | Rad: 4.0 |
| `cursed_golem_active_35` | **Retaliation Quake** | Branch D — Fortress and Defense (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 35 | CD: 6.5s | Dmg: 48 | Rad: 4.5 |
| `cursed_golem_active_36` | **Stoneheart Guard** | Branch D — Fortress and Defense (T3) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 40 | CD: 7.5s | Dmg: 42 | Rad: 4.0 |
| `cursed_golem_active_37` | **Immovable Titan** | Branch D — Fortress and Defense (T3) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 40 | CD: 8.0s | Dmg: 52 | Rad: 5.0 |
| `cursed_golem_active_38` | **Fortress Roar** | Branch D — Fortress and Defense (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 35 | CD: 7.0s | Dmg: 46 | Rad: 5.5 |
| `cursed_golem_active_39` | **Ancient Reassembly** | Branch D — Fortress and Defense (T4) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / Self | Cost: 50 | CD: 10.0s | Dmg: 35 | Rad: 4.5 |
| `cursed_golem_active_40` | **Adamantine Form** | Branch D — Fortress and Defense (T4) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.CrowdControl / Self | Cost: 55 | CD: 12.0s | Dmg: 85 | Rad: 6.5 |
| `cursed_golem_active_41` | **Gravity Pull** | Branch E — Gravity and Cataclysm (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 20 | CD: 4.5s | Dmg: 30 | Rad: 4.0 |
| `cursed_golem_active_42` | **Crushing Field** | Branch E — Gravity and Cataclysm (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard / AimDirection | Cost: 25 | CD: 5.5s | Dmg: 36 | Rad: 4.5 |
| `cursed_golem_active_43` | **Meteoric Fist** | Branch E — Gravity and Cataclysm (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 35 | CD: 6.0s | Dmg: 55 | Rad: 4.5 |
| `cursed_golem_active_44` | **Gravitic Pulse** | Branch E — Gravity and Cataclysm (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 30 | CD: 6.0s | Dmg: 44 | Rad: 5.0 |
| `cursed_golem_active_45` | **Stone Orbit** | Branch E — Gravity and Cataclysm (T2) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Projectile / Self | Cost: 35 | CD: 7.0s | Dmg: 40 | Rad: 3.5 |
| `cursed_golem_active_46` | **Gravity Prison** | Branch E — Gravity and Cataclysm (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard / AimDirection | Cost: 40 | CD: 7.5s | Dmg: 52 | Rad: 4.5 |
| `cursed_golem_active_47` | **Falling Mountain** | Branch E — Gravity and Cataclysm (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard / AimDirection | Cost: 45 | CD: 8.5s | Dmg: 68 | Rad: 5.5 |
| `cursed_golem_active_48` | **Worldstone Core** | Branch E — Gravity and Cataclysm (T3) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform / Self | Cost: 45 | CD: 9.0s | Dmg: 50 | Rad: 5.0 |
| `cursed_golem_active_49` | **Titan's Judgment** | Branch E — Gravity and Cataclysm (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard / AimDirection | Cost: 50 | CD: 10.0s | Dmg: 80 | Rad: 6.0 |
| `cursed_golem_active_50` | **Colossus Ascension** | Branch E — Gravity and Cataclysm (T4) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard / Self | Cost: 60 | CD: 13.0s | Dmg: 95 | Rad: 7.0 |
| `cursed_golem_ultimate_50` | **Colossus Awakening** | Milestone Ultimates (T5) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform / Self | Cost: 50 | CD: 25s | Dmg: 70 | Rad: 7 |
| `cursed_golem_ultimate_100` | **Faultline Dominion** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / Self | Cost: 60 | CD: 30s | Dmg: 85 | Rad: 8 |
| `cursed_golem_ultimate_150` | **Heart of the Mountain** | Milestone Ultimates (T5) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Heal / Self | Cost: 60 | CD: 32s | Dmg: 65 | Rad: 6.5 |
| `cursed_golem_ultimate_200` | **Gravitic Cataclysm** | Milestone Ultimates (T5) | AbilityEffectType.Gravity | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 70 | CD: 35s | Dmg: 110 | Rad: 9 |
| `cursed_golem_ultimate_250` | **Worldstone Avatar** | Milestone Ultimates (T5) | AbilityEffectType.Transform | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Gravity / Self | Cost: 80 | CD: 40s | Dmg: 135 | Rad: 10 |

### Step-by-Step Prompts for Every Cursed Golem Skill
#### Skill 01: Titan Punch (`cursed_golem_active_01` — Branch A — Colossal Physical Combat, Tier 1)
- **Gameplay Context:** Deliver an extremely powerful forward punch that staggers enemies and knocks them back. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `2.2`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Titan Punch", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting deliver an extremely powerful forward punch that staggers enemies and knocks them back., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Titan Punch" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 02: Stonebreaker (`cursed_golem_active_02` — Branch A — Colossal Physical Combat, Tier 1)
- **Gameplay Context:** Deliver a heavy armor-crushing strike that shatters enemy defenses. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `2.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Stonebreaker", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting deliver a heavy armor-crushing strike that shatters enemy defenses., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Stonebreaker" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 03: Boulder Uppercut (`cursed_golem_active_03` — Branch A — Colossal Physical Combat, Tier 2)
- **Gameplay Context:** Launch enemies violently into the air with a rising stone fist. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `2.0`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Boulder Uppercut", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting launch enemies violently into the air with a rising stone fist., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Boulder Uppercut" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 04: Mountain Slam (`cursed_golem_active_04` — Branch A — Colossal Physical Combat, Tier 2)
- **Gameplay Context:** Pound the ground with titanic force, releasing a radial shockwave that staggers invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Mountain Slam", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting pound the ground with titanic force, releasing a radial shockwave that staggers invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Mountain Slam" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 05: Granite Sweep (`cursed_golem_active_05` — Branch A — Colossal Physical Combat, Tier 2)
- **Gameplay Context:** Perform a wide sweeping stone-arm attack that cleaves invaders across a broad frontal arc. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `3.8`, Duration: `0.45s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Granite Sweep", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting perform a wide sweeping stone-arm attack that cleaves invaders across a broad frontal arc., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Granite Sweep" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 06: Crushing Charge (`cursed_golem_active_06` — Branch A — Colossal Physical Combat, Tier 3)
- **Gameplay Context:** Rush forward unstoppably through nearby enemies, trampling all in your path. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `2.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crushing Charge", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting rush forward unstoppably through nearby enemies, trampling all in your path., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crushing Charge" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 07: Bedrock Hammer (`cursed_golem_active_07` — Branch A — Colossal Physical Combat, Tier 3)
- **Gameplay Context:** Transform one arm into a giant stone hammer and smash down with devastating impact. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `3.2`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Bedrock Hammer", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting transform one arm into a giant stone hammer and smash down with devastating impact., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Bedrock Hammer" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 08: Colossus Grip (`cursed_golem_active_08` — Branch A — Colossal Physical Combat, Tier 3)
- **Gameplay Context:** Grab distant enemies with ancient gravitational stone power, pulling them inward before slamming them. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Gravity | AbilityEffectType.CrowdControl`, Radius: `2.8`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Colossus Grip", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting grab distant enemies with ancient gravitational stone power, pulling them inward before slamming them., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Colossus Grip" (AbilityEffectType.MeleeArc | AbilityEffectType.Gravity | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 09: Double Fist Impact (`cursed_golem_active_09` — Branch A — Colossal Physical Combat, Tier 4)
- **Gameplay Context:** Strike the ground with both colossal fists simultaneously, creating an overwhelming earth tremor. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Double Fist Impact", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting strike the ground with both colossal fists simultaneously, creating an overwhelming earth tremor., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Double Fist Impact" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 10: Worldbreaker Punch (`cursed_golem_active_10` — Branch A — Colossal Physical Combat, Tier 4)
- **Gameplay Context:** Release a catastrophic shockwave from an empowered stone punch that pierces through armies. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `0.65s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Worldbreaker Punch", Cursed Golem ability (Branch A — Colossal Physical Combat), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting release a catastrophic shockwave from an empowered stone punch that pierces through armies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Worldbreaker Punch" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 11: Stone Spike (`cursed_golem_active_11` — Branch B — Earth and Stone Magic, Tier 1)
- **Gameplay Context:** Summon sharp stone spikes bursting from beneath enemies, impaling and slowing them. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `2.2`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Stone Spike", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting summon sharp stone spikes bursting from beneath enemies, impaling and slowing them., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Stone Spike" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 12: Boulder Throw (`cursed_golem_active_12` — Branch B — Earth and Stone Magic, Tier 1)
- **Gameplay Context:** Launch a colossal rock projectile that crushes invaders in a line and knocks them back. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.CrowdControl`, Radius: `1.5`, Duration: `1.2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Boulder Throw", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting launch a colossal rock projectile that crushes invaders in a line and knocks them back., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Boulder Throw" (AbilityEffectType.Projectile | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 13: Rockfall (`cursed_golem_active_13` — Branch B — Earth and Stone Magic, Tier 2)
- **Gameplay Context:** Call down a relentless rain of heavy rocks over an area, crushing all invaders beneath. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rockfall", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting call down a relentless rain of heavy rocks over an area, crushing all invaders beneath., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rockfall" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 14: Stone Wall (`cursed_golem_active_14` — Branch B — Earth and Stone Magic, Tier 2)
- **Gameplay Context:** Raise an impenetrable stone bulwark granting an absorption barrier and emitting concussive shock. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `3.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Stone Wall", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting raise an impenetrable stone bulwark granting an absorption barrier and emitting concussive shock., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Stone Wall" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 15: Earth Pillar (`cursed_golem_active_15` — Branch B — Earth and Stone Magic, Tier 2)
- **Gameplay Context:** Erupt a massive stone monolith violently from the earth, launching enemies into the air. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `2.5`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Earth Pillar", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting erupt a massive stone monolith violently from the earth, launching enemies into the air., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Earth Pillar" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 16: Rolling Boulder (`cursed_golem_active_16` — Branch B — Earth and Stone Magic, Tier 3)
- **Gameplay Context:** Hurl an enormous rolling boulder that plows forward continuously, bowling through ranks of invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.CrowdControl`, Radius: `1.8`, Duration: `1.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rolling Boulder", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting hurl an enormous rolling boulder that plows forward continuously, bowling through ranks of invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rolling Boulder" (AbilityEffectType.Projectile | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 17: Crystal Eruption (`cursed_golem_active_17` — Branch B — Earth and Stone Magic, Tier 3)
- **Gameplay Context:** Detonate sharp crystalline formations that erupt outwards in all directions, shredding invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Projectile`, Radius: `4.0`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crystal Eruption", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting detonate sharp crystalline formations that erupt outwards in all directions, shredding invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crystal Eruption" (AbilityEffectType.RadialAoE | AbilityEffectType.Projectile), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 18: Tremor Field (`cursed_golem_active_18` — Branch B — Earth and Stone Magic, Tier 3)
- **Gameplay Context:** Create a persistent zone of violent seismic vibrations that repeatedly staggers and slows enemies. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Tremor Field", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting create a persistent zone of violent seismic vibrations that repeatedly staggers and slows enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Tremor Field" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 19: Living Stone (`cursed_golem_active_19` — Branch B — Earth and Stone Magic, Tier 4)
- **Gameplay Context:** Awaken fractured stone fragments as mobile animated constructs that batter nearby enemies. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `2.0`, Duration: `15.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Living Stone", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting awaken fractured stone fragments as mobile animated constructs that batter nearby enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Living Stone" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 20: Mountain's Wrath (`cursed_golem_active_20` — Branch B — Earth and Stone Magic, Tier 4)
- **Gameplay Context:** Unleash the catastrophic fury of tectonic plates, causing a chain of violent ground eruptions. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard`, Radius: `6.0`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Mountain's Wrath", Cursed Golem ability (Branch B — Earth and Stone Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting unleash the catastrophic fury of tectonic plates, causing a chain of violent ground eruptions., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Mountain's Wrath" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 21: Rune of Crushing (`cursed_golem_active_21` — Branch C — Ancient Runic Magic, Tier 1)
- **Gameplay Context:** Inscribe a crushing rune on your stone fists that empowers physical impacts and releases concussive bursts. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE`, Radius: `2.5`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rune of Crushing", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting inscribe a crushing rune on your stone fists that empowers physical impacts and releases concussive bursts., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rune of Crushing" (AbilityEffectType.MeleeArc | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 22: Rune of Protection (`cursed_golem_active_22` — Branch C — Ancient Runic Magic, Tier 1)
- **Gameplay Context:** Channel a runic barrier that absorbs incoming damage and emits a tremor shockwave upon impact. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `8.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rune of Protection", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting channel a runic barrier that absorbs incoming damage and emits a tremor shockwave upon impact., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rune of Protection" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 23: Rune of Binding (`cursed_golem_active_23` — Branch C — Ancient Runic Magic, Tier 2)
- **Gameplay Context:** Inscribe an ancient binding rune that locks invaders in place with impenetrable runic chains. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.0`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rune of Binding", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting inscribe an ancient binding rune that locks invaders in place with impenetrable runic chains., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rune of Binding" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 24: Rune of Reflection (`cursed_golem_active_24` — Branch C — Ancient Runic Magic, Tier 2)
- **Gameplay Context:** Erect an arcane runic mirror that grants an absorption barrier and reflects kinetic force back at attackers. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rune of Reflection", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting erect an arcane runic mirror that grants an absorption barrier and reflects kinetic force back at attackers., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rune of Reflection" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 25: Rune of Detonation (`cursed_golem_active_25` — Branch C — Ancient Runic Magic, Tier 2)
- **Gameplay Context:** Plant a volatile runic glyph that violently detonates when invaders step near, tearing the earth. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rune of Detonation", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting plant a volatile runic glyph that violently detonates when invaders step near, tearing the earth., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rune of Detonation" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 26: Rune of Reinforcement (`cursed_golem_active_26` — Branch C — Ancient Runic Magic, Tier 3)
- **Gameplay Context:** Etch protective fortification runes into stone armor, surging defense and pulsing restorative wards. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.Heal | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `8.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rune of Reinforcement", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting etch protective fortification runes into stone armor, surging defense and pulsing restorative wards., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rune of Reinforcement" (AbilityEffectType.Shield | AbilityEffectType.Heal | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 27: Rune of Reversal (`cursed_golem_active_27` — Branch C — Ancient Runic Magic, Tier 3)
- **Gameplay Context:** Unleash an inverted gravitic rune that reverses incoming momentum, blasting invaders away. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rune of Reversal", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting unleash an inverted gravitic rune that reverses incoming momentum, blasting invaders away., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rune of Reversal" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 28: Rune of Resonance (`cursed_golem_active_28` — Branch C — Ancient Runic Magic, Tier 3)
- **Gameplay Context:** Link runic frequencies across the battlefield, triggering a harmonic chain reaction between invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rune of Resonance", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting link runic frequencies across the battlefield, triggering a harmonic chain reaction between invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rune of Resonance" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 29: Ancient Rune Circle (`cursed_golem_active_29` — Branch C — Ancient Runic Magic, Tier 4)
- **Gameplay Context:** Inscribe an enormous runic circle on the floor, enhancing the golem while burning and crippling all intruders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `5.5`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ancient Rune Circle", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting inscribe an enormous runic circle on the floor, enhancing the golem while burning and crippling all intruders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ancient Rune Circle" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 30: Runeheart Awakening (`cursed_golem_active_30` — Branch C — Ancient Runic Magic, Tier 4)
- **Gameplay Context:** Awaken the primordial runic core within your chest, supercharging all runic magic and triggering catastrophic surges. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform`, Radius: `6.5`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Runeheart Awakening", Cursed Golem ability (Branch C — Ancient Runic Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting awaken the primordial runic core within your chest, supercharging all runic magic and triggering catastrophic surges., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Runeheart Awakening" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 31: Granite Skin (`cursed_golem_active_31` — Branch D — Fortress and Defense, Tier 1)
- **Gameplay Context:** Harden your exterior stone plates into unyielding granite, gaining a barrier and releasing a concussive shock. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `3.0`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Granite Skin", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting harden your exterior stone plates into unyielding granite, gaining a barrier and releasing a concussive shock., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Granite Skin" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 32: Living Fortress (`cursed_golem_active_32` — Branch D — Fortress and Defense, Tier 1)
- **Gameplay Context:** Root your titanic bulk into the dungeon floor, gaining an impenetrable defense shield and staggering nearby foes. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Living Fortress", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting root your titanic bulk into the dungeon floor, gaining an impenetrable defense shield and staggering nearby foes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Living Fortress" (AbilityEffectType.Shield | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 33: Cursed Regeneration (`cursed_golem_active_33` — Branch D — Fortress and Defense, Tier 2)
- **Gameplay Context:** Channel dark earthen energy into your stone core to rapidly repair physical integrity and erect stone barriers. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `3.0`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Cursed Regeneration", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting channel dark earthen energy into your stone core to rapidly repair physical integrity and erect stone barriers., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Cursed Regeneration" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 34: Obsidian Shell (`cursed_golem_active_34` — Branch D — Fortress and Defense, Tier 2)
- **Gameplay Context:** Encase your outer chassis in sharp volcanic obsidian, absorbing damage and blasting razor obsidian shards outward. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `4.0`, Duration: `5.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Obsidian Shell", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting encase your outer chassis in sharp volcanic obsidian, absorbing damage and blasting razor obsidian shards outward., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Obsidian Shell" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 35: Retaliation Quake (`cursed_golem_active_35` — Branch D — Fortress and Defense, Tier 2)
- **Gameplay Context:** Store kinetic energy from incoming impacts, releasing a devastating seismic shockwave that knocks back all nearby invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Retaliation Quake", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting store kinetic energy from incoming impacts, releasing a devastating seismic shockwave that knocks back all nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Retaliation Quake" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 36: Stoneheart Guard (`cursed_golem_active_36` — Branch D — Fortress and Defense, Tier 3)
- **Gameplay Context:** Surround your soulstone with impenetrable layered bedrock shields, slowing attackers and absorbing heavy damage. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `6.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Stoneheart Guard", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting surround your soulstone with impenetrable layered bedrock shields, slowing attackers and absorbing heavy damage., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Stoneheart Guard" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 37: Immovable Titan (`cursed_golem_active_37` — Branch D — Fortress and Defense, Tier 3)
- **Gameplay Context:** Assume the posture of an ancient mountain colossus, gaining massive barrier and crushing the earth underfoot. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Immovable Titan", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting assume the posture of an ancient mountain colossus, gaining massive barrier and crushing the earth underfoot., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Immovable Titan" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 38: Fortress Roar (`cursed_golem_active_38` — Branch D — Fortress and Defense, Tier 3)
- **Gameplay Context:** Emit a resonating seismic bellow from your stone throat, crippling all nearby invaders and shattering their armor. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Fortress Roar", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting emit a resonating seismic bellow from your stone throat, crippling all nearby invaders and shattering their armor., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Fortress Roar" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 39: Ancient Reassembly (`cursed_golem_active_39` — Branch D — Fortress and Defense, Tier 4)
- **Gameplay Context:** Re-knit splintered masonry and broken runes, instantly repairing health and generating a massive protective bulwark. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `4.5`, Duration: `7.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ancient Reassembly", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting re-knit splintered masonry and broken runes, instantly repairing health and generating a massive protective bulwark., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ancient Reassembly" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 40: Adamantine Form (`cursed_golem_active_40` — Branch D — Fortress and Defense, Tier 4)
- **Gameplay Context:** Transmute your stone frame into unbreakable adamantine, granting supreme barrier defenses and releasing catastrophic seismic tremors. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.CrowdControl`, Radius: `6.5`, Duration: `8.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Adamantine Form", Cursed Golem ability (Branch D — Fortress and Defense), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting transmute your stone frame into unbreakable adamantine, granting supreme barrier defenses and releasing catastrophic seismic tremors., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Adamantine Form" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 41: Gravity Pull (`cursed_golem_active_41` — Branch E — Gravity and Cataclysm, Tier 1)
- **Gameplay Context:** Create a localized gravitational anomaly, pulling all surrounding invaders toward the epicenter. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.0`, Duration: `1.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Gravity Pull", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting create a localized gravitational anomaly, pulling all surrounding invaders toward the epicenter., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Gravity Pull" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 42: Crushing Field (`cursed_golem_active_42` — Branch E — Gravity and Cataclysm, Tier 1)
- **Gameplay Context:** Dramatically increase gravitational pressure in an area, crushing invaders downward and halting movement. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard`, Radius: `4.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crushing Field", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting dramatically increase gravitational pressure in an area, crushing invaders downward and halting movement., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crushing Field" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 43: Meteoric Fist (`cursed_golem_active_43` — Branch E — Gravity and Cataclysm, Tier 2)
- **Gameplay Context:** Leap into the air and bring down a magically reinforced fist like a falling meteor, obliterating ground targets. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Meteoric Fist", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting leap into the air and bring down a magically reinforced fist like a falling meteor, obliterating ground targets., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Meteoric Fist" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 44: Gravitic Pulse (`cursed_golem_active_44` — Branch E — Gravity and Cataclysm, Tier 2)
- **Gameplay Context:** Release an expanding wave of repulsive gravitational force, flinging away all invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Gravitic Pulse", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting release an expanding wave of repulsive gravitational force, flinging away all invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Gravitic Pulse" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 45: Stone Orbit (`cursed_golem_active_45` — Branch E — Gravity and Cataclysm, Tier 2)
- **Gameplay Context:** Summon dense gravitating boulders that orbit your chassis, shredding encroaching invaders and deflecting blows. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Projectile`, Radius: `3.5`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Stone Orbit", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting summon dense gravitating boulders that orbit your chassis, shredding encroaching invaders and deflecting blows., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Stone Orbit" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Projectile), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 46: Gravity Prison (`cursed_golem_active_46` — Branch E — Gravity and Cataclysm, Tier 3)
- **Gameplay Context:** Trap targeted invaders inside a compressed hyper-gravity vortex, suspending and crushing them. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard`, Radius: `4.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Gravity Prison", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting trap targeted invaders inside a compressed hyper-gravity vortex, suspending and crushing them., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Gravity Prison" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 47: Falling Mountain (`cursed_golem_active_47` — Branch E — Gravity and Cataclysm, Tier 3)
- **Gameplay Context:** Tear a colossal mountain mass from the sky, dropping it onto the battlefield with cataclysmic impact. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard`, Radius: `5.5`, Duration: `1.2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Falling Mountain", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting tear a colossal mountain mass from the sky, dropping it onto the battlefield with cataclysmic impact., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Falling Mountain" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 48: Worldstone Core (`cursed_golem_active_48` — Branch E — Gravity and Cataclysm, Tier 3)
- **Gameplay Context:** Overcharge your primordial core with cataclysmic worldstone energy, supercharging shields and radiating kinetic pulses. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform`, Radius: `5.0`, Duration: `6.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Worldstone Core", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting overcharge your primordial core with cataclysmic worldstone energy, supercharging shields and radiating kinetic pulses., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Worldstone Core" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 49: Titan's Judgment (`cursed_golem_active_49` — Branch E — Gravity and Cataclysm, Tier 4)
- **Gameplay Context:** Channel all gravitational mass into an earth-shattering ground slam, fracturing tectonic faults across the battlefield. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard`, Radius: `6.0`, Duration: `1.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Titan's Judgment", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting channel all gravitational mass into an earth-shattering ground slam, fracturing tectonic faults across the battlefield., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Titan's Judgment" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (AimDirection) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 50: Colossus Ascension (`cursed_golem_active_50` — Branch E — Gravity and Cataclysm, Tier 4)
- **Gameplay Context:** Ascend to primordial cataclysm form, generating a gargantuan barrier, manipulating gravity, and triggering continuous seismic collapses. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard`, Radius: `7.0`, Duration: `9.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Colossus Ascension", Cursed Golem ability (Branch E — Gravity and Cataclysm), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting ascend to primordial cataclysm form, generating a gargantuan barrier, manipulating gravity, and triggering continuous seismic collapses., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Colossus Ascension" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 51: Colossus Awakening (`cursed_golem_ultimate_50` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Awaken the ancient colossus within, gaining an enormous barrier and triggering massive seismic eruptions. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform`, Radius: `7`, Duration: `10s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Colossus Awakening", Cursed Golem ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting awaken the ancient colossus within, gaining an enormous barrier and triggering massive seismic eruptions., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Colossus Awakening" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Transform), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 52: Faultline Dominion (`cursed_golem_ultimate_100` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Split the battlefield with powerful earthquakes, stone eruptions, and falling boulders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `8`, Duration: `12s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Faultline Dominion", Cursed Golem ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting split the battlefield with powerful earthquakes, stone eruptions, and falling boulders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Faultline Dominion" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 53: Heart of the Mountain (`cursed_golem_ultimate_150` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Enter a legendary defensive state that absorbs damage and releases stored energy through counterattacks. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Heal`, Radius: `6.5`, Duration: `12s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Heart of the Mountain", Cursed Golem ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting enter a legendary defensive state that absorbs damage and releases stored energy through counterattacks., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Heart of the Mountain" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Heal), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 54: Gravitic Cataclysm (`cursed_golem_ultimate_200` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Create a massive gravity field that drags enemies together before detonating in a devastating explosion. (Targeting: `Self`, Effect: `AbilityEffectType.Gravity | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `9`, Duration: `8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Gravitic Cataclysm", Cursed Golem ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting create a massive gravity field that drags enemies together before detonating in a devastating explosion., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Gravitic Cataclysm" (AbilityEffectType.Gravity | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 55: Worldstone Avatar (`cursed_golem_ultimate_250` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Transform into an enormous awakened worldstone titan combining colossal physical attacks, gravity powers, and runic magic. (Targeting: `Self`, Effect: `AbilityEffectType.Transform | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Gravity`, Radius: `10`, Duration: `15s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Worldstone Avatar", Cursed Golem ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #4DB38C and glowing #6EE7B7 highlights, depicting transform into an enormous awakened worldstone titan combining colossal physical attacks, gravity powers, and runic magic., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Worldstone Avatar" (AbilityEffectType.Transform | AbilityEffectType.Shield | AbilityEffectType.RadialAoE | AbilityEffectType.Gravity), frame 1: arcane cast flash on Cursed Golem hands in #6EE7B7, frame 2: active delivery (Self) with #4DB38C core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`

---

## 5. Unity Import, Slicing & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Filter / Format | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Character Sheet** | `Assets/_Game/Art/Characters/cursed_golem/char_cursed_golem_sheet.png` | `192x32` (or `384x32`) | `32` | `(0.5, 0.1)` | `Point`, `RGBA32` | `LichLordVisualPresenter._bodyRenderer` (`PlaceholderSprites.Circle` tinted `#4DB38C`) |
| **Portrait Icon** | `Assets/_Game/Art/UI/Portraits/portrait_cursed_golem.png` | `48x48` | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `CharacterSelectPresenter` class card color block |
| **Basic Attack VFX** | `Assets/_Game/Art/VFX/Characters/vfx_cursed_golem_basic.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `ProjectileView` / `AttackTelegraphPresenter` (`PlaceholderSprites.Circle`) |
| **Signature VFX** | `Assets/_Game/Art/VFX/Characters/vfx_cursed_golem_signature.png` | `256x64` (`4x 64x64`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `DeathWaveVfxPresenter` expanding ring placeholder |
| **Skill Icons (55x)** | `Assets/_Game/Art/UI/Skills/cursed_golem/icon_<ability_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `SkillTreeScreenPresenter` & `HudPresenter` slot squares |
