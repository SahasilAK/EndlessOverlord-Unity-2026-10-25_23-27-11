# Ancient Royal Aegis (`arm_ancient_royal_aegis`) — Armor Art, Icon & Aura Prompt Specification

> **Asset Category:** Equipment Armor #06/30 (`07_Armors`)
> **Catalog ID:** `"arm_ancient_royal_aegis"` (`ArmorCatalog.cs`)
> **Rarity Tier:** `ItemRarity.Epic` (`#9B59B6`)
> **Stat Modifiers:** `FlatArmor = 22f, FlatMaxHealth = 90f, CooldownReductionPercent = 0.08f` | **Price:** Buy `140g` / Sell `70g`
> **Lore Description:** *Ceremonial plate of forgotten kings blessed with indestructible wards.*

---

## 1. Base Design & Visual Prompt (`16x16 px` Icon + `32x32 px` Character Aura Overlay)

### 1.1 Copy-Paste 16x16 Inventory Icon Prompt (`icon_arm_ancient_royal_aegis.png`)
```text
16x16 pixel art armor chestplate/vestment icon of "Ancient Royal Aegis" (Epic rarity): Ceremonial plate of forgotten kings blessed with indestructible wards.
Canvas: 16x16 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: front-facing torso armor bust with distinct shoulder pauldrons/mantle filling 14x14 px, top-left zenithal lighting, rarity accent trim in #9B59B6.
```

### 1.2 Step-by-Step Character Equip & Defensive Proc Variations (`vfx_arm_ancient_royal_aegis_ward.png` — `64x32 px`, 2 frames of `32x32`)
- **Frame 01 (`0..31, 0..31`) — `Equipped_Passive_Trim`:**
  > `32x32 pixel art subtle armor aura overlay for Ancient Royal Aegis: 4 corner runic/material motes in #9B59B6 hovering around the boss shoulders (`X:8..24, Y:14..26`), transparent center.`
- **Frame 02 (`32..63, 0..31`) — `OnHit_Mitigation_Flash`:**
  > `32x32 pixel art defensive ward flash when Ancient Royal Aegis absorbs damage: hexagonal/cloak deflection shield ring in #9B59B6 and #FFFFFF lasting 0.12s.`

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Armor Icon** | `Assets/_Game/Art/UI/Equipment/Armors/icon_arm_ancient_royal_aegis.png` | `16x16` | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` armor slot |
| **Mitigation Overlay** | `Assets/_Game/Art/VFX/Armors/vfx_arm_ancient_royal_aegis_ward.png` | `64x32` (`2x 32x32`) | `32` | `(0.5, 0.1)` | `LichLordVisualPresenter` armor flash |
