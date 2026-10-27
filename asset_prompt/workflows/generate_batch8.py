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

def generate_icon(name_id, prompt_text, out_path, w=32, h=32):
    if out_path.exists() and out_path.stat().st_size > 300:
        print(f"Skipping {name_id}: Already exists at {out_path}")
        return

    print(f"\n==========================================")
    print(f"GENERATING ICON: {name_id} ({w}x{h})")
    print(f"==========================================")

    g = build_txt2img_graph(prompt_text, f"{name_id}_icon", seed=505, scale_w=w, scale_h=h)
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
# BATCH 8 DEFINITIONS: Final Minion Summons & Equipment Weapons/Armors Icons
# ==============================================================================

# 1. Grave Spearman
spearman_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art sprite of Grave Spearman, reach phalanx skeleton soldier in rusted chainmail scraps #5A2D82, gripping long halberd-pike diagonally with both bony hands, glowing necrotic emerald eyes #30E680, solid flat dark background #110E1B"
spearman_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Spearman. Left leg stepping forward, pike poised, glowing eyes #30E680, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Spearman. Right leg stepping forward, torso settling, soul wisp trailing behind, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Spearman. Long forward two-handed pike thrust with emerald pierce streak and white motion arc, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Spearman. Knocked back, skull tilted back, bone splinters flying, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Spearman. Disjointed pile of bleached bones and skull on ground, fading green wisp, solid flat dark background #110E1B")
]

# 2. Grave Hound
hound_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art sprite of Grave Hound, low-slung quadruped skeletal wolf-hound with fang-filled skull, jagged exposed spine, glowing green soul-heart inside ribcage #30E680, solid flat dark background #110E1B"
hound_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Hound. Quadruped prowling stride frame 1, low-slung posture, glowing ribcage soul #30E680, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Hound. Quadruped prowling stride frame 2, coiled leg spring, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Hound. Forward pounce snap biting with snapping jaws and green kinetic streak #30E680, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Hound. Staggered backward, skull thrown back, bone fragments flying, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Grave Hound. Collapsing into pile of canine ribs and fanged skull, solid flat dark background #110E1B")
]

# 3. Wight Captain
captain_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art sprite of Wight Captain, regal undead commander knight in tarnished dark-steel plate armor #B8C2CC, horned crown-helm with blazing green visor #30E680, tattered purple cape #7D3C98, raising rune bastard sword, solid flat dark background #110E1B"
captain_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Wight Captain. Heavy marching stride frame 1, cape swaying, sword upright, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Wight Captain. Heavy marching stride frame 2, regal posture, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Wight Captain. Sweeping command bastard sword slash with emerald aura wave #30E680, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Wight Captain. Steel plate armor deflects strike with green sparks, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Wight Captain. Slumps forward, crown-helm clattering against stone, royal cape draped, solid flat dark background #110E1B")
]

# 4. Dread Abomination
abom_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art sprite of Dread Abomination, massive stitched flesh-and-bone Goliath with colossal bone-blade arm, iron belly chains, leaking green plague mist #30E680, solid flat dark background #110E1B"
abom_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dread Abomination. Heavy lumbering stomp frame 1, giant bone arm dragging, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dread Abomination. Heavy lumbering stomp frame 2, iron chains rattling, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dread Abomination. Massive 180 degree cleaver sweep spraying toxic green necrotic droplets #30E680, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dread Abomination. Huge frame shudders from impact, toxic ichor spraying, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Dread Abomination. Massive body topples forward in heap of rotting tissue and massive bones, solid flat dark background #110E1B")
]

# 5. Skeletal Dragon
dragon_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art sprite of Skeletal Dragon, undead wyrm summon with tattered purple membrane wings #6E2BC4, horned dragon skull blazing with emerald soulfire #30E680, coiled spine and ribcage, hovering, solid flat dark background #110E1B"
dragon_anims = [
    ("walk1", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Skeletal Dragon. Undulating serpentine hover flight frame 1, skeletal wings beating downward, solid flat dark background #110E1B"),
    ("walk2", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Skeletal Dragon. Undulating hover flight frame 2, skeletal wings upstroke, soulfire blazing, solid flat dark background #110E1B"),
    ("attack", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Skeletal Dragon. Unleashes forward emerald soulfire breath cone incinerating foes #30E680, solid flat dark background #110E1B"),
    ("hit", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Skeletal Dragon. Reclines backward midair, spectral dragon ribs vibrating, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 32x32 pixel art of Skeletal Dragon. Dragon skull drops to ground, wings folding into heap of ancient wyrm bones, solid flat dark background #110E1B")
]

# Weapons (32x32 icons)
weapons = [
    ("icon_wpn_1h_iron_broadsword", "16bitscene, item icon, masterpiece 2D pixel art diagonal 45 degree iron broadsword weapon icon, steel blade, brass guard, leather wrapped hilt, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_wpn_2h_colossal_claymore", "16bitscene, item icon, masterpiece 2D pixel art diagonal 45 degree colossal two-handed greatsword weapon icon, heavy darkened steel blade with notched edge, spiked iron crossguard, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_wpn_dag_shadow_fang", "16bitscene, item icon, masterpiece 2D pixel art diagonal 45 degree curved shadow dagger weapon icon, dark obsidian curved blade with dripping purple venom glow #9B59B6, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_wpn_bow_composite_shortbow", "16bitscene, item icon, masterpiece 2D pixel art composite recurve shortbow weapon icon with taut glowing string and notched hunting arrow, bone and dark yew wood limbs, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_wpn_stf_bone_staff", "16bitscene, item icon, masterpiece 2D pixel art diagonal necrotic wizard staff weapon icon, ancient gnarled bone shaft topped with horned demon skull clutching floating cyan frost gem, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_wpn_axe_crescent_waraxe", "16bitscene, item icon, masterpiece 2D pixel art diagonal double-headed crescent battle axe weapon icon, heavy sharpened steel crescent blades with blood runes, reinforced oak haft, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_wpn_wand_apprentice", "16bitscene, item icon, masterpiece 2D pixel art diagonal arcane wand weapon icon, carved polished elder wood with spiraling emerald mana vein and glowing crystal tip, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_wpn_scythe_grim", "16bitscene, item icon, masterpiece 2D pixel art diagonal reaper death scythe weapon icon, massive curved ebony blade trailing spectral soul mist #2ECC71, long dark staff, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B")
]

# Armors (32x32 icons)
armors = [
    ("icon_item_crypt_robes", "16bitscene, item icon, masterpiece 2D pixel art necromancer crypt robes armor icon, tattered royal purple burial shroud vestment with carved bone collar and runic hem, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_item_necrotic_shroud", "16bitscene, item icon, masterpiece 2D pixel art necrotic shadow shroud armor mantle icon, dark ethereal cloak with woven soul threads and ghostly emerald trim #30E680, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_item_infernal_plate", "16bitscene, item icon, masterpiece 2D pixel art infernal demon cuirass armor plate icon, black obsidian chestplate forged in hellfire with glowing red molten seams #E74C3C and spiked pauldrons, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_item_frost_cowl", "16bitscene, item icon, masterpiece 2D pixel art glacial frost cowl armor hood and mantle icon, crystalline ice-blue woven hood frosted with snow crystals and sapphire brooch, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_item_leather_brigandine", "16bitscene, item icon, masterpiece 2D pixel art studded leather brigandine armor vest icon, toughened brown leather doublet reinforced with steel rivets and belt buckles, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B"),
    ("icon_item_iron_mail", "16bitscene, item icon, masterpiece 2D pixel art heavy iron chainmail hauberk armor chestpiece icon, interlocking steel chain rings over padded gambeson with iron gorget, 1px dark outline #110E1B, crisp 32x32, solid dark background #110E1B")
]

def main():
    summons_dir = PROJECT_ROOT / "Assets" / "_Game" / "Art" / "Characters" / "Summons"
    weapons_dir = PROJECT_ROOT / "Assets" / "_Game" / "Art" / "UI" / "Equipment" / "Weapons"
    armors_dir = PROJECT_ROOT / "Assets" / "_Game" / "Art" / "UI" / "Equipment" / "Armors"

    print(">>> STARTING BATCH 8 ASSET GENERATION <<<")
    
    # 1. Minion Summons
    generate_and_stitch_character("sum_grave_spearman", spearman_base, spearman_anims, summons_dir / "sum_grave_spearman_sheet.png", 32, 32)
    generate_and_stitch_character("sum_grave_hound", hound_base, hound_anims, summons_dir / "sum_grave_hound_sheet.png", 32, 32)
    generate_and_stitch_character("sum_wight_captain", captain_base, captain_anims, summons_dir / "sum_wight_captain_sheet.png", 32, 32)
    generate_and_stitch_character("sum_dread_abomination", abom_base, abom_anims, summons_dir / "sum_dread_abomination_sheet.png", 32, 32)
    generate_and_stitch_character("sum_skeletal_dragon", dragon_base, dragon_anims, summons_dir / "sum_skeletal_dragon_sheet.png", 32, 32)

    # 2. Equipment Weapons
    for name, prompt in weapons:
        generate_icon(name, prompt, weapons_dir / f"{name}.png", 32, 32)

    # 3. Equipment Armors
    for name, prompt in armors:
        generate_icon(name, prompt, armors_dir / f"{name}.png", 32, 32)

    print("\n>>> BATCH 8 COMPLETE: ALL ASSETS GENERATED AND SAVED <<<")

if __name__ == "__main__":
    main()
