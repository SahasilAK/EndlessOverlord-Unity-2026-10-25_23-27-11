# Main Menu, Title Logo & Character Select UI (`ui_01_main_menu_and_char_select`) — UI & Menu Audio Prompt Specification

> **Asset Category:** User Interface Audio (`10_UI_And_HUD`)
> **Target Presenters:** `MainMenuPresenter, CharacterSelectPresenter, BiomeSelectPresenter`
> **Acoustic Profile:** Ominous title screen dark-choir drone + heavy stone button hover/click (`ui_click`, `ui_hover`) + class selection signature sting.

---

## 1. Step-by-Step UI Audio Prompts

1. **Primary Action / Confirm (`sfx_ui_01_main_menu_and_char_select_confirm.wav`):**
   > `Crisp dark-fantasy UI confirmation sound for Main Menu, Title Logo & Character Select UI: Ominous title screen dark-choir drone + heavy stone button hover/click (`ui_click`, `ui_hover`) + class selection signature sting., zero latency transient, 0.22s, stereo 44.1kHz 16-bit WAV.`
2. **Hover / Navigation Tick (`sfx_ui_01_main_menu_and_char_select_hover.wav`):**
   > `Subtle carved-stone and metallic UI hover tick, 0.06s, `-8 dBFS`, mono 44.1kHz WAV.`
3. **Modal Open / Transition (`sfx_ui_01_main_menu_and_char_select_modal_open.wav`):**
   > `Gothic iron-and-parchment panel slide with a faint arcane resonance, 0.30s, stereo 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| UI Event | Cue / Presenter | Target `.wav` Path |
| :--- | :--- | :--- |
| **Confirm / Trigger** | `MainMenuPresenter, CharacterSelectPresenter, BiomeSelectPresenter` (`CueBus.Publish`) | `Assets/_Game/Audio/SFX/UI/sfx_ui_01_main_menu_and_char_select_confirm.wav` |
| **Hover / Select** | `MainMenuPresenter, CharacterSelectPresenter, BiomeSelectPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_01_main_menu_and_char_select_hover.wav` |
| **Modal Open** | `MainMenuPresenter, CharacterSelectPresenter, BiomeSelectPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_01_main_menu_and_char_select_modal_open.wav` |
