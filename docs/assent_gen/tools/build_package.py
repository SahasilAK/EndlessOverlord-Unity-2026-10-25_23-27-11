"""Rebuild evidence inventories, catalog briefs and ComfyUI graphs. No generation/import."""
import csv
import json
import re
from pathlib import Path
from PIL import Image

OUT = Path(__file__).resolve().parents[1]
ROOT = OUT.parents[1]
REF = ROOT / 'docs/old_assets/images'

def write_json(path, value):
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(json.dumps(value, indent=2, ensure_ascii=False) + '\n', encoding='utf-8')

def write_csv(path, rows, fields):
    with path.open('w', newline='', encoding='utf-8') as f:
        w = csv.DictWriter(f, fieldnames=fields)
        w.writeheader()
        w.writerows(rows)

def inventories():
    rows = []
    for p in sorted(REF.glob('*.png')):
        with Image.open(p) as im:
            alpha = im.convert('RGBA').getchannel('A').getextrema()
            rows.append(dict(file=p.name, width=im.width, height=im.height,
                             alpha_min=alpha[0], alpha_max=alpha[1],
                             square_cell_count=im.width // im.height if im.width % im.height == 0 else '',
                             note='Cell count is geometric only; inspect poses and repeats.'))
    write_csv(OUT / 'reference_inventory.csv', rows, list(rows[0]))
    paths = {}
    for p in sorted((ROOT / 'Assets/_Game/Art').rglob('*.png')):
        paths[p.relative_to(ROOT).as_posix()] = {'references': [], 'tags': []}
    for p in sorted((ROOT / 'Assets/_Game/Scripts').rglob('*.cs')):
        for line_no, line in enumerate(p.read_text(encoding='utf-8-sig').splitlines(), 1):
            for rel in re.findall(r'"(_Game/Art/[^"{}]+\.png)"', line):
                path = 'Assets/' + rel
                paths.setdefault(path, {'references': [], 'tags': []})['references'].append(f'{p.relative_to(ROOT).as_posix()}:{line_no}')
    census = ROOT / 'docs/Development/ASSET_CENSUS.csv'
    with census.open(encoding='utf-8-sig') as f:
        for r in csv.DictReader(f):
            if r['asset_type'] == 'SoundEffect':
                continue
            path = r['unity_path']
            paths.setdefault(path, {'references': [], 'tags': []})['tags'].append(r['asset_id'])
    # Interpolated player paths cannot be extracted as literal paths.
    for actor in ('lich_lord','demon_monarch','vampire_queen','shadow_reaper','cursed_golem'):
        path=f'Assets/_Game/Art/Characters/{actor}/char_{actor}_sheet.png'
        paths.setdefault(path, {'references': [], 'tags': []})['references'].append('Assets/_Game/Scripts/Game/Combat/LichLordVisualPresenter.cs:LoadSprites (interpolated)')
    rows=[]
    for path, data in sorted(paths.items()):
        p=ROOT/path
        size=''
        if p.is_file():
            with Image.open(p) as im: size=f'{im.width}x{im.height}'
        rows.append(dict(path=path, exists=p.is_file(), dimensions=size,
                         action='replace_and_review' if p.is_file() else 'create_or_reconcile_path',
                         census_tags=';'.join(data['tags']), runtime_references=';'.join(data['references']),
                         quality='unreviewed', animation='unverified'))
    write_csv(OUT/'inventory.csv',rows,list(rows[0]))
    sites=[]
    for p in sorted((ROOT/'Assets/_Game/Scripts/Game').rglob('*.cs')):
        for line_no,line in enumerate(p.read_text(encoding='utf-8-sig').splitlines(),1):
            if 'PlaceholderSprites.' in line:
                sites.append(dict(source=p.relative_to(ROOT).as_posix(),line=line_no,code=line.strip(),action='Review: replace art placeholder or retain intentional UI primitive'))
    write_csv(OUT/'placeholder_sites.csv',sites,list(sites[0]))

STYLE='Detailed dark fantasy pixel art, angular clusters, dark contour, stepped material shading, restrained highlights, upper-left light, no text, no border, no scenery.'
NEG='photorealistic, 3d, painterly, blurry, chibi, gradient background, checkerboard, watermark, text, cropped, extra limbs, duplicate object, sprite sheet'
LICH="Detailed dark fantasy pixel sprite of one full-body ivory skeletal monarch, exposed ribs, deep teal tattered hood and layered robes, dark open crown with cyan inlays, cyan eyes, crooked ivory staff on the viewer's right with a faceted cyan diamond crystal. Upright frontal stance matching the reference, calm idle pose, feet planted, angular pixel clusters, dark contour, stepped teal cloth shading, restrained cyan highlights, upper-left light, entire crown staff and feet visible, generous margin, uniform neutral gray background, no ground shadow."

def catalogs():
    entries=[]
    def add(kind, ident, name, desc, source, **extra):
        subject=f'{name}. {desc}'
        presentation='One isolated equipment icon, diagonal three-quarter view, clear material silhouette, uniform gray background.' if kind in ('weapon','armor') else 'One full-body upright character, frontal three-quarter view, entire silhouette visible, uniform gray background.'
        if kind=='spell': presentation='One isolated magic effect phase, center pivot, luminous core and stepped tapering particles, uniform gray background. Generate cast, travel or active, impact and dissipate separately; no character.'
        if kind=='biome': presentation='One top-down dungeon floor tile, subdued contrast beneath actors, orthographic square, continuous material at all four edges, no perspective horizon, no props. Check tiling manually.'
        if kind=='skill': presentation='One readable skill icon, a single emblem representing the named ability, dark fantasy materials and class palette, centered, no lettering, no UI border, uniform gray background.'
        entries.append(dict(kind=kind,id=ident,name=name,design=desc,source=source,positive=f'{STYLE} {presentation} {subject}',negative=NEG,**extra))
    for kind,filename in [('weapon','Equipment/WeaponCatalog.cs'),('armor','Equipment/ArmorCatalog.cs')]:
        p=ROOT/'Assets/_Game/Scripts/Core'/filename
        for m in re.finditer(r'Add\("([^"]+)",\s*"([^"]+)",\s*"([^"]+)"',p.read_text(encoding='utf-8-sig')):
            add(kind,*m.groups(),p.relative_to(ROOT).as_posix(),deliverables=['icon','world_drop']+(['held_sprite','attack_effect'] if kind=='weapon' else []))
    p=ROOT/'Assets/_Game/Scripts/Core/Combat/LegendaryBossCatalog.cs'
    for m in re.finditer(r'new LegendaryBossDefinition\(\s*"([^"]+)",\s*"([^"]+)",\s*"([^"]+)",(.*?)description:\s*"([^"]+)"',p.read_text(encoding='utf-8-sig'),re.S):
        ident,name,title,body,desc=m.groups()
        add('boss',ident,name,title+'. '+desc,p.relative_to(ROOT).as_posix(),abilities=re.findall(r'Ability:\s*BossAbilityType\.(\w+)',body),states=['idle','run','attack','cast','hurt','death'])
    p=ROOT/'Assets/_Game/Scripts/Core/Combat/InvaderCatalog.cs'
    for m in re.finditer(r'new InvaderDefinition\(\s*InvaderArchetype\.(\w+),\s*"([^"]+)"(.*?)description:\s*"([^"]+)"',p.read_text(encoding='utf-8-sig'),re.S):
        ident,name,body,desc=m.groups()
        add('invader',ident,name,desc,p.relative_to(ROOT).as_posix(),states=['idle','run','attack','hurt','death'])
    p=ROOT/'Assets/_Game/Scripts/Core/Magic/MagicSchoolCatalog.cs'
    for m in re.finditer(r'new SpellDefinition\("([^"]+)",\s*"([^"]+)",\s*MagicSchoolType\.(\w+),\s*\d+,\s*CastBehaviorType\.(\w+)([^\n]+)',p.read_text(encoding='utf-8-sig')):
        ident,name,school,behavior,tail=m.groups()
        desc=re.search(r'description:\s*"([^"]+)"',tail)
        add('spell',ident,name,desc[1] if desc else f'{school} magic, {behavior}',p.relative_to(ROOT).as_posix(),school=school,behavior=behavior,deliverables=['icon','cast','travel_or_active','impact','dissipate','telegraph_if_damaging_area'])
    p=ROOT/'Assets/_Game/Scripts/Core/Dungeons/DungeonModel.cs'
    for m in re.finditer(r'new DungeonBiome\((\d+),\s*"([^"]+)",\s*"([^"]+)"',p.read_text(encoding='utf-8-sig'),re.S):
        ident,name,desc=m.groups()
        add('biome','biome_'+ident,name,' '.join(desc.split()),p.relative_to(ROOT).as_posix(),deliverables=['floor_variants','walls','corners','pillar','obstacle','animated_accent'])
    p=ROOT/'Assets/_Game/Scripts/Core/Abilities/AbilityRegistry.cs'
    for m in re.finditer(r'new AbilityDefinition\(\s*id:\s*"([^"]+)",\s*characterId:\s*CharacterCatalog\.(\w+),\s*name:\s*"([^"]+)",\s*description:\s*"([^"]+)"',p.read_text(encoding='utf-8-sig'),re.S):
        ident,character,name,desc=m.groups()
        add('skill',ident,name,desc,p.relative_to(ROOT).as_posix(),character=character,deliverables=['icon','review_runtime_for_visible_effects'])
    assert len({e['id'] for e in entries})==len(entries), 'Duplicate catalog ID'
    write_json(OUT/'prompts/catalog_prompts.json',entries)
    print('Catalog briefs:', {k:sum(e['kind']==k for e in entries) for k in sorted({e['kind'] for e in entries})})

def graph(prompt, image_input, denoise, filename):
    api={
        '1':{'class_type':'CheckpointLoaderSimple','inputs':{'ckpt_name':'sd_xl_base_1.0_0.9vae.safetensors'}},
        '2':{'class_type':'CLIPTextEncode','inputs':{'text':prompt,'clip':['1',1]}},
        '3':{'class_type':'CLIPTextEncode','inputs':{'text':NEG,'clip':['1',1]}},
        '4':{'class_type':'LoadImage','inputs':{'image':image_input}} if image_input else {'class_type':'EmptyLatentImage','inputs':{'width':1024,'height':1024,'batch_size':1}},
        '7':{'class_type':'KSampler','inputs':{'model':['1',0],'positive':['2',0],'negative':['3',0],'latent_image':['6',0] if image_input else ['4',0],'seed':271027,'steps':30,'cfg':5.5,'sampler_name':'dpmpp_2m','scheduler':'karras','denoise':denoise}},
        '8':{'class_type':'VAEDecode','inputs':{'samples':['7',0],'vae':['1',2]}},
        '9':{'class_type':'SaveImage','inputs':{'images':['8',0],'filename_prefix':'EndlessOverlord/pilot/'+filename+'_master'}},
        '10':{'class_type':'ImageScale','inputs':{'image':['8',0],'upscale_method':'nearest-exact','width':128,'height':128,'crop':'disabled'}},
        '11':{'class_type':'SaveImage','inputs':{'images':['10',0],'filename_prefix':'EndlessOverlord/pilot/'+filename+'_128_preview'}}}
    if image_input:
        api['5']={'class_type':'ImageScale','inputs':{'image':['4',0],'upscale_method':'nearest-exact','width':1024,'height':1024,'crop':'disabled'}}
        api['6']={'class_type':'VAEEncode','inputs':{'pixels':['5',0],'vae':['1',2]}}
    spec={
      'CheckpointLoaderSimple':([],['MODEL','CLIP','VAE'],['ckpt_name']),
      'CLIPTextEncode':(['clip'],['CONDITIONING'],['text']),
      'LoadImage':([],['IMAGE','MASK'],['image']),
      'EmptyLatentImage':([],['LATENT'],['width','height','batch_size']),
      'ImageScale':(['image'],['IMAGE'],['upscale_method','width','height','crop']),
      'VAEEncode':(['pixels','vae'],['LATENT'],[]),
      'KSampler':(['model','positive','negative','latent_image'],['LATENT'],['seed','steps','cfg','sampler_name','scheduler','denoise']),
      'VAEDecode':(['samples','vae'],['IMAGE'],[]),
      'SaveImage':(['images'],[],['filename_prefix'])}
    positions={1:[0,0],2:[380,0],3:[380,290],4:[0,370],5:[380,520],6:[730,530],7:[900,0],8:[1260,0],9:[1630,0],10:[1260,220],11:[1630,500]}
    nodes={}; links=[]
    for key,v in sorted(api.items(),key=lambda x:int(x[0])):
        ins,outs,widgets=spec[v['class_type']]
        values=[v['inputs'][w] for w in widgets]
        if v['class_type']=='KSampler': values.insert(1,'fixed')
        if v['class_type']=='LoadImage': values.append('image')
        nodes[key]={'id':int(key),'type':v['class_type'],'pos':positions[int(key)],'size':[330,240] if v['class_type']=='CLIPTextEncode' else [310,300],'flags':{},'order':len(nodes),'mode':0,'inputs':[],'outputs':[{'name':o,'type':o,'links':[],'slot_index':i} for i,o in enumerate(outs)],'properties':{'Node name for S&R':v['class_type']},'widgets_values':values}
        nodes[key]['title']={'4':'REFERENCE / POSE GUIDE (upload provided PNG)','7':'Fixed seed; denoise is NOT identity lock','9':'MASTER candidate (background still present)','11':'128px REVIEW preview, NOT production alpha'}.get(key,v['class_type'])
        if key=='4' and not image_input: nodes[key]['title']='Concept canvas / batch size'
    for key,v in api.items():
        for name in spec[v['class_type']][0]:
            src,slot=v['inputs'][name]; target_slot=len(nodes[key]['inputs']); typ=nodes[src]['outputs'][slot]['type']; lid=len(links)+1
            nodes[key]['inputs'].append({'name':name,'type':typ,'link':lid})
            nodes[src]['outputs'][slot]['links'].append(lid)
            links.append([lid,int(src),slot,int(key),target_slot,typ])
    ui={'last_node_id':max(map(int,nodes)),'last_link_id':len(links),'nodes':list(nodes.values()),'links':links,'groups':[],'config':{},'extra':{},'version':0.4}
    write_json(OUT/f'workflows/{filename}.json',ui)
    write_json(OUT/f'workflows/{filename}_api.json',api)

def reference_crop():
    # Geometric crop only: do not repaint or infer alpha from dark pixels.
    with Image.open(REF/'boss_lich_lord_idle.png') as im:
        assert im.size==(2048,512),im.size
        cell=im.convert('RGBA').crop((0,0,512,512))
        bg=Image.new('RGBA',(512,512),(112,112,112,255))
        scaled=cell.resize((448,448),Image.Resampling.NEAREST)
        bg.alpha_composite(scaled,(32,32))
        bg.convert('RGB').save(OUT/'references/lich_lord_reference.png')

if __name__=='__main__':
    inventories(); catalogs(); reference_crop()
    graph(LICH,'lich_lord_reference.png',0.28,'01_Lich_Reference_Master')
    graph(LICH+' Breathing rises slightly, keep feet and staff tip anchored.','lich_lord_pose_guide.png',0.22,'02_Pose_Guided_Frame')
    graph(STYLE+' One isolated ivory bone staff, crooked shaft, faceted cyan diamond crystal, dark teal bindings, diagonal three-quarter icon, uniform neutral gray background, generous margins.',None,1.0,'03_Item_Effect_Concept')
    print('Package rebuilt. Workflow 02 requires an authored pose guide; no pose is fabricated.')
