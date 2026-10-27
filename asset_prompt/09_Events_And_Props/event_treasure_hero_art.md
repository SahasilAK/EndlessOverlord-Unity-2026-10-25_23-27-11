# Treasure Hero In-Run Event (`event_treasure_hero`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** Special Wave Event (`InRunEventType.TreasureHero`) (`InRunEventModel.cs`)
> **Frame Size:** `32x32 px` | **Sprite Strip Size:** `192x32 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#F1C40F` | **Emissive Accent:** `#2ECC71` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Fleeing greedy adventurer burdened with an overflowing golden backpack dropping trails of glittering coins and gems (`32x32` 6-frame sprint & drop strip).

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_event_treasure_hero_sheet.png` — `192x32 px`)
```text
2D top-down 3/4 view pixel art sprite strip (192x32 pixels total, frames of 32x32 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Treasure Hero In-Run Event (Special Wave Event (`InRunEventType.TreasureHero`)) — Fleeing greedy adventurer burdened with an overflowing golden backpack dropping trails of glittering coins and gems (`32x32` 6-frame sprint & drop strip).
Color Palette: primary #F1C40F, glowing emissive #2ECC71, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #2ECC71 particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_event_treasure_hero_sheet.png` | `192x32` (`32x32` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
