# Grave Hound (`grave_hound`) — Summon Art & Animation Prompt Specification

> **Asset Category:** Player Summon / Undead Minion (`02_Summons`)
> **Archetype Role:** Fast Quadruped Bone Beast
> **Parent Class Affinity:** Lich Lord (`CharacterCatalog.LichLordId`) & Necromancy Skill Branch
> **Sprite Sheet Spec:** `192x32 px` (6 frames of `32x32 px`), `32 PPU`, Pivot `(0.5, 0.1)`, `FilterMode.Point`

---

## 1. Base Design & Visual Prompt

### 1.1 Palette Specification
- **Outer Rim Outline:** `#110E1B` (1px solid dark border)
- **Primary Bone / Structure:** `#BFB8A8` (Highlights `#F5F2EB`, Shadow `#7A7365`)
- **Necrotic Soul Glow:** `#30E680` (Emissive eyes, weapon core, summon runes)
- **Vestments / Armor Trim:** `#2B1E38` (Ties minion visually to Lich Lord's `#6E2BC4` faction)

### 1.2 Copy-Paste Base Design Prompt (Frame 1 — Idle)
```text
2D top-down 3/4 view pixel art sprite of Grave Hound (Fast Quadruped Bone Beast), undead minion summon in a dark fantasy roguelite.
Canvas: 32x32 pixels, transparent background (RGBA32), crisp pixel art, zero anti-aliasing, 1px dark outline (#110E1B).
Design & Silhouette: Low-slung quadruped skeletal wolf-hound with elongatedfang-filled skull, jagged exposed spine ridges, glowing green soul-heart inside ribcage (`#30E680`), trailing dark violet ectoplasm.
Palette: bleached bone #BFB8A8, necrotic emerald glow #30E680, dark royal trim #2B1E38, deep shadow #1B1526.
Lighting: top-left zenithal light, distinct silhouette readable against dark dungeon stone floors.
```

---

## 2. Step-by-Step 6-Frame Sprite Strip Prompts (`sum_grave_hound_sheet.png` — `192x32 px`)

- **Frame 01 (`0..31, 0..31`) — `Idle`:**
  > `Consistent 32x32 pixel art of Grave Hound. Alert combat-ready stance facing South-East, #30E680 eyes glowing brightly, weapon/claws poised.`
- **Frame 02 (`32..63, 0..31`) — `Walk_1`:**
  > `Consistent 32x32 pixel art of Grave Hound. Left leg/limb stepping forward, torso bobbing 1px up, weapon swinging back 1px.`
- **Frame 03 (`64..95, 0..31`) — `Walk_2`:**
  > `Consistent 32x32 pixel art of Grave Hound. Right leg/limb stepping forward, torso settling 1px down, 1px #30E680 soul wisp trailing behind.`
- **Frame 04 (`96..127, 0..31`) — `Attack`:**
  > `Consistent 32x32 pixel art of Grave Hound. Forward pounce bite snapping jaws shut with `16x16` green fang-crunch VFX. Forward lunge with bright #30E680 and #FFFFFF motion arc on weapon edge.`
- **Frame 05 (`128..159, 0..31`) — `Hit_React`:**
  > `Consistent 32x32 pixel art of Grave Hound. Knocked back 2px, skull tilted back, 50% white/emerald flash on bone pixels, 2 bone splinters flying off.`
- **Frame 06 (`160..191, 0..31`) — `Death_Collapse`:**
  > `Consistent 32x32 pixel art of Grave Hound. Disjointed pile of #BFB8A8 bones and skull on ground, #30E680 eye light fading into a rising wisp.`

---

## 3. Spawn Eruption & Attack VFX Prompts

- **Spawn Rise VFX (`vfx_sum_grave_hound_spawn.png` — `128x32 px`, 4 frames of `32x32`):**
  > `4-frame pixel art summon materialization strip (128x32 px): Frame 1 glowing #30E680 necromantic floor sigil, Frame 2 skeletal hand/bones bursting upward through dirt and purple mist #2B1E38, Frame 3 vertical column of #30E680 soul fire, Frame 4 settling bone dust.`
- **Minion Attack VFX (`vfx_sum_grave_hound_attack.png` — `48x16 px`, 3 frames of `16x16`):**
  > `3-frame pixel art attack effect (48x16 px): Forward pounce bite snapping jaws shut with `16x16` green fang-crunch VFX. Crisp #30E680 core with #110E1B 1px outer contrast rim.`

---

## 4. Unity Integration & Placeholder Replacement

| Target Asset | Unity Path | Size | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Sprite Sheet** | `Assets/_Game/Art/Characters/Summons/sum_grave_hound_sheet.png` | `192x32` | `32` | `(0.5, 0.1)` | `SkeletonView` / `SummonEntity` (`PlaceholderSprites.Circle` green tint) |
| **Spawn VFX** | `Assets/_Game/Art/VFX/Summons/vfx_sum_grave_hound_spawn.png` | `128x32` | `32` | `(0.5, 0.1)` | Instant pop-in spawn in `GameBootstrap` |
| **Attack VFX** | `Assets/_Game/Art/VFX/Summons/vfx_sum_grave_hound_attack.png` | `48x16` | `16` | `(0.5, 0.5)` | `ProjectileView` / melee flash placeholder |
