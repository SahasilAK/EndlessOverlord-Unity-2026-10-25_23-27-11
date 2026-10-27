# Grimoires, Tomes & Codices (`WeaponCategory.Spellbook`) — Audio & SFX Prompt Specification

> **Asset Category:** Equipment Weapon Audio #12/15 (`06_Weapons`)
> **Catalog Enum:** `WeaponCategory.Spellbook` (`WeaponCatalog.cs` — 10 Weapons)
> **Acoustic Profile:** Fluttering parchment page turn + deep forbidden incantation boom.

---

## 1. Base Sound Identity
- **Category Foley & Synthesis:** Fluttering parchment page turn + deep forbidden incantation boom.
- **Rarity Scaling:** Common/Uncommon tiers use pure physical foley (`sfx_wpn_spellbook_swing_physical.wav`), while Rare/Epic/Legendary tiers layer an arcane harmonic tail (`sfx_wpn_spellbook_swing_enchanted.wav`).

---

## 2. Step-by-Step Audio Variations

1. **Equip / Unequip UI Foley (`sfx_wpn_spellbook_equip.wav` — `CueIds.ItemEquipped` / `"item_equipped"`):**
   > `Crisp dark-fantasy inventory equip sound for Grimoires, Tomes & Codices: Fluttering parchment page turn + deep forbidden incantation boom. metallic/wooden lock-in click, 0.25s, mono 44.1kHz WAV.`
2. **Standard Attack Swing / Release (`sfx_wpn_spellbook_attack.wav`):**
   > `Punchy combat attack sound for Grimoires, Tomes & Codices: Fluttering parchment page turn + deep forbidden incantation boom., fast 8ms transient, 0.30s duration, mono 44.1kHz WAV.`
3. **Legendary / Enchanted Strike (`sfx_wpn_spellbook_legendary_hit.wav`):**
   > `Mythic weapon impact for Necronomicon Original (The blasphemous tome of eternal dread and oblivion.): heavy Fluttering parchment page turn + deep forbidden incantation boom. layered with resonant sub-bass drop and arcane crystalline shimmer, 0.50s, mono 44.1kHz WAV.`

---

## 3. Unity `CueBus` Integration Table

| Action | `CueIds` Constant | Cue String | Target `.wav` Path |
| :--- | :--- | :--- | :--- |
| **Equip** | `CueIds.ItemEquipped` | `"item_equipped"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_spellbook_equip.wav` |
| **Attack** | `"wpn_attack_spellbook"` | `"wpn_attack_spellbook"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_spellbook_attack.wav` |
| **Legendary Impact** | `"wpn_hit_legendary_spellbook"` | `"wpn_hit_legendary_spellbook"` | `Assets/_Game/Audio/SFX/Equipment/Weapons/sfx_wpn_spellbook_legendary_hit.wav` |
