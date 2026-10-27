"""Pack already cleaned, aligned RGBA frames. Never resize/recenter/key backgrounds."""
import argparse
import json
from pathlib import Path
from PIL import Image

def pack(folder, output, expected_count, fps, loop, pivot=(0.5,0.1), state=None):
    files=sorted(folder.glob('*.png'))
    if len(files)!=expected_count:
        raise ValueError(f'Expected {expected_count} PNGs, found {len(files)}')
    if fps<=0 or expected_count<1: raise ValueError('Positive frame count and FPS required')
    if not all(0<=v<=1 for v in pivot): raise ValueError('Pivot must be normalized')
    if output.exists() or output.with_suffix('.json').exists(): raise ValueError('Output exists; choose a new filename')
    frames=[]
    for file in files:
        with Image.open(file) as im:
            if im.mode!='RGBA': raise ValueError(f'{file.name}: RGBA required after cleanup')
            frame=im.copy()
        alpha=frame.getchannel('A')
        lo,hi=alpha.getextrema()
        if lo==255: raise ValueError(f'{file.name}: opaque background; clean alpha first')
        if hi==0: raise ValueError(f'{file.name}: empty frame (omit terminal clear frame; despawn in runtime)')
        if frames and frame.size!=frames[0].size: raise ValueError('All frames must have identical canvas size')
        frames.append(frame)
    w,h=frames[0].size
    strip=Image.new('RGBA',(w*len(frames),h))
    for i,frame in enumerate(frames): strip.paste(frame,(i*w,0))
    output.parent.mkdir(parents=True,exist_ok=True)
    strip.save(output)
    meta=dict(state=state or output.stem,cell_width=w,cell_height=h,frame_count=len(frames),fps=fps,loop=loop,pivot=list(pivot),frames=[f.name for f in files],release_frame=None,facing='front_right',sockets={},status='packed_not_approved')
    output.with_suffix('.json').write_text(json.dumps(meta,indent=2)+'\n',encoding='utf-8')
    return meta

if __name__=='__main__':
    p=argparse.ArgumentParser(description=__doc__)
    p.add_argument('folder',type=Path);p.add_argument('output',type=Path)
    p.add_argument('--count',type=int,required=True);p.add_argument('--fps',type=float,required=True)
    p.add_argument('--loop',action='store_true');p.add_argument('--pivot',nargs=2,type=float,default=[0.5,0.1])
    p.add_argument('--state',required=True,choices=['idle','run','attack','cast','hurt','death','spawn','dash','muzzle','travel','impact','telegraph','active','dissipate'])
    a=p.parse_args()
    try: print(json.dumps(pack(a.folder,a.output,a.count,a.fps,a.loop,a.pivot,a.state),indent=2))
    except ValueError as e: p.exit(1,str(e)+'\n')
