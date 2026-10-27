# Crypt Robes (`item_crypt_robes`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #01/30 (`07_Armors`)
> **Catalog ID:** `"item_crypt_robes"` (`ItemRarity.Common`)
> **Lore Description:** *Ancient burial shrouds reinforced with bone splints.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Crypt Robes* (Ancient burial shrouds reinforced with bone splints.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_item_crypt_robes_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Crypt Robes" (Ancient burial shrouds reinforced with bone splints.): satisfying buckle strap and common-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_item_crypt_robes_mitigate.wav`):**
   > `Defensive impact deflection sound when "Crypt Robes" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_item_crypt_robes_equip.wav` |
| **Deflect** | `"armor_mitigate_crypt_robes"` | `"armor_mitigate_crypt_robes"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_item_crypt_robes_mitigate.wav` |
