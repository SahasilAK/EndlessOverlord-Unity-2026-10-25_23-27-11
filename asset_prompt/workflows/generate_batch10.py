import json
import urllib.request
import time
import os
import shutil
from pathlib import Path
from PIL import Image

COMFY_URL = "http://127.0.0.1:8188"
PROJECT_ROOT = Path(r"d:\WORK\AI\Games\EndlessOverlord\My project")
OUTPUT_DIR = Path(r"D:\WORK\gen\StabilityMatrix-win-x64\Data\Packages\ComfyUI\output")
INPUT_DIR = Path(r"D:\WORK\gen\StabilityMatrix-win-x64\Data\Packages\ComfyUI\input")
OUTLINE_RGBA = (17, 14, 27, 255)

INPUT_DIR.mkdir(parents=True, exist_ok=True)

def queue_prompt(prompt_workflow):
    data = json.dumps({"prompt": prompt_workflow}).encode('utf-8')
    req = urllib.request.Request(f"{COMFY_URL}/prompt", data=data, headers={'Content-Type': 'application/json'})
    with urllib.request.urlopen(req) as response:
        return json.loads(response.read().decode('utf-8'))

def get_history(prompt_id):
    with urllib.request.urlopen(f"{COMFY_URL}/history/{prompt_id}") as response:
        return json.loads(response.read().decode('utf-8'))

def wait_for_completion(prompt_id):
    while True:
        hist = get_history(prompt_id)
        if prompt_id in hist:
            return hist[prompt_id]
        time.sleep(0.8)

def remove_solid_bg(img: Image.Image, tolerance: int = 40) -> Image.Image:
    img = img.convert("RGBA")
    w, h = img.size
    px = img.load()
    bg_r, bg_g, bg_b, _ = px[0, 0]
    visited = set()
    stack = [(0, 0), (w - 1, 0), (0, h - 1), (w - 1, h - 1)]
    while stack:
        x, y = stack.pop()
        if (x, y) in visited or x < 0 or y < 0 or x >= w or y >= h:
            continue
        visited.add((x, y))
        r, g, b, a = px[x, y]
        if a == 0 or (abs(r - bg_r) <= tolerance and abs(g - bg_g) <= tolerance and abs(b - bg_b) <= tolerance):
            px[x, y] = (0, 0, 0, 0)
            stack.extend([(x + 1, y), (x - 1, y), (x, y + 1), (x, y - 1)])
    return img

def enforce_outline(img: Image.Image) -> Image.Image:
    w, h = img.size
    src = img.load()
    out = img.copy()
    dst = out.load()
    for y in range(h):
        for x in range(w):
            if src[x, y][3] == 0:
                for nx, ny in ((x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1)):
                    if 0 <= nx < w and 0 <= ny < h and src[nx, ny][3] > 128:
                        dst[x, y] = OUTLINE_RGBA
                        break
    return out

def build_txt2img_graph(prompt_text, prefix, seed=42, width=512, height=512, scale_w=32, scale_h=32):
    return {
        "1": {"inputs": {"ckpt_name": "RetroDiffusionModel.safetensors"}, "class_type": "CheckpointLoaderSimple"},
        "2": {
            "inputs": {
                "lora_name": "lcm-lora-sdv1-5.safetensors",
                "strength_model": 1.0,
                "strength_clip": 1.0,
                "model": ["1", 0],
                "clip": ["1", 1]
            },
            "class_type": "LoraLoader"
        },
        "3": {"inputs": {"width": width, "height": height, "batch_size": 1}, "class_type": "EmptyLatentImage"},
        "4": {"inputs": {"text": prompt_text, "clip": ["2", 1]}, "class_type": "CLIPTextEncode"},
        "5": {"inputs": {"text": "blurry, anti-aliasing, realistic, 3d render, smooth shading, gradient, multiple characters, cropped, text", "clip": ["2", 1]}, "class_type": "CLIPTextEncode"},
        "6": {
            "inputs": {
                "seed": seed, "steps": 6, "cfg": 1.8, "sampler_name": "lcm", "scheduler": "sgm_uniform",
                "denoise": 1.0, "model": ["2", 0], "positive": ["4", 0], "negative": ["5", 0], "latent_image": ["3", 0]
            },
            "class_type": "KSampler"
        },
        "7": {"inputs": {"samples": ["6", 0], "vae": ["1", 2]}, "class_type": "VAEDecode"},
        "8": {
            "inputs": {"upscale_method": "nearest-exact", "width": scale_w, "height": scale_h, "crop": "disabled", "image": ["7", 0]},
            "class_type": "ImageScale"
        },
        "9": {"inputs": {"filename_prefix": prefix, "images": ["8", 0]}, "class_type": "SaveImage"}
    }

def generate_ui_element(name_id, prompt_text, out_path, w=32, h=32, latent_w=512, latent_h=512, enforce_rim=True):
    if out_path.exists() and out_path.stat().st_size > 300:
        print(f"Skipping {name_id}: Already exists at {out_path}")
        return

    print(f"\n==========================================")
    print(f"GENERATING UI: {name_id} ({w}x{h})")
    print(f"==========================================")

    g = build_txt2img_graph(prompt_text, f"{name_id}_ui", seed=606, width=latent_w, height=latent_h, scale_w=w, scale_h=h)
    res = queue_prompt(g)
    hist = wait_for_completion(res["prompt_id"])
    out_name = hist["outputs"]["9"]["images"][0]["filename"]
    
    im = Image.open(OUTPUT_DIR / out_name)
    im = remove_solid_bg(im)
    if im.size != (w, h):
        im = im.resize((w, h), Image.Resampling.NEAREST)
    if enforce_rim:
        im = enforce_outline(im)
    out_path.parent.mkdir(parents=True, exist_ok=True)
    im.save(out_path, "PNG")
    print(f"SAVED: {out_path} ({im.width}x{im.height} px)")
    time.sleep(2.5)

# ==============================================================================
# BATCH 10 DEFINITIONS: UI HUD Frames, Skill Badges & Game Title Logo
# ==============================================================================

ui_items = [
    # 1. Game Title Logo Crest (320x96)
    ("ui_logo_title_crest", "Assets/_Game/Art/UI/Frames/ui_logo_title_crest.png", 
     "16bitscene, title logo, masterpiece 2D pixel art game title logo FINAL BOSS ETERNAL DUNGEON, dark fantasy gothic typography, carved aged bone and dark iron filigree with glowing emerald runes #30E680, 1px dark outline #110E1B, crisp pixel art, transparent background", 
     320, 96, 512, 256, False),

    # 2. 9-Slice Gothic Box Frame (48x48)
    ("ui_frame_gothic_box", "Assets/_Game/Art/UI/Frames/ui_frame_gothic_box.png",
     "16bitscene, ui frame, masterpiece 2D pixel art square 9-slice dark iron and carved obsidian panel frame with spiked corner rivets and bevel edge #3D354B, dark center #1A1624, 1px dark outline #110E1B, crisp 48x48",
     48, 48, 512, 512, True),

    # 3. Health Bar Frame (160x24)
    ("ui_hud_health_bar_frame", "Assets/_Game/Art/UI/HUD/ui_hud_health_bar_frame.png",
     "16bitscene, ui frame, masterpiece 2D pixel art horizontal health bar container frame, gothic dark-iron border with carved bone skull crest at left end, hollow central chamber, 1px dark outline #110E1B, crisp 160x24",
     160, 24, 512, 256, True),

    # 4. Crimson Bar Fill (16x16)
    ("ui_hud_bar_fill_crimson", "Assets/_Game/Art/UI/HUD/ui_hud_bar_fill_crimson.png",
     "16bitscene, ui bar, masterpiece 2D pixel art horizontal health bar fill texture, glowing molten blood crimson #D91A4D with subtle diagonal highlight stripes and specular shine, crisp 16x16",
     16, 16, 512, 512, False),

    # 5. Emerald Bar Fill (16x16)
    ("ui_hud_bar_fill_emerald", "Assets/_Game/Art/UI/HUD/ui_hud_bar_fill_emerald.png",
     "16bitscene, ui bar, masterpiece 2D pixel art horizontal mana soul resource bar fill texture, radiant luminous emerald #30E680 with mystic soul swirls and bright edge, crisp 16x16",
     16, 16, 512, 512, False),

    # 6. Ability Hotbar Slot Frame (32x32)
    ("ui_hud_hotbar_slot_frame", "Assets/_Game/Art/UI/HUD/ui_hud_hotbar_slot_frame.png",
     "16bitscene, ui slot, masterpiece 2D pixel art square ability hotbar slot button frame, weathered dark steel with runic corner braces and recessed dark socket #1A1624, 1px dark outline #110E1B, crisp 32x32",
     32, 32, 512, 512, True),

    # 7. Skill Node - Locked (28x28)
    ("ui_skill_node_locked", "Assets/_Game/Art/UI/Badges/ui_skill_node_locked.png",
     "16bitscene, ui icon, masterpiece 2D pixel art diamond skill node icon, locked weathered dark iron padlock #3A3542 with heavy chains, 1px dark outline #110E1B, crisp 28x28",
     28, 28, 512, 512, True),

    # 8. Skill Node - Available (28x28)
    ("ui_skill_node_available", "Assets/_Game/Art/UI/Badges/ui_skill_node_available.png",
     "16bitscene, ui icon, masterpiece 2D pixel art diamond skill node icon, unlockable glowing runic eye socket pulsing with bright emerald soulfire #30E680, 1px dark outline #110E1B, crisp 28x28",
     28, 28, 512, 512, True),

    # 9. Skill Node - Active (28x28)
    ("ui_skill_node_active", "Assets/_Game/Art/UI/Badges/ui_skill_node_active.png",
     "16bitscene, ui icon, masterpiece 2D pixel art diamond skill node icon, active rank royal amethyst gem #9B59B6 set in silver filigree with radiant energy facets, 1px dark outline #110E1B, crisp 28x28",
     28, 28, 512, 512, True),

    # 10. Skill Node - Mastered (28x28)
    ("ui_skill_node_mastered", "Assets/_Game/Art/UI/Badges/ui_skill_node_mastered.png",
     "16bitscene, ui icon, masterpiece 2D pixel art diamond skill node icon, mastered sovereign golden dragon crest #F1C40F gleaming with blazing divine stars, 1px dark outline #110E1B, crisp 28x28",
     28, 28, 512, 512, True),

    # 11. Button - Normal (96x24)
    ("ui_button_normal", "Assets/_Game/Art/UI/Frames/ui_button_normal.png",
     "16bitscene, ui button, masterpiece 2D pixel art horizontal gothic dark-iron button plate with bronze rivets and dark obsidian bevel, 1px dark outline #110E1B, crisp 96x24",
     96, 24, 512, 256, True),

    # 12. Button - Hover (96x24)
    ("ui_button_hover", "Assets/_Game/Art/UI/Frames/ui_button_hover.png",
     "16bitscene, ui button, masterpiece 2D pixel art horizontal gothic dark-iron button plate highlighted with glowing emerald runic trim #30E680, 1px dark outline #110E1B, crisp 96x24",
     96, 24, 512, 256, True)
]

def main():
    print(">>> STARTING BATCH 10 ASSET GENERATION: UI HUD & FRAMES <<<")
    for name_id, rel_path, prompt, w, h, lw, lh, rim in ui_items:
        dest_path = PROJECT_ROOT / rel_path
        generate_ui_element(name_id, prompt, dest_path, w, h, lw, lh, rim)
    print("\n>>> BATCH 10 COMPLETE: ALL UI ASSETS GENERATED AND SAVED <<<")

if __name__ == "__main__":
    main()
