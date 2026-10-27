# Chitinous Husk (`arm_chitinous_husk`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #19/30 (`07_Armors`)
> **Catalog ID:** `"arm_chitinous_husk"` (`ItemRarity.Common`)
> **Lore Description:** *Harvested from giant cave burrowers, light and durable.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Chitinous Husk* (Harvested from giant cave burrowers, light and durable.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_chitinous_husk_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Chitinous Husk" (Harvested from giant cave burrowers, light and durable.): satisfying buckle strap and common-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_chitinous_husk_mitigate.wav`):**
   > `Defensive impact deflection sound when "Chitinous Husk" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_chitinous_husk_equip.wav` |
| **Deflect** | `"armor_mitigate_chitinous_husk"` | `"armor_mitigate_chitinous_husk"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_chitinous_husk_mitigate.wav` |
