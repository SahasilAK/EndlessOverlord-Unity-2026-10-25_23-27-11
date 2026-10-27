# Ashen Cinder Robes (`arm_ashen_cinder_robes`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #23/30 (`07_Armors`)
> **Catalog ID:** `"arm_ashen_cinder_robes"` (`ItemRarity.Common`)
> **Lore Description:** *Charred robes radiating faint embers.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Ashen Cinder Robes* (Charred robes radiating faint embers.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_ashen_cinder_robes_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Ashen Cinder Robes" (Charred robes radiating faint embers.): satisfying buckle strap and common-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_ashen_cinder_robes_mitigate.wav`):**
   > `Defensive impact deflection sound when "Ashen Cinder Robes" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_ashen_cinder_robes_equip.wav` |
| **Deflect** | `"armor_mitigate_ashen_cinder_robes"` | `"armor_mitigate_ashen_cinder_robes"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_ashen_cinder_robes_mitigate.wav` |
