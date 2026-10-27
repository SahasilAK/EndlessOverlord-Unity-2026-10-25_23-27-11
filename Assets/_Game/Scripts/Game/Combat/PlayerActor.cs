using System;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Graybox Lich Lord. Owns health, dash and facing; movement runs in FixedUpdate on a Rigidbody2D.
    /// Damage is decided here in the simulation layer, never by visuals.
    /// Integrates with <see cref="RewardInventory"/> and <see cref="PlayerInventory"/> to apply real runtime stat buffs.
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

        public float LifeStealFraction
        {
            get
            {
                float rewards = _run != null && _run.Rewards != null ? _run.Rewards.LifeStealFraction : 0f;
                float equip = _run != null && _run.Inventory != null ? _run.Inventory.GetEquippedStats().LifeStealPercent : 0f;
                return rewards + equip;
            }
        }

        public void Initialize(RunContext run, PlayerInputAdapter input)
        {
            _run = run;
            _hud = run.Hud;
            _input = input;
            float startingMaxHp = run.Character.BaseMaxHealth + run.MetaStats.BonusMaxHealth;
            Health = new Health(startingMaxHp);
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
            _baseColor = _run != null && _run.Character != null
                ? _run.Character.ClassColor.ToUnityColor()
                : new Color(0.45f, 0.85f, 0.9f);
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

            if (_run != null && _run.Inventory != null)
                _run.Inventory.Changed += OnEquipmentChanged;

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
            if (_run != null && _run.Inventory != null)
                _run.Inventory.Changed -= OnEquipmentChanged;

            ActorRegistry.Unregister(this);
            if (ActorRegistry.Player == this)
                ActorRegistry.Player = null;
        }

        private void OnRewardAcquired(RewardDefinition def, int newRank)
        {
            RecalculateMaxHealth();
            UpdateDashCooldown();
        }

        private void OnEquipmentChanged()
        {
            RecalculateMaxHealth();
            UpdateDashCooldown();
        }

        private void RecalculateMaxHealth()
        {
            if (_run == null || Health == null) return;
            float baseMax = _run.Character.BaseMaxHealth + _run.MetaStats.BonusMaxHealth;
            float rewardHp = _run.Rewards != null ? _run.Rewards.BonusMaxHealth : 0f;
            float equipHp = _run.Inventory != null ? _run.Inventory.GetEquippedStats().FlatMaxHealth : 0f;
            float targetMax = baseMax + rewardHp + equipHp;

            if (Math.Abs(Health.Max - targetMax) > 0.01f)
            {
                float delta = targetMax - Health.Max;
                Health.IncreaseMax(delta, healDelta: delta > 0f);
                if (_hud != null)
                {
                    _hud.SetMaxHealth(Health.Max);
                    _hud.SetHealth(Health.Current);
                }
            }
        }

        private void UpdateDashCooldown()
        {
            if (_run == null || _dash == null) return;
            float baseCd = CombatTuning.DashCooldown;
            float rewardRed = _run.Rewards != null ? _run.Rewards.BonusDashCooldownReductionPercent : 0f;
            float equipRed = _run.Inventory != null ? _run.Inventory.GetEquippedStats().CooldownReductionPercent : 0f;
            var mutationDef = _run.ActiveMutation != DungeonMutationType.None
                ? InRunEventCatalog.GetMutation(_run.ActiveMutation)
                : null;
            float mutCdr = mutationDef != null ? mutationDef.CooldownReductionBonus : 0f;
            float totalRed = Math.Min(0.75f, rewardRed + equipRed + mutCdr + _shrineCdrBonus);
            _dash.SetCooldownTime(baseCd * (1f - totalRed));
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

        private float _barrierAmount;
        private float _barrierDuration;
        private GameObject _barrierVisual;
        private float _slowMultiplier = 1f;
        private float _slowTimer;
        private float _shrineDamageBonus;
        private float _shrineSpeedBonus;
        private float _shrineCdrBonus;
        private float _shrineBuffTimer;

        public float BarrierAmount => _barrierAmount;
        public float ShrineDamageBonus => _shrineDamageBonus;
        public float ShrineSpeedBonus => _shrineSpeedBonus;
        public float ShrineCdrBonus => _shrineCdrBonus;

        public bool ActivateShrine(ShrineInstance shrine)
        {
            if (shrine == null || !Alive || _run == null) return false;
            if (!shrine.TryActivate(Health.Current, out float hpCost, out float barrierGranted, out int goldGranted))
                return false;

            if (hpCost > 0f)
            {
                Health.TakeDamage(hpCost);
            }
            if (barrierGranted > 0f)
            {
                SetBarrier(barrierGranted, shrine.Definition.Duration);
            }
            if (goldGranted > 0 && _run.Hud != null)
            {
                _run.Hud.AddGold(goldGranted);
                _run.Hud.AddScore(goldGranted * 10);
            }

            if (shrine.Definition.Type == ShrineType.Blood)
            {
                _shrineDamageBonus = 0.35f;
                _shrineBuffTimer = shrine.Definition.Duration;
            }
            else if (shrine.Definition.Type == ShrineType.Frenzy)
            {
                _shrineSpeedBonus = 0.40f;
                _shrineCdrBonus = 0.30f;
                _shrineBuffTimer = shrine.Definition.Duration;
                UpdateDashCooldown();
            }

            return true;
        }

        public void ApplySlow(float slowPercent, float duration)
        {
            if (!Alive) return;
            float targetMult = Mathf.Clamp(1f - slowPercent, 0.2f, 1f);
            if (_slowTimer <= 0f || targetMult < _slowMultiplier)
            {
                _slowMultiplier = targetMult;
            }
            if (duration > _slowTimer)
            {
                _slowTimer = duration;
            }
        }

        public void SetBarrier(float amount, float duration)
        {
            _barrierAmount = amount;
            _barrierDuration = duration;
            if (_barrierVisual == null)
            {
                _barrierVisual = new GameObject("BarrierVisual");
                _barrierVisual.transform.SetParent(transform, false);
                _barrierVisual.transform.localScale = new Vector3(1.4f, 1.4f, 1f);
                var sr = _barrierVisual.AddComponent<SpriteRenderer>();
                sr.sprite = PlaceholderSprites.Square;
                sr.color = new Color(0.2f, 0.9f, 0.85f, 0.45f);
                sr.sortingOrder = 9;
            }
            _barrierVisual.SetActive(true);
        }

        public void Teleport(Vector2 destination)
        {
            if (_rb != null)
            {
                _rb.position = destination;
            }
            transform.position = (Vector3)destination;
        }

        private void FixedUpdate()
        {
            if (!Alive || GameSession.Machine.Current != RunState.PlayingWave)
            {
                _rb.linearVelocity = Vector2.zero;
                return;
            }
            float dt = Time.fixedDeltaTime;
            if (_slowTimer > 0f)
            {
                _slowTimer -= dt;
                if (_slowTimer <= 0f)
                    _slowMultiplier = 1f;
            }
            float vx, vy;
            if (_dash.Tick(dt, out vx, out vy))
            {
                _rb.linearVelocity = new Vector2(vx, vy);
            }
            else
            {
                Vector2 move = _input != null ? _input.Move : Vector2.zero;
                var equipStats = _run != null && _run.Inventory != null ? _run.Inventory.GetEquippedStats() : default(ItemStats);
                float metaSpeed = _run != null ? _run.MetaStats.BonusMoveSpeedPercent : 0f;
                var mutationDef = _run != null && _run.ActiveMutation != DungeonMutationType.None
                    ? InRunEventCatalog.GetMutation(_run.ActiveMutation)
                    : null;
                float mutSpeed = mutationDef != null ? mutationDef.MoveSpeedMultiplierBonus : 0f;
                float speedMultiplier = 1f + (_run != null && _run.Rewards != null ? _run.Rewards.BonusMoveSpeedPercent : 0f) + equipStats.MoveSpeedPercent + metaSpeed + mutSpeed + _shrineSpeedBonus;
                float baseSpeed = _run != null && _run.Character != null ? _run.Character.BaseMoveSpeed : CombatTuning.PlayerMoveSpeed;
                MovementMath.Velocity(move.x, move.y, baseSpeed * speedMultiplier * _slowMultiplier, out vx, out vy);
                _rb.linearVelocity = new Vector2(vx, vy);
                if (move.sqrMagnitude > 0.01f && !HasTargetAim)
                    AimDirection = move.normalized;
            }
        }

        /// <summary>Set by the auto attacker while it has a target so facing follows the target.</summary>
        public bool HasTargetAim { get; set; }

        private void Update()
        {
            if (_barrierDuration > 0f)
            {
                _barrierDuration -= Time.deltaTime;
                if (_barrierDuration <= 0f || _barrierAmount <= 0f)
                {
                    _barrierAmount = 0f;
                    if (_barrierVisual != null) _barrierVisual.SetActive(false);
                }
            }

            if (_shrineBuffTimer > 0f)
            {
                _shrineBuffTimer -= Time.deltaTime;
                if (_shrineBuffTimer <= 0f)
                {
                    _shrineDamageBonus = 0f;
                    _shrineSpeedBonus = 0f;
                    _shrineCdrBonus = 0f;
                    UpdateDashCooldown();
                }
            }

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

            if (_run != null && _run.Magic != null)
            {
                _run.Magic.Tick(Time.deltaTime);
            }
        }

        public SpellCastResult CastSpell(SpellInstance spell, IDamageable target)
        {
            if (spell == null || _run == null || !Alive)
                return SpellCastResult.Fail("", "Invalid cast.");

            float targetDistance = target != null ? Vector2.Distance(Position, target.Position) : 0f;
            int availableTargets = target != null ? 1 : 0;
            float availableResource = _run.Hud != null ? _run.Hud.Resource : float.MaxValue;
            var equipStats = _run.Inventory != null ? _run.Inventory.GetEquippedStats() : default(ItemStats);

            var result = SpellCastSimulator.ExecuteSpell(
                spell,
                _run.Magic,
                Position.x,
                Position.y,
                targetDistance,
                availableTargets,
                availableResource,
                equipStats.FlatDamage * 0.02f
            );

            if (result.Success)
            {
                if (_run.Hud != null && spell.Definition.ResourceCost > 0f)
                {
                    _run.Hud.TrySpendResource(spell.Definition.ResourceCost);
                }
                if (target != null && result.DamageDealt > 0f)
                {
                    target.ReceiveDamage(result.DamageDealt);
                }
            }

            return result;
        }

        public float ReceiveDamage(float rawDamage)
        {
            if (!Alive || _dash.Invulnerable)
                return 0f;

            var equipStats = _run != null && _run.Inventory != null ? _run.Inventory.GetEquippedStats() : default(ItemStats);
            float metaArmor = _run != null ? _run.MetaStats.BonusArmor : 0f;
            float baseArmor = _run != null && _run.Character != null ? _run.Character.BaseArmor : CombatTuning.PlayerArmor;
            float armor = baseArmor + (_run != null && _run.Rewards != null ? _run.Rewards.BonusArmor : 0f) + equipStats.FlatArmor + metaArmor;
            float damageAfterArmor = DamageCalculator.Compute(rawDamage, armor);

            // Barrier absorption (e.g. Cursed Golem Rune of Protection)
            if (_barrierAmount > 0f)
            {
                float absorbed = Mathf.Min(_barrierAmount, damageAfterArmor);
                _barrierAmount -= absorbed;
                damageAfterArmor -= absorbed;

                if (_run != null && _run.Character != null && _run.Character.Id == CharacterCatalog.CursedGolemId && _run.Hud != null)
                {
                    _run.Hud.AddResource(15f); // Golem gains Runic Charge when absorbing hits
                }

                if (_barrierAmount <= 0f && _barrierVisual != null)
                {
                    _barrierVisual.SetActive(false);
                }
            }

            // Demon Monarch gains Infernal Fury on taking damage
            if (_run != null && _run.Character != null && _run.Character.Id == CharacterCatalog.DemonMonarchId && _run.Hud != null)
            {
                _run.Hud.AddResource(10f);
            }

            float dealt = Health.TakeDamage(damageAfterArmor);
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
