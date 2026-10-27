# Spectral Cuirass (`arm_spectral_cuirass`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #08/30 (`07_Armors`)
> **Catalog ID:** `"arm_spectral_cuirass"` (`ItemRarity.Rare`)
> **Lore Description:** *Semi-tangible armor that deflects kinetic blows into the ether.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Spectral Cuirass* (Semi-tangible armor that deflects kinetic blows into the ether.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_spectral_cuirass_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Spectral Cuirass" (Semi-tangible armor that deflects kinetic blows into the ether.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_spectral_cuirass_mitigate.wav`):**
   > `Defensive impact deflection sound when "Spectral Cuirass" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_spectral_cuirass_equip.wav` |
| **Deflect** | `"armor_mitigate_spectral_cuirass"` | `"armor_mitigate_spectral_cuirass"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_spectral_cuirass_mitigate.wav` |
