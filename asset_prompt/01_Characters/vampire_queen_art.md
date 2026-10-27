# Vampire Queen (`vampire_queen`) — Complete Art & Animation Prompt Specification

> **Asset Category:** Playable Boss Overlord (`01_Characters`)
> **Catalog ID:** `CharacterCatalog.VampireQueenId` (`"vampire_queen"`)
> **Class Title:** *Matriarch of the Crimson Court*
> **Combat Profile:** HP: 110 | Armor: 7 | MoveSpeed: 5.0 | DashSpeed: 14.5 (0.22s, 2.2s CD) | **Resource:** Blood Essence (Max 100, +4/s regen, +16 per kill)
> **Starting Weapon:** Crimson Rapier (`wpn_1h_sunforged_rapier` — rapid sanguine lunge, Range 3.5, Damage 16.0)
> **Signature Ability:** Sanguine Lunge (`vampire_queen_active_01`) & Blood Nova Drain (Radius 3.4, Damage 34.0)

---

## 1. Base Character Design & Master Visual Prompt

### 1.1 Color Palette & Pixel Rules
| Swatch Role | Hex Code | RGB Float (`CharacterCatalog`) | Usage |
| :--- | :--- | :--- | :--- |
| **Outer Rim Outline** | `#110E1B` | `(0.067, 0.055, 0.106)` | Mandatory 1px dark silhouette border around all opaque pixels |
| **Primary Class Color** | `#D91A4D` | Matches `CharacterCatalog` RGB | Primary robes/armor/mantle body mass (60% of sprite area) |
| **Secondary Energy / Glow** | `#FF6680` | High-value emissive | Eyes, weapon core, spell casting hands, signature aura (20% of sprite area) |
| **Trim / Bone / Metal** | `#F2E8DC` | Neutral contrast | Crown, pauldrons, weapon shaft, structural trim (15% of sprite area) |
| **Deep Shadow** | `#240811` | recessed folds | Interior cloak folds, under-chin shadow, lower hem (5% of sprite area) |

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle Reference)
```text
Masterpiece 2D top-down 3/4 view pixel art character sprite of Vampire Queen (Matriarch of the Crimson Court), playable final boss in a dark fantasy survival roguelite.
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, zero motion blur, 1px solid dark outline (#110E1B).
Silhouette & Design: Regal aristocratic vampire sovereign with flowing silver-ivory hair (`#F2E8DC`), high-collared gothic ballgown of deep crimson velvet (`#D91A4D`), bat-wing silhouette cape floating behind, left hand levitating a spinning orb of liquid blood (`#FF6680`), right hand poised with a needle-thin Crimson Rapier.
Color Palette: strictly constrained to 5-6 ramps centered on Primary #D91A4D, Emissive #FF6680, Trim #F2E8DC, and Shadow #240811.
Lighting: zenithal top-left light source; head, crown, shoulders, and weapon tip receive brightest value ramp; lower body drops into #240811 shadow.
Foot Anchor: character feet/lower robe hem centered at X=16, Y=3 from bottom edge (Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step Character Sprite Sheet Prompts (`char_vampire_queen_sheet.png`)

To ensure **100% visual consistency** across all states, generate or assemble a **12-Frame Master Character Strip (`384x32 px` total, 12 frames of `32x32 px`)** or **2 rows of 6 frames (`192x64 px`)** using Frame 1 as the locked style/character reference.

### Row 1: Core Locomotion & Combat Strip (`192x32 px` — Required by `PIXEL_ART_STYLE_GUIDE.md`)

- **Frame 01 (`X: 0..31, Y: 0..31`) — `Idle_01` (Base Stance):**
  > `Same 32x32 pixel art character Vampire Queen from Base Prompt. Neutral commanding boss stance, facing South-East (3/4 top-down), weapon held at ready on right side (X:22..29), left hand glowing with 2px #FF6680 energy ember, silhouette fits within 26x28 px centered box.`
- **Frame 02 (`X: 32..63, Y: 0..31`) — `Walk_01` (Left Stride / Hover Up):**
  > `Consistent 32x32 pixel art of Vampire Queen. Predatory aristocratic glide: gown trails smoothly, bat-wing cape flutters at tips, 2 crimson blood droplets orbit her waist. Torso shifted 1px up (Y+1), left shoulder forward 1px, weapon trails 1px back, mantle/cloak flares 2px to the left.`
- **Frame 03 (`X: 64..95, Y: 0..31`) — `Walk_02` (Right Stride / Hover Down):**
  > `Consistent 32x32 pixel art of Vampire Queen. Opposite phase of walk cycle: torso settles 1px down, right weapon shoulder surges forward 1px, lower cloak ripples right, 2px #FF6680 particle detaches from hem.`
- **Frame 04 (`X: 96..127, Y: 0..31`) — `Attack_Cast_01` (Primary Strike / Spell Release):**
  > `Consistent 32x32 pixel art of Vampire Queen. Lightning-fast fencing thrust with Crimson Rapier accompanied by a whipping blood-ribbon slash (`32x32 px`). Weapon and casting hand thrust forward toward right edge (X:24..31), eyes and weapon core flare pure white #FFFFFF with #FF6680 2px halo, dynamic forward lean.`
- **Frame 05 (`X: 128..159, Y: 0..31`) — `Hit_React_01` (Damaged Recoil):**
  > `Consistent 32x32 pixel art of Vampire Queen. Knocked backward 2px to the left, torso arched back, arms flung wide defensively, 40% of primary #D91A4D pixels shifted toward high-contrast white/red hit flash (#FF5555 / #FFFFFF), 3 sharp impact sparks around chest.`
- **Frame 06 (`X: 160..191, Y: 0..31`) — `Death_01` (Defeat Collapse / Dissolve):**
  > `Consistent 32x32 pixel art of Vampire Queen. Weapon dropped horizontally at Y:2..5, crown/helm cracked on ground, body collapsing into a swirling vortex of #D91A4D ash and ascending #FF6680 soul embers.`

### Row 2: Extended Boss Action States (`192x32 px` — Optional Polish Strip)

- **Frame 07 — `Idle_02` (Breathing / Aura Pulse):** Chest/shoulders rise 1px, `#FF6680` eye/core glow expands by 1px.
- **Frame 08 — `Dash_Phase` (High-Speed Evasion):** Compressed horizontal silhouette tilted 25° forward, trailing 3 semi-transparent `#D91A4D` motion lines.
- **Frame 09 — `Signature_Windup` (Channeling Ultimate):** Both arms raised overhead, levitating 3px off floor, surrounded by converging `#FF6680` runes.
- **Frame 10 — `Signature_Release` (Nova Eruption):** Dissolves into a swirling vortex of 6 crimson bats (`#D91A4D`) and re-materializes with an exploding radial Blood Nova (`64x64 px`).
- **Frame 11 — `Summon_Command` (Raising Minions / Totems):** Left hand extended palm-down over a glowing ground sigil (`#FF6680`).
- **Frame 12 — `Death_02` (Final Remnant):** Collapsed pile of `#F2E8DC` armor/robes with a single flickering `#FF6680` ember at center.

---

## 3. Basic Attack, Starting Weapon & Signature VFX Prompts

### 3.1 Basic Attack Projectile / Swing Strip (`vfx_vampire_queen_basic_attack.png` — `48x16 px`, 3 frames of `16x16`)
```text
3-frame horizontal pixel art sprite strip (48x16 px total, three 16x16 frames), transparent background, 1px dark rim (#110E1B) on opaque cores, FilterMode.Point.
Subject: Vampire Queen basic attack effect (Crimson Rapier (`wpn_1h_sunforged_rapier` — rapid sanguine lunge, Range 3.5, Damage 16.0)).
- Frame 1 (0..15): Muzzle/hand cast flash — 10x10 px star-burst in #FFFFFF core and #FF6680 outer spikes.
- Frame 2 (16..31): Active traveling projectile or melee slash arc — 14x12 px directional crescent/bolt facing Right (+X), #FFFFFF hot core, #FF6680 mid-body, #D91A4D trailing tail.
- Frame 3 (32..47): Impact shatter — 15x15 px radial burst of 6 pixel shards in #FF6680 and #D91A4D.
```

### 3.2 Signature Nova / Ultimate Eruption (`vfx_vampire_queen_signature_nova.png` — `256x64 px`, 4 frames of `64x64`)
```text
4-frame horizontal pixel art AoE explosion strip (256x64 px total, four 64x64 frames), top-down perspective, transparent background, crisp pixel art.
Subject: Sanguine Lunge (`vampire_queen_active_01`) & Blood Nova Drain (Radius 3.4, Damage 34.0) (Dissolves into a swirling vortex of 6 crimson bats (`#D91A4D`) and re-materializes with an exploding radial Blood Nova (`64x64 px`).).
- Frame 1 (64x64): Ground telegraph ring (radius 28px) inscribed with glowing #FF6680 runes.
- Frame 2 (64x64): Initial eruption — dense central pillar/wave of #FFFFFF and #FF6680 expanding to radius 18px.
- Frame 3 (64x64): Peak shockwave — outer jagged ring at radius 30px in #D91A4D and #FF6680, hollow center with ground cracks.
- Frame 4 (64x64): Dissipating embers and smoke rings fading into #240811 particles.
```

---

## 4. Complete Class Skill Tree Assets (56 Abilities from `AbilityRegistry.cs`)

Below are all **56 class abilities** registered for **Vampire Queen** across its 4 skill tree branches, with exact icon and VFX generation prompts.

| Ability ID | Name | Branch & Tier | Effect / Targeting | Cost | Cooldown | Base Dmg | Radius |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `vampire_queen_active_01` | **Sanguine Burst** | Branch A — Blood Sorcery (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal / Self | Cost: 25 | CD: 4.0s | Dmg: 30 | Rad: 4.5 |
| `vampire_queen_active_02` | **Crimson Lance** | Branch A — Blood Sorcery (T1) | AbilityEffectType.Projectile | AbilityEffectType.Heal / AimDirection | Cost: 20 | CD: 3.5s | Dmg: 36 | Rad: 1.5 |
| `vampire_queen_active_03` | **Blood Well** | Branch A — Blood Sorcery (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal / AreaCursorOrAim | Cost: 30 | CD: 5.5s | Dmg: 40 | Rad: 3.8 |
| `vampire_queen_active_04` | **Transfusion** | Branch A — Blood Sorcery (T2) | AbilityEffectType.Heal | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE / NearestInvader | Cost: 25 | CD: 5.0s | Dmg: 44 | Rad: 3.0 |
| `vampire_queen_active_05` | **Sanguine Ward** | Branch A — Blood Sorcery (T2) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / Self | Cost: 30 | CD: 6.0s | Dmg: 32 | Rad: 3.5 |
| `vampire_queen_active_06` | **Blood Boil** | Branch A — Blood Sorcery (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Heal / Self | Cost: 35 | CD: 6.5s | Dmg: 52 | Rad: 4.8 |
| `vampire_queen_active_07` | **Exsanguinate** | Branch A — Blood Sorcery (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.Heal / AimDirection | Cost: 40 | CD: 7.0s | Dmg: 58 | Rad: 4.0 |
| `vampire_queen_active_08` | **Crimson Tempest** | Branch A — Blood Sorcery (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 40 | CD: 7.5s | Dmg: 62 | Rad: 4.5 |
| `vampire_queen_active_09` | **Haemomancy** | Branch A — Blood Sorcery (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / Self | Cost: 45 | CD: 8.5s | Dmg: 72 | Rad: 5.5 |
| `vampire_queen_active_10` | **Heartstopper** | Branch A — Blood Sorcery (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl / NearestInvader | Cost: 55 | CD: 10.0s | Dmg: 90 | Rad: 6.0 |
| `vampire_queen_active_11` | **Rapier Thrust** | Branch B — Precision & Rapier (T1) | AbilityEffectType.MeleeArc | AbilityEffectType.Heal / AimDirection | Cost: 15 | CD: 2.0s | Dmg: 32 | Rad: 3.5 |
| `vampire_queen_active_12` | **Crimson Needle** | Branch B — Precision & Rapier (T1) | AbilityEffectType.Projectile | AbilityEffectType.Heal / AimDirection | Cost: 18 | CD: 3.0s | Dmg: 28 | Rad: 0.8 |
| `vampire_queen_active_13` | **Riposte** | Branch B — Precision & Rapier (T2) | AbilityEffectType.MeleeArc | AbilityEffectType.Shield / AimDirection | Cost: 20 | CD: 4.5s | Dmg: 44 | Rad: 4.0 |
| `vampire_queen_active_14` | **Fencing Lunge** | Branch B — Precision & Rapier (T2) | AbilityEffectType.Dash | AbilityEffectType.MeleeArc / AimDirection | Cost: 25 | CD: 5.0s | Dmg: 46 | Rad: 3.0 |
| `vampire_queen_active_15` | **Needle Rain** | Branch B — Precision & Rapier (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AreaCursorOrAim | Cost: 30 | CD: 5.5s | Dmg: 48 | Rad: 4.2 |
| `vampire_queen_active_16` | **Arterial Flurry** | Branch B — Precision & Rapier (T3) | AbilityEffectType.MeleeArc | AbilityEffectType.Heal / AimDirection | Cost: 35 | CD: 6.0s | Dmg: 56 | Rad: 4.0 |
| `vampire_queen_active_17` | **Crystal Volley** | Branch B — Precision & Rapier (T3) | AbilityEffectType.Projectile | AbilityEffectType.RadialAoE / AimDirection | Cost: 35 | CD: 6.5s | Dmg: 60 | Rad: 2.0 |
| `vampire_queen_active_18` | **Peerless Feint** | Branch B — Precision & Rapier (T3) | AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 35 | CD: 6.0s | Dmg: 64 | Rad: 3.5 |
| `vampire_queen_active_19` | **Grand Battement** | Branch B — Precision & Rapier (T4) | AbilityEffectType.MeleeArc | AbilityEffectType.Projectile / AimDirection | Cost: 45 | CD: 7.5s | Dmg: 75 | Rad: 4.5 |
| `vampire_queen_active_20` | **Aristocratic Execution** | Branch B — Precision & Rapier (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc | AbilityEffectType.Heal / Self | Cost: 55 | CD: 9.5s | Dmg: 92 | Rad: 6.0 |
| `vampire_queen_active_21` | **Bat Swarm** | Branch C — Swarm & Dominance (T1) | AbilityEffectType.Summon | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl / NearestInvader | Cost: 20 | CD: 4.5s | Dmg: 30 | Rad: 3.5 |
| `vampire_queen_active_22` | **Blood Thrall** | Branch C — Swarm & Dominance (T1) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 25 | CD: 5.5s | Dmg: 34 | Rad: 3.0 |
| `vampire_queen_active_23` | **Leech Infestation** | Branch C — Swarm & Dominance (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 25 | CD: 5.0s | Dmg: 40 | Rad: 3.8 |
| `vampire_queen_active_24` | **Vampiric Command** | Branch C — Swarm & Dominance (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 30 | CD: 6.0s | Dmg: 42 | Rad: 5.0 |
| `vampire_queen_active_25` | **Shroud of Night** | Branch C — Swarm & Dominance (T2) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 30 | CD: 5.5s | Dmg: 45 | Rad: 4.0 |
| `vampire_queen_active_26` | **Dominion Choke** | Branch C — Swarm & Dominance (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Heal / NearestInvader | Cost: 35 | CD: 6.5s | Dmg: 54 | Rad: 3.5 |
| `vampire_queen_active_27` | **Hive Shriek** | Branch C — Swarm & Dominance (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 35 | CD: 6.0s | Dmg: 52 | Rad: 5.0 |
| `vampire_queen_active_28` | **Crimson Gargoyle** | Branch C — Swarm & Dominance (T3) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 40 | CD: 7.5s | Dmg: 62 | Rad: 4.0 |
| `vampire_queen_active_29` | **Apex Colony** | Branch C — Swarm & Dominance (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal / AreaCursorOrAim | Cost: 45 | CD: 8.5s | Dmg: 74 | Rad: 5.5 |
| `vampire_queen_active_30` | **Queen's Dominion** | Branch C — Swarm & Dominance (T4) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 55 | CD: 10.0s | Dmg: 90 | Rad: 6.5 |
| `vampire_queen_active_31` | **Crimson Mist** | Branch D — Mist & Domination (T1) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 20 | CD: 4.5s | Dmg: 32 | Rad: 3.5 |
| `vampire_queen_active_32` | **Hypnotic Gaze** | Branch D — Mist & Domination (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / NearestInvader | Cost: 25 | CD: 5.0s | Dmg: 38 | Rad: 3.0 |
| `vampire_queen_active_33` | **Noble Command** | Branch D — Mist & Domination (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 28 | CD: 5.5s | Dmg: 42 | Rad: 4.5 |
| `vampire_queen_active_34` | **Veil of Night** | Branch D — Mist & Domination (T2) | AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.Heal / AreaCursorOrAim | Cost: 30 | CD: 6.0s | Dmg: 45 | Rad: 4.5 |
| `vampire_queen_active_35` | **Blood Illusion** | Branch D — Mist & Domination (T2) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 32 | CD: 6.5s | Dmg: 48 | Rad: 3.5 |
| `vampire_queen_active_36` | **Phantom Waltz** | Branch D — Mist & Domination (T3) | AbilityEffectType.Dash | AbilityEffectType.RadialAoE / AimDirection | Cost: 35 | CD: 6.0s | Dmg: 54 | Rad: 3.5 |
| `vampire_queen_active_37` | **Scarlet Prison** | Branch D — Mist & Domination (T3) | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE / AreaCursorOrAim | Cost: 38 | CD: 7.0s | Dmg: 58 | Rad: 4.0 |
| `vampire_queen_active_38` | **Royal Decree** | Branch D — Mist & Domination (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 42 | CD: 7.5s | Dmg: 64 | Rad: 5.5 |
| `vampire_queen_active_39` | **Midnight Embrace** | Branch D — Mist & Domination (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 48 | CD: 8.5s | Dmg: 76 | Rad: 4.5 |
| `vampire_queen_active_40` | **Dark Ballroom** | Branch D — Mist & Domination (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.Hazard / Self | Cost: 55 | CD: 10.0s | Dmg: 92 | Rad: 6.5 |
| `vampire_queen_active_41` | **Blood Covenant** | Branch E — Forbidden Royal Blood Rituals (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 20 | CD: 4.5s | Dmg: 36 | Rad: 4.0 |
| `vampire_queen_active_42` | **Crimson Chalice** | Branch E — Forbidden Royal Blood Rituals (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal / Self | Cost: 25 | CD: 5.0s | Dmg: 40 | Rad: 4.0 |
| `vampire_queen_active_43` | **Scarlet Resurrection** | Branch E — Forbidden Royal Blood Rituals (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl / Self | Cost: 30 | CD: 6.0s | Dmg: 46 | Rad: 4.8 |
| `vampire_queen_active_44` | **Throne of Thorns** | Branch E — Forbidden Royal Blood Rituals (T2) | AbilityEffectType.Hazard | AbilityEffectType.RadialAoE / Self | Cost: 32 | CD: 6.0s | Dmg: 48 | Rad: 4.5 |
| `vampire_queen_active_45` | **Royal Feast** | Branch E — Forbidden Royal Blood Rituals (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal / Self | Cost: 35 | CD: 6.5s | Dmg: 52 | Rad: 5.0 |
| `vampire_queen_active_46` | **Queen's Masquerade** | Branch E — Forbidden Royal Blood Rituals (T3) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 40 | CD: 7.0s | Dmg: 60 | Rad: 4.5 |
| `vampire_queen_active_47` | **Blood Seraph** | Branch E — Forbidden Royal Blood Rituals (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / AimDirection | Cost: 42 | CD: 7.0s | Dmg: 65 | Rad: 4.5 |
| `vampire_queen_active_48` | **Sanguine Dominion** | Branch E — Forbidden Royal Blood Rituals (T3) | AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AreaCursorOrAim | Cost: 45 | CD: 8.0s | Dmg: 70 | Rad: 5.5 |
| `vampire_queen_active_49` | **Eternal Thirst** | Branch E — Forbidden Royal Blood Rituals (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal / Self | Cost: 50 | CD: 8.5s | Dmg: 82 | Rad: 5.0 |
| `vampire_queen_active_50` | **Crimson Coronation** | Branch E — Forbidden Royal Blood Rituals (T4) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield / Self | Cost: 60 | CD: 11.0s | Dmg: 105 | Rad: 7.0 |
| `vampire_queen_active_needle` | **Crimson Needle** | Branch B — Precision & Rapier (T1) | AbilityEffectType.Projectile / AimDirection | Cost: 18 | CD: 3s | Dmg: 28 | Rad: 0.8 |
| `vampire_queen_ultimate_50` | **Crimson Masquerade** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl / Self | Cost: 70 | CD: 25s | Dmg: 60 | Rad: 8 |
| `vampire_queen_ultimate_100` | **Blood Moon Ascension** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal / Self | Cost: 75 | CD: 28s | Dmg: 70 | Rad: 10 |
| `vampire_queen_ultimate_150` | **Sovereign Feast** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.Shield / Self | Cost: 80 | CD: 30s | Dmg: 85 | Rad: 8.5 |
| `vampire_queen_ultimate_200` | **Grand Vampire Court** | Milestone Ultimates (T5) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Heal / Self | Cost: 85 | CD: 32s | Dmg: 95 | Rad: 9 |
| `vampire_queen_ultimate_250` | **Apocalypse of Scarlet** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard / Self | Cost: 100 | CD: 35s | Dmg: 120 | Rad: 12 |

### Step-by-Step Prompts for Every Vampire Queen Skill
#### Skill 01: Sanguine Burst (`vampire_queen_active_01` — Branch A — Blood Sorcery, Tier 1)
- **Gameplay Context:** Detonate a crimson blood nova that damages all nearby enemies and siphons health back to the Queen. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal`, Radius: `4.5`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Sanguine Burst", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting detonate a crimson blood nova that damages all nearby enemies and siphons health back to the queen., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Sanguine Burst" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 02: Crimson Lance (`vampire_queen_active_02` — Branch A — Blood Sorcery, Tier 1)
- **Gameplay Context:** Condense vital blood into a high-velocity piercing spear that impales invaders in a straight line. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.Heal`, Radius: `1.5`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Lance", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting condense vital blood into a high-velocity piercing spear that impales invaders in a straight line., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Lance" (AbilityEffectType.Projectile | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 03: Blood Well (`vampire_queen_active_03` — Branch A — Blood Sorcery, Tier 2)
- **Gameplay Context:** Manifest a boiling pool of cursed blood on the arena floor that damages invaders and siphons their life force. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal`, Radius: `3.8`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blood Well", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting manifest a boiling pool of cursed blood on the arena floor that damages invaders and siphons their life force., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blood Well" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 04: Transfusion (`vampire_queen_active_04` — Branch A — Blood Sorcery, Tier 2)
- **Gameplay Context:** Channel a vicious vampiric tether to the closest invader, draining their vitality directly into your health pool. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.Heal | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE`, Radius: `3.0`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Transfusion", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting channel a vicious vampiric tether to the closest invader, draining their vitality directly into your health pool., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Transfusion" (AbilityEffectType.Heal | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (NearestInvader) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 05: Sanguine Ward (`vampire_queen_active_05` — Branch A — Blood Sorcery, Tier 2)
- **Gameplay Context:** Weave crystallized platelets into an impervious blood barrier that absorbs incoming attacks. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Sanguine Ward", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting weave crystallized platelets into an impervious blood barrier that absorbs incoming attacks., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Sanguine Ward" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 06: Blood Boil (`vampire_queen_active_06` — Branch A — Blood Sorcery, Tier 3)
- **Gameplay Context:** Overheat the blood inside surrounding invaders, causing violent internal hemorrhaging and staggering them. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Heal`, Radius: `4.8`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blood Boil", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting overheat the blood inside surrounding invaders, causing violent internal hemorrhaging and staggering them., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blood Boil" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 07: Exsanguinate (`vampire_queen_active_07` — Branch A — Blood Sorcery, Tier 3)
- **Gameplay Context:** Rip gallons of vital fluid from forward enemies, dealing massive damage and instantly restoring health. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Heal`, Radius: `4.0`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Exsanguinate", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting rip gallons of vital fluid from forward enemies, dealing massive damage and instantly restoring health., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Exsanguinate" (AbilityEffectType.MeleeArc | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 08: Crimson Tempest (`vampire_queen_active_08` — Branch A — Blood Sorcery, Tier 3)
- **Gameplay Context:** Unleash a howling vortex of blood-red wind that slices all enemies caught in its spinning wake. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Tempest", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting unleash a howling vortex of blood-red wind that slices all enemies caught in its spinning wake., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Tempest" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 09: Haemomancy (`vampire_queen_active_09` — Branch A — Blood Sorcery, Tier 4)
- **Gameplay Context:** Manipulate the blood pools of fallen foes to trigger secondary chained detonations across the arena. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `5.5`, Duration: `1.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Haemomancy", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting manipulate the blood pools of fallen foes to trigger secondary chained detonations across the arena., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Haemomancy" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 10: Heartstopper (`vampire_queen_active_10` — Branch A — Blood Sorcery, Tier 4)
- **Gameplay Context:** Crush the beating heart of an invader from within, releasing an apocalyptic crimson shockwave that executes weak targets. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl`, Radius: `6.0`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Heartstopper", Vampire Queen ability (Branch A — Blood Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting crush the beating heart of an invader from within, releasing an apocalyptic crimson shockwave that executes weak targets., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Heartstopper" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (NearestInvader) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 11: Rapier Thrust (`vampire_queen_active_11` — Branch B — Precision & Rapier, Tier 1)
- **Gameplay Context:** Execute a blindingly fast forward rapier thrust that pierces enemy defenses and inflicts bleeding. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Heal`, Radius: `3.5`, Duration: `0.2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rapier Thrust", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting execute a blindingly fast forward rapier thrust that pierces enemy defenses and inflicts bleeding., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rapier Thrust" (AbilityEffectType.MeleeArc | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 12: Crimson Needle (`vampire_queen_active_12` — Branch B — Precision & Rapier, Tier 1)
- **Gameplay Context:** Cast rapid piercing crystal blood needles that tear through lines of invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.Heal`, Radius: `0.8`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Needle", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting cast rapid piercing crystal blood needles that tear through lines of invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Needle" (AbilityEffectType.Projectile | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 13: Riposte (`vampire_queen_active_13` — Branch B — Precision & Rapier, Tier 2)
- **Gameplay Context:** Adopt a graceful defensive fencing guard, parrying incoming attacks and executing an instantaneous counter-thrust. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Shield`, Radius: `4.0`, Duration: `0.35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Riposte", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting adopt a graceful defensive fencing guard, parrying incoming attacks and executing an instantaneous counter-thrust., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Riposte" (AbilityEffectType.MeleeArc | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 14: Fencing Lunge (`vampire_queen_active_14` — Branch B — Precision & Rapier, Tier 2)
- **Gameplay Context:** Lunge forward with aristocratic speed, impaling all foes in your path and passing through their ranks. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.MeleeArc`, Radius: `3.0`, Duration: `0.25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Fencing Lunge", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting lunge forward with aristocratic speed, impaling all foes in your path and passing through their ranks., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Fencing Lunge" (AbilityEffectType.Dash | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 15: Needle Rain (`vampire_queen_active_15` — Branch B — Precision & Rapier, Tier 2)
- **Gameplay Context:** Launch a dense barrage of crystalline scarlet needles skyward that rains down upon targeted enemies. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `4.2`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Needle Rain", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting launch a dense barrage of crystalline scarlet needles skyward that rains down upon targeted enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Needle Rain" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 16: Arterial Flurry (`vampire_queen_active_16` — Branch B — Precision & Rapier, Tier 3)
- **Gameplay Context:** Unleash a rapid sequence of surgical rapier strikes that carve into enemy arteries with precision. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Heal`, Radius: `4.0`, Duration: `0.45s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Arterial Flurry", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting unleash a rapid sequence of surgical rapier strikes that carve into enemy arteries with precision., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Arterial Flurry" (AbilityEffectType.MeleeArc | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 17: Crystal Volley (`vampire_queen_active_17` — Branch B — Precision & Rapier, Tier 3)
- **Gameplay Context:** Conjure orbiting shards of crystallized blood and fire them simultaneously in a devastating spread. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.RadialAoE`, Radius: `2.0`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crystal Volley", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting conjure orbiting shards of crystallized blood and fire them simultaneously in a devastating spread., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crystal Volley" (AbilityEffectType.Projectile | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 18: Peerless Feint (`vampire_queen_active_18` — Branch B — Precision & Rapier, Tier 3)
- **Gameplay Context:** Perform a deceptive fencing feint, teleporting behind the target and striking with devastating fatal force. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `0.3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Peerless Feint", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting perform a deceptive fencing feint, teleporting behind the target and striking with devastating fatal force., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Peerless Feint" (AbilityEffectType.Dash | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 19: Grand Battement (`vampire_queen_active_19` — Branch B — Precision & Rapier, Tier 4)
- **Gameplay Context:** Perform an aristocratic sweeping rapier slash that launches a high-frequency blade wave slicing through the arena. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.Projectile`, Radius: `4.5`, Duration: `0.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Grand Battement", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting perform an aristocratic sweeping rapier slash that launches a high-frequency blade wave slicing through the arena., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Grand Battement" (AbilityEffectType.MeleeArc | AbilityEffectType.Projectile), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 20: Aristocratic Execution (`vampire_queen_active_20` — Branch B — Precision & Rapier, Tier 4)
- **Gameplay Context:** Channel the full refinement of vampiric nobility, executing an immaculate multi-target fencing assault that decimates all in range. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc | AbilityEffectType.Heal`, Radius: `6.0`, Duration: `0.6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Aristocratic Execution", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting channel the full refinement of vampiric nobility, executing an immaculate multi-target fencing assault that decimates all in range., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Aristocratic Execution" (AbilityEffectType.RadialAoE | AbilityEffectType.MeleeArc | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 21: Bat Swarm (`vampire_queen_active_21` — Branch C — Swarm & Dominance, Tier 1)
- **Gameplay Context:** Summon a ferocious swarm of spectral bats that harass enemies and hinder their movement. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.Summon | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Bat Swarm", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting summon a ferocious swarm of spectral bats that harass enemies and hinder their movement., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Bat Swarm" (AbilityEffectType.Summon | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (NearestInvader) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 22: Blood Thrall (`vampire_queen_active_22` — Branch C — Swarm & Dominance, Tier 1)
- **Gameplay Context:** Raise a loyal vampiric blood thrall from crimson essence to engage invaders and draw enemy aggression. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `3.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blood Thrall", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting raise a loyal vampiric blood thrall from crimson essence to engage invaders and draw enemy aggression., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blood Thrall" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 23: Leech Infestation (`vampire_queen_active_23` — Branch C — Swarm & Dominance, Tier 2)
- **Gameplay Context:** Release a crawling wave of crimson leeches that latch onto invaders, siphoning life and slowing their advance. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl`, Radius: `3.8`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Leech Infestation", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting release a crawling wave of crimson leeches that latch onto invaders, siphoning life and slowing their advance., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Leech Infestation" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 24: Vampiric Command (`vampire_queen_active_24` — Branch C — Swarm & Dominance, Tier 2)
- **Gameplay Context:** Exert imperial dominance over all summoned thralls and swarms, sending them into a berserk frenzy. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `5.0`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Vampiric Command", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting exert imperial dominance over all summoned thralls and swarms, sending them into a berserk frenzy., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Vampiric Command" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 25: Shroud of Night (`vampire_queen_active_25` — Branch C — Swarm & Dominance, Tier 2)
- **Gameplay Context:** Disperse into a nocturnal cloud of chittering bats, gaining evasive speed and continuously damaging encroaching foes. (Targeting: `Self`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.0`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Shroud of Night", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting disperse into a nocturnal cloud of chittering bats, gaining evasive speed and continuously damaging encroaching foes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Shroud of Night" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 26: Dominion Choke (`vampire_queen_active_26` — Branch C — Swarm & Dominance, Tier 3)
- **Gameplay Context:** Grip an invader with spectral telekinetic vampire claws, crushing their airway and slamming them into the stone. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Heal`, Radius: `3.5`, Duration: `1.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Dominion Choke", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting grip an invader with spectral telekinetic vampire claws, crushing their airway and slamming them into the stone., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Dominion Choke" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (NearestInvader) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 27: Hive Shriek (`vampire_queen_active_27` — Branch C — Swarm & Dominance, Tier 3)
- **Gameplay Context:** Emit an ultrasound screech that disorients and pushes all surrounding invaders backward, leaving them vulnerable. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.0`, Duration: `0.4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hive Shriek", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting emit an ultrasound screech that disorients and pushes all surrounding invaders backward, leaving them vulnerable., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hive Shriek" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 28: Crimson Gargoyle (`vampire_queen_active_28` — Branch C — Swarm & Dominance, Tier 3)
- **Gameplay Context:** Animate a gargoyle of hardened crystalline blood from dungeon masonry to crush foes with devastating aerial slams. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `4.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Gargoyle", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting animate a gargoyle of hardened crystalline blood from dungeon masonry to crush foes with devastating aerial slams., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Gargoyle" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 29: Apex Colony (`vampire_queen_active_29` — Branch C — Swarm & Dominance, Tier 4)
- **Gameplay Context:** Summon a massive vortex of thousands of carnivorous vampire bats that completely engulfs a targeted sector. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal`, Radius: `5.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Apex Colony", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting summon a massive vortex of thousands of carnivorous vampire bats that completely engulfs a targeted sector., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Apex Colony" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 30: Queen's Dominion (`vampire_queen_active_30` — Branch C — Swarm & Dominance, Tier 4)
- **Gameplay Context:** Proclaim ultimate nocturnal authority over the battlefield, summoning royal vanguard thralls and blasting all foes. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `6.5`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Queen's Dominion", Vampire Queen ability (Branch C — Swarm & Dominance), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting proclaim ultimate nocturnal authority over the battlefield, summoning royal vanguard thralls and blasting all foes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Queen's Dominion" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 31: Crimson Mist (`vampire_queen_active_31` — Branch D — Mist & Domination, Tier 1)
- **Gameplay Context:** Disperse into a blood-infused mist, granting movement speed, passing through foes, and damaging nearby invaders. (Targeting: `Self`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `3.5`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Mist", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting disperse into a blood-infused mist, granting movement speed, passing through foes, and damaging nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Mist" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 32: Hypnotic Gaze (`vampire_queen_active_32` — Branch D — Mist & Domination, Tier 1)
- **Gameplay Context:** Fix a piercing royal gaze upon invading foes, stunning them and forcing blood vessels to rupture. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.0`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hypnotic Gaze", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting fix a piercing royal gaze upon invading foes, stunning them and forcing blood vessels to rupture., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hypnotic Gaze" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (NearestInvader) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 33: Noble Command (`vampire_queen_active_33` — Branch D — Mist & Domination, Tier 2)
- **Gameplay Context:** Issue an aristocrat's absolute command, striking terror into surrounding foes and turning their momentum against each other. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `1.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Noble Command", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting issue an aristocrat's absolute command, striking terror into surrounding foes and turning their momentum against each other., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Noble Command" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 34: Veil of Night (`vampire_queen_active_34` — Branch D — Mist & Domination, Tier 2)
- **Gameplay Context:** Deploy a sprawling shroud of shadowy nocturnal fog that blinds invaders and continuously siphons their vital essence. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.Heal`, Radius: `4.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Veil of Night", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting deploy a sprawling shroud of shadowy nocturnal fog that blinds invaders and continuously siphons their vital essence., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Veil of Night" (AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 35: Blood Illusion (`vampire_queen_active_35` — Branch D — Mist & Domination, Tier 2)
- **Gameplay Context:** Conjure a crimson duplicate that taunts nearby invaders before violently detonating into crystalline shrapnel. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blood Illusion", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting conjure a crimson duplicate that taunts nearby invaders before violently detonating into crystalline shrapnel., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blood Illusion" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 36: Phantom Waltz (`vampire_queen_active_36` — Branch D — Mist & Domination, Tier 3)
- **Gameplay Context:** Perform a dizzying ballroom blink sequence, flashing across enemy ranks while delivering precise arterial strikes. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Dash | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Phantom Waltz", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting perform a dizzying ballroom blink sequence, flashing across enemy ranks while delivering precise arterial strikes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Phantom Waltz" (AbilityEffectType.Dash | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 37: Scarlet Prison (`vampire_queen_active_37` — Branch D — Mist & Domination, Tier 3)
- **Gameplay Context:** Encase target invaders in a cage of solidified coagulated blood, locking them in place while spikes impale them. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.Hazard | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE`, Radius: `4.0`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Scarlet Prison", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting encase target invaders in a cage of solidified coagulated blood, locking them in place while spikes impale them., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Scarlet Prison" (AbilityEffectType.Hazard | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 38: Royal Decree (`vampire_queen_active_38` — Branch D — Mist & Domination, Tier 3)
- **Gameplay Context:** Proclaim an imperial judgment across the ground, creating a zone of oppressive dominance that shatters enemy defenses. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.5`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Royal Decree", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting proclaim an imperial judgment across the ground, creating a zone of oppressive dominance that shatters enemy defenses., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Royal Decree" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 39: Midnight Embrace (`vampire_queen_active_39` — Branch D — Mist & Domination, Tier 4)
- **Gameplay Context:** Summon the suffocating chill of midnight to seize enemies in a shadowy embrace, dealing catastrophic damage and stealing life. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Midnight Embrace", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting summon the suffocating chill of midnight to seize enemies in a shadowy embrace, dealing catastrophic damage and stealing life., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Midnight Embrace" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 40: Dark Ballroom (`vampire_queen_active_40` — Branch D — Mist & Domination, Tier 4)
- **Gameplay Context:** Transform the battlefield into a spectral nocturnal ballroom: waves of dark mist surge, granting massive barriers and devastating all who oppose the Queen. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.Hazard`, Radius: `6.5`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Dark Ballroom", Vampire Queen ability (Branch D — Mist & Domination), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting transform the battlefield into a spectral nocturnal ballroom: waves of dark mist surge, granting massive barriers and devastating all who oppose the queen., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Dark Ballroom" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield | AbilityEffectType.Hazard), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 41: Blood Covenant (`vampire_queen_active_41` — Branch E — Forbidden Royal Blood Rituals, Tier 1)
- **Gameplay Context:** Form an ancient forbidden pact, sacrificing a fraction of life to unleash a searing crimson wave and erect an enduring barrier. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.0`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blood Covenant", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting form an ancient forbidden pact, sacrificing a fraction of life to unleash a searing crimson wave and erect an enduring barrier., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blood Covenant" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 42: Crimson Chalice (`vampire_queen_active_42` — Branch E — Forbidden Royal Blood Rituals, Tier 1)
- **Gameplay Context:** Drink from the consecrated royal chalice, recovering vital health, emitting a cleansing pulse, and damaging nearby invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal`, Radius: `4.0`, Duration: `1.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Chalice", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting drink from the consecrated royal chalice, recovering vital health, emitting a cleansing pulse, and damaging nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Chalice" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 43: Scarlet Resurrection (`vampire_queen_active_43` — Branch E — Forbidden Royal Blood Rituals, Tier 2)
- **Gameplay Context:** Invoke primordial ancestral bloodlines to blast encroaching invaders backward and trigger powerful cellular rejuvenation. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl`, Radius: `4.8`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Scarlet Resurrection", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting invoke primordial ancestral bloodlines to blast encroaching invaders backward and trigger powerful cellular rejuvenation., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Scarlet Resurrection" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 44: Throne of Thorns (`vampire_queen_active_44` — Branch E — Forbidden Royal Blood Rituals, Tier 2)
- **Gameplay Context:** Raise a jagged perimeter of calcified blood thorn pillars that continuously impale and shred advancing invaders. (Targeting: `Self`, Effect: `AbilityEffectType.Hazard | AbilityEffectType.RadialAoE`, Radius: `4.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Throne of Thorns", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting raise a jagged perimeter of calcified blood thorn pillars that continuously impale and shred advancing invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Throne of Thorns" (AbilityEffectType.Hazard | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 45: Royal Feast (`vampire_queen_active_45` — Branch E — Forbidden Royal Blood Rituals, Tier 2)
- **Gameplay Context:** Channel imperial blood dominance to violently siphon life force from surrounding invaders, gorging on their vitality. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal`, Radius: `5.0`, Duration: `1.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Royal Feast", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting channel imperial blood dominance to violently siphon life force from surrounding invaders, gorging on their vitality., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Royal Feast" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 46: Queen's Masquerade (`vampire_queen_active_46` — Branch E — Forbidden Royal Blood Rituals, Tier 3)
- **Gameplay Context:** Unleash a flurry of dancing spectral masquerade illusions that swirl across the battlefield, distracting and slicing invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `4.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Queen's Masquerade", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting unleash a flurry of dancing spectral masquerade illusions that swirl across the battlefield, distracting and slicing invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Queen's Masquerade" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 47: Blood Seraph (`vampire_queen_active_47` — Branch E — Forbidden Royal Blood Rituals, Tier 3)
- **Gameplay Context:** Manifest monumental wings of razor-sharp crystalline blood, sweeping forward to cleave enemy formations. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `4.5`, Duration: `1.2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blood Seraph", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting manifest monumental wings of razor-sharp crystalline blood, sweeping forward to cleave enemy formations., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blood Seraph" (AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 48: Sanguine Dominion (`vampire_queen_active_48` — Branch E — Forbidden Royal Blood Rituals, Tier 3)
- **Gameplay Context:** Claim complete sovereignty over the earth, saturating the floor with blood energy that slows invaders and strengthens blood magic. (Targeting: `AreaCursorOrAim`, Effect: `AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `5.5`, Duration: `4.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Sanguine Dominion", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting claim complete sovereignty over the earth, saturating the floor with blood energy that slows invaders and strengthens blood magic., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Sanguine Dominion" (AbilityEffectType.Hazard | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AreaCursorOrAim) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 49: Eternal Thirst (`vampire_queen_active_49` — Branch E — Forbidden Royal Blood Rituals, Tier 4)
- **Gameplay Context:** Awaken an all-consuming predatory frenzy: razor crimson claws shred all nearby invaders with extreme lifesteal. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal`, Radius: `5.0`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Eternal Thirst", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting awaken an all-consuming predatory frenzy: razor crimson claws shred all nearby invaders with extreme lifesteal., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Eternal Thirst" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 50: Crimson Coronation (`vampire_queen_active_50` — Branch E — Forbidden Royal Blood Rituals, Tier 4)
- **Gameplay Context:** Ascend to the absolute pinnacle of vampiric royalty: unleash a catastrophic scarlet cataclysm, summon elite guardians, and erect an impenetrable shield. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield`, Radius: `7.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Coronation", Vampire Queen ability (Branch E — Forbidden Royal Blood Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting ascend to the absolute pinnacle of vampiric royalty: unleash a catastrophic scarlet cataclysm, summon elite guardians, and erect an impenetrable shield., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Coronation" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 51: Crimson Needle (`vampire_queen_active_needle` — Branch B — Precision & Rapier, Tier 1)
- **Gameplay Context:** Cast rapid piercing crystal blood needles that tear through lines of invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile`, Radius: `0.8`, Duration: `0.8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Needle", Vampire Queen ability (Branch B — Precision & Rapier), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting cast rapid piercing crystal blood needles that tear through lines of invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Needle" (AbilityEffectType.Projectile), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (AimDirection) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 52: Crimson Masquerade (`vampire_queen_ultimate_50` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Unleash an arena-wide vampiric siphon that drains blood from all invading heroes. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl`, Radius: `8`, Duration: `1s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crimson Masquerade", Vampire Queen ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting unleash an arena-wide vampiric siphon that drains blood from all invading heroes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crimson Masquerade" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 53: Blood Moon Ascension (`vampire_queen_ultimate_100` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Summon a celestial Blood Moon overhead, bathing the dungeon in crimson light that constantly siphons life from all enemies. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal`, Radius: `10`, Duration: `6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Blood Moon Ascension", Vampire Queen ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting summon a celestial blood moon overhead, bathing the dungeon in crimson light that constantly siphons life from all enemies., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Blood Moon Ascension" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 54: Sovereign Feast (`vampire_queen_ultimate_150` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Transform into an unstoppable mist avatar of royal blood, consuming all enemies within reach and gaining total invulnerability. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.Shield`, Radius: `8.5`, Duration: `3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Sovereign Feast", Vampire Queen ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting transform into an unstoppable mist avatar of royal blood, consuming all enemies within reach and gaining total invulnerability., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Sovereign Feast" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.Shield), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 55: Grand Vampire Court (`vampire_queen_ultimate_200` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Summon the ancient Vampire Aristocracy to take the field, commanding spectral royal thralls to slaughter encroaching invaders. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Heal`, Radius: `9`, Duration: `8s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Grand Vampire Court", Vampire Queen ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting summon the ancient vampire aristocracy to take the field, commanding spectral royal thralls to slaughter encroaching invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Grand Vampire Court" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.Heal), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 56: Apocalypse of Scarlet (`vampire_queen_ultimate_250` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Rupture all blood vessels in the dungeon realm, causing every invader to violently combust in a scarlet cataclysm. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard`, Radius: `12`, Duration: `2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Apocalypse of Scarlet", Vampire Queen ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #D91A4D and glowing #FF6680 highlights, depicting rupture all blood vessels in the dungeon realm, causing every invader to violently combust in a scarlet cataclysm., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Apocalypse of Scarlet" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal | AbilityEffectType.CrowdControl | AbilityEffectType.Hazard), frame 1: arcane cast flash on Vampire Queen hands in #FF6680, frame 2: active delivery (Self) with #D91A4D core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`

---

## 5. Unity Import, Slicing & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Filter / Format | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Character Sheet** | `Assets/_Game/Art/Characters/vampire_queen/char_vampire_queen_sheet.png` | `192x32` (or `384x32`) | `32` | `(0.5, 0.1)` | `Point`, `RGBA32` | `LichLordVisualPresenter._bodyRenderer` (`PlaceholderSprites.Circle` tinted `#D91A4D`) |
| **Portrait Icon** | `Assets/_Game/Art/UI/Portraits/portrait_vampire_queen.png` | `48x48` | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `CharacterSelectPresenter` class card color block |
| **Basic Attack VFX** | `Assets/_Game/Art/VFX/Characters/vfx_vampire_queen_basic.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `ProjectileView` / `AttackTelegraphPresenter` (`PlaceholderSprites.Circle`) |
| **Signature VFX** | `Assets/_Game/Art/VFX/Characters/vfx_vampire_queen_signature.png` | `256x64` (`4x 64x64`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `DeathWaveVfxPresenter` expanding ring placeholder |
| **Skill Icons (56x)** | `Assets/_Game/Art/UI/Skills/vampire_queen/icon_<ability_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `SkillTreeScreenPresenter` & `HudPresenter` slot squares |
