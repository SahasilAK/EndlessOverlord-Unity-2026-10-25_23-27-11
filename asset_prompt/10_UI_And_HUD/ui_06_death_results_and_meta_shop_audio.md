# Run Results, Defeat/Victory Banner & Meta-Progression Altar (`ui_06_death_results_and_meta_shop`) — UI & Menu Audio Prompt Specification

> **Asset Category:** User Interface Audio (`10_UI_And_HUD`)
> **Target Presenters:** `DeathResultsScreenPresenter, MetaProgressionScreenPresenter`
> **Acoustic Profile:** Solemn cathedral bell toll on defeat + rapid counting tick for Soul Essence tally + permanent dark-altar blessing engraving boom.

---

## 1. Step-by-Step UI Audio Prompts

1. **Primary Action / Confirm (`sfx_ui_06_death_results_and_meta_shop_confirm.wav`):**
   > `Crisp dark-fantasy UI confirmation sound for Run Results, Defeat/Victory Banner & Meta-Progression Altar: Solemn cathedral bell toll on defeat + rapid counting tick for Soul Essence tally + permanent dark-altar blessing engraving boom., zero latency transient, 0.22s, stereo 44.1kHz 16-bit WAV.`
2. **Hover / Navigation Tick (`sfx_ui_06_death_results_and_meta_shop_hover.wav`):**
   > `Subtle carved-stone and metallic UI hover tick, 0.06s, `-8 dBFS`, mono 44.1kHz WAV.`
3. **Modal Open / Transition (`sfx_ui_06_death_results_and_meta_shop_modal_open.wav`):**
   > `Gothic iron-and-parchment panel slide with a faint arcane resonance, 0.30s, stereo 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| UI Event | Cue / Presenter | Target `.wav` Path |
| :--- | :--- | :--- |
| **Confirm / Trigger** | `DeathResultsScreenPresenter, MetaProgressionScreenPresenter` (`CueBus.Publish`) | `Assets/_Game/Audio/SFX/UI/sfx_ui_06_death_results_and_meta_shop_confirm.wav` |
| **Hover / Select** | `DeathResultsScreenPresenter, MetaProgressionScreenPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_06_death_results_and_meta_shop_hover.wav` |
| **Modal Open** | `DeathResultsScreenPresenter, MetaProgressionScreenPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_06_death_results_and_meta_shop_modal_open.wav` |
