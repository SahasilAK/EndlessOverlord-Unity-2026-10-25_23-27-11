# One-Handed Swords (`WeaponCategory.OneHandedSword`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #01/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.OneHandedSword` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Balanced double-edged martial blades and rapiers (`45°` diagonal icon orientation, sweeping `180°` slash arc).
- **Base Palette:** Primary Steel/Structure `#C8D0D8`, Signature Energy `#4A90E2`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#4A90E2` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `One-Handed Swords` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_1h_iron_broadsword` | **Iron Broadsword** | `Common` (`#95A5A6`) | `FlatDamage = 5f, AttackSpeedPercent = 0.05f` | 20g / 10g | Standard reliable soldier blade. |
| 02 | `wpn_1h_steel_arming_sword` | **Steel Arming Sword** | `Common` (`#95A5A6`) | `FlatDamage = 7f, AttackSpeedPercent = 0.05f` | 25g / 12g | Balanced double-edged arming blade. |
| 03 | `wpn_1h_soldier_gladius` | **Soldier Gladius** | `Common` (`#95A5A6`) | `FlatDamage = 6f, AttackSpeedPercent = 0.12f` | 25g / 12g | Short thrusting gladius with quick recovery. |
| 04 | `wpn_1h_flame_brand_blade` | **Flamebrand Blade** | `Uncommon` (`#2ECC71`) | `FlatDamage = 9f, DamagePercent = 0.08f` | 40g / 20g | Serrated sword pulsing with latent heat. |
| 05 | `wpn_1h_tempest_cutlass` | **Tempest Cutlass** | `Uncommon` (`#2ECC71`) | `FlatDamage = 8f, MoveSpeedPercent = 0.08f, AttackSpeedPercent = 0.10f` | 45g / 22g | Curved blade whistling like coastal gales. |
| 06 | `wpn_1h_frostbite_saber` | **Frostbite Saber** | `Rare` (`#3498DB`) | `FlatDamage = 13f, AttackSpeedPercent = 0.15f` | 70g / 35g | Crystalline steel chilling flesh on contact. |
| 07 | `wpn_1h_runed_broadsword` | **Runed Broadsword** | `Rare` (`#3498DB`) | `FlatDamage = 14f, DamagePercent = 0.12f, CooldownReductionPercent = 0.08f` | 75g / 37g | Carved with ancient battle glyphs. |
| 08 | `wpn_1h_sunforged_rapier` | **Sunforged Rapier** | `Epic` (`#9B59B6`) | `FlatDamage = 19f, AttackSpeedPercent = 0.25f, LifeStealPercent = 0.08f` | 120g / 60g | Blinding solar needle piercing enemy armor. |
| 09 | `wpn_1h_dread_executioner` | **Dread Executioner** | `Epic` (`#9B59B6`) | `FlatDamage = 22f, DamagePercent = 0.18f` | 130g / 65g | Heavy notched blade cleaving bone clean. |
| 10 | `wpn_1h_excalibur_of_shadows` | **Excalibur of Shadows** | `Legendary` (`#F1C40F`) | `FlatDamage = 32f, DamagePercent = 0.28f, AttackSpeedPercent = 0.20f, LifeStealPercent = 0.12f` | 250g / 125g | Legendary umbral sword crowned with void energy. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Iron Broadsword (`wpn_1h_iron_broadsword` — `Common`)
- **Lore & Stats:** Standard reliable soldier blade. (`FlatDamage = 5f, AttackSpeedPercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_iron_broadsword.png`):**
  > `16x16 pixel art weapon icon of "Iron Broadsword" (One-Handed Swords, Common rarity): Standard reliable soldier blade. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #95A5A6 rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_iron_broadsword_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Iron Broadsword": Frame 1 windup glint in #95A5A6, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Steel Arming Sword (`wpn_1h_steel_arming_sword` — `Common`)
- **Lore & Stats:** Balanced double-edged arming blade. (`FlatDamage = 7f, AttackSpeedPercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_steel_arming_sword.png`):**
  > `16x16 pixel art weapon icon of "Steel Arming Sword" (One-Handed Swords, Common rarity): Balanced double-edged arming blade. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #95A5A6 rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_steel_arming_sword_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Steel Arming Sword": Frame 1 windup glint in #95A5A6, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Soldier Gladius (`wpn_1h_soldier_gladius` — `Common`)
- **Lore & Stats:** Short thrusting gladius with quick recovery. (`FlatDamage = 6f, AttackSpeedPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_soldier_gladius.png`):**
  > `16x16 pixel art weapon icon of "Soldier Gladius" (One-Handed Swords, Common rarity): Short thrusting gladius with quick recovery. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #95A5A6 rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_soldier_gladius_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Soldier Gladius": Frame 1 windup glint in #95A5A6, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Flamebrand Blade (`wpn_1h_flame_brand_blade` — `Uncommon`)
- **Lore & Stats:** Serrated sword pulsing with latent heat. (`FlatDamage = 9f, DamagePercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_flame_brand_blade.png`):**
  > `16x16 pixel art weapon icon of "Flamebrand Blade" (One-Handed Swords, Uncommon rarity): Serrated sword pulsing with latent heat. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #2ECC71 rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_flame_brand_blade_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Flamebrand Blade": Frame 1 windup glint in #2ECC71, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Tempest Cutlass (`wpn_1h_tempest_cutlass` — `Uncommon`)
- **Lore & Stats:** Curved blade whistling like coastal gales. (`FlatDamage = 8f, MoveSpeedPercent = 0.08f, AttackSpeedPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_tempest_cutlass.png`):**
  > `16x16 pixel art weapon icon of "Tempest Cutlass" (One-Handed Swords, Uncommon rarity): Curved blade whistling like coastal gales. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #2ECC71 rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_tempest_cutlass_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Tempest Cutlass": Frame 1 windup glint in #2ECC71, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Frostbite Saber (`wpn_1h_frostbite_saber` — `Rare`)
- **Lore & Stats:** Crystalline steel chilling flesh on contact. (`FlatDamage = 13f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_frostbite_saber.png`):**
  > `16x16 pixel art weapon icon of "Frostbite Saber" (One-Handed Swords, Rare rarity): Crystalline steel chilling flesh on contact. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #3498DB rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_frostbite_saber_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Frostbite Saber": Frame 1 windup glint in #3498DB, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Runed Broadsword (`wpn_1h_runed_broadsword` — `Rare`)
- **Lore & Stats:** Carved with ancient battle glyphs. (`FlatDamage = 14f, DamagePercent = 0.12f, CooldownReductionPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_runed_broadsword.png`):**
  > `16x16 pixel art weapon icon of "Runed Broadsword" (One-Handed Swords, Rare rarity): Carved with ancient battle glyphs. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #3498DB rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_runed_broadsword_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Runed Broadsword": Frame 1 windup glint in #3498DB, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Sunforged Rapier (`wpn_1h_sunforged_rapier` — `Epic`)
- **Lore & Stats:** Blinding solar needle piercing enemy armor. (`FlatDamage = 19f, AttackSpeedPercent = 0.25f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_sunforged_rapier.png`):**
  > `16x16 pixel art weapon icon of "Sunforged Rapier" (One-Handed Swords, Epic rarity): Blinding solar needle piercing enemy armor. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #9B59B6 rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_sunforged_rapier_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Sunforged Rapier": Frame 1 windup glint in #9B59B6, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Dread Executioner (`wpn_1h_dread_executioner` — `Epic`)
- **Lore & Stats:** Heavy notched blade cleaving bone clean. (`FlatDamage = 22f, DamagePercent = 0.18f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_dread_executioner.png`):**
  > `16x16 pixel art weapon icon of "Dread Executioner" (One-Handed Swords, Epic rarity): Heavy notched blade cleaving bone clean. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #9B59B6 rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_dread_executioner_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Dread Executioner": Frame 1 windup glint in #9B59B6, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Excalibur of Shadows (`wpn_1h_excalibur_of_shadows` — `Legendary`)
- **Lore & Stats:** Legendary umbral sword crowned with void energy. (`FlatDamage = 32f, DamagePercent = 0.28f, AttackSpeedPercent = 0.20f, LifeStealPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_1h_excalibur_of_shadows.png`):**
  > `16x16 pixel art weapon icon of "Excalibur of Shadows" (One-Handed Swords, Legendary rarity): Legendary umbral sword crowned with void energy. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #C8D0D8 with #F1C40F rarity accent and #4A90E2 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_1h_excalibur_of_shadows_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Excalibur of Shadows": Frame 1 windup glint in #F1C40F, Frame 2 active one-handed swords swing/projectile in #4A90E2 and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/one_handed_sword/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_one_handed_sword_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
