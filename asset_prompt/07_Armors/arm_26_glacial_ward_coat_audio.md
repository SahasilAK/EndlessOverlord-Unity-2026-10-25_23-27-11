# Glacial Ward Coat (`arm_glacial_ward_coat`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #26/30 (`07_Armors`)
> **Catalog ID:** `"arm_glacial_ward_coat"` (`ItemRarity.Rare`)
> **Lore Description:** *Chilled pelts and ice runes deflecting searing blows.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Glacial Ward Coat* (Chilled pelts and ice runes deflecting searing blows.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_glacial_ward_coat_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Glacial Ward Coat" (Chilled pelts and ice runes deflecting searing blows.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_glacial_ward_coat_mitigate.wav`):**
   > `Defensive impact deflection sound when "Glacial Ward Coat" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_glacial_ward_coat_equip.wav` |
| **Deflect** | `"armor_mitigate_glacial_ward_coat"` | `"armor_mitigate_glacial_ward_coat"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_glacial_ward_coat_mitigate.wav` |
