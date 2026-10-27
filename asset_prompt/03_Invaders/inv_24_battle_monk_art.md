# Battle Monk (`InvaderArchetype.BattleMonk`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #24 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.BattleMonk` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.Melee`
> **Lore & Behavior:** Disciplined martial artist delivering rapid consecutive strikes.
> **Combat Stats:** HP `52` | Armor `7` | Speed `3.6` | Radius `0.5` | Range `1.3` | Damage `12` | Windup `0.35s` | Cooldown `0.9s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#D9A666` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#EACEAA` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#6C5333` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Battle Monk (Disciplined martial artist delivering rapid consecutive strikes.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (Melee): Frontline martial adventurer clad in burnished steel/iron plates, forward-leaning aggressive posture, distinct melee weapon held high for clear swing telegraphing.
Color Palette: primary tunic/armor accent #D9A666, highlight #EACEAA, shadow #6C5333, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_battle_monk_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Battle Monk. Ready combat stance facing South-East, signature Melee weapon/focus clearly visible in right hand, #D9A666 tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Battle Monk. Left boot forward, right arm swinging forward, head bobbing 1px up, #D9A666 cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Battle Monk. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.35s` -> Strike):**
  > `Consistent 32x32 pixel art of Battle Monk. Full Melee attack release (disciplined martial artist delivering rapid consecutive strikes.): weapon/spell thrust forward with bright #FFFFFF and #EACEAA action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Battle Monk. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Battle Monk. Fallen face-down on dungeon stones, weapon dropped beside body, #D9A666 cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_battle_monk_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Battle Monk (Melee) attack effect (Range 1.3).
- Frame 1 (16x16): Windup telegraph glint (0.35s) in #EACEAA and #FFFFFF.
- Frame 2 (16x16): Active Melee projectile / weapon slash wave in #D9A666 with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #D9A666.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_battle_monk_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#D9A666`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_battle_monk_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
