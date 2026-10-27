# Ancient Cursed Relics & Orbs (`WeaponCategory.RelicWeapon`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #15/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.RelicWeapon` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Otherworldly singing crystal resonance + deep abyssal pulse.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Otherworldly singing crystal resonance + deep abyssal pulse.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_relic_weapon_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_relic_weapon_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_relic_weapon_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Ancient Cursed Relics & Orbs: Otherworldly singing crystal resonance + deep abyssal pulse. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_relic_weapon_attack.wav`):**
   > `Punchy combat attack sound for Ancient Cursed Relics & Orbs: Otherworldly singing crystal resonance + deep abyssal pulse., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_relic_weapon_legendary_hit.wav`):**
   > `Mythic weapon impact for Ancient Cursed Relics & Orbs (): heavy Otherworldly singing crystal resonance + deep abyssal pulse. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_relic_weapon_equip.wav` |
| **Attack** | `"wpn_attack_relic_weapon"` | `"wpn_attack_relic_weapon"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_relic_weapon_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_relic_weapon"` | `"wpn_hit_legendary_relic_weapon"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_relic_weapon_legendary_hit.wav` |
