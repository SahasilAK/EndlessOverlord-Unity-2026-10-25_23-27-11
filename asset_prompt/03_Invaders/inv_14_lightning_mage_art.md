# Lightning Mage (`InvaderArchetype.LightningMage`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #14 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.LightningMage` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.Caster`
> **Lore & Behavior:** Storm evoker releasing rapid-discharge kinetic bolts.
> **Combat Stats:** HP `30` | Armor `1` | Speed `2.7` | Radius `0.45` | Range `7.5` | Damage `15` | Windup `0.5s` | Cooldown `2.2s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#F2F233` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#F7F78E` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#797919` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Lightning Mage (Storm evoker releasing rapid-discharge kinetic bolts.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (Caster): Robed arcane/elemental scholar with tall pointed hat or glowing circlet, levitating spell focus or elemental staff radiating high-contrast magic particles.
Color Palette: primary tunic/armor accent #F2F233, highlight #F7F78E, shadow #797919, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_lightning_mage_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Lightning Mage. Ready combat stance facing South-East, signature Caster weapon/focus clearly visible in right hand, #F2F233 tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Lightning Mage. Left boot forward, right arm swinging forward, head bobbing 1px up, #F2F233 cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Lightning Mage. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.5s` -> Strike):**
  > `Consistent 32x32 pixel art of Lightning Mage. Full Caster attack release (storm evoker releasing rapid-discharge kinetic bolts.): weapon/spell thrust forward with bright #FFFFFF and #F7F78E action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Lightning Mage. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Lightning Mage. Fallen face-down on dungeon stones, weapon dropped beside body, #F2F233 cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_lightning_mage_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Lightning Mage (Caster) attack effect (Range 7.5).
- Frame 1 (16x16): Windup telegraph glint (0.5s) in #F7F78E and #FFFFFF.
- Frame 2 (16x16): Active Caster projectile / weapon slash wave in #F2F233 with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #F2F233.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_lightning_mage_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#F2F233`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_lightning_mage_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
