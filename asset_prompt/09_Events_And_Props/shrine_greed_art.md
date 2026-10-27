# Shrine of Greed (`shrine_greed`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** Interactive Arena Shrine (`ShrineType.GreedShrine`) (`InRunEventModel.cs`)
> **Frame Size:** `24x36 px` | **Sprite Strip Size:** `48x36 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#F1C40F` | **Emissive Accent:** `#FFF59D` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Gilded demonic coffer pedestal overflowing with cursed gold coins (`#F1C40F`) and a grinning golden gargoyle maw (`#FFF59D`). Grants +60 Gold immediately, spawns extra elite wave.

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_shrine_greed_sheet.png` — `48x36 px`)
```text
2D top-down 3/4 view pixel art sprite strip (48x36 pixels total, frames of 24x36 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Shrine of Greed (Interactive Arena Shrine (`ShrineType.GreedShrine`)) — Gilded demonic coffer pedestal overflowing with cursed gold coins (`#F1C40F`) and a grinning golden gargoyle maw (`#FFF59D`). Grants +60 Gold immediately, spawns extra elite wave.
Color Palette: primary #F1C40F, glowing emissive #FFF59D, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #FFF59D particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_shrine_greed_sheet.png` | `48x36` (`24x36` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
