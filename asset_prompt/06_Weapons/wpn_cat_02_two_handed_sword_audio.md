# Two-Handed Greatswords (`WeaponCategory.TwoHandedSword`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #02/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.TwoHandedSword` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Heavy low-pitched iron broad-cleave + ground-shaking impact thud.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Heavy low-pitched iron broad-cleave + ground-shaking impact thud.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_two_handed_sword_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_two_handed_sword_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_two_handed_sword_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Two-Handed Greatswords: Heavy low-pitched iron broad-cleave + ground-shaking impact thud. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_two_handed_sword_attack.wav`):**
   > `Punchy combat attack sound for Two-Handed Greatswords: Heavy low-pitched iron broad-cleave + ground-shaking impact thud., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_two_handed_sword_legendary_hit.wav`):**
   > `Mythic weapon impact for World Breaker Blade (Mythic god-slaying greatsword shattering bedrock.): heavy Heavy low-pitched iron broad-cleave + ground-shaking impact thud. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_two_handed_sword_equip.wav` |
| **Attack** | `"wpn_attack_two_handed_sword"` | `"wpn_attack_two_handed_sword"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_two_handed_sword_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_two_handed_sword"` | `"wpn_hit_legendary_two_handed_sword"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_two_handed_sword_legendary_hit.wav` |
