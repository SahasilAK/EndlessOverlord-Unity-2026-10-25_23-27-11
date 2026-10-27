# Run Results, Defeat/Victory Banner & Meta-Progression Altar (`ui_06_death_results_and_meta_shop`) — UI & HUD Pixel Art Prompt Specification

> **Asset Category:** User Interface & HUD Module (`10_UI_And_HUD`)
> **Target Presenters:** `DeathResultsScreenPresenter, MetaProgressionScreenPresenter`
> **Technical Spec:** `16 PPU`, `FilterMode.Point`, `RGBA32`, 9-Slice Borders configured in Unity Sprite Editor.

---

## 1. Module Contents & Visual Design

- **UI Style Rule:** Dark Gothic Obsidian-and-Bone frames (`#1A1624` fill, `#3D354B` bevel, `#110E1B` 1px outer border) so high-saturation skill/item icons and health bars pop with instant legibility.
- **Included Elements:** Contains: (1) `THE OVERLORD HAS FALLEN` & `ETERNAL SOVEREIGN VICTORIOUS` Gothic Banners (`256x48 px`), (2) Run Statistics Scroll & Soul Essence Tally Counter (`220x140 px`), (3) Meta-Progression Dark Sanctum Upgrade Altar Icons (12 permanent blessing icons `24x24 px`: Max HP, Base Damage, Armor, Move Speed, Greed, Soul Harvest, Starting Rerolls, Relic Capacity).

### 1.1 Copy-Paste UI Sprite Sheet Prompt (`ui_06_death_results_and_meta_shop_sheet.png`)
```text
Pixel art dark fantasy roguelite UI sprite sheet for "Run Results, Defeat/Victory Banner & Meta-Progression Altar" (DeathResultsScreenPresenter, MetaProgressionScreenPresenter).
Style: crisp 16-bit gothic dark-iron, carved bone, and runic obsidian frames (#1A1624 interior, #3D354B highlighted bevel, #110E1B 1px solid outline), zero anti-aliasing, transparent background (RGBA32), FilterMode.Point.
Elements included on sheet: Contains: (1) `THE OVERLORD HAS FALLEN` & `ETERNAL SOVEREIGN VICTORIOUS` Gothic Banners (`256x48 px`), (2) Run Statistics Scroll & Soul Essence Tally Counter (`220x140 px`), (3) Meta-Progression Dark Sanctum Upgrade Altar Icons (12 permanent blessing icons `24x24 px`: Max HP, Base Damage, Armor, Move Speed, Greed, Soul Harvest, Starting Rerolls, Relic Capacity).
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | PPU | Filter | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- |
| **UI Atlas Sheet** | `Assets/_Game/Art/UI/Sheets/ui_06_death_results_and_meta_shop_sheet.png` | `16` | `Point`, `RGBA32` | Runtime `Image` solid-color quads in `DeathResultsScreenPresenter, MetaProgressionScreenPresenter` |
