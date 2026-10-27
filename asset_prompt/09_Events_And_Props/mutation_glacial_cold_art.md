# Glacial Cold Mutation (`mutation_glacial_cold`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** Dungeon Mutation Hazard (`DungeonMutationType.GlacialCold`) (`InRunEventModel.cs`)
> **Frame Size:** `32x32 px` | **Sprite Strip Size:** `128x32 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#00E5FF` | **Emissive Accent:** `#E0F7FA` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Creeping permafrost ice-crystal floor patches, drifting blizzard snowflakes, and frosted screen-corner vignette (`-15%` global move speed, ice shatter procs).

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_mutation_glacial_cold_sheet.png` — `128x32 px`)
```text
2D top-down 3/4 view pixel art sprite strip (128x32 pixels total, frames of 32x32 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Glacial Cold Mutation (Dungeon Mutation Hazard (`DungeonMutationType.GlacialCold`)) — Creeping permafrost ice-crystal floor patches, drifting blizzard snowflakes, and frosted screen-corner vignette (`-15%` global move speed, ice shatter procs).
Color Palette: primary #00E5FF, glowing emissive #E0F7FA, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #E0F7FA particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_mutation_glacial_cold_sheet.png` | `128x32` (`32x32` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
