# Grimoires, Tomes & Codices (`WeaponCategory.Spellbook`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #12/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Spellbook` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 10 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Levitating leather-and-bone bound spellbooks with glowing pages and chain clasps (isometric open/closed tome).
- **Base Palette:** Primary Steel/Structure `#8E44AD`, Signature Energy `#FF66CC`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#FF66CC` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Grimoires, Tomes & Codices` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 01 | `wpn_spb_novice_tome` | **Novice Tome** | `Common` (`#95A5A6`) | `FlatDamage = 4f, DamagePercent = 0.05f` | 20g / 10g | Leatherbound primer containing rudimentary cantrips. |
| 02 | `wpn_spb_elemental_scroll` | **Elemental Scroll** | `Common` (`#95A5A6`) | `FlatDamage = 5f, DamagePercent = 0.06f` | 25g / 12g | Parchment inscribed with basic elemental equations. |
| 03 | `wpn_spb_cantrip_grimoire` | **Cantrip Grimoire** | `Common` (`#95A5A6`) | `FlatDamage = 6f, CooldownReductionPercent = 0.05f` | 25g / 12g | Well-thumbed collection of minor hexes. |
| 04 | `wpn_spb_tome_of_embers` | **Tome of Embers** | `Uncommon` (`#2ECC71`) | `FlatDamage = 9f, DamagePercent = 0.10f` | 50g / 25g | Pages scorched with self-igniting fire runes. |
| 05 | `wpn_spb_pages_of_permafrost` | **Pages of Permafrost** | `Uncommon` (`#2ECC71`) | `FlatDamage = 8f, CooldownReductionPercent = 0.08f` | 50g / 25g | Vellum cold to the touch containing winter litanies. |
| 06 | `wpn_spb_grimoire_of_souls` | **Grimoire of Souls** | `Rare` (`#3498DB`) | `FlatDamage = 13f, LifeStealPercent = 0.08f, DamagePercent = 0.10f` | 80g / 40g | Bound in flayed parchment humming with trapped whispers. |
| 07 | `wpn_spb_codex_of_arcane_might` | **Codex of Arcane Might** | `Rare` (`#3498DB`) | `FlatDamage = 15f, DamagePercent = 0.15f` | 85g / 42g | Encyclopedic mastery of destructive transmutation. |
| 08 | `wpn_spb_book_of_forbidden_flesh` | **Book of Forbidden Flesh** | `Epic` (`#9B59B6`) | `FlatDamage = 21f, LifeStealPercent = 0.12f, FlatMaxHealth = 40f` | 140g / 70g | Mutates outgoing spells into parasitic blood leeches. |
| 09 | `wpn_spb_chronicles_of_storm` | **Chronicles of Storm** | `Epic` (`#9B59B6`) | `FlatDamage = 22f, DamagePercent = 0.22f, AttackSpeedPercent = 0.15f` | 145g / 72g | Living parchment summoning tempest cataclysms. |
| 10 | `wpn_spb_necronomicon_original` | **Necronomicon Original** | `Legendary` (`#F1C40F`) | `FlatDamage = 34f, DamagePercent = 0.35f, CooldownReductionPercent = 0.20f, LifeStealPercent = 0.15f` | 300g / 150g | The blasphemous tome of eternal dread and oblivion. |

---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX

#### Weapon 01/10: Novice Tome (`wpn_spb_novice_tome` — `Common`)
- **Lore & Stats:** Leatherbound primer containing rudimentary cantrips. (`FlatDamage = 4f, DamagePercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_novice_tome.png`):**
  > `16x16 pixel art weapon icon of "Novice Tome" (Grimoires, Tomes & Codices, Common rarity): Leatherbound primer containing rudimentary cantrips. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #95A5A6 rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_novice_tome_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Novice Tome": Frame 1 windup glint in #95A5A6, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 02/10: Elemental Scroll (`wpn_spb_elemental_scroll` — `Common`)
- **Lore & Stats:** Parchment inscribed with basic elemental equations. (`FlatDamage = 5f, DamagePercent = 0.06f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_elemental_scroll.png`):**
  > `16x16 pixel art weapon icon of "Elemental Scroll" (Grimoires, Tomes & Codices, Common rarity): Parchment inscribed with basic elemental equations. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #95A5A6 rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_elemental_scroll_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Elemental Scroll": Frame 1 windup glint in #95A5A6, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 03/10: Cantrip Grimoire (`wpn_spb_cantrip_grimoire` — `Common`)
- **Lore & Stats:** Well-thumbed collection of minor hexes. (`FlatDamage = 6f, CooldownReductionPercent = 0.05f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_cantrip_grimoire.png`):**
  > `16x16 pixel art weapon icon of "Cantrip Grimoire" (Grimoires, Tomes & Codices, Common rarity): Well-thumbed collection of minor hexes. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #95A5A6 rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_cantrip_grimoire_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Cantrip Grimoire": Frame 1 windup glint in #95A5A6, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 04/10: Tome of Embers (`wpn_spb_tome_of_embers` — `Uncommon`)
- **Lore & Stats:** Pages scorched with self-igniting fire runes. (`FlatDamage = 9f, DamagePercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_tome_of_embers.png`):**
  > `16x16 pixel art weapon icon of "Tome of Embers" (Grimoires, Tomes & Codices, Uncommon rarity): Pages scorched with self-igniting fire runes. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #2ECC71 rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_tome_of_embers_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Tome of Embers": Frame 1 windup glint in #2ECC71, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 05/10: Pages of Permafrost (`wpn_spb_pages_of_permafrost` — `Uncommon`)
- **Lore & Stats:** Vellum cold to the touch containing winter litanies. (`FlatDamage = 8f, CooldownReductionPercent = 0.08f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_pages_of_permafrost.png`):**
  > `16x16 pixel art weapon icon of "Pages of Permafrost" (Grimoires, Tomes & Codices, Uncommon rarity): Vellum cold to the touch containing winter litanies. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #2ECC71 rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_pages_of_permafrost_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Pages of Permafrost": Frame 1 windup glint in #2ECC71, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 06/10: Grimoire of Souls (`wpn_spb_grimoire_of_souls` — `Rare`)
- **Lore & Stats:** Bound in flayed parchment humming with trapped whispers. (`FlatDamage = 13f, LifeStealPercent = 0.08f, DamagePercent = 0.10f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_grimoire_of_souls.png`):**
  > `16x16 pixel art weapon icon of "Grimoire of Souls" (Grimoires, Tomes & Codices, Rare rarity): Bound in flayed parchment humming with trapped whispers. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #3498DB rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_grimoire_of_souls_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Grimoire of Souls": Frame 1 windup glint in #3498DB, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 07/10: Codex of Arcane Might (`wpn_spb_codex_of_arcane_might` — `Rare`)
- **Lore & Stats:** Encyclopedic mastery of destructive transmutation. (`FlatDamage = 15f, DamagePercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_codex_of_arcane_might.png`):**
  > `16x16 pixel art weapon icon of "Codex of Arcane Might" (Grimoires, Tomes & Codices, Rare rarity): Encyclopedic mastery of destructive transmutation. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #3498DB rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_codex_of_arcane_might_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Codex of Arcane Might": Frame 1 windup glint in #3498DB, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 08/10: Book of Forbidden Flesh (`wpn_spb_book_of_forbidden_flesh` — `Epic`)
- **Lore & Stats:** Mutates outgoing spells into parasitic blood leeches. (`FlatDamage = 21f, LifeStealPercent = 0.12f, FlatMaxHealth = 40f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_book_of_forbidden_flesh.png`):**
  > `16x16 pixel art weapon icon of "Book of Forbidden Flesh" (Grimoires, Tomes & Codices, Epic rarity): Mutates outgoing spells into parasitic blood leeches. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #9B59B6 rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_book_of_forbidden_flesh_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Book of Forbidden Flesh": Frame 1 windup glint in #9B59B6, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 09/10: Chronicles of Storm (`wpn_spb_chronicles_of_storm` — `Epic`)
- **Lore & Stats:** Living parchment summoning tempest cataclysms. (`FlatDamage = 22f, DamagePercent = 0.22f, AttackSpeedPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_chronicles_of_storm.png`):**
  > `16x16 pixel art weapon icon of "Chronicles of Storm" (Grimoires, Tomes & Codices, Epic rarity): Living parchment summoning tempest cataclysms. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #9B59B6 rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_chronicles_of_storm_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Chronicles of Storm": Frame 1 windup glint in #9B59B6, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`
#### Weapon 10/10: Necronomicon Original (`wpn_spb_necronomicon_original` — `Legendary`)
- **Lore & Stats:** The blasphemous tome of eternal dread and oblivion. (`FlatDamage = 34f, DamagePercent = 0.35f, CooldownReductionPercent = 0.20f, LifeStealPercent = 0.15f`).
- **16x16 Item Icon Prompt (`icon_wpn_spb_necronomicon_original.png`):**
  > `16x16 pixel art weapon icon of "Necronomicon Original" (Grimoires, Tomes & Codices, Legendary rarity): The blasphemous tome of eternal dread and oblivion. Oriented diagonally at 45 degrees filling 14x14 px, 1px #110E1B dark outline, primary material #8E44AD with #F1C40F rarity accent and #FF66CC energy glint, crisp pixel art, zero anti-aliasing, transparent background (RGBA32).`
- **In-Hand / Attack Slash Overlay (`vfx_wpn_spb_necronomicon_original_swing.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art weapon attack strip (48x16 px) for "Necronomicon Original": Frame 1 windup glint in #F1C40F, Frame 2 active grimoires, tomes & codices swing/projectile in #FF66CC and #FFFFFF, Frame 3 impact sparks.`

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/spellbook/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_spellbook_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
