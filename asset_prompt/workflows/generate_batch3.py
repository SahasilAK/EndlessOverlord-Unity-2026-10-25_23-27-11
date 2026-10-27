import json
import urllib.request
import time
import os
import shutil
import re
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
    if out_path.exists() and out_path.stat().st_size > 500:
        print(f"Skipping {name_id}: Already exists at {out_path}")
        return

    print(f"\n==========================================")
    print(f"GENERATING TILE: {name_id} ({w}x{h})")
    print(f"==========================================")

    g = build_txt2img_graph(prompt_text, f"{name_id}_tile", seed=101, scale_w=w, scale_h=h)
    res = queue_prompt(g)
    hist = wait_for_completion(res["prompt_id"])
    out_name = hist["outputs"]["9"]["images"][0]["filename"]
    
    im = Image.open(OUTPUT_DIR / out_name)
    if im.size != (w, h):
        im = im.resize((w, h), Image.Resampling.NEAREST)
    out_path.parent.mkdir(parents=True, exist_ok=True)
    im.save(out_path, "PNG")
    print(f"SAVED TILE: {out_path} ({im.width}x{im.height} px)")
    time.sleep(2.5)

# ==============================================================================
# BATCH 3 ASSET DEFINITIONS
# ==============================================================================

# 1. Crossbowman Invader
crossbowman_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Crossbowman Invader, heavy armor-piercing ranged marksman in travel cloak and leather brigandine #CC8C4C, iron bracers, mechanical crossbow loaded with bolt held horizontally, solid flat dark background #110E1B"
crossbowman_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Crossbowman. Advancing stride frame 1, left boot forward, crossbow held ready, cloak flares, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Crossbowman. Advancing stride frame 2, right boot forward, heavy bolt chambered, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Crossbowman. Fires mechanical crossbow with bright piercing bolt flash and muzzle smoke #CC8C4C, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Crossbowman. Staggered backward from strike, crossbow recoils upward, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Crossbowman. Collapsed to stones, crossbow dropped, soul orb rising, solid flat dark background #110E1B")
]

# 2. Spearman Invader
spearman_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Spearman Invader, nimble agile reach infantry in reinforced gambeson #4DB882, long ashwood spear with steel point held forward, round buckler, solid flat dark background #110E1B"
spearman_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spearman. Stride frame 1, spear gripped two-handed low, advancing forward, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spearman. Stride frame 2, opposite step, spear tip level, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spearman. Rapid linear spear thrust forward with sharp green-steel action streak #4DB882, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spearman. Knocked backward, spear tip pulled back defensively, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Spearman. Collapsed to ground, ashwood spear clattering to stone, solid flat dark background #110E1B")
]

# 3. Paladin Invader
paladin_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Paladin Invader, holy knight crusader in polished golden-steel plate armor #E6B800, white tabard with sun crest, blessed warhammer and kite shield, solid flat dark background #110E1B"
paladin_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Paladin. Holy crusade stride frame 1, warhammer resting on shoulder, kite shield raised, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Paladin. Holy crusade stride frame 2, armored boots thudding forward, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Paladin. Overhead warhammer slam erupting with radiant holy light #E6B800 and divine ground sparks, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Paladin. Shield deflects blow with golden sparks, slight stagger, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Paladin. Fallen to knees then stones, golden light dissipating into motes, solid flat dark background #110E1B")
]

# 4. Cleric Invader
cleric_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Cleric Invader, holy support healer in ceremonial white and sky-blue robes #4DA6FF, golden sun talisman, silver scepter held aloft, solid flat dark background #110E1B"
cleric_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Cleric. Gentle walking stride frame 1, robes billowing, silver scepter held forward, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Cleric. Walking stride frame 2, holy talisman swinging gently, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Cleric. Chanting prayer with raised scepter, glowing aura of blue-gold healing radiance #4DA6FF expanding outward, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Cleric. Staggered backward, robes fluttering, clutching holy talisman, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Cleric. Robes collapsing to ground, scepter rolling away, ethereal prayer light fading, solid flat dark background #110E1B")
]

# 5. Ice Mage Invader
ice_mage_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Ice Mage Invader, cryomancer in frost-rimed sapphire robes #3399FF, pointed wizard cowl, carved crystal glacial staff topped with floating ice shard, solid flat dark background #110E1B"
ice_mage_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ice Mage. Gliding stride frame 1, sapphire robes trailing frost motes, staff held forward, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ice Mage. Gliding stride frame 2, floating ice shard orbiting staff tip, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ice Mage. Thrusts glacial staff forward casting jagged frost nova shards and freezing mist #3399FF, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ice Mage. Frost barrier shatters in ice crystals as mage recoils backward, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Ice Mage. Collapsing into freezing frost mist, crystal staff shattering into ice shards, solid flat dark background #110E1B")
]

# 6. Lightning Mage Invader
lightning_mage_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Lightning Mage Invader, storm channeler in indigo and crackling gold robes #FFD13B, storm conductor staff with arcing electricity, electric sparks around fingertips, solid flat dark background #110E1B"
lightning_mage_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Lightning Mage. Rapid energetic step frame 1, indigo cloak flaring, electric sparks trailing feet, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Lightning Mage. Step frame 2, conductor staff humming with voltage, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Lightning Mage. Unleashes jagged branching chain lightning bolt from staff with blinding yellow-white flash #FFD13B, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Lightning Mage. Jolted backward by impact, sparks discharging erratically, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Lightning Mage. Body dissipates in crackling electric shockwave, staff falling to ground, solid flat dark background #110E1B")
]

# 7. Arcane Wizard Invader
arcane_wizard_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Arcane Wizard Invader, high sorcerer in regal amethyst robes #A855F7, glowing runic tome floating beside hand, star-tipped arcane staff, solid flat dark background #110E1B"
arcane_wizard_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Arcane Wizard. Levitation step frame 1, amethyst robes hovering 2px above stones, runic tome turning pages, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Arcane Wizard. Levitation step frame 2, mystical violet aura humming, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Arcane Wizard. Conjures pulsing arcane singularity sphere and three homing magic missiles in brilliant violet #A855F7, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Arcane Wizard. Arcane ward flickers with geometric glyphs upon impact, wizard stumbles, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Arcane Wizard. Fades into shimmering violet astral dust, runic tome gently closing, solid flat dark background #110E1B")
]

# 8. Witch Hunter Invader
witch_hunter_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art hero sprite of Witch Hunter Invader, grim inquisitor in wide-brimmed hat, long charcoal duster #B35900, silver-inlaid flintlock pistol in left hand, sanctified rapier in right, solid flat dark background #110E1B"
witch_hunter_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Witch Hunter. Stalking step frame 1, long duster swaying, eyes shaded by hat brim, weapons ready, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Witch Hunter. Stalking step frame 2, flintlock raised level, rapier poised low, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Witch Hunter. Quick-draw fires silver flintlock pistol with orange powder flash #B35900 followed by lunging rapier thrust, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Witch Hunter. Hat tips back, grits teeth through impact, recoils 1px defensively, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Witch Hunter. Slumps against stone, wide hat falling off, flintlock and rapier slipping away, solid flat dark background #110E1B")
]

# 9. Boss: Grand Inquisitor Malakor (48x48)
malakor_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Grand Inquisitor Malakor The Cleanser, towering zealot commander in ornate crimson-enameled armor #CC3340, flowing executioner cloak, double-barrel sanctified hand cannon and flaming judgment greatsword, solid flat dark background #110E1B"
malakor_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Inquisitor Malakor. Imposing heavy stride frame 1, flaming sword held two-handed, crimson executioner cloak billowing, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Inquisitor Malakor. Imposing heavy stride frame 2, opposite armored boot forward, fiery ground sparks, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Inquisitor Malakor. Sweeping judgment greatsword arc engulfing arena in holy flame waves #CC3340, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Inquisitor Malakor. Heavy plate absorbs impact with sparks, eyes glowing with incandescent fury, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Inquisitor Malakor. Drops to one knee, greatsword planted in stone cracking floor, fiery spirit ascending, solid flat dark background #110E1B")
]

# 10. Boss: Warlord Kraag (48x48)
kraag_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Warlord Kraag The Mountain Breaker, massive barbarian warlord in spiked iron plate and mammoth furs #D9531E, wielding a colossal two-handed jagged battleaxe, glowing red war paint, solid flat dark background #110E1B"
kraag_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Warlord Kraag. Earth-shaking stride frame 1, colossal battleaxe resting on massive shoulder, furs bristling, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Warlord Kraag. Earth-shaking stride frame 2, heavy iron boots pounding stones, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Warlord Kraag. Leaping overhead ground-slam with colossal battleaxe tearing fissures in dungeon stone #D9531E, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Warlord Kraag. Roaring through the pain, blood-red eyes blazing, barely budging from the hit, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Warlord Kraag. Collapsing like a fallen mountain, battleaxe embedding deep in bedrock, warrior soul roaring into ether, solid flat dark background #110E1B")
]

# 11. Boss: High Priestess Elara (48x48)
elara_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of High Priestess Elara Of The Sunlit Font, radiant solar avatar in flowing white and woven solar-gold vestments #FFCC00, halo of spinning sun sigils, floating grand staff of the dawn, solid flat dark background #110E1B"
elara_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Priestess Elara. Floating solar grace frame 1, gold vestments flowing, dawn staff pulsing with sunlight, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Priestess Elara. Floating solar grace frame 2, spinning sun halo rotating 45 degrees, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Priestess Elara. Summons cascading pillar of blinding solar wrath #FFCC00 descending from heaven, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Priestess Elara. Sun halo flickers violently as radiant barrier absorbs strike, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Priestess Elara. Vestments dissolve into brilliant golden morning light, sun halo ascending into sky, solid flat dark background #110E1B")
]

# 12. Biome 1 Tiles
biome1_floor_prompt = "16bitscene, seamless floor tile, masterpiece top-down 2D pixel art dungeon floor tile of Forgotten Necropolis, cracked ancient dark slate cobblestone #2A2E30, subtle moss and faint burial runes in joints, zero anti-aliasing, perfectly tileable 32x32"
biome1_wall_prompt = "16bitscene, seamless wall tile, masterpiece top-down 2D pixel art dungeon perimeter wall of Forgotten Necropolis, carved granite crypt masonry #3E4447, skull reliefs, dark mortar, 1px solid outline #110E1B, crisp pixel art 32x32"
biome1_pillar_prompt = "16bitscene, obstacle tile, masterpiece top-down 2D pixel art crypt pillar obstacle of Forgotten Necropolis, weathered stone obelisk #6B7275 with chained skeletal statue, 1px solid dark outline #110E1B, crisp pixel art 32x32"
biome1_portal_prompt = "16bitscene, portal sprite, masterpiece top-down 2D pixel art invader rift portal of Forgotten Necropolis, swirling ethereal rift gate in turquoise soul fire #4DEEEA and abyssal dark violet, 1px solid outline #110E1B, crisp pixel art 48x48"

if __name__ == "__main__":
    art_invaders = PROJECT_ROOT / "Assets/_Game/Art/Characters/Invaders"
    art_bosses = PROJECT_ROOT / "Assets/_Game/Art/Characters/Bosses"
    art_env = PROJECT_ROOT / "Assets/_Game/Art/Environment/Dungeons"

    # Generate Invaders
    generate_and_stitch_character("inv_03_crossbowman", crossbowman_base, crossbowman_anims, art_invaders / "inv_03_crossbowman_sheet.png", 32, 32)
    generate_and_stitch_character("inv_04_spearman", spearman_base, spearman_anims, art_invaders / "inv_04_spearman_sheet.png", 32, 32)
    generate_and_stitch_character("inv_09_paladin", paladin_base, paladin_anims, art_invaders / "inv_09_paladin_sheet.png", 32, 32)
    generate_and_stitch_character("inv_10_cleric", cleric_base, cleric_anims, art_invaders / "inv_10_cleric_sheet.png", 32, 32)
    generate_and_stitch_character("inv_13_ice_mage", ice_mage_base, ice_mage_anims, art_invaders / "inv_13_ice_mage_sheet.png", 32, 32)
    generate_and_stitch_character("inv_14_lightning_mage", lightning_mage_base, lightning_mage_anims, art_invaders / "inv_14_lightning_mage_sheet.png", 32, 32)
    generate_and_stitch_character("inv_15_arcane_wizard", arcane_wizard_base, arcane_wizard_anims, art_invaders / "inv_15_arcane_wizard_sheet.png", 32, 32)
    generate_and_stitch_character("inv_16_witch_hunter", witch_hunter_base, witch_hunter_anims, art_invaders / "inv_16_witch_hunter_sheet.png", 32, 32)

    # Generate Bosses (48x48)
    generate_and_stitch_character("boss_04_malakor", malakor_base, malakor_anims, art_bosses / "boss_04_malakor_sheet.png", 48, 48)
    generate_and_stitch_character("boss_05_kraag", kraag_base, kraag_anims, art_bosses / "boss_05_kraag_sheet.png", 48, 48)
    generate_and_stitch_character("boss_06_elara", elara_base, elara_anims, art_bosses / "boss_06_elara_sheet.png", 48, 48)

    # Generate Environment Tiles
    generate_tile("biome_01_floor", biome1_floor_prompt, art_env / "biome_01_floor.png", 32, 32)
    generate_tile("biome_01_wall", biome1_wall_prompt, art_env / "biome_01_wall.png", 32, 32)
    generate_tile("biome_01_pillar", biome1_pillar_prompt, art_env / "biome_01_pillar.png", 32, 32)
    generate_tile("biome_01_portal", biome1_portal_prompt, art_env / "biome_01_portal.png", 48, 48)

    print("\n>>> BATCH 3 GENERATION COMPLETE! <<<")
