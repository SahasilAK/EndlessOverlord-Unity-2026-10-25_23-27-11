# Wave Reward Cards, Spell Evolution & Synergy Modals (`ui_04_wave_rewards_and_modals`) — UI & HUD Pixel Art Prompt Specification

> **Asset Category:** User Interface & HUD Module (`10_UI_And_HUD`)
> **Target Presenters:** `WaveRewardModalPresenter, PauseMenuPresenter`
> **Technical Spec:** `16 PPU`, `FilterMode.Point`, `RGBA32`, 9-Slice Borders configured in Unity Sprite Editor.

---

## 1. Module Contents & Visual Design

- **UI Style Rule:** Dark Gothic Obsidian-and-Bone frames (`#1A1624` fill, `#3D354B` bevel, `#110E1B` 1px outer border) so high-saturation skill/item icons and health bars pop with instant legibility.
- **Included Elements:** Contains: (1) 3-Choice Wave Reward Card Frames (`80x112 px` in Common `#95A5A6`, Uncommon `#2ECC71`, Rare `#3498DB`, Epic `#9B59B6`, Legendary `#F1C40F`, and Ascended Evolution `#FF0066`), (2) Card Flip / Reveal Shimmer Strip (`320x112 px`, 4 frames), (3) Reroll & Banish Runic Buttons (`48x20 px`), (4) Pause & Settings Sliders.

### 1.1 Copy-Paste UI Sprite Sheet Prompt (`ui_04_wave_rewards_and_modals_sheet.png`)
```text
Pixel art dark fantasy roguelite UI sprite sheet for "Wave Reward Cards, Spell Evolution & Synergy Modals" (WaveRewardModalPresenter, PauseMenuPresenter).
Style: crisp 16-bit gothic dark-iron, carved bone, and runic obsidian frames (#1A1624 interior, #3D354B highlighted bevel, #110E1B 1px solid outline), zero anti-aliasing, transparent background (RGBA32), FilterMode.Point.
Elements included on sheet: Contains: (1) 3-Choice Wave Reward Card Frames (`80x112 px` in Common `#95A5A6`, Uncommon `#2ECC71`, Rare `#3498DB`, Epic `#9B59B6`, Legendary `#F1C40F`, and Ascended Evolution `#FF0066`), (2) Card Flip / Reveal Shimmer Strip (`320x112 px`, 4 frames), (3) Reroll & Banish Runic Buttons (`48x20 px`), (4) Pause & Settings Sliders.
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | PPU | Filter | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- |
| **UI Atlas Sheet** | `Assets/_Game/Art/UI/Sheets/ui_04_wave_rewards_and_modals_sheet.png` | `16` | `Point`, `RGBA32` | Runtime `Image` solid-color quads in `WaveRewardModalPresenter, PauseMenuPresenter` |
