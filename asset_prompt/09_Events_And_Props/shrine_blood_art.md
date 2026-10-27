# Shrine of Blood (`shrine_blood`) — Prop, Event & VFX Art Prompt Specification

> **Asset Category:** In-Run Event / Interactive Prop (`09_Events_And_Props`)
> **System Binding:** Interactive Arena Shrine (`ShrineType.BloodShrine`) (`InRunEventModel.cs`)
> **Frame Size:** `24x36 px` | **Sprite Strip Size:** `48x36 px` | **PPU:** `32` | **Filter:** `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

- **Primary Palette:** `#D91A4D` | **Emissive Accent:** `#FF6680` | **Outer Rim:** `#110E1B` (1px solid dark outline).
- **Visual Concept:** Gothic sacrificial blood chalice altar carved from dark obsidian with overflowing crimson liquid blood (`#D91A4D`) and levitating ruby droplets (`#FF6680`). Sacrifice 20% current HP for +25% Permanent Damage.

### 1.1 Copy-Paste Master Sprite Strip Prompt (`prop_shrine_blood_sheet.png` — `48x36 px`)
```text
2D top-down 3/4 view pixel art sprite strip (48x36 pixels total, frames of 24x36 pixels), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Subject: Shrine of Blood (Interactive Arena Shrine (`ShrineType.BloodShrine`)) — Gothic sacrificial blood chalice altar carved from dark obsidian with overflowing crimson liquid blood (`#D91A4D`) and levitating ruby droplets (`#FF6680`). Sacrifice 20% current HP for +25% Permanent Damage.
Color Palette: primary #D91A4D, glowing emissive #FF6680, dark stone base #26222E, top-left zenithal lighting.
- State 1 (Active / Intact): Full glowing emissive state with #FF6680 particles and bright #FFFFFF core highlights.
- State 2 (Triggered /Burst): High-contrast activation flash expanding outward.
- State 3 (Consumed / Dormant): Depleted dark stone/iron state with faded embers (#3A3542) so the player clearly sees the shrine/event was used.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prop / Event Sheet** | `Assets/_Game/Art/Props/Events/prop_shrine_blood_sheet.png` | `48x36` (`24x36` frames) | `32` | `(0.5, 0.1)` | `ShrineView` / `TreasureChestView` / `InRunEventBannerPresenter` |
