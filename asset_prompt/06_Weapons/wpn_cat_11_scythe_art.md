# Reaper War-Scythes (`WeaponCategory.Scythe`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #11/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Scythe` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Sweeping curved executioner blades and soul-harvesters (`L`-shaped snath with long crescent reaper blade).
- **Base Palette:** Primary Steel/Structure `#4A3B5C`, Signature Energy `#B366FF`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#B366FF` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Reaper War-Scythes` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_scy_farmer_scythe` | **Farmer Scythe** | `Common` (`#95A5A6`) | `FlatDamage = 7f, AttackSpeedPercent = 0.04f` | 20g / 10g | Curved agrarian blade repurposed for butchering invaders. |
| 02 | `wpn_scy_reaper_sickle` | **Reaper Sickle** | `Common` (`#95A5A6`) | `FlatDamage = 6f, AttackSpeedPercent = 0.12f` | 25g / 12g | Hooked sickle ideal for swift execution sweeps. |
| 03 | `wpn_scy_bone_sickle` | **Bone Sickle** | `Common` (`#95A5A6`) | `FlatDamage = 8f, LifeStealPercent = 0.04f` | 25g / 12g | Fashioned from hardened pelvis and rib bone. |
| 04 | `wpn_scy_blood_crescent` | **Blood Crescent** | `Uncommon` (`#2ECC71`) | `FlatDamage = 11f, LifeStealPercent = 0.08f` | 50g / 25g | Deep red crescent blade drinking spilled vitals. |
| 05 | `wpn_scy_grave_harvester` | **Grave Harvester** | `Uncommon` (`#2ECC71`) | `FlatDamage = 12f, DamagePercent = 0.08f` | 55g / 27g | Heavy cemetery scythe cleaving broad arcs. |
| 06 | `item_death_scythe` | **Death Scythe** | `Rare` (`#3498DB`) | `FlatDamage = 10f, LifeStealPercent = 0.12f` | 50g / 25g | A cruel scythe blade that harvests the life essence of struck foes. |
| 07 | `wpn_scy_soul_collector` | **Soul Collector** | `Rare` (`#3498DB`) | `FlatDamage = 16f, LifeStealPercent = 0.10f, AttackSpeedPercent = 0.10f` | 85g / 42g | Traps the screams of fallen heroes in misty trails. |
| 08 | `wpn_scy_abyssal_war_scythe` | **Abyssal War Scythe** | `Epic` (`#9B59B6`) | `FlatDamage = 23f, DamagePercent = 0.20f, LifeStealPercent = 0.14f` | 145g / 72g | Forged from midnight ore to harvest entire armies. |
| 09 | `wpn_scy_grim_judgment` | **Grim Judgment** | `Epic` (`#9B59B6`) | `FlatDamage = 25f, DamagePercent = 0.22f, AttackSpeedPercent = 0.15f` | 155g / 77g | The embodiment of unavoidable mortality. |
| 10 | `wpn_scy_scythe_of_the_final_hour` | **Scythe of the Final Hour** | `Legendary` (`#F1C40F`) | `FlatDamage = 38f, DamagePercent = 0.32f, LifeStealPercent = 0.18f, AttackSpeedPercent = 0.20f` | 300g / 150g | Cuts the thread of fate itself. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Farmer Scythe (`wpn_scy_farmer_scythe` — `Common`)
- **Lore & Stats:** Curved agrarian blade repurposed for butchering invaders. (`FlatDamage = 7f, AttackSpeedPercent = 0.04f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_farmer_scythe.png`):**
  > `16x16 pixel art weapon icon of "Farmer Scythe" (Reaper War-Scythes, Common rarity): Curved agrarian blade repurposed for butchering invaders. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #95A5A6 rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_farmer_scythe_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Farmer Scythe": Frame 1 windup glint in #95A5A6, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Reaper Sickle (`wpn_scy_reaper_sickle` — `Common`)
- **Lore & Stats:** Hooked sickle ideal for swift execution sweeps. (`FlatDamage = 6f, AttackSpeedPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_reaper_sickle.png`):**
  > `16x16 pixel art weapon icon of "Reaper Sickle" (Reaper War-Scythes, Common rarity): Hooked sickle ideal for swift execution sweeps. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #95A5A6 rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_reaper_sickle_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Reaper Sickle": Frame 1 windup glint in #95A5A6, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Bone Sickle (`wpn_scy_bone_sickle` — `Common`)
- **Lore & Stats:** Fashioned from hardened pelvis and rib bone. (`FlatDamage = 8f, LifeStealPercent = 0.04f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_bone_sickle.png`):**
  > `16x16 pixel art weapon icon of "Bone Sickle" (Reaper War-Scythes, Common rarity): Fashioned from hardened pelvis and rib bone. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #95A5A6 rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_bone_sickle_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Bone Sickle": Frame 1 windup glint in #95A5A6, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Blood Crescent (`wpn_scy_blood_crescent` — `Uncommon`)
- **Lore & Stats:** Deep red crescent blade drinking spilled vitals. (`FlatDamage = 11f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_blood_crescent.png`):**
  > `16x16 pixel art weapon icon of "Blood Crescent" (Reaper War-Scythes, Uncommon rarity): Deep red crescent blade drinking spilled vitals. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #2ECC71 rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_blood_crescent_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Blood Crescent": Frame 1 windup glint in #2ECC71, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Grave Harvester (`wpn_scy_grave_harvester` — `Uncommon`)
- **Lore & Stats:** Heavy cemetery scythe cleaving broad arcs. (`FlatDamage = 12f, DamagePercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_grave_harvester.png`):**
  > `16x16 pixel art weapon icon of "Grave Harvester" (Reaper War-Scythes, Uncommon rarity): Heavy cemetery scythe cleaving broad arcs. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #2ECC71 rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_grave_harvester_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Grave Harvester": Frame 1 windup glint in #2ECC71, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Death Scythe (`item_death_scythe` — `Rare`)
- **Lore & Stats:** A cruel scythe blade that harvests the life essence of struck foes. (`FlatDamage = 10f, LifeStealPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_item_death_scythe.png`):**
  > `16x16 pixel art weapon icon of "Death Scythe" (Reaper War-Scythes, Rare rarity): A cruel scythe blade that harvests the life essence of struck foes. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #3498DB rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_item_death_scythe_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Death Scythe": Frame 1 windup glint in #3498DB, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Soul Collector (`wpn_scy_soul_collector` — `Rare`)
- **Lore & Stats:** Traps the screams of fallen heroes in misty trails. (`FlatDamage = 16f, LifeStealPercent = 0.10f, AttackSpeedPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_soul_collector.png`):**
  > `16x16 pixel art weapon icon of "Soul Collector" (Reaper War-Scythes, Rare rarity): Traps the screams of fallen heroes in misty trails. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #3498DB rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_soul_collector_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Soul Collector": Frame 1 windup glint in #3498DB, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Abyssal War Scythe (`wpn_scy_abyssal_war_scythe` — `Epic`)
- **Lore & Stats:** Forged from midnight ore to harvest entire armies. (`FlatDamage = 23f, DamagePercent = 0.20f, LifeStealPercent = 0.14f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_abyssal_war_scythe.png`):**
  > `16x16 pixel art weapon icon of "Abyssal War Scythe" (Reaper War-Scythes, Epic rarity): Forged from midnight ore to harvest entire armies. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #9B59B6 rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_abyssal_war_scythe_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Abyssal War Scythe": Frame 1 windup glint in #9B59B6, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Grim Judgment (`wpn_scy_grim_judgment` — `Epic`)
- **Lore & Stats:** The embodiment of unavoidable mortality. (`FlatDamage = 25f, DamagePercent = 0.22f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_grim_judgment.png`):**
  > `16x16 pixel art weapon icon of "Grim Judgment" (Reaper War-Scythes, Epic rarity): The embodiment of unavoidable mortality. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #9B59B6 rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_grim_judgment_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Grim Judgment": Frame 1 windup glint in #9B59B6, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Scythe of the Final Hour (`wpn_scy_scythe_of_the_final_hour` — `Legendary`)
- **Lore & Stats:** Cuts the thread of fate itself. (`FlatDamage = 38f, DamagePercent = 0.32f, LifeStealPercent = 0.18f, AttackSpeedPercent = 0.20f`).
- **16x16 Item Icon Prompt (`icon_wpn_scy_scythe_of_the_final_hour.png`):**
  > `16x16 pixel art weapon icon of "Scythe of the Final Hour" (Reaper War-Scythes, Legendary rarity): Cuts the thread of fate itself. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #4A3B5C with #F1C40F rarity accent and #B366FF energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_scy_scythe_of_the_final_hour_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Scythe of the Final Hour": Frame 1 windup glint in #F1C40F, Frame 2 active reaper war-scythes swing/projectile in #B366FF and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/scythe/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_scythe_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
