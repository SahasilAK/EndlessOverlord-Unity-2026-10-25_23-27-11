# Skill Tree Graph, Node Frames & Pre-Run Loadout UI (`ui_03_skill_tree_and_loadout`) — UI & HUD Pixel Art Prompt Specification

> **Asset Category:** User Interface & HUD Module (`10_UI_And_HUD`)
> **Target Presenters:** `SkillTreeScreenPresenter, LoadoutScreenPresenter`
> **Technical Spec:** `16 PPU`, `FilterMode.Point`, `RGBA32`, 9-Slice Borders configured in Unity Sprite Editor.

---

## 1. Module Contents & Visual Design

- **UI Style Rule:** Dark Gothic Obsidian-and-Bone frames (`#1A1624` fill, `#3D354B` bevel, `#110E1B` 1px outer border) so high-saturation skill/item icons and health bars pop with instant legibility.
- **Included Elements:** Contains: (1) 4-Branch Skill Tree Background Canvas (`320x180 px` dark constellation parchment), (2) Skill Node Frames (`28x28 px`: Locked `#3A3542`, Unlockable Pulsing `#30E680`, Active Rank 1–9 `#9B59B6`, Mastered Rank 10–20 Gold `#F1C40F`), (3) Branch Connector Pipes (`16x8 px`: Inactive & Lit), (4) Pre-Run Loadout Paper-Doll Slots (Weapon, Armor, 4 Active Skills, Starting Spell).

### 1.1 Copy-Paste UI Sprite Sheet Prompt (`ui_03_skill_tree_and_loadout_sheet.png`)
```text
Pixel art dark fantasy roguelite UI sprite sheet for "Skill Tree Graph, Node Frames & Pre-Run Loadout UI" (SkillTreeScreenPresenter, LoadoutScreenPresenter).
Style: crisp 16-bit gothic dark-iron, carved bone, and runic obsidian frames (#1A1624 interior, #3D354B highlighted bevel, #110E1B 1px solid outline), zero anti-aliasing, transparent background (RGBA32), FilterMode.Point.
Elements included on sheet: Contains: (1) 4-Branch Skill Tree Background Canvas (`320x180 px` dark constellation parchment), (2) Skill Node Frames (`28x28 px`: Locked `#3A3542`, Unlockable Pulsing `#30E680`, Active Rank 1–9 `#9B59B6`, Mastered Rank 10–20 Gold `#F1C40F`), (3) Branch Connector Pipes (`16x8 px`: Inactive & Lit), (4) Pre-Run Loadout Paper-Doll Slots (Weapon, Armor, 4 Active Skills, Starting Spell).
```

---

## 2. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | PPU | Filter | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- |
| **UI Atlas Sheet** | `Assets/_Game/Art/UI/Sheets/ui_03_skill_tree_and_loadout_sheet.png` | `16` | `Point`, `RGBA32` | Runtime `Image` solid-color quads in `SkillTreeScreenPresenter, LoadoutScreenPresenter` |
