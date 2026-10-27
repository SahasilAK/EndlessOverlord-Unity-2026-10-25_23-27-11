# Reference-led art direction

## Visual target

Detailed dark-fantasy pixel sprites: readable anatomy, angular clusters, dark contours, selective highlights, layered cloth and armor, saturated magic against muted bodies. Characters are upright, frontal or slightly turned, rather than tiny top-down chibi figures. Idle sheets show four poses; action strips have many poses. Match costume and materials, not incidental green fringes, opaque backgrounds or repeated frames.

Lich Lord: ivory skeleton, exposed ribs, deep teal torn hood and robes, dark open crown with cyan inlays, crooked ivory staff on screen-right with a cyan diamond crystal. Demon Monarch: charcoal horned plate, crimson trims, orange fissures and flaming sword. Vampire Queen: pale face, ornate burgundy gown and crown, red floating gems. Shadow Reaper: purple hood, skull, long violet scythe. Cursed Golem: broad slate slabs, moss, green luminous cracks. These supersede the old purple/green Lich and amber Golem prescriptions.

## Working resolution (pilot decision)

Preserve 512/1024 masters. Compare 128x128 and 256x256 final character cells; start with 128 to retain costume detail. Nearest resampling is a preview, not automatic pixel-art cleanup. Clean clusters and thin features after choosing the size. All frames use a common scale and origin: no per-frame auto-fit. Leave room for crown, staff and attacks.

Proposed export: characters 128 square, large bosses 192/256, equipment icons 64, projectiles 64, large effects 128/256, environment tiles 64. Confirm with the camera before bulk generation. Set PPU from desired world footprint; current actors also apply transform scaling. A 128px canvas at 128 PPU is one world unit before transforms. Preserve collider sizes and attack ranges.

## Animation contract

One horizontal PNG strip per state, left to right. Sidecar JSON records cell size, pivot, fps, loop, frame count, release/contact frame, facing and sockets. Current Unity code does NOT consume this contract yet.

| State | Frames | FPS | Loop | Motion |
|---|---:|---:|---|---|
| idle | 4 | 6 | yes | breathing/cloth, stable feet |
| run | 8 | 12 | yes | contact/down/passing/up on both legs |
| attack | 8 | 12 | no | anticipation, strike, follow-through, recovery |
| cast | 8 | 12 | no | gather, aim, release, recovery |
| hurt | 3 | 12 | no | recoil/settle; flash is runtime tint |
| death | 8 | 10 | no | collapse/dissolve, hold final frame |
| spawn | 6 | 10 | no | summoned appearance into idle |
| dash | 4 | 16 | no | lean and trailing cloth |

Pilot: four idle frames, then cast and run after approval. Produce only states supported by each actor's gameplay; add cast for casters and spawn for summons. Separate muzzle, travel and impact animations. Separate telegraph, active hit and dissipate effects. Do not bake large effects into character sheets.

Facing: pilot frontal/slight right turn; assess flipX in arena. Mirroring swaps staff/shield handedness. Author left/right sets if that is unacceptable. Up/down sets are a later production decision.

## Acceptance

Compare silhouette, face, costume, palette and pixel scale to reference. Scrub frames for foot sliding, changing crown/ribs/staff, jitter and flicker. Check alpha on black, white and gray. Do not delete every dark pixel to remove a background. Require readability at actual camera size, coherent contact/release timing, no clipping and a clean loop seam. Record feedback before scaling up.
