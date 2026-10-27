#!/usr/bin/env python3
"""
Final Boss: Eternal Dungeon — Automated Sprite Sheet & Outline Post-Processor
=============================================================================
Usage:
  python asset_prompt/workflows/process_sprites_to_unity.py <input_folder_of_frames> <output_png_path> [frame_width] [frame_height]

Example (Lich Lord 6 frames -> 192x32 sheet):
  python asset_prompt/workflows/process_sprites_to_unity.py raw_drop/lich_lord Assets/_Game/Art/Characters/lich_lord/char_lich_lord_sheet.png 32 32

What this script does automatically (0 GB GPU load):
1. Loads all .png frames in alphabetical order (e.g., 01.png .. 06.png) from <input_folder_of_frames>.
2. Flood-fills / keys out the solid background color from the 4 corners into clean RGBA32 alpha=0 transparency.
3. Resizes each frame using crisp Nearest-Neighbor sampling to exact (frame_width x frame_height).
4. Enforces the mandatory 1px #110E1B dark outer rim outline around all opaque character pixels.
5. Stitches all frames horizontally into a single Unity-ready sprite sheet PNG.
"""
import sys
import os
from pathlib import Path
from PIL import Image

OUTLINE_RGBA = (17, 14, 27, 255)  # #110E1B mandatory 1px dark rim

def remove_solid_bg(img: Image.Image, tolerance: int = 38) -> Image.Image:
    img = img.convert("RGBA")
    w, h = img.size
    px = img.load()
    # Sample top-left corner as background color reference
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

def enforce_1px_dark_outline(img: Image.Image) -> Image.Image:
    w, h = img.size
    src = img.load()
    out = img.copy()
    dst = out.load()
    for y in range(h):
        for x in range(w):
            if src[x, y][3] == 0:
                # Check 4-connected neighbors for opaque pixels
                for nx, ny in ((x - 1, y), (x + 1, y), (x, y - 1), (x, y + 1)):
                    if 0 <= nx < w and 0 <= ny < h and src[nx, ny][3] > 128:
                        dst[x, y] = OUTLINE_RGBA
                        break
    return out

def process_folder(in_dir: Path, out_file: Path, fw: int = 32, fh: int = 32):
    files = sorted([p for p in in_dir.iterdir() if p.suffix.lower() in (".png", ".jpg", ".webp")])
    if not files:
        print(f"No images found in {in_dir}")
        return
    processed_frames = []
    for f in files:
        im = Image.open(f)
        im = remove_solid_bg(im)
        if im.size != (fw, fh):
            im = im.resize((fw, fh), Image.Resampling.NEAREST)
        im = enforce_1px_dark_outline(im)
        processed_frames.append(im)

    sheet = Image.new("RGBA", (fw * len(processed_frames), fh), (0, 0, 0, 0))
    for idx, frame in enumerate(processed_frames):
        sheet.paste(frame, (idx * fw, 0))

    out_file.parent.mkdir(parents=True, exist_ok=True)
    sheet.save(out_file, "PNG")
    print(f"Saved {len(processed_frames)}-frame sheet ({sheet.width}x{sheet.height} px) -> {out_file}")

if __name__ == "__main__":
    if len(sys.argv) < 3:
        print(__doc__)
        sys.exit(0)
    in_folder = Path(sys.argv[1])
    out_path = Path(sys.argv[2])
    width = int(sys.argv[3]) if len(sys.argv) > 3 else 32
    height = int(sys.argv[4]) if len(sys.argv) > 4 else 32
    process_folder(in_folder, out_path, width, height)
