# Voidstrider Shroud (`arm_voidstrider_shroud`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #29/30 (`07_Armors`)
> **Catalog ID:** `"arm_voidstrider_shroud"` (`ItemRarity.Epic`)
> **Lore Description:** *Phases the wearer between physical dimensions during sprints.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Voidstrider Shroud* (Phases the wearer between physical dimensions during sprints.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_voidstrider_shroud_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Voidstrider Shroud" (Phases the wearer between physical dimensions during sprints.): satisfying buckle strap and epic-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_voidstrider_shroud_mitigate.wav`):**
   > `Defensive impact deflection sound when "Voidstrider Shroud" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_voidstrider_shroud_equip.wav` |
| **Deflect** | `"armor_mitigate_voidstrider_shroud"` | `"armor_mitigate_voidstrider_shroud"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_voidstrider_shroud_mitigate.wav` |
