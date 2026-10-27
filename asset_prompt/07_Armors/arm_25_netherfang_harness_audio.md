# Netherfang Harness (`arm_netherfang_harness`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #25/30 (`07_Armors`)
> **Catalog ID:** `"arm_netherfang_harness"` (`ItemRarity.Uncommon`)
> **Lore Description:** *Brimstone-cured harness leaching life from attacker strikes.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Netherfang Harness* (Brimstone-cured harness leaching life from attacker strikes.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_netherfang_harness_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Netherfang Harness" (Brimstone-cured harness leaching life from attacker strikes.): satisfying buckle strap and uncommon-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_netherfang_harness_mitigate.wav`):**
   > `Defensive impact deflection sound when "Netherfang Harness" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_netherfang_harness_equip.wav` |
| **Deflect** | `"armor_mitigate_netherfang_harness"` | `"armor_mitigate_netherfang_harness"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_netherfang_harness_mitigate.wav` |
