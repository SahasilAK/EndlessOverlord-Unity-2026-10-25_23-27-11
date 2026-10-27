# Darius, The Voidstalker (`boss_darius`) — Legendary Boss Art & Animation Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss #21 (`04_Bosses`)
> **Catalog ID:** `"boss_darius"` (`LegendaryBossCatalog.cs`)
> **Base Archetype Lineage:** `InvaderArchetype.ShadowDuelist`
> **Lore & Combat Identity:** Nether-touched stalker emerging from pitch-black rift tears to execute precision cuts.
> **Combat Stats:** HP `250` | Armor `12` | Speed `3.9` | Radius `0.72` | Range `1.8` | Damage `29` | Windup `0.32s` | Cooldown `1.15s`
> **Primary Ability:** `BossAbilityType.PhantomBlinkStrike` (Dimensional shadow-step vanish & cross-slash execution (`48x48 px`))
> **Secondary Ability:** `BossAbilityType.ShockwaveSlam` (Tectonic ground-shattering slam ring (`64x64 px`))
> **Sprite Sheet Spec:** `288x48 px` (6 frames of `48x48 px`), `32 PPU` (1.5×1.5 world units), Pivot `Bottom-Center (0.5, 0.08)`, `FilterMode.Point`

---

## 1. Base Design & Visual Prompt (`48x48 px`)

### 1.1 Color Palette & Boss Presence
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Sovereign Hue:** `#331A59` (`LegendaryBossCatalog` RGB — dominates cape, crest, and weapon aura)
- **Radiant Highlight:** `#8E81A3` (Crown/helm crest, shoulder pauldrons, weapon edge)
- **Shadow Core:** `#190D2C` (Deep armor recesses and lower mantle)
- **Heroic Masterwork Trim:** `#FFE082` (Burnished champion gold filigree)

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Walk / Command Stance)
```text
Masterpiece 2D top-down 3/4 view pixel art boss sprite of Darius, "The Voidstalker" (ShadowDuelist champion): Nether-touched stalker emerging from pitch-black rift tears to execute precision cuts.
Canvas: 48x48 pixels (1.5x larger than standard 32x32 units), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: imposing heroic commander filling a 40x44 px footprint, ornate crested helm/crown, oversized masterwork ShadowDuelist weapon glowing with #331A59 energy, sweeping champion cape, unmistakable silhouette.
Palette: primary #331A59, highlight #8E81A3, deep shadow #190D2C, champion gold #FFE082.
Foot Anchor: centered at X=24, Y=4 from bottom edge (Unity Pivot 0.5, 0.08).
```

---

## 2. Step-by-Step 6-Frame Boss Sprite Sheet Prompts (`boss_21_boss_darius_sheet.png` — `288x48 px`)

- **Frame 01 (`0..47, 0..47`) — `Walk_Command`:**
  > `Consistent 48x48 pixel art of Darius (The Voidstalker). Striding forward with commanding presence, cape billowing, masterwork weapon poised, #331A59 aura motes rising.`
- **Frame 02 (`48..95, 0..47`) — `Windup_Telegraph` (`0.32s`):**
  > `Consistent 48x48 pixel art of Darius. High-contrast telegraph pose: weapon drawn high overhead/back, body coiled, bright #FFFFFF and #8E81A3 warning glint on weapon tip (8px star).`
- **Frame 03 (`96..143, 0..47`) — `Primary_Ability` (`PhantomBlinkStrike`):**
  > `Consistent 48x48 pixel art of Darius. Executing PhantomBlinkStrike (dimensional shadow-step vanish & cross-slash execution (`48x48 px`)): explosive forward release with #331A59 and #FFFFFF energy arc wrapping 270° around body.`
- **Frame 04 (`144..191, 0..47`) — `Secondary_Ability` (`ShockwaveSlam`):**
  > `Consistent 48x48 pixel art of Darius. Executing ShockwaveSlam (tectonic ground-shattering slam ring (`64x64 px`)): secondary stance with off-hand/weapon channeling #8E81A3 sigils.`
- **Frame 05 (`192..239, 0..47`) — `Enrage_State` (`BossModifier.Enrage`):**
  > `Consistent 48x48 pixel art of Darius. Enraged berserk posture: eyes blazing pure #FFFFFF, jagged crimson-gold (#FF3300 / #8E81A3) flame crown erupting from shoulders, 2px outer aura ring.`
- **Frame 06 (`240..287, 0..47`) — `Death_Defeat`:**
  > `Consistent 48x48 pixel art of Darius. Dropped to one knee then collapsed, masterwork weapon planted/shattered on dungeon stone, golden-violet champion soul ascending.`

---

## 3. Boss Ability & Modifier VFX Prompts

1. **Primary Ability VFX (`vfx_boss_darius_phantomblinkstrike.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Darius's PhantomBlinkStrike: Dimensional shadow-step vanish & cross-slash execution (`48x48 px`), colored in #331A59 and #FFFFFF with #110E1B 1px outer contrast rim.`
2. **Secondary Ability VFX (`vfx_boss_darius_shockwaveslam.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Darius's ShockwaveSlam: Tectonic ground-shattering slam ring (`64x64 px`), colored in #8E81A3 and #331A59.`
3. **Boss Modifier Overlays (`BossModifier` shared 48x48 overlays):**
   - `Enrage`: Pulsing `#FF2A00` jaggedaura border.
   - `Barrier`: Translucent `#00E5FF` hexagonal dome (`CueIds.BossModifierBarrier`).
   - `Regeneration`: Ascending `#2ECC71` emerald plus-sparks.
   - `ElementalOvercharge`: Orbiting tri-elemental orbs (`#FF4500`, `#00E5FF`, `#FFD700`).

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Boss Sheet** | `Assets/_Game/Art/Characters/Bosses/boss_21_boss_darius_sheet.png` | `288x48` (`6x 48x48`) | `32` | `(0.5, 0.08)` | `InvaderView` boss scale (`PlaceholderSprites.Circle` tinted `#331A59`) |
| **Boss Portrait** | `Assets/_Game/Art/UI/BossPortraits/portrait_boss_darius.png` | `48x48` | `16` | `(0.5, 0.5)` | `BossEncounterBannerPresenter` nameplate icon |
| **Primary VFX** | `Assets/_Game/Art/VFX/Bosses/vfx_boss_darius_primary.png` | `192x48` (`4x 48x48`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` boss ring |
