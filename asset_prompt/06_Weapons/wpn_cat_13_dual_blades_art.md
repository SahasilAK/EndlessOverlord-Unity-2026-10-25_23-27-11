# Paired Twin-Blades & Chakrams (`WeaponCategory.DualBlades`) — Complete Art & Icon Prompt Specification (10 Weapons)

> **Asset Category:** Equipment Weapon Category #13/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.DualBlades` (`WeaponCatalog.cs`)
> **Total Weapons in Category:** 0 (Common → Legendary progression)
> **Icon Spec:** `16x16 px`, `16 PPU`, Pivot `Center (0.5, 0.5)`, `FilterMode.Point`, `RGBA32`

---

## 1. Base Category Silhouette & Style Rules

- **Category Silhouette:** Crossed dual scimitars, shadow kamas, and eclipse twin-chakrams (`X`-crossed pair in `16x16` frame).
- **Base Palette:** Primary Steel/Structure `#5D6D7E`, Signature Energy `#1ABC9C`, Outer Rim `#110E1B`.
- **Rarity Progression Visual Rule:**
  - `Common` (`#95A5A6`): Weathered iron, ash wood, simple utilitarian silhouette.
  - `Uncommon` (`#2ECC71`): Polished steel, subtle elemental tint on blade edge.
  - `Rare` (`#3498DB`): Masterwork ornate crossguard/head, glowing runic inscriptions.
  - `Epic` (`#9B59B6`): Dark/celestial alloy, floating particle pixels (`1-2 px`) around weapon tip.
  - `Legendary` (`#F1C40F`): Mythic sovereign artifact, radiant `#FFFFFF` + `#1ABC9C` core, unmistakable silhouette.

---

## 2. All 10 Weapons in `Paired Twin-Blades & Chakrams` (`WeaponCatalog.cs`)

| # | Item ID | Name | Rarity | Stat Modifiers | Buy / Sell | Description |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |


---

## 3. Step-by-Step Prompts for All 10 Weapons & Attack VFX



---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **10 Weapon Icons** | `Assets/_Game/Art/UI/Equipment/Weapons/dual_blades/icon_<weapon_id>.png` | `16x16` each | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` slot square |
| **Category Attack VFX** | `Assets/_Game/Art/VFX/Weapons/vfx_wpn_dual_blades_strip.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `ProjectileView` / melee telegraph placeholder |
