# Paired Twin-Blades & Chakrams (`WeaponCategory.DualBlades`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #13/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.DualBlades` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Double-hit rapid steel flurry + whistling spinning blade slice.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Double-hit rapid steel flurry + whistling spinning blade slice.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_dual_blades_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_dual_blades_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_dual_blades_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Paired Twin-Blades & Chakrams: Double-hit rapid steel flurry + whistling spinning blade slice. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_dual_blades_attack.wav`):**
   > `Punchy combat attack sound for Paired Twin-Blades & Chakrams: Double-hit rapid steel flurry + whistling spinning blade slice., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_dual_blades_legendary_hit.wav`):**
   > `Mythic weapon impact for Paired Twin-Blades & Chakrams (): heavy Double-hit rapid steel flurry + whistling spinning blade slice. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_dual_blades_equip.wav` |
| **Attack** | `"wpn_attack_dual_blades"` | `"wpn_attack_dual_blades"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_dual_blades_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_dual_blades"` | `"wpn_hit_legendary_dual_blades"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_dual_blades_legendary_hit.wav` |
