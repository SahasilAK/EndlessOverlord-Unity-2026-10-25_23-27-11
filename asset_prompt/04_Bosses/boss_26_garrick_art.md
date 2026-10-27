# Garrick, The Behemoth Slayer (`boss_garrick`) — Legendary Boss Art & Animation Prompt Specification

> **Asset Category:** Milestone Legendary Hero Boss #26 (`04_Bosses`)
> **Catalog ID:** `"boss_garrick"` (`LegendaryBossCatalog.cs`)
> **Base Archetype Lineage:** `InvaderArchetype.MonsterHunter`
> **Lore & Combat Identity:** Veteran colossus hunter deploying heavy siege stakes designed to topple giant beasts.
> **Combat Stats:** HP `290` | Armor `18` | Speed `3.0` | Radius `0.85` | Range `7.8` | Damage `30` | Windup `0.7s` | Cooldown `2.0s`
> **Primary Ability:** `BossAbilityType.MultishotBarrage` (Fan-spread volley of 7 high-velocity heroic projectiles (`24x24 px` each))
> **Secondary Ability:** `BossAbilityType.ShockwaveSlam` (Tectonic ground-shattering slam ring (`64x64 px`))
> **Sprite Sheet Spec:** `288x48 px` (6 frames of `48x48 px`), `32 PPU` (1.5×1.5 world units), Pivot `Bottom-Center (0.5, 0.08)`, `FilterMode.Point`

---

## 1. Base Design & Visual Prompt (`48x48 px`)

### 1.1 Color Palette & Boss Presence
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Sovereign Hue:** `#BF6633` (`LegendaryBossCatalog` RGB — dominates cape, crest, and weapon aura)
- **Radiant Highlight:** `#DBAA8E` (Crown/helm crest, shoulder pauldrons, weapon edge)
- **Shadow Core:** `#5F3319` (Deep armor recesses and lower mantle)
- **Heroic Masterwork Trim:** `#FFE082` (Burnished champion gold filigree)

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Walk / Command Stance)
```text
Masterpiece 2D top-down 3/4 view pixel art boss sprite of Garrick, "The Behemoth Slayer" (MonsterHunter champion): Veteran colossus hunter deploying heavy siege stakes designed to topple giant beasts.
Canvas: 48x48 pixels (1.5x larger than standard 32x32 units), transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Silhouette: imposing heroic commander filling a 40x44 px footprint, ornate crested helm/crown, oversized masterwork MonsterHunter weapon glowing with #BF6633 energy, sweeping champion cape, unmistakable silhouette.
Palette: primary #BF6633, highlight #DBAA8E, deep shadow #5F3319, champion gold #FFE082.
Foot Anchor: centered at X=24, Y=4 from bottom edge (Unity Pivot 0.5, 0.08).
```

---

## 2. Step-by-Step 6-Frame Boss Sprite Sheet Prompts (`boss_26_boss_garrick_sheet.png` — `288x48 px`)

- **Frame 01 (`0..47, 0..47`) — `Walk_Command`:**
  > `Consistent 48x48 pixel art of Garrick (The Behemoth Slayer). Striding forward with commanding presence, cape billowing, masterwork weapon poised, #BF6633 aura motes rising.`
- **Frame 02 (`48..95, 0..47`) — `Windup_Telegraph` (`0.7s`):**
  > `Consistent 48x48 pixel art of Garrick. High-contrast telegraph pose: weapon drawn high overhead/back, body coiled, bright #FFFFFF and #DBAA8E warning glint on weapon tip (8px star).`
- **Frame 03 (`96..143, 0..47`) — `Primary_Ability` (`MultishotBarrage`):**
  > `Consistent 48x48 pixel art of Garrick. Executing MultishotBarrage (fan-spread volley of 7 high-velocity heroic projectiles (`24x24 px` each)): explosive forward release with #BF6633 and #FFFFFF energy arc wrapping 270° around body.`
- **Frame 04 (`144..191, 0..47`) — `Secondary_Ability` (`ShockwaveSlam`):**
  > `Consistent 48x48 pixel art of Garrick. Executing ShockwaveSlam (tectonic ground-shattering slam ring (`64x64 px`)): secondary stance with off-hand/weapon channeling #DBAA8E sigils.`
- **Frame 05 (`192..239, 0..47`) — `Enrage_State` (`BossModifier.Enrage`):**
  > `Consistent 48x48 pixel art of Garrick. Enraged berserk posture: eyes blazing pure #FFFFFF, jagged crimson-gold (#FF3300 / #DBAA8E) flame crown erupting from shoulders, 2px outer aura ring.`
- **Frame 06 (`240..287, 0..47`) — `Death_Defeat`:**
  > `Consistent 48x48 pixel art of Garrick. Dropped to one knee then collapsed, masterwork weapon planted/shattered on dungeon stone, golden-violet champion soul ascending.`

---

## 3. Boss Ability & Modifier VFX Prompts

1. **Primary Ability VFX (`vfx_boss_garrick_multishotbarrage.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Garrick's MultishotBarrage: Fan-spread volley of 7 high-velocity heroic projectiles (`24x24 px` each), colored in #BF6633 and #FFFFFF with #110E1B 1px outer contrast rim.`
2. **Secondary Ability VFX (`vfx_boss_garrick_shockwaveslam.png` — `192x48 px`, 4 frames of `48x48`):**
   > `4-frame pixel art boss ability strip (192x48 px) for Garrick's ShockwaveSlam: Tectonic ground-shattering slam ring (`64x64 px`), colored in #DBAA8E and #BF6633.`
3. **Boss Modifier Overlays (`BossModifier` shared 48x48 overlays):**
   - `Enrage`: Pulsing `#FF2A00` jaggedaura border.
   - `Barrier`: Translucent `#00E5FF` hexagonal dome (`CueIds.BossModifierBarrier`).
   - `Regeneration`: Ascending `#2ECC71` emerald plus-sparks.
   - `ElementalOvercharge`: Orbiting tri-elemental orbs (`#FF4500`, `#00E5FF`, `#FFD700`).

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Boss Sheet** | `Assets/_Game/Art/Characters/Bosses/boss_26_boss_garrick_sheet.png` | `288x48` (`6x 48x48`) | `32` | `(0.5, 0.08)` | `InvaderView` boss scale (`PlaceholderSprites.Circle` tinted `#BF6633`) |
| **Boss Portrait** | `Assets/_Game/Art/UI/BossPortraits/portrait_boss_garrick.png` | `48x48` | `16` | `(0.5, 0.5)` | `BossEncounterBannerPresenter` nameplate icon |
| **Primary VFX** | `Assets/_Game/Art/VFX/Bosses/vfx_boss_garrick_primary.png` | `192x48` (`4x 48x48`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` boss ring |
