using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>Smooth follow camera clamped so the view never leaves the arena.</summary>
    public sealed class CameraFollow : MonoBehaviour
    {
        private Transform _target;
        private Camera _camera;
        private Vector3 _velocity;

        public float SmoothTime = 0.15f;

        public void Initialize(Camera cam, Transform target)
        {
            _camera = cam;
            _target = target;
            if (_camera != null && _target != null)
                _camera.transform.position = Clamp(_target.position);
        }

        private void LateUpdate()
        {
            if (_camera == null || _target == null)
                return;
            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, Clamp(_target.position), ref _velocity, SmoothTime);
        }

        private Vector3 Clamp(Vector3 p)
        {
            float halfH = _camera.orthographicSize;
            float halfW = halfH * _camera.aspect;
            float maxX = Mathf.Max(0f, ArenaConfig.HalfWidth - halfW);
            float maxY = Mathf.Max(0f, ArenaConfig.HalfHeight - halfH);
            return new Vector3(Mathf.Clamp(p.x, -maxX, maxX), Mathf.Clamp(p.y, -maxY, maxY), -10f);
        }
    }

    /// <summary>
    /// Placeholder presentation for cue events: floating damage numbers (pooled TextMesh).
    /// Real VFX/audio/animation presenters will subscribe to the same CueBus later.
    /// </summary>
    public sealed class CuePresenter : MonoBehaviour
    {
        private sealed class FloatingNumber : MonoBehaviour
        {
            public TextMesh Mesh;
            public float Age;
            public Color Color;
            public System.Action<FloatingNumber> Finished;

            private void Update()
            {
                Age += Time.deltaTime;
                transform.position += Vector3.up * (1.2f * Time.deltaTime);
                var c = Color;
                c.a = Mathf.Clamp01(1f - Age / 0.7f);
                Mesh.color = c;
                if (Age >= 0.7f)
                    Finished(this);
            }
        }

        private ObjectPool<FloatingNumber> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<FloatingNumber>(Create, n => { n.Age = 0f; n.gameObject.SetActive(true); }, n => n.gameObject.SetActive(false));
        }

        private void OnEnable() { CueBus.Requested += OnCue; }
        private void OnDisable() { CueBus.Requested -= OnCue; }

        private FloatingNumber Create()
        {
            var go = new GameObject("FloatingNumber");
            go.transform.SetParent(transform, false);
            var tm = go.AddComponent<TextMesh>();
            tm.font = UiFactory.DefaultFont;
            tm.fontSize = 64;
            tm.characterSize = 0.08f;
            tm.anchor = TextAnchor.MiddleCenter;
            go.GetComponent<MeshRenderer>().sharedMaterial = tm.font.material;
            go.GetComponent<MeshRenderer>().sortingOrder = 50;
            var n = go.AddComponent<FloatingNumber>();
            n.Mesh = tm;
            n.Finished = f => _pool.Release(f);
            return n;
        }

        private void OnCue(CueRequest cue)
        {
            if (cue.Id == CueIds.InvaderHit || cue.Id == CueIds.PlayerHit)
            {
                var n = _pool.Get();
                n.Color = cue.Id == CueIds.PlayerHit ? new Color(1f, 0.3f, 0.3f) : Color.white;
                n.Mesh.text = Mathf.CeilToInt(cue.Value).ToString();
                n.transform.position = new Vector3(cue.X, cue.Y + 0.7f, 0f);
            }
        }
    }

    /// <summary>
    /// P2 sandbox spawner: keeps exactly one invader alive, respawning a couple of seconds after each kill
    /// at a seed-determined point away from the player. The real wave director replaces this in P3.
    /// </summary>
    public sealed class SandboxSpawner : MonoBehaviour
    {
        private const float RespawnDelay = 2f;
        private const float MinSpawnDistance = 8f;
        private const float EdgeMargin = 2f;

        private SplitMix64Rng _rng;
        private RunContext _run;
        private InvaderActor _current;
        private float _timer = 1f;

        public int KillCount { get; private set; }
        public int SpawnCount { get; private set; }
        public InvaderActor Current { get { return _current; } }

        public void Initialize(RunContext run)
        {
            _run = run;
            _rng = new SplitMix64Rng(run.Seed ^ 0x57A9C0DEL);
        }

        private void Update()
        {
            if (_run == null || _current != null)
                return;
            var player = ActorRegistry.Player;
            if (player == null || !player.Alive)
                return;
            _timer -= Time.deltaTime;
            if (_timer > 0f)
                return;

            Vector2 pos = PickSpawnPoint(player.Position);
            _current = InvaderActor.Spawn(pos);
            _current.Killed += OnKilled;
            SpawnCount++;
        }

        private Vector2 PickSpawnPoint(Vector2 playerPos)
        {
            for (int i = 0; i < 20; i++)
            {
                float x = (float)(_rng.NextDouble() * 2.0 - 1.0) * (ArenaConfig.HalfWidth - EdgeMargin);
                float y = (float)(_rng.NextDouble() * 2.0 - 1.0) * (ArenaConfig.HalfHeight - EdgeMargin);
                var p = new Vector2(x, y);
                if ((p - playerPos).sqrMagnitude >= MinSpawnDistance * MinSpawnDistance)
                    return p;
            }
            // Fallback: opposite side of the arena from the player.
            return new Vector2(playerPos.x > 0f ? -ArenaConfig.HalfWidth + EdgeMargin : ArenaConfig.HalfWidth - EdgeMargin, 0f);
        }

        private void OnKilled(InvaderActor actor)
        {
            _current = null;
            _timer = RespawnDelay;
            KillCount++;
            var hud = _run.Hud;
            hud.SetResource(hud.Resource + CombatTuning.SoulPerKill);
            hud.AddScore(CombatTuning.ScorePerKill);
        }
    }
}
