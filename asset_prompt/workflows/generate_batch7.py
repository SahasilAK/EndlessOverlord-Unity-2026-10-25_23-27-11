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
        "9": {"inputs": {"filename_prefix": prefix, "images": ["8", 0]}, "class_type": "SaveImage"},
        "10": {"inputs": {"filename_prefix": f"{prefix}_master", "images": ["7", 0]}, "class_type": "SaveImage"}
    }

def build_img2img_graph(image_filename, prompt_text, prefix, seed, scale_w=32, scale_h=32, denoise=0.42):
    return {
        "1": {"inputs": {"ckpt_name": "RetroDiffusionModel.safetensors"}, "class_type": "CheckpointLoaderSimple"},
        "2": {
            "inputs": {
                "lora_name": "lcm-lora-sdv1-5.safetensors",
                "strength_model": 1.0, "strength_clip": 1.0, "model": ["1", 0], "clip": ["1", 1]
            },
            "class_type": "LoraLoader"
        },
        "3": {"inputs": {"image": image_filename, "upload": "image"}, "class_type": "LoadImage"},
        "4": {"inputs": {"text": prompt_text, "clip": ["2", 1]}, "class_type": "CLIPTextEncode"},
        "5": {"inputs": {"text": "blurry, anti-aliasing, realistic, 3d render, smooth shading, gradient, multiple characters, cropped, text", "clip": ["2", 1]}, "class_type": "CLIPTextEncode"},
        "11": {"inputs": {"pixels": ["3", 0], "vae": ["1", 2]}, "class_type": "VAEEncode"},
        "6": {
            "inputs": {
                "seed": seed, "steps": 8, "cfg": 1.8, "sampler_name": "lcm", "scheduler": "sgm_uniform",
                "denoise": denoise, "model": ["2", 0], "positive": ["4", 0], "negative": ["5", 0], "latent_image": ["11", 0]
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

def generate_and_stitch_character(name_id, base_prompt, anim_prompts, out_path, fw=32, fh=32):
    if out_path.exists() and out_path.stat().st_size > 1000:
        print(f"Skipping {name_id}: Already exists at {out_path}")
        return

    print(f"\n==========================================")
    print(f"GENERATING: {name_id} ({fw}x{fh})")
    print(f"==========================================")

    g1 = build_txt2img_graph(base_prompt, f"{name_id}_F01", seed=42, scale_w=fw, scale_h=fh)
    res1 = queue_prompt(g1)
    hist1 = wait_for_completion(res1["prompt_id"])
    
    f1_img_name = hist1["outputs"]["9"]["images"][0]["filename"]
    f1_master_name = hist1["outputs"]["10"]["images"][0]["filename"]
    
    f1_img_path = OUTPUT_DIR / f1_img_name
    f1_master_path = OUTPUT_DIR / f1_master_name
    
    dest_master = INPUT_DIR / f"{name_id}_master.png"
    shutil.copyfile(f1_master_path, dest_master)
    
    frame_files = [f1_img_path]
    time.sleep(2.5)

    for idx, (frame_key, prompt_txt) in enumerate(anim_prompts, 2):
        print(f"  -> Frame {idx:02d}: {frame_key}...")
        g = build_img2img_graph(f"{name_id}_master.png", prompt_txt, f"{name_id}_F{idx:02d}", seed=42 + idx * 23, scale_w=fw, scale_h=fh)
        res = queue_prompt(g)
        hist = wait_for_completion(res["prompt_id"])
        out_name = hist["outputs"]["9"]["images"][0]["filename"]
        frame_files.append(OUTPUT_DIR / out_name)
        time.sleep(2.5)

    processed = []
    for fp in frame_files:
        im = Image.open(fp)
        im = remove_solid_bg(im)
        if im.size != (fw, fh):
            im = im.resize((fw, fh), Image.Resampling.NEAREST)
        im = enforce_outline(im)
        processed.append(im)

    sheet = Image.new("RGBA", (fw * len(processed), fh), (0, 0, 0, 0))
    for i, fr in enumerate(processed):
        sheet.paste(fr, (i * fw, 0))

    out_path.parent.mkdir(parents=True, exist_ok=True)
    sheet.save(out_path, "PNG")
    print(f"SAVED SPRITE SHEET: {out_path} ({sheet.width}x{sheet.height} px)")

def generate_tile(name_id, prompt_text, out_path, w=32, h=32):
    if out_path.exists() and out_path.stat().st_size > 300:
        print(f"Skipping {name_id}: Already exists at {out_path}")
        return

    print(f"\n==========================================")
    print(f"GENERATING TILE/VFX: {name_id} ({w}x{h})")
    print(f"==========================================")

    g = build_txt2img_graph(prompt_text, f"{name_id}_art", seed=404, scale_w=w, scale_h=h)
    res = queue_prompt(g)
    hist = wait_for_completion(res["prompt_id"])
    out_name = hist["outputs"]["9"]["images"][0]["filename"]
    
    im = Image.open(OUTPUT_DIR / out_name)
    im = remove_solid_bg(im)
    if im.size != (w, h):
        im = im.resize((w, h), Image.Resampling.NEAREST)
    im = enforce_outline(im)
    out_path.parent.mkdir(parents=True, exist_ok=True)
    im.save(out_path, "PNG")
    print(f"SAVED: {out_path} ({im.width}x{im.height} px)")
    time.sleep(2.5)

# ==============================================================================
# BATCH 7 DEFINITIONS: Final Invaders (28-30), Biome 2 & 3 Tilesets, Remaining VFX
# ==============================================================================

# 1. Royal Champion (32x32)
champion_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Royal Champion Invader, supreme warrior in gilded golden dragon plate armor #F39C12, crimson king tabard, radiant sovereign broadsword, solid flat dark background #110E1B"
champion_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Royal Champion. Resolute heavy stride frame 1, broadsword drawn, crimson tabard swaying, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Royal Champion. Resolute stride frame 2, gilded armor gleaming, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Royal Champion. Overhead sweeping royal blade slash unleashing bright gold-white sun arc #F39C12, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Royal Champion. Gilded plate deflects impact with sparks, firm recovery, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Royal Champion. Falls to one knee, sword planted in dungeon stone, soul essence ascending, solid flat dark background #110E1B")
]

# 2. Dragon Hunter (32x32)
dh_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Dragon Hunter Invader, dragonslayer in crimson wyrm-scale armor #C0392B, draconic horn helmet, colossal dragon-bone lance, solid flat dark background #110E1B"
dh_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dragon Hunter. Wyrm-stalking stride frame 1, colossal bone lance gripped two-handed, scale armor clattering, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dragon Hunter. Wyrm-stalking stride frame 2, lance level forward, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dragon Hunter. Piercing dragon-lance impale with fiery crimson-orange thrust shockwave #C0392B, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dragon Hunter. Wyrm scales absorb strike with fiery sparks, defensive stagger, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dragon Hunter. Collapses face first, dragon-bone lance shattering upon dungeon stones, solid flat dark background #110E1B")
]

# 3. Shadow Duelist (32x32)
duelist_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Shadow Duelist Invader, graceful void fencer in obsidian leather coat #2C3E50, violet energy rapier, phantom mist swirling at boots, solid flat dark background #110E1B"
duelist_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shadow Duelist. Elegant fencing advance frame 1, void rapier poised forward, obsidian coat fluttering, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shadow Duelist. Fencing advance frame 2, swift agile footwork, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shadow Duelist. Blinding void-thrust barrage with multiple violet trail afterimages #8E44AD, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shadow Duelist. Shadow form flickers translucent upon hit, acrobatic backstep, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shadow Duelist. Dissipates into shadow mist, void rapier dissolving into ethereal sparks, solid flat dark background #110E1B")
]

# Remaining VFX
hazard_prompt = "16bitscene, hazard pool, masterpiece 2D pixel art circular bubbling necrotic acid slime pool #27AE60 with dissolving bubbles and toxic edge, 1px solid dark outline, crisp 32x32"
barrier_prompt = "16bitscene, shield visual, masterpiece 2D pixel art spherical magical energy barrier shield bubble in translucent turquoise #1ABC9C with hexagonal energy lattice lines and specular rim glint, crisp 32x32"

# Biome 2: Infernal Citadel
b2_floor = "16bitscene, floor tile, masterpiece top-down 2D pixel art dungeon floor of Infernal Citadel, dark volcanic obsidian basalt cobblestones #3A1F1D with glowing orange magma seams #D35400, perfectly tileable 32x32"
b2_wall = "16bitscene, wall tile, masterpiece top-down 2D pixel art perimeter fortress wall of Infernal Citadel, volcanic dark stone masonry #4A2328 with iron spikes and molten mortar, 1px dark outline, crisp 32x32"
b2_pillar = "16bitscene, obstacle tile, masterpiece top-down 2D pixel art crypt obstacle of Infernal Citadel, brass gargoyle statue on jagged volcanic spire #78281F, 1px dark outline, crisp 32x32"
b2_portal = "16bitscene, portal sprite, masterpiece top-down 2D pixel art invader rift portal of Infernal Citadel, roaring infernal hellfire rift gate in blazing orange-red #FF5722, 1px dark outline, crisp 48x48"

# Biome 3: Frozen Cathedral
b3_floor = "16bitscene, floor tile, masterpiece top-down 2D pixel art dungeon floor of Frozen Cathedral, glazed azure ice cathedral marble flags #1C2833 with frosted blue veins #2980B9, perfectly tileable 32x32"
b3_wall = "16bitscene, wall tile, masterpiece top-down 2D pixel art perimeter wall of Frozen Cathedral, frost-rimed gothic cathedral masonry #2C3E50 with icicles, 1px dark outline, crisp 32x32"
b3_pillar = "16bitscene, obstacle tile, masterpiece top-down 2D pixel art cathedral obstacle of Frozen Cathedral, crystalline glacial ice spire #5DADE2, 1px dark outline, crisp 32x32"
b3_portal = "16bitscene, portal sprite, masterpiece top-down 2D pixel art invader rift portal of Frozen Cathedral, swirling blizzard vortex gate in crystalline cyan-white #AED6F1, 1px dark outline, crisp 48x48"

if __name__ == "__main__":
    art_invaders = PROJECT_ROOT / "Assets/_Game/Art/Characters/Invaders"
    art_vfx = PROJECT_ROOT / "Assets/_Game/Art/VFX"
    art_env = PROJECT_ROOT / "Assets/_Game/Art/Environment/Dungeons"

    # 1. Remaining VFX
    generate_tile("vfx_hazard_pool", hazard_prompt, art_vfx / "vfx_hazard_pool.png", 32, 32)
    generate_tile("vfx_barrier_shield", barrier_prompt, art_vfx / "vfx_barrier_shield.png", 32, 32)

    # 2. Final 3 Invaders (Completes all 30 Invaders!)
    generate_and_stitch_character("inv_28_royal_champion", champion_base, champion_anims, art_invaders / "inv_28_royal_champion_sheet.png", 32, 32)
    generate_and_stitch_character("inv_29_dragon_hunter", dh_base, dh_anims, art_invaders / "inv_29_dragon_hunter_sheet.png", 32, 32)
    generate_and_stitch_character("inv_30_shadow_duelist", duelist_base, duelist_anims, art_invaders / "inv_30_shadow_duelist_sheet.png", 32, 32)

    # 3. Biome 2 Tiles (Infernal Citadel)
    generate_tile("biome_02_floor", b2_floor, art_env / "biome_02_floor.png", 32, 32)
    generate_tile("biome_02_wall", b2_wall, art_env / "biome_02_wall.png", 32, 32)
    generate_tile("biome_02_pillar", b2_pillar, art_env / "biome_02_pillar.png", 32, 32)
    generate_tile("biome_02_portal", b2_portal, art_env / "biome_02_portal.png", 48, 48)

    # 4. Biome 3 Tiles (Frozen Cathedral)
    generate_tile("biome_03_floor", b3_floor, art_env / "biome_03_floor.png", 32, 32)
    generate_tile("biome_03_wall", b3_wall, art_env / "biome_03_wall.png", 32, 32)
    generate_tile("biome_03_pillar", b3_pillar, art_env / "biome_03_pillar.png", 32, 32)
    generate_tile("biome_03_portal", b3_portal, art_env / "biome_03_portal.png", 48, 48)

    print("\n>>> BATCH 7 GENERATION COMPLETE! <<<")
