# Crossbows, Repeaters & Ballistas (`WeaponCategory.Crossbow`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #08/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Crossbow` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Mechanical arbalests, witch-hunter repeaters, and gatling bolt-casters (stock + horizontal steel prod).
- **Base Palette:** Primary Steel/Structure `#6E7B85`, Signature Energy `#E67E22`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#E67E22` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Crossbows, Repeaters & Ballistas` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_xbw_light_hand_crossbow` | **Light Hand Crossbow** | `Common` (`#95A5A6`) | `FlatDamage = 5f, AttackSpeedPercent = 0.10f` | 20g / 10g | Compact one-handed quarrel launcher. |
| 02 | `wpn_xbw_iron_arbalest` | **Iron Arbalest** | `Common` (`#95A5A6`) | `FlatDamage = 8f` | 25g / 12g | Steel prod delivering brutal punch. |
| 03 | `wpn_xbw_repeater_crossbow` | **Repeater Crossbow** | `Common` (`#95A5A6`) | `FlatDamage = 6f, AttackSpeedPercent = 0.20f` | 30g / 15g | Magazine-fed rapid-fire quarrel projector. |
| 04 | `wpn_xbw_heavy_siege_arbalest` | **Heavy Siege Arbalest** | `Uncommon` (`#2ECC71`) | `FlatDamage = 13f, DamagePercent = 0.10f` | 50g / 25g | Winch-cranked arbalest punching iron shields. |
| 05 | `wpn_xbw_barbed_bolt_caster` | **Barbed Bolt Caster** | `Uncommon` (`#2ECC71`) | `FlatDamage = 11f, AttackSpeedPercent = 0.15f` | 55g / 27g | Barbed bolts tearing through flesh. |
| 06 | `wpn_xbw_witch_hunter_repeater` | **Witch Hunter Repeater** | `Rare` (`#3498DB`) | `FlatDamage = 16f, AttackSpeedPercent = 0.25f` | 85g / 42g | Blessed repeater firing silver tipped quarrels. |
| 07 | `wpn_xbw_consecrated_cross_caster` | **Consecrated Cross Caster** | `Rare` (`#3498DB`) | `FlatDamage = 17f, DamagePercent = 0.14f` | 90g / 45g | Holy symbol firing radiant darts. |
| 08 | `wpn_xbw_twin_demon_arbalest` | **Twin Demon Arbalest** | `Epic` (`#9B59B6`) | `FlatDamage = 24f, AttackSpeedPercent = 0.25f, LifeStealPercent = 0.08f` | 145g / 72g | Dual-barrel monstrosity launching twin heavy quarrels. |
| 09 | `wpn_xbw_dread_ballista` | **Dread Ballista** | `Epic` (`#9B59B6`) | `FlatDamage = 28f, DamagePercent = 0.22f` | 150g / 75g | Miniature portable ballista turning invaders to paste. |
| 10 | `wpn_xbw_apocalypse_repeater` | **Apocalypse Repeater** | `Legendary` (`#F1C40F`) | `FlatDamage = 38f, DamagePercent = 0.32f, AttackSpeedPercent = 0.35f` | 300g / 150g | Gatling mechanism cycling soul-charged bolts. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Light Hand Crossbow (`wpn_xbw_light_hand_crossbow` — `Common`)
- **Lore & Stats:** Compact one-handed quarrel launcher. (`FlatDamage = 5f, AttackSpeedPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_light_hand_crossbow.png`):**
  > `16x16 pixel art weapon icon of "Light Hand Crossbow" (Crossbows, Repeaters & Ballistas, Common rarity): Compact one-handed quarrel launcher. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #95A5A6 rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_light_hand_crossbow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Light Hand Crossbow": Frame 1 windup glint in #95A5A6, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Iron Arbalest (`wpn_xbw_iron_arbalest` — `Common`)
- **Lore & Stats:** Steel prod delivering brutal punch. (`FlatDamage = 8f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_iron_arbalest.png`):**
  > `16x16 pixel art weapon icon of "Iron Arbalest" (Crossbows, Repeaters & Ballistas, Common rarity): Steel prod delivering brutal punch. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #95A5A6 rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_iron_arbalest_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Iron Arbalest": Frame 1 windup glint in #95A5A6, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Repeater Crossbow (`wpn_xbw_repeater_crossbow` — `Common`)
- **Lore & Stats:** Magazine-fed rapid-fire quarrel projector. (`FlatDamage = 6f, AttackSpeedPercent = 0.20f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_repeater_crossbow.png`):**
  > `16x16 pixel art weapon icon of "Repeater Crossbow" (Crossbows, Repeaters & Ballistas, Common rarity): Magazine-fed rapid-fire quarrel projector. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #95A5A6 rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_repeater_crossbow_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Repeater Crossbow": Frame 1 windup glint in #95A5A6, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Heavy Siege Arbalest (`wpn_xbw_heavy_siege_arbalest` — `Uncommon`)
- **Lore & Stats:** Winch-cranked arbalest punching iron shields. (`FlatDamage = 13f, DamagePercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_heavy_siege_arbalest.png`):**
  > `16x16 pixel art weapon icon of "Heavy Siege Arbalest" (Crossbows, Repeaters & Ballistas, Uncommon rarity): Winch-cranked arbalest punching iron shields. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #2ECC71 rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_heavy_siege_arbalest_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Heavy Siege Arbalest": Frame 1 windup glint in #2ECC71, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Barbed Bolt Caster (`wpn_xbw_barbed_bolt_caster` — `Uncommon`)
- **Lore & Stats:** Barbed bolts tearing through flesh. (`FlatDamage = 11f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_barbed_bolt_caster.png`):**
  > `16x16 pixel art weapon icon of "Barbed Bolt Caster" (Crossbows, Repeaters & Ballistas, Uncommon rarity): Barbed bolts tearing through flesh. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #2ECC71 rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_barbed_bolt_caster_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Barbed Bolt Caster": Frame 1 windup glint in #2ECC71, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Witch Hunter Repeater (`wpn_xbw_witch_hunter_repeater` — `Rare`)
- **Lore & Stats:** Blessed repeater firing silver tipped quarrels. (`FlatDamage = 16f, AttackSpeedPercent = 0.25f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_witch_hunter_repeater.png`):**
  > `16x16 pixel art weapon icon of "Witch Hunter Repeater" (Crossbows, Repeaters & Ballistas, Rare rarity): Blessed repeater firing silver tipped quarrels. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #3498DB rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_witch_hunter_repeater_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Witch Hunter Repeater": Frame 1 windup glint in #3498DB, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Consecrated Cross Caster (`wpn_xbw_consecrated_cross_caster` — `Rare`)
- **Lore & Stats:** Holy symbol firing radiant darts. (`FlatDamage = 17f, DamagePercent = 0.14f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_consecrated_cross_caster.png`):**
  > `16x16 pixel art weapon icon of "Consecrated Cross Caster" (Crossbows, Repeaters & Ballistas, Rare rarity): Holy symbol firing radiant darts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #3498DB rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_consecrated_cross_caster_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Consecrated Cross Caster": Frame 1 windup glint in #3498DB, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Twin Demon Arbalest (`wpn_xbw_twin_demon_arbalest` — `Epic`)
- **Lore & Stats:** Dual-barrel monstrosity launching twin heavy quarrels. (`FlatDamage = 24f, AttackSpeedPercent = 0.25f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_twin_demon_arbalest.png`):**
  > `16x16 pixel art weapon icon of "Twin Demon Arbalest" (Crossbows, Repeaters & Ballistas, Epic rarity): Dual-barrel monstrosity launching twin heavy quarrels. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #9B59B6 rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_twin_demon_arbalest_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Twin Demon Arbalest": Frame 1 windup glint in #9B59B6, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Dread Ballista (`wpn_xbw_dread_ballista` — `Epic`)
- **Lore & Stats:** Miniature portable ballista turning invaders to paste. (`FlatDamage = 28f, DamagePercent = 0.22f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_dread_ballista.png`):**
  > `16x16 pixel art weapon icon of "Dread Ballista" (Crossbows, Repeaters & Ballistas, Epic rarity): Miniature portable ballista turning invaders to paste. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #9B59B6 rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_dread_ballista_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Dread Ballista": Frame 1 windup glint in #9B59B6, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Apocalypse Repeater (`wpn_xbw_apocalypse_repeater` — `Legendary`)
- **Lore & Stats:** Gatling mechanism cycling soul-charged bolts. (`FlatDamage = 38f, DamagePercent = 0.32f, AttackSpeedPercent = 0.35f`).
- **16x16 Item Icon Prompt (`icon_wpn_xbw_apocalypse_repeater.png`):**
  > `16x16 pixel art weapon icon of "Apocalypse Repeater" (Crossbows, Repeaters & Ballistas, Legendary rarity): Gatling mechanism cycling soul-charged bolts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #6E7B85 with #F1C40F rarity accent and #E67E22 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_xbw_apocalypse_repeater_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Apocalypse Repeater": Frame 1 windup glint in #F1C40F, Frame 2 active crossbows, repeaters & ballistas swing/projectile in #E67E22 and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/crossbow/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_crossbow_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
