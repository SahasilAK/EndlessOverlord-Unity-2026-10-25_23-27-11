# Knight (`InvaderArchetype.Knight`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #08 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.Knight` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.Tank`
> **Lore & Behavior:** Heavy plate-armored guardian absorbing heavy punishment.
> **Combat Stats:** HP `65` | Armor `15` | Speed `2.3` | Radius `0.58` | Range `1.4` | Damage `12` | Windup `0.6s` | Cooldown `1.8s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#B2BFCC` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#D4DBE2` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#595F66` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Knight (Heavy plate-armored guardian absorbing heavy punishment.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (Tank): Broad-shouldered heavy bastion encased in layered full-plate armor and carrying an oversized tower shield or siege weapon that fills 26x28 px.
Color Palette: primary tunic/armor accent #B2BFCC, highlight #D4DBE2, shadow #595F66, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_knight_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Knight. Ready combat stance facing South-East, signature Tank weapon/focus clearly visible in right hand, #B2BFCC tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Knight. Left boot forward, right arm swinging forward, head bobbing 1px up, #B2BFCC cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Knight. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.6s` -> Strike):**
  > `Consistent 32x32 pixel art of Knight. Full Tank attack release (heavy plate-armored guardian absorbing heavy punishment.): weapon/spell thrust forward with bright #FFFFFF and #D4DBE2 action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Knight. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Knight. Fallen face-down on dungeon stones, weapon dropped beside body, #B2BFCC cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_knight_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Knight (Tank) attack effect (Range 1.4).
- Frame 1 (16x16): Windup telegraph glint (0.6s) in #D4DBE2 and #FFFFFF.
- Frame 2 (16x16): Active Tank projectile / weapon slash wave in #B2BFCC with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #B2BFCC.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_knight_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#B2BFCC`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_knight_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
