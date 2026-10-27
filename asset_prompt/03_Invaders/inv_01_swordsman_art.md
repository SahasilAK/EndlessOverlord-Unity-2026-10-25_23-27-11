# Swordsman (`InvaderArchetype.Swordsman`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #01 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.Swordsman` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.Melee`
> **Lore & Behavior:** Standard frontline melee infantry with balanced strikes.
> **Combat Stats:** HP `35` | Armor `5` | Speed `3.2` | Radius `0.5` | Range `1.3` | Damage `10` | Windup `0.5s` | Cooldown `1.4s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#E6594C` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#F1A39C` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#732C26` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Swordsman (Standard frontline melee infantry with balanced strikes.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (Melee): Frontline martial adventurer clad in burnished steel/iron plates, forward-leaning aggressive posture, distinct melee weapon held high for clear swing telegraphing.
Color Palette: primary tunic/armor accent #E6594C, highlight #F1A39C, shadow #732C26, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_swordsman_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Swordsman. Ready combat stance facing South-East, signature Melee weapon/focus clearly visible in right hand, #E6594C tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Swordsman. Left boot forward, right arm swinging forward, head bobbing 1px up, #E6594C cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Swordsman. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.5s` -> Strike):**
  > `Consistent 32x32 pixel art of Swordsman. Full Melee attack release (standard frontline melee infantry with balanced strikes.): weapon/spell thrust forward with bright #FFFFFF and #F1A39C action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Swordsman. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Swordsman. Fallen face-down on dungeon stones, weapon dropped beside body, #E6594C cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_swordsman_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Swordsman (Melee) attack effect (Range 1.3).
- Frame 1 (16x16): Windup telegraph glint (0.5s) in #F1A39C and #FFFFFF.
- Frame 2 (16x16): Active Melee projectile / weapon slash wave in #E6594C with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #E6594C.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_swordsman_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#E6594C`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_swordsman_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
