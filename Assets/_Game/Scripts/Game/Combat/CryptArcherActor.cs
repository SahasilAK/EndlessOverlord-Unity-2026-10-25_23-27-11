using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Autonomous summoned ranged minion for Lich Lord's Crypt Archer ability.
    /// Pursues invaders, maintains ranged standoff distance, fires piercing bone arrows,
    /// and cleans up upon death or expiry.
    /// </summary>
    public sealed class CryptArcherActor : MonoBehaviour, IDamageable
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private Health _health;
        private float _lifetime = 18f;
        private float _attackCooldown = 1.4f;
        private float _cooldownTimer;
        private int _id;
        private float _damage = 12f;
        private int _pierce = 1;
        private readonly List<TargetCandidate> _buffer = new List<TargetCandidate>();

        public int Id => _id;
        public Faction Team => Faction.Player;
        public Vector2 Position => _rb != null ? _rb.position : (Vector2)transform.position;
        public float Radius => 0.4f;
        public bool Alive => _health != null && !_health.IsDead;
        public Health Health => _health;

        public static CryptArcherActor Spawn(Vector2 position, float damage = 12f, int pierce = 1)
        {
            var go = new GameObject("CryptArcher");
            go.transform.position = position;
            go.transform.localScale = new Vector3(0.65f, 0.65f, 1f);

            var archer = go.AddComponent<CryptArcherActor>();
            archer._damage = damage;
            archer._pierce = pierce;
            archer.Initialize();
            return archer;
        }

        private void Initialize()
        {
            _id = ActorRegistry.NextActorId();
            _health = new Health(40f);
            _health.Died += OnDied;

            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            _rb.freezeRotation = true;
            _rb.linearDamping = 2.5f;

            var col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = 0.5f;

            _sr = gameObject.AddComponent<SpriteRenderer>();
            _sr.sprite = PlaceholderSprites.Square;
            _sr.color = new Color(0.6f, 0.85f, 1f, 0.95f);
            _sr.sortingOrder = 8;

            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(transform, false);
            labelGo.transform.localPosition = new Vector3(0f, 0.6f, 0f);
            var text = labelGo.AddComponent<TextMesh>();
            text.text = "[ARCHER]";
            text.fontSize = 18;
            text.characterSize = 0.05f;
            text.anchor = TextAnchor.MiddleCenter;
            text.color = new Color(0.7f, 0.9f, 1f);

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
            {
                _cooldownTimer -= Time.deltaTime;
            }

            if (GameSession.Machine.Current != RunState.PlayingWave)
            {
                if (_rb != null) _rb.linearVelocity = Vector2.zero;
                return;
            }

            // Target acquisition
            ActorRegistry.FillCandidates(_buffer);
            int targetId = TargetSelector.FindNearest(Position.x, Position.y, 12f, Faction.Player, _buffer);
            if (targetId == TargetSelector.None || !ActorRegistry.TryGet(targetId, out var target) || !target.Alive)
            {
                if (_rb != null) _rb.linearVelocity = Vector2.zero;
                return;
            }

            Vector2 targetPos = target.Position;
            Vector2 toTarget = targetPos - Position;
            float dist = toTarget.magnitude;
            Vector2 dir = dist > 0.001f ? toTarget / dist : Vector2.zero;

            // Ranged standoff: keep ~4.5m distance
            float desiredDist = 4.5f;
            float moveSpeed = 3.2f;

            if (dist > desiredDist + 1f)
            {
                if (_rb != null) _rb.linearVelocity = dir * moveSpeed;
            }
            else if (dist < desiredDist - 1f)
            {
                if (_rb != null) _rb.linearVelocity = -dir * (moveSpeed * 0.8f);
            }
            else
            {
                if (_rb != null) _rb.linearVelocity = Vector2.zero;
            }

            // Ranged attack
            if (_cooldownTimer <= 0f && dist <= 8.5f && ProjectileManager.Instance != null)
            {
                _cooldownTimer = _attackCooldown;
                ProjectileManager.Instance.FireCustom(
                    Faction.Player,
                    Position,
                    dir,
                    speed: 12f,
                    radius: 0.18f,
                    damage: _damage,
                    lifetime: 1.2f,
                    color: new Color(0.4f, 0.9f, 1f),
                    cueId: CueIds.ArrowFire,
                    pierce: _pierce
                );
            }
        }
    }
}
