"""Validate local graph links and inputs against running ComfyUI without queuing a job."""
import json
import urllib.request
from pathlib import Path

BASE=Path(__file__).resolve().parents[1]

def check():
    with urllib.request.urlopen('http://127.0.0.1:8188/object_info',timeout=10) as r:
        schema=json.load(r)
    reports=[]
    for p in sorted((BASE/'workflows').glob('*_api.json')):
        api=json.loads(p.read_text())
        ui=json.loads(p.with_name(p.name.replace('_api','')).read_text())
        nodes={n['id']:n for n in ui['nodes']}
        assert set(nodes)==set(map(int,api)),p.name
        for lid,src,slot,dst,ins,typ in ui['links']:
            assert nodes[dst]['inputs'][ins]['link']==lid
            assert lid in nodes[src]['outputs'][slot]['links']
            assert nodes[src]['outputs'][slot]['type']==typ
            input_name=nodes[dst]['inputs'][ins]['name']
            assert api[str(dst)]['inputs'][input_name]==[str(src),slot]
        for key,node in api.items():
            s=schema[node['class_type']]
            required=s['input'].get('required',{})
            assert set(required)<=set(node['inputs']), (key,'missing input')
            for name,value in node['inputs'].items():
                definition=required.get(name,s['input'].get('optional',{}).get(name))
                assert definition is not None,(key,name)
                if isinstance(value,list):
                    source,slot=value
                    assert source in api
                    actual=schema[api[source]['class_type']]['output'][slot]
                    assert actual==definition[0],(key,name,actual,definition[0])
                elif isinstance(definition[0],list) and name!='image':
                    assert value in definition[0],(key,name,value,'unavailable')
                elif len(definition)>1 and isinstance(value,(int,float)):
                    opts=definition[1]
                    assert opts.get('min',value)<=value<=opts.get('max',value)
        reports.append(p.name+': link/schema/model selector validation passed')
    catalog=json.loads((BASE/'prompts/catalog_prompts.json').read_text(encoding='utf-8'))
    counts={k:sum(e['kind']==k for e in catalog) for k in {e['kind'] for e in catalog}}
    for kind,count in dict(weapon=150,armor=30,boss=30,invader=30,spell=80,biome=20).items(): assert counts[kind]==count
    for e in catalog: assert (BASE.parents[1]/e['source']).is_file(),e['source']
    reports.append('Catalog counts and source paths validated: '+str(counts))
    reports.append('No inference queued. Image quality, GPU execution and Unity integration not validated by this check.')
    print('\n'.join(reports))
    return reports

if __name__=='__main__': check()
