# Void Distortion Mutation (`mutation_void_distortion`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** Dungeon Mutation Hazard (`DungeonMutationType.VoidDistortion`) (`InRunEventModel.cs`)
> **Frame Size:** `32x32 px` | **Sprite Strip Size:** `128x32 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#8E44AD` | **Emissive Accent:** `#D980FA` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Gravitational purple rift anomalies, warped floor tiles, and floating reality shards (`+20%` Cooldown speed, random gravity wells).

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_mutation_void_distortion_sheet.png` — `128x32 px`)
```text
2D top-down 3/4 view pixel art sprite strip (128x32 pixels total, frames of 32x32 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Void Distortion Mutation (Dungeon Mutation Hazard (`DungeonMutationType.VoidDistortion`)) — Gravitational purple rift anomalies, warped floor tiles, and floating reality shards (`+20%` Cooldown speed, random gravity wells).
Color Palette: primary #8E44AD, glowing emissive #D980FA, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #D980FA particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_mutation_void_distortion_sheet.png` | `128x32` (`32x32` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
