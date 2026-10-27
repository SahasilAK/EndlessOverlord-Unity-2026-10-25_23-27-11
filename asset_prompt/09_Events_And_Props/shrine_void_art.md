# Shrine of the Void (`shrine_void`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** Interactive Arena Shrine (`ShrineType.VoidShrine`) (`InRunEventModel.cs`)
> **Frame Size:** `24x36 px` | **Sprite Strip Size:** `48x36 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#9B59B6` | **Emissive Accent:** `#E056FD` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Fractured floating dark-matter obelisk orbiting a miniature purple-magenta singularity (`#9B59B6` / `#E056FD`). Grants +20% Cooldown Reduction & +25 Max Resource.

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_shrine_void_sheet.png` — `48x36 px`)
```text
2D top-down 3/4 view pixel art sprite strip (48x36 pixels total, frames of 24x36 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Shrine of the Void (Interactive Arena Shrine (`ShrineType.VoidShrine`)) — Fractured floating dark-matter obelisk orbiting a miniature purple-magenta singularity (`#9B59B6` / `#E056FD`). Grants +20% Cooldown Reduction & +25 Max Resource.
Color Palette: primary #9B59B6, glowing emissive #E056FD, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #E056FD particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_shrine_void_sheet.png` | `48x36` (`24x36` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
