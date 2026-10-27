# Feral Gauntlets & Talons (`WeaponCategory.Claws`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #14/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Claws` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Vicious triple-claw flesh rip + feral metallic scrape.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Vicious triple-claw flesh rip + feral metallic scrape.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_claws_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_claws_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_claws_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Feral Gauntlets & Talons: Vicious triple-claw flesh rip + feral metallic scrape. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_claws_attack.wav`):**
   > `Punchy combat attack sound for Feral Gauntlets & Talons: Vicious triple-claw flesh rip + feral metallic scrape., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_claws_legendary_hit.wav`):**
   > `Mythic weapon impact for Claws of the Beast God (Primal divine wrath condensed into rending claws.): heavy Vicious triple-claw flesh rip + feral metallic scrape. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_claws_equip.wav` |
| **Attack** | `"wpn_attack_claws"` | `"wpn_attack_claws"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_claws_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_claws"` | `"wpn_hit_legendary_claws"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_claws_legendary_hit.wav` |
