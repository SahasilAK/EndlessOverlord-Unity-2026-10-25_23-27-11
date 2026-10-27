# Dreadlord Pauldrons & Plate (`arm_dreadlord_pauldrons`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #28/30 (`07_Armors`)
> **Catalog ID:** `"arm_dreadlord_pauldrons"` (`ItemRarity.Epic`)
> **Lore Description:** *Imposing horned armor commanding dark obedience.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Dreadlord Pauldrons & Plate* (Imposing horned armor commanding dark obedience.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_dreadlord_pauldrons_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Dreadlord Pauldrons & Plate" (Imposing horned armor commanding dark obedience.): satisfying buckle strap and epic-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_dreadlord_pauldrons_mitigate.wav`):**
   > `Defensive impact deflection sound when "Dreadlord Pauldrons & Plate" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_dreadlord_pauldrons_equip.wav` |
| **Deflect** | `"armor_mitigate_dreadlord_pauldrons"` | `"armor_mitigate_dreadlord_pauldrons"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_dreadlord_pauldrons_mitigate.wav` |
