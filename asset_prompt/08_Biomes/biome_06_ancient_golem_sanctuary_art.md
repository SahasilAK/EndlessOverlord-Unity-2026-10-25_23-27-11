# Ancient Golem Sanctuary (Biome #06) — Environment Tileset, Props & Portal Art Prompt Specification

> **Asset Category:** Dungeon Arena Biome #06/20 (`08_Biomes`)
> **Catalog Entry:** `BiomeCatalog.GetById(6)` — `"Ancient Golem Sanctuary"` (`DungeonModel.cs`)
> **Environmental Theme:** Massive stone structures, ancient runic engravings, carved pillars, magical crystals, moss-covered statues, and glowing mechanical runes.
> **Technical Spec:** `32 PPU`, Seamless Floor Tiles (`32x32 px`), Wall/Border Tiles (`32x32 px`), Destructible/Static Obstacles (`32x32` & `48x48 px`), Invader Spawn Portal (`48x48 px` 4-frame strip).

---

## 1. Exact Biome Palette (`DungeonModel.cs`)

| Surface Layer | Exact Hex (`BiomeCatalog`) | Saturation / Value Cap | Usage in Arena |
| :--- | :--- | :--- | :--- |
| **Floor Base** | `#1E2621` | Value ≤ 32%, Sat ≤ 35% | Walkable arena tiles (never competes with combat sprites) |
| **Wall / Perimeter** | `#2A382F` | Value ≤ 42% | Impassable outer boundary walls and cliff edges |
| **Obstacle / Pillar** | `#4D7C0F` | Medium contrast + `#110E1B` rim | Interior cover pillars, statues, and hazards |
| **Invader Portal** | `#10B981` | High emissive | Swirling rift gates where invading heroes spawn |
| **Ambient Accent** | `#6EE7B7` | Emissive highlights | Glowing runes, braziers, crystals, and ambient motes |

---

## 2. Step-by-Step Environment Asset Prompts

### 2.1 Seamless Floor Tileset (`biome_06_ancient_golem_sanctuary_floors.png` — `128x32 px`, 4 variations of `32x32`)
```text
4-tile horizontal pixel art dungeon floor tileset strip (128x32 px total, four 32x32 seamless tiles) for "Ancient Golem Sanctuary": Massive stone structures, ancient runic engravings, carved pillars, magical crystals, moss-covered statues, and glowing mechanical runes.
Base color #1E2621, subtle low-contrast stone/surface texture, zero high-frequency noise, value strictly capped below 32% brightness so 32x32 characters pop on top.
- Tile 1 (0..31): Standard seamless base floor tile in #1E2621.
- Tile 2 (32..63): Cracked/weathered variation with subtle #2A382F crevices.
- Tile 3 (64..95): Floor tile with faint recessed #6EE7B7 runic/environmental detail.
- Tile 4 (96..127): Arena center medallion / border transition tile.
```

### 2.2 Perimeter Walls & Corner Trim (`biome_06_ancient_golem_sanctuary_walls.png` — `128x32 px`, 4 tiles of `32x32`)
```text
4-tile horizontal pixel art top-down 3/4 dungeon wall tileset (128x32 px, four 32x32 tiles) for "Ancient Golem Sanctuary".
Primary wall color #2A382F with #4D7C0F trim and 1px #110E1B outer collision edge: North Wall, South Wall, East/West Wall, and Corner Pillar.
```

### 2.3 Interior Arena Obstacles & Props (`biome_06_ancient_golem_sanctuary_props.png` — `128x48 px`, 4 props of `32x48`)
```text
4 distinct 2D top-down 3/4 pixel art dungeon obstacle props (each 32x48 px, 128x48 strip, transparent background, 1px #110E1B outline) for "Ancient Golem Sanctuary" (Massive stone structures, ancient runic engravings, carved pillars, magical crystals, moss-covered statues, and glowing mechanical runes.):
- Prop 1: Signature thematic pillar/statue in #4D7C0F with #6EE7B7 glow.
- Prop 2: Environmental hazard / brazier / crystal cluster in #6EE7B7.
- Prop 3: Intact thematic destructible prop (tombstone/anvil/bookshelf/mushroom).
- Prop 4: Destroyed rubble state of Prop 3.
```

### 2.4 Invader Spawn Portal Strip (`biome_06_ancient_golem_sanctuary_portal.png` — `192x48 px`, 4 frames of `48x48`)
```text
4-frame horizontal pixel art animated portal strip (192x48 px, four 48x48 frames) for "Ancient Golem Sanctuary".
Swirling dimensional breach framed by #4D7C0F stone arch, vortex core in #10B981 and #6EE7B7 with #FFFFFF center sparks: Frame 1 dormant sigil, Frame 2 active swirling vortex, Frame 3 invader spawn pulse, Frame 4 rift closing.
```

---

## 3. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Floor Tiles** | `Assets/_Game/Art/Environments/Biome_06_ancient_golem_sanctuary/floors.png` | `128x32` (`4x 32x32`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` floor quad (`#1E2621`) |
| **Wall Tiles** | `Assets/_Game/Art/Environments/Biome_06_ancient_golem_sanctuary/walls.png` | `128x32` (`4x 32x32`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` wall quads (`#2A382F`) |
| **Obstacle Props** | `Assets/_Game/Art/Environments/Biome_06_ancient_golem_sanctuary/props.png` | `128x48` (`4x 32x48`) | `32` | `(0.5, 0.1)` | `DungeonArenaPresenter` obstacle blocks (`#4D7C0F`) |
| **Portal Strip** | `Assets/_Game/Art/Environments/Biome_06_ancient_golem_sanctuary/portal.png` | `192x48` (`4x 48x48`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` portal markers (`#10B981`) |
