# Scepters & Spell Wands (`WeaponCategory.Wand`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #10/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Wand` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** High-pitched crystalline chime + rapid arcane spark zip.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** High-pitched crystalline chime + rapid arcane spark zip.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_wand_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_wand_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_wand_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Scepters & Spell Wands: High-pitched crystalline chime + rapid arcane spark zip. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_wand_attack.wav`):**
   > `Punchy combat attack sound for Scepters & Spell Wands: High-pitched crystalline chime + rapid arcane spark zip., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_wand_legendary_hit.wav`):**
   > `Mythic weapon impact for Wand of Endless Void (Infinite nullity streams from its obsidian tip.): heavy High-pitched crystalline chime + rapid arcane spark zip. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_wand_equip.wav` |
| **Attack** | `"wpn_attack_wand"` | `"wpn_attack_wand"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_wand_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_wand"` | `"wpn_hit_legendary_wand"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_wand_legendary_hit.wav` |
