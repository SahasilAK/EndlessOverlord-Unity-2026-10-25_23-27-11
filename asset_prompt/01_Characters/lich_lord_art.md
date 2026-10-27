# Lich Lord (`lich_lord`) — Complete Art & Animation Prompt Specification

> **Asset Category:** Playable Boss Overlord (`01_Characters`)
> **Catalog ID:** `CharacterCatalog.LichLordId` (`"lich_lord"`)
> **Class Title:** *Arch-Necromancer of the Eternal Crypt*
> **Combat Profile:** HP: 100 | Armor: 5 | MoveSpeed: 4.5 | DashSpeed: 12.0 (0.2s, 3.0s CD) | **Resource:** Soul Essence (Max 100, +6/s regen, +12 per kill)
> **Starting Weapon:** Soul Staff (`wpn_stf_soul_staff` — ranged necrotic bolts, Range 12.0, Damage 15.0)
> **Signature Ability:** Bone Sentinel (`lich_lord_active_01`) & Death Wave Nova (Radius 3.0, Damage 35.0)

---

## 1. Base Character Design & Master Visual Prompt

### 1.1 Color Palette & Pixel Rules
| Swatch Role | Hex Code | RGB Float (`CharacterCatalog`) | Usage |
| :--- | :--- | :--- | :--- |
| **Outer Rim Outline** | `#110E1B` | `(0.067, 0.055, 0.106)` | Mandatory 1px dark silhouette border around all opaque pixels |
| **Primary Class Color** | `#6E2BC4` | Matches `CharacterCatalog` RGB | Primary robes/armor/mantle body mass (60% of sprite area) |
| **Secondary Energy / Glow** | `#30E680` | High-value emissive | Eyes, weapon core, spell casting hands, signature aura (20% of sprite area) |
| **Trim / Bone / Metal** | `#D8D2C5` | Neutral contrast | Crown, pauldrons, weapon shaft, structural trim (15% of sprite area) |
| **Deep Shadow** | `#1E142B` | recessed folds | Interior cloak folds, under-chin shadow, lower hem (5% of sprite area) |

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle Reference)
```text
Masterpiece 2D top-down 3/4 view pixel art character sprite of Lich Lord (Arch-Necromancer of the Eternal Crypt), playable final boss in a dark fantasy survival roguelite.
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, zero motion blur, 1px solid dark outline (#110E1B).
Silhouette & Design: Tall gaunt skeletal arch-monarch draped in tattered royal purple-violet vestments (`#6E2BC4`), high spiked bone collar and floating necrotic shoulder pauldrons (`#D8D2C5`), jagged dark-iron crown hovering 1px above skull, glowing emerald-mint eye sockets (`#30E680`), right hand gripping a gnarled Soul Staff topped with a levitating green skull-lantern, lower robe fraying into levitating ghostly wisps (no feet touching ground).
Color Palette: strictly constrained to 5-6 ramps centered on Primary #6E2BC4, Emissive #30E680, Trim #D8D2C5, and Shadow #1E142B.
Lighting: zenithal top-left light source; head, crown, shoulders, and weapon tip receive brightest value ramp; lower body drops into #1E142B shadow.
Foot Anchor: character feet/lower robe hem centered at X=16, Y=3 from bottom edge (Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step Character Sprite Sheet Prompts (`char_lich_lord_sheet.png`)

To ensure **100% visual consistency** across all states, generate or assemble a **12-Frame Master Character Strip (`384x32 px` total, 12 frames of `32x32 px`)** or **2 rows of 6 frames (`192x64 px`)** using Frame 1 as the locked style/character reference.

### Row 1: Core Locomotion & Combat Strip (`192x32 px` — Required by `PIXEL_ART_STYLE_GUIDE.md`)

- **Frame 01 (`X: 0..31, Y: 0..31`) — `Idle_01` (Base Stance):**
  > `Same 32x32 pixel art character Lich Lord from Base Prompt. Neutral commanding boss stance, facing South-East (3/4 top-down), weapon held at ready on right side (X:22..29), left hand glowing with 2px #30E680 energy ember, silhouette fits within 26x28 px centered box.`
- **Frame 02 (`X: 32..63, Y: 0..31`) — `Walk_01` (Left Stride / Hover Up):**
  > `Consistent 32x32 pixel art of Lich Lord. Hover-glide animation: robe hem ripples in a 2-frame sine wave, torso bobs 1px vertically, green soul-lantern leaves a 2px trailing ember. Torso shifted 1px up (Y+1), left shoulder forward 1px, weapon trails 1px back, mantle/cloak flares 2px to the left.`
- **Frame 03 (`X: 64..95, Y: 0..31`) — `Walk_02` (Right Stride / Hover Down):**
  > `Consistent 32x32 pixel art of Lich Lord. Opposite phase of walk cycle: torso settles 1px down, right weapon shoulder surges forward 1px, lower cloak ripples right, 2px #30E680 particle detaches from hem.`
- **Frame 04 (`X: 96..127, Y: 0..31`) — `Attack_Cast_01` (Primary Strike / Spell Release):**
  > `Consistent 32x32 pixel art of Lich Lord. Raises Soul Staff forward at 45° angle; staff skull jaw unhinges and flashes `#30E680`, releasing a `16x16` Soul Bolt projectile. Weapon and casting hand thrust forward toward right edge (X:24..31), eyes and weapon core flare pure white #FFFFFF with #30E680 2px halo, dynamic forward lean.`
- **Frame 05 (`X: 128..159, Y: 0..31`) — `Hit_React_01` (Damaged Recoil):**
  > `Consistent 32x32 pixel art of Lich Lord. Knocked backward 2px to the left, torso arched back, arms flung wide defensively, 40% of primary #6E2BC4 pixels shifted toward high-contrast white/red hit flash (#FF5555 / #FFFFFF), 3 sharp impact sparks around chest.`
- **Frame 06 (`X: 160..191, Y: 0..31`) — `Death_01` (Defeat Collapse / Dissolve):**
  > `Consistent 32x32 pixel art of Lich Lord. Weapon dropped horizontally at Y:2..5, crown/helm cracked on ground, body collapsing into a swirling vortex of #6E2BC4 ash and ascending #30E680 soul embers.`

### Row 2: Extended Boss Action States (`192x32 px` — Optional Polish Strip)

- **Frame 07 — `Idle_02` (Breathing / Aura Pulse):** Chest/shoulders rise 1px, `#30E680` eye/core glow expands by 1px.
- **Frame 08 — `Dash_Phase` (High-Speed Evasion):** Compressed horizontal silhouette tilted 25° forward, trailing 3 semi-transparent `#6E2BC4` motion lines.
- **Frame 09 — `Signature_Windup` (Channeling Ultimate):** Both arms raised overhead, levitating 3px off floor, surrounded by converging `#30E680` runes.
- **Frame 10 — `Signature_Release` (Nova Eruption):** Slams staff butt into ground; 360° emerald necrotic shockwave ring expands (`64x64 px`) while a skeletal hand claws upward to spawn a Bone Sentinel.
- **Frame 11 — `Summon_Command` (Raising Minions / Totems):** Left hand extended palm-down over a glowing ground sigil (`#30E680`).
- **Frame 12 — `Death_02` (Final Remnant):** Collapsed pile of `#D8D2C5` armor/robes with a single flickering `#30E680` ember at center.

---

## 3. Basic Attack, Starting Weapon & Signature VFX Prompts

### 3.1 Basic Attack Projectile / Swing Strip (`vfx_lich_lord_basic_attack.png` — `48x16 px`, 3 frames of `16x16`)
```text
3-frame horizontal pixel art sprite strip (48x16 px total, three 16x16 frames), transparent background, 1px dark rim (#110E1B) on opaque cores, FilterMode.Point.
Subject: Lich Lord basic attack effect (Soul Staff (`wpn_stf_soul_staff` — ranged necrotic bolts, Range 12.0, Damage 15.0)).
- Frame 1 (0..15): Muzzle/hand cast flash — 10x10 px star-burst in #FFFFFF core and #30E680 outer spikes.
- Frame 2 (16..31): Active traveling projectile or melee slash arc — 14x12 px directional crescent/bolt facing Right (+X), #FFFFFF hot core, #30E680 mid-body, #6E2BC4 trailing tail.
- Frame 3 (32..47): Impact shatter — 15x15 px radial burst of 6 pixel shards in #30E680 and #6E2BC4.
```

### 3.2 Signature Nova / Ultimate Eruption (`vfx_lich_lord_signature_nova.png` — `256x64 px`, 4 frames of `64x64`)
```text
4-frame horizontal pixel art AoE explosion strip (256x64 px total, four 64x64 frames), top-down perspective, transparent background, crisp pixel art.
Subject: Bone Sentinel (`lich_lord_active_01`) & Death Wave Nova (Radius 3.0, Damage 35.0) (Slams staff butt into ground; 360° emerald necrotic shockwave ring expands (`64x64 px`) while a skeletal hand claws upward to spawn a Bone Sentinel.).
- Frame 1 (64x64): Ground telegraph ring (radius 28px) inscribed with glowing #30E680 runes.
- Frame 2 (64x64): Initial eruption — dense central pillar/wave of #FFFFFF and #30E680 expanding to radius 18px.
- Frame 3 (64x64): Peak shockwave — outer jagged ring at radius 30px in #6E2BC4 and #30E680, hollow center with ground cracks.
- Frame 4 (64x64): Dissipating embers and smoke rings fading into #1E142B particles.
```

---

## 4. Complete Class Skill Tree Assets (55 Abilities from `AbilityRegistry.cs`)

Below are all **55 class abilities** registered for **Lich Lord** across its 4 skill tree branches, with exact icon and VFX generation prompts.

| Ability ID | Name | Branch & Tier | Effect / Targeting | Cost | Cooldown | Base Dmg | Radius |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| `lich_lord_active_01` | **Bone Sentinel** | Branch A — Undead Summoning (T1) | AbilityEffectType.Summon / AimDirection | Cost: 25 | CD: 8s | Dmg: 14 | Rad: 1 |
| `lich_lord_active_02` | **Crypt Archer** | Branch A — Undead Summoning (T2) | AbilityEffectType.Summon | AbilityEffectType.Projectile / AimDirection | Cost: 25 | CD: 10s | Dmg: 12 | Rad: 1 |
| `lich_lord_active_03` | **Grave Spearman** | Branch A — Undead Summoning (T2) | AbilityEffectType.Summon | AbilityEffectType.MeleeArc / AimDirection | Cost: 30 | CD: 9s | Dmg: 18 | Rad: 1.5 |
| `lich_lord_active_04` | **Grave Hound** | Branch A — Undead Summoning (T3) | AbilityEffectType.Summon / AimDirection | Cost: 30 | CD: 7s | Dmg: 10 | Rad: 0.8 |
| `lich_lord_active_05` | **Bone Mage** | Branch A — Undead Summoning (T3) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 35 | CD: 10s | Dmg: 16 | Rad: 1.5 |
| `lich_lord_active_06` | **Ossuary Guardian** | Branch A — Undead Summoning (T4) | AbilityEffectType.Summon | AbilityEffectType.Shield / AimDirection | Cost: 40 | CD: 12s | Dmg: 22 | Rad: 1.2 |
| `lich_lord_active_07` | **Wight Captain** | Branch A — Undead Summoning (T4) | AbilityEffectType.Summon / AimDirection | Cost: 45 | CD: 14s | Dmg: 20 | Rad: 4.5 |
| `lich_lord_active_08` | **Corpse Collector** | Branch A — Undead Summoning (T4) | AbilityEffectType.Summon / AimDirection | Cost: 30 | CD: 11s | Dmg: 8 | Rad: 5 |
| `lich_lord_active_09` | **Dread Abomination** | Branch A — Undead Summoning (T5) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 55 | CD: 18s | Dmg: 35 | Rad: 3.5 |
| `lich_lord_active_10` | **Skeletal Dragon** | Branch A — Undead Summoning (T5) | AbilityEffectType.Summon | AbilityEffectType.MeleeArc / AimDirection | Cost: 70 | CD: 22s | Dmg: 45 | Rad: 5 |
| `lich_lord_active_11` | **Grave Frost** | Branch B — Frost and Death Magic (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 15 | CD: 5s | Dmg: 22 | Rad: 4.5 |
| `lich_lord_active_12` | **Permafrost Sigil** | Branch B — Frost and Death Magic (T2) | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / AimDirection | Cost: 25 | CD: 8s | Dmg: 12 | Rad: 3 |
| `lich_lord_active_13` | **Chill Lance** | Branch B — Frost and Death Magic (T2) | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl / AimDirection | Cost: 20 | CD: 4s | Dmg: 32 | Rad: 0.25 |
| `lich_lord_active_14` | **Wailing Blizzard** | Branch B — Frost and Death Magic (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / AimDirection | Cost: 35 | CD: 9s | Dmg: 18 | Rad: 3.5 |
| `lich_lord_active_15` | **Glacial Tomb** | Branch B — Frost and Death Magic (T3) | AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE / NearestInvader | Cost: 30 | CD: 8s | Dmg: 28 | Rad: 2 |
| `lich_lord_active_16` | **Shiver Pulse** | Branch B — Frost and Death Magic (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 25 | CD: 6s | Dmg: 26 | Rad: 6 |
| `lich_lord_active_17` | **Rime Chain** | Branch B — Frost and Death Magic (T4) | AbilityEffectType.Projectile | AbilityEffectType.CrowdControl / NearestInvader | Cost: 35 | CD: 7s | Dmg: 24 | Rad: 4 |
| `lich_lord_active_18` | **Frozen Lantern** | Branch B — Frost and Death Magic (T4) | AbilityEffectType.Summon | AbilityEffectType.Shield | AbilityEffectType.Projectile / Self | Cost: 30 | CD: 12s | Dmg: 14 | Rad: 1.6 |
| `lich_lord_active_19` | **Hail Crown** | Branch B — Frost and Death Magic (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / Self | Cost: 50 | CD: 14s | Dmg: 40 | Rad: 6 |
| `lich_lord_active_20` | **Winter of the Dead** | Branch B — Frost and Death Magic (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Summon / Self | Cost: 65 | CD: 20s | Dmg: 50 | Rad: 12 |
| `lich_lord_active_21` | **Witchfire Orb** | Branch C — Fire, Wind and Thunder Sorcery (T1) | AbilityEffectType.Projectile | AbilityEffectType.RadialAoE / AimDirection | Cost: 15 | CD: 2.2s | Dmg: 22 | Rad: 1.5 |
| `lich_lord_active_22` | **Graveflame Pillar** | Branch C — Fire, Wind and Thunder Sorcery (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 24 | CD: 4.5s | Dmg: 38 | Rad: 2.2 |
| `lich_lord_active_23` | **Ashen Gust** | Branch C — Fire, Wind and Thunder Sorcery (T2) | AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl / AimDirection | Cost: 18 | CD: 4.0s | Dmg: 20 | Rad: 5.5 |
| `lich_lord_active_24` | **Static Cadaver** | Branch C — Fire, Wind and Thunder Sorcery (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 26 | CD: 6.0s | Dmg: 32 | Rad: 2.8 |
| `lich_lord_active_25` | **Deathbolt** | Branch C — Fire, Wind and Thunder Sorcery (T3) | AbilityEffectType.Projectile | AbilityEffectType.RadialAoE / AimDirection | Cost: 25 | CD: 3.5s | Dmg: 45 | Rad: 0.35 |
| `lich_lord_active_26` | **Corpse Spark** | Branch C — Fire, Wind and Thunder Sorcery (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Projectile / NearestInvader | Cost: 30 | CD: 7.0s | Dmg: 34 | Rad: 1.5 |
| `lich_lord_active_27` | **Storm of Ash** | Branch C — Fire, Wind and Thunder Sorcery (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 36 | CD: 8.5s | Dmg: 40 | Rad: 3.5 |
| `lich_lord_active_28` | **Phantom Cyclone** | Branch C — Fire, Wind and Thunder Sorcery (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Gravity | AbilityEffectType.Hazard / AimDirection | Cost: 38 | CD: 10.0s | Dmg: 42 | Rad: 3.2 |
| `lich_lord_active_29` | **Ember Ossuary** | Branch C — Fire, Wind and Thunder Sorcery (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / Self | Cost: 52 | CD: 13.0s | Dmg: 55 | Rad: 4.5 |
| `lich_lord_active_30` | **Thunder Coffin** | Branch C — Fire, Wind and Thunder Sorcery (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / NearestInvader | Cost: 65 | CD: 16.0s | Dmg: 70 | Rad: 3.5 |
| `lich_lord_active_31` | **Soul Siphon** | Branch D — Forbidden Dark Magic (T1) | AbilityEffectType.RadialAoE | AbilityEffectType.Heal / AimDirection | Cost: 16 | CD: 3.0s | Dmg: 22 | Rad: 2.5 |
| `lich_lord_active_32` | **Hex of Frailty** | Branch D — Forbidden Dark Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 20 | CD: 4.5s | Dmg: 24 | Rad: 3.0 |
| `lich_lord_active_33` | **Withering Field** | Branch D — Forbidden Dark Magic (T2) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 24 | CD: 5.0s | Dmg: 30 | Rad: 3.0 |
| `lich_lord_active_34` | **Black Sun** | Branch D — Forbidden Dark Magic (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Gravity | AbilityEffectType.Hazard / AimDirection | Cost: 30 | CD: 7.0s | Dmg: 38 | Rad: 3.5 |
| `lich_lord_active_35` | **Spirit Leech** | Branch D — Forbidden Dark Magic (T3) | AbilityEffectType.Projectile | AbilityEffectType.Heal / NearestInvader | Cost: 25 | CD: 5.5s | Dmg: 35 | Rad: 0.3 |
| `lich_lord_active_36` | **Doom Bind** | Branch D — Forbidden Dark Magic (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / AimDirection | Cost: 32 | CD: 7.5s | Dmg: 36 | Rad: 3.2 |
| `lich_lord_active_37` | **Ebon Grasp** | Branch D — Forbidden Dark Magic (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl / AimDirection | Cost: 34 | CD: 8.0s | Dmg: 40 | Rad: 3.5 |
| `lich_lord_active_38` | **Curse Spread** | Branch D — Forbidden Dark Magic (T4) | AbilityEffectType.RadialAoE | AbilityEffectType.Projectile / NearestInvader | Cost: 30 | CD: 6.5s | Dmg: 32 | Rad: 2.0 |
| `lich_lord_active_39` | **Death Sentence** | Branch D — Forbidden Dark Magic (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / NearestInvader | Cost: 48 | CD: 12.0s | Dmg: 65 | Rad: 2.5 |
| `lich_lord_active_40` | **Umbral Nova** | Branch D — Forbidden Dark Magic (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 60 | CD: 15.0s | Dmg: 75 | Rad: 8.0 |
| `lich_lord_active_41` | **Reanimate Fallen** | Branch E — Necromantic Rituals (T1) | AbilityEffectType.Summon | AbilityEffectType.Heal / NearestInvader | Cost: 20 | CD: 4.5s | Dmg: 20 | Rad: 1.5 |
| `lich_lord_active_42` | **Tomb Relay** | Branch E — Necromantic Rituals (T2) | AbilityEffectType.RadialAoE / AimDirection | Cost: 18 | CD: 5.0s | Dmg: 25 | Rad: 2.5 |
| `lich_lord_active_43` | **Legion Formation** | Branch E — Necromantic Rituals (T2) | AbilityEffectType.Heal / Self | Cost: 22 | CD: 7.0s | Dmg: 0 | Rad: 6.0 |
| `lich_lord_active_44` | **Soul Furnace** | Branch E — Necromantic Rituals (T3) | AbilityEffectType.RadialAoE | AbilityEffectType.Hazard / AimDirection | Cost: 35 | CD: 6.5s | Dmg: 45 | Rad: 3.5 |
| `lich_lord_active_45` | **Boneshield** | Branch E — Necromantic Rituals (T3) | AbilityEffectType.Shield | AbilityEffectType.RadialAoE / Self | Cost: 28 | CD: 8.0s | Dmg: 18 | Rad: 2.2 |
| `lich_lord_active_46` | **Corpse Explosion** | Branch E — Necromantic Rituals (T3) | AbilityEffectType.RadialAoE / NearestInvader | Cost: 30 | CD: 5.5s | Dmg: 52 | Rad: 3.8 |
| `lich_lord_active_47` | **Soul Anchor** | Branch E — Necromantic Rituals (T4) | AbilityEffectType.Heal | AbilityEffectType.Hazard / AimDirection | Cost: 36 | CD: 9.0s | Dmg: 15 | Rad: 3.2 |
| `lich_lord_active_48` | **Death Pact** | Branch E — Necromantic Rituals (T4) | AbilityEffectType.Heal | AbilityEffectType.RadialAoE / Self | Cost: 15 | CD: 6.0s | Dmg: 30 | Rad: 3.5 |
| `lich_lord_active_49` | **Necropolis Gate** | Branch E — Necromantic Rituals (T4) | AbilityEffectType.Summon | AbilityEffectType.Hazard / AimDirection | Cost: 45 | CD: 11.0s | Dmg: 22 | Rad: 3.0 |
| `lich_lord_active_50` | **Crown of Silence** | Branch E — Necromantic Rituals (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 55 | CD: 14.0s | Dmg: 68 | Rad: 7.5 |
| `lich_lord_ultimate_50` | **Army of the Damned** | Milestone Ultimates (T5) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / Self | Cost: 60 | CD: 25s | Dmg: 60 | Rad: 6 |
| `lich_lord_ultimate_100` | **Ossuary Dragon** | Milestone Ultimates (T5) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / AimDirection | Cost: 75 | CD: 30s | Dmg: 80 | Rad: 6.5 |
| `lich_lord_ultimate_150` | **Eternal Winter Court** | Milestone Ultimates (T5) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 75 | CD: 30s | Dmg: 70 | Rad: 7.5 |
| `lich_lord_ultimate_200` | **Black Star Eclipse** | Milestone Ultimates (T5) | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl / Self | Cost: 80 | CD: 32s | Dmg: 110 | Rad: 8.5 |
| `lich_lord_ultimate_250` | **World Grave Awakening** | Milestone Ultimates (T5) | AbilityEffectType.Summon | AbilityEffectType.RadialAoE / Self | Cost: 90 | CD: 35s | Dmg: 90 | Rad: 9 |

### Step-by-Step Prompts for Every Lich Lord Skill
#### Skill 01: Bone Sentinel (`lich_lord_active_01` — Branch A — Undead Summoning, Tier 1)
- **Gameplay Context:** Summon a defensive skeleton swordsman that guards you and engages nearby invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon`, Radius: `1`, Duration: `20s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Bone Sentinel", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a defensive skeleton swordsman that guards you and engages nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Bone Sentinel" (AbilityEffectType.Summon), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 02: Crypt Archer (`lich_lord_active_02` — Branch A — Undead Summoning, Tier 2)
- **Gameplay Context:** Summon an undead ranged archer that lofts piercing bone arrows at distant targets. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.Projectile`, Radius: `1`, Duration: `18s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crypt Archer", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon an undead ranged archer that lofts piercing bone arrows at distant targets., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crypt Archer" (AbilityEffectType.Summon | AbilityEffectType.Projectile), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 03: Grave Spearman (`lich_lord_active_03` — Branch A — Undead Summoning, Tier 2)
- **Gameplay Context:** Summon a piercing melee defender that impales invaders in a line and knocks them back. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.MeleeArc`, Radius: `1.5`, Duration: `20s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Grave Spearman", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a piercing melee defender that impales invaders in a line and knocks them back., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Grave Spearman" (AbilityEffectType.Summon | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 04: Grave Hound (`lich_lord_active_04` — Branch A — Undead Summoning, Tier 3)
- **Gameplay Context:** Summon a rapid undead hunter that sprints swiftly toward distant threats and tears into invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon`, Radius: `0.8`, Duration: `18s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Grave Hound", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a rapid undead hunter that sprints swiftly toward distant threats and tears into invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Grave Hound" (AbilityEffectType.Summon), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 05: Bone Mage (`lich_lord_active_05` — Branch A — Undead Summoning, Tier 3)
- **Gameplay Context:** Summon an elemental skeleton caster that casts concentrated bolts of dark elemental magic. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `1.5`, Duration: `20s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Bone Mage", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon an elemental skeleton caster that casts concentrated bolts of dark elemental magic., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Bone Mage" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 06: Ossuary Guardian (`lich_lord_active_06` — Branch A — Undead Summoning, Tier 4)
- **Gameplay Context:** Summon a colossal armored skeleton defender that absorbs damage and intercepts enemy strikes. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.Shield`, Radius: `1.2`, Duration: `24s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ossuary Guardian", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a colossal armored skeleton defender that absorbs damage and intercepts enemy strikes., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ossuary Guardian" (AbilityEffectType.Summon | AbilityEffectType.Shield), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 07: Wight Captain (`lich_lord_active_07` — Branch A — Undead Summoning, Tier 4)
- **Gameplay Context:** Summon a dread undead commander whose authoritative presence buffs all nearby allied summons. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon`, Radius: `4.5`, Duration: `25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Wight Captain", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a dread undead commander whose authoritative presence buffs all nearby allied summons., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Wight Captain" (AbilityEffectType.Summon), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 08: Corpse Collector (`lich_lord_active_08` — Branch A — Undead Summoning, Tier 4)
- **Gameplay Context:** Summon a scavenger minion that gathers soul fragments and channels extra Soul Essence directly to the Lich. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon`, Radius: `5`, Duration: `25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Corpse Collector", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a scavenger minion that gathers soul fragments and channels extra soul essence directly to the lich., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Corpse Collector" (AbilityEffectType.Summon), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 09: Dread Abomination (`lich_lord_active_09` — Branch A — Undead Summoning, Tier 5)
- **Gameplay Context:** Summon a massive stitched monstrosity that crushes enemies with devastating area slams. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `28s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Dread Abomination", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a massive stitched monstrosity that crushes enemies with devastating area slams., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Dread Abomination" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 10: Skeletal Dragon (`lich_lord_active_10` — Branch A — Undead Summoning, Tier 5)
- **Gameplay Context:** Summon a colossal undead dragon that glides above the arena and blankets invaders in dark fire. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.MeleeArc`, Radius: `5`, Duration: `30s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Skeletal Dragon", Lich Lord ability (Branch A — Undead Summoning), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a colossal undead dragon that glides above the arena and blankets invaders in dark fire., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Skeletal Dragon" (AbilityEffectType.Summon | AbilityEffectType.MeleeArc), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 11: Grave Frost (`lich_lord_active_11` — Branch B — Frost and Death Magic, Tier 1)
- **Gameplay Context:** Release an expanding ring of glacial frost, dealing damage and slowing all nearby invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `4.5`, Duration: `3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Grave Frost", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting release an expanding ring of glacial frost, dealing damage and slowing all nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Grave Frost" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 12: Permafrost Sigil (`lich_lord_active_12` — Branch B — Frost and Death Magic, Tier 2)
- **Gameplay Context:** Etch a persistent freezing rune on the ground that deals continuous damage and chills passing invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `3`, Duration: `6s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Permafrost Sigil", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting etch a persistent freezing rune on the ground that deals continuous damage and chills passing invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Permafrost Sigil" (AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 13: Chill Lance (`lich_lord_active_13` — Branch B — Frost and Death Magic, Tier 2)
- **Gameplay Context:** Hurl a high-velocity piercing ice spear that impales and chills multiple invaders in a line. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.CrowdControl`, Radius: `0.25`, Duration: `1.2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Chill Lance", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting hurl a high-velocity piercing ice spear that impales and chills multiple invaders in a line., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Chill Lance" (AbilityEffectType.Projectile | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 14: Wailing Blizzard (`lich_lord_active_14` — Branch B — Frost and Death Magic, Tier 3)
- **Gameplay Context:** Conjure a moving howling blizzard storm that sweeps forward, freezing and shredding invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Wailing Blizzard", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting conjure a moving howling blizzard storm that sweeps forward, freezing and shredding invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Wailing Blizzard" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 15: Glacial Tomb (`lich_lord_active_15` — Branch B — Frost and Death Magic, Tier 3)
- **Gameplay Context:** Entomb the nearest targeted invader in solid ice, stunning them and dealing area damage. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE`, Radius: `2`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Glacial Tomb", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting entomb the nearest targeted invader in solid ice, stunning them and dealing area damage., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Glacial Tomb" (AbilityEffectType.CrowdControl | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 16: Shiver Pulse (`lich_lord_active_16` — Branch B — Frost and Death Magic, Tier 4)
- **Gameplay Context:** Unleash a sudden shockwave of absolute zero, violently knocking back and chilling surrounding invaders. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `6`, Duration: `3s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Shiver Pulse", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting unleash a sudden shockwave of absolute zero, violently knocking back and chilling surrounding invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Shiver Pulse" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 17: Rime Chain (`lich_lord_active_17` — Branch B — Frost and Death Magic, Tier 4)
- **Gameplay Context:** Cast arcing bands of frost that tether and jump between up to 4 nearby invaders, dealing damage and slowing. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.CrowdControl`, Radius: `4`, Duration: `2s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Rime Chain", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting cast arcing bands of frost that tether and jump between up to 4 nearby invaders, dealing damage and slowing., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Rime Chain" (AbilityEffectType.Projectile | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 18: Frozen Lantern (`lich_lord_active_18` — Branch B — Frost and Death Magic, Tier 4)
- **Gameplay Context:** Summon an orbiting frost lantern that shields the Lich Lord and fires frost bolts at nearby invaders. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.Shield | AbilityEffectType.Projectile`, Radius: `1.6`, Duration: `15s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Frozen Lantern", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon an orbiting frost lantern that shields the lich lord and fires frost bolts at nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Frozen Lantern" (AbilityEffectType.Summon | AbilityEffectType.Shield | AbilityEffectType.Projectile), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 19: Hail Crown (`lich_lord_active_19` — Branch B — Frost and Death Magic, Tier 5)
- **Gameplay Context:** Call down a barrage of jagged hail and ice spikes in a wide area around the Lich Lord. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `6`, Duration: `4s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hail Crown", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting call down a barrage of jagged hail and ice spikes in a wide area around the lich lord., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hail Crown" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 20: Winter of the Dead (`lich_lord_active_20` — Branch B — Frost and Death Magic, Tier 5)
- **Gameplay Context:** Engulf the entire battlefield in an ancient sub-zero freeze, slowing invaders and empowering all player summons. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Summon`, Radius: `12`, Duration: `10s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Winter of the Dead", Lich Lord ability (Branch B — Frost and Death Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting engulf the entire battlefield in an ancient sub-zero freeze, slowing invaders and empowering all player summons., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Winter of the Dead" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl | AbilityEffectType.Summon), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 21: Witchfire Orb (`lich_lord_active_21` — Branch C — Fire, Wind and Thunder Sorcery, Tier 1)
- **Gameplay Context:** Hurl a crackling orb of green witchfire that detonates on contact, scorching nearby invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.RadialAoE`, Radius: `1.5`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Witchfire Orb", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting hurl a crackling orb of green witchfire that detonates on contact, scorching nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Witchfire Orb" (AbilityEffectType.Projectile | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 22: Graveflame Pillar (`lich_lord_active_22` — Branch C — Fire, Wind and Thunder Sorcery, Tier 2)
- **Gameplay Context:** Erupt a column of violent dark flames at the target location, dealing heavy damage and leaving a burning ground zone. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `2.2`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Graveflame Pillar", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting erupt a column of violent dark flames at the target location, dealing heavy damage and leaving a burning ground zone., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Graveflame Pillar" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 23: Ashen Gust (`lich_lord_active_23` — Branch C — Fire, Wind and Thunder Sorcery, Tier 2)
- **Gameplay Context:** Unleash a fierce cone of ash and wind, dealing damage and violently repelling advancing invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl`, Radius: `5.5`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ashen Gust", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting unleash a fierce cone of ash and wind, dealing damage and violently repelling advancing invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ashen Gust" (AbilityEffectType.MeleeArc | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 24: Static Cadaver (`lich_lord_active_24` — Branch C — Fire, Wind and Thunder Sorcery, Tier 3)
- **Gameplay Context:** Electrify a targeted area or fallen remnant into a high-voltage trap that repeatedly zaps passing invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `2.8`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Static Cadaver", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting electrify a targeted area or fallen remnant into a high-voltage trap that repeatedly zaps passing invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Static Cadaver" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 25: Deathbolt (`lich_lord_active_25` — Branch C — Fire, Wind and Thunder Sorcery, Tier 3)
- **Gameplay Context:** Fire a concentrated bolt of lethal lightning that pierces through multiple enemies in a straight line. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.RadialAoE`, Radius: `0.35`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Deathbolt", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting fire a concentrated bolt of lethal lightning that pierces through multiple enemies in a straight line., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Deathbolt" (AbilityEffectType.Projectile | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 26: Corpse Spark (`lich_lord_active_26` — Branch C — Fire, Wind and Thunder Sorcery, Tier 4)
- **Gameplay Context:** Cast an arcing spark of electrical necrosis that jumps rapidly between up to 4 nearby invaders. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Projectile`, Radius: `1.5`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Corpse Spark", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting cast an arcing spark of electrical necrosis that jumps rapidly between up to 4 nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Corpse Spark" (AbilityEffectType.RadialAoE | AbilityEffectType.Projectile), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 27: Storm of Ash (`lich_lord_active_27` — Branch C — Fire, Wind and Thunder Sorcery, Tier 4)
- **Gameplay Context:** Conjure a turbulent burning windstorm that engulfs invaders in swirling embers and damaging heat. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `5.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Storm of Ash", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting conjure a turbulent burning windstorm that engulfs invaders in swirling embers and damaging heat., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Storm of Ash" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 28: Phantom Cyclone (`lich_lord_active_28` — Branch C — Fire, Wind and Thunder Sorcery, Tier 4)
- **Gameplay Context:** Unleash a spirit-filled tornado that drags nearby invaders toward its swirling core while ripping flesh. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Gravity | AbilityEffectType.Hazard`, Radius: `3.2`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Phantom Cyclone", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting unleash a spirit-filled tornado that drags nearby invaders toward its swirling core while ripping flesh., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Phantom Cyclone" (AbilityEffectType.RadialAoE | AbilityEffectType.Gravity | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 29: Ember Ossuary (`lich_lord_active_29` — Branch C — Fire, Wind and Thunder Sorcery, Tier 5)
- **Gameplay Context:** Form an explosive circular barrier of blazing bone runes that detonate when invaders breach them. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `4.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ember Ossuary", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting form an explosive circular barrier of blazing bone runes that detonate when invaders breach them., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ember Ossuary" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 30: Thunder Coffin (`lich_lord_active_30` — Branch C — Fire, Wind and Thunder Sorcery, Tier 5)
- **Gameplay Context:** Trap an enemy commander in an electric lightning prison before detonating in a catastrophic thunderclap. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `2.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Thunder Coffin", Lich Lord ability (Branch C — Fire, Wind and Thunder Sorcery), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting trap an enemy commander in an electric lightning prison before detonating in a catastrophic thunderclap., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Thunder Coffin" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 31: Soul Siphon (`lich_lord_active_31` — Branch D — Forbidden Dark Magic, Tier 1)
- **Gameplay Context:** Tear vital soul essence from nearby invaders, dealing necrotic damage and siphoning health back to the Lich Lord. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Heal`, Radius: `2.5`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Soul Siphon", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting tear vital soul essence from nearby invaders, dealing necrotic damage and siphoning health back to the lich lord., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Soul Siphon" (AbilityEffectType.RadialAoE | AbilityEffectType.Heal), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 32: Hex of Frailty (`lich_lord_active_32` — Branch D — Forbidden Dark Magic, Tier 2)
- **Gameplay Context:** Cast an ancient enervating hex over an area, dealing damage and crippling enemy defenses for 4 seconds. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.0`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Hex of Frailty", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting cast an ancient enervating hex over an area, dealing damage and crippling enemy defenses for 4 seconds., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Hex of Frailty" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 33: Withering Field (`lich_lord_active_33` — Branch D — Forbidden Dark Magic, Tier 2)
- **Gameplay Context:** Infect the battlefield ground with a decaying necrotic zone that continuously withers and saps invader vitality. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `3.0`, Duration: `4.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Withering Field", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting infect the battlefield ground with a decaying necrotic zone that continuously withers and saps invader vitality., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Withering Field" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 34: Black Sun (`lich_lord_active_34` — Branch D — Forbidden Dark Magic, Tier 3)
- **Gameplay Context:** Summon a dense sphere of gravitational darkness that drags approaching invaders inward while crushing them. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Gravity | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Black Sun", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a dense sphere of gravitational darkness that drags approaching invaders inward while crushing them., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Black Sun" (AbilityEffectType.RadialAoE | AbilityEffectType.Gravity | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 35: Spirit Leech (`lich_lord_active_35` — Branch D — Forbidden Dark Magic, Tier 3)
- **Gameplay Context:** Launch a parasitic shadowy wraith that latches onto the target, continuously draining health back to the Lich. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.Projectile | AbilityEffectType.Heal`, Radius: `0.3`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Spirit Leech", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting launch a parasitic shadowy wraith that latches onto the target, continuously draining health back to the lich., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Spirit Leech" (AbilityEffectType.Projectile | AbilityEffectType.Heal), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 36: Doom Bind (`lich_lord_active_36` — Branch D — Forbidden Dark Magic, Tier 4)
- **Gameplay Context:** Burst shadowy tendrils from the earth, dealing crushing damage and locking all enemies in place with root bonds. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `3.2`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Doom Bind", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting burst shadowy tendrils from the earth, dealing crushing damage and locking all enemies in place with root bonds., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Doom Bind" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 37: Ebon Grasp (`lich_lord_active_37` — Branch D — Forbidden Dark Magic, Tier 4)
- **Gameplay Context:** Summon clawing skeletal limbs from subterranean graves, tearing through invaders and heavily slowing movement. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl`, Radius: `3.5`, Duration: `4.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ebon Grasp", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon clawing skeletal limbs from subterranean graves, tearing through invaders and heavily slowing movement., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ebon Grasp" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 38: Curse Spread (`lich_lord_active_38` — Branch D — Forbidden Dark Magic, Tier 4)
- **Gameplay Context:** Transmit a virulent wave of darkness that damages a target and leaps to infect neighboring enemies with active curses. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Projectile`, Radius: `2.0`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Curse Spread", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting transmit a virulent wave of darkness that damages a target and leaps to infect neighboring enemies with active curses., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Curse Spread" (AbilityEffectType.RadialAoE | AbilityEffectType.Projectile), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 39: Death Sentence (`lich_lord_active_39` — Branch D — Forbidden Dark Magic, Tier 5)
- **Gameplay Context:** Brand a powerful invader with an ominous execution glyph, delivering massive necrotic damage when the timer expires. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `2.5`, Duration: `2.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Death Sentence", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting brand a powerful invader with an ominous execution glyph, delivering massive necrotic damage when the timer expires., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Death Sentence" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 40: Umbral Nova (`lich_lord_active_40` — Branch D — Forbidden Dark Magic, Tier 5)
- **Gameplay Context:** Release an apocalyptic shockwave of pure void darkness across the arena, obliterating invaders in a massive blast radius. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `8.0`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Umbral Nova", Lich Lord ability (Branch D — Forbidden Dark Magic), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting release an apocalyptic shockwave of pure void darkness across the arena, obliterating invaders in a massive blast radius., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Umbral Nova" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 41: Reanimate Fallen (`lich_lord_active_41` — Branch E — Necromantic Rituals, Tier 1)
- **Gameplay Context:** Perform an unholy ritual raising fallen foes as temporary skeletal revenants to fight at the Lich Lord's side. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.Summon | AbilityEffectType.Heal`, Radius: `1.5`, Duration: `8.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Reanimate Fallen", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting perform an unholy ritual raising fallen foes as temporary skeletal revenants to fight at the lich lord's side., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Reanimate Fallen" (AbilityEffectType.Summon | AbilityEffectType.Heal), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 42: Tomb Relay (`lich_lord_active_42` — Branch E — Necromantic Rituals, Tier 2)
- **Gameplay Context:** Instantly teleport all active undead minions to the target location with a violent necrotic impact shockwave. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE`, Radius: `2.5`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Tomb Relay", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting instantly teleport all active undead minions to the target location with a violent necrotic impact shockwave., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Tomb Relay" (AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 43: Legion Formation (`lich_lord_active_43` — Branch E — Necromantic Rituals, Tier 2)
- **Gameplay Context:** Command all active undead into an aggressive combat formation, granting massive attack speed and armor for 6 seconds. (Targeting: `Self`, Effect: `AbilityEffectType.Heal`, Radius: `6.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Legion Formation", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting command all active undead into an aggressive combat formation, granting massive attack speed and armor for 6 seconds., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Legion Formation" (AbilityEffectType.Heal), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 44: Soul Furnace (`lich_lord_active_44` — Branch E — Necromantic Rituals, Tier 3)
- **Gameplay Context:** Convert stored Soul Essence into a volcanic eruption of necrotic flame, incinerating all nearby invaders. (Targeting: `AimDirection`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.Hazard`, Radius: `3.5`, Duration: `3.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Soul Furnace", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting convert stored soul essence into a volcanic eruption of necrotic flame, incinerating all nearby invaders., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Soul Furnace" (AbilityEffectType.RadialAoE | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 45: Boneshield (`lich_lord_active_45` — Branch E — Necromantic Rituals, Tier 3)
- **Gameplay Context:** Conjure a whirling barrier of dense bone plates around the Lich Lord, granting armor and absorbing incoming damage. (Targeting: `Self`, Effect: `AbilityEffectType.Shield | AbilityEffectType.RadialAoE`, Radius: `2.2`, Duration: `8.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Boneshield", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting conjure a whirling barrier of dense bone plates around the lich lord, granting armor and absorbing incoming damage., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Boneshield" (AbilityEffectType.Shield | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 46: Corpse Explosion (`lich_lord_active_46` — Branch E — Necromantic Rituals, Tier 3)
- **Gameplay Context:** Detonate fallen remains or marked targets in the targeted area, unleashing a catastrophic concussive blast of bone and gore. (Targeting: `NearestInvader`, Effect: `AbilityEffectType.RadialAoE`, Radius: `3.8`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Corpse Explosion", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting detonate fallen remains or marked targets in the targeted area, unleashing a catastrophic concussive blast of bone and gore., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Corpse Explosion" (AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (NearestInvader) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 47: Soul Anchor (`lich_lord_active_47` — Branch E — Necromantic Rituals, Tier 4)
- **Gameplay Context:** Plant an unholy obelisk at targeted location that pulses protective wards, healing the Lich Lord and all nearby summons. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Heal | AbilityEffectType.Hazard`, Radius: `3.2`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Soul Anchor", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting plant an unholy obelisk at targeted location that pulses protective wards, healing the lich lord and all nearby summons., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Soul Anchor" (AbilityEffectType.Heal | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 48: Death Pact (`lich_lord_active_48` — Branch E — Necromantic Rituals, Tier 4)
- **Gameplay Context:** Sacrifice active undead minions to instantly restore health and Soul Essence to the Lich Lord. (Targeting: `Self`, Effect: `AbilityEffectType.Heal | AbilityEffectType.RadialAoE`, Radius: `3.5`, Duration: `0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Death Pact", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting sacrifice active undead minions to instantly restore health and soul essence to the lich lord., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Death Pact" (AbilityEffectType.Heal | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 49: Necropolis Gate (`lich_lord_active_49` — Branch E — Necromantic Rituals, Tier 4)
- **Gameplay Context:** Tear open a portal to the underworld, periodically summoning undead reinforcements over 6 seconds. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.Hazard`, Radius: `3.0`, Duration: `6.0s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Necropolis Gate", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting tear open a portal to the underworld, periodically summoning undead reinforcements over 6 seconds., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Necropolis Gate" (AbilityEffectType.Summon | AbilityEffectType.Hazard), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 50: Crown of Silence (`lich_lord_active_50` — Branch E — Necromantic Rituals, Tier 5)
- **Gameplay Context:** Unleash a deafening necrotic silence across a 7.5m radius, interrupting and silencing all enemies for 3.5 seconds. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `7.5`, Duration: `3.5s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Crown of Silence", Lich Lord ability (Branch E — Necromantic Rituals), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting unleash a deafening necrotic silence across a 7.5m radius, interrupting and silencing all enemies for 3.5 seconds., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Crown of Silence" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 51: Army of the Damned (`lich_lord_ultimate_50` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Summon a temporary army of skeleton defenders accompanied by a catastrophic necrotic blast. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `6`, Duration: `25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Army of the Damned", Lich Lord ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a temporary army of skeleton defenders accompanied by a catastrophic necrotic blast., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Army of the Damned" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 52: Ossuary Dragon (`lich_lord_ultimate_100` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Summon a colossal ancient undead dragon that swoops across the arena bathed in dark flames. (Targeting: `AimDirection`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `6.5`, Duration: `30s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Ossuary Dragon", Lich Lord ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting summon a colossal ancient undead dragon that swoops across the arena bathed in dark flames., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Ossuary Dragon" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (AimDirection) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 53: Eternal Winter Court (`lich_lord_ultimate_150` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Unleash an absolute glacial domain that deep-freezes invaders and summons 2 frost guardians. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `7.5`, Duration: `25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Eternal Winter Court", Lich Lord ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting unleash an absolute glacial domain that deep-freezes invaders and summons 2 frost guardians., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Eternal Winter Court" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 54: Black Star Eclipse (`lich_lord_ultimate_200` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Conjure an apocalyptic dying dark star that bombards the arena and harvests 40 Soul Essence. (Targeting: `Self`, Effect: `AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl`, Radius: `8.5`, Duration: `25s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "Black Star Eclipse", Lich Lord ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting conjure an apocalyptic dying dark star that bombards the arena and harvests 40 soul essence., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "Black Star Eclipse" (AbilityEffectType.RadialAoE | AbilityEffectType.CrowdControl), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`
#### Skill 55: World Grave Awakening (`lich_lord_ultimate_250` — Milestone Ultimates, Tier 5)
- **Gameplay Context:** Awaken all dead across the arena in a necrotic apocalypse, reviving 4 sentinels, 2 archers, and 1 wight captain. (Targeting: `Self`, Effect: `AbilityEffectType.Summon | AbilityEffectType.RadialAoE`, Radius: `9`, Duration: `35s`).
- **16x16 Skill Icon Prompt:**
  > `16x16 pixel art skill icon for "World Grave Awakening", Lich Lord ability (Milestone Ultimates), dark fantasy roguelite UI, 1px dark border #110E1B, primary palette #6E2BC4 and glowing #30E680 highlights, depicting awaken all dead across the arena in a necrotic apocalypse, reviving 4 sentinels, 2 archers, and 1 wight captain., crisp readable silhouette at 16x16, no anti-aliasing, FilterMode.Point.`
- **3-Frame Cast & Impact VFX Strip Prompt (`48x24 px`, 3 frames of `24x24` or `96x32` for Ultimates):**
  > `3-frame horizontal pixel art VFX sprite strip for "World Grave Awakening" (AbilityEffectType.Summon | AbilityEffectType.RadialAoE), frame 1: arcane cast flash on Lich Lord hands in #30E680, frame 2: active delivery (Self) with #6E2BC4 core and #110E1B 1px outer rim, frame 3: terminal burst/dissipation particles, transparent background, RGBA32.`

---

## 5. Unity Import, Slicing & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Filter / Format | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Character Sheet** | `Assets/_Game/Art/Characters/lich_lord/char_lich_lord_sheet.png` | `192x32` (or `384x32`) | `32` | `(0.5, 0.1)` | `Point`, `RGBA32` | `LichLordVisualPresenter._bodyRenderer` (`PlaceholderSprites.Circle` tinted `#6E2BC4`) |
| **Portrait Icon** | `Assets/_Game/Art/UI/Portraits/portrait_lich_lord.png` | `48x48` | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `CharacterSelectPresenter` class card color block |
| **Basic Attack VFX** | `Assets/_Game/Art/VFX/Characters/vfx_lich_lord_basic.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `ProjectileView` / `AttackTelegraphPresenter` (`PlaceholderSprites.Circle`) |
| **Signature VFX** | `Assets/_Game/Art/VFX/Characters/vfx_lich_lord_signature.png` | `256x64` (`4x 64x64`) | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `DeathWaveVfxPresenter` expanding ring placeholder |
| **Skill Icons (55x)** | `Assets/_Game/Art/UI/Skills/lich_lord/icon_<ability_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `Point`, `RGBA32` | `SkillTreeScreenPresenter` & `HudPresenter` slot squares |
