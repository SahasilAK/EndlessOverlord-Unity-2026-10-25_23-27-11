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

# ==============================================================================
# BATCH 4 DEFINITIONS: Bosses 7-10 & Key Summons
# ==============================================================================

# 1. Boss: Sylas (The Gale Ranger) (48x48)
sylas_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Sylas The Gale Ranger, elite wind marksman in aerodynamic forest-emerald tunic #2ECC71 and fluttering owl feather cloak, magnificent wind-carved composite longbow, solid flat dark background #110E1B"
sylas_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Sylas. Swift gliding leap frame 1, bow gripped ready, emerald cloak streaming in wind vortex, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Sylas. Swift gliding leap frame 2, opposite foot landing, feather cloak settling, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Sylas. Rapidly looses a fan of five spiraling cyclone gale arrows with jade wind trails #2ECC71, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Sylas. Wind shield deflects blow with swirling emerald leaves, agile backstep, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Sylas. Dissipates into a gentle whirlwind of emerald leaves and ascending wind spirits, solid flat dark background #110E1B")
]

# 2. Boss: Baron von Eisen (The Iron Bastion) (48x48)
eisen_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Baron von Eisen The Iron Bastion, colossal fortress juggernaut in impervious dark riveted iron plate #5D6D7E, gigantic spiked tower shield taller than himself, heavy steam-powered warhammer, solid flat dark background #110E1B"
eisen_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Baron von Eisen. Slow earth-shattering march frame 1, tower shield locked forward, steam venting from pauldron valves, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Baron von Eisen. March frame 2, iron greaves crushing stone, warhammer swinging, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Baron von Eisen. Slams tower shield into dungeon floor unleashing shockwave barrier and kinetic iron spike eruption #5D6D7E, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Baron von Eisen. Shrugs off impact with metallic clatter and spark spray from heavy iron plating, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Baron von Eisen. Massive iron plates buckling, steam exploding from core, tower shield planting firmly in bedrock as monument, solid flat dark background #110E1B")
]

# 3. Boss: Vaelin (The Storm Weaver) (48x48)
vaelin_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Vaelin The Storm Weaver, tempest sorceress in electric cerulean robes #00D2D3, crackling lightning aura, orb of contained supercells hovering between hands, solid flat dark background #110E1B"
vaelin_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Vaelin. Hovering storm drift frame 1, cerulean robes crackling with high-voltage arcs, storm orb pulsating, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Vaelin. Storm drift frame 2, lightning arcs jumping to stone floor, hair floating, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Vaelin. Unleashes catastrophic multi-prong thunderstrike and electric storm vortex across arena #00D2D3, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Vaelin. Electric discharge pops defensively as sorceress reels backward, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Vaelin. Implodes into blinding electric nova, lingering blue ozone sparks ascending into dungeon vault, solid flat dark background #110E1B")
]

# 4. Boss: Lord Alden (The Royal Champion) (48x48)
alden_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Lord Alden The Royal Champion, legendary grand marshal in gilded royal dragon plate armor #F39C12, crimson king cloak with ermine trim, runic sovereign greatsword crowned with radiant sunlight, solid flat dark background #110E1B"
alden_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Alden. Regal champion advance frame 1, royal greatsword resting on shoulder, crimson cloak billowing, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Alden. Regal champion advance frame 2, gilded sabatons gleaming, sovereign authority stance, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Alden. Executes Royal Cleave with 360-degree golden dragon blade arc and explosive holy radiance shockwave #F39C12, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Alden. Golden dragon breastplate deflects strike with royal sigil glint, champion steps back firmly, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Alden. Drops to one knee, greatsword plunged into dungeon stone, royal cloak draping floor as champion salutes, solid flat dark background #110E1B")
]

# 5. Summon: Bone Mage (32x32)
bone_mage_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art minion sprite of Bone Mage summon, skeletal spellcaster in tattered dark violet cowl #8E44AD, ribcage glowing with necromantic green flame, bone wand channeling soul bolt, solid flat dark background #110E1B"
bone_mage_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Bone Mage. Creeping skeletal step frame 1, tattered cowl floating, bone wand held forward, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Bone Mage. Step frame 2, green soul flame flaring inside skull, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Bone Mage. Casts necrotic soul bolt missile from wand tip with emerald-violet trail #8E44AD, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Bone Mage. Bone joints rattle, skull recoils backward defensively, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Bone Mage. Bones collapse into a heap of ash and white bone shards, cowl settling flat, solid flat dark background #110E1B")
]

# 6. Summon: Corpse Collector (32x32)
corpse_collector_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art minion sprite of Corpse Collector summon, hunchbacked ghoul mortician in butcher leather apron #7D6608, bone meat-hook and lantern of collected soul embers, solid flat dark background #110E1B"
corpse_collector_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Corpse Collector. Shuffling hunchback stride frame 1, bone hook dragging on stones, lantern swinging, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Corpse Collector. Shuffling stride frame 2, grotesque head bobbing, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Corpse Collector. Swings heavy bone hook in sweeping arc and reaps soul essence #7D6608, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Corpse Collector. Stumbles sideways with raspy hiss, clutching soul lantern, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Corpse Collector. Collapses face-first, soul lantern shattering into green motes, solid flat dark background #110E1B")
]

if __name__ == "__main__":
    art_bosses = PROJECT_ROOT / "Assets/_Game/Art/Characters/Bosses"
    art_summons = PROJECT_ROOT / "Assets/_Game/Art/Characters/Summons"

    # Generate Bosses 7-10 (48x48)
    generate_and_stitch_character("boss_07_sylas", sylas_base, sylas_anims, art_bosses / "boss_07_sylas_sheet.png", 48, 48)
    generate_and_stitch_character("boss_08_eisen", eisen_base, eisen_anims, art_bosses / "boss_08_eisen_sheet.png", 48, 48)
    generate_and_stitch_character("boss_09_vaelin", vaelin_base, vaelin_anims, art_bosses / "boss_09_vaelin_sheet.png", 48, 48)
    generate_and_stitch_character("boss_10_alden", alden_base, alden_anims, art_bosses / "boss_10_alden_sheet.png", 48, 48)

    # Generate Summons (32x32)
    generate_and_stitch_character("sum_bone_mage", bone_mage_base, bone_mage_anims, art_summons / "sum_bone_mage_sheet.png", 32, 32)
    generate_and_stitch_character("sum_corpse_collector", corpse_collector_base, corpse_collector_anims, art_summons / "sum_corpse_collector_sheet.png", 32, 32)

    print("\n>>> BATCH 4 GENERATION COMPLETE! <<<")
