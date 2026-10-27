# Arcane, Elemental & Necrotic Staves (`WeaponCategory.Staff`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #09/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Staff` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Full-length archmage staves crowned with floating skulls, crystals, or elemental cages (tall diagonal shaft with glowing head).
- **Base Palette:** Primary Steel/Structure `#7D5BA6`, Signature Energy `#30E680`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#30E680` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Arcane, Elemental & Necrotic Staves` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_stf_carved_oak_staff` | **Carved Oak Staff** | `Common` (`#95A5A6`) | `FlatDamage = 5f, CooldownReductionPercent = 0.05f` | 20g / 10g | Gnarled oak focusing novice incantations. |
| 02 | `wpn_stf_apprentice_cane` | **Apprentice Cane** | `Common` (`#95A5A6`) | `FlatDamage = 6f, CooldownReductionPercent = 0.06f` | 25g / 12g | Lightweight wizard cane boosting mana circulation. |
| 03 | `wpn_stf_iron_capped_quarterstaff` | **Iron Capped Quarterstaff** | `Common` (`#95A5A6`) | `FlatDamage = 7f, AttackSpeedPercent = 0.05f` | 25g / 12g | Martial staff doubling as focus and bludgeon. |
| 04 | `wpn_stf_flame_heart_stave` | **Flame Heart Stave** | `Uncommon` (`#2ECC71`) | `FlatDamage = 10f, DamagePercent = 0.10f` | 50g / 25g | Ember-topped staff scorching enemies with every blast. |
| 05 | `wpn_stf_glacier_staff` | **Glacier Staff** | `Uncommon` (`#2ECC71`) | `FlatDamage = 9f, CooldownReductionPercent = 0.10f` | 50g / 25g | Carved from blue glacial ice that never melts. |
| 06 | `wpn_stf_thunder_stave` | **Thunder Stave** | `Rare` (`#3498DB`) | `FlatDamage = 14f, AttackSpeedPercent = 0.15f, DamagePercent = 0.10f` | 80g / 40g | Conducts atmospheric static into crackling bolts. |
| 07 | `wpn_stf_druidic_totem_staff` | **Druidic Totem Staff** | `Rare` (`#3498DB`) | `FlatDamage = 13f, LifeStealPercent = 0.08f, FlatMaxHealth = 30f` | 85g / 42g | Sprouts verdant vines restoring lifeforce. |
| 08 | `wpn_stf_astral_archmage_staff` | **Astral Archmage Staff** | `Epic` (`#9B59B6`) | `FlatDamage = 21f, DamagePercent = 0.22f, CooldownReductionPercent = 0.15f` | 140g / 70g | Crowned with orbiting stellar rings. |
| 09 | `wpn_stf_dread_necromancer_staff` | **Dread Necromancer Staff** | `Epic` (`#9B59B6`) | `FlatDamage = 23f, DamagePercent = 0.20f, LifeStealPercent = 0.10f` | 150g / 75g | Carved with shrieking skulls fueling dark arts. |
| 10 | `wpn_stf_staff_of_creation` | **Staff of Creation** | `Legendary` (`#F1C40F`) | `FlatDamage = 35f, DamagePercent = 0.35f, CooldownReductionPercent = 0.20f, LifeStealPercent = 0.12f` | 300g / 150g | Primordial relic containing the genesis spark of worlds. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Carved Oak Staff (`wpn_stf_carved_oak_staff` — `Common`)
- **Lore & Stats:** Gnarled oak focusing novice incantations. (`FlatDamage = 5f, CooldownReductionPercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_carved_oak_staff.png`):**
  > `16x16 pixel art weapon icon of "Carved Oak Staff" (Arcane, Elemental & Necrotic Staves, Common rarity): Gnarled oak focusing novice incantations. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #95A5A6 rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_carved_oak_staff_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Carved Oak Staff": Frame 1 windup glint in #95A5A6, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Apprentice Cane (`wpn_stf_apprentice_cane` — `Common`)
- **Lore & Stats:** Lightweight wizard cane boosting mana circulation. (`FlatDamage = 6f, CooldownReductionPercent = 0.06f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_apprentice_cane.png`):**
  > `16x16 pixel art weapon icon of "Apprentice Cane" (Arcane, Elemental & Necrotic Staves, Common rarity): Lightweight wizard cane boosting mana circulation. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #95A5A6 rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_apprentice_cane_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Apprentice Cane": Frame 1 windup glint in #95A5A6, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Iron Capped Quarterstaff (`wpn_stf_iron_capped_quarterstaff` — `Common`)
- **Lore & Stats:** Martial staff doubling as focus and bludgeon. (`FlatDamage = 7f, AttackSpeedPercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_iron_capped_quarterstaff.png`):**
  > `16x16 pixel art weapon icon of "Iron Capped Quarterstaff" (Arcane, Elemental & Necrotic Staves, Common rarity): Martial staff doubling as focus and bludgeon. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #95A5A6 rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_iron_capped_quarterstaff_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Iron Capped Quarterstaff": Frame 1 windup glint in #95A5A6, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Flame Heart Stave (`wpn_stf_flame_heart_stave` — `Uncommon`)
- **Lore & Stats:** Ember-topped staff scorching enemies with every blast. (`FlatDamage = 10f, DamagePercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_flame_heart_stave.png`):**
  > `16x16 pixel art weapon icon of "Flame Heart Stave" (Arcane, Elemental & Necrotic Staves, Uncommon rarity): Ember-topped staff scorching enemies with every blast. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #2ECC71 rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_flame_heart_stave_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Flame Heart Stave": Frame 1 windup glint in #2ECC71, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Glacier Staff (`wpn_stf_glacier_staff` — `Uncommon`)
- **Lore & Stats:** Carved from blue glacial ice that never melts. (`FlatDamage = 9f, CooldownReductionPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_glacier_staff.png`):**
  > `16x16 pixel art weapon icon of "Glacier Staff" (Arcane, Elemental & Necrotic Staves, Uncommon rarity): Carved from blue glacial ice that never melts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #2ECC71 rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_glacier_staff_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Glacier Staff": Frame 1 windup glint in #2ECC71, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Thunder Stave (`wpn_stf_thunder_stave` — `Rare`)
- **Lore & Stats:** Conducts atmospheric static into crackling bolts. (`FlatDamage = 14f, AttackSpeedPercent = 0.15f, DamagePercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_thunder_stave.png`):**
  > `16x16 pixel art weapon icon of "Thunder Stave" (Arcane, Elemental & Necrotic Staves, Rare rarity): Conducts atmospheric static into crackling bolts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #3498DB rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_thunder_stave_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Thunder Stave": Frame 1 windup glint in #3498DB, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Druidic Totem Staff (`wpn_stf_druidic_totem_staff` — `Rare`)
- **Lore & Stats:** Sprouts verdant vines restoring lifeforce. (`FlatDamage = 13f, LifeStealPercent = 0.08f, FlatMaxHealth = 30f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_druidic_totem_staff.png`):**
  > `16x16 pixel art weapon icon of "Druidic Totem Staff" (Arcane, Elemental & Necrotic Staves, Rare rarity): Sprouts verdant vines restoring lifeforce. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #3498DB rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_druidic_totem_staff_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Druidic Totem Staff": Frame 1 windup glint in #3498DB, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Astral Archmage Staff (`wpn_stf_astral_archmage_staff` — `Epic`)
- **Lore & Stats:** Crowned with orbiting stellar rings. (`FlatDamage = 21f, DamagePercent = 0.22f, CooldownReductionPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_astral_archmage_staff.png`):**
  > `16x16 pixel art weapon icon of "Astral Archmage Staff" (Arcane, Elemental & Necrotic Staves, Epic rarity): Crowned with orbiting stellar rings. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #9B59B6 rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_astral_archmage_staff_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Astral Archmage Staff": Frame 1 windup glint in #9B59B6, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Dread Necromancer Staff (`wpn_stf_dread_necromancer_staff` — `Epic`)
- **Lore & Stats:** Carved with shrieking skulls fueling dark arts. (`FlatDamage = 23f, DamagePercent = 0.20f, LifeStealPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_dread_necromancer_staff.png`):**
  > `16x16 pixel art weapon icon of "Dread Necromancer Staff" (Arcane, Elemental & Necrotic Staves, Epic rarity): Carved with shrieking skulls fueling dark arts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #9B59B6 rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_dread_necromancer_staff_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Dread Necromancer Staff": Frame 1 windup glint in #9B59B6, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Staff of Creation (`wpn_stf_staff_of_creation` — `Legendary`)
- **Lore & Stats:** Primordial relic containing the genesis spark of worlds. (`FlatDamage = 35f, DamagePercent = 0.35f, CooldownReductionPercent = 0.20f, LifeStealPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_stf_staff_of_creation.png`):**
  > `16x16 pixel art weapon icon of "Staff of Creation" (Arcane, Elemental & Necrotic Staves, Legendary rarity): Primordial relic containing the genesis spark of worlds. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #7D5BA6 with #F1C40F rarity accent and #30E680 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_stf_staff_of_creation_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Staff of Creation": Frame 1 windup glint in #F1C40F, Frame 2 active arcane, elemental & necrotic staves swing/projectile in #30E680 and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/staff/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_staff_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
