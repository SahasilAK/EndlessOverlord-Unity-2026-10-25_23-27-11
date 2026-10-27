# Infernal Plate (`arm_infernal_plate`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #03/30 (`07_Armors`)
> **Catalog ID:** `"arm_infernal_plate"` (`ItemRarity.Uncommon`)
> **Lore Description:** *Forged in sulfurous magma pits, burning any invader bold enough to strike.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Infernal Plate* (Forged in sulfurous magma pits, burning any invader bold enough to strike.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_infernal_plate_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Infernal Plate" (Forged in sulfurous magma pits, burning any invader bold enough to strike.): satisfying buckle strap and uncommon-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_infernal_plate_mitigate.wav`):**
   > `Defensive impact deflection sound when "Infernal Plate" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_infernal_plate_equip.wav` |
| **Deflect** | `"armor_mitigate_infernal_plate"` | `"armor_mitigate_infernal_plate"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_infernal_plate_mitigate.wav` |
