# Vampire's Crimson Palace (Biome #04) — Environment Tileset, Props & Portal Art Prompt Specification

> **Asset Category:** Dungeon Arena Biome #04/20 (`08_Biomes`)
> **Catalog Entry:** `BiomeCatalog.GetById(4)` — `"Vampire's Crimson Palace"` (`DungeonModel.cs`)
> **Environmental Theme:** Elegant gothic flooring, red carpets, marble pillars, grand chandeliers, stained glass, crimson curtains, and royal vampire decorations.
> **Technical Spec:** `32 PPU`, Seamless Floor Tiles (`32x32 px`), Wall/Border Tiles (`32x32 px`), Destructible/Static Obstacles (`32x32` & `48x48 px`), Invader Spawn Portal (`48x48 px` 4-frame strip).

---

## 1. Exact Biome Palette (`DungeonModel.cs`)

| Surface Layer | Exact Hex (`BiomeCatalog`) | Saturation / Value Cap | Usage in Arena |
| :--- | :--- | :--- | :--- |
| **Floor Base** | `#2D151E` | Value ≤ 32%, Sat ≤ 35% | Walkable arena tiles (never competes with combat sprites) |
| **Wall / Perimeter** | `#1E161B` | Value ≤ 42% | Impassable outer boundary walls and cliff edges |
| **Obstacle / Pillar** | `#7F1D1D` | Medium contrast + `#110E1B` rim | Interior cover pillars, statues, and hazards |
| **Invader Portal** | `#E11D48` | High emissive | Swirling rift gates where invading heroes spawn |
| **Ambient Accent** | `#FDA4AF` | Emissive highlights | Glowing runes, braziers, crystals, and ambient motes |

---

## 2. Step-by-Step Environment Asset Prompts

### 2.1 Seamless Floor Tileset (`biome_04_vampire_s_crimson_palace_floors.png` — `128x32 px`, 4 variations of `32x32`)
```text
4-tile horizontal pixel art dungeon floor tileset strip (128x32 px total, four 32x32 seamless tiles) for "Vampire's Crimson Palace": Elegant gothic flooring, red carpets, marble pillars, grand chandeliers, stained glass, crimson curtains, and royal vampire decorations.
Base color #2D151E, subtle low-contrast stone/surface texture, zero high-frequency noise, value strictly capped below 32% brightness so 32x32 characters pop on top.
- Tile 1 (0..31): Standard seamless base floor tile in #2D151E.
- Tile 2 (32..63): Cracked/weathered variation with subtle #1E161B crevices.
- Tile 3 (64..95): Floor tile with faint recessed #FDA4AF runic/environmental detail.
- Tile 4 (96..127): Arena center medallion / border transition tile.
```

### 2.2 Perimeter Walls & Corner Trim (`biome_04_vampire_s_crimson_palace_walls.png` — `128x32 px`, 4 tiles of `32x32`)
```text
4-tile horizontal pixel art top-down 3/4 dungeon wall tileset (128x32 px, four 32x32 tiles) for "Vampire's Crimson Palace".
Primary wall color #1E161B with #7F1D1D trim and 1px #110E1B outer collision edge: North Wall, South Wall, East/West Wall, and Corner Pillar.
```

### 2.3 Interior Arena Obstacles & Props (`biome_04_vampire_s_crimson_palace_props.png` — `128x48 px`, 4 props of `32x48`)
```text
4 distinct 2D top-down 3/4 pixel art dungeon obstacle props (each 32x48 px, 128x48 strip, transparent background, 1px #110E1B outline) for "Vampire's Crimson Palace" (Elegant gothic flooring, red carpets, marble pillars, grand chandeliers, stained glass, crimson curtains, and royal vampire decorations.):
- Prop 1: Signature thematic pillar/statue in #7F1D1D with #FDA4AF glow.
- Prop 2: Environmental hazard / brazier / crystal cluster in #FDA4AF.
- Prop 3: Intact thematic destructible prop (tombstone/anvil/bookshelf/mushroom).
- Prop 4: Destroyed rubble state of Prop 3.
```

### 2.4 Invader Spawn Portal Strip (`biome_04_vampire_s_crimson_palace_portal.png` — `192x48 px`, 4 frames of `48x48`)
```text
4-frame horizontal pixel art animated portal strip (192x48 px, four 48x48 frames) for "Vampire's Crimson Palace".
Swirling dimensional breach framed by #7F1D1D stone arch, vortex core in #E11D48 and #FDA4AF with #FFFFFF center sparks: Frame 1 dormant sigil, Frame 2 active swirling vortex, Frame 3 invader spawn pulse, Frame 4 rift closing.
```

---

## 3. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Floor Tiles** | `Assets/_Game/Art/Environments/Biome_04_vampire_s_crimson_palace/floors.png` | `128x32` (`4x 32x32`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` floor quad (`#2D151E`) |
| **Wall Tiles** | `Assets/_Game/Art/Environments/Biome_04_vampire_s_crimson_palace/walls.png` | `128x32` (`4x 32x32`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` wall quads (`#1E161B`) |
| **Obstacle Props** | `Assets/_Game/Art/Environments/Biome_04_vampire_s_crimson_palace/props.png` | `128x48` (`4x 32x48`) | `32` | `(0.5, 0.1)` | `DungeonArenaPresenter` obstacle blocks (`#7F1D1D`) |
| **Portal Strip** | `Assets/_Game/Art/Environments/Biome_04_vampire_s_crimson_palace/portal.png` | `192x48` (`4x 48x48`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` portal markers (`#E11D48`) |
