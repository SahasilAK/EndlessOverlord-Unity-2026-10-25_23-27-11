# Combat HUD, Health/Resource Bars, Ability Slots & Boss Banner (`ui_02_combat_hud_and_bars`) — UI & HUD Pixel Art Prompt Specification

> **Asset Category:** User Interface & HUD Module (`10_UI_And_HUD`)
> **Target Presenters:** `HudPresenter, BossEncounterBannerPresenter, InRunEventBannerPresenter`
> **Technical Spec:** `16 PPU`, `FilterMode.Point`, `RGBA32`, 9-Slice Borders configured in Unity Sprite Editor.

---

## 1. Module Contents & Visual Design

- **UI Style Rule:** Dark Gothic Obsidian-and-Bone frames (`#1A1624` fill, `#3D354B` bevel, `#110E1B` 1px outer border) so high-saturation skill/item icons and health bars pop with instant legibility.
- **Included Elements:** Contains: (1) Overlord HP & Class Resource Bar Frame (`160x24 px` gothic bone-and-iron frame + segmented fill strips in Blood Crimson `#D91A4D`, Soul Emerald `#30E680`, Hellfire `#FF9E00`, Shadow `#B366FF`, Runic `#6EE7B7`), (2) 6-Slot Active Ability Hotbar Frame (`24x24 px` per slot + radial cooldown sweep mask + keybind badge), (3) Top-Center Wave Progress & Threat Meter Frame (`180x28 px`), (4) Milestone Boss Health Bar & Warning Banner (`240x36 px`).

### 1.1 Copy-Paste UI Sprite Sheet Prompt (`ui_02_combat_hud_and_bars_sheet.png`)
```text
Pixel art dark fantasy roguelite UI sprite sheet for "Combat HUD, Health/Resource Bars, Ability Slots & Boss Banner" (HudPresenter, BossEncounterBannerPresenter, InRunEventBannerPresenter).
Style: crisp 16-bit gothic dark-iron, carved bone, and runic obsidian frames (#1A1624 interior, #3D354B highlighted bevel, #110E1B 1px solid outline), zero anti-aliasing, transparent background (RGBA32), FilterMode.Point.
Elements included on sheet: Contains: (1) Overlord HP & Class Resource Bar Frame (`160x24 px` gothic bone-and-iron frame + segmented fill strips in Blood Crimson `#D91A4D`, Soul Emerald `#30E680`, Hellfire `#FF9E00`, Shadow `#B366FF`, Runic `#6EE7B7`), (2) 6-Slot Active Ability Hotbar Frame (`24x24 px` per slot + radial cooldown sweep mask + keybind badge), (3) Top-Center Wave Progress & Threat Meter Frame (`180x28 px`), (4) Milestone Boss Health Bar & Warning Banner (`240x36 px`).
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | PPU | Filter | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- |
| **UI Atlas Sheet** | `Assets/_Game/Art/UI/Sheets/ui_02_combat_hud_and_bars_sheet.png` | `16` | `Point`, `RGBA32` | Runtime `Image` solid-color quads in `HudPresenter, BossEncounterBannerPresenter, InRunEventBannerPresenter` |
