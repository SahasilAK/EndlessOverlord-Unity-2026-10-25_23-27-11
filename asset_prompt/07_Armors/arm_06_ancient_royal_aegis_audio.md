# Ancient Royal Aegis (`arm_ancient_royal_aegis`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #06/30 (`07_Armors`)
> **Catalog ID:** `"arm_ancient_royal_aegis"` (`ItemRarity.Epic`)
> **Lore Description:** *Ceremonial plate of forgotten kings blessed with indestructible wards.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Ancient Royal Aegis* (Ceremonial plate of forgotten kings blessed with indestructible wards.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_ancient_royal_aegis_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Ancient Royal Aegis" (Ceremonial plate of forgotten kings blessed with indestructible wards.): satisfying buckle strap and epic-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_ancient_royal_aegis_mitigate.wav`):**
   > `Defensive impact deflection sound when "Ancient Royal Aegis" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_ancient_royal_aegis_equip.wav` |
| **Deflect** | `"armor_mitigate_ancient_royal_aegis"` | `"armor_mitigate_ancient_royal_aegis"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_ancient_royal_aegis_mitigate.wav` |
