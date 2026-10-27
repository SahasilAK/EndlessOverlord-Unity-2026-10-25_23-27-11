# Test one character: Lich Lord

This is an art review outside Unity. The existing Unity presenter still slices six 32px frames; dropping a 128px sheet over it will crop it incorrectly. Integration follows your feedback.

## 1. Open the prepared workflow

1. Launch the existing ComfyUI package from Stability Matrix. Open its browser UI (currently `http://127.0.0.1:8188`).
2. Open **EndlessOverlord_v2 / 01_Lich_Reference_Master** in ComfyUI's workflow browser (refresh if necessary). These graphs were copied into the installed package. Alternatively drag `docs/assent_gen/workflows/01_Lich_Reference_Master.json` onto the canvas. Do not load the `_api.json` version in the editor. Ignore the old Turbo workflows.
3. In **REFERENCE / POSE GUIDE**, upload `docs/assent_gen/references/lich_lord_reference.png`. This is the first reference idle cell, padded and composited over gray; it is not a newly generated asset.
4. Check checkpoint `sd_xl_base_1.0_0.9vae.safetensors`. No LoRA, ControlNet, IP-Adapter or extra download is required for this graph.
5. The positive prompt is already filled with the teal/cyan Lich identity. Keep seed **271027**, **fixed**, 30 steps, CFG 5.5, dpmpp_2m, karras, denoise **0.28**. One frame per queue.

## 2. Generate and judge the master

Click **Queue / Run** once. The two Save Image nodes write into ComfyUI's `output/EndlessOverlord/pilot/`: a 1024px master and a 128px nearest-neighbor review preview. They are RGB candidates with background, not finished transparent sprites.

Compare the generated image to the reference at equal displayed size. The correct direction is teal cloth, ivory skeleton, cyan crown/crystal, same thin body, same staff shape. Inspect at 128px without browser smoothing and at 256px (resize a copy in your pixel editor with nearest-neighbor). Keep the master intact.

If identity drifts, hold the seed and try **0.18** denoise. If it barely changes and you want a stronger redraw, try **0.35**. Do not change checkpoint, seed, CFG and denoise together. Record settings and save each candidate. An almost unchanged reference at low denoise is a useful control, not proof the pipeline can invent consistent new characters.

If the run fails, capture the error text and workflow settings. Schema validation checked the installed node/model choices, but no GPU inference or output-quality pass was claimed during preparation. Do not install arbitrary missing nodes: these graphs use core nodes only.

## 3. Four-frame idle test

After choosing the master, make four copies at exactly the same canvas size and position. In a pixel editor, rough in the four `idle` poses from `prompts/characters.md`; keep feet, staff tip, scale and crown shape consistent. For a subtle idle, frame 01 can be the master. These guides supply actual motion; an unchanged input plus different words is not a reliable animation workflow.

Load `02_Pose_Guided_Frame.json`. Upload each guide in turn (the initial filename is a placeholder until uploaded). Use the full character prompt plus that frame's pose suffix. Start denoise **0.22**, fixed seed 271027. Set both Save Image prefixes to include `lich_lord_idle_01`, then `02`, `03`, `04` before each run. Keep corresponding master and preview paired. Higher denoise may change identity; lower denoise may preserve rough edits.

Review all four together. Clean alpha and pixel clusters in your editor. Do not automatically remove dark colors: the outline and deep teal robe must survive. Work at chosen export resolution, keep identical canvases and ground anchor, and save cleaned RGBA frames as `01.png` through `04.png` under `docs/assent_gen/staging/lich_lord/idle/`.

## 4. Pack and preview

From the Unity project root, PowerShell:

```powershell
& 'D:/WORK/gen/StabilityMatrix-win-x64/Data/Packages/ComfyUI/venv/Scripts/python.exe' docs/assent_gen/tools/pack_state.py docs/assent_gen/staging/lich_lord/idle docs/assent_gen/staging/lich_lord/lich_lord_idle.png --state idle --count 4 --fps 6 --loop
```

The packer rejects opaque/empty frames, mismatched sizes, wrong count and existing output filenames. It does not resize, align, mask, or improve images. The accompanying JSON is a proposed future import contract. If choosing a different foot anchor, supply `--pivot X Y` normalized to the cell (Unity bottom-left convention).

Preview the four frames at 6 FPS in your sprite editor on dark, white and gray backgrounds. Require a smooth loop, stable staff/crown/ribs, no foot sliding and no visible background fringe. Only then extend to run/cast. Do not batch other characters yet.

## 5. Feedback to send

Send the reference comparison, chosen master, 128/256 comparison and four-frame preview, plus seed/denoise. Describe which of silhouette, detail, palette, pixel scale or motion is wrong. Fill `pilot_feedback.md`; your decision determines the next workflow revision.
