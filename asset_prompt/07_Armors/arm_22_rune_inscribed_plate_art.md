# Rune Inscribed Plate (`arm_rune_inscribed_plate`) — Armor Art, Icon & Aura Prompt Specification

> **Asset Category:** Equipment Armor #22/30 (`07_Armors`)
> **Catalog ID:** `"arm_rune_inscribed_plate"` (`ArmorCatalog.cs`)
> **Rarity Tier:** `ItemRarity.Rare` (`#3498DB`)
> **Stat Modifiers:** `FlatArmor = 15f, FlatMaxHealth = 55f, CooldownReductionPercent = 0.08f` | **Price:** Buy `80g` / Sell `40g`
> **Lore Description:** *Glows with defensive runic inscriptions absorbing shocks.*

---

## 1. Base Design & Visual Prompt (`16x16 px` Icon + `32x32 px` Character Aura Overlay)

### 1.1 Copy-Paste 16x16 Inventory Icon Prompt (`icon_arm_rune_inscribed_plate.png`)
```text
16x16 pixel art armor chestplate/vestment icon of "Rune Inscribed Plate" (Rare rarity): Glows with defensive runic inscriptions absorbing shocks.
Canvas: 16x16 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: front-facing torso armor bust with distinct shoulder pauldrons/mantle filling 14x14 px, top-left zenithal lighting, rarity accent trim in #3498DB.
```

### 1.2 Step-by-Step Character Equip & Defensive Proc Variations (`vfx_arm_rune_inscribed_plate_ward.png` — `64x32 px`, 2 frames of `32x32`)
- **Frame 01 (`0..31, 0..31`) — `Equipped_Passive_Trim`:**
  > `32x32 pixel art subtle armor aura overlay for Rune Inscribed Plate: 4 corner runic/material motes in #3498DB hovering around the boss shoulders (`X:8..24, Y:14..26`), transparent center.`
- **Frame 02 (`32..63, 0..31`) — `OnHit_Mitigation_Flash`:**
  > `32x32 pixel art defensive ward flash when Rune Inscribed Plate absorbs damage: hexagonal/cloak deflection shield ring in #3498DB and #FFFFFF lasting 0.12s.`

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Armor Icon** | `Assets/_Game/Art/UI/Equipment/Armors/icon_arm_rune_inscribed_plate.png` | `16x16` | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` armor slot |
| **Mitigation Overlay** | `Assets/_Game/Art/VFX/Armors/vfx_arm_rune_inscribed_plate_ward.png` | `64x32` (`2x 32x32`) | `32` | `(0.5, 0.1)` | `LichLordVisualPresenter` armor flash |
