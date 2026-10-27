# Holy Crusade In-Run Event (`event_holy_crusade`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** Special Wave Event (`InRunEventType.HolyCrusade`) (`InRunEventModel.cs`)
> **Frame Size:** `48x48 px` | **Sprite Strip Size:** `192x48 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#FFE082` | **Emissive Accent:** `#FFF8E1` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Descending golden sunbeam pillars, sanctified war-banners planted in the arena, and radiant crusader spawn herald VFX.

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_event_holy_crusade_sheet.png` — `192x48 px`)
```text
2D top-down 3/4 view pixel art sprite strip (192x48 pixels total, frames of 48x48 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Holy Crusade In-Run Event (Special Wave Event (`InRunEventType.HolyCrusade`)) — Descending golden sunbeam pillars, sanctified war-banners planted in the arena, and radiant crusader spawn herald VFX.
Color Palette: primary #FFE082, glowing emissive #FFF8E1, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #FFF8E1 particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_event_holy_crusade_sheet.png` | `192x48` (`48x48` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
