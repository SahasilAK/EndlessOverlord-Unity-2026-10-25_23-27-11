# Art replacement and missing animation implementation plan

**Goal:** replace tagged and already-wired placeholder art with reference-quality assets, add missing art and animation, and preserve the built Unity game's behavior.

**Architecture:** ComfyUI candidates remain in staging. Approved per-state sprites enter a Unity animation asset database keyed by actual catalog IDs. Presenters consume clips without controlling gameplay damage or cooldowns.

**Stack:** existing Unity C#, Stability Matrix / ComfyUI SDXL, Pillow for inventory and packing, a pixel editor for pose/alpha/cluster cleanup.

**Spec:** `ART_DIRECTION.md`. Production begins after the user tests the Lich workflow and gives feedback. This document plans future Unity changes; none are claimed implemented in this pass.

## Evidence and scope

Read `inventory.csv` for current files, old census tags and literal runtime references. Read `placeholder_sites.csv` for primitive fallbacks. These are evidence inventories, not perfect semantic coverage: interpolated biome/UI paths need an additional runtime sweep. A missing old census path can be a stale name, not missing art. Do not generate duplicate assets for it until mapped to a current catalog ID.

`prompts/catalog_prompts.json` extracts current weapons, armors, bosses, invaders, spells, biomes and skill definitions. Use IDs from this file; prose names in `docs/Development/CONTENT_COVERAGE.md` are stale in places. The five playable characters and ten census summons have briefs in `prompts/`.

## Task 1 — Lich visual approval

Files: `workflows/01_Lich_Reference_Master.json`, `02_Pose_Guided_Frame.json`, `prompts/characters.md`, `pilot_feedback.md`.

- [ ] Run the one-character procedure. Keep master, seed and parameter record.
- [ ] Approve silhouette, palette and pixel density at 128 and 256 before selecting the export cell size.
- [ ] Approve four idle frames at 6 FPS. Then produce eight run and eight cast frames.
- [ ] Compare animation seam and identity; reject staff/rib/crown changes, foot sliding and clipped silhouettes.
- [ ] Confirm facing strategy and authored left/right need. Freeze the contract only after feedback.

Deliverable: approved Lich master plus idle/run/cast strips, not a roster batch. Verify by playing loops and scrubbing nonloops, on contrasting backgrounds and at actual game size.

## Task 2 — Asset identity and importer

Create `Assets/_Game/Scripts/Game/Art/ActorArtDefinition.cs` (ScriptableObject), `ArtDatabase.cs`, `SpriteClipPlayer.cs`, and `Assets/_Game/Scripts/Editor/ArtImportPipeline.cs` in the existing appropriate assemblies. First inspect asmdefs and editor folder conventions.

Interfaces: database key = exact gameplay entity ID; actor definition maps state/facing to Sprite[] with FPS, loop, pivot, contact/release marker and optional weapon socket coordinates. Texture import = Sprite Multiple, Point, no compression, no mipmaps, full-rect slices with a shared cell size. Marker time is presentation metadata, never an extra damage trigger.

- [ ] Reconcile every census tag/current path with catalog IDs; record aliases and replacement destination in the inventory.
- [ ] Add per-state assets under `Assets/_Game/Art/Production/<category>/<id>/<state>.png`; preserve existing files and `.meta` until mappings migrate.
- [ ] Validate dimensions, frame counts, alpha, pivots, required states and duplicate IDs. Fail visibly on malformed strips.
- [ ] Set PPU and visual child transform to match old world footprint; retain actor colliders and gameplay root scale.
- [ ] Use serialized imported sprites for builds. `SpriteSheetRegistry.cs` currently reads raw files via `Application.dataPath`, which is unsuitable as the sole packaged-build asset path.
- [ ] Test a player build, not only Editor PlayMode: sprites must load with no loose project PNG dependency.

Tests: valid/invalid clips, empty state fallback, repeated cached loads, asset ID collisions, correct frame rects and pivot, asset references included in build. Do not delete old assets before fallback and build tests pass.

## Task 3 — Actor animation integration

Modify `Assets/_Game/Scripts/Game/Combat/LichLordVisualPresenter.cs`, `InvaderActor.cs`, `UndeadMinionActor.cs`, `CryptArcherActor.cs` and the actual summon presenters found during integration. Replace fixed 32px six-slot slicing with the new clip player.

- [ ] Player: idle/run/attack/cast/hurt/death/dash state transitions, using current events and velocity. Priority: death, hurt, active attack/cast, movement, idle. Restart a one-shot once per event, not each Update.
- [ ] Enemies/bosses: replace `GetIdleSprite`-only behavior with state-driven clips. Synchronize windup/recovery with existing attack timers; do not delay or duplicate gameplay hits.
- [ ] Summons: spawn/idle/run/attack/hurt/death; implement quadruped and flying poses only where actor behavior needs them.
- [ ] Death plays once and holds its terminal frame. Pooled actors reset timers, facing and state on reuse. Despawn timing must permit the death visual without retaining collision/damage.
- [ ] Boss primary and secondary abilities get distinct visual cues even when sharing a cast body clip.

Tests: each state reachable, death never loops, hurt interrupts safely, zero-motion idle, rapid casts, pool reuse, flip/socket consistency, hit release aligned at varying cooldown speeds. Run relevant existing `ArtAndAudioPlayModeTests`, combat, class, invader and boss suites using the project's test runner. Record fresh results rather than copying old counts.

## Task 4 — Weapons, magic, projectiles and impacts

Modify `SpriteSheetRegistry.GetItemSprite` or replace callers with database lookups; currently multiple items share a category fallback icon. Inspect `ProjectilesAndAttack.cs`, `MagicCastSystem.cs` if present, `HazardZoneActor.cs`, `AbilityLoadoutController.cs` and their actual effect actors.

- [ ] Produce icon and drop sprite for each of 150 weapon and 30 armor IDs. Group held-weapon animations by the 15 families in `asset_families.md`.
- [ ] Define authored grip offsets and mirror behavior. Add held sprites only to characters designed for interchangeable equipment; do not layer a second weapon over a baked one.
- [ ] All 80 spells: icon, cast, travel/active, impact and dissipate; area damage also gets telegraph. Use behavior contracts, not one generic bolt for every school.
- [ ] Class skills: review every extracted skill ID against `AbilityLoadoutController`; passive skills need icons, not fabricated projectile art. Visible buffs/debuffs, shields, chains, summons and hazards need dedicated cues.
- [ ] Boss ability pool: telegraph + active + end for ColossalCleave, ShockwaveSlam, MultishotBarrage, PhantomBlinkStrike, MoltenEruption, GlacialPrison, ThunderStrike, SanctuaryWard, WhirlwindCharge and SummonVanguard.
- [ ] Add modifier cues for enrage/regeneration/barrier/reinforcements/elemental overcharge, and magic synergy effects where runtime exposes them.

Tests: launch/travel/impact lifecycle and pooling, no immortal loop after despawn, correct projectile orientation, beam tiling/end caps, area radius matches gameplay, warnings visible in crowds, frame-rate-independent playback. Gameplay remains authoritative for hit detection.

## Task 5 — Environment, props and UI

- [ ] All 20 biomes: floor variants, walls/edges/corners, pillars and collision props; seams checked in 3x3 patches. Decorative motion cannot move collision boundaries.
- [ ] Coin/gem/potion pickup loops, chest opening, merchant interaction, five shrine state sets and four mutation effects. Check actual event triggers before adding clips.
- [ ] UI: five portraits, skill/equipment icons, slot and card states, health/resource bars, menu/results/merchant/skill-tree frames. Use nine-slice borders; retain native text and accessibility/readability.
- [ ] Review each `placeholder_sites.csv` row. Solid health-bar fills and layout primitives may intentionally remain procedural; do not replace them blindly.

Tests: read at target resolution, tile seams, UI scaling, item identification without relying solely on rarity color, event state changes, no loss of interactability.

## Task 6 — Batches and final replacement

Order: approved Lich -> one melee and one ranged invader -> Bone Sentinel and Crypt Archer -> one boss -> one biome and full combat effects -> remaining four players -> remaining invaders/bosses/summons -> all equipment/spells/biomes/UI.

- [ ] For each batch update manifest with source reference, exact checkpoint/hash, seed, prompts, settings, generation date, alpha cleanup, palette and approval state.
- [ ] Wire approved assets, verify in arena and in a player build, then mark `imported`, `in_game_verified`, `animation_verified`. File existence alone never means complete.
- [ ] Profile spawn-heavy fights, texture memory, sorting and animation update cost. Atlas where useful; avoid per-frame disk I/O.
- [ ] Retire only assets with zero remaining references after serialized and runtime lookup audit. Preserve rollback commits and `.meta` GUIDs during path replacements.

Completion criteria: every catalog ID has an approved mapping or explicit intentionally shared mapping; every reachable visual state has a verified clip; no stale placeholder fallback appears in normal play; build and relevant tests pass; user accepts the resulting art.

## Review focus

Check stale IDs versus actual missing assets; alpha holes in bone/teal/black materials; foot pivot and world-size changes; loop/nonloop behavior under pooling; and release timing at high attack speed. Tasks 1–4 above own these checks.
