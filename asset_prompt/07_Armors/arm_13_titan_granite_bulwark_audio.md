# Titan Granite Bulwark (`arm_titan_granite_bulwark`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #13/30 (`07_Armors`)
> **Catalog ID:** `"arm_titan_granite_bulwark"` (`ItemRarity.Epic`)
> **Lore Description:** *Solid stone armor turning the wearer into a walking fortress.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Titan Granite Bulwark* (Solid stone armor turning the wearer into a walking fortress.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_titan_granite_bulwark_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Titan Granite Bulwark" (Solid stone armor turning the wearer into a walking fortress.): satisfying buckle strap and epic-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_titan_granite_bulwark_mitigate.wav`):**
   > `Defensive impact deflection sound when "Titan Granite Bulwark" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_titan_granite_bulwark_equip.wav` |
| **Deflect** | `"armor_mitigate_titan_granite_bulwark"` | `"armor_mitigate_titan_granite_bulwark"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_titan_granite_bulwark_mitigate.wav` |
