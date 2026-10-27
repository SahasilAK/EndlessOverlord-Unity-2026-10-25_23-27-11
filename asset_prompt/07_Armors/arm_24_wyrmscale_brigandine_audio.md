# Wyrmscale Brigandine (`arm_wyrmscale_brigandine`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #24/30 (`07_Armors`)
> **Catalog ID:** `"arm_wyrmscale_brigandine"` (`ItemRarity.Rare`)
> **Lore Description:** *Supple leather lined with fire-drake scales.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Wyrmscale Brigandine* (Supple leather lined with fire-drake scales.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_wyrmscale_brigandine_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Wyrmscale Brigandine" (Supple leather lined with fire-drake scales.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_wyrmscale_brigandine_mitigate.wav`):**
   > `Defensive impact deflection sound when "Wyrmscale Brigandine" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_wyrmscale_brigandine_equip.wav` |
| **Deflect** | `"armor_mitigate_wyrmscale_brigandine"` | `"armor_mitigate_wyrmscale_brigandine"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_wyrmscale_brigandine_mitigate.wav` |
