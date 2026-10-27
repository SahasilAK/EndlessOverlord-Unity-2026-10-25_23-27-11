# Battleaxes & Cleavers (`WeaponCategory.Axe`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #05/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Axe` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Single and double-bitted berserker axes, bearded frost axes, and Ragnarok cleavers (heavy curved crescent head).
- **Base Palette:** Primary Steel/Structure `#9AA5B1`, Signature Energy `#D35400`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#D35400` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Battleaxes & Cleavers` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_axe_woodchopper_hatchet` | **Woodchopper Hatchet** | `Common` (`#95A5A6`) | `FlatDamage = 6f` | 20g / 10g | Simple hand axe with keen edge. |
| 02 | `wpn_axe_iron_battleaxe` | **Iron Battleaxe** | `Common` (`#95A5A6`) | `FlatDamage = 8f` | 25g / 12g | Double-bitted martial axe. |
| 03 | `wpn_axe_berserker_tomahawk` | **Berserker Tomahawk** | `Common` (`#95A5A6`) | `FlatDamage = 7f, AttackSpeedPercent = 0.10f` | 25g / 12g | Fast swinging throwing and chopping tomahawk. |
| 04 | `wpn_axe_molten_chopper` | **Molten Chopper** | `Uncommon` (`#2ECC71`) | `FlatDamage = 12f, DamagePercent = 0.08f` | 50g / 25g | Furnace-heated edge cauterizing deep wounds. |
| 05 | `wpn_axe_frost_bearded_axe` | **Frost Bearded Axe** | `Uncommon` (`#2ECC71`) | `FlatDamage = 11f, AttackSpeedPercent = 0.08f` | 50g / 25g | Norwegian bearded axe coated in biting frost. |
| 06 | `wpn_axe_skull_splitter` | **Skull Splitter** | `Rare` (`#3498DB`) | `FlatDamage = 17f, DamagePercent = 0.14f` | 80g / 40g | Weighted wedge crushing helms and craniums. |
| 07 | `wpn_axe_viking_crescent` | **Viking Crescent** | `Rare` (`#3498DB`) | `FlatDamage = 18f, AttackSpeedPercent = 0.12f` | 85g / 42g | Sweeping crescent axe cleaving multiple foes. |
| 08 | `wpn_axe_infernal_waraxe` | **Infernal Waraxe** | `Epic` (`#9B59B6`) | `FlatDamage = 25f, DamagePercent = 0.20f, LifeStealPercent = 0.08f` | 140g / 70g | Demon-forged battleaxe hungry for violence. |
| 09 | `wpn_axe_frenzy_labrys` | **Frenzy Labrys** | `Epic` (`#9B59B6`) | `FlatDamage = 26f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.08f` | 150g / 75g | Double-bladed labrys accelerating with each kill. |
| 10 | `wpn_axe_ragnarok_cleaver` | **Ragnarok Cleaver** | `Legendary` (`#F1C40F`) | `FlatDamage = 38f, DamagePercent = 0.32f, LifeStealPercent = 0.12f` | 290g / 145g | World-ending battleaxe sundering the sky. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Woodchopper Hatchet (`wpn_axe_woodchopper_hatchet` — `Common`)
- **Lore & Stats:** Simple hand axe with keen edge. (`FlatDamage = 6f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_woodchopper_hatchet.png`):**
  > `16x16 pixel art weapon icon of "Woodchopper Hatchet" (Battleaxes & Cleavers, Common rarity): Simple hand axe with keen edge. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #95A5A6 rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_woodchopper_hatchet_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Woodchopper Hatchet": Frame 1 windup glint in #95A5A6, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Iron Battleaxe (`wpn_axe_iron_battleaxe` — `Common`)
- **Lore & Stats:** Double-bitted martial axe. (`FlatDamage = 8f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_iron_battleaxe.png`):**
  > `16x16 pixel art weapon icon of "Iron Battleaxe" (Battleaxes & Cleavers, Common rarity): Double-bitted martial axe. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #95A5A6 rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_iron_battleaxe_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Iron Battleaxe": Frame 1 windup glint in #95A5A6, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Berserker Tomahawk (`wpn_axe_berserker_tomahawk` — `Common`)
- **Lore & Stats:** Fast swinging throwing and chopping tomahawk. (`FlatDamage = 7f, AttackSpeedPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_berserker_tomahawk.png`):**
  > `16x16 pixel art weapon icon of "Berserker Tomahawk" (Battleaxes & Cleavers, Common rarity): Fast swinging throwing and chopping tomahawk. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #95A5A6 rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_berserker_tomahawk_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Berserker Tomahawk": Frame 1 windup glint in #95A5A6, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Molten Chopper (`wpn_axe_molten_chopper` — `Uncommon`)
- **Lore & Stats:** Furnace-heated edge cauterizing deep wounds. (`FlatDamage = 12f, DamagePercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_molten_chopper.png`):**
  > `16x16 pixel art weapon icon of "Molten Chopper" (Battleaxes & Cleavers, Uncommon rarity): Furnace-heated edge cauterizing deep wounds. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #2ECC71 rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_molten_chopper_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Molten Chopper": Frame 1 windup glint in #2ECC71, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Frost Bearded Axe (`wpn_axe_frost_bearded_axe` — `Uncommon`)
- **Lore & Stats:** Norwegian bearded axe coated in biting frost. (`FlatDamage = 11f, AttackSpeedPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_frost_bearded_axe.png`):**
  > `16x16 pixel art weapon icon of "Frost Bearded Axe" (Battleaxes & Cleavers, Uncommon rarity): Norwegian bearded axe coated in biting frost. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #2ECC71 rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_frost_bearded_axe_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Frost Bearded Axe": Frame 1 windup glint in #2ECC71, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Skull Splitter (`wpn_axe_skull_splitter` — `Rare`)
- **Lore & Stats:** Weighted wedge crushing helms and craniums. (`FlatDamage = 17f, DamagePercent = 0.14f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_skull_splitter.png`):**
  > `16x16 pixel art weapon icon of "Skull Splitter" (Battleaxes & Cleavers, Rare rarity): Weighted wedge crushing helms and craniums. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #3498DB rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_skull_splitter_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Skull Splitter": Frame 1 windup glint in #3498DB, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Viking Crescent (`wpn_axe_viking_crescent` — `Rare`)
- **Lore & Stats:** Sweeping crescent axe cleaving multiple foes. (`FlatDamage = 18f, AttackSpeedPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_viking_crescent.png`):**
  > `16x16 pixel art weapon icon of "Viking Crescent" (Battleaxes & Cleavers, Rare rarity): Sweeping crescent axe cleaving multiple foes. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #3498DB rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_viking_crescent_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Viking Crescent": Frame 1 windup glint in #3498DB, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Infernal Waraxe (`wpn_axe_infernal_waraxe` — `Epic`)
- **Lore & Stats:** Demon-forged battleaxe hungry for violence. (`FlatDamage = 25f, DamagePercent = 0.20f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_infernal_waraxe.png`):**
  > `16x16 pixel art weapon icon of "Infernal Waraxe" (Battleaxes & Cleavers, Epic rarity): Demon-forged battleaxe hungry for violence. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #9B59B6 rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_infernal_waraxe_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Infernal Waraxe": Frame 1 windup glint in #9B59B6, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Frenzy Labrys (`wpn_axe_frenzy_labrys` — `Epic`)
- **Lore & Stats:** Double-bladed labrys accelerating with each kill. (`FlatDamage = 26f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_frenzy_labrys.png`):**
  > `16x16 pixel art weapon icon of "Frenzy Labrys" (Battleaxes & Cleavers, Epic rarity): Double-bladed labrys accelerating with each kill. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #9B59B6 rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_frenzy_labrys_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Frenzy Labrys": Frame 1 windup glint in #9B59B6, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Ragnarok Cleaver (`wpn_axe_ragnarok_cleaver` — `Legendary`)
- **Lore & Stats:** World-ending battleaxe sundering the sky. (`FlatDamage = 38f, DamagePercent = 0.32f, LifeStealPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_axe_ragnarok_cleaver.png`):**
  > `16x16 pixel art weapon icon of "Ragnarok Cleaver" (Battleaxes & Cleavers, Legendary rarity): World-ending battleaxe sundering the sky. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #9AA5B1 with #F1C40F rarity accent and #D35400 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_axe_ragnarok_cleaver_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Ragnarok Cleaver": Frame 1 windup glint in #F1C40F, Frame 2 active battleaxes & cleavers swing/projectile in #D35400 and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/axe/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_axe_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
