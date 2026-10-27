# Complete Beginner’s Pipeline: Generating & Integrating Art + Audio for *Final Boss: Eternal Dungeon*

> **Target Audience:** Zero prior experience in pixel art, sound design, or AI asset pipelines.
> **Goal:** Turn the 356 prompt files inside [`asset_prompt/`](./00_MASTER_INDEX.md) into crisp, transparent sprite sheets (`.png`) and sound effects (`.wav`) wired directly into Unity 6.

---

## 1. The Big Picture (Why You Don’t Just Type into ChatGPT and Drop It into Unity)

If you ask a standard AI image generator (like Midjourney or ChatGPT) for a `"32x32 pixel art Lich Lord"`, it will actually generate a **`1024x1024` high-resolution painting that *looks* blocky**, with blurry edges, fake grey-and-white checkerboards instead of real transparency, and inconsistent pixel sizes ("mixels").

To get **real, game-ready assets** with zero prior experience, you follow a **4-Step Assembly Line**:

```
[1. Prompt File (.md)] 
       │
       ▼
[2. AI Generator (Base Frame -> Animation Frames)]
       │
       ▼
[3. Post-Process (Snap to 32x32 Grid + Remove Background + Pack Horizontal Strip)]
       │
       ▼
[4. Drop into Assets/_Game/Art/... or Assets/_Game/Audio/... in Unity]
```

---

## 2. Which AI Models & Tools Should You Use? (Ranked for Beginners)

### A. For 2D Pixel Art & Sprite Sheets (`*_art.md`)

| Option | Tool / Model Name | Best For | Why Choose This? |
| :--- | :--- | :--- | :--- |
| **#1 Easiest (Recommended)** | **PixelLab.ai** (Web App or Aseprite/Pixelorama Plugin) | Characters (`32x32`), Bosses (`48x48`), Animations, Directional Views, Tilesets | **Purpose-built for pixel art games.** Generates native `32x32` / `48x48` pixel grids with real transparent backgrounds, locks your base character (`Frame 1`), and generates walk/attack animations directly from that reference. |
| **#2 Best General Web Tool** | **Scenario.com** or **Leonardo.ai** *(using Pixel Art / Retro Diffusion models)* | Icons (`16x16`), Weapons, Armors, Props, UI Frames | Great batch generation for 150 weapons, 80 spell icons, and 30 armors. Use the **"Character Reference" / "Image-to-Image"** slider at `65%–75%` strength to generate frames 2–6 from Frame 1. |
| **#3 Best Free / Local** | **ComfyUI** + **Flux.1-dev** (or **SDXL**) + **Retro Diffusion / PixelArt XL LoRA** | Unlimited free local generation on your own GPU | Zero subscription cost; lets you automate all 178 assets via a batch script, plus auto-downscale with the `PixelOE` or `Nearest-Neighbor` node. |
| **#4 Quickest Concepting** | **ChatGPT (GPT-4o Image Gen)** or **Midjourney v6.1** (`--sref`) | Creating the "Master Reference Sheet" (`Frame 1`) | Understands complex descriptions best, **but** outputs `1024x1024` images that **must** be downscaled to `32x32` using a pixel-snapper (see Section 4). |

### B. For Sound Effects & Dungeon Ambience (`*_audio.md`)

| Option | Tool / Model Name | Best For | Why Choose This? |
| :--- | :--- | :--- | :--- |
| **#1 Easiest & Best SFX** | **ElevenLabs Sound Effects** (`elevenlabs.io/sound-effects`) | Combat SFX, Spells, Boss Roars, UI Clicks, Ambience | You literally copy-paste the prompt from any `*_audio.md` file, set duration (`0.5s` to `2.0s`), and download clean audio. |
| **#2 Best Free Retro/Game SFX** | **Chiptone** (`sfbgames.itch.io/chiptone`) or **jsfxr** (`sfxr.me`) | 16-bit Hits, Projectiles, Coins, UI Beeps, Jumps/Dashes | 100% free, instant one-click `.wav` export, zero background noise, classic crisp roguelite game feel. |
| **#3 Best for Biome Music & 30s Loops** | **Suno v4** / **Udio** / **Stable Audio Open** | `08_Biomes/` 30-second ambient loops & combat music | Generates full dark-fantasy orchestral/synth loops from the Biome prompts. |

---

## 3. Free Software to Install on Your PC Before Starting (Takes 10 Minutes)

You only need **3 lightweight, free tools** on Windows:
1. **Pixelorama** *(Free on Steam / itch.io)* or **Libresprite / Aseprite**:
   - Used to view `32x32` sprites at actual pixel size and export horizontal sprite strips (`192x32 px`).
   - *(Note: We also have the `game-art-automation` skill installed in this workspace, which means **I can automate Pixelorama or Python image packing for you** whenever you put raw images in a folder!)*
2. **Audacity** *(Free audio editor — `audacityteam.org`)* or **ffmpeg**:
   - Used to trim empty silence off the start of `.wav` files so sound effects play the exact millisecond you click attack.
3. **Python `rembg` & `Pillow`** *(Already available via Python)*:
   - Used to automatically strip backgrounds, resize `1024x1024` AI outputs down to `32x32`, and stitch 6 frames into a single `192x32` `.png` sheet.

---

## 4. Step-by-Step Walkthrough: How to Generate Your First Complete Character (`Lich Lord`)

Open [`01_Characters/lich_lord_art.md`](./01_Characters/lich_lord_art.md) side-by-side and follow these exact steps:

### Step 1: Generate the Base Character (`Frame 01 — Idle`)
1. Copy the **Base Design Prompt** from **Section 1.2** of [`lich_lord_art.md`](./01_Characters/lich_lord_art.md):
   > `Masterpiece 2D top-down 3/4 view pixel art character sprite of Lich Lord (Arch-Necromancer of the Eternal Crypt)...`
2. **If using PixelLab.ai:**
   - Select **"Create Character"** -> Set canvas size to **`32x32`** (or `64x64` then downscale `50%` Nearest Neighbor).
   - Paste the prompt, set view to **Low Top-Down (3/4)**, enable **Transparent Background**, and click **Generate**.
   - Pick the best result where the silhouette is clear, the robe is royal purple (`#6E2BC4`), and the eyes/staff glow emerald (`#30E680`).
3. **If using ChatGPT / Leonardo / Midjourney:**
   - Paste the prompt and add: `"centered single character on a solid flat magenta #FF00FF background, large clean pixel grid"`.
   - Save the image as `lich_lord_frame_01.png`.

### Step 2: Generate the Animation Variations (`Frames 02 to 06`) with 100% Consistency
**Never generate Frame 2 from scratch**—otherwise the AI will invent a completely different character! Always lock **Frame 1** as your **Reference Image**:

- **In PixelLab.ai:**
  1. Click **"Animate"** on your locked `Frame 01` character.
  2. Choose **Walk** (`2 frames` or `4 frames`), **Attack/Cast**, **Hit**, and **Death**—or paste the exact frame prompts from **Section 2** of [`lich_lord_art.md`](./01_Characters/lich_lord_art.md) (`Frame 02 — Walk_01`, `Frame 03 — Walk_02`, `Frame 04 — Attack_Cast_01`, `Frame 05 — Hit_React_01`, `Frame 06 — Death_01`).
  3. Export as a **Horizontal Sprite Strip** (`192x32 px`).

- **In Leonardo.ai / Scenario / ComfyUI (Image-to-Image Workflow):**
  1. Upload `lich_lord_frame_01.png` into the **Image Guidance / Character Reference (`IP-Adapter` + `Img2Img`)** slot.
  2. Set **Image Strength / Denoising** to **`0.35` – `0.45`** (this forces the AI to keep 65% of the exact pixels of Frame 1 and only move the arms/robes).
  3. Paste the **Frame 02 (`Walk_01`)** prompt from [`lich_lord_art.md`](./01_Characters/lich_lord_art.md) -> Save as `frame_02.png`.
  4. Repeat for **Frame 03 (`Walk_02`)**, **Frame 04 (`Attack_Cast_01`)**, **Frame 05 (`Hit_React_01`)**, and **Frame 06 (`Death_01`)**.

### Step 3: Generate the Associated VFX & Skill Icons (Same File!)
Still inside [`lich_lord_art.md`](./01_Characters/lich_lord_art.md):
1. Scroll to **Section 3.1 (`Basic Attack Projectile`)** -> Copy the prompt -> Generate the 3-frame `48x16 px` Soul Bolt strip (`Cast Flash`, `Traveling Bolt`, `Impact Shatter`).
2. Scroll to **Section 3.2 (`Signature Nova`)** -> Copy the prompt -> Generate the 4-frame `256x64 px` Death Wave ring strip.
3. Scroll to **Section 4 (`Complete Class Skill Tree Assets`)** -> Copy the `16x16 Skill Icon Prompt` for each skill as you need them.

### Step 4: Pack & Clean the Sprite Sheet (Automated!)
If you generated 6 separate images (`frame_01.png` .. `frame_06.png`) at high resolution (e.g. `1024x1024`), you don't even have to resize or stitch them by hand!
- Just drop your raw AI images into a folder (for example, `raw_assets/lich_lord/`) and tell me in chat:
  > *"Process raw_assets/lich_lord/ into the Unity sprite sheet"*
- I will run a Python script that automatically:
  1. Removes the background to clean `RGBA32` transparency.
  2. Downscales each frame to exact **`32x32 px`** (or `48x48` for bosses, `16x16` for icons) using crisp **Nearest-Neighbor** sampling.
  3. Adds the mandatory `1px` `#110E1B` dark outline if missing.
  4. Stitches Frames 1–6 horizontally into `Assets/_Game/Art/Characters/lich_lord/char_lich_lord_sheet.png` (`192x32 px`).

---

## 5. Step-by-Step Walkthrough: How to Generate the Audio (`*_audio.md`)

Open [`01_Characters/lich_lord_audio.md`](./01_Characters/lich_lord_audio.md):

1. Go to **[ElevenLabs Sound Effects](https://elevenlabs.io/sound-effects)** (or Stable Audio).
2. In **Section 2** of [`lich_lord_audio.md`](./01_Characters/lich_lord_audio.md), copy the prompt under **Basic Attack — Soul Bolt Launch (`CueIds.PlayerSoulBolt`)**:
   > `Dark fantasy 16-bit retro-modern hybrid game sound effect for Lich Lord - Basic Attack — Soul Bolt Launch. Timbre: Cavernous sub-bass bone resonance + reversed cathedral choir whispers + crackling emerald soul-fire FM synthesis...`
3. Set the duration slider in ElevenLabs to **`0.5s` – `1.0s`** and click **Generate**.
4. Audition the 4 generated variations, download the punchiest one, and save it to the exact path listed in **Section 4** of the markdown file:
   - `Assets/_Game/Audio/SFX/Characters/lich_lord/sfx_lich_lord_player_soul_bolt.wav`
5. Repeat for the remaining 5 core cues (`player_death_wave`, `player_summon`, `player_dash`, `player_hit`, `player_death`).

> **Pro Tip for Audio:** If an AI audio tool generates `.mp3` files or adds a `0.2s` silent gap at the start of the clip, just drop the raw audio files into a folder and ask me: *"Convert and trim the audio files in raw_audio/"* — I will batch-convert them to `44100 Hz` 16-bit Mono `.wav`, strip leading silence, and normalize peak volume to `-1.5 dBFS` automatically.

---

## 6. Recommended Order of Production (Don’t Do All 356 Files at Once!)

With 178 asset pairs, trying to generate everything on Day 1 is overwhelming. Follow this **4-Batch Priority Roadmap** so you can see your game transform inside Unity within your very first hour:

### Batch 1: The "Vertical Slice" Core (1.5 Hours — Immediately Playable!)
Generate only these **12 asset pairs** first to replace 80% of what you see on screen during Wave 1–5:
1. **1 Overlord:** [`01_Characters/lich_lord_art.md`](./01_Characters/lich_lord_art.md) & [`lich_lord_audio.md`](./01_Characters/lich_lord_audio.md)
2. **2 Core Summons:** [`02_Summons/bone_sentinel_art.md`](./02_Summons/bone_sentinel_art.md) & [`crypt_archer_art.md`](./02_Summons/crypt_archer_art.md) (+ audio)
3. **5 Wave-1 Invaders:** `inv_01_swordsman`, `inv_02_archer`, `inv_05_knight`, `inv_08_fire_mage`, `inv_11_priest` (`03_Invaders/`)
4. **1 Milestone Boss:** [`04_Bosses/boss_01_valerius_art.md`](./04_Bosses/boss_01_valerius_art.md) (+ audio)
5. **1 Arena Biome:** [`08_Biomes/biome_01_forgotten_necropolis_art.md`](./08_Biomes/biome_01_forgotten_necropolis_art.md) (+ audio)
6. **Combat HUD & Pickups:** [`10_UI_And_HUD/ui_02_combat_hud_and_bars_art.md`](./10_UI_And_HUD/ui_02_combat_hud_and_bars_art.md) & [`09_Events_And_Props/loot_pickups_and_chests_art.md`](./09_Events_And_Props/loot_pickups_and_chests_art.md)

### Batch 2: All 5 Overlords, All 10 Summons & Core Magic (Day 2)
- Remaining 4 Overlords (`demon_monarch`, `vampire_queen`, `shadow_reaper`, `cursed_golem`).
- Remaining 8 Summons (`02_Summons/`).
- Top 6 Magic Schools (`Fire`, `Ice`, `Lightning`, `Necromancy`, `Blood`, `Shadow` in `05_Magic_Schools/`).
- All 5 Interactive Shrines (`09_Events_And_Props/shrine_*`).

### Batch 3: Full Roster of Invaders, Bosses & Biomes (Days 3–4)
- Remaining 25 Invaders (`03_Invaders/`).
- Remaining 29 Legendary Bosses (`04_Bosses/`).
- Remaining 19 Dungeon Biomes (`08_Biomes/`).

### Batch 4: Equipment Icons & Deep Skill Tree Icons (Days 5–6 — Easy Batching)
- Because `16x16` icons have **no animation frames**, you can batch-generate the 15 Weapon Categories (`06_Weapons/`), 30 Armors (`07_Armors/`), and remaining 14 Magic Schools (`05_Magic_Schools/`) very rapidly using a single locked `16x16` icon style preset.

---

## 7. How Unity Integration Works (What Happens After You Generate Files)

Every single `_art.md` and `_audio.md` file ends with a **Unity Import & Placeholder Replacement Table** specifying the exact destination path (e.g., `Assets/_Game/Art/Characters/lich_lord/char_lich_lord_sheet.png`).

When you have generated even **one** asset (or a folder of raw images/audio):
1. Place the files into the project (or a `raw_assets/` drop folder).
2. Tell me in chat: **"Integrate the new assets I added."**
3. I will automatically:
   - Resize, remove backgrounds, and pack your sprite strips if needed.
   - Configure Unity’s `TextureImporter` (`FilterMode.Point`, `TextureImporterCompression.Uncompressed`, `PPU = 32` or `16`, `Pivot = (0.5, 0.1)`, sprite sheet slicing rects).
   - Wire the sliced sprites and `.wav` `AudioClip`s into `LichLordVisualPresenter`, `InvaderView`, `DungeonArenaPresenter`, `HudPresenter`, and `AudioCuePresenter` in place of `PlaceholderSprites`.
   - Run the automated PlayMode & EditMode test suites in Unity to verify zero regressions.


---

## 8. 100% Free & Open-Source Local Pipeline (For Your RTX 5070 Ti 12GB GPU)

Because your machine has an **NVIDIA GeForce RTX 5070 Ti Laptop GPU with 12 GB VRAM**, you do **not** need any paid cloud subscriptions or restrictive free tiers. You can generate **100% of the 356 art and audio files locally on your own GPU for $0**, with zero daily credit limits.

### 8.1 The Open-Source Stack (All 100% Free / MIT / Apache / Open-Weights)

| Task | Open-Source Tool | Exact Model to Download | Why It Fits Your 12GB RTX 5070 Ti |
| :--- | :--- | :--- | :--- |
| **1. GUI Launcher** | **[Stability Matrix](https://github.com/LykosAI/StabilityMatrix)** *(1-click open-source manager for Windows)* | Installs **ComfyUI** (portable) + Python + CUDA automatically | Zero command-line setup; 1-click model browser for HuggingFace & Civitai. |
| **2. Pixel Art Generator** | **ComfyUI** (installed via Stability Matrix) | **Option A (Ultra-Light 2 GB — Recommended for Laptop):** `All-In-One-Pixel-Model` / `RetroDiffusion (SD 1.5)` (`~2.0 GB`) + `AnimateDiff-Lightning` (for instant 6-frame walk/attack strips!)<br>**Option B (4-Step Turbo 3.2 GB):** `DreamShaper XL Lightning (FP16)` + `Pixel Art XL v1.1 LoRA` | Option A uses only **~2.4 GB VRAM** and generates a `256x256` / `512x512` sprite in **0.4 seconds** with zero laptop heat! |
| **3. Pixel Downscaler & Transparency** | **Python (`rembg` + `Pillow` / `PixelOE`)** + **[Pixelorama](https://orama-interactive.itch.io/pixelorama)** | `u2net` (built into `rembg`) | Strips background to `RGBA32`, snaps to exact `32x32` / `48x48` / `16x16` grid, adds `1px` `#110E1B` outline, and packs horizontal strips. |
| **4. AI Sound Effects & Ambience** | **[AudioLDM-2](https://huggingface.co/cvssp/audioldm2)** or **[TangoFlux](https://huggingface.co/declare-lab/TangoFlux)** *(or ComfyUI Stable Audio Open node)* | `declare-lab/TangoFlux` or `stabilityai/stable-audio-open-1.0` | Runs locally in ~3 seconds per `.wav` file on 12GB VRAM; reads the exact text prompts from your `*_audio.md` files! |
| **5. Crisp 16-Bit Retro SFX Layer** | **[jsfxr / sfxr.me](https://sfxr.me/)** & **[Chiptone](https://sfbgames.itch.io/chiptone)** | Procedural synth (0 MB, runs in browser/offline) | Instant 1-click `.wav` export for hits, projectiles, coins, dashes, and UI clicks. |

---

### 8.2 Step-by-Step Local Setup (One-Time 15-Minute Setup)

#### Step 1: Install ComfyUI via Stability Matrix
1. Download **[Stability Matrix](https://github.com/LykosAI/StabilityMatrix/releases)** (free, open-source).
2. Open it -> Click **Add Package** -> Choose **ComfyUI**.
3. Open the **Model Browser** tab inside Stability Matrix and download these 3 free open-weights files:
   - **Checkpoint:** `Juggernaut XL` or `SDXL Base 1.0` (`~6.5 GB`)
   - **LoRA:** Search `"Pixel Art XL"` (by *nerijs*) (`~170 MB`) — *this forces every output into a clean 1:8 pixel grid!*
   - **IP-Adapter (for animation consistency):** Install `ComfyUI_IPAdapter_plus` from the Extensions tab.

#### Step 2: How to Generate a Complete 6-Frame Sprite Sheet in ComfyUI
1. **Generate Frame 01 (Idle):**
   - Load `SDXL Base` + `Pixel Art XL LoRA` (weight `0.85`).
   - Set resolution to **`1024x1024`** (because `Pixel Art XL` draws exact `8x8` pixel blocks, so `1024 ÷ 8 = 128`, which downscales cleanly by `4x` to **`32x32`**!).
   - Paste **Section 1.2 Base Design Prompt** from any `_art.md` file + append: `, pixel art, flat solid magenta background #FF00FF, centered single character`.
   - Click **Queue Prompt** (generates in 2 seconds) until you get a Frame 01 you love.
2. **Generate Frames 02–06 (Walk, Attack, Hit, Death) with Locked Consistency:**
   - Feed your saved `Frame 01` image into two nodes:
     1. **`Load Image` -> `IPAdapter` (weight `0.80`)**: Locks the exact character colors, armor, and proportions.
     2. **`Load Image` -> `VAE Encode` -> `KSampler` (`denoise = 0.42`)**: Keeps the pose anchored while allowing arms/legs/weapons to move.
   - Paste the **Frame 02 (`Walk_01`)** prompt from **Section 2** of the `_art.md` file -> Queue -> Save as `02.png`.
   - Repeat for `03.png` (`Walk_02`), `04.png` (`Attack`), `05.png` (`Hit`), and `06.png` (`Death`).

#### Step 3: How to Generate All Audio Locally for Free
You can run **TangoFlux** or **AudioLDM-2** directly in Python on your RTX 5070 Ti to batch-generate every single `.wav` file from your `*_audio.md` prompts without clicking anything!
- Or, for quick manual SFX, combine **Stable Audio Open** (inside ComfyUI's built-in audio nodes) with **[Chiptone](https://sfbgames.itch.io/chiptone)** for crisp 16-bit transients.


---

## 9. Laptop GPU Safety & Sequential Execution Rules (RTX 5070 Ti 12 GB Laptop)

Because you are running on a **Laptop GPU (RTX 5070 Ti 12 GB VRAM)**, thermal management and VRAM ceiling (`12,227 MiB`) are critical. Never run Image Generation, Audio Generation, and Unity PlayMode rendering simultaneously.

### 9.1 The 5 Golden Rules for Your Laptop GPU
1. **Strictly `Batch Size = 1` (One Generation at a Time):**
   - Always keep `batch_size = 1` in ComfyUI (`Empty Latent Image` node). Generating 1 image at a time uses ~6.8 GB VRAM and finishes in ~2 seconds without spiking your laptop’s power draw or fan thermals.
2. **Never Load the Image Model and Audio Model at the Same Time:**
   - `SDXL + IP-Adapter` uses ~7.5 GB VRAM. `Stable Audio Open` uses ~5.2 GB VRAM. Loading both together exceeds 12 GB and spills into slow system RAM (causing 10x slowdowns or Out-Of-Memory crashes).
   - **Rule:** Finish your **Art batch** first -> Click **"Free Model and Node Cache"** in ComfyUI Manager -> Then run your **Audio batch**.
3. **Launch ComfyUI with `--lowvram` or `--normalvram --reserve-vram 1.5`:**
   - In Stability Matrix -> ComfyUI Launch Options, add `--reserve-vram 1.5` so Windows, your browser, and Unity Editor always have 1.5 GB of breathing room.
4. **Pause or Close Unity Editor During Heavy Generation Sessions:**
   - Unity 6 with URP 2D holds ~1.2 GB–2.0 GB of VRAM just sitting open. Close Unity (or minimize it on an empty scene) while generating sprites/audio, then open Unity once the files are ready to import.
5. **Add a `2.0s` Cooldown Between Automated Queue Items:**
   - If running a Python script that loops through prompts sequentially, include `time.sleep(2.0)` between each single generation so laptop GPU core temperatures stay in the safe `65°C–74°C` range instead of thermal-throttling at `87°C`.

### 9.2 Sequential Single-Item Workflow Order
```
[Phase 1: Art Only — Batch Size 1]
  Generate Frame 1 -> Wait 2s -> Generate Frame 2 -> ... -> Frame 6
  (Unload ComfyUI Image Models / Click "Free VRAM")
       │
       ▼
[Phase 2: CPU Post-Processing — 0 GB GPU VRAM]
  Python script removes background, snaps to 32x32, adds #110E1B outline, stitches 192x32 strip
       │
       ▼
[Phase 3: Audio Only — Batch Size 1]
  Load Stable Audio Open -> Generate 1 WAV at a time -> Unload ComfyUI
       │
       ▼
[Phase 4: Open Unity 6 & Import]
```
