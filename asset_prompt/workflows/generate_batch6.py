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

def generate_vfx(name_id, prompt_text, out_path, w=32, h=32):
    if out_path.exists() and out_path.stat().st_size > 300:
        print(f"Skipping {name_id}: Already exists at {out_path}")
        return

    print(f"\n==========================================")
    print(f"GENERATING VFX: {name_id} ({w}x{h})")
    print(f"==========================================")

    g = build_txt2img_graph(prompt_text, f"{name_id}_vfx", seed=303, scale_w=w, scale_h=h)
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
    print(f"SAVED VFX: {out_path} ({im.width}x{im.height} px)")
    time.sleep(2.5)

# ==============================================================================
# BATCH 6 DEFINITIONS: Combat VFX & Remaining Invader Archetypes
# ==============================================================================

# 1. Assassin (32x32)
assassin_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Assassin Invader, lethal shadow executioner in midnight-black cowl #1C2833, dual poisoned obsidian kris daggers glowing with venomous green, smoke wisps at heels, solid flat dark background #110E1B"
assassin_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Assassin. Shadow step frame 1, dual daggers held in reverse grip, smoke flaring, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Assassin. Shadow step frame 2, stalking crouch advance, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Assassin. Cross-slash flurry with dual venomous kris daggers with toxic emerald smear #2ECC71, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Assassin. Disperses partially into shadow smoke upon impact, recoil step, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Assassin. Dissolves entirely into midnight mist, dual daggers dropping to floor, solid flat dark background #110E1B")
]

# 2. Beastmaster (32x32)
beastmaster_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Beastmaster Invader, wild pack leader in dire wolf pelt and leather harness #A04000, bone horn hanging from neck, spiked animal-training whip in hand, solid flat dark background #110E1B"
beastmaster_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Beastmaster. Hunting stride frame 1, whip held coiled, wolf head cowl snarling, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Beastmaster. Hunting stride frame 2, heavy booted step, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Beastmaster. Cracks barbed whip forward with loud sonic shockwave and red beast aura #A04000, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Beastmaster. Wolf cowl absorbs impact with bristling fur, defensive recoil, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Beastmaster. Falls to floor, bone horn cracking, spirit wolves howling as they fade, solid flat dark background #110E1B")
]

# 3. Summoner (32x32)
summoner_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Summoner Invader, planar mystic in midnight-blue occult robes #2471A3, brass astrolabe staff topped with spinning planar crystal, orbiting spirit motes, solid flat dark background #110E1B"
summoner_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Summoner. Floating astral step frame 1, robes billowing, planar crystal humming, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Summoner. Astral step frame 2, spirit motes circling, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Summoner. Slams astrolabe staff casting glowing planar summoning circle with blue energy pillars #2471A3, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Summoner. Planar ward flashes absorbing blow, mystic stumbles backward, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Summoner. Banished through closing celestial rift, robes collapsing empty to floor, solid flat dark background #110E1B")
]

# 4. Holy Templar (32x32)
templar_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Holy Templar Invader, zealous crusader in heavy silver knight plate #D4AC0D with red cross tabard, massive two-handed grand executioner sword, solid flat dark background #110E1B"
templar_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Holy Templar. Unstoppable crusade advance frame 1, greatsword held upright, tabard flowing, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Holy Templar. Crusade advance frame 2, iron boots thudding forward, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Holy Templar. Sweeping overhead grand executioner sword strike with radiant golden-red holy arc #D4AC0D, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Holy Templar. Plate armor rings with sparks, firm counter-balance step, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Holy Templar. Falls to knees in prayer before collapsing, greatsword upright in stone, solid flat dark background #110E1B")
]

# 5. Rune Knight (32x32)
runeknight_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Rune Knight Invader, arcane warrior in dark runic plate armor #16A085 glowing with cyan ancient glyphs, runed broadsword etched with power, solid flat dark background #110E1B"
runeknight_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Rune Knight. Heavy runic march frame 1, cyan glyphs pulsating along breastplate, sword drawn, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Rune Knight. Runic march frame 2, opposite step, weapon trailing runic light, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Rune Knight. Unleashes explosive runic blade thrust unleashing expanding cyan glyph burst #16A085, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Rune Knight. Protective runic barrier flashes upon impact, knight absorbs shock, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Rune Knight. Armor glyphs shatter like glass, body falling to stones, solid flat dark background #110E1B")
]

# 6. Spellblade (32x32)
spellblade_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Spellblade Invader, nimble battle-mage in leather-trimmed violet gambeson #8E44AD, enchanted crystal rapier humming with magic, floating arcane spell orbs, solid flat dark background #110E1B"
spellblade_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spellblade. Fencing lunge stride frame 1, crystal rapier poised horizontally, violet cloak flaring, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spellblade. Fencing stride frame 2, agile blade repositioning, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spellblade. Lightning-fast enchanted rapier thrust followed by prismatic magic wave in violet-cyan #8E44AD, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spellblade. Parry deflection with crystal blade, acrobatic recoil, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spellblade. Dissolves into prismatic spell sparklers, crystal rapier fading, solid flat dark background #110E1B")
]

# VFX Prompts
bolt_prompt = "16bitscene, projectile sprite, masterpiece 2D pixel art magic projectile bolt #2ECC71, glowing emerald necrotic soul bolt with bright white core and trailing comet tail, 1px solid dark outline #110E1B, crisp 16x16"
slash_prompt = "16bitscene, attack visual, masterpiece 2D pixel art curved crescent sword slash arc in bright cyan-white steel #00D2D3 with fading speed trail, 1px solid outline, crisp 32x16"
hazard_prompt = "16bitscene, hazard pool, masterpiece 2D pixel art circular bubbling necrotic acid slime pool #27AE60 with dissolving bubbles and toxic edge, 1px solid dark outline, crisp 32x32"
barrier_prompt = "16bitscene, shield visual, masterpiece 2D pixel art spherical magical energy barrier shield bubble in translucent turquoise #1ABC9C with hexagonal energy lattice lines and specular rim glint, crisp 32x32"

if __name__ == "__main__":
    art_invaders = PROJECT_ROOT / "Assets/_Game/Art/Characters/Invaders"
    art_vfx = PROJECT_ROOT / "Assets/_Game/Art/VFX"

    # Generate Remaining Invaders
    generate_and_stitch_character("inv_07_assassin", assassin_base, assassin_anims, art_invaders / "inv_07_assassin_sheet.png", 32, 32)
    generate_and_stitch_character("inv_21_beastmaster", beastmaster_base, beastmaster_anims, art_invaders / "inv_21_beastmaster_sheet.png", 32, 32)
    generate_and_stitch_character("inv_22_summoner", summoner_base, summoner_anims, art_invaders / "inv_22_summoner_sheet.png", 32, 32)
    generate_and_stitch_character("inv_25_holy_templar", templar_base, templar_anims, art_invaders / "inv_25_holy_templar_sheet.png", 32, 32)
    generate_and_stitch_character("inv_26_rune_knight", runeknight_base, runeknight_anims, art_invaders / "inv_26_rune_knight_sheet.png", 32, 32)
    generate_and_stitch_character("inv_27_spellblade", spellblade_base, spellblade_anims, art_invaders / "inv_27_spellblade_sheet.png", 32, 32)

    # Generate Combat VFX
    generate_vfx("vfx_projectile_bolt", bolt_prompt, art_vfx / "vfx_projectile_bolt.png", 16, 16)
    generate_vfx("vfx_melee_slash", slash_prompt, art_vfx / "vfx_melee_slash.png", 32, 16)
    generate_vfx("vfx_hazard_pool", hazard_prompt, art_vfx / "vfx_hazard_pool.png", 32, 32)
    generate_vfx("vfx_barrier_shield", barrier_prompt, art_vfx / "vfx_barrier_shield.png", 32, 32)

    print("\n>>> BATCH 6 GENERATION COMPLETE! <<<")
