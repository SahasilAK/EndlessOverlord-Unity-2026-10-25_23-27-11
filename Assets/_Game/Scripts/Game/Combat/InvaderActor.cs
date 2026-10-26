using System;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Basic melee invader: seeks the player, telegraphs a windup, strikes if the player is still in range
    /// (so it can be dodged), then recovers. Death happens exactly once via <see cref="Health"/>.
    /// </summary>
    public sealed class InvaderActor : MonoBehaviour, IDamageable
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _body;
        private SpriteRenderer _telegraph;
        private Transform _barFill;
        private MeleeAttackSim _melee;
        private Color _baseColor = new Color(0.9f, 0.35f, 0.3f);
        private float _flash;
        private int _id;
        private bool _wasWinding;

        public Health Health { get; private set; }

        /// <summary>Raised once when this invader dies.</summary>
        public event Action<InvaderActor> Killed;

        public int Id { get { return _id; } }
        public Faction Team { get { return Faction.Invader; } }
        public Vector2 Position { get { return _rb != null ? _rb.position : (Vector2)transform.position; } }
        public float Radius { get { return CombatTuning.InvaderRadius; } }
        public bool Alive { get { return Health != null && !Health.IsDead; } }
        public MeleeAttackSim Melee { get { return _melee; } }

        public static InvaderActor Spawn(Vector2 position)
        {
            var go = new GameObject("Invader");
            go.transform.position = position;
            var actor = go.AddComponent<InvaderActor>();
            actor.Initialize();
            return actor;
        }

        private void Initialize()
        {
            _id = ActorRegistry.NextActorId();
            Health = new Health(CombatTuning.InvaderHealth);
            _melee = new MeleeAttackSim(CombatTuning.InvaderMeleeRange, CombatTuning.InvaderWindup, CombatTuning.InvaderRecover);

            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            _rb.freezeRotation = true;
            _rb.mass = 3f;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            var col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = 0.5f;

            transform.localScale = new Vector3(Radius * 2f, Radius * 2f, 1f);
            _body = gameObject.AddComponent<SpriteRenderer>();
            _body.sprite = PlaceholderSprites.Square;
            _body.color = _baseColor;
            _body.sortingOrder = 5;

            // Telegraph (area of the strike) shown only during windup.
            var tg = new GameObject("Telegraph");
            tg.transform.SetParent(transform, false);
            _telegraph = tg.AddComponent<SpriteRenderer>();
            _telegraph.sprite = PlaceholderSprites.Square;
            _telegraph.color = new Color(1f, 0.15f, 0.1f, 0.35f);
            _telegraph.sortingOrder = 4;
            _telegraph.enabled = false;

            // Health bar (background + fill) above the body.
            var bg = new GameObject("BarBg");
            bg.transform.SetParent(transform, false);
            var bgr = bg.AddComponent<SpriteRenderer>();
            bgr.sprite = PlaceholderSprites.Square;
            bgr.color = new Color(0f, 0f, 0f, 0.7f);
            bgr.sortingOrder = 20;
            var fill = new GameObject("BarFill");
            fill.transform.SetParent(transform, false);
            var fr = fill.AddComponent<SpriteRenderer>();
            fr.sprite = PlaceholderSprites.Square;
            fr.color = new Color(0.3f, 0.9f, 0.3f);
            fr.sortingOrder = 21;
            _barFill = fill.transform;
            bg.transform.localPosition = new Vector3(0f, 0.85f, 0f);
            fill.transform.localPosition = new Vector3(0f, 0.85f, 0f);
            _barBgTransform = bg.transform;

            Health.Changed += OnHealthChanged;
            ActorRegistry.Register(this);
            OnHealthChanged();
        }

        private Transform _barBgTransform;

        private void OnDestroy()
        {
            if (Health != null)
                Health.Changed -= OnHealthChanged;
            ActorRegistry.Unregister(this);
        }

        private void OnHealthChanged()
        {
            if (_barFill == null || Health == null)
                return;
            // Bar sizes are in local space; parent scale is Radius*2, so divide to get a fixed world width.
            float worldWidth = 1.2f, worldHeight = 0.14f;
            float sx = worldWidth / transform.localScale.x;
            float sy = worldHeight / transform.localScale.y;
            float frac = Health.Max > 0f ? Health.Current / Health.Max : 0f;
            _barBgTransform.localScale = new Vector3(sx, sy, 1f);
            _barFill.localScale = new Vector3(sx * frac, sy * 0.8f, 1f);
            _barFill.localPosition = new Vector3(-(sx * (1f - frac)) * 0.5f, _barBgTransform.localPosition.y, 0f);
        }

        private void FixedUpdate()
        {
            if (!Alive)
                return;
            var player = ActorRegistry.Player;
            bool targetValid = player != null && player.Alive;
            Vector2 toPlayer = targetValid ? player.Position - Position : Vector2.zero;
            float dist = toPlayer.magnitude;

            bool struck = _melee.Tick(Time.fixedDeltaTime, dist, targetValid);
            if (struck)
            {
                CueBus.Raise(CueIds.InvaderStrike, Position.x, Position.y);
                // Re-check range at the moment of the strike: stepping or dashing away dodges it.
                if (targetValid && dist <= _melee.Range * 1.15f)
                    player.ReceiveDamage(CombatTuning.InvaderMeleeDamage);
            }

            bool windingNow = _melee.Phase == MeleePhase.Windup;
            if (windingNow && !_wasWinding)
                CueBus.Raise(CueIds.InvaderWindup, Position.x, Position.y);
            _wasWinding = windingNow;

            bool approach = targetValid && _melee.Phase == MeleePhase.Idle && dist > _melee.Range * 0.8f;
            _rb.linearVelocity = approach ? toPlayer / dist * CombatTuning.InvaderMoveSpeed : Vector2.zero;
        }

        private void Update()
        {
            if (_telegraph != null)
            {
                bool winding = Alive && _melee.Phase == MeleePhase.Windup;
                _telegraph.enabled = winding;
                if (winding)
                {
                    // World-space area diameter = 2 * Range, growing with windup progress.
                    float d = _melee.Range * 2f * Mathf.Max(0.15f, _melee.WindupProgress);
                    _telegraph.transform.localScale = new Vector3(d / transform.localScale.x, d / transform.localScale.y, 1f);
                }
            }
            if (_flash > 0f) _flash -= Time.deltaTime;
            _body.color = _flash > 0f ? Color.white : _baseColor;
        }

        public float ReceiveDamage(float rawDamage)
        {
            if (!Alive)
                return 0f;
            float dealt = Health.TakeDamage(DamageCalculator.Compute(rawDamage, CombatTuning.InvaderArmor));
            if (dealt <= 0f)
                return 0f;
            _flash = 0.08f;
            CueBus.Raise(CueIds.InvaderHit, Position.x, Position.y, dealt);
            if (Health.IsDead)
                Die();
            return dealt;
        }

        private void Die()
        {
            _melee.Cancel();
            ActorRegistry.Unregister(this);
            CueBus.Raise(CueIds.InvaderDeath, Position.x, Position.y);
            var handler = Killed;
            Killed = null;
            handler?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
