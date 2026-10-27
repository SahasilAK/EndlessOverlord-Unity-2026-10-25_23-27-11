# Void Mantle (`arm_void_mantle`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #10/30 (`07_Armors`)
> **Catalog ID:** `"arm_void_mantle"` (`ItemRarity.Epic`)
> **Lore Description:** *Torn from the void boundary, swallowing incoming projectile energy.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Void Mantle* (Torn from the void boundary, swallowing incoming projectile energy.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_void_mantle_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Void Mantle" (Torn from the void boundary, swallowing incoming projectile energy.): satisfying buckle strap and epic-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_void_mantle_mitigate.wav`):**
   > `Defensive impact deflection sound when "Void Mantle" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_void_mantle_equip.wav` |
| **Deflect** | `"armor_mitigate_void_mantle"` | `"armor_mitigate_void_mantle"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_void_mantle_mitigate.wav` |
