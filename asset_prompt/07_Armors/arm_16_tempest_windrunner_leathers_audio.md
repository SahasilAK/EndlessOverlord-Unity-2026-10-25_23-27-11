# Tempest Windrunner Leathers (`arm_tempest_windrunner_leathers`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #16/30 (`07_Armors`)
> **Catalog ID:** `"arm_tempest_windrunner_leathers"` (`ItemRarity.Common`)
> **Lore Description:** *Light boiled leather built for extreme mobility.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Tempest Windrunner Leathers* (Light boiled leather built for extreme mobility.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_tempest_windrunner_leathers_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Tempest Windrunner Leathers" (Light boiled leather built for extreme mobility.): satisfying buckle strap and common-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_tempest_windrunner_leathers_mitigate.wav`):**
   > `Defensive impact deflection sound when "Tempest Windrunner Leathers" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_tempest_windrunner_leathers_equip.wav` |
| **Deflect** | `"armor_mitigate_tempest_windrunner_leathers"` | `"armor_mitigate_tempest_windrunner_leathers"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_tempest_windrunner_leathers_mitigate.wav` |
