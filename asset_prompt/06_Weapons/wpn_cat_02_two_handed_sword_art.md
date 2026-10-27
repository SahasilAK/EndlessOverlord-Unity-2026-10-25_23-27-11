# Two-Handed Greatswords (`WeaponCategory.TwoHandedSword`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #02/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.TwoHandedSword` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Colossal highlander claymores, flamberges, and world-breaker slabs (`45°` diagonal orientation, heavy `220°` cleave arc).
- **Base Palette:** Primary Steel/Structure `#A3AEB8`, Signature Energy `#E74C3C`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#E74C3C` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Two-Handed Greatswords` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_2h_heavy_claymore` | **Heavy Claymore** | `Common` (`#95A5A6`) | `FlatDamage = 9f` | 25g / 12g | Long massive highlander greatsword. |
| 02 | `wpn_2h_bastard_sword` | **Bastard Sword** | `Common` (`#95A5A6`) | `FlatDamage = 10f, AttackSpeedPercent = -0.05f` | 25g / 12g | Versatile long steel blade. |
| 03 | `wpn_2h_zweihander` | **Zweihander** | `Common` (`#95A5A6`) | `FlatDamage = 12f, AttackSpeedPercent = -0.08f` | 30g / 15g | Immense flamberge greatsword sweeping wide arcs. |
| 04 | `wpn_2h_obsidian_greatsword` | **Obsidian Greatsword** | `Uncommon` (`#2ECC71`) | `FlatDamage = 15f, DamagePercent = 0.10f` | 50g / 25g | Razor-sharp volcanic glass cleaver. |
| 05 | `wpn_2h_crimson_cleaver` | **Crimson Cleaver** | `Uncommon` (`#2ECC71`) | `FlatDamage = 16f, LifeStealPercent = 0.06f` | 55g / 27g | Heavy slab stained with centuries of conqueror blood. |
| 06 | `wpn_2h_cataclysm_edge` | **Cataclysm Edge** | `Rare` (`#3498DB`) | `FlatDamage = 22f, DamagePercent = 0.15f` | 85g / 42g | Jagged volcanic greatsword shaking the arena. |
| 07 | `wpn_2h_soul_reaper_blade` | **Soul Reaper Blade** | `Rare` (`#3498DB`) | `FlatDamage = 20f, LifeStealPercent = 0.10f, CooldownReductionPercent = 0.08f` | 90g / 45g | Ethereal greatsword carving through spiritual bonds. |
| 08 | `wpn_2h_abyssal_colossus` | **Abyssal Colossus** | `Epic` (`#9B59B6`) | `FlatDamage = 28f, DamagePercent = 0.22f, FlatArmor = 6f` | 140g / 70g | Colossal sword forged in crushing deep ocean trenches. |
| 09 | `wpn_2h_titan_slab` | **Titan Slab** | `Epic` (`#9B59B6`) | `FlatDamage = 32f, DamagePercent = 0.25f, AttackSpeedPercent = -0.05f` | 150g / 75g | Monolithic blunt blade flattening invaders. |
| 10 | `wpn_2h_world_breaker_blade` | **World Breaker Blade** | `Legendary` (`#F1C40F`) | `FlatDamage = 45f, DamagePercent = 0.35f, LifeStealPercent = 0.15f` | 300g / 150g | Mythic god-slaying greatsword shattering bedrock. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Heavy Claymore (`wpn_2h_heavy_claymore` — `Common`)
- **Lore & Stats:** Long massive highlander greatsword. (`FlatDamage = 9f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_heavy_claymore.png`):**
  > `16x16 pixel art weapon icon of "Heavy Claymore" (Two-Handed Greatswords, Common rarity): Long massive highlander greatsword. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #95A5A6 rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_heavy_claymore_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Heavy Claymore": Frame 1 windup glint in #95A5A6, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Bastard Sword (`wpn_2h_bastard_sword` — `Common`)
- **Lore & Stats:** Versatile long steel blade. (`FlatDamage = 10f, AttackSpeedPercent = -0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_bastard_sword.png`):**
  > `16x16 pixel art weapon icon of "Bastard Sword" (Two-Handed Greatswords, Common rarity): Versatile long steel blade. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #95A5A6 rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_bastard_sword_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Bastard Sword": Frame 1 windup glint in #95A5A6, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Zweihander (`wpn_2h_zweihander` — `Common`)
- **Lore & Stats:** Immense flamberge greatsword sweeping wide arcs. (`FlatDamage = 12f, AttackSpeedPercent = -0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_zweihander.png`):**
  > `16x16 pixel art weapon icon of "Zweihander" (Two-Handed Greatswords, Common rarity): Immense flamberge greatsword sweeping wide arcs. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #95A5A6 rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_zweihander_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Zweihander": Frame 1 windup glint in #95A5A6, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Obsidian Greatsword (`wpn_2h_obsidian_greatsword` — `Uncommon`)
- **Lore & Stats:** Razor-sharp volcanic glass cleaver. (`FlatDamage = 15f, DamagePercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_obsidian_greatsword.png`):**
  > `16x16 pixel art weapon icon of "Obsidian Greatsword" (Two-Handed Greatswords, Uncommon rarity): Razor-sharp volcanic glass cleaver. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #2ECC71 rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_obsidian_greatsword_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Obsidian Greatsword": Frame 1 windup glint in #2ECC71, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Crimson Cleaver (`wpn_2h_crimson_cleaver` — `Uncommon`)
- **Lore & Stats:** Heavy slab stained with centuries of conqueror blood. (`FlatDamage = 16f, LifeStealPercent = 0.06f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_crimson_cleaver.png`):**
  > `16x16 pixel art weapon icon of "Crimson Cleaver" (Two-Handed Greatswords, Uncommon rarity): Heavy slab stained with centuries of conqueror blood. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #2ECC71 rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_crimson_cleaver_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Crimson Cleaver": Frame 1 windup glint in #2ECC71, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Cataclysm Edge (`wpn_2h_cataclysm_edge` — `Rare`)
- **Lore & Stats:** Jagged volcanic greatsword shaking the arena. (`FlatDamage = 22f, DamagePercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_cataclysm_edge.png`):**
  > `16x16 pixel art weapon icon of "Cataclysm Edge" (Two-Handed Greatswords, Rare rarity): Jagged volcanic greatsword shaking the arena. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #3498DB rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_cataclysm_edge_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Cataclysm Edge": Frame 1 windup glint in #3498DB, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Soul Reaper Blade (`wpn_2h_soul_reaper_blade` — `Rare`)
- **Lore & Stats:** Ethereal greatsword carving through spiritual bonds. (`FlatDamage = 20f, LifeStealPercent = 0.10f, CooldownReductionPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_soul_reaper_blade.png`):**
  > `16x16 pixel art weapon icon of "Soul Reaper Blade" (Two-Handed Greatswords, Rare rarity): Ethereal greatsword carving through spiritual bonds. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #3498DB rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_soul_reaper_blade_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Soul Reaper Blade": Frame 1 windup glint in #3498DB, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Abyssal Colossus (`wpn_2h_abyssal_colossus` — `Epic`)
- **Lore & Stats:** Colossal sword forged in crushing deep ocean trenches. (`FlatDamage = 28f, DamagePercent = 0.22f, FlatArmor = 6f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_abyssal_colossus.png`):**
  > `16x16 pixel art weapon icon of "Abyssal Colossus" (Two-Handed Greatswords, Epic rarity): Colossal sword forged in crushing deep ocean trenches. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #9B59B6 rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_abyssal_colossus_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Abyssal Colossus": Frame 1 windup glint in #9B59B6, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Titan Slab (`wpn_2h_titan_slab` — `Epic`)
- **Lore & Stats:** Monolithic blunt blade flattening invaders. (`FlatDamage = 32f, DamagePercent = 0.25f, AttackSpeedPercent = -0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_titan_slab.png`):**
  > `16x16 pixel art weapon icon of "Titan Slab" (Two-Handed Greatswords, Epic rarity): Monolithic blunt blade flattening invaders. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #9B59B6 rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_titan_slab_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Titan Slab": Frame 1 windup glint in #9B59B6, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: World Breaker Blade (`wpn_2h_world_breaker_blade` — `Legendary`)
- **Lore & Stats:** Mythic god-slaying greatsword shattering bedrock. (`FlatDamage = 45f, DamagePercent = 0.35f, LifeStealPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_2h_world_breaker_blade.png`):**
  > `16x16 pixel art weapon icon of "World Breaker Blade" (Two-Handed Greatswords, Legendary rarity): Mythic god-slaying greatsword shattering bedrock. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A3AEB8 with #F1C40F rarity accent and #E74C3C energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_2h_world_breaker_blade_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "World Breaker Blade": Frame 1 windup glint in #F1C40F, Frame 2 active two-handed greatswords swing/projectile in #E74C3C and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/two_handed_sword/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_two_handed_sword_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
