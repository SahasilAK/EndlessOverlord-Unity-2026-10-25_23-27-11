# Royal Champion (`InvaderArchetype.RoyalChampion`) — Invader Art & Animation Prompt Specification

> **Asset Category:** Hero Invader Archetype #28 (`03_Invaders`)
> **Catalog Enum:** `InvaderArchetype.RoyalChampion` (`InvaderCatalog.cs`)
> **Combat Role:** `InvaderCombatRole.Tank`
> **Lore & Behavior:** Elite commander of the royal vanguard; cleaves foes and summons reinforcements upon enrage.
> **Combat Stats:** HP `220` | Armor `15` | Speed `2.4` | Radius `0.9` | Range `2.4` | Damage `20` | Windup `0.55s` | Cooldown `1.8s`

---

## 1. Base Design & Visual Prompt

### 1.1 Color Palette & Faction Readability
- **Outer Rim Outline:** `#110E1B` (1px solid dark outline)
- **Primary Archetype Color:** `#9933BF` (Exact `InvaderCatalog` RGB tint — used on cloak, tabard, or magical trim)
- **Highlight / Rim Light:** `#C68EDB` (Top-left zenithal highlight on helmet, shoulders, and weapon edge)
- **Deep Shadow:** `#4C195F` (Lower boots, interior cape folds)
- **Invader Faction Rule:** Warm/High-Value Heroic Steel & Gold (`#E8C170`) accents so player immediately distinguishes invading heroes from dark-toned player minions.

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of an invading hero adventurer: Royal Champion (Elite commander of the royal vanguard; cleaves foes and summons reinforcements upon enrage.).
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px solid dark outline (#110E1B).
Role Silhouette (Tank): Broad-shouldered heavy bastion encased in layered full-plate armor and carrying an oversized tower shield or siege weapon that fills 26x28 px.
Color Palette: primary tunic/armor accent #9933BF, highlight #C68EDB, shadow #4C195F, burnished steel #C4CCD4, warm heroic gold trim #E8C170.
Perspective & Anchor: 3/4 overhead dungeon crawler view, feet anchored at bottom-center (X=16, Y=3, Unity Pivot 0.5, 0.1).
```

---

## 2. Step-by-Step 6-Frame Sprite Sheet Prompts (`inv_royal_champion_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Royal Champion. Ready combat stance facing South-East, signature Tank weapon/focus clearly visible in right hand, #9933BF tabard/cloak crisp.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Royal Champion. Left boot forward, right arm swinging forward, head bobbing 1px up, #9933BF cloak flaring 1px back.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Royal Champion. Right boot forward, left arm forward, torso 1px lower, dynamic advancing march into the boss lair.`
- **Frame 04 (`96..127, 0..31`) — `Attack_Cast` (Windup `0.55s` -> Strike):**
  > `Consistent 32x32 pixel art of Royal Champion. Full Tank attack release (elite commander of the royal vanguard; cleaves foes and summons reinforcements upon enrage.): weapon/spell thrust forward with bright #FFFFFF and #C68EDB action smear.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Royal Champion. Staggered backward 2px from boss hit, posture broken, 45% white/red hit flash overlay, 2 pixel sparks.`
- **Frame 06 (`160..191, 0..31`) — `Death`:**
  > `Consistent 32x32 pixel art of Royal Champion. Fallen face-down on dungeon stones, weapon dropped beside body, #9933BF cloak pooled on floor, soul orb rising.`

---

## 3. Projectile / Attack Telegraph VFX Prompt (`vfx_inv_royal_champion_attack.png` — `48x16 px`)

```text
3-frame horizontal pixel art sprite strip (48x16 px, three 16x16 frames), transparent background, 1px #110E1B outline on solid cores.
Subject: Royal Champion (Tank) attack effect (Range 2.4).
- Frame 1 (16x16): Windup telegraph glint (0.55s) in #C68EDB and #FFFFFF.
- Frame 2 (16x16): Active Tank projectile / weapon slash wave in #9933BF with #FFFFFF leading edge.
- Frame 3 (16x16): On-hit burst dispersing into 5 pixel sparks of #9933BF.
```

---

## 4. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Current Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Invader Sheet** | `Assets/_Game/Art/Characters/Invaders/inv_royal_champion_sheet.png` | `192x32` (`6x 32x32`) | `32` | `(0.5, 0.1)` | `InvaderView` (`PlaceholderSprites.Circle` tinted `#9933BF`) |
| **Attack / Proj VFX** | `Assets/_Game/Art/VFX/Invaders/vfx_inv_royal_champion_attack.png` | `48x16` (`3x 16x16`) | `16` | `(0.5, 0.5)` | `AttackTelegraphPresenter` / `ProjectileView` |
