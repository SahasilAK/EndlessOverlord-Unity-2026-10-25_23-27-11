# Plague Doctor Vestments (`arm_plague_doctor_vestments`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #12/30 (`07_Armors`)
> **Catalog ID:** `"arm_plague_doctor_vestments"` (`ItemRarity.Uncommon`)
> **Lore Description:** *Waxed leathers steeped in aromatic anti-toxins.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Plague Doctor Vestments* (Waxed leathers steeped in aromatic anti-toxins.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_plague_doctor_vestments_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Plague Doctor Vestments" (Waxed leathers steeped in aromatic anti-toxins.): satisfying buckle strap and uncommon-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_plague_doctor_vestments_mitigate.wav`):**
   > `Defensive impact deflection sound when "Plague Doctor Vestments" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_plague_doctor_vestments_equip.wav` |
| **Deflect** | `"armor_mitigate_plague_doctor_vestments"` | `"armor_mitigate_plague_doctor_vestments"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_plague_doctor_vestments_mitigate.wav` |
