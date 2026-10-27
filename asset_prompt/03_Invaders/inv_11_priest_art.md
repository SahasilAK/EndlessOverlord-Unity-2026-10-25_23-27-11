# Priest (`InvaderArchetype.Priest`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #11 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.Priest` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.HealerSupport`
> **Lore & Behavior:** Sanctuary chanter reinforcing allies with divine blessings.
> **Combat Stats:** HP `36` | Armor `4` | Speed `2.6` | Radius `0.48` | Range `6.5` | Damage `5` | Windup `0.75s` | Cooldown `3.0s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#F2F2D9` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#F7F7EA` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#79796C` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Priest (Sanctuary chanter reinforcing allies with divine blessings.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (HealerSupport): Sanctified cleric/bard/enchanter in ivory-and-gold vestments, holding a luminous holy symbol, censer, or banner that projects a warm buff halo.
Color Palette: primary tunic/armor accent #F2F2D9, highlight #F7F7EA, shadow #79796C, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_priest_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Priest. Ready combat stance facing South-East, signature HealerSupport weapon/focus clearly visible in right hand, #F2F2D9 tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Priest. Left boot forward, right arm swinging forward, head bobbing 1px up, #F2F2D9 cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Priest. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.75s` -> Strike):**
  > `Consistent 32x32 pixel art of Priest. Full HealerSupport attack release (sanctuary chanter reinforcing allies with divine blessings.): weapon/spell thrust forward with bright #FFFFFF and #F7F7EA action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Priest. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Priest. Fallen face-down on dungeon stones, weapon dropped beside body, #F2F2D9 cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_priest_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Priest (HealerSupport) attack effect (Range 6.5).
- Frame 1 (16x16): Windup telegraph glint (0.75s) in #F7F7EA and #FFFFFF.
- Frame 2 (16x16): Active HealerSupport projectile / weapon slash wave in #F2F2D9 with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #F2F2D9.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_priest_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#F2F2D9`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_priest_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
