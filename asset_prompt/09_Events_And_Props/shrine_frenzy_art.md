# Shrine of Frenzy (`shrine_frenzy`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** Interactive Arena Shrine (`ShrineType.FrenzyShrine`) (`InRunEventModel.cs`)
> **Frame Size:** `24x36 px` | **Sprite Strip Size:** `48x36 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#FF5722` | **Emissive Accent:** `#FFD54F` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Jagged iron war-totem crowned with blazing berserker horns and roaring orange-gold flames (`#FF5722` / `#FFD54F`). Grants +25% Attack Speed & +15% Move Speed, but -10 Armor.

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_shrine_frenzy_sheet.png` — `48x36 px`)
```text
2D top-down 3/4 view pixel art sprite strip (48x36 pixels total, frames of 24x36 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Shrine of Frenzy (Interactive Arena Shrine (`ShrineType.FrenzyShrine`)) — Jagged iron war-totem crowned with blazing berserker horns and roaring orange-gold flames (`#FF5722` / `#FFD54F`). Grants +25% Attack Speed & +15% Move Speed, but -10 Armor.
Color Palette: primary #FF5722, glowing emissive #FFD54F, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #FFD54F particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_shrine_frenzy_sheet.png` | `48x36` (`24x36` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
