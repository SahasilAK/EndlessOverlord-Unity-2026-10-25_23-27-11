# Blightward Jerkin (`arm_blightward_jerkin`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #27/30 (`07_Armors`)
> **Catalog ID:** `"arm_blightward_jerkin"` (`ItemRarity.Common`)
> **Lore Description:** *Treated jerkin resisting acid and dungeon toxins.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Blightward Jerkin* (Treated jerkin resisting acid and dungeon toxins.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_blightward_jerkin_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Blightward Jerkin" (Treated jerkin resisting acid and dungeon toxins.): satisfying buckle strap and common-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_blightward_jerkin_mitigate.wav`):**
   > `Defensive impact deflection sound when "Blightward Jerkin" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_blightward_jerkin_equip.wav` |
| **Deflect** | `"armor_mitigate_blightward_jerkin"` | `"armor_mitigate_blightward_jerkin"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_blightward_jerkin_mitigate.wav` |
