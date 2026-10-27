# Skill Tree Graph, Node Frames & Pre-Run Loadout UI (`ui_03_skill_tree_and_loadout`) — UI & Menu Audio Prompt Specification

> **Asset Category:** User Interface Audio (`10_UI_And_HUD`)
> **Target Presenters:** `SkillTreeScreenPresenter, LoadoutScreenPresenter`
> **Acoustic Profile:** Resonant rune-carving skill unlock sound (`SkillUnlocked`) + rank-up harmonic pitch climb + milestone 5/10/15/20 ascension fanfare.

---

## 1. Step-by-Step UI Audio Prompts

1. **Primary Action / Confirm (`sfx_ui_03_skill_tree_and_loadout_confirm.wav`):**
   > `Crisp dark-fantasy UI confirmation sound for Skill Tree Graph, Node Frames & Pre-Run Loadout UI: Resonant rune-carving skill unlock sound (`SkillUnlocked`) + rank-up harmonic pitch climb + milestone 5/10/15/20 ascension fanfare., zero latency transient, 0.22s, stereo 44.1kHz 16-bit WAV.`
2. **Hover / Navigation Tick (`sfx_ui_03_skill_tree_and_loadout_hover.wav`):**
   > `Subtle carved-stone and metallic UI hover tick, 0.06s, `-8 dBFS`, mono 44.1kHz WAV.`
3. **Modal Open / Transition (`sfx_ui_03_skill_tree_and_loadout_modal_open.wav`):**
   > `Gothic iron-and-parchment panel slide with a faint arcane resonance, 0.30s, stereo 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| UI Event | Cue / Presenter | Target `.wav` Path |
| :--- | :--- | :--- |
| **Confirm / Trigger** | `SkillTreeScreenPresenter, LoadoutScreenPresenter` (`CueBus.Publish`) | `Assets/_Game/Audio/SFX/UI/sfx_ui_03_skill_tree_and_loadout_confirm.wav` |
| **Hover / Select** | `SkillTreeScreenPresenter, LoadoutScreenPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_03_skill_tree_and_loadout_hover.wav` |
| **Modal Open** | `SkillTreeScreenPresenter, LoadoutScreenPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_03_skill_tree_and_loadout_modal_open.wav` |
