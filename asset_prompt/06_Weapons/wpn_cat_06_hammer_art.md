# Warhammers, Mauls & Flails (`WeaponCategory.Hammer`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #06/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Hammer` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Blunt-trauma blacksmith mallets, granite earthshakers, and stormhammers (blocky crushing head, radial shockwave ring).
- **Base Palette:** Primary Steel/Structure `#7F8C8D`, Signature Energy `#F1C40F`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#F1C40F` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Warhammers, Mauls & Flails` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_ham_blacksmith_mallet` | **Blacksmith Mallet** | `Common` (`#95A5A6`) | `FlatDamage = 7f` | 20g / 10g | Heavy iron mallet delivering blunt trauma. |
| 02 | `wpn_ham_stone_warhammer` | **Stone Warhammer** | `Common` (`#95A5A6`) | `FlatDamage = 9f, FlatArmor = 2f` | 25g / 12g | Chiseled granite warhammer ignoring armor plates. |
| 03 | `wpn_ham_heavy_mace` | **Heavy Mace** | `Common` (`#95A5A6`) | `FlatDamage = 8f, DamagePercent = 0.05f` | 25g / 12g | Flanged steel mace fracturing enemy limbs. |
| 04 | `wpn_ham_granite_smasher` | **Granite Smasher** | `Uncommon` (`#2ECC71`) | `FlatDamage = 13f, FlatArmor = 4f` | 50g / 25g | Enchanted bedrock hammer shattering stone. |
| 05 | `wpn_ham_thunder_mallet` | **Thunder Mallet** | `Uncommon` (`#2ECC71`) | `FlatDamage = 12f, DamagePercent = 0.10f` | 55g / 27g | Discharges concussive shockwaves upon impact. |
| 06 | `wpn_ham_earthshaker_maul` | **Earthshaker Maul** | `Rare` (`#3498DB`) | `FlatDamage = 19f, DamagePercent = 0.15f` | 85g / 42g | Two-handed maul sending fissures through arena floors. |
| 07 | `wpn_ham_concussive_flail` | **Concussive Flail** | `Rare` (`#3498DB`) | `FlatDamage = 18f, AttackSpeedPercent = 0.10f` | 80g / 40g | Spiked iron sphere bypassing enemy parries. |
| 08 | `wpn_ham_titan_anvil_hammer` | **Titan Anvil Hammer** | `Epic` (`#9B59B6`) | `FlatDamage = 27f, DamagePercent = 0.22f, FlatArmor = 8f` | 145g / 72g | Ancient forge hammer forged to reshape continents. |
| 09 | `wpn_ham_radiant_morningstar` | **Radiant Morningstar** | `Epic` (`#9B59B6`) | `FlatDamage = 26f, AttackSpeedPercent = 0.18f, LifeStealPercent = 0.08f` | 140g / 70g | Holy spiked morningstar purging demonic corruption. |
| 10 | `wpn_ham_mjolnir_of_the_deep` | **Mjolnir of the Deep** | `Legendary` (`#F1C40F`) | `FlatDamage = 40f, DamagePercent = 0.35f, FlatArmor = 10f` | 300g / 150g | Abyssal stormhammer unleashing tectonic crackles. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Blacksmith Mallet (`wpn_ham_blacksmith_mallet` — `Common`)
- **Lore & Stats:** Heavy iron mallet delivering blunt trauma. (`FlatDamage = 7f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_blacksmith_mallet.png`):**
  > `16x16 pixel art weapon icon of "Blacksmith Mallet" (Warhammers, Mauls & Flails, Common rarity): Heavy iron mallet delivering blunt trauma. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #95A5A6 rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_blacksmith_mallet_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Blacksmith Mallet": Frame 1 windup glint in #95A5A6, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Stone Warhammer (`wpn_ham_stone_warhammer` — `Common`)
- **Lore & Stats:** Chiseled granite warhammer ignoring armor plates. (`FlatDamage = 9f, FlatArmor = 2f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_stone_warhammer.png`):**
  > `16x16 pixel art weapon icon of "Stone Warhammer" (Warhammers, Mauls & Flails, Common rarity): Chiseled granite warhammer ignoring armor plates. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #95A5A6 rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_stone_warhammer_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Stone Warhammer": Frame 1 windup glint in #95A5A6, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Heavy Mace (`wpn_ham_heavy_mace` — `Common`)
- **Lore & Stats:** Flanged steel mace fracturing enemy limbs. (`FlatDamage = 8f, DamagePercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_heavy_mace.png`):**
  > `16x16 pixel art weapon icon of "Heavy Mace" (Warhammers, Mauls & Flails, Common rarity): Flanged steel mace fracturing enemy limbs. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #95A5A6 rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_heavy_mace_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Heavy Mace": Frame 1 windup glint in #95A5A6, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Granite Smasher (`wpn_ham_granite_smasher` — `Uncommon`)
- **Lore & Stats:** Enchanted bedrock hammer shattering stone. (`FlatDamage = 13f, FlatArmor = 4f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_granite_smasher.png`):**
  > `16x16 pixel art weapon icon of "Granite Smasher" (Warhammers, Mauls & Flails, Uncommon rarity): Enchanted bedrock hammer shattering stone. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #2ECC71 rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_granite_smasher_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Granite Smasher": Frame 1 windup glint in #2ECC71, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Thunder Mallet (`wpn_ham_thunder_mallet` — `Uncommon`)
- **Lore & Stats:** Discharges concussive shockwaves upon impact. (`FlatDamage = 12f, DamagePercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_thunder_mallet.png`):**
  > `16x16 pixel art weapon icon of "Thunder Mallet" (Warhammers, Mauls & Flails, Uncommon rarity): Discharges concussive shockwaves upon impact. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #2ECC71 rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_thunder_mallet_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Thunder Mallet": Frame 1 windup glint in #2ECC71, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Earthshaker Maul (`wpn_ham_earthshaker_maul` — `Rare`)
- **Lore & Stats:** Two-handed maul sending fissures through arena floors. (`FlatDamage = 19f, DamagePercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_earthshaker_maul.png`):**
  > `16x16 pixel art weapon icon of "Earthshaker Maul" (Warhammers, Mauls & Flails, Rare rarity): Two-handed maul sending fissures through arena floors. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #3498DB rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_earthshaker_maul_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Earthshaker Maul": Frame 1 windup glint in #3498DB, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Concussive Flail (`wpn_ham_concussive_flail` — `Rare`)
- **Lore & Stats:** Spiked iron sphere bypassing enemy parries. (`FlatDamage = 18f, AttackSpeedPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_concussive_flail.png`):**
  > `16x16 pixel art weapon icon of "Concussive Flail" (Warhammers, Mauls & Flails, Rare rarity): Spiked iron sphere bypassing enemy parries. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #3498DB rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_concussive_flail_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Concussive Flail": Frame 1 windup glint in #3498DB, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Titan Anvil Hammer (`wpn_ham_titan_anvil_hammer` — `Epic`)
- **Lore & Stats:** Ancient forge hammer forged to reshape continents. (`FlatDamage = 27f, DamagePercent = 0.22f, FlatArmor = 8f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_titan_anvil_hammer.png`):**
  > `16x16 pixel art weapon icon of "Titan Anvil Hammer" (Warhammers, Mauls & Flails, Epic rarity): Ancient forge hammer forged to reshape continents. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #9B59B6 rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_titan_anvil_hammer_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Titan Anvil Hammer": Frame 1 windup glint in #9B59B6, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Radiant Morningstar (`wpn_ham_radiant_morningstar` — `Epic`)
- **Lore & Stats:** Holy spiked morningstar purging demonic corruption. (`FlatDamage = 26f, AttackSpeedPercent = 0.18f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_radiant_morningstar.png`):**
  > `16x16 pixel art weapon icon of "Radiant Morningstar" (Warhammers, Mauls & Flails, Epic rarity): Holy spiked morningstar purging demonic corruption. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #9B59B6 rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_radiant_morningstar_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Radiant Morningstar": Frame 1 windup glint in #9B59B6, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Mjolnir of the Deep (`wpn_ham_mjolnir_of_the_deep` — `Legendary`)
- **Lore & Stats:** Abyssal stormhammer unleashing tectonic crackles. (`FlatDamage = 40f, DamagePercent = 0.35f, FlatArmor = 10f`).
- **16x16 Item Icon Prompt (`icon_wpn_ham_mjolnir_of_the_deep.png`):**
  > `16x16 pixel art weapon icon of "Mjolnir of the Deep" (Warhammers, Mauls & Flails, Legendary rarity): Abyssal stormhammer unleashing tectonic crackles. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7F8C8D with #F1C40F rarity accent and #F1C40F energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_ham_mjolnir_of_the_deep_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Mjolnir of the Deep": Frame 1 windup glint in #F1C40F, Frame 2 active warhammers, mauls & flails swing/projectile in #F1C40F and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/hammer/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_hammer_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
