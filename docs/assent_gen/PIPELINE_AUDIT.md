# Pipeline audit — 2026-10-27

## Verified locally

- Stability Matrix: `D:/WORK/gen/StabilityMatrix-win-x64`.
- ComfyUI: `Data/Packages/ComfyUI`, running at `http://127.0.0.1:8188`.
- Python: `Data/Packages/ComfyUI/venv/Scripts/python.exe`; Pillow import succeeded.
- New editor graphs installed under `Data/Packages/ComfyUI/user/default/workflows/EndlessOverlord_v2`. The reference crop was copied to `ComfyUI/input/lich_lord_reference.png`. Workflow 02 deliberately requires uploading an authored pose guide.
- `/object_info` lists SDXL base `sd_xl_base_1.0_0.9vae.safetensors`, SD1.5, `RetroDiffusionModel.safetensors`, and an offset example file in the checkpoint selector. A selector entry is not proof of compatible model architecture; the offset LoRA must not be treated as a checkpoint.
- Available LoRA: `lcm-lora-sdv1-5.safetensors`. New graphs do not mix it into SDXL.
- IP-Adapter nodes and `ip-adapter-plus_sd15.safetensors` exist. CLIPVisionLoader has no encoder choices. No working IP-Adapter identity path is claimed.
- ControlNet choices are SD1.5 tile and OpenPose. They are not compatible additions to the new SDXL graph.
- Existing reference idle strips are 2048x512 (four square cells); many actions are 8192x512 (16). Inspect whether frames repeat; dimensions do not prove animation quality.
- 121 PNG files were present under `Assets/_Game/Art` at audit time. These are replacement candidates, not approved finished art.

## Problems in retired pipeline

Six slots mixed unrelated states, with only two walk poses and single cast/death poses. Automatic 32px downscaling discarded detail. Prompt palette did not match reference. Animation graph had img2img but no IP-Adapter despite documentation describing identity locking. Denoise was incorrectly described as preserving a percentage of exact pixels. Timing, VRAM and consistency claims were not measured guarantees.

## New workflow boundaries

01: reference crop -> 1024 nearest enlargement -> SDXL VAE -> partial denoise 0.28 -> full master plus 128 review preview.

02: approved master edited into a pose guide -> same SDXL path at denoise 0.22. This is assisted frame cleanup; the pose guide supplies motion. Repeated prompts on an unchanged idle image are not a dependable walk-cycle generator.

03: SDXL text-to-image for individual item/effect concepts, then approved concepts feed 02 for variations. Use family prompts and catalog identities.

All use batch 1, fixed seed 271027, 30 steps, CFG 5.5, dpmpp_2m/karras. These are starting parameters to test, not quality guarantees. No LoRA/download/custom-node dependency. Background remains RGB until deliberate alpha cleanup. No automatic import or runtime replacement.

If pilot fails style fidelity, first adjust source framing/denoise; then evaluate a compatible pixel-art SDXL LoRA with documented provenance. If pose control is insufficient, add a separately validated matching SDXL pose/lineart control model and compatible IP-Adapter/vision encoder. Do not silently substitute the installed SD1.5 pair.

## Sources

- Installed ComfyUI `/object_info`, `nodes.py`, `execution.py`, and installed `ComfyUI_IPAdapter_plus` source are the compatibility authority for this machine.
- [ComfyUI img2img explanation](https://github.com/Comfy-Org/workflow_templates/blob/main/site/knowledge/concepts/img2img.md): partial denoise conditions generation on an encoded source image; it does not guarantee animation identity.

Unity findings and exact source files are in `REPLACEMENT_PLAN.md`. Catalog code takes precedence over older prose coverage reports, which name a different legendary boss roster.
