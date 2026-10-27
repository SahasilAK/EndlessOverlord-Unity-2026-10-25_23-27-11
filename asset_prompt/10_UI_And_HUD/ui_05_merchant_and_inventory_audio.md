# Rare Merchant Shop, Item Comparison & Inventory Grid (`ui_05_merchant_and_inventory`) — UI & Menu Audio Prompt Specification

> **Asset Category:** User Interface Audio (`10_UI_And_HUD`)
> **Target Presenters:** `MerchantShopModalPresenter, TreasureChestModalPresenter`
> **Acoustic Profile:** Jingling coin purse purchase (`MerchantPurchase` / `ItemPurchased`) + anvil/cloth equip lock (`ItemEquipped`) + insufficient-gold dull buzz.

---

## 1. Step-by-Step UI Audio Prompts

1. **Primary Action / Confirm (`sfx_ui_05_merchant_and_inventory_confirm.wav`):**
   > `Crisp dark-fantasy UI confirmation sound for Rare Merchant Shop, Item Comparison & Inventory Grid: Jingling coin purse purchase (`MerchantPurchase` / `ItemPurchased`) + anvil/cloth equip lock (`ItemEquipped`) + insufficient-gold dull buzz., zero latency transient, 0.22s, stereo 44.1kHz 16-bit WAV.`
2. **Hover / Navigation Tick (`sfx_ui_05_merchant_and_inventory_hover.wav`):**
   > `Subtle carved-stone and metallic UI hover tick, 0.06s, `-8 dBFS`, mono 44.1kHz WAV.`
3. **Modal Open / Transition (`sfx_ui_05_merchant_and_inventory_modal_open.wav`):**
   > `Gothic iron-and-parchment panel slide with a faint arcane resonance, 0.30s, stereo 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| UI Event | Cue / Presenter | Target `.wav` Path |
| :--- | :--- | :--- |
| **Confirm / Trigger** | `MerchantShopModalPresenter, TreasureChestModalPresenter` (`CueBus.Publish`) | `Assets/_Game/Audio/SFX/UI/sfx_ui_05_merchant_and_inventory_confirm.wav` |
| **Hover / Select** | `MerchantShopModalPresenter, TreasureChestModalPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_05_merchant_and_inventory_hover.wav` |
| **Modal Open** | `MerchantShopModalPresenter, TreasureChestModalPresenter` | `Assets/_Game/Audio/SFX/UI/sfx_ui_05_merchant_and_inventory_modal_open.wav` |
