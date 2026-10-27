# Abyssal Dreadplate (`arm_abyssal_dreadplate`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #15/30 (`07_Armors`)
> **Catalog ID:** `"arm_abyssal_dreadplate"` (`ItemRarity.Epic`)
> **Lore Description:** *Heavy armor forged in crushing deep trenches, terrifying foes.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Abyssal Dreadplate* (Heavy armor forged in crushing deep trenches, terrifying foes.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_abyssal_dreadplate_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Abyssal Dreadplate" (Heavy armor forged in crushing deep trenches, terrifying foes.): satisfying buckle strap and epic-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_abyssal_dreadplate_mitigate.wav`):**
   > `Defensive impact deflection sound when "Abyssal Dreadplate" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_abyssal_dreadplate_equip.wav` |
| **Deflect** | `"armor_mitigate_abyssal_dreadplate"` | `"armor_mitigate_abyssal_dreadplate"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_abyssal_dreadplate_mitigate.wav` |
