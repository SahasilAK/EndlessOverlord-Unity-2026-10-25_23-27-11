using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Autonomous summoned minion for Lich Lord's signature ability (Bone Sentinel).
    /// Fully simulated AI entity: pursues enemies, performs melee attacks, has real health,
    /// and cleans up upon death or expiry.
    /// </summary>
    public sealed class BoneSentinelActor : MonoBehaviour, IDamageable
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private Health _health;
        private float _lifetime = 20f;
        private float _attackCooldown = 0.8f;
        private float _cooldownTimer;
        private int _id;
        private readonly List<TargetCandidate> _buffer = new List<TargetCandidate>();

        public int Id => _id;
        public Faction Team => Faction.Player;
        public Vector2 Position => _rb != null ? _rb.position : (Vector2)transform.position;
        public float Radius => 0.4f;
        public bool Alive => _health != null && !_health.IsDead;
        public Health Health => _health;

        public static BoneSentinelActor Spawn(Vector2 position)
        {
            var go = new GameObject("BoneSentinel");
            go.transform.position = position;
            go.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            var sentinel = go.AddComponent<BoneSentinelActor>();
            sentinel.Initialize();
            return sentinel;
        }

        private void Initialize()
        {
            _id = ActorRegistry.NextActorId();
            _health = new Health(50f);
            _health.Died += OnDied;

            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            _rb.freezeRotation = true;
            _rb.linearDamping = 2f;
            var col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = 0.5f;

            _sr = gameObject.AddComponent<SpriteRenderer>();
            _sr.sprite = PlaceholderSprites.Square;
            _sr.color = new Color(0.85f, 0.95f, 1f, 0.9f);
            _sr.sortingOrder = 8;

            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(transform, false);
            labelGo.transform.localPosition = new Vector3(0f, 0.6f, 0f);
            var text = labelGo.AddComponent<TextMesh>();
            text.text = "[SENTINEL]";
            text.fontSize = 20;
            text.characterSize = 0.06f;
            text.anchor = TextAnchor.MiddleCenter;
            text.color = Color.white;

            ActorRegistry.Register(this);
        }

        private void OnDestroy()
        {
            ActorRegistry.Unregister(this);
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }

        public float ReceiveDamage(float rawDamage)
        {
            if (!Alive) return 0f;
            float dealt = _health.TakeDamage(rawDamage);
            if (dealt > 0f)
            {
                CueBus.Raise(CueIds.InvaderHit, Position.x, Position.y, dealt);
            }
            return dealt;
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0f)
            {
                Destroy(gameObject);
                return;
            }

            if (_cooldownTimer > 0f)
                _cooldownTimer -= Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (!Alive) return;

            ActorRegistry.FillCandidates(_buffer);
            int targetId = TargetSelector.FindNearest(Position.x, Position.y, 10f, Faction.Player, _buffer);
            if (targetId != TargetSelector.None && ActorRegistry.TryGet(targetId, out var target) && target.Alive)
            {
                Vector2 diff = target.Position - Position;
                float dist = diff.magnitude;
                if (dist > 1.2f)
                {
                    _rb.linearVelocity = (diff / dist) * 4.5f;
                }
                else
                {
                    _rb.linearVelocity = Vector2.zero;
                    if (_cooldownTimer <= 0f)
                    {
                        target.ReceiveDamage(14f);
                        CueBus.Raise(CueIds.InvaderStrike, Position.x, Position.y);
                        _cooldownTimer = _attackCooldown;
                    }
                }
            }
            else
            {
                var player = ActorRegistry.Player;
                if (player != null && player.Alive)
                {
                    Vector2 diff = player.Position - Position;
                    if (diff.magnitude > 2.5f)
                    {
                        _rb.linearVelocity = diff.normalized * 3.5f;
                    }
                    else
                    {
                        _rb.linearVelocity = Vector2.zero;
                    }
                }
            }
        }
    }
}
