# Character prompts

Workflow 01 starts from the matching reference crop. Workflow 02 starts from a pose-edited copy of the approved master, at the SAME scale and origin. Prompt text cannot replace the image input. Keep checkpoint, seed, camera and palette stable.

## Shared positive prefix

Detailed dark fantasy pixel sprite, one full-body character, upright frontal three-quarter presentation, deliberate angular pixel clusters, clear dark contour, stepped material shading, restrained highlights, readable limbs and silhouette, light from upper left, centered with margin around the entire figure, isolated on a uniform neutral gray background, no ground shadow.

## Identity suffixes

- **lich_lord:** ivory skeletal monarch, exposed rib cage, deep teal tattered hood and layered robes, dark open crown with cyan inlays, cyan eyes, crooked ivory staff on viewer's right with faceted cyan diamond crystal, thin bones, severe expression. No purple robes or green skull lantern.
- **demon_monarch:** massive horned monarch in charcoal segmented plate, crimson trim, dark cape, orange visor and molten chest fissures, broad flaming sword and heavy boots.
- **vampire_queen:** pale regal vampire, black hair, ornate burgundy crown, layered crimson velvet gown, fitted bodice, long sleeves, floating red crystal droplets above open hands.
- **shadow_reaper:** ivory skull beneath a deep purple hood, ragged violet layered cloak, skeletal hands, long purple scythe shaft and curved luminous lilac blade.
- **cursed_golem:** massive squat humanoid built from slate slabs, square shoulders, huge fists, moss in crevices, emerald cracks and green eyes, heavy stone feet.

Preserve the matching reference silhouette, material shapes and proportions.

## Negative prompt

photograph, photorealistic, 3d render, painterly brushwork, smooth airbrush shading, chibi, tiny simplified icon, blurry outline, inconsistent pixel sizes, excessive bloom, grain, text, watermark, labels, frame borders, sprite sheet, duplicate character, extra limbs, extra weapon, missing hands, cropped crown, cropped staff, cropped feet, environment, ground plane, checkerboard background

## Lich pilot complete positive prompt

Detailed dark fantasy pixel sprite of one full-body ivory skeletal monarch, exposed ribs, deep teal tattered hood and layered robes, dark open crown with cyan inlays, cyan eyes, crooked ivory staff on the viewer's right with a faceted cyan diamond crystal. Upright frontal stance matching the reference, calm idle pose, feet planted, readable thin bones, angular pixel clusters, dark contour, stepped teal cloth shading and restrained cyan highlights, light from upper left, entire crown staff and feet visible, centered with generous margin on a uniform neutral gray background, no ground shadow.

## Pose suffixes for workflow 02

Append ONE suffix to the full identity prompt. Edit the approved master into that rough pose first. Do not recursively generate each frame from the preceding output: return to the same approved identity master.

| Frame | Pose suffix / guide edit |
|---|---|
| idle/01 | neutral stance, staff upright, robe at rest |
| idle/02 | ribs rise slightly, sleeves open slightly, feet and staff tip anchored |
| idle/03 | breathing peak, crystal slightly brighter, anatomy unchanged |
| idle/04 | settle toward neutral, robe follows, transition to frame 01 |
| cast/01 | neutral ready stance |
| cast/02 | elbows inward, weight back |
| cast/03 | staff draws back, free hand gathers a cyan glint |
| cast/04 | anticipation peak, staff aimed right |
| cast/05 | release, arm extends toward target; projectile is separate |
| cast/06 | follow-through, robe follows arm |
| cast/07 | staff returns upward, body recovers |
| cast/08 | return to idle silhouette |
| run/01..08 | left contact/down/passing/up, right contact/down/passing/up |
| hurt/01..03 | impact recoil, maximum recoil, recover |
| death/01..08 | stagger, knees buckle, staff lowers, body falls, bones separate, cloth settles, crystal fades, still remains |

Other actors use the same phases adapted to their anatomy. Golem death is stone collapse; vampire death is gown collapse and blood mist; reaper death is cloak collapse and violet dispersal; demon death is armored collapse and fading embers. Never substitute a recolored Lich silhouette.
