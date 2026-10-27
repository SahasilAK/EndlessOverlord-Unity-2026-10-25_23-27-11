# Dragon Bone Mail (`arm_dragon_bone_mail`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #04/30 (`07_Armors`)
> **Catalog ID:** `"arm_dragon_bone_mail"` (`ItemRarity.Rare`)
> **Lore Description:** *Interlocking scales harvested from ancient elder drakes.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Dragon Bone Mail* (Interlocking scales harvested from ancient elder drakes.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_dragon_bone_mail_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Dragon Bone Mail" (Interlocking scales harvested from ancient elder drakes.): satisfying buckle strap and rare-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_dragon_bone_mail_mitigate.wav`):**
   > `Defensive impact deflection sound when "Dragon Bone Mail" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_dragon_bone_mail_equip.wav` |
| **Deflect** | `"armor_mitigate_dragon_bone_mail"` | `"armor_mitigate_dragon_bone_mail"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_dragon_bone_mail_mitigate.wav` |
