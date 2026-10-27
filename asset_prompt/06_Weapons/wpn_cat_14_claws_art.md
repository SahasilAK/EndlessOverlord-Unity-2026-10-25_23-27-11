# Feral Gauntlets & Talons (`WeaponCategory.Claws`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #14/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Claws` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Three-pronged wrist-mounted predator claws, vampiric talons, and void rippers (3 parallel curved blades + gauntlet).
- **Base Palette:** Primary Steel/Structure `#902923`, Signature Energy `#FF3366`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#FF3366` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Feral Gauntlets & Talons` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_clw_iron_knuckles` | **Iron Knuckles** | `Common` (`#95A5A6`) | `FlatDamage = 4f, AttackSpeedPercent = 0.15f` | 20g / 10g | Reinforced fist guards for brutal close-quarters beatdowns. |
| 02 | `wpn_clw_spiked_fists` | **Spiked Fists** | `Common` (`#95A5A6`) | `FlatDamage = 5f, AttackSpeedPercent = 0.18f` | 25g / 12g | Steel spikes piercing deep upon every punch. |
| 03 | `wpn_clw_wolf_fang_claws` | **Wolf Fang Claws** | `Common` (`#95A5A6`) | `FlatDamage = 6f, MoveSpeedPercent = 0.06f, AttackSpeedPercent = 0.15f` | 25g / 12g | Serrated fangs strapped to leather gauntlets. |
| 04 | `wpn_clw_panther_talons` | **Panther Talons** | `Uncommon` (`#2ECC71`) | `FlatDamage = 8f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.08f` | 50g / 25g | Stealthy curved talons ripping jugulars. |
| 05 | `wpn_clw_poison_spurs` | **Poison Spurs** | `Uncommon` (`#2ECC71`) | `FlatDamage = 9f, AttackSpeedPercent = 0.20f` | 50g / 25g | Hollow needles injecting paralytic toxin. |
| 06 | `wpn_clw_blood_ripper_claws` | **Blood Ripper Claws** | `Rare` (`#3498DB`) | `FlatDamage = 12f, AttackSpeedPercent = 0.28f, LifeStealPercent = 0.08f` | 80g / 40g | Tears arterial wounds restoring user vitality. |
| 07 | `wpn_clw_demonic_talons` | **Demonic Talons** | `Rare` (`#3498DB`) | `FlatDamage = 14f, DamagePercent = 0.12f, AttackSpeedPercent = 0.25f` | 85g / 42g | Brimstone talons rending flesh and armor alike. |
| 08 | `wpn_clw_dragon_rending_claws` | **Dragon Rending Claws** | `Epic` (`#9B59B6`) | `FlatDamage = 20f, DamagePercent = 0.20f, AttackSpeedPercent = 0.32f` | 140g / 70g | Claws of ancient drakes slicing diamond shields. |
| 09 | `wpn_clw_shadow_rend_fists` | **Shadow Rend Fists** | `Epic` (`#9B59B6`) | `FlatDamage = 21f, AttackSpeedPercent = 0.38f, MoveSpeedPercent = 0.12f` | 150g / 75g | Disappears into shadow between frantic tearing strikes. |
| 10 | `wpn_clw_claws_of_the_beast_god` | **Claws of the Beast God** | `Legendary` (`#F1C40F`) | `FlatDamage = 30f, DamagePercent = 0.28f, AttackSpeedPercent = 0.45f, LifeStealPercent = 0.12f` | 280g / 140g | Primal divine wrath condensed into rending claws. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Iron Knuckles (`wpn_clw_iron_knuckles` — `Common`)
- **Lore & Stats:** Reinforced fist guards for brutal close-quarters beatdowns. (`FlatDamage = 4f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_iron_knuckles.png`):**
  > `16x16 pixel art weapon icon of "Iron Knuckles" (Feral Gauntlets & Talons, Common rarity): Reinforced fist guards for brutal close-quarters beatdowns. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #95A5A6 rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_iron_knuckles_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Iron Knuckles": Frame 1 windup glint in #95A5A6, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Spiked Fists (`wpn_clw_spiked_fists` — `Common`)
- **Lore & Stats:** Steel spikes piercing deep upon every punch. (`FlatDamage = 5f, AttackSpeedPercent = 0.18f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_spiked_fists.png`):**
  > `16x16 pixel art weapon icon of "Spiked Fists" (Feral Gauntlets & Talons, Common rarity): Steel spikes piercing deep upon every punch. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #95A5A6 rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_spiked_fists_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Spiked Fists": Frame 1 windup glint in #95A5A6, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Wolf Fang Claws (`wpn_clw_wolf_fang_claws` — `Common`)
- **Lore & Stats:** Serrated fangs strapped to leather gauntlets. (`FlatDamage = 6f, MoveSpeedPercent = 0.06f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_wolf_fang_claws.png`):**
  > `16x16 pixel art weapon icon of "Wolf Fang Claws" (Feral Gauntlets & Talons, Common rarity): Serrated fangs strapped to leather gauntlets. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #95A5A6 rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_wolf_fang_claws_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Wolf Fang Claws": Frame 1 windup glint in #95A5A6, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Panther Talons (`wpn_clw_panther_talons` — `Uncommon`)
- **Lore & Stats:** Stealthy curved talons ripping jugulars. (`FlatDamage = 8f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_panther_talons.png`):**
  > `16x16 pixel art weapon icon of "Panther Talons" (Feral Gauntlets & Talons, Uncommon rarity): Stealthy curved talons ripping jugulars. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #2ECC71 rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_panther_talons_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Panther Talons": Frame 1 windup glint in #2ECC71, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Poison Spurs (`wpn_clw_poison_spurs` — `Uncommon`)
- **Lore & Stats:** Hollow needles injecting paralytic toxin. (`FlatDamage = 9f, AttackSpeedPercent = 0.20f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_poison_spurs.png`):**
  > `16x16 pixel art weapon icon of "Poison Spurs" (Feral Gauntlets & Talons, Uncommon rarity): Hollow needles injecting paralytic toxin. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #2ECC71 rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_poison_spurs_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Poison Spurs": Frame 1 windup glint in #2ECC71, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Blood Ripper Claws (`wpn_clw_blood_ripper_claws` — `Rare`)
- **Lore & Stats:** Tears arterial wounds restoring user vitality. (`FlatDamage = 12f, AttackSpeedPercent = 0.28f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_blood_ripper_claws.png`):**
  > `16x16 pixel art weapon icon of "Blood Ripper Claws" (Feral Gauntlets & Talons, Rare rarity): Tears arterial wounds restoring user vitality. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #3498DB rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_blood_ripper_claws_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Blood Ripper Claws": Frame 1 windup glint in #3498DB, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Demonic Talons (`wpn_clw_demonic_talons` — `Rare`)
- **Lore & Stats:** Brimstone talons rending flesh and armor alike. (`FlatDamage = 14f, DamagePercent = 0.12f, AttackSpeedPercent = 0.25f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_demonic_talons.png`):**
  > `16x16 pixel art weapon icon of "Demonic Talons" (Feral Gauntlets & Talons, Rare rarity): Brimstone talons rending flesh and armor alike. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #3498DB rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_demonic_talons_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Demonic Talons": Frame 1 windup glint in #3498DB, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Dragon Rending Claws (`wpn_clw_dragon_rending_claws` — `Epic`)
- **Lore & Stats:** Claws of ancient drakes slicing diamond shields. (`FlatDamage = 20f, DamagePercent = 0.20f, AttackSpeedPercent = 0.32f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_dragon_rending_claws.png`):**
  > `16x16 pixel art weapon icon of "Dragon Rending Claws" (Feral Gauntlets & Talons, Epic rarity): Claws of ancient drakes slicing diamond shields. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #9B59B6 rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_dragon_rending_claws_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Dragon Rending Claws": Frame 1 windup glint in #9B59B6, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Shadow Rend Fists (`wpn_clw_shadow_rend_fists` — `Epic`)
- **Lore & Stats:** Disappears into shadow between frantic tearing strikes. (`FlatDamage = 21f, AttackSpeedPercent = 0.38f, MoveSpeedPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_shadow_rend_fists.png`):**
  > `16x16 pixel art weapon icon of "Shadow Rend Fists" (Feral Gauntlets & Talons, Epic rarity): Disappears into shadow between frantic tearing strikes. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #9B59B6 rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_shadow_rend_fists_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Shadow Rend Fists": Frame 1 windup glint in #9B59B6, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Claws of the Beast God (`wpn_clw_claws_of_the_beast_god` — `Legendary`)
- **Lore & Stats:** Primal divine wrath condensed into rending claws. (`FlatDamage = 30f, DamagePercent = 0.28f, AttackSpeedPercent = 0.45f, LifeStealPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_clw_claws_of_the_beast_god.png`):**
  > `16x16 pixel art weapon icon of "Claws of the Beast God" (Feral Gauntlets & Talons, Legendary rarity): Primal divine wrath condensed into rending claws. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #902923 with #F1C40F rarity accent and #FF3366 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_clw_claws_of_the_beast_god_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Claws of the Beast God": Frame 1 windup glint in #F1C40F, Frame 2 active feral gauntlets & talons swing/projectile in #FF3366 and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/claws/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_claws_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
