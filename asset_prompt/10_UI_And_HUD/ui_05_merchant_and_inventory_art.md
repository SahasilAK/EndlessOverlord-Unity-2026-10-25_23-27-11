# Rare Merchant Shop, Item Comparison & Inventory Grid (`ui_05_merchant_and_inventory`) — UI & HUD Pixel Art Prompt Specification

> **Asset Category:** User Interface & HUD Module (`10_UI_And_HUD`)
> **Target Presenters:** `MerchantShopModalPresenter, TreasureChestModalPresenter`
> **Technical Spec:** `16 PPU`, `FilterMode.Point`, `RGBA32`, 9-Slice Borders configured in Unity Sprite Editor.

---

## 1. Module Contents & Visual Design

- **UI Style Rule:** Dark Gothic Obsidian-and-Bone frames (`#1A1624` fill, `#3D354B` bevel, `#110E1B` 1px outer border) so high-saturation skill/item icons and health bars pop with instant legibility.
- **Included Elements:** Contains: (1) Smuggler Merchant Shop Window Frame (`280x160 px` with lantern-lit velvet shelf background), (2) 20-Slot Inventory Grid Cell (`24x24 px` with rarity corner pips), (3) Gold Price Tag & Buy/Sell Button Badges (`48x16 px`), (4) Stat Comparison Tooltip Frame (`96x120 px` with green▲/red▼ delta arrows).

### 1.1 Copy-Paste UI Sprite Sheet Prompt (`ui_05_merchant_and_inventory_sheet.png`)
```text
Pixel art dark fantasy roguelite UI sprite sheet for "Rare Merchant Shop, Item Comparison & Inventory Grid" (MerchantShopModalPresenter, TreasureChestModalPresenter).
Style: crisp 16-bit gothic dark-iron, carved bone, and runic obsidian frames (#1A1624 interior, #3D354B highlighted bevel, #110E1B 1px solid outline), zero anti-aliasing, transparent background (RGBA32), FilterMode.Point.
Elements included on sheet: Contains: (1) Smuggler Merchant Shop Window Frame (`280x160 px` with lantern-lit velvet shelf background), (2) 20-Slot Inventory Grid Cell (`24x24 px` with rarity corner pips), (3) Gold Price Tag & Buy/Sell Button Badges (`48x16 px`), (4) Stat Comparison Tooltip Frame (`96x120 px` with green▲/red▼ delta arrows).
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | PPU | Filter | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- |
| **UI Atlas Sheet** | `Assets/_Game/Art/UI/Sheets/ui_05_merchant_and_inventory_sheet.png` | `16` | `Point`, `RGBA32` | Runtime `Image` solid-color quads in `MerchantShopModalPresenter, TreasureChestModalPresenter` |
