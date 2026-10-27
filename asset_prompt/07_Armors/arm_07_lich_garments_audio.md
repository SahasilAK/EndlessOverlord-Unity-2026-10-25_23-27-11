# Lich Garments (`arm_lich_garments`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #07/30 (`07_Armors`)
> **Catalog ID:** `"arm_lich_garments"` (`ItemRarity.Epic`)
> **Lore Description:** *Empowered vestments radiating perpetual cold and death magic.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Lich Garments* (Empowered vestments radiating perpetual cold and death magic.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_lich_garments_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Lich Garments" (Empowered vestments radiating perpetual cold and death magic.): satisfying buckle strap and epic-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_lich_garments_mitigate.wav`):**
   > `Defensive impact deflection sound when "Lich Garments" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_lich_garments_equip.wav` |
| **Deflect** | `"armor_mitigate_lich_garments"` | `"armor_mitigate_lich_garments"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_lich_garments_mitigate.wav` |
