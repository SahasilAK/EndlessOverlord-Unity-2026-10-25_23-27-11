# Shadow Duelist (`InvaderArchetype.ShadowDuelist`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #30 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.ShadowDuelist` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.Assassin`
> **Lore & Behavior:** Master swordsman dancing through shadows with blinding afterimage thrusts.
> **Combat Stats:** HP `44` | Armor `5` | Speed `4.1` | Radius `0.45` | Range `1.5` | Damage `18` | Windup `0.3s` | Cooldown `1.0s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#402659` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#9587A3` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#20132C` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Shadow Duelist (Master swordsman dancing through shadows with blinding afterimage thrusts.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (Assassin): Low-crouching hooded infiltrator in dark leather and twin curved blades, asymmetric scarf trailing behind, poised for high-speed dash strikes.
Color Palette: primary tunic/armor accent #402659, highlight #9587A3, shadow #20132C, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_shadow_duelist_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Shadow Duelist. Ready combat stance facing South-East, signature Assassin weapon/focus clearly visible in right hand, #402659 tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Shadow Duelist. Left boot forward, right arm swinging forward, head bobbing 1px up, #402659 cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Shadow Duelist. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.3s` -> Strike):**
  > `Consistent 32x32 pixel art of Shadow Duelist. Full Assassin attack release (master swordsman dancing through shadows with blinding afterimage thrusts.): weapon/spell thrust forward with bright #FFFFFF and #9587A3 action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Shadow Duelist. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Shadow Duelist. Fallen face-down on dungeon stones, weapon dropped beside body, #402659 cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_shadow_duelist_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Shadow Duelist (Assassin) attack effect (Range 1.5).
- Frame 1 (16x16): Windup telegraph glint (0.3s) in #9587A3 and #FFFFFF.
- Frame 2 (16x16): Active Assassin projectile / weapon slash wave in #402659 with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #402659.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_shadow_duelist_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#402659`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_shadow_duelist_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
