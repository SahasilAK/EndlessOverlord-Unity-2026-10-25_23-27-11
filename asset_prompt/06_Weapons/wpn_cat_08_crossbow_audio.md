# Crossbows, Repeaters & Ballistas (`WeaponCategory.Crossbow`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #08/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Crossbow` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Mechanical steel ratchet lock + high-tension quarrel snap.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Mechanical steel ratchet lock + high-tension quarrel snap.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_crossbow_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_crossbow_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_crossbow_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Crossbows, Repeaters & Ballistas: Mechanical steel ratchet lock + high-tension quarrel snap. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_crossbow_attack.wav`):**
   > `Punchy combat attack sound for Crossbows, Repeaters & Ballistas: Mechanical steel ratchet lock + high-tension quarrel snap., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_crossbow_legendary_hit.wav`):**
   > `Mythic weapon impact for Apocalypse Repeater (Gatling mechanism cycling soul-charged bolts.): heavy Mechanical steel ratchet lock + high-tension quarrel snap. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_crossbow_equip.wav` |
| **Attack** | `"wpn_attack_crossbow"` | `"wpn_attack_crossbow"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_crossbow_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_crossbow"` | `"wpn_hit_legendary_crossbow"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_crossbow_legendary_hit.wav` |
