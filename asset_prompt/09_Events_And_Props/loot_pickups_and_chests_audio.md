# Loot Pickups, Chests & Merchant Cart (`loot_pickups_and_chests`) — Event & Prop Audio Prompt Specification

> **Asset Category:** In-Run Event / Prop Audio (`09_Events_And_Props`)
> **Primary Cue Binding:** `CueIds.ChestOpen` (`"chest_open"`)
> **Acoustic Identity:** Heavy iron chest latch unbolting + creaking lid + cascading gold/gem fanfare (`CueIds.ChestSpawn`, `CueIds.ChestOpen`, `CueIds.MerchantPurchase`).

---

## 1. Step-by-Step Audio Variations

1. **Spawn / Warning Notification (`sfx_loot_pickups_and_chests_appear.wav`):**
   > `Dark fantasy dungeon event notification sound for "Loot Pickups, Chests & Merchant Cart": Heavy iron chest latch unbolting + creaking lid + cascading gold/gem fanfare (`CueIds.ChestSpawn`, `CueIds.ChestOpen`, `CueIds.MerchantPurchase`)., distinct spatial presence, 0.65s, stereo 44.1kHz WAV.`
2. **Interaction / Activation Trigger (`sfx_loot_pickups_and_chests_activate.wav` — `CueIds.ChestOpen` / `"chest_open"`):**
   > `Satisfying interactive activation sound for "Loot Pickups, Chests & Merchant Cart" (Complete world pickup sheet: (1) Tiered Treasure Chests (Common Iron, Rare Azure, Epic Void, Legendary Sovereign Gold — Closed & Open frames `24x24`), (2) Gold Coin stack (`16x16`), (3) Soul Essence orb (`16x16`), (4) Rare Merchant Smuggler hooded figure & lantern cart (`48x48`).): crisp transient and resonant tail, 0.75s, mono 44.1kHz WAV.`

---

## 2. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Activate / Trigger** | `CueIds.ChestOpen` | `"chest_open"` | `Assets/_Game/Audio/SFX/Events/sfx_loot_pickups_and_chests_chest_open.wav` |
