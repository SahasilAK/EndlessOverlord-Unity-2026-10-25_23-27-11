# Spears, Halberds & Lances (`WeaponCategory.Spear`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #04/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Spear` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Long-reach polearms, dragonbane pikes, and celestial tridents (elongated diagonal shaft with ornate tip, linear thrust streak).
- **Base Palette:** Primary Steel/Structure `#B5C0C9`, Signature Energy `#F39C12`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#F39C12` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Spears, Halberds & Lances` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_spr_ash_wood_spear` | **Ash Wood Spear** | `Common` (`#95A5A6`) | `FlatDamage = 6f, AttackSpeedPercent = 0.05f` | 20g / 10g | Long pointed spear maintaining standoff distance. |
| 02 | `wpn_spr_bronze_pike` | **Bronze Pike** | `Common` (`#95A5A6`) | `FlatDamage = 7f, AttackSpeedPercent = 0.06f` | 25g / 12g | Sturdy phalanx pike with flared spearhead. |
| 03 | `wpn_spr_soldier_halberd` | **Soldier Halberd** | `Common` (`#95A5A6`) | `FlatDamage = 8f, AttackSpeedPercent = 0.04f` | 30g / 15g | Versatile axe-spear hybrid for chopping and thrusting. |
| 04 | `wpn_spr_storm_lance` | **Storm Lance** | `Uncommon` (`#2ECC71`) | `FlatDamage = 11f, AttackSpeedPercent = 0.08f` | 50g / 25g | Crackling lightning conducts down the steel shaft. |
| 05 | `wpn_spr_glaive_of_reaping` | **Glaive of Reaping** | `Uncommon` (`#2ECC71`) | `FlatDamage = 12f, DamagePercent = 0.08f` | 55g / 27g | Curved blade slicing through dense ranks. |
| 06 | `wpn_spr_wyrm_piercer` | **Wyrm Piercer** | `Rare` (`#3498DB`) | `FlatDamage = 16f, DamagePercent = 0.12f` | 80g / 40g | Hardened dragon-bone tip punching scales. |
| 07 | `wpn_spr_celestial_trident` | **Celestial Trident** | `Rare` (`#3498DB`) | `FlatDamage = 15f, AttackSpeedPercent = 0.15f, LifeStealPercent = 0.06f` | 85g / 42g | Three-pronged sacred fork radiating starlight. |
| 08 | `wpn_spr_sun_piercer_lance` | **Sun Piercer Lance** | `Epic` (`#9B59B6`) | `FlatDamage = 22f, DamagePercent = 0.18f, AttackSpeedPercent = 0.15f` | 135g / 67g | Channeling brilliant sunbeams into piercing strikes. |
| 09 | `wpn_spr_dread_naginata` | **Dread Naginata** | `Epic` (`#9B59B6`) | `FlatDamage = 24f, DamagePercent = 0.20f, LifeStealPercent = 0.08f` | 145g / 72g | Cursed polearm sweeping bloody swaths. |
| 10 | `wpn_spr_dragoon_god_lance` | **Dragoon God Lance** | `Legendary` (`#F1C40F`) | `FlatDamage = 35f, DamagePercent = 0.30f, AttackSpeedPercent = 0.25f` | 280g / 140g | Celestial dragonslayer spear leaping through thunderheads. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Ash Wood Spear (`wpn_spr_ash_wood_spear` — `Common`)
- **Lore & Stats:** Long pointed spear maintaining standoff distance. (`FlatDamage = 6f, AttackSpeedPercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_ash_wood_spear.png`):**
  > `16x16 pixel art weapon icon of "Ash Wood Spear" (Spears, Halberds & Lances, Common rarity): Long pointed spear maintaining standoff distance. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #95A5A6 rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_ash_wood_spear_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Ash Wood Spear": Frame 1 windup glint in #95A5A6, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Bronze Pike (`wpn_spr_bronze_pike` — `Common`)
- **Lore & Stats:** Sturdy phalanx pike with flared spearhead. (`FlatDamage = 7f, AttackSpeedPercent = 0.06f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_bronze_pike.png`):**
  > `16x16 pixel art weapon icon of "Bronze Pike" (Spears, Halberds & Lances, Common rarity): Sturdy phalanx pike with flared spearhead. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #95A5A6 rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_bronze_pike_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Bronze Pike": Frame 1 windup glint in #95A5A6, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Soldier Halberd (`wpn_spr_soldier_halberd` — `Common`)
- **Lore & Stats:** Versatile axe-spear hybrid for chopping and thrusting. (`FlatDamage = 8f, AttackSpeedPercent = 0.04f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_soldier_halberd.png`):**
  > `16x16 pixel art weapon icon of "Soldier Halberd" (Spears, Halberds & Lances, Common rarity): Versatile axe-spear hybrid for chopping and thrusting. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #95A5A6 rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_soldier_halberd_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Soldier Halberd": Frame 1 windup glint in #95A5A6, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Storm Lance (`wpn_spr_storm_lance` — `Uncommon`)
- **Lore & Stats:** Crackling lightning conducts down the steel shaft. (`FlatDamage = 11f, AttackSpeedPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_storm_lance.png`):**
  > `16x16 pixel art weapon icon of "Storm Lance" (Spears, Halberds & Lances, Uncommon rarity): Crackling lightning conducts down the steel shaft. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #2ECC71 rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_storm_lance_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Storm Lance": Frame 1 windup glint in #2ECC71, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Glaive of Reaping (`wpn_spr_glaive_of_reaping` — `Uncommon`)
- **Lore & Stats:** Curved blade slicing through dense ranks. (`FlatDamage = 12f, DamagePercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_glaive_of_reaping.png`):**
  > `16x16 pixel art weapon icon of "Glaive of Reaping" (Spears, Halberds & Lances, Uncommon rarity): Curved blade slicing through dense ranks. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #2ECC71 rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_glaive_of_reaping_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Glaive of Reaping": Frame 1 windup glint in #2ECC71, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Wyrm Piercer (`wpn_spr_wyrm_piercer` — `Rare`)
- **Lore & Stats:** Hardened dragon-bone tip punching scales. (`FlatDamage = 16f, DamagePercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_wyrm_piercer.png`):**
  > `16x16 pixel art weapon icon of "Wyrm Piercer" (Spears, Halberds & Lances, Rare rarity): Hardened dragon-bone tip punching scales. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #3498DB rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_wyrm_piercer_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Wyrm Piercer": Frame 1 windup glint in #3498DB, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Celestial Trident (`wpn_spr_celestial_trident` — `Rare`)
- **Lore & Stats:** Three-pronged sacred fork radiating starlight. (`FlatDamage = 15f, AttackSpeedPercent = 0.15f, LifeStealPercent = 0.06f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_celestial_trident.png`):**
  > `16x16 pixel art weapon icon of "Celestial Trident" (Spears, Halberds & Lances, Rare rarity): Three-pronged sacred fork radiating starlight. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #3498DB rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_celestial_trident_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Celestial Trident": Frame 1 windup glint in #3498DB, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Sun Piercer Lance (`wpn_spr_sun_piercer_lance` — `Epic`)
- **Lore & Stats:** Channeling brilliant sunbeams into piercing strikes. (`FlatDamage = 22f, DamagePercent = 0.18f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_sun_piercer_lance.png`):**
  > `16x16 pixel art weapon icon of "Sun Piercer Lance" (Spears, Halberds & Lances, Epic rarity): Channeling brilliant sunbeams into piercing strikes. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #9B59B6 rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_sun_piercer_lance_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Sun Piercer Lance": Frame 1 windup glint in #9B59B6, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Dread Naginata (`wpn_spr_dread_naginata` — `Epic`)
- **Lore & Stats:** Cursed polearm sweeping bloody swaths. (`FlatDamage = 24f, DamagePercent = 0.20f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_dread_naginata.png`):**
  > `16x16 pixel art weapon icon of "Dread Naginata" (Spears, Halberds & Lances, Epic rarity): Cursed polearm sweeping bloody swaths. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #9B59B6 rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_dread_naginata_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Dread Naginata": Frame 1 windup glint in #9B59B6, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Dragoon God Lance (`wpn_spr_dragoon_god_lance` — `Legendary`)
- **Lore & Stats:** Celestial dragonslayer spear leaping through thunderheads. (`FlatDamage = 35f, DamagePercent = 0.30f, AttackSpeedPercent = 0.25f`).
- **16x16 Item Icon Prompt (`icon_wpn_spr_dragoon_god_lance.png`):**
  > `16x16 pixel art weapon icon of "Dragoon God Lance" (Spears, Halberds & Lances, Legendary rarity): Celestial dragonslayer spear leaping through thunderheads. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #B5C0C9 with #F1C40F rarity accent and #F39C12 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spr_dragoon_god_lance_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Dragoon God Lance": Frame 1 windup glint in #F1C40F, Frame 2 active spears, halberds & lances swing/projectile in #F39C12 and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/spear/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_spear_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
