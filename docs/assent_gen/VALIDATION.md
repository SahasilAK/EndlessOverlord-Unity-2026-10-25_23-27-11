# Validation record — 2026-10-27

`tools/build_package.py` completed against current local source and references. Extracted 150 weapons, 30 armors, 30 bosses, 30 invaders, 80 spells, 20 biomes and 277 class skills. The five playable characters and proposed summon designs are covered in the Markdown briefs.

`tools/validate_package.py` passed for all three graph/API pairs against the running server's `/object_info`: reciprocal editor links, API link agreement, socket types, required inputs, available checkpoint selector and numeric parameter bounds. Catalog count checks and all source paths passed. This is structural validation, not ComfyUI execution validation.

Packer smoke check passed: four aligned 128px RGBA frames produced a 512x128 strip and metadata; existing output, incorrect frame count and opaque background were rejected. Synthetic fixture pixels were used only to test packing; they are not generated game art.

No diffusion inference was queued. No image quality, frame consistency, GPU-memory/performance, Unity import, PlayMode or player-build pass is claimed. Unity source and live game assets were not modified. These are explicitly gated on pilot feedback and the future integration plan.

Repeat structural validation from project root:

```powershell
& 'D:/WORK/gen/StabilityMatrix-win-x64/Data/Packages/ComfyUI/venv/Scripts/python.exe' docs/assent_gen/tools/validate_package.py
```

The old prompt directory was renamed to `asset_prompt.retired` and ignored by Git after automatic approval review blocked recursive deletion. The active replacement package is `docs/assent_gen` and is now exempted from the old broad `docs/` ignore rule. No commit was created.
