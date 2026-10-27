# Blightward Jerkin (`arm_blightward_jerkin`) — Armor Art, Icon & Aura Prompt Specification

> **Asset Category:** Equipment Armor #27/30 (`07_Armors`)
> **Catalog ID:** `"arm_blightward_jerkin"` (`ArmorCatalog.cs`)
> **Rarity Tier:** `ItemRarity.Common` (`#95A5A6`)
> **Stat Modifiers:** `FlatArmor = 6f, FlatMaxHealth = 22f` | **Price:** Buy `20g` / Sell `10g`
> **Lore Description:** *Treated jerkin resisting acid and dungeon toxins.*

---

## 1. Base Design & Visual Prompt (`16x16 px` Icon + `32x32 px` Character Aura Overlay)

### 1.1 Copy-Paste 16x16 Inventory Icon Prompt (`icon_arm_blightward_jerkin.png`)
```text
16x16 pixel art armor chestplate/vestment icon of "Blightward Jerkin" (Common rarity): Treated jerkin resisting acid and dungeon toxins.
Canvas: 16x16 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: front-facing torso armor bust with distinct shoulder pauldrons/mantle filling 14x14 px, top-left zenithal lighting, rarity accent trim in #95A5A6.
```

### 1.2 Step-by-Step Character Equip & Defensive Proc Variations (`vfx_arm_blightward_jerkin_ward.png` — `64x32 px`, 2 frames of `32x32`)
- **Frame 01 (`0..31, 0..31`) — `Equipped_Passive_Trim`:**
  > `32x32 pixel art subtle armor aura overlay for Blightward Jerkin: 4 corner runic/material motes in #95A5A6 hovering around the boss shoulders (`X:8..24, Y:14..26`), transparent center.`
- **Frame 02 (`32..63, 0..31`) — `OnHit_Mitigation_Flash`:**
  > `32x32 pixel art defensive ward flash when Blightward Jerkin absorbs damage: hexagonal/cloak deflection shield ring in #95A5A6 and #FFFFFF lasting 0.12s.`

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Armor Icon** | `Assets/_Game/Art/UI/Equipment/Armors/icon_arm_blightward_jerkin.png` | `16x16` | `16` | `(0.5, 0.5)` | `LoadoutScreenPresenter` & `MerchantShopModalPresenter` armor slot |
| **Mitigation Overlay** | `Assets/_Game/Art/VFX/Armors/vfx_arm_blightward_jerkin_ward.png` | `64x32` (`2x 32x32`) | `32` | `(0.5, 0.1)` | `LichLordVisualPresenter` armor flash |
