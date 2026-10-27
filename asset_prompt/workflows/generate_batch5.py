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

def generate_prop(name_id, prompt_text, out_path, w=32, h=32):
    if out_path.exists() and out_path.stat().st_size > 300:
        print(f"Skipping {name_id}: Already exists at {out_path}")
        return

    print(f"\n==========================================")
    print(f"GENERATING PROP: {name_id} ({w}x{h})")
    print(f"==========================================")

    g = build_txt2img_graph(prompt_text, f"{name_id}_prop", seed=202, scale_w=w, scale_h=h)
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
    print(f"SAVED PROP: {out_path} ({im.width}x{im.height} px)")
    time.sleep(2.5)

# ==============================================================================
# BATCH 5 DEFINITIONS: Invaders 17-24 & In-World Props / Pickups
# ==============================================================================

# 1. Monster Hunter (32x32)
mh_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Monster Hunter Invader, rugged beast slayer in fur-lined dark leather #A04000, horned skull shoulder pauldron, barbed iron harpoon and serrated cleaver, solid flat dark background #110E1B"
mh_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Monster Hunter. Tracking step frame 1, harpoon resting on shoulder, furs swaying, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Monster Hunter. Tracking step frame 2, heavy boots stepping quietly, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Monster Hunter. Powerful lunging harpoon impale with sharp red-orange action smear #A04000, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Monster Hunter. Skull pauldron deflects strike, defensive recoil, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Monster Hunter. Collapsed face down, harpoon clattering to stone floor, solid flat dark background #110E1B")
]

# 2. Ranger (32x32)
ranger_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Ranger Invader, wilderness scout in forest-green tunic #27AE60, leaf-patterned hooded cloak, recurve yew bow and twin hunting daggers at belt, solid flat dark background #110E1B"
ranger_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ranger. Silent wilderness stride frame 1, bow held low, green cloak fluttering, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ranger. Silent stride frame 2, agile foot placement, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ranger. Rapid double-shot loose from recurve bow with green piercing arrow trails #27AE60, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ranger. Agile backflip stagger, cloak swirling defensively, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ranger. Sinks to ground into leaves, recurve bow slipping from grasp, solid flat dark background #110E1B")
]

# 3. Druid (32x32)
druid_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Druid Invader, primal nature shapeshifter in antlered bark headdress and mossy brown robes #1E8449, gnarled living oak staff with blooming green buds, solid flat dark background #110E1B"
druid_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Druid. Root-gliding stride frame 1, mossy robes trailing leaves, oak staff pulsing with green mana, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Druid. Stride frame 2, antler tips gleaming, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Druid. Summons thorny bramble vines erupting from floor with blooming emerald nature magic #1E8449, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Druid. Bark skin absorbs hit with wood chips flying, druid reels, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Druid. Dissolves into autumn leaves and moss settling on stones, solid flat dark background #110E1B")
]

# 4. Alchemist (32x32)
alchemist_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Alchemist Invader, eccentric potion researcher in brass goggles and stained apron #D4AC0D, bandolier of volatile glass flasks, brass hand mortar, solid flat dark background #110E1B"
alchemist_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Alchemist. Hurried clattering step frame 1, glass flasks bobbing on belt, goggles reflecting light, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Alchemist. Hurried step frame 2, brass mortar held ready, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Alchemist. Hurls volatile explosive chemical flask shattering in yellow-green acid burst #D4AC0D, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Alchemist. Coughs in toxic puff, flasks clink together violently, recoiling, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Alchemist. Collapses as broken flasks spill bubbling colorful chemical pool, solid flat dark background #110E1B")
]

# 5. Shield Guardian (32x32)
sg_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Shield Guardian Invader, impenetrable bulwark protector in heavy square iron plate #7F8C8D, towering rectangular pavise tower shield covering entire body, solid flat dark background #110E1B"
sg_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shield Guardian. Slow fortified advance frame 1, pavise shield locked forward, iron boots clanking, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shield Guardian. Fortified advance frame 2, shield wall stance, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shield Guardian. Heavy concussive shield slam forward creating white shockwave impact #7F8C8D, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shield Guardian. Pavise shield rings like a bell, sparks fly, unyielding stance, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Shield Guardian. Slumps behind planted pavise shield, armor settling to stone, solid flat dark background #110E1B")
]

# 6. Battle Monk (32x32)
monk_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Battle Monk Invader, disciplined martial artist in saffron-orange wrapped cloth #E67E22, beaded prayer necklace, iron prayer wraps on fists glowing with chi energy, solid flat dark background #110E1B"
monk_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Battle Monk. Low martial glide frame 1, wrapped fists held in ready crane guard, saffron sashes flowing, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Battle Monk. Martial glide frame 2, light balanced footwork, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Battle Monk. Explosive palm strike thrust unleashing orange chi shockwave sphere #E67E22, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Battle Monk. Iron prayer wraps deflect blow with chi flare, graceful recovery step, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Battle Monk. Drops into lotus posture on stone floor, golden chi essence floating skyward, solid flat dark background #110E1B")
]

# Props Prompts
gold_prompt = "16bitscene, pickup sprite, masterpiece 2D pixel art single gold coin and small gold bullion stack #F1C40F, bright specular shine glint #FFFFFF, 1px solid dark outline #110E1B, crisp 16x16"
chest_prompt = "16bitscene, chest sprite, masterpiece 2D pixel art medieval wooden iron-banded treasure chest #A04000 with gilded lock #F39C12, 1px solid dark outline #110E1B, crisp 24x24"
shrine_prompt = "16bitscene, altar sprite, masterpiece 2D pixel art ancient stone blood shrine altar #8E44AD with glowing crimson chalice flame #E74C3C, carved runic skulls, 1px solid dark outline #110E1B, crisp 32x32"
lantern_prompt = "16bitscene, lantern sprite, masterpiece 2D pixel art ornate wrought-iron frozen street lantern #3498DB with blue spirit flame, icicles hanging from rim, 1px solid dark outline #110E1B, crisp 32x32"

if __name__ == "__main__":
    art_invaders = PROJECT_ROOT / "Assets/_Game/Art/Characters/Invaders"
    art_props = PROJECT_ROOT / "Assets/_Game/Art/Props"

    # Generate Remaining Invaders
    generate_and_stitch_character("inv_17_monster_hunter", mh_base, mh_anims, art_invaders / "inv_17_monster_hunter_sheet.png", 32, 32)
    generate_and_stitch_character("inv_18_ranger", ranger_base, ranger_anims, art_invaders / "inv_18_ranger_sheet.png", 32, 32)
    generate_and_stitch_character("inv_19_druid", druid_base, druid_anims, art_invaders / "inv_19_druid_sheet.png", 32, 32)
    generate_and_stitch_character("inv_20_alchemist", alchemist_base, alchemist_anims, art_invaders / "inv_20_alchemist_sheet.png", 32, 32)
    generate_and_stitch_character("inv_23_shield_guardian", sg_base, sg_anims, art_invaders / "inv_23_shield_guardian_sheet.png", 32, 32)
    generate_and_stitch_character("inv_24_battle_monk", monk_base, monk_anims, art_invaders / "inv_24_battle_monk_sheet.png", 32, 32)

    # Generate In-World Pickups and Props
    generate_prop("loot_gold_coin", gold_prompt, art_props / "loot_gold_coin.png", 16, 16)
    generate_prop("loot_item_chest", chest_prompt, art_props / "loot_item_chest.png", 24, 24)
    generate_prop("shrine_altar", shrine_prompt, art_props / "shrine_altar.png", 32, 32)
    generate_prop("prop_frozen_lantern", lantern_prompt, art_props / "prop_frozen_lantern.png", 32, 32)

    print("\n>>> BATCH 5 GENERATION COMPLETE! <<<")
