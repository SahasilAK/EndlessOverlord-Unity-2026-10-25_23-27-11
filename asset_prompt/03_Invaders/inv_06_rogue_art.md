# Rogue (`InvaderArchetype.Rogue`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #06 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.Rogue` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.Assassin`
> **Lore & Behavior:** Evasive skirmisher darting around the dungeon.
> **Combat Stats:** HP `28` | Armor `2` | Speed `4.0` | Radius `0.42` | Range `1.2` | Damage `14` | Windup `0.35s` | Cooldown `1.1s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#80808C` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#B9B9BF` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#404046` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Rogue (Evasive skirmisher darting around the dungeon.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (Assassin): Low-crouching hooded infiltrator in dark leather and twin curved blades, asymmetric scarf trailing behind, poised for high-speed dash strikes.
Color Palette: primary tunic/armor accent #80808C, highlight #B9B9BF, shadow #404046, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_rogue_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Rogue. Ready combat stance facing South-East, signature Assassin weapon/focus clearly visible in right hand, #80808C tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Rogue. Left boot forward, right arm swinging forward, head bobbing 1px up, #80808C cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Rogue. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.35s` -> Strike):**
  > `Consistent 32x32 pixel art of Rogue. Full Assassin attack release (evasive skirmisher darting around the dungeon.): weapon/spell thrust forward with bright #FFFFFF and #B9B9BF action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Rogue. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Rogue. Fallen face-down on dungeon stones, weapon dropped beside body, #80808C cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_rogue_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Rogue (Assassin) attack effect (Range 1.2).
- Frame 1 (16x16): Windup telegraph glint (0.35s) in #B9B9BF and #FFFFFF.
- Frame 2 (16x16): Active Assassin projectile / weapon slash wave in #80808C with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #80808C.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_rogue_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#80808C`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_rogue_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
