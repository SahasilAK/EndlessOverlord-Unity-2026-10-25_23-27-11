# Abandoned Dwarven Forge (Biome #09) — Environment Tileset, Props & Portal Art Prompt Specification

> **Asset Category:** Dungeon Arena Biome #09/20 (`08_Biomes`)
> **Catalog Entry:** `BiomeCatalog.GetById(9)` — `"Abandoned Dwarven Forge"` (`DungeonModel.cs`)
> **Environmental Theme:** Massive anvils, machinery, metal flooring, conveyor mechanisms, molten forges, enormous chains, and underground workshops.
> **Technical Spec:** `32 PPU`, Seamless Floor Tiles (`32x32 px`), Wall/Border Tiles (`32x32 px`), Destructible/Static Obstacles (`32x32` & `48x48 px`), Invader Spawn Portal (`48x48 px` 4-frame strip).

---

## 1. Exact Biome Palette (`DungeonModel.cs`)

| Surface Layer | Exact Hex (`BiomeCatalog`) | Saturation / Value Cap | Usage in Arena |
| :--- | :--- | :--- | :--- |
| **Floor Base** | `#2B2625` | Value ≤ 32%, Sat ≤ 35% | Walkable arena tiles (never competes with combat sprites) |
| **Wall / Perimeter** | `#3D3432` | Value ≤ 42% | Impassable outer boundary walls and cliff edges |
| **Obstacle / Pillar** | `#B45309` | Medium contrast + `#110E1B` rim | Interior cover pillars, statues, and hazards |
| **Invader Portal** | `#F59E0B` | High emissive | Swirling rift gates where invading heroes spawn |
| **Ambient Accent** | `#FCD34D` | Emissive highlights | Glowing runes, braziers, crystals, and ambient motes |

---

## 2. Step-by-Step Environment Asset Prompts

### 2.1 Seamless Floor Tileset (`biome_09_abandoned_dwarven_forge_floors.png` — `128x32 px`, 4 variations of `32x32`)
```text
4-tile horizontal pixel art dungeon floor tileset strip (128x32 px total, four 32x32 seamless tiles) for "Abandoned Dwarven Forge": Massive anvils, machinery, metal flooring, conveyor mechanisms, molten forges, enormous chains, and underground workshops.
Base color #2B2625, subtle low-contrast stone/surface texture, zero high-frequency noise, value strictly capped below 32% brightness so 32x32 characters pop on top.
- Tile 1 (0..31): Standard seamless base floor tile in #2B2625.
- Tile 2 (32..63): Cracked/weathered variation with subtle #3D3432 crevices.
- Tile 3 (64..95): Floor tile with faint recessed #FCD34D runic/environmental detail.
- Tile 4 (96..127): Arena center medallion / border transition tile.
```

### 2.2 Perimeter Walls & Corner Trim (`biome_09_abandoned_dwarven_forge_walls.png` — `128x32 px`, 4 tiles of `32x32`)
```text
4-tile horizontal pixel art top-down 3/4 dungeon wall tileset (128x32 px, four 32x32 tiles) for "Abandoned Dwarven Forge".
Primary wall color #3D3432 with #B45309 trim and 1px #110E1B outer collision edge: North Wall, South Wall, East/West Wall, and Corner Pillar.
```

### 2.3 Interior Arena Obstacles & Props (`biome_09_abandoned_dwarven_forge_props.png` — `128x48 px`, 4 props of `32x48`)
```text
4 distinct 2D top-down 3/4 pixel art dungeon obstacle props (each 32x48 px, 128x48 strip, transparent background, 1px #110E1B outline) for "Abandoned Dwarven Forge" (Massive anvils, machinery, metal flooring, conveyor mechanisms, molten forges, enormous chains, and underground workshops.):
- Prop 1: Signature thematic pillar/statue in #B45309 with #FCD34D glow.
- Prop 2: Environmental hazard / brazier / crystal cluster in #FCD34D.
- Prop 3: Intact thematic destructible prop (tombstone/anvil/bookshelf/mushroom).
- Prop 4: Destroyed rubble state of Prop 3.
```

### 2.4 Invader Spawn Portal Strip (`biome_09_abandoned_dwarven_forge_portal.png` — `192x48 px`, 4 frames of `48x48`)
```text
4-frame horizontal pixel art animated portal strip (192x48 px, four 48x48 frames) for "Abandoned Dwarven Forge".
Swirling dimensional breach framed by #B45309 stone arch, vortex core in #F59E0B and #FCD34D with #FFFFFF center sparks: Frame 1 dormant sigil, Frame 2 active swirling vortex, Frame 3 invader spawn pulse, Frame 4 rift closing.
```

---

## 3. Unity Import & Placeholder Replacement Spec

| Target Asset | Unity Project Path | Dimensions | PPU | Pivot | Replaces Placeholder |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Floor Tiles** | `Assets/_Game/Art/Environments/Biome_09_abandoned_dwarven_forge/floors.png` | `128x32` (`4x 32x32`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` floor quad (`#2B2625`) |
| **Wall Tiles** | `Assets/_Game/Art/Environments/Biome_09_abandoned_dwarven_forge/walls.png` | `128x32` (`4x 32x32`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` wall quads (`#3D3432`) |
| **Obstacle Props** | `Assets/_Game/Art/Environments/Biome_09_abandoned_dwarven_forge/props.png` | `128x48` (`4x 32x48`) | `32` | `(0.5, 0.1)` | `DungeonArenaPresenter` obstacle blocks (`#B45309`) |
| **Portal Strip** | `Assets/_Game/Art/Environments/Biome_09_abandoned_dwarven_forge/portal.png` | `192x48` (`4x 48x48`) | `32` | `(0.5, 0.5)` | `DungeonArenaPresenter` portal markers (`#F59E0B`) |
