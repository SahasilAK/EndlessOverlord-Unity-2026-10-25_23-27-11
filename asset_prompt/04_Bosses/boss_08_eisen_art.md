# Baron von Eisen, The Iron Bastion (`boss_eisen`) — Legendary Boss Art & Animation Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss #08 (`04_Bosses`)
> **Catalog ID:** `"boss_eisen"` (`LegendaryBossCatalog.cs`)
> **Base Archetype Lineage:** `InvaderArchetype.ShieldGuardian`
> **Lore & Combat Identity:** Immovable armored lord deflecting assaults with colossal adamantine plating.
> **Combat Stats:** HP `380` | Armor `28` | Speed `2.2` | Radius `1.0` | Range `2.0` | Damage `22` | Windup `0.7s` | Cooldown `2.0s`
> **Primary Ability:** `BossAbilityType.ColossalCleave` (Massive 180° crescent weapon wave (`64x48 px`))
> **Secondary Ability:** `BossAbilityType.SanctuaryWard` (Sacred golden dome barrier & floor sigil (`64x64 px`))
> **Sprite Sheet Spec:** `288x48 px` (6 frames of `48x48 px`), `32 PPU` (1.5×1.5 world units), Pivot `Bottom-Center (0.5, 0.08)`, `FilterMode.Point`

---

## 1. Base Design & Visual Prompt (`48x48 px`)

### 1.1 Color Palette & Boss Presence
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Sovereign Hue:** `#A6B2C7` (`LegendaryBossCatalog` RGB — dominates cape, crest, and weapon aura)
- **Radiant Highlight:** `#CED4E0` (Crown/helm crest, shoulder pauldrons, weapon edge)
- **Shadow Core:** `#535963` (Deep armor recesses and lower mantle)
- **Heroic Masterwork Trim:** `#FFE082` (Burnished champion gold filigree)

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Walk / Command Stance)
```text
Masterpiece 2D top-down 3/4 view pixel art boss sprite of Baron von Eisen, "The Iron Bastion" (ShieldGuardian champion): Immovable armored lord deflecting assaults with colossal adamantine plating.
Canvas: 48x48 pixels (1.5x larger than standard 32x32 units), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: imposing heroic commander filling a 40x44 px footprint, ornate crested helm/crown, oversized masterwork ShieldGuardian weapon glowing with #A6B2C7 energy, sweeping champion cape, unmistakable silhouette.
Palette: primary #A6B2C7, highlight #CED4E0, deep shadow #535963, champion gold #FFE082.
Foot Anchor: centered at X=24, Y=4 from bottom edge (Unity Pivot 0.5, 0.08).
```

---

## 2. Step-by-Step 6-Frame Boss Sprite Sheet Prompts (`boss_08_boss_eisen_sheet.png` — `288x48 px`)

- **Frame 01 (`0..47, 0..47`) — `Walk_Command`:**
  > `Consistent 48x48 pixel art of Baron von Eisen (The Iron Bastion). Striding forward with commanding presence, cape billowing, masterwork weapon poised, #A6B2C7 aura motes rising.`
- **Frame 02 (`48..95, 0..47`) — `Windup_Telegraph` (`0.7s`):**
  > `Consistent 48x48 pixel art of Baron von Eisen. High-contrast telegraph pose: weapon drawn high overhead/back, body coiled, bright #FFFFFF and #CED4E0 warning glint on weapon tip (8px star).`
- **Frame 03 (`96..143, 0..47`) — `Primary_Ability` (`ColossalCleave`):**
  > `Consistent 48x48 pixel art of Baron von Eisen. Executing ColossalCleave (massive 180° crescent weapon wave (`64x48 px`)): explosive forward release with #A6B2C7 and #FFFFFF energy arc wrapping 270° around body.`
- **Frame 04 (`144..191, 0..47`) — `Secondary_Ability` (`SanctuaryWard`):**
  > `Consistent 48x48 pixel art of Baron von Eisen. Executing SanctuaryWard (sacred golden dome barrier & floor sigil (`64x64 px`)): secondary stance with off-hand/weapon channeling #CED4E0 sigils.`
- **Frame 05 (`192..239, 0..47`) — `Enrage_State` (`BossModifier.Enrage`):**
  > `Consistent 48x48 pixel art of Baron von Eisen. Enraged berserk posture: eyes blazing pure #FFFFFF, jagged crimson-gold (#FF3300 / #CED4E0) flame crown erupting from shoulders, 2px outer aura ring.`
- **Frame 06 (`240..287, 0..47`) — `Death_Defeat`:**
  > `Consistent 48x48 pixel art of Baron von Eisen. Dropped to one knee then collapsed, masterwork weapon planted/shattered on dungeon stone, golden-violet champion soul ascending.`

---

## 3. Boss Ability & Modifier VFX Prompts

1. **Primary Ability VFX (`vfx_boss_eisen_colossalcleave.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Baron von Eisen's ColossalCleave: Massive 180° crescent weapon wave (`64x48 px`), colored in #A6B2C7 and #FFFFFF with #110E1B 1px outer contrast rim.`
2. **Secondary Ability VFX (`vfx_boss_eisen_sanctuaryward.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Baron von Eisen's SanctuaryWard: Sacred golden dome barrier & floor sigil (`64x64 px`), colored in #CED4E0 and #A6B2C7.`
3. **Boss Modifier Overlays (`BossModifier` shared 48x48 overlays):**
   - `Enrage`: Pulsing `#FF2A00` jaggedaura border.
   - `Barrier`: Translucent `#00E5FF` hexagonal dome (`CueIds.BossModifierBarrier`).
   - `Regeneration`: Ascending `#2ECC71` emerald plus-sparks.
   - `ElementalOvercharge`: Orbiting tri-elemental orbs (`#FF4500`, `#00E5FF`, `#FFD700`).

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Boss Sheet** | `Assets/_Game/Art/Characters/Bosses/boss_08_boss_eisen_sheet.png` | `288x48` (`6x 48x48`) | `32` | `(0.5, 0.08)` | `InvaderView` boss scale (`PlaceholderSprites.Circle` tinted `#A6B2C7`) |
| **Boss Portrait** | `Assets/_Game/Art/UI/BossPortraits/portrait_boss_eisen.png` | `48x48` | `16` | `(0.5, 0.5)` | `BossEncounterBannerPresenter` nameplate icon |
| **Primary VFX** | `Assets/_Game/Art/VFX/Bosses/vfx_boss_eisen_primary.png` | `192x48` (`4x 48x48`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` boss ring |
