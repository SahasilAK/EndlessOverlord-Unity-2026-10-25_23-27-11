# Daggers & Stilettos (`WeaponCategory.Dagger`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #03/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Dagger` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Twin-capable assassin knives, venomous fangs, and void needles (compact needle silhouette, rapid piercing stabs).
- **Base Palette:** Primary Steel/Structure `#8E99A4`, Signature Energy `#9B59B6`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#9B59B6` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Daggers & Stilettos` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_dag_rusty_shiv` | **Rusty Shiv** | `Common` (`#95A5A6`) | `FlatDamage = 3f, AttackSpeedPercent = 0.15f` | 15g / 7g | Pitted crude iron knife. |
| 02 | `wpn_dag_iron_stiletto` | **Iron Stiletto** | `Common` (`#95A5A6`) | `FlatDamage = 4f, AttackSpeedPercent = 0.20f` | 20g / 10g | Needle point slipping through armor seams. |
| 03 | `wpn_dag_assassin_knife` | **Assassin Knife** | `Common` (`#95A5A6`) | `FlatDamage = 5f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.05f` | 25g / 12g | Darkened rogue weapon weighted for critical stabs. |
| 04 | `wpn_dag_venomous_fang` | **Venomous Fang** | `Uncommon` (`#2ECC71`) | `FlatDamage = 7f, AttackSpeedPercent = 0.25f` | 45g / 22g | Dripping caustic serpent venom. |
| 05 | `wpn_dag_shadow_quill` | **Shadow Quill** | `Uncommon` (`#2ECC71`) | `FlatDamage = 6f, MoveSpeedPercent = 0.10f, AttackSpeedPercent = 0.25f` | 50g / 25g | Featherweight blade sliding between shadows. |
| 06 | `wpn_dag_blood_letter` | **Blood Letter** | `Rare` (`#3498DB`) | `FlatDamage = 10f, AttackSpeedPercent = 0.30f, LifeStealPercent = 0.08f` | 75g / 37g | Grooved parrying dagger feeding upon wounds. |
| 07 | `wpn_dag_phantom_parry_dagger` | **Phantom Parry Dagger** | `Rare` (`#3498DB`) | `FlatDamage = 11f, AttackSpeedPercent = 0.32f, CooldownReductionPercent = 0.10f` | 80g / 40g | Phases through steel to puncture hearts. |
| 08 | `wpn_dag_void_needle` | **Void Needle** | `Epic` (`#9B59B6`) | `FlatDamage = 16f, AttackSpeedPercent = 0.40f, MoveSpeedPercent = 0.12f` | 130g / 65g | Condensed null-matter needle stitching reality shut. |
| 09 | `wpn_dag_eclipsed_talon` | **Eclipsed Talon** | `Epic` (`#9B59B6`) | `FlatDamage = 18f, DamagePercent = 0.15f, AttackSpeedPercent = 0.38f` | 140g / 70g | Claw forged during a total lunar eclipse. |
| 10 | `wpn_dag_whisper_of_oblivion` | **Whisper of Oblivion** | `Legendary` (`#F1C40F`) | `FlatDamage = 26f, AttackSpeedPercent = 0.50f, DamagePercent = 0.25f, LifeStealPercent = 0.12f` | 260g / 130g | Silent executioner dagger striking before sound arrives. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Rusty Shiv (`wpn_dag_rusty_shiv` — `Common`)
- **Lore & Stats:** Pitted crude iron knife. (`FlatDamage = 3f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_rusty_shiv.png`):**
  > `16x16 pixel art weapon icon of "Rusty Shiv" (Daggers & Stilettos, Common rarity): Pitted crude iron knife. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #95A5A6 rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_rusty_shiv_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Rusty Shiv": Frame 1 windup glint in #95A5A6, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Iron Stiletto (`wpn_dag_iron_stiletto` — `Common`)
- **Lore & Stats:** Needle point slipping through armor seams. (`FlatDamage = 4f, AttackSpeedPercent = 0.20f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_iron_stiletto.png`):**
  > `16x16 pixel art weapon icon of "Iron Stiletto" (Daggers & Stilettos, Common rarity): Needle point slipping through armor seams. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #95A5A6 rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_iron_stiletto_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Iron Stiletto": Frame 1 windup glint in #95A5A6, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Assassin Knife (`wpn_dag_assassin_knife` — `Common`)
- **Lore & Stats:** Darkened rogue weapon weighted for critical stabs. (`FlatDamage = 5f, AttackSpeedPercent = 0.22f, MoveSpeedPercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_assassin_knife.png`):**
  > `16x16 pixel art weapon icon of "Assassin Knife" (Daggers & Stilettos, Common rarity): Darkened rogue weapon weighted for critical stabs. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #95A5A6 rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_assassin_knife_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Assassin Knife": Frame 1 windup glint in #95A5A6, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Venomous Fang (`wpn_dag_venomous_fang` — `Uncommon`)
- **Lore & Stats:** Dripping caustic serpent venom. (`FlatDamage = 7f, AttackSpeedPercent = 0.25f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_venomous_fang.png`):**
  > `16x16 pixel art weapon icon of "Venomous Fang" (Daggers & Stilettos, Uncommon rarity): Dripping caustic serpent venom. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #2ECC71 rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_venomous_fang_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Venomous Fang": Frame 1 windup glint in #2ECC71, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Shadow Quill (`wpn_dag_shadow_quill` — `Uncommon`)
- **Lore & Stats:** Featherweight blade sliding between shadows. (`FlatDamage = 6f, MoveSpeedPercent = 0.10f, AttackSpeedPercent = 0.25f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_shadow_quill.png`):**
  > `16x16 pixel art weapon icon of "Shadow Quill" (Daggers & Stilettos, Uncommon rarity): Featherweight blade sliding between shadows. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #2ECC71 rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_shadow_quill_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Shadow Quill": Frame 1 windup glint in #2ECC71, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Blood Letter (`wpn_dag_blood_letter` — `Rare`)
- **Lore & Stats:** Grooved parrying dagger feeding upon wounds. (`FlatDamage = 10f, AttackSpeedPercent = 0.30f, LifeStealPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_blood_letter.png`):**
  > `16x16 pixel art weapon icon of "Blood Letter" (Daggers & Stilettos, Rare rarity): Grooved parrying dagger feeding upon wounds. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #3498DB rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_blood_letter_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Blood Letter": Frame 1 windup glint in #3498DB, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Phantom Parry Dagger (`wpn_dag_phantom_parry_dagger` — `Rare`)
- **Lore & Stats:** Phases through steel to puncture hearts. (`FlatDamage = 11f, AttackSpeedPercent = 0.32f, CooldownReductionPercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_phantom_parry_dagger.png`):**
  > `16x16 pixel art weapon icon of "Phantom Parry Dagger" (Daggers & Stilettos, Rare rarity): Phases through steel to puncture hearts. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #3498DB rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_phantom_parry_dagger_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Phantom Parry Dagger": Frame 1 windup glint in #3498DB, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Void Needle (`wpn_dag_void_needle` — `Epic`)
- **Lore & Stats:** Condensed null-matter needle stitching reality shut. (`FlatDamage = 16f, AttackSpeedPercent = 0.40f, MoveSpeedPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_void_needle.png`):**
  > `16x16 pixel art weapon icon of "Void Needle" (Daggers & Stilettos, Epic rarity): Condensed null-matter needle stitching reality shut. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #9B59B6 rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_void_needle_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Void Needle": Frame 1 windup glint in #9B59B6, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Eclipsed Talon (`wpn_dag_eclipsed_talon` — `Epic`)
- **Lore & Stats:** Claw forged during a total lunar eclipse. (`FlatDamage = 18f, DamagePercent = 0.15f, AttackSpeedPercent = 0.38f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_eclipsed_talon.png`):**
  > `16x16 pixel art weapon icon of "Eclipsed Talon" (Daggers & Stilettos, Epic rarity): Claw forged during a total lunar eclipse. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #9B59B6 rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_eclipsed_talon_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Eclipsed Talon": Frame 1 windup glint in #9B59B6, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Whisper of Oblivion (`wpn_dag_whisper_of_oblivion` — `Legendary`)
- **Lore & Stats:** Silent executioner dagger striking before sound arrives. (`FlatDamage = 26f, AttackSpeedPercent = 0.50f, DamagePercent = 0.25f, LifeStealPercent = 0.12f`).
- **16x16 Item Icon Prompt (`icon_wpn_dag_whisper_of_oblivion.png`):**
  > `16x16 pixel art weapon icon of "Whisper of Oblivion" (Daggers & Stilettos, Legendary rarity): Silent executioner dagger striking before sound arrives. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E99A4 with #F1C40F rarity accent and #9B59B6 energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_dag_whisper_of_oblivion_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Whisper of Oblivion": Frame 1 windup glint in #F1C40F, Frame 2 active daggers & stilettos swing/projectile in #9B59B6 and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/dagger/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_dagger_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
