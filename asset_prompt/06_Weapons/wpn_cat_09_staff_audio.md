# Arcane, Elemental & Necrotic Staves (`WeaponCategory.Staff`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #09/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Staff` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Resonant wood/bone staff thump + harmonic arcane discharge.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Resonant wood/bone staff thump + harmonic arcane discharge.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_staff_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_staff_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_staff_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Arcane, Elemental & Necrotic Staves: Resonant wood/bone staff thump + harmonic arcane discharge. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_staff_attack.wav`):**
   > `Punchy combat attack sound for Arcane, Elemental & Necrotic Staves: Resonant wood/bone staff thump + harmonic arcane discharge., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_staff_legendary_hit.wav`):**
   > `Mythic weapon impact for Staff of Creation (Primordial relic containing the genesis spark of worlds.): heavy Resonant wood/bone staff thump + harmonic arcane discharge. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_staff_equip.wav` |
| **Attack** | `"wpn_attack_staff"` | `"wpn_attack_staff"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_staff_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_staff"` | `"wpn_hit_legendary_staff"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_staff_legendary_hit.wav` |
