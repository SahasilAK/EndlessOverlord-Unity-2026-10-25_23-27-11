# Shadow Cloak (`arm_shadow_cloak`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #05/30 (`07_Armors`)
> **Catalog ID:** `"arm_shadow_cloak"` (`ItemRarity.Uncommon`)
> **Lore Description:** *Woven from distilled twilight, muffling steps and evading detection.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Shadow Cloak* (Woven from distilled twilight, muffling steps and evading detection.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_shadow_cloak_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Shadow Cloak" (Woven from distilled twilight, muffling steps and evading detection.): satisfying buckle strap and uncommon-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_shadow_cloak_mitigate.wav`):**
   > `Defensive impact deflection sound when "Shadow Cloak" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_shadow_cloak_equip.wav` |
| **Deflect** | `"armor_mitigate_shadow_cloak"` | `"armor_mitigate_shadow_cloak"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_shadow_cloak_mitigate.wav` |
