# Spears, Halberds & Lances (`WeaponCategory.Spear`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #04/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Spear` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Long polearm shaft whoosh + armor-piercing metallic punch.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Long polearm shaft whoosh + armor-piercing metallic punch.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_spear_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_spear_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_spear_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Spears, Halberds & Lances: Long polearm shaft whoosh + armor-piercing metallic punch. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_spear_attack.wav`):**
   > `Punchy combat attack sound for Spears, Halberds & Lances: Long polearm shaft whoosh + armor-piercing metallic punch., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_spear_legendary_hit.wav`):**
   > `Mythic weapon impact for Dragoon God Lance (Celestial dragonslayer spear leaping through thunderheads.): heavy Long polearm shaft whoosh + armor-piercing metallic punch. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_spear_equip.wav` |
| **Attack** | `"wpn_attack_spear"` | `"wpn_attack_spear"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_spear_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_spear"` | `"wpn_hit_legendary_spear"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_spear_legendary_hit.wav` |
