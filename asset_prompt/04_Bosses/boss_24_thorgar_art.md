# Thorgar, The Rune Forger (`boss_thorgar`) — Legendary Boss Art & Animation Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss #24 (`04_Bosses`)
> **Catalog ID:** `"boss_thorgar"` (`LegendaryBossCatalog.cs`)
> **Base Archetype Lineage:** `InvaderArchetype.RuneKnight`
> **Lore & Combat Identity:** Dwarven runic artisan hammering ancient power glyphs into the stone floors.
> **Combat Stats:** HP `320` | Armor `22` | Speed `2.7` | Radius `0.9` | Range `2.4` | Damage `29` | Windup `0.6s` | Cooldown `1.8s`
> **Primary Ability:** `BossAbilityType.ThunderStrike` (Heaven-piercing lightning pillar impact (`32x64 px`))
> **Secondary Ability:** `BossAbilityType.ColossalCleave` (Massive 180° crescent weapon wave (`64x48 px`))
> **Sprite Sheet Spec:** `288x48 px` (6 frames of `48x48 px`), `32 PPU` (1.5×1.5 world units), Pivot `Bottom-Center (0.5, 0.08)`, `FilterMode.Point`

---

## 1. Base Design & Visual Prompt (`48x48 px`)

### 1.1 Color Palette & Boss Presence
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Sovereign Hue:** `#59CCD9` (`LegendaryBossCatalog` RGB — dominates cape, crest, and weapon aura)
- **Radiant Highlight:** `#A3E2EA` (Crown/helm crest, shoulder pauldrons, weapon edge)
- **Shadow Core:** `#2C666C` (Deep armor recesses and lower mantle)
- **Heroic Masterwork Trim:** `#FFE082` (Burnished champion gold filigree)

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Walk / Command Stance)
```text
Masterpiece 2D top-down 3/4 view pixel art boss sprite of Thorgar, "The Rune Forger" (RuneKnight champion): Dwarven runic artisan hammering ancient power glyphs into the stone floors.
Canvas: 48x48 pixels (1.5x larger than standard 32x32 units), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: imposing heroic commander filling a 40x44 px footprint, ornate crested helm/crown, oversized masterwork RuneKnight weapon glowing with #59CCD9 energy, sweeping champion cape, unmistakable silhouette.
Palette: primary #59CCD9, highlight #A3E2EA, deep shadow #2C666C, champion gold #FFE082.
Foot Anchor: centered at X=24, Y=4 from bottom edge (Unity Pivot 0.5, 0.08).
```

---

## 2. Step-by-Step 6-Frame Boss Sprite Sheet Prompts (`boss_24_boss_thorgar_sheet.png` — `288x48 px`)

- **Frame 01 (`0..47, 0..47`) — `Walk_Command`:**
  > `Consistent 48x48 pixel art of Thorgar (The Rune Forger). Striding forward with commanding presence, cape billowing, masterwork weapon poised, #59CCD9 aura motes rising.`
- **Frame 02 (`48..95, 0..47`) — `Windup_Telegraph` (`0.6s`):**
  > `Consistent 48x48 pixel art of Thorgar. High-contrast telegraph pose: weapon drawn high overhead/back, body coiled, bright #FFFFFF and #A3E2EA warning glint on weapon tip (8px star).`
- **Frame 03 (`96..143, 0..47`) — `Primary_Ability` (`ThunderStrike`):**
  > `Consistent 48x48 pixel art of Thorgar. Executing ThunderStrike (heaven-piercing lightning pillar impact (`32x64 px`)): explosive forward release with #59CCD9 and #FFFFFF energy arc wrapping 270° around body.`
- **Frame 04 (`144..191, 0..47`) — `Secondary_Ability` (`ColossalCleave`):**
  > `Consistent 48x48 pixel art of Thorgar. Executing ColossalCleave (massive 180° crescent weapon wave (`64x48 px`)): secondary stance with off-hand/weapon channeling #A3E2EA sigils.`
- **Frame 05 (`192..239, 0..47`) — `Enrage_State` (`BossModifier.Enrage`):**
  > `Consistent 48x48 pixel art of Thorgar. Enraged berserk posture: eyes blazing pure #FFFFFF, jagged crimson-gold (#FF3300 / #A3E2EA) flame crown erupting from shoulders, 2px outer aura ring.`
- **Frame 06 (`240..287, 0..47`) — `Death_Defeat`:**
  > `Consistent 48x48 pixel art of Thorgar. Dropped to one knee then collapsed, masterwork weapon planted/shattered on dungeon stone, golden-violet champion soul ascending.`

---

## 3. Boss Ability & Modifier VFX Prompts

1. **Primary Ability VFX (`vfx_boss_thorgar_thunderstrike.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Thorgar's ThunderStrike: Heaven-piercing lightning pillar impact (`32x64 px`), colored in #59CCD9 and #FFFFFF with #110E1B 1px outer contrast rim.`
2. **Secondary Ability VFX (`vfx_boss_thorgar_colossalcleave.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Thorgar's ColossalCleave: Massive 180° crescent weapon wave (`64x48 px`), colored in #A3E2EA and #59CCD9.`
3. **Boss Modifier Overlays (`BossModifier` shared 48x48 overlays):**
   - `Enrage`: Pulsing `#FF2A00` jaggedaura border.
   - `Barrier`: Translucent `#00E5FF` hexagonal dome (`CueIds.BossModifierBarrier`).
   - `Regeneration`: Ascending `#2ECC71` emerald plus-sparks.
   - `ElementalOvercharge`: Orbiting tri-elemental orbs (`#FF4500`, `#00E5FF`, `#FFD700`).

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Boss Sheet** | `Assets/_Game/Art/Characters/Bosses/boss_24_boss_thorgar_sheet.png` | `288x48` (`6x 48x48`) | `32` | `(0.5, 0.08)` | `InvaderView` boss scale (`PlaceholderSprites.Circle` tinted `#59CCD9`) |
| **Boss Portrait** | `Assets/_Game/Art/UI/BossPortraits/portrait_boss_thorgar.png` | `48x48` | `16` | `(0.5, 0.5)` | `BossEncounterBannerPresenter` nameplate icon |
| **Primary VFX** | `Assets/_Game/Art/VFX/Bosses/vfx_boss_thorgar_primary.png` | `192x48` (`4x 48x48`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` boss ring |
