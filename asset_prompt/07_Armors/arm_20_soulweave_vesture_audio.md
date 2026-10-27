# Soulweave Vesture (`arm_soulweave_vesture`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #20/30 (`07_Armors`)
> **Catalog ID:** `"arm_soulweave_vesture"` (`ItemRarity.Uncommon`)
> **Lore Description:** *Pliant robes woven with spirit threads humming softly.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Soulweave Vesture* (Pliant robes woven with spirit threads humming softly.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_soulweave_vesture_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Soulweave Vesture" (Pliant robes woven with spirit threads humming softly.): satisfying buckle strap and uncommon-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_soulweave_vesture_mitigate.wav`):**
   > `Defensive impact deflection sound when "Soulweave Vesture" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_soulweave_vesture_equip.wav` |
| **Deflect** | `"armor_mitigate_soulweave_vesture"` | `"armor_mitigate_soulweave_vesture"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_soulweave_vesture_mitigate.wav` |
