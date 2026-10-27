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
# BATCH 11 DEFINITIONS: Final Legendary Bosses (21 to 30)
# ==============================================================================

# 21. Darius the Voidstalker (ShadowDuelist)
darius_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Darius, The Voidstalker, nether-touched shadow assassin in obsidian hooded cloak #331A59, glowing purple rift eyes, dual ethereal void daggers dripping darkness, solid flat dark background #110E1B"
darius_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Darius. Gliding forward like a shadow, trailing faint purple mist, dual void daggers gripped reverse, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Darius. Crouching low into darkness, twin void daggers crackling with violet rift lightning, solid flat dark background #110E1B"),
    ("blink", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Darius. Vanishing into pitch-black rift tear and reappearing with cross-cutting void slice #331A59, solid flat dark background #110E1B"),
    ("slam", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Darius. Plunging void blades into stone, erupting abyssal shockwave rift of dark matter, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Darius. Enraged shadow form, shadowy tendrils and void eyes erupting from cloak, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Darius. Collapses into swirling black hole vortex that collapses into nothingness, solid flat dark background #110E1B")
]

# 22. Kallista the Beast Sovereign (Beastmaster)
kallista_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Kallista, The Beast Sovereign, apex huntress in saber-toothed pelt mantle and leather harness #B37340, bone necklace, recurve hunting longbow, solid flat dark background #110E1B"
kallista_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kallista. Predatory stalking stride, recurve bow in hand, predator furs fluttering, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kallista. Drawing bowstring to ear, notched barbed heavy hunting arrow glowing amber, solid flat dark background #110E1B"),
    ("vanguard", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kallista. Blowing carved bone war horn, summoning spectral hunting beasts #B37340, solid flat dark background #110E1B"),
    ("charge", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kallista. Leaping whirlwind blade slash with bone carving knives leaving amber trails, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kallista. Primal fury roar, glowing feline eyes, beast spirit aura enveloping her shoulders, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kallista. Drops bow and falls to knees, spirit beasts howling into the wind as she fades, solid flat dark background #110E1B")
]

# 23. High Magus Corvin the Glacial Warden (IceMage)
corvin_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of High Magus Corvin, The Glacial Warden, frost archmage in ornate icy-blue tiered robes #73CCF2, crown of glacial icicles, crystalline staff topped with weeping sapphire orb, solid flat dark background #110E1B"
corvin_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Magus Corvin. Levitating smoothly over frozen ground, frozen robes swirling with snowflakes, crystalline staff forward, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Magus Corvin. Both hands on weeping frost staff, channeling howling subzero glacial tempest vortex, solid flat dark background #110E1B"),
    ("prison", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Magus Corvin. Thrusting staff down, locking arena in towering glacial ice crystal pillars #73CCF2, solid flat dark background #110E1B"),
    ("eruption", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Magus Corvin. Detonating subterranean permafrost geyser showering razor ice shards, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Magus Corvin. Eyes blazing blinding cyan, blizzard storm sphere orbiting his floating body, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of High Magus Corvin. Glacial staff shatters into ice powder, robes freeze solid and shatter into frost crystals, solid flat dark background #110E1B")
]

# 24. Thorgar the Rune Forger (RuneKnight)
thorgar_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Thorgar, The Rune Forger, dwarven runic smith in heavy bronze-trimmed cyan runic plate armor #59CCD9, braided fiery beard, colossal glowing runic forging hammer, solid flat dark background #110E1B"
thorgar_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Thorgar. Heavy resolute march, runic smithing hammer resting on armored shoulder, beard swaying, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Thorgar. Rearing hammer overhead with both hands, cyan power runes igniting along metal, solid flat dark background #110E1B"),
    ("thunder", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Thorgar. Smashing anvil-hammer onto stone, detonating thunderstorm runic shockwave #59CCD9, solid flat dark background #110E1B"),
    ("cleave", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Thorgar. Colossal horizontal hammer cleave releasing sweeping electrical lightning crescent, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Thorgar. Bellowing smiths roar, entire body engulfed in molten cyan runic fire, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Thorgar. Leans upon his forging hammer as runic glyphs slowly flicker and extinguish, solid flat dark background #110E1B")
]

# 25. Matron Alyssa the Puppet Empress (Summoner)
alyssa_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Matron Alyssa, The Puppet Empress, gothic summoner in magenta-violet Victorian ballgown #D959A6, cracked porcelain smiling mask, hands outstretched with glowing razor strings, solid flat dark background #110E1B"
alyssa_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Matron Alyssa. Eerie marionette gliding steps, elegant gown rustling, fingers twitching invisible wires, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Matron Alyssa. Hands raised like puppeteer, pulling glowing magenta control threads tight, solid flat dark background #110E1B"),
    ("vanguard", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Matron Alyssa. Snapping fingers, animating screeching porcelain blade puppets #D959A6 from shadows, solid flat dark background #110E1B"),
    ("ward", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Matron Alyssa. Spinning razor thread web forming glowing protective geometric string barrier, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Matron Alyssa. Porcelain mask fractures revealing dark void, thousands of razor strings tearing the air, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Matron Alyssa. Snapping puppet strings, collapsing lifelessly like an abandoned doll upon the floor, solid flat dark background #110E1B")
]

# 26. Garrick the Behemoth Slayer (MonsterHunter)
garrick_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Garrick, The Behemoth Slayer, scarred veteran colossus hunter in heavy brown leather trench coat #BF6633, reinforced shoulder pauldron, colossal siege harpoon arbalest, solid flat dark background #110E1B"
garrick_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Garrick. Heavy tracking stride, siege harpoon resting across chest, ragged coat swaying, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Garrick. Bracing heavy siege arbalest on knee, locking massive steel cable winch, solid flat dark background #110E1B"),
    ("barrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Garrick. Unleashing devastating barrage of barbed steel harpoons with explosive muzzle flare #BF6633, solid flat dark background #110E1B"),
    ("slam", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Garrick. Smashing heavy spiked siege anchor into ground triggering concussive shockwave, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Garrick. Teeth gritted in grim fury, loading oversized incendiary dreadnought bolts, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Garrick. Falls to one knee, planting harpoon in stone, spitting blood before collapsing, solid flat dark background #110E1B")
]

# 27. Roland the Unyielding Bulwark (Paladin)
roland_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Roland, The Unyielding Bulwark, legendary crusader in gold-trimmed stone-white fortress armor #E6D9B3, winged greathelm, massive impenetrable tower pavise shield, flanged war mace, solid flat dark background #110E1B"
roland_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Roland. Immovable fortress march, giant tower shield held firm, heavy flanged mace ready, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Roland. Pulling heavy war mace back, bracing shield against chest with golden glow, solid flat dark background #110E1B"),
    ("cleave", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Roland. Crushing horizontal mace sweep releasing booming radiant golden shockwave #E6D9B3, solid flat dark background #110E1B"),
    ("ward", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Roland. Slamming tower shield into earth, projecting unbreakable radiant golden aegis barrier, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Roland. Golden holy radiance pulsing through cracked armor, war mace ablaze with divine light, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Roland. Remains standing upright locked behind tower shield, head bowed in honorable silence, solid flat dark background #110E1B")
]

# 28. Kaelen the Bloodhound (WitchHunter)
witchhunter_kaelen_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Kaelen, The Bloodhound, grim inquisitor in wide-brimmed hat and blood-crimson duster coat #A63333, silver inquisitor crest, double-barreled shotgun hand cannon and silver rapier, solid flat dark background #110E1B"
witchhunter_kaelen_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kaelen. Confident huntsman stride, duster billowing, shotgun leveled forward, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kaelen. Cocking twin shotgun hammers, silver rapier raised in guard, eyes glowing under hat brim, solid flat dark background #110E1B"),
    ("thunder", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kaelen. Point-blank shotgun blast erupting blinding incendiary flame and thunderous smoke #A63333, solid flat dark background #110E1B"),
    ("blink", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kaelen. Shadow-roll dash reappearing with lightning-fast silver rapier thrust, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kaelen. Relentless zealot fury, loading silver sanctified buckshot, blood red eyes flaming, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Kaelen. Stumbles back, tips wide-brim hat over eyes, collapsing into crimson embers, solid flat dark background #110E1B")
]

# 29. Lady Isabelle the Cinder Knight (Spellblade)
isabelle_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Lady Isabelle, The Cinder Knight, spellblade champion in blackened volcanic plate #F26626 with glowing molten lava veins, flaming curved scimitar blade, solid flat dark background #110E1B"
isabelle_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Isabelle. Fluid duelist glide, leaving burning footprints, flaming scimitar held low, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Isabelle. Crouched dueling posture, blade heating to blinding white-hot molten magma, solid flat dark background #110E1B"),
    ("eruption", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Isabelle. Slashing upward, erupting towering geyser of volcanic magma and lava boulders #F26626, solid flat dark background #110E1B"),
    ("charge", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Isabelle. Spinning corkscrew fire dash carving fiery scorched ring into arena stone, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Isabelle. Magma armor erupting into continuous blazing fire cloak, twin flaming swords ignited, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lady Isabelle. Blackened armor cools to brittle gray ash, crumbling softly into dormant cinder dust, solid flat dark background #110E1B")
]

# 30. Lord Cassian the Sovereign Duelist (Rogue)
cassian_base = "16bitscene, pixelsprite, masterpiece 2D top-down 3/4 view pixel art boss sprite of Lord Cassian, The Sovereign Duelist, peerless fencing master in midnight royal blue velvet tunic #4D66BF, silver filigree masquerade mask, ornate ceremonial silver foil rapier, solid flat dark background #110E1B"
cassian_anims = [
    ("walk", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Cassian. Aristocratic fencing advance, left arm tucked behind back, rapier point leveled, solid flat dark background #110E1B"),
    ("windup", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Cassian. Elegant lunging windup, silver blade humming with concentrated electric blue pierce aura, solid flat dark background #110E1B"),
    ("blink", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Cassian. Vanishing into thin air, reappearing with flurry of five sonic blue thrusts #4D66BF, solid flat dark background #110E1B"),
    ("cleave", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Cassian. Grand sweeping parry-riposte arc discharging slicing sapphire crescent blade, solid flat dark background #110E1B"),
    ("enrage", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Cassian. Sovereign fury, cape whipping, silver mask glowing with peerless dueling pride, solid flat dark background #110E1B"),
    ("death", "16bitscene, pixelsprite, Consistent 48x48 pixel art of Lord Cassian. Performs one final flawless fencing salute before gracefully dissolving into starry blue motes, solid flat dark background #110E1B")
]

def main():
    bosses_dir = PROJECT_ROOT / "Assets" / "_Game" / "Art" / "Characters" / "Bosses"

    print(">>> STARTING BATCH 11 ASSET GENERATION: FINAL LEGENDARY BOSSES 21-30 <<<")
    
    generate_and_stitch_character("boss_21_darius", darius_base, darius_anims, bosses_dir / "boss_21_darius_sheet.png", 48, 48)
    generate_and_stitch_character("boss_22_kallista", kallista_base, kallista_anims, bosses_dir / "boss_22_kallista_sheet.png", 48, 48)
    generate_and_stitch_character("boss_23_corvin", corvin_base, corvin_anims, bosses_dir / "boss_23_corvin_sheet.png", 48, 48)
    generate_and_stitch_character("boss_24_thorgar", thorgar_base, thorgar_anims, bosses_dir / "boss_24_thorgar_sheet.png", 48, 48)
    generate_and_stitch_character("boss_25_alyssa", alyssa_base, alyssa_anims, bosses_dir / "boss_25_alyssa_sheet.png", 48, 48)
    generate_and_stitch_character("boss_26_garrick", garrick_base, garrick_anims, bosses_dir / "boss_26_garrick_sheet.png", 48, 48)
    generate_and_stitch_character("boss_27_roland", roland_base, roland_anims, bosses_dir / "boss_27_roland_sheet.png", 48, 48)
    generate_and_stitch_character("boss_28_witchhunter_kaelen", witchhunter_kaelen_base, witchhunter_kaelen_anims, bosses_dir / "boss_28_witchhunter_kaelen_sheet.png", 48, 48)
    generate_and_stitch_character("boss_29_isabelle", isabelle_base, isabelle_anims, bosses_dir / "boss_29_isabelle_sheet.png", 48, 48)
    generate_and_stitch_character("boss_30_cassian", cassian_base, cassian_anims, bosses_dir / "boss_30_cassian_sheet.png", 48, 48)

    print("\n>>> BATCH 11 COMPLETE: ALL 10 BOSSES GENERATED AND SAVED <<<")

if __name__ == "__main__":
    main()
