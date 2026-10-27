# Combat HUD, Health/Resource Bars, Ability Slots & Boss Banner (`ui_02_combat_hud_and_bars`) — UI & Menu Audio Prompt Specification

> **Asset Category:** User Interface Audio (`10_UI_And_HUD`)
> **Target Presenters:** `HudPresenter, BossEncounterBannerPresenter, InRunEventBannerPresenter`
> **Acoustic Profile:** Subtle cooldown-ready crystal ping + low-HP heartbeat warning pulse + wave-start war horn (`WaveStart`) + wave-clear chord (`WaveComplete`).

---

## 1. Step-by-Step UI Audio Prompts

1. **Primary Action / Confirm (`sfx_ui_02_combat_hud_and_bars_confirm.wav`):**
   > `Crisp dark-fantasy UI confirmation sound for Combat HUD, Health/Resource Bars, Ability Slots & Boss Banner: Subtle cooldown-ready crystal ping + low-HP heartbeat warning pulse + wave-start war horn (`WaveStart`) + wave-clear chord (`WaveComplete`)., zero latency transient, 0.22s, stereo 44.1kHz 16-bit WAV.`
2. **Hover / Navigation Tick (`sfx_ui_02_combat_hud_and_bars_hover.wav`):**
   > `Subtle carved-stone and metallic UI hover tick, 0.06s, `-8 dBFS`, mono 44.1kHz WAV.`
3. **Modal Open / Transition (`sfx_ui_02_combat_hud_and_bars_modal_open.wav`):**
   > `Gothic iron-and-parchment panel slide with a faint arcane resonance, 0.30s, stereo 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| UI Event | Cue / Presenter | Target `.wav` Path |
| :--- | :--- | :--- |
| **Confirm / Trigger** | `HudPresenter, BossEncounterBannerPresenter, InRunEventBannerPresenter` (`CueBus.Publish`) | `Assets/_Game/Audio/SFX/UI/sfx_ui_02_combat_hud_and_bars_confirm.wav` |
| **Hover / Select** | `HudPresenter, BossEncounterBannerPresenter, InRunEventBannerPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_02_combat_hud_and_bars_hover.wav` |
| **Modal Open** | `HudPresenter, BossEncounterBannerPresenter, InRunEventBannerPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_02_combat_hud_and_bars_modal_open.wav` |
