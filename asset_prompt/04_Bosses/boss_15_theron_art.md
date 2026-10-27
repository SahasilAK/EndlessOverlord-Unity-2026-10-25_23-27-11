# Theron, The Dragonbane Dragoon (`boss_theron`) — Legendary Boss Art & Animation Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss #15 (`04_Bosses`)
> **Catalog ID:** `"boss_theron"` (`LegendaryBossCatalog.cs`)
> **Base Archetype Lineage:** `InvaderArchetype.Spearman`
> **Lore & Combat Identity:** Sky-leaping dragoon plunging wyrmtooth pikes straight through defensive wards.
> **Combat Stats:** HP `290` | Armor `18` | Speed `3.2` | Radius `0.82` | Range `3.2` | Damage `28` | Windup `0.5s` | Cooldown `1.5s`
> **Primary Ability:** `BossAbilityType.WhirlwindCharge` (360° spinning blade tornado rush (`48x48 px`))
> **Secondary Ability:** `BossAbilityType.ShockwaveSlam` (Tectonic ground-shattering slam ring (`64x64 px`))
> **Sprite Sheet Spec:** `288x48 px` (6 frames of `48x48 px`), `32 PPU` (1.5×1.5 world units), Pivot `Bottom-Center (0.5, 0.08)`, `FilterMode.Point`

---

## 1. Base Design & Visual Prompt (`48x48 px`)

### 1.1 Color Palette & Boss Presence
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Sovereign Hue:** `#E68033` (`LegendaryBossCatalog` RGB — dominates cape, crest, and weapon aura)
- **Radiant Highlight:** `#F1B98E` (Crown/helm crest, shoulder pauldrons, weapon edge)
- **Shadow Core:** `#734019` (Deep armor recesses and lower mantle)
- **Heroic Masterwork Trim:** `#FFE082` (Burnished champion gold filigree)

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Walk / Command Stance)
```text
Masterpiece 2D top-down 3/4 view pixel art boss sprite of Theron, "The Dragonbane Dragoon" (Spearman champion): Sky-leaping dragoon plunging wyrmtooth pikes straight through defensive wards.
Canvas: 48x48 pixels (1.5x larger than standard 32x32 units), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: imposing heroic commander filling a 40x44 px footprint, ornate crested helm/crown, oversized masterwork Spearman weapon glowing with #E68033 energy, sweeping champion cape, unmistakable silhouette.
Palette: primary #E68033, highlight #F1B98E, deep shadow #734019, champion gold #FFE082.
Foot Anchor: centered at X=24, Y=4 from bottom edge (Unity Pivot 0.5, 0.08).
```

---

## 2. Step-by-Step 6-Frame Boss Sprite Sheet Prompts (`boss_15_boss_theron_sheet.png` — `288x48 px`)

- **Frame 01 (`0..47, 0..47`) — `Walk_Command`:**
  > `Consistent 48x48 pixel art of Theron (The Dragonbane Dragoon). Striding forward with commanding presence, cape billowing, masterwork weapon poised, #E68033 aura motes rising.`
- **Frame 02 (`48..95, 0..47`) — `Windup_Telegraph` (`0.5s`):**
  > `Consistent 48x48 pixel art of Theron. High-contrast telegraph pose: weapon drawn high overhead/back, body coiled, bright #FFFFFF and #F1B98E warning glint on weapon tip (8px star).`
- **Frame 03 (`96..143, 0..47`) — `Primary_Ability` (`WhirlwindCharge`):**
  > `Consistent 48x48 pixel art of Theron. Executing WhirlwindCharge (360° spinning blade tornado rush (`48x48 px`)): explosive forward release with #E68033 and #FFFFFF energy arc wrapping 270° around body.`
- **Frame 04 (`144..191, 0..47`) — `Secondary_Ability` (`ShockwaveSlam`):**
  > `Consistent 48x48 pixel art of Theron. Executing ShockwaveSlam (tectonic ground-shattering slam ring (`64x64 px`)): secondary stance with off-hand/weapon channeling #F1B98E sigils.`
- **Frame 05 (`192..239, 0..47`) — `Enrage_State` (`BossModifier.Enrage`):**
  > `Consistent 48x48 pixel art of Theron. Enraged berserk posture: eyes blazing pure #FFFFFF, jagged crimson-gold (#FF3300 / #F1B98E) flame crown erupting from shoulders, 2px outer aura ring.`
- **Frame 06 (`240..287, 0..47`) — `Death_Defeat`:**
  > `Consistent 48x48 pixel art of Theron. Dropped to one knee then collapsed, masterwork weapon planted/shattered on dungeon stone, golden-violet champion soul ascending.`

---

## 3. Boss Ability & Modifier VFX Prompts

1. **Primary Ability VFX (`vfx_boss_theron_whirlwindcharge.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Theron's WhirlwindCharge: 360° spinning blade tornado rush (`48x48 px`), colored in #E68033 and #FFFFFF with #110E1B 1px outer contrast rim.`
2. **Secondary Ability VFX (`vfx_boss_theron_shockwaveslam.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Theron's ShockwaveSlam: Tectonic ground-shattering slam ring (`64x64 px`), colored in #F1B98E and #E68033.`
3. **Boss Modifier Overlays (`BossModifier` shared 48x48 overlays):**
   - `Enrage`: Pulsing `#FF2A00` jaggedaura border.
   - `Barrier`: Translucent `#00E5FF` hexagonal dome (`CueIds.BossModifierBarrier`).
   - `Regeneration`: Ascending `#2ECC71` emerald plus-sparks.
   - `ElementalOvercharge`: Orbiting tri-elemental orbs (`#FF4500`, `#00E5FF`, `#FFD700`).

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Boss Sheet** | `Assets/_Game/Art/Characters/Bosses/boss_15_boss_theron_sheet.png` | `288x48` (`6x 48x48`) | `32` | `(0.5, 0.08)` | `InvaderView` boss scale (`PlaceholderSprites.Circle` tinted `#E68033`) |
| **Boss Portrait** | `Assets/_Game/Art/UI/BossPortraits/portrait_boss_theron.png` | `48x48` | `16` | `(0.5, 0.5)` | `BossEncounterBannerPresenter` nameplate icon |
| **Primary VFX** | `Assets/_Game/Art/VFX/Bosses/vfx_boss_theron_primary.png` | `192x48` (`4x 48x48`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` boss ring |
