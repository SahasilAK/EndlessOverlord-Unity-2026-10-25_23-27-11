# Bloodforged Carapace (`arm_bloodforged_carapace`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #09/30 (`07_Armors`)
> **Catalog ID:** `"arm_bloodforged_carapace"` (`ItemRarity.Rare`)
> **Lore Description:** *Living armor feeding on spilled blood to knit defensive plates.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Bloodforged Carapace* (Living armor feeding on spilled blood to knit defensive plates.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_bloodforged_carapace_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Bloodforged Carapace" (Living armor feeding on spilled blood to knit defensive plates.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_bloodforged_carapace_mitigate.wav`):**
   > `Defensive impact deflection sound when "Bloodforged Carapace" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_bloodforged_carapace_equip.wav` |
| **Deflect** | `"armor_mitigate_bloodforged_carapace"` | `"armor_mitigate_bloodforged_carapace"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_bloodforged_carapace_mitigate.wav` |
