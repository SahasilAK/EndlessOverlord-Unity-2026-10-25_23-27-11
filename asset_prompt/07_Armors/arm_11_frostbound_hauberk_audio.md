# Frostbound Hauberk (`arm_frostbound_hauberk`) — Armor Audio & SFX Prompt Specification

> **Asset Category:** Equipment Armor Audio #11/30 (`07_Armors`)
> **Catalog ID:** `"arm_frostbound_hauberk"` (`ItemRarity.Uncommon`)
> **Lore Description:** *Rings of frozen steel numbing enemy impacts.*

---

## 1. Base Sound Identity
- **Material Foley:** Tailored to *Frostbound Hauberk* (Rings of frozen steel numbing enemy impacts.) — heavy metallic plate clank, supple drake-scale rustle, or resonant arcane cloth hum.

---

## 2. Step-by-Step Audio Variations

1. **Armor Equip (`sfx_arm_frostbound_hauberk_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Dark fantasy inventory armor equip sound for "Frostbound Hauberk" (Rings of frozen steel numbing enemy impacts.): satisfying buckle strap and uncommon-tier ward lock-in chime, 0.30s, mono 44.1kHz WAV.`
2. **Armor Damage Mitigation Hit (`sfx_arm_frostbound_hauberk_mitigate.wav`):**
   > `Defensive impact deflection sound when "Frostbound Hauberk" absorbs an invader strike, 0.20s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Event | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_frostbound_hauberk_equip.wav` |
| **Deflect** | `"armor_mitigate_frostbound_hauberk"` | `"armor_mitigate_frostbound_hauberk"` | `Assets/_Game/Audio/SFX/Equipment/Armors/sfx_arm_frostbound_hauberk_mitigate.wav` |
