# Necrotic Shroud (`item_necrotic_shroud`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #02/30 (`07_Armors`)
> **Catalog ID:** `"item_necrotic_shroud"` (`ItemRarity.Rare`)
> **Lore Description:** *A heavy mantle steeped in dark sorcery, shielding the wearer from harm.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Necrotic Shroud* (A heavy mantle steeped in dark sorcery, shielding the wearer from harm.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_item_necrotic_shroud_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Necrotic Shroud" (A heavy mantle steeped in dark sorcery, shielding the wearer from harm.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_item_necrotic_shroud_mitigate.wav`):**
   > `Defensive impact deflection sound when "Necrotic Shroud" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_item_necrotic_shroud_equip.wav` |
| **Deflect** | `"armor_mitigate_necrotic_shroud"` | `"armor_mitigate_necrotic_shroud"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_item_necrotic_shroud_mitigate.wav` |
