# Rune Inscribed Plate (`arm_rune_inscribed_plate`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #22/30 (`07_Armors`)
> **Catalog ID:** `"arm_rune_inscribed_plate"` (`ItemRarity.Rare`)
> **Lore Description:** *Glows with defensive runic inscriptions absorbing shocks.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Rune Inscribed Plate* (Glows with defensive runic inscriptions absorbing shocks.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_rune_inscribed_plate_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Rune Inscribed Plate" (Glows with defensive runic inscriptions absorbing shocks.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_rune_inscribed_plate_mitigate.wav`):**
   > `Defensive impact deflection sound when "Rune Inscribed Plate" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_rune_inscribed_plate_equip.wav` |
| **Deflect** | `"armor_mitigate_rune_inscribed_plate"` | `"armor_mitigate_rune_inscribed_plate"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_rune_inscribed_plate_mitigate.wav` |
