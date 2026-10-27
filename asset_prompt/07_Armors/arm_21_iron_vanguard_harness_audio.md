# Iron Vanguard Harness (`arm_iron_vanguard_harness`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #21/30 (`07_Armors`)
> **Catalog ID:** `"arm_iron_vanguard_harness"` (`ItemRarity.Common`)
> **Lore Description:** *Standard heavy harness of the frontline shock troops.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Iron Vanguard Harness* (Standard heavy harness of the frontline shock troops.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_iron_vanguard_harness_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Iron Vanguard Harness" (Standard heavy harness of the frontline shock troops.): satisfying buckle strap and common-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_iron_vanguard_harness_mitigate.wav`):**
   > `Defensive impact deflection sound when "Iron Vanguard Harness" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_iron_vanguard_harness_equip.wav` |
| **Deflect** | `"armor_mitigate_iron_vanguard_harness"` | `"armor_mitigate_iron_vanguard_harness"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_iron_vanguard_harness_mitigate.wav` |
