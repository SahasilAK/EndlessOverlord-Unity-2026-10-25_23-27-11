# Weapons, projectiles, effects and supporting art

Use workflow 03 for new concepts. Once accepted, use workflow 02 with pose/shape guides for moving phases. Use the common pixel language in `characters.md`. Each catalog entry provides identity, gameplay description and required deliverables; generate one deliverable at a time, not everything described in one picture.

## Weapon forms

Create a 64px icon and a separate held/world sprite per weapon ID. Keep grip/socket origin consistent within the family. Do not replace 150 weapons with 15 category icons. Rarity borders belong to UI; the underlying icon must identify the actual weapon.

Append the catalog name/material description to one form below:

| Family | Form prompt and motion requirements |
|---|---|
| one-handed sword | distinct blade, guard, wrapped grip; diagonal view; 8-frame slash and 6-frame thrust |
| two-handed sword | long blade, extended two-hand grip, heavy guard; 8-frame heavy cleave |
| dagger | short asymmetric blade, compact guard; 6-frame stab and 6-frame quick slash |
| spear | long shaft, readable spearhead and grip point; 6-frame thrust |
| axe | readable crescent head, haft and counterweight; 8-frame chop |
| hammer | broad blunt head and reinforced shaft; 8-frame slam plus separate ground impact |
| bow | curved limbs, visible string, arrow rest; 6-frame draw/release, separate arrow |
| crossbow | stock, transverse limbs and bowstring; 6-frame fire/reload, separate bolt |
| staff | long carved shaft with distinct magical head; 8-frame cast, separate muzzle |
| wand | short tapered magical implement with tip ornament; 6-frame flick cast |
| spellbook | thick covers, spine, clasp and readable pages; 6-frame open/cast/close |
| magical orb | central sphere, material facets, contained energy; 4-frame hover and 6-frame pulse |
| scythe | long shaft, curved blade and stable hand sockets; 8-frame sweep, separate violet crescent |
| claws | paired curved talons and wrist mount, consistent finger count; 6-frame rake |
| cursed relic | distinct artifact silhouette based on catalog description; 4-frame pulse and 6-frame activation |

These motion counts describe shared animation families. Weapons with different silhouettes still need their own aligned held sprites. Attachments and wielding animation are future Unity integration, not current workflow output.

## Projectile example: Lich necrotic bolt

One isolated cyan necrotic projectile, bright ivory-cyan diamond core, teal angular flame tail tapering to the left, travels right, dark fantasy pixel clusters, hard stepped silhouette, restrained brightness, centered on uniform neutral gray, no caster, no target, no text, no ground shadow.

Generate muzzle 4 frames (gather/flare/release/fade), travel 4 frames (tail flicker while core stays centered), impact 6 frames (contact/burst/expansion/fragments/fade/clear). Travel loops; muzzle and impact do not. Pivots and impact position are explicit. Keep all travel frames the same canvas; never use an impact frame as a traveling bolt.

## Effect example: scythe slash

One isolated broad crescent of violet spectral energy, lilac-white inner cutting edge, deep purple outer band, angular trailing fragments, sweeping clockwise from upper right to lower left, sharp stepped pixel clusters, clear empty center, uniform gray background, no scythe weapon, no character, no lettering.

Eight phases: thin anticipation arc, growing arc, full cutting crescent, strike peak, stretched follow-through, broken arc, sparse fragments, extinguished. Reference `weapon_scythe_slash.png` is shape/color inspiration; its green backing is not final transparency.

## Spell behavior contracts

For every spell in `catalog_prompts.json`, append its school/material and use its actual behavior:

| Behavior | Separate deliverables |
|---|---|
| DirectProjectile / HomingSeeker | cast 4, travel 4 loop, impact 6 |
| RadialAoENova / AuraPulse | telegraph 4, expanding pulse 8, dissipate 4 |
| ContinuousBeam | startup 4, tiled middle 4 loop, source/end caps 4, shutdown 4 |
| OrbitingShield | summon 4, orb/barrier 4 loop, break 6 |
| ChainingArc | start flash 3, segment 4 loop, endpoint hit 4 |
| GroundHazard | telegraph 4, formation 6, active 6 loop, dissipate 6 |
| WaveCone | anticipation 4, directional fan 8, dissipate 4 |
| FallingImpact | floor warning 4, falling object 4, impact 8, debris 6 |

All area warnings must read beneath enemies and avoid looking identical to active damage. Generate the visual radius on a known canvas; runtime scales it to gameplay radius. School colors start from game definitions; do not recolor every school cyan. Large alpha glows need careful masks; simple chroma deletion can destroy them.

## Summons

Shared identity: ivory bone, aged iron/teal cloth, restrained cyan necromancy, readable minion scale. Each needs idle/run/attack/hurt/death plus spawn; ranged casters additionally cast. Use source actor behavior to decide which states are reachable.

- Bone Sentinel: rib cage, cracked round shield, short iron sword, protective stance.
- Crypt Archer: slender skeleton, tattered hood, aged bow, bone arrow quiver.
- Grave Spearman: tall skeleton, long spear, battered kettle helmet.
- Grave Hound: low quadruped rib cage, long skull, clear four-legged gait.
- Bone Mage: floating skeletal caster, compact teal robe, short crystal staff.
- Ossuary Guardian: heavy stacked bone plates, huge shield, broad defensive stance; verify gameplay identity before production.
- Wight Captain: armored undead officer, crested helmet, torn banner and longsword.
- Corpse Collector: hunched undead porter, hooked implement and bone bundles.
- Dread Abomination: massive asymmetrical fused bones, oversized arms, lumbering gait.
- Skeletal Dragon: long skeletal neck and tail, ribbed wings, four clear limbs; allow a larger cell and separate airborne poses if used.

These are proposed art briefs, not assertions that every census tag has a separate actor implementation.

## Other gaps

Armor: one recognizable silhouette per catalog ID, visible material difference between cloth, mail and plate. If armor is cosmetic, author overlays against approved poses; do not assume icons equip themselves visually.

Props: coin spin 6, gem pulse 4, potion glint 4, chest closed/opening 6/open, merchant idle 4/interact 4, shrines dormant/activate 6/active 4/spent. Shrine identities: blood droplet, frenzy claws, greed coins, protection shield, void ring. Preserve state silhouettes.

Biomes: floor variants, edges, corners, walls, pillars, obstacles and accents per catalog. Keep floor values quiet. Verify seamless joins in a 3x3 tile preview. Animated lava/flame/water 6-frame loops; do not animate collision geometry.

UI: dark carved stone/aged metal frames, teal-cyan restrained highlights, clear content wells, no generated text. Supply nine-slice borders plus normal/hover/pressed/disabled/selected states. Include portraits, skill icons, equipment slots, health/resource bars, reward cards, merchant slots and death/results panels. Text stays native Unity UI.
