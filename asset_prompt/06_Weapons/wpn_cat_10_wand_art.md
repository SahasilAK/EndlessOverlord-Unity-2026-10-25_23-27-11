# Scepters & Spell Wands (`WeaponCategory.Wand`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #10/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Wand` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Compact rapid-casting foci tipped with Condensed stars, lich fingers, or void prisms (slender baton with oversized glowing tip).
- **Base Palette:** Primary Steel/Structure `#9B7EDE`, Signature Energy `#00E5FF`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#00E5FF` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Scepters & Spell Wands` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `item_bone_wand` | **Bone Wand** | `Common` (`#95A5A6`) | `FlatDamage = 4f, AttackSpeedPercent = 0.10f` | 20g / 10g | A carved skeletal wand channels focused necrotic blasts. |
| 02 | `wpn_wnd_willow_wand` | **Willow Wand** | `Common` (`#95A5A6`) | `FlatDamage = 5f, AttackSpeedPercent = 0.12f` | 20g / 10g | Pliant wand flicking precise magic darts. |
| 03 | `wpn_wnd_flint_spark_wand` | **Flint Spark Wand** | `Common` (`#95A5A6`) | `FlatDamage = 6f, AttackSpeedPercent = 0.10f` | 25g / 12g | Emits quick fiery sparks on flicking. |
| 04 | `wpn_wnd_crystal_channel_wand` | **Crystal Channel Wand** | `Uncommon` (`#2ECC71`) | `FlatDamage = 8f, AttackSpeedPercent = 0.16f` | 45g / 22g | Quartz tip amplifying arcane projectile velocity. |
| 05 | `wpn_wnd_pyroclast_wand` | **Pyroclast Wand** | `Uncommon` (`#2ECC71`) | `FlatDamage = 9f, DamagePercent = 0.08f` | 50g / 25g | Spits miniature cinder spheres. |
| 06 | `wpn_wnd_rime_wand` | **Rime Wand** | `Rare` (`#3498DB`) | `FlatDamage = 12f, AttackSpeedPercent = 0.20f` | 75g / 37g | Fires freezing splinters slowing invaders. |
| 07 | `wpn_wnd_tempest_wand` | **Tempest Wand** | `Rare` (`#3498DB`) | `FlatDamage = 13f, AttackSpeedPercent = 0.22f, DamagePercent = 0.10f` | 80g / 40g | Crackles with chain lightning arcs. |
| 08 | `wpn_wnd_sorcerer_fury_wand` | **Sorcerer Fury Wand** | `Epic` (`#9B59B6`) | `FlatDamage = 18f, AttackSpeedPercent = 0.35f` | 135g / 67g | Accelerates projectile cadence to machine-like flurries. |
| 09 | `wpn_wnd_nether_crystal_wand` | **Nether Crystal Wand** | `Epic` (`#9B59B6`) | `FlatDamage = 20f, DamagePercent = 0.20f, LifeStealPercent = 0.08f` | 145g / 72g | Channels dark void bolts through amethyst prisms. |
| 10 | `wpn_wnd_wand_of_endless_void` | **Wand of Endless Void** | `Legendary` (`#F1C40F`) | `FlatDamage = 28f, AttackSpeedPercent = 0.45f, DamagePercent = 0.25f` | 270g / 135g | Infinite nullity streams from its obsidian tip. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Bone Wand (`item_bone_wand` — `Common`)
- **Lore & Stats:** A carved skeletal wand channels focused necrotic blasts. (`FlatDamage = 4f, AttackSpeedPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_item_bone_wand.png`):**
  > `16x16 pixel art weapon icon of "Bone Wand" (Scepters & Spell Wands, Common rarity): A carved skeletal wand channels focused necrotic blasts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #95A5A6 rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_item_bone_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Bone Wand": Frame 1 windup glint in #95A5A6, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Willow Wand (`wpn_wnd_willow_wand` — `Common`)
- **Lore & Stats:** Pliant wand flicking precise magic darts. (`FlatDamage = 5f, AttackSpeedPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_willow_wand.png`):**
  > `16x16 pixel art weapon icon of "Willow Wand" (Scepters & Spell Wands, Common rarity): Pliant wand flicking precise magic darts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #95A5A6 rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_willow_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Willow Wand": Frame 1 windup glint in #95A5A6, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Flint Spark Wand (`wpn_wnd_flint_spark_wand` — `Common`)
- **Lore & Stats:** Emits quick fiery sparks on flicking. (`FlatDamage = 6f, AttackSpeedPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_flint_spark_wand.png`):**
  > `16x16 pixel art weapon icon of "Flint Spark Wand" (Scepters & Spell Wands, Common rarity): Emits quick fiery sparks on flicking. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #95A5A6 rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_flint_spark_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Flint Spark Wand": Frame 1 windup glint in #95A5A6, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Crystal Channel Wand (`wpn_wnd_crystal_channel_wand` — `Uncommon`)
- **Lore & Stats:** Quartz tip amplifying arcane projectile velocity. (`FlatDamage = 8f, AttackSpeedPercent = 0.16f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_crystal_channel_wand.png`):**
  > `16x16 pixel art weapon icon of "Crystal Channel Wand" (Scepters & Spell Wands, Uncommon rarity): Quartz tip amplifying arcane projectile velocity. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #2ECC71 rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_crystal_channel_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Crystal Channel Wand": Frame 1 windup glint in #2ECC71, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Pyroclast Wand (`wpn_wnd_pyroclast_wand` — `Uncommon`)
- **Lore & Stats:** Spits miniature cinder spheres. (`FlatDamage = 9f, DamagePercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_pyroclast_wand.png`):**
  > `16x16 pixel art weapon icon of "Pyroclast Wand" (Scepters & Spell Wands, Uncommon rarity): Spits miniature cinder spheres. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #2ECC71 rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_pyroclast_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Pyroclast Wand": Frame 1 windup glint in #2ECC71, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Rime Wand (`wpn_wnd_rime_wand` — `Rare`)
- **Lore & Stats:** Fires freezing splinters slowing invaders. (`FlatDamage = 12f, AttackSpeedPercent = 0.20f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_rime_wand.png`):**
  > `16x16 pixel art weapon icon of "Rime Wand" (Scepters & Spell Wands, Rare rarity): Fires freezing splinters slowing invaders. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #3498DB rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_rime_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Rime Wand": Frame 1 windup glint in #3498DB, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Tempest Wand (`wpn_wnd_tempest_wand` — `Rare`)
- **Lore & Stats:** Crackles with chain lightning arcs. (`FlatDamage = 13f, AttackSpeedPercent = 0.22f, DamagePercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_tempest_wand.png`):**
  > `16x16 pixel art weapon icon of "Tempest Wand" (Scepters & Spell Wands, Rare rarity): Crackles with chain lightning arcs. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #3498DB rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_tempest_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Tempest Wand": Frame 1 windup glint in #3498DB, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Sorcerer Fury Wand (`wpn_wnd_sorcerer_fury_wand` — `Epic`)
- **Lore & Stats:** Accelerates projectile cadence to machine-like flurries. (`FlatDamage = 18f, AttackSpeedPercent = 0.35f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_sorcerer_fury_wand.png`):**
  > `16x16 pixel art weapon icon of "Sorcerer Fury Wand" (Scepters & Spell Wands, Epic rarity): Accelerates projectile cadence to machine-like flurries. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #9B59B6 rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_sorcerer_fury_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Sorcerer Fury Wand": Frame 1 windup glint in #9B59B6, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Nether Crystal Wand (`wpn_wnd_nether_crystal_wand` — `Epic`)
- **Lore & Stats:** Channels dark void bolts through amethyst prisms. (`FlatDamage = 20f, DamagePercent = 0.20f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_nether_crystal_wand.png`):**
  > `16x16 pixel art weapon icon of "Nether Crystal Wand" (Scepters & Spell Wands, Epic rarity): Channels dark void bolts through amethyst prisms. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #9B59B6 rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_nether_crystal_wand_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Nether Crystal Wand": Frame 1 windup glint in #9B59B6, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Wand of Endless Void (`wpn_wnd_wand_of_endless_void` — `Legendary`)
- **Lore & Stats:** Infinite nullity streams from its obsidian tip. (`FlatDamage = 28f, AttackSpeedPercent = 0.45f, DamagePercent = 0.25f`).
- **16x16 Item Icon Prompt (`icon_wpn_wnd_wand_of_endless_void.png`):**
  > `16x16 pixel art weapon icon of "Wand of Endless Void" (Scepters & Spell Wands, Legendary rarity): Infinite nullity streams from its obsidian tip. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9B7EDE with #F1C40F rarity accent and #00E5FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_wnd_wand_of_endless_void_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Wand of Endless Void": Frame 1 windup glint in #F1C40F, Frame 2 active scepters & spell wands swing/projectile in #00E5FF and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/wand/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_wand_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
