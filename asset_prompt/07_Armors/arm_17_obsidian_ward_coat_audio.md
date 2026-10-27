# Obsidian Ward Coat (`arm_obsidian_ward_coat`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #17/30 (`07_Armors`)
> **Catalog ID:** `"arm_obsidian_ward_coat"` (`ItemRarity.Uncommon`)
> **Lore Description:** *Volcanic obsidian plates sewn into thick hide.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Obsidian Ward Coat* (Volcanic obsidian plates sewn into thick hide.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_obsidian_ward_coat_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Obsidian Ward Coat" (Volcanic obsidian plates sewn into thick hide.): satisfying buckle strap and uncommon-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_obsidian_ward_coat_mitigate.wav`):**
   > `Defensive impact deflection sound when "Obsidian Ward Coat" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_obsidian_ward_coat_equip.wav` |
| **Deflect** | `"armor_mitigate_obsidian_ward_coat"` | `"armor_mitigate_obsidian_ward_coat"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_obsidian_ward_coat_mitigate.wav` |
