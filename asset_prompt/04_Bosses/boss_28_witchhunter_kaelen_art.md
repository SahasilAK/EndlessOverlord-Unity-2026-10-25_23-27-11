# Kaelen, The Bloodhound (`boss_witchhunter_kaelen`) — Legendary Boss Art & Animation Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss #28 (`04_Bosses`)
> **Catalog ID:** `"boss_witchhunter_kaelen"` (`LegendaryBossCatalog.cs`)
> **Base Archetype Lineage:** `InvaderArchetype.WitchHunter`
> **Lore & Combat Identity:** Trackdown specialist relentlessly hunting dungeon horrors with incendiary shotguns.
> **Combat Stats:** HP `270` | Armor `15` | Speed `3.1` | Radius `0.78` | Range `6.8` | Damage `27` | Windup `0.5s` | Cooldown `1.55s`
> **Primary Ability:** `BossAbilityType.ThunderStrike` (Heaven-piercing lightning pillar impact (`32x64 px`))
> **Secondary Ability:** `BossAbilityType.PhantomBlinkStrike` (Dimensional shadow-step vanish & cross-slash execution (`48x48 px`))
> **Sprite Sheet Spec:** `288x48 px` (6 frames of `48x48 px`), `32 PPU` (1.5×1.5 world units), Pivot `Bottom-Center (0.5, 0.08)`, `FilterMode.Point`

---

## 1. Base Design & Visual Prompt (`48x48 px`)

### 1.1 Color Palette & Boss Presence
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Sovereign Hue:** `#A63333` (`LegendaryBossCatalog` RGB — dominates cape, crest, and weapon aura)
- **Radiant Highlight:** `#CE8E8E` (Crown/helm crest, shoulder pauldrons, weapon edge)
- **Shadow Core:** `#531919` (Deep armor recesses and lower mantle)
- **Heroic Masterwork Trim:** `#FFE082` (Burnished champion gold filigree)

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Walk / Command Stance)
```text
Masterpiece 2D top-down 3/4 view pixel art boss sprite of Kaelen, "The Bloodhound" (WitchHunter champion): Trackdown specialist relentlessly hunting dungeon horrors with incendiary shotguns.
Canvas: 48x48 pixels (1.5x larger than standard 32x32 units), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: imposing heroic commander filling a 40x44 px footprint, ornate crested helm/crown, oversized masterwork WitchHunter weapon glowing with #A63333 energy, sweeping champion cape, unmistakable silhouette.
Palette: primary #A63333, highlight #CE8E8E, deep shadow #531919, champion gold #FFE082.
Foot Anchor: centered at X=24, Y=4 from bottom edge (Unity Pivot 0.5, 0.08).
```

---

## 2. Step-by-Step 6-Frame Boss Sprite Sheet Prompts (`boss_28_boss_witchhunter_kaelen_sheet.png` — `288x48 px`)

- **Frame 01 (`0..47, 0..47`) — `Walk_Command`:**
  > `Consistent 48x48 pixel art of Kaelen (The Bloodhound). Striding forward with commanding presence, cape billowing, masterwork weapon poised, #A63333 aura motes rising.`
- **Frame 02 (`48..95, 0..47`) — `Windup_Telegraph` (`0.5s`):**
  > `Consistent 48x48 pixel art of Kaelen. High-contrast telegraph pose: weapon drawn high overhead/back, body coiled, bright #FFFFFF and #CE8E8E warning glint on weapon tip (8px star).`
- **Frame 03 (`96..143, 0..47`) — `Primary_Ability` (`ThunderStrike`):**
  > `Consistent 48x48 pixel art of Kaelen. Executing ThunderStrike (heaven-piercing lightning pillar impact (`32x64 px`)): explosive forward release with #A63333 and #FFFFFF energy arc wrapping 270° around body.`
- **Frame 04 (`144..191, 0..47`) — `Secondary_Ability` (`PhantomBlinkStrike`):**
  > `Consistent 48x48 pixel art of Kaelen. Executing PhantomBlinkStrike (dimensional shadow-step vanish & cross-slash execution (`48x48 px`)): secondary stance with off-hand/weapon channeling #CE8E8E sigils.`
- **Frame 05 (`192..239, 0..47`) — `Enrage_State` (`BossModifier.Enrage`):**
  > `Consistent 48x48 pixel art of Kaelen. Enraged berserk posture: eyes blazing pure #FFFFFF, jagged crimson-gold (#FF3300 / #CE8E8E) flame crown erupting from shoulders, 2px outer aura ring.`
- **Frame 06 (`240..287, 0..47`) — `Death_Defeat`:**
  > `Consistent 48x48 pixel art of Kaelen. Dropped to one knee then collapsed, masterwork weapon planted/shattered on dungeon stone, golden-violet champion soul ascending.`

---

## 3. Boss Ability & Modifier VFX Prompts

1. **Primary Ability VFX (`vfx_boss_witchhunter_kaelen_thunderstrike.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Kaelen's ThunderStrike: Heaven-piercing lightning pillar impact (`32x64 px`), colored in #A63333 and #FFFFFF with #110E1B 1px outer contrast rim.`
2. **Secondary Ability VFX (`vfx_boss_witchhunter_kaelen_phantomblinkstrike.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Kaelen's PhantomBlinkStrike: Dimensional shadow-step vanish & cross-slash execution (`48x48 px`), colored in #CE8E8E and #A63333.`
3. **Boss Modifier Overlays (`BossModifier` shared 48x48 overlays):**
   - `Enrage`: Pulsing `#FF2A00` jaggedaura border.
   - `Barrier`: Translucent `#00E5FF` hexagonal dome (`CueIds.BossModifierBarrier`).
   - `Regeneration`: Ascending `#2ECC71` emerald plus-sparks.
   - `ElementalOvercharge`: Orbiting tri-elemental orbs (`#FF4500`, `#00E5FF`, `#FFD700`).

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Boss Sheet** | `Assets/_Game/Art/Characters/Bosses/boss_28_boss_witchhunter_kaelen_sheet.png` | `288x48` (`6x 48x48`) | `32` | `(0.5, 0.08)` | `InvaderView` boss scale (`PlaceholderSprites.Circle` tinted `#A63333`) |
| **Boss Portrait** | `Assets/_Game/Art/UI/BossPortraits/portrait_boss_witchhunter_kaelen.png` | `48x48` | `16` | `(0.5, 0.5)` | `BossEncounterBannerPresenter` nameplate icon |
| **Primary VFX** | `Assets/_Game/Art/VFX/Bosses/vfx_boss_witchhunter_kaelen_primary.png` | `192x48` (`4x 48x48`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` boss ring |
