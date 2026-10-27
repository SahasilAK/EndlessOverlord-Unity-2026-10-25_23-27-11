# Art generation restart

The Unity base game already exists. This phase replaces its art and fills animation gaps using Stability Matrix's ComfyUI. Visual authority: `docs/old_assets/images`, not the previous 32x32 prompts. `asset_prompt` is retired as `asset_prompt.retired` (recursive deletion was blocked). Audio generation is outside this replacement package.

Start with **[TEST_ONE_CHARACTER.md](TEST_ONE_CHARACTER.md)**. Do not batch the roster until the Lich Lord pilot passes your visual review.

- [Art direction and animation contract](ART_DIRECTION.md)
- [Replacement and integration plan](REPLACEMENT_PLAN.md)
- [Pipeline audit](PIPELINE_AUDIT.md)
- [Character prompts](prompts/characters.md)
- [Weapons, effects and projectiles](prompts/asset_families.md)
- `prompts/catalog_prompts.json`: individual briefs from current C# catalogs, with IDs and source paths.
- `inventory.csv`: existing PNGs, literal runtime references and legacy census tags. Existence is not quality approval.
- `reference_inventory.csv`: reference dimensions and transparency.
- `workflows/*.json`: ComfyUI editor graphs; `*_api.json`: equivalent API prompts.

Generation produces candidates. Seeds and img2img do not guarantee identity, exact palette, alpha or valid animation. Review results at game scale. Raw outputs stay outside `Assets` until approved.
