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

def build_txt2img_graph(prompt_text, prefix, seed=42, width=512, height=512, scale_w=48, scale_h=48):
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

def build_img2img_graph(image_filename, prompt_text, prefix, seed, scale_w=48, scale_h=48, denoise=0.42):
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

def generate_and_stitch_character(name_id, base_prompt, anim_prompts, out_path, fw=48, fh=48):
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
    print(f"SAVED BOSS SPRITE SHEET: {out_path} ({sheet.width}x{sheet.height} px)")

# ==============================================================================
# BATCH 9 DEFINITIONS: Extended Legendary Bosses (11 to 20)
# ==============================================================================

# 11. Lady Vespera (Crossbowman)
vespera_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Lady Vespera, Twilight Huntress, silent marksman dual-wielding heavy repeating crossbows, purple hooded cowl and cape #8040A6, crested visor, solid flat dark background #110E1B"
vespera_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Vespera. Striding forward with commanding poise, repeating crossbows held ready, cape billowing, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Vespera. Raising crossbows with glowing violet sights, coiled spring windup, solid flat dark background #110E1B"),
    ("multishot", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Vespera. Rapid fan-fire barrage of purple shadow bolts #8040A6 with bright muzzle flashes, solid flat dark background #110E1B"),
    ("blink", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Vespera. Shadow-step blink vanishing into ethereal purple mist #8040A6, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Vespera. Berserk enrage posture, eyes blazing pure white, twin crossbows loaded with void energy, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Vespera. Falls to knee, crossbows clattering on stone, shadow essence rising, solid flat dark background #110E1B")
]

# 12. Brother Kaelen (BattleMonk)
kaelen_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Brother Kaelen, The Iron Palm, ascetic martial grandmaster in monk wraps #D8A659, beaded prayer cord, brass cestus gauntlets glowing with amber ki, solid flat dark background #110E1B"
kaelen_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Brother Kaelen. Measured martial advance, balanced horse stance, glowing brass cestus, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Brother Kaelen. Pulling back iron fist, gathering concentrated golden ki vortex around knuckle, solid flat dark background #110E1B"),
    ("slam", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Brother Kaelen. Earthshaking shockwave palm strike detonating amber ki fissure #D8A659 on stone, solid flat dark background #110E1B"),
    ("ward", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Brother Kaelen. Focusing golden sanctuary bell barrier with floating mantras, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Brother Kaelen. Enraged roaring stance, golden ki dragon aura erupting from shoulders, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Brother Kaelen. Sits into final meditation pose as spiritual ki dissipates into golden dust, solid flat dark background #110E1B")
]

# 13. Morvath (ArcaneWizard)
morvath_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Morvath, The Soul Harvester, forbidden arcanist in tiered amethyst robes #994DE6, floating runic tomes, soul-lantern staff, solid flat dark background #110E1B"
morvath_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Morvath. Levitation glide forward, voluminous purple robes swirling, staff aloft, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Morvath. Staff raised high, gathering spiraling dark violet arcane vortex, solid flat dark background #110E1B"),
    ("prison", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Morvath. Unleashing glacial soul-ice crystal pillars encasing area in purple frost #994DE6, solid flat dark background #110E1B"),
    ("thunder", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Morvath. Calling down violet lightning pillar through staff tip with thunderous flash, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Morvath. Enraged posture, four spectral arcane arms manifesting from back, glowing purple runes, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Morvath. Soul-lantern shatters, robes collapse into pile of purple embers, solid flat dark background #110E1B")
]

# 14. Commander Gerald (Knight)
gerald_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Commander Gerald of the Silver Watch, disciplined legionary commander in polished silver-blue plate armor #CCD9F2, white plume helm, heavy tower shield, solid flat dark background #110E1B"
gerald_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Commander Gerald. Resolute martial march, tower shield locked forward, silver blade drawn, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Commander Gerald. Broadsword pulled back in two-handed stance, silver armor gleaming, solid flat dark background #110E1B"),
    ("cleave", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Commander Gerald. Colossal horizontal sword cleave releasing radiant white-silver arc #CCD9F2, solid flat dark background #110E1B"),
    ("vanguard", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Commander Gerald. Raising sword to rally vanguard, silver watch heraldic banner surging, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Commander Gerald. Enraged stance, dented silver plate, crimson eyes blazing beneath silver visor, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Commander Gerald. Drops to knee leaning upon tower shield, blade resting in ground, solid flat dark background #110E1B")
]

# 15. Theron (Spearman)
theron_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Theron, The Dragonbane Dragoon, sky dragoon in bronze-orange dragonscale armor #E68033, winged helm, colossal serrated dragontooth spear, solid flat dark background #110E1B"
theron_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Theron. Agile leaping stride, serrated dragontooth spear held level, scale cloak swaying, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Theron. High crouching windup, spear point aimed straight at target with fiery orange spark, solid flat dark background #110E1B"),
    ("charge", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Theron. Blinding whirlwind spear thrust drill leaving blazing fire trails #E68033, solid flat dark background #110E1B"),
    ("slam", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Theron. Aerial plunge slam impaling dungeon floor with volcanic dragon shockwave, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Theron. Enraged dragon roar, flaming wyrm wings flaring from pauldrons, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Theron. Collapses, dragontooth spear shattered in half upon stone, dragon embers drifting, solid flat dark background #110E1B")
]

# 16. Arch-Druid Bran (Druid)
bran_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Arch-Druid Bran, The Primal Maw, ancient druid in moss-grown bark armor #40A64D, great stag antlers, gnarled living oak staff, solid flat dark background #110E1B"
bran_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Arch-Druid Bran. Heavy lumbering stride, roots shifting beneath feet, stag antlers crowned with leaves, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Arch-Druid Bran. Raising living oak staff with both hands, emerald nature runes swirling, solid flat dark background #110E1B"),
    ("earthshaker", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Arch-Druid Bran. Stomping staff down, erupting thorny vine brambles and emerald shockwave #40A64D, solid flat dark background #110E1B"),
    ("summons", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Arch-Druid Bran. Summoning spectral spirit wolves in burst of autumn leaves and green light, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Arch-Druid Bran. Enraged primal maw transformation, bear claws manifesting, emerald eyes blazing, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Arch-Druid Bran. Turns to petrified ancient oak stump, stag antlers blooming with quiet moss, solid flat dark background #110E1B")
]

# 17. Lord Zephyr (Spellblade)
zephyr_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Lord Zephyr, The Swiftblade, wind fencer in sky-blue leather doublet #73BFCC, feathered duel hat, dual tempest rapiers humming with cyan storm gusts, solid flat dark background #110E1B"
zephyr_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Zephyr. Featherweight fencing glide, twin rapiers crossing in elegant salute, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Zephyr. Coiling backward in lunging stance, blades generating swirling miniature cyclone, solid flat dark background #110E1B"),
    ("blink", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Zephyr. Tempest-speed flash strike with twin cyan sonic booms and afterimages #73BFCC, solid flat dark background #110E1B"),
    ("whirlwind", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Zephyr. Spinning 360 degree whirlwind dance cutting air with razor-sharp gale blades, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Zephyr. Enraged gale stance, cloak whipped by fierce tempest winds, glowing turquoise eyes, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Zephyr. Bows gracefully as twin rapiers dissolve into a fading gentle breeze, solid flat dark background #110E1B")
]

# 18. Grand Alchemist Aurelius (Alchemist)
aurelius_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Grand Alchemist Aurelius, The Toxifier, mad apothecary in acid-stained leather apron #99D933, brass plague mask, belt racked with glowing chemical flasks, solid flat dark background #110E1B"
aurelius_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Alchemist Aurelius. Skulking stride, glass vials clinking, caustic green fumes venting from mask, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Alchemist Aurelius. Shaking volatile triangular concoction flask until violently glowing neon lime, solid flat dark background #110E1B"),
    ("eruption", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Alchemist Aurelius. Shattering chemical flask releasing boiling acid geyser and smoke #99D933, solid flat dark background #110E1B"),
    ("barrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Alchemist Aurelius. Hurling cluster of explosive chemical grenades across arena, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Alchemist Aurelius. Drinks mutagen brew, veins bulging with toxic green luminescence, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Grand Alchemist Aurelius. Acid bottles combust, dissolving into fizzing lime puddle and glass shards, solid flat dark background #110E1B")
]

# 19. Ragnar (Berserker)
ragnar_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Ragnar, The Frost Jarl, massive northern warlord in frostbitten fur and iron mail #59B3F2, horned bear skull mantle, dual colossal glacial battleaxes, solid flat dark background #110E1B"
ragnar_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Ragnar. Heavy brutal stomp, twin frost battleaxes resting on massive shoulders, icy breath, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Ragnar. Rearing back with both axes overhead, frosty rime coating muscles, solid flat dark background #110E1B"),
    ("cleave", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Ragnar. Cataclysmic double overhead cleave shattering stone with subzero ice spikes #59B3F2, solid flat dark background #110E1B"),
    ("prison", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Ragnar. Bellowing war shout freezing floor and erecting towering jagged ice fortress spikes, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Ragnar. Berserker fury, bear skull glowing blue, blizzards whipping around giant physique, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Ragnar. Drops to both knees, roaring one last defiance before freezing into permanent ice statue, solid flat dark background #110E1B")
]

# 20. Lady Seraphina (HolyTemplar)
seraphina_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Lady Seraphina, The Celestial Beacon, holy templar paladin in ornate gilded radiant plate armor #F2D980, glowing halo crown, flaming solar warhammer, solid flat dark background #110E1B"
seraphina_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Seraphina. Majestic stride, gilded plate armor gleaming, halo crown pulsing, warhammer upright, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Seraphina. Raising solar warhammer to the heavens, gathering radiant celestial sun flare, solid flat dark background #110E1B"),
    ("ward", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Seraphina. Erecting sacred geometric sun sanctuary dome with golden runic rays #F2D980, solid flat dark background #110E1B"),
    ("thunder", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Seraphina. Smashing warhammer down, discharging divine celestial thunderbolt beam from skies, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Seraphina. Archangel wings of pure golden light unfurling, solar aura searing surroundings, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Seraphina. Kneels in solemn prayer, gilded armor dissolving into ascending choir of light rays, solid flat dark background #110E1B")
]

def main():
    bosses_dir = PROJECT_ROOT / "Assets" / "_Game" / "Art" / "Characters" / "Bosses"

    print(">>> STARTING BATCH 9 ASSET GENERATION: EXTENDED BOSSES 11-20 <<<")
    
    generate_and_stitch_character("boss_11_vespera", vespera_base, vespera_anims, bosses_dir / "boss_11_vespera_sheet.png", 48, 48)
    generate_and_stitch_character("boss_12_kaelen_monk", kaelen_base, kaelen_anims, bosses_dir / "boss_12_kaelen_monk_sheet.png", 48, 48)
    generate_and_stitch_character("boss_13_morvath", morvath_base, morvath_anims, bosses_dir / "boss_13_morvath_sheet.png", 48, 48)
    generate_and_stitch_character("boss_14_gerald", gerald_base, gerald_anims, bosses_dir / "boss_14_gerald_sheet.png", 48, 48)
    generate_and_stitch_character("boss_15_theron", theron_base, theron_anims, bosses_dir / "boss_15_theron_sheet.png", 48, 48)
    generate_and_stitch_character("boss_16_bran", bran_base, bran_anims, bosses_dir / "boss_16_bran_sheet.png", 48, 48)
    generate_and_stitch_character("boss_17_zephyr", zephyr_base, zephyr_anims, bosses_dir / "boss_17_zephyr_sheet.png", 48, 48)
    generate_and_stitch_character("boss_18_aurelius", aurelius_base, aurelius_anims, bosses_dir / "boss_18_aurelius_sheet.png", 48, 48)
    generate_and_stitch_character("boss_19_ragnar", ragnar_base, ragnar_anims, bosses_dir / "boss_19_ragnar_sheet.png", 48, 48)
    generate_and_stitch_character("boss_20_seraphina", seraphina_base, seraphina_anims, bosses_dir / "boss_20_seraphina_sheet.png", 48, 48)

    print("\n>>> BATCH 9 COMPLETE: ALL 10 BOSSES GENERATED AND SAVED <<<")

if __name__ == "__main__":
    main()
