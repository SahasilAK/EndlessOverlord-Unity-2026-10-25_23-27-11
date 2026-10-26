using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Graybox Lich Lord. Owns health, dash and facing; movement runs in FixedUpdate on a Rigidbody2D.
    /// Damage is decided here in the simulation layer, never by visuals.
    /// Integrates with <see cref="RewardInventory"/> to apply real runtime stat buffs.
    /// </summary>
    public sealed class PlayerActor : MonoBehaviour, IDamageable
    {
        private RunContext _run;
        private Rigidbody2D _rb;
        private SpriteRenderer _body;
        private Transform _aimMarker;
        private PlayerInputAdapter _input;
        private HudModel _hud;
        private DashSim _dash;
        private Color _baseColor;
        private float _flash;

        public Health Health { get; private set; }
        public Vector2 AimDirection { get; set; } = Vector2.right;
        public DashSim Dash { get { return _dash; } }
        public RunContext Run { get { return _run; } }

        public int Id { get { return ActorRegistry.PlayerId; } }
        public Faction Team { get { return Faction.Player; } }
        public Vector2 Position { get { return _rb != null ? _rb.position : (Vector2)transform.position; } }
        public float Radius { get { return CombatTuning.PlayerRadius; } }
        public bool Alive { get { return Health != null && !Health.IsDead; } }

        public float LifeStealFraction => _run != null && _run.Rewards != null ? _run.Rewards.LifeStealFraction : 0f;

        public void Initialize(RunContext run, PlayerInputAdapter input)
        {
            _run = run;
            _hud = run.Hud;
            _input = input;
            Health = new Health(run.Character.BaseMaxHealth);
            _dash = new DashSim(CombatTuning.DashSpeed, CombatTuning.DashDuration, CombatTuning.DashCooldown);

            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            _rb.freezeRotation = true;
            _rb.linearDamping = 0f;
            _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
            var col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = 0.5f;   // local radius; transform scale gives the world size

            _body = gameObject.AddComponent<SpriteRenderer>();
            _body.sprite = PlaceholderSprites.Square;
            _baseColor = new Color(0.45f, 0.85f, 0.9f);
            _body.color = _baseColor;
            _body.sortingOrder = 10;
            transform.localScale = new Vector3(CombatTuning.PlayerRadius * 2f, CombatTuning.PlayerRadius * 2f, 1f);

            var marker = new GameObject("AimMarker");
            marker.transform.SetParent(transform, false);
            marker.transform.localScale = new Vector3(0.35f, 0.35f, 1f);
            var mr = marker.AddComponent<SpriteRenderer>();
            mr.sprite = PlaceholderSprites.Square;
            mr.color = Color.white;
            mr.sortingOrder = 11;
            _aimMarker = marker.transform;

            Health.Changed += OnHealthChanged;
            if (_input != null)
                _input.DashPressed += OnDashPressed;

            if (_run != null && _run.Rewards != null)
                _run.Rewards.RewardAcquired += OnRewardAcquired;

            ActorRegistry.Player = this;
            ActorRegistry.Register(this);
            OnHealthChanged();
        }

        private void OnDestroy()
        {
            if (_input != null)
                _input.DashPressed -= OnDashPressed;
            if (Health != null)
                Health.Changed -= OnHealthChanged;
            if (_run != null && _run.Rewards != null)
                _run.Rewards.RewardAcquired -= OnRewardAcquired;

            ActorRegistry.Unregister(this);
            if (ActorRegistry.Player == this)
                ActorRegistry.Player = null;
        }

        private void OnRewardAcquired(RewardDefinition def, int newRank)
        {
            if (def.ModifierType == RewardModifierType.MaxHealth)
            {
                Health.IncreaseMax(def.ValuePerRank, healDelta: true);
                if (_hud != null)
                {
                    _hud.SetMaxHealth(Health.Max);
                    _hud.SetHealth(Health.Current);
                }
            }
            else if (def.ModifierType == RewardModifierType.DashCooldownReductionPercent)
            {
                float baseCd = CombatTuning.DashCooldown;
                float reduction = _run != null ? _run.Rewards.BonusDashCooldownReductionPercent : 0f;
                _dash.SetCooldownTime(baseCd * (1f - reduction));
            }
        }

        private void OnHealthChanged()
        {
            if (_hud != null)
                _hud.SetHealth(Health.Current);
        }

        private void OnDashPressed()
        {
            if (!Alive || GameSession.Machine.Current != RunState.PlayingWave)
                return;
            Vector2 dir = _input.Move.sqrMagnitude > 0.01f ? _input.Move : AimDirection;
            if (_dash.TryStart(dir.x, dir.y))
            {
                CueBus.Raise(CueIds.PlayerDash, Position.x, Position.y);
            }
        }

        private void FixedUpdate()
        {
            if (!Alive || GameSession.Machine.Current != RunState.PlayingWave)
            {
                _rb.linearVelocity = Vector2.zero;
                return;
            }
            float dt = Time.fixedDeltaTime;
            float vx, vy;
            if (_dash.Tick(dt, out vx, out vy))
            {
                _rb.linearVelocity = new Vector2(vx, vy);
            }
            else
            {
                Vector2 move = _input != null ? _input.Move : Vector2.zero;
                float speedMultiplier = 1f + (_run != null && _run.Rewards != null ? _run.Rewards.BonusMoveSpeedPercent : 0f);
                MovementMath.Velocity(move.x, move.y, CombatTuning.PlayerMoveSpeed * speedMultiplier, out vx, out vy);
                _rb.linearVelocity = new Vector2(vx, vy);
                if (move.sqrMagnitude > 0.01f && !HasTargetAim)
                    AimDirection = move.normalized;
            }
        }

        /// <summary>Set by the auto attacker while it has a target so facing follows the target.</summary>
        public bool HasTargetAim { get; set; }

        private void Update()
        {
            if (_aimMarker != null)
            {
                var d = AimDirection.sqrMagnitude > 0.0001f ? AimDirection.normalized : Vector2.right;
                _aimMarker.position = (Vector3)(Position + d * 0.55f);
            }
            if (_body != null)
            {
                if (_flash > 0f) _flash -= Time.deltaTime;
                _body.color = _dash != null && _dash.Active ? new Color(1f, 1f, 1f, 0.45f)
                            : _flash > 0f ? Color.red
                            : _baseColor;
            }
        }

        public float ReceiveDamage(float rawDamage)
        {
            if (!Alive || _dash.Invulnerable)
                return 0f;
            float armor = CombatTuning.PlayerArmor + (_run != null && _run.Rewards != null ? _run.Rewards.BonusArmor : 0f);
            float dealt = Health.TakeDamage(DamageCalculator.Compute(rawDamage, armor));
            if (dealt > 0f)
            {
                _flash = 0.12f;
                CueBus.Raise(CueIds.PlayerHit, Position.x, Position.y, dealt);
                if (Health.IsDead)
                    CueBus.Raise(CueIds.PlayerDeath, Position.x, Position.y);
            }
            return dealt;
        }
    }
}
