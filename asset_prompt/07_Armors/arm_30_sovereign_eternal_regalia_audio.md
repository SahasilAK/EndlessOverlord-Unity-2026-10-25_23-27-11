# Sovereign Eternal Regalia (`arm_sovereign_eternal_regalia`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #30/30 (`07_Armors`)
> **Catalog ID:** `"arm_sovereign_eternal_regalia"` (`ItemRarity.Legendary`)
> **Lore Description:** *The imperial battle-regalia of the undying dungeon lord.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Sovereign Eternal Regalia* (The imperial battle-regalia of the undying dungeon lord.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_sovereign_eternal_regalia_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Sovereign Eternal Regalia" (The imperial battle-regalia of the undying dungeon lord.): satisfying buckle strap and legendary-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_sovereign_eternal_regalia_mitigate.wav`):**
   > `Defensive impact deflection sound when "Sovereign Eternal Regalia" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_sovereign_eternal_regalia_equip.wav` |
| **Deflect** | `"armor_mitigate_sovereign_eternal_regalia"` | `"armor_mitigate_sovereign_eternal_regalia"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_sovereign_eternal_regalia_mitigate.wav` |
