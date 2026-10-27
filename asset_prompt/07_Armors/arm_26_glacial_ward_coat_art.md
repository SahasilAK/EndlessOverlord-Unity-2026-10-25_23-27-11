# Glacial Ward Coat (`arm_glacial_ward_coat`) — Armor Art, Icon & Aura Prompt Specification

> **Asset Category:** Equipment Armor #26/30 (`07_Armors`)
> **Catalog ID:** `"arm_glacial_ward_coat"` (`ArmorCatalog.cs`)
> **Rarity Tier:** `ItemRarity.Rare` (`#3498DB`)
> **Stat Modifiers:** `FlatArmor = 14f, FlatMaxHealth = 55f, CooldownReductionPercent = 0.06f` | **Price:** Buy `80g` / Sell `40g`
> **Lore Description:** *Chilled pelts and ice runes deflecting searing blows.*

---

## 1. Base Design & Visual Prompt (`16x16 px` Icon + `32x32 px` Character Aura Overlay)

### 1.1 Copy-Paste 16x16 Inventory Icon Prompt (`icon_arm_glacial_ward_coat.png`)
```text
16x16 pixel art armor chestplate/vestment icon of "Glacial Ward Coat" (Rare rarity): Chilled pelts and ice runes deflecting searing blows.
Canvas: 16x16 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: front-facing torso armor bust with distinct shoulder pauldrons/mantle filling 14x14 px, top-left zenithal lighting, rarity accent trim in #3498DB.
```

### 1.2 Step-by-Step Character Equip & Defensive Proc Variations (`vfx_arm_glacial_ward_coat_ward.png` — `64x32 px`, 2 frames of `32x32`)
- **Frame 01 (`0..31, 0..31`) — `Equipped_Passive_Trim`:**
  > `32x32 pixel art subtle armor aura overlay for Glacial Ward Coat: 4 corner runic/material motes in #3498DB hovering around the boss shoulders (`X:8..24, Y:14..26`), transparent center.`
- **Frame 02 (`32..63, 0..31`) — `OnHit_Mitigation_Flash`:**
  > `32x32 pixel art defensive ward flash when Glacial Ward Coat absorbs damage: hexagonal/cloak deflection shield ring in #3498DB and #FFFFFF lasting 0.12s.`

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Armor Icon** | `Assets/_Game/Art/UI/Equipment/Armors/icon_arm_glacial_ward_coat.png` | `16x16` | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` armor slot |
| **Mitigation Overlay** | `Assets/_Game/Art/VFX/Armors/vfx_arm_glacial_ward_coat_ward.png` | `64x32` (`2x 32x32`) | `32` | `(0.5, 0.1)` | `LichLordVisualPresenter` armor flash |
