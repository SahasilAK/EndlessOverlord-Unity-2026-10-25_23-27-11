# Sunforged Radiant Mail (`arm_sunforged_radiant_mail`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #14/30 (`07_Armors`)
> **Catalog ID:** `"arm_sunforged_radiant_mail"` (`ItemRarity.Rare`)
> **Lore Description:** *Gleams with dawn light that disorients attackers.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Sunforged Radiant Mail* (Gleams with dawn light that disorients attackers.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_sunforged_radiant_mail_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Sunforged Radiant Mail" (Gleams with dawn light that disorients attackers.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_sunforged_radiant_mail_mitigate.wav`):**
   > `Defensive impact deflection sound when "Sunforged Radiant Mail" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_sunforged_radiant_mail_equip.wav` |
| **Deflect** | `"armor_mitigate_sunforged_radiant_mail"` | `"armor_mitigate_sunforged_radiant_mail"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_sunforged_radiant_mail_mitigate.wav` |
