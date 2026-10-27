# One-Handed Swords (`WeaponCategory.OneHandedSword`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #01/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.OneHandedSword` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Crisp high-carbon steel ring + fast air-cutting whoosh.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Crisp high-carbon steel ring + fast air-cutting whoosh.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_one_handed_sword_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_one_handed_sword_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_one_handed_sword_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for One-Handed Swords: Crisp high-carbon steel ring + fast air-cutting whoosh. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_one_handed_sword_attack.wav`):**
   > `Punchy combat attack sound for One-Handed Swords: Crisp high-carbon steel ring + fast air-cutting whoosh., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_one_handed_sword_legendary_hit.wav`):**
   > `Mythic weapon impact for Excalibur of Shadows (Legendary umbral sword crowned with void energy.): heavy Crisp high-carbon steel ring + fast air-cutting whoosh. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_one_handed_sword_equip.wav` |
| **Attack** | `"wpn_attack_one_handed_sword"` | `"wpn_attack_one_handed_sword"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_one_handed_sword_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_one_handed_sword"` | `"wpn_hit_legendary_one_handed_sword"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_one_handed_sword_legendary_hit.wav` |
