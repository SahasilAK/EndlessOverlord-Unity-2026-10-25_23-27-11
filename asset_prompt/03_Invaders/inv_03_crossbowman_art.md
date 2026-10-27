# Crossbowman (`InvaderArchetype.Crossbowman`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #03 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.Crossbowman` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.Ranged`
> **Lore & Behavior:** Heavy armor-piercing ranged marksman with powerful, slow bolts.
> **Combat Stats:** HP `30` | Armor `4` | Speed `2.4` | Radius `0.48` | Range `8.5` | Damage `16` | Windup `0.9s` | Cooldown `3.0s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#CC8C4C` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#E2BF9C` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#664626` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Crossbowman (Heavy armor-piercing ranged marksman with powerful, slow bolts.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (Ranged): Agile marksman in travel cloak and leather brigandine, ranged weapon (bow/crossbow/firearm) held horizontally across chest with bright projectile tip.
Color Palette: primary tunic/armor accent #CC8C4C, highlight #E2BF9C, shadow #664626, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_crossbowman_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Crossbowman. Ready combat stance facing South-East, signature Ranged weapon/focus clearly visible in right hand, #CC8C4C tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Crossbowman. Left boot forward, right arm swinging forward, head bobbing 1px up, #CC8C4C cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Crossbowman. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.9s` -> Strike):**
  > `Consistent 32x32 pixel art of Crossbowman. Full Ranged attack release (heavy armor-piercing ranged marksman with powerful, slow bolts.): weapon/spell thrust forward with bright #FFFFFF and #E2BF9C action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Crossbowman. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Crossbowman. Fallen face-down on dungeon stones, weapon dropped beside body, #CC8C4C cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_crossbowman_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Crossbowman (Ranged) attack effect (Range 8.5).
- Frame 1 (16x16): Windup telegraph glint (0.9s) in #E2BF9C and #FFFFFF.
- Frame 2 (16x16): Active Ranged projectile / weapon slash wave in #CC8C4C with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #CC8C4C.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_crossbowman_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#CC8C4C`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_crossbowman_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
