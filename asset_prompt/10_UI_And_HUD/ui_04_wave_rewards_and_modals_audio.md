# Wave Reward Cards, Spell Evolution & Synergy Modals (`ui_04_wave_rewards_and_modals`) — UI & Menu Audio Prompt Specification

> **Asset Category:** User Interface Audio (`10_UI_And_HUD`)
> **Target Presenters:** `WaveRewardModalPresenter, PauseMenuPresenter`
> **Acoustic Profile:** Mystical tarot/grimoire card deal whoosh + rarity-tiered reward selection chime + ascended spell evolution thunderous choir sting (`SpellEvolutionUnlock`).

---

## 1. Step-by-Step UI Audio Prompts

1. **Primary Action / Confirm (`sfx_ui_04_wave_rewards_and_modals_confirm.wav`):**
   > `Crisp dark-fantasy UI confirmation sound for Wave Reward Cards, Spell Evolution & Synergy Modals: Mystical tarot/grimoire card deal whoosh + rarity-tiered reward selection chime + ascended spell evolution thunderous choir sting (`SpellEvolutionUnlock`)., zero latency transient, 0.22s, stereo 44.1kHz 16-bit WAV.`
2. **Hover / Navigation Tick (`sfx_ui_04_wave_rewards_and_modals_hover.wav`):**
   > `Subtle carved-stone and metallic UI hover tick, 0.06s, `-8 dBFS`, mono 44.1kHz WAV.`
3. **Modal Open / Transition (`sfx_ui_04_wave_rewards_and_modals_modal_open.wav`):**
   > `Gothic iron-and-parchment panel slide with a faint arcane resonance, 0.30s, stereo 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| UI Event | Cue / Presenter | Target `.wav` Path |
| :--- | :--- | :--- |
| **Confirm / Trigger** | `WaveRewardModalPresenter, PauseMenuPresenter` (`CueBus.Publish`) | `Assets/_Game/Audio/SFX/UI/sfx_ui_04_wave_rewards_and_modals_confirm.wav` |
| **Hover / Select** | `WaveRewardModalPresenter, PauseMenuPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_04_wave_rewards_and_modals_hover.wav` |
| **Modal Open** | `WaveRewardModalPresenter, PauseMenuPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_04_wave_rewards_and_modals_modal_open.wav` |
