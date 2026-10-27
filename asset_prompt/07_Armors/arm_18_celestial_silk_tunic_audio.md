# Celestial Silk Tunic (`arm_celestial_silk_tunic`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #18/30 (`07_Armors`)
> **Catalog ID:** `"arm_celestial_silk_tunic"` (`ItemRarity.Rare`)
> **Lore Description:** *Woven from fallen stars, accelerating spellcasting.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Celestial Silk Tunic* (Woven from fallen stars, accelerating spellcasting.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_celestial_silk_tunic_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Celestial Silk Tunic" (Woven from fallen stars, accelerating spellcasting.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_celestial_silk_tunic_mitigate.wav`):**
   > `Defensive impact deflection sound when "Celestial Silk Tunic" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_celestial_silk_tunic_equip.wav` |
| **Deflect** | `"armor_mitigate_celestial_silk_tunic"` | `"armor_mitigate_celestial_silk_tunic"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_celestial_silk_tunic_mitigate.wav` |
