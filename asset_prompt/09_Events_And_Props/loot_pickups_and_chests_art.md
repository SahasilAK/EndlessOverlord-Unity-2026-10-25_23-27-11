# Loot Pickups, Chests & Merchant Cart (`loot_pickups_and_chests`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** World Pickups & Shop Props (`TreasureChestView`, `MerchantShopModalPresenter`) (`InRunEventModel.cs`)
> **Frame Size:** `24x24 px` | **Sprite Strip Size:** `96x24 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#F39C12` | **Emissive Accent:** `#30E680` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Complete world pickup sheet: (1) Tiered Treasure Chests (Common Iron, Rare Azure, Epic Void, Legendary Sovereign Gold — Closed & Open frames `24x24`), (2) Gold Coin stack (`16x16`), (3) Soul Essence orb (`16x16`), (4) Rare Merchant Smuggler hooded figure & lantern cart (`48x48`).

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_loot_pickups_and_chests_sheet.png` — `96x24 px`)
```text
2D top-down 3/4 view pixel art sprite strip (96x24 pixels total, frames of 24x24 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Loot Pickups, Chests & Merchant Cart (World Pickups & Shop Props (`TreasureChestView`, `MerchantShopModalPresenter`)) — Complete world pickup sheet: (1) Tiered Treasure Chests (Common Iron, Rare Azure, Epic Void, Legendary Sovereign Gold — Closed & Open frames `24x24`), (2) Gold Coin stack (`16x16`), (3) Soul Essence orb (`16x16`), (4) Rare Merchant Smuggler hooded figure & lantern cart (`48x48`).
Color Palette: primary #F39C12, glowing emissive #30E680, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #30E680 particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_loot_pickups_and_chests_sheet.png` | `96x24` (`24x24` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
