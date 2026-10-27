# Cleric (`InvaderArchetype.Cleric`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #10 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.Cleric` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.HealerSupport`
> **Lore & Behavior:** Field medic casting restorative waves on injured comrades.
> **Combat Stats:** HP `40` | Armor `8` | Speed `2.5` | Radius `0.5` | Range `6.0` | Damage `6` | Windup `0.8s` | Cooldown `3.5s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#F2E64C` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#F7F19C` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#797326` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Cleric (Field medic casting restorative waves on injured comrades.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (HealerSupport): Sanctified cleric/bard/enchanter in ivory-and-gold vestments, holding a luminous holy symbol, censer, or banner that projects a warm buff halo.
Color Palette: primary tunic/armor accent #F2E64C, highlight #F7F19C, shadow #797326, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_cleric_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Cleric. Ready combat stance facing South-East, signature HealerSupport weapon/focus clearly visible in right hand, #F2E64C tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Cleric. Left boot forward, right arm swinging forward, head bobbing 1px up, #F2E64C cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Cleric. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.8s` -> Strike):**
  > `Consistent 32x32 pixel art of Cleric. Full HealerSupport attack release (field medic casting restorative waves on injured comrades.): weapon/spell thrust forward with bright #FFFFFF and #F7F19C action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Cleric. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Cleric. Fallen face-down on dungeon stones, weapon dropped beside body, #F2E64C cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_cleric_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Cleric (HealerSupport) attack effect (Range 6.0).
- Frame 1 (16x16): Windup telegraph glint (0.8s) in #F7F19C and #FFFFFF.
- Frame 2 (16x16): Active HealerSupport projectile / weapon slash wave in #F2E64C with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #F2E64C.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_cleric_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#F2E64C`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_cleric_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
