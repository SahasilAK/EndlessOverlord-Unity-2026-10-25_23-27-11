# Hunting & Greatbows (`WeaponCategory.Bow`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #07/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Bow` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Recurve, composite, and starfall greatbows strung with glowing energy cords (curved limbs at `45°`, arrow nocked).
- **Base Palette:** Primary Steel/Structure `#A67C52`, Signature Energy `#2ECC71`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#2ECC71` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Hunting & Greatbows` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_bow_short_hunting_bow` | **Short Hunting Bow** | `Common` (`#95A5A6`) | `FlatDamage = 4f, AttackSpeedPercent = 0.12f` | 20g / 10g | Nimble recurve bow firing rapid arrows. |
| 02 | `wpn_bow_cedar_longbow` | **Cedar Longbow** | `Common` (`#95A5A6`) | `FlatDamage = 6f, AttackSpeedPercent = 0.08f` | 25g / 12g | High-draw longbow launching piercing shafts. |
| 03 | `wpn_bow_recurve_composite` | **Recurve Composite Bow** | `Common` (`#95A5A6`) | `FlatDamage = 5f, AttackSpeedPercent = 0.15f` | 25g / 12g | Laminated horn and wood launching arrows with venomous speed. |
| 04 | `wpn_bow_gale_whisper_bow` | **Gale Whisper Bow** | `Uncommon` (`#2ECC71`) | `FlatDamage = 8f, AttackSpeedPercent = 0.18f, MoveSpeedPercent = 0.06f` | 50g / 25g | Arrows propelled by trapped wind currents. |
| 05 | `wpn_bow_flame_quiver_bow` | **Flame Quiver Bow** | `Uncommon` (`#2ECC71`) | `FlatDamage = 9f, DamagePercent = 0.08f` | 50g / 25g | Ignites arrows into fiery streaks. |
| 06 | `wpn_bow_viper_sting_bow` | **Viper Sting Bow** | `Rare` (`#3498DB`) | `FlatDamage = 13f, AttackSpeedPercent = 0.22f` | 80g / 40g | Envenomed broadheads weakening targets. |
| 07 | `wpn_bow_frost_feather_bow` | **Frost Feather Bow** | `Rare` (`#3498DB`) | `FlatDamage = 12f, AttackSpeedPercent = 0.20f, CooldownReductionPercent = 0.08f` | 85g / 42g | Chilling volleys dampening enemy advance. |
| 08 | `wpn_bow_starfall_greatbow` | **Starfall Greatbow** | `Epic` (`#9B59B6`) | `FlatDamage = 19f, DamagePercent = 0.20f, AttackSpeedPercent = 0.25f` | 140g / 70g | Massive bow raining luminous starlight arrows. |
| 09 | `wpn_bow_shadow_composite_bow` | **Shadow Composite Bow** | `Epic` (`#9B59B6`) | `FlatDamage = 20f, AttackSpeedPercent = 0.28f, MoveSpeedPercent = 0.10f` | 145g / 72g | Ethereal arrows phasing through obstacles. |
| 10 | `wpn_bow_artemis_silver_bow` | **Artemis Silver Bow** | `Legendary` (`#F1C40F`) | `FlatDamage = 30f, DamagePercent = 0.30f, AttackSpeedPercent = 0.35f, LifeStealPercent = 0.10f` | 280g / 140g | Divine moonbow never missing its destined target. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Short Hunting Bow (`wpn_bow_short_hunting_bow` — `Common`)
- **Lore & Stats:** Nimble recurve bow firing rapid arrows. (`FlatDamage = 4f, AttackSpeedPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_short_hunting_bow.png`):**
  > `16x16 pixel art weapon icon of "Short Hunting Bow" (Hunting & Greatbows, Common rarity): Nimble recurve bow firing rapid arrows. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #95A5A6 rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_short_hunting_bow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Short Hunting Bow": Frame 1 windup glint in #95A5A6, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Cedar Longbow (`wpn_bow_cedar_longbow` — `Common`)
- **Lore & Stats:** High-draw longbow launching piercing shafts. (`FlatDamage = 6f, AttackSpeedPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_cedar_longbow.png`):**
  > `16x16 pixel art weapon icon of "Cedar Longbow" (Hunting & Greatbows, Common rarity): High-draw longbow launching piercing shafts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #95A5A6 rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_cedar_longbow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Cedar Longbow": Frame 1 windup glint in #95A5A6, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Recurve Composite Bow (`wpn_bow_recurve_composite` — `Common`)
- **Lore & Stats:** Laminated horn and wood launching arrows with venomous speed. (`FlatDamage = 5f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_recurve_composite.png`):**
  > `16x16 pixel art weapon icon of "Recurve Composite Bow" (Hunting & Greatbows, Common rarity): Laminated horn and wood launching arrows with venomous speed. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #95A5A6 rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_recurve_composite_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Recurve Composite Bow": Frame 1 windup glint in #95A5A6, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Gale Whisper Bow (`wpn_bow_gale_whisper_bow` — `Uncommon`)
- **Lore & Stats:** Arrows propelled by trapped wind currents. (`FlatDamage = 8f, AttackSpeedPercent = 0.18f, MoveSpeedPercent = 0.06f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_gale_whisper_bow.png`):**
  > `16x16 pixel art weapon icon of "Gale Whisper Bow" (Hunting & Greatbows, Uncommon rarity): Arrows propelled by trapped wind currents. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #2ECC71 rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_gale_whisper_bow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Gale Whisper Bow": Frame 1 windup glint in #2ECC71, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Flame Quiver Bow (`wpn_bow_flame_quiver_bow` — `Uncommon`)
- **Lore & Stats:** Ignites arrows into fiery streaks. (`FlatDamage = 9f, DamagePercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_flame_quiver_bow.png`):**
  > `16x16 pixel art weapon icon of "Flame Quiver Bow" (Hunting & Greatbows, Uncommon rarity): Ignites arrows into fiery streaks. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #2ECC71 rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_flame_quiver_bow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Flame Quiver Bow": Frame 1 windup glint in #2ECC71, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Viper Sting Bow (`wpn_bow_viper_sting_bow` — `Rare`)
- **Lore & Stats:** Envenomed broadheads weakening targets. (`FlatDamage = 13f, AttackSpeedPercent = 0.22f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_viper_sting_bow.png`):**
  > `16x16 pixel art weapon icon of "Viper Sting Bow" (Hunting & Greatbows, Rare rarity): Envenomed broadheads weakening targets. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #3498DB rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_viper_sting_bow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Viper Sting Bow": Frame 1 windup glint in #3498DB, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Frost Feather Bow (`wpn_bow_frost_feather_bow` — `Rare`)
- **Lore & Stats:** Chilling volleys dampening enemy advance. (`FlatDamage = 12f, AttackSpeedPercent = 0.20f, CooldownReductionPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_frost_feather_bow.png`):**
  > `16x16 pixel art weapon icon of "Frost Feather Bow" (Hunting & Greatbows, Rare rarity): Chilling volleys dampening enemy advance. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #3498DB rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_frost_feather_bow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Frost Feather Bow": Frame 1 windup glint in #3498DB, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Starfall Greatbow (`wpn_bow_starfall_greatbow` — `Epic`)
- **Lore & Stats:** Massive bow raining luminous starlight arrows. (`FlatDamage = 19f, DamagePercent = 0.20f, AttackSpeedPercent = 0.25f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_starfall_greatbow.png`):**
  > `16x16 pixel art weapon icon of "Starfall Greatbow" (Hunting & Greatbows, Epic rarity): Massive bow raining luminous starlight arrows. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #9B59B6 rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_starfall_greatbow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Starfall Greatbow": Frame 1 windup glint in #9B59B6, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Shadow Composite Bow (`wpn_bow_shadow_composite_bow` — `Epic`)
- **Lore & Stats:** Ethereal arrows phasing through obstacles. (`FlatDamage = 20f, AttackSpeedPercent = 0.28f, MoveSpeedPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_shadow_composite_bow.png`):**
  > `16x16 pixel art weapon icon of "Shadow Composite Bow" (Hunting & Greatbows, Epic rarity): Ethereal arrows phasing through obstacles. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #9B59B6 rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_shadow_composite_bow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Shadow Composite Bow": Frame 1 windup glint in #9B59B6, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Artemis Silver Bow (`wpn_bow_artemis_silver_bow` — `Legendary`)
- **Lore & Stats:** Divine moonbow never missing its destined target. (`FlatDamage = 30f, DamagePercent = 0.30f, AttackSpeedPercent = 0.35f, LifeStealPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_bow_artemis_silver_bow.png`):**
  > `16x16 pixel art weapon icon of "Artemis Silver Bow" (Hunting & Greatbows, Legendary rarity): Divine moonbow never missing its destined target. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #A67C52 with #F1C40F rarity accent and #2ECC71 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_bow_artemis_silver_bow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Artemis Silver Bow": Frame 1 windup glint in #F1C40F, Frame 2 active hunting & greatbows swing/projectile in #2ECC71 and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/bow/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_bow_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
