# Battleaxes & Cleavers (`WeaponCategory.Axe`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #05/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Axe` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Brutal chopping steel transient + meaty bone-splitting crunch.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Brutal chopping steel transient + meaty bone-splitting crunch.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_axe_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_axe_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_axe_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Battleaxes & Cleavers: Brutal chopping steel transient + meaty bone-splitting crunch. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_axe_attack.wav`):**
   > `Punchy combat attack sound for Battleaxes & Cleavers: Brutal chopping steel transient + meaty bone-splitting crunch., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_axe_legendary_hit.wav`):**
   > `Mythic weapon impact for Ragnarok Cleaver (World-ending battleaxe sundering the sky.): heavy Brutal chopping steel transient + meaty bone-splitting crunch. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_axe_equip.wav` |
| **Attack** | `"wpn_attack_axe"` | `"wpn_attack_axe"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_axe_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_axe"` | `"wpn_hit_legendary_axe"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_axe_legendary_hit.wav` |
