using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Invader entity supporting distinct combat archetypes:
    /// - Swordsman: Melee chaser, windup cleave, strike.
    /// - Archer: Ranged kiter, maintains distance, telegraphed shot, fires arrows.
    /// - Cleric: Support healer, seeks and channels heals on injured allies.
    /// - RoyalChampion: Multi-phase Boss with cleave, charge dash, enrage at 50% HP, summon guards, and triple shockwave!
    /// </summary>
    public sealed class InvaderActor : MonoBehaviour, IDamageable
    {
        private Rigidbody2D _rb;
        private SpriteRenderer _body;
        private SpriteRenderer _telegraph;
        private Transform _barFill;
        private Transform _barBgTransform;
        private Color _baseColor;
        private float _flash;
        private int _id;

        // Attack simulators and timers
        private MeleeAttackSim _melee;
        private float _actionCooldown;
        private float _actionWindup;
        private bool _isActionWinding;
        private Vector2 _aimDirection = Vector2.right;

        // Boss-specific states
        private bool _isEnraged;
        private float _bossDashTimer;
        private float _bossDashActive;
        private Vector2 _bossDashDir;

        public Health Health { get; private set; }
        public InvaderArchetype Archetype { get; private set; } = InvaderArchetype.Swordsman;
        public bool IsElite { get; private set; }
        public bool IsBoss => Archetype == InvaderArchetype.RoyalChampion;

        public float MoveSpeed { get; private set; }
        public float Armor { get; private set; }
        public float SoulReward { get; private set; }
        public long ScoreReward { get; private set; }

        public event Action<InvaderActor> Killed;

        public int Id => _id;
        public Faction Team => Faction.Invader;
        public Vector2 Position => _rb != null ? _rb.position : (Vector2)transform.position;
        public float Radius { get; private set; } = CombatTuning.InvaderRadius;
        public bool Alive => Health != null && !Health.IsDead;
        public MeleeAttackSim Melee => _melee;

        public static InvaderActor Spawn(Vector2 position, InvaderArchetype archetype = InvaderArchetype.Swordsman, bool isElite = false)
        {
            string name = isElite ? $"Elite_{archetype}" : archetype.ToString();
            var go = new GameObject(name);
            go.transform.position = position;
            var actor = go.AddComponent<InvaderActor>();
            actor.Initialize(archetype, isElite);
            return actor;
        }

        private void Initialize(InvaderArchetype archetype, bool isElite)
        {
            Archetype = archetype;
            IsElite = isElite;
            _id = ActorRegistry.NextActorId();

            float hp;
            float armor;
            float speed;

            switch (archetype)
            {
                case InvaderArchetype.Archer:
                    hp = CombatTuning.ArcherHealth;
                    armor = CombatTuning.ArcherArmor;
                    speed = CombatTuning.ArcherMoveSpeed;
                    Radius = CombatTuning.ArcherRadius;
                    _baseColor = new Color(0.95f, 0.65f, 0.2f); // Amber Orange
                    _actionCooldown = 1.0f; // Initial delay
                    break;

                case InvaderArchetype.Cleric:
                    hp = CombatTuning.ClericHealth;
                    armor = CombatTuning.ClericArmor;
                    speed = CombatTuning.ClericMoveSpeed;
                    Radius = CombatTuning.ClericRadius;
                    _baseColor = new Color(0.95f, 0.9f, 0.3f); // Golden Yellow
                    _actionCooldown = 1.5f;
                    break;

                case InvaderArchetype.RoyalChampion:
                    hp = CombatTuning.BossHealth;
                    armor = CombatTuning.BossArmor;
                    speed = CombatTuning.BossMoveSpeed;
                    Radius = CombatTuning.BossRadius;
                    _baseColor = new Color(0.6f, 0.2f, 0.75f); // Royal Purple
                    _melee = new MeleeAttackSim(CombatTuning.BossCleaveRange, CombatTuning.BossCleaveWindup, CombatTuning.BossCleaveRecover);
                    _bossDashTimer = CombatTuning.BossDashCooldown;
                    break;

                case InvaderArchetype.Swordsman:
                default:
                    hp = CombatTuning.InvaderHealth;
                    armor = CombatTuning.InvaderArmor;
                    speed = CombatTuning.InvaderMoveSpeed;
                    Radius = CombatTuning.InvaderRadius;
                    _baseColor = new Color(0.9f, 0.35f, 0.3f); // Crimson Red
                    _melee = new MeleeAttackSim(CombatTuning.InvaderMeleeRange, CombatTuning.InvaderWindup, CombatTuning.InvaderRecover);
                    break;
            }

            if (isElite)
            {
                hp *= CombatTuning.EliteHealthMultiplier;
                armor += 5f;
                speed *= CombatTuning.EliteSpeedMultiplier;
                _baseColor = Color.Lerp(_baseColor, new Color(1f, 0.85f, 0.1f), 0.5f); // Golden sheen
            }

            MoveSpeed = speed;
            Armor = armor;
            SoulReward = IsBoss ? CombatTuning.BossSoulReward : (isElite ? CombatTuning.SoulPerKill * 2.5f : CombatTuning.SoulPerKill);
            ScoreReward = IsBoss ? CombatTuning.BossScoreReward : (isElite ? (long)(CombatTuning.ScorePerKill * CombatTuning.EliteScoreMultiplier) : CombatTuning.ScorePerKill);

            Health = new Health(hp);

            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            _rb.freezeRotation = true;
            _rb.mass = IsBoss ? 15f : (isElite ? 5f : 3f);
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;

            var col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = 0.5f;

            transform.localScale = new Vector3(Radius * 2f, Radius * 2f, 1f);

            _body = gameObject.AddComponent<SpriteRenderer>();
            _body.sprite = PlaceholderSprites.Square;
            _body.color = _baseColor;
            _body.sortingOrder = IsBoss ? 8 : 5;

            // Telegraph
            var tg = new GameObject("Telegraph");
            tg.transform.SetParent(transform, false);
            _telegraph = tg.AddComponent<SpriteRenderer>();
            _telegraph.sprite = PlaceholderSprites.Square;
            _telegraph.color = archetype == InvaderArchetype.Cleric
                ? new Color(0.2f, 1f, 0.4f, 0.35f) // Green/Gold for heal
                : new Color(1f, 0.15f, 0.1f, 0.35f); // Red for attack
            _telegraph.sortingOrder = 4;
            _telegraph.enabled = false;

            // Health bar
            var bg = new GameObject("BarBg");
            bg.transform.SetParent(transform, false);
            var bgr = bg.AddComponent<SpriteRenderer>();
            bgr.sprite = PlaceholderSprites.Square;
            bgr.color = new Color(0f, 0f, 0f, 0.75f);
            bgr.sortingOrder = 20;

            var fill = new GameObject("BarFill");
            fill.transform.SetParent(transform, false);
            var fr = fill.AddComponent<SpriteRenderer>();
            fr.sprite = PlaceholderSprites.Square;
            fr.color = IsBoss ? new Color(0.9f, 0.2f, 0.8f) : (isElite ? new Color(1f, 0.85f, 0.1f) : new Color(0.3f, 0.9f, 0.3f));
            fr.sortingOrder = 21;
            _barFill = fill.transform;

            float barY = IsBoss ? 1.15f : 0.85f;
            bg.transform.localPosition = new Vector3(0f, barY, 0f);
            fill.transform.localPosition = new Vector3(0f, barY, 0f);
            _barBgTransform = bg.transform;

            Health.Changed += OnHealthChanged;
            ActorRegistry.Register(this);
            OnHealthChanged();
        }

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

            float worldWidth = IsBoss ? 2.4f : (IsElite ? 1.5f : 1.2f);
            float worldHeight = IsBoss ? 0.22f : 0.14f;
            float sx = worldWidth / transform.localScale.x;
            float sy = worldHeight / transform.localScale.y;
            float frac = Health.Max > 0f ? Health.Current / Health.Max : 0f;

            _barBgTransform.localScale = new Vector3(sx, sy, 1f);
            _barFill.localScale = new Vector3(sx * frac, sy * 0.8f, 1f);
            _barFill.localPosition = new Vector3(-(sx * (1f - frac)) * 0.5f, _barBgTransform.localPosition.y, 0f);

            // Boss Enrage check (< 50% HP)
            if (IsBoss && !_isEnraged && Health.Current <= Health.Max * 0.5f)
            {
                TriggerBossEnrage();
            }
        }

        private void TriggerBossEnrage()
        {
            _isEnraged = true;
            MoveSpeed = CombatTuning.BossEnragedSpeed;
            _baseColor = new Color(0.85f, 0.1f, 0.25f); // Enraged Crimson
            if (_body != null) _body.color = _baseColor;

            CueBus.Raise(CueIds.BossEnrage, Position.x, Position.y);

            // Boss summons 2 Swordsman reinforcements
            Spawn(Position + new Vector2(-1.8f, 0f), InvaderArchetype.Swordsman, false);
            Spawn(Position + new Vector2(1.8f, 0f), InvaderArchetype.Swordsman, false);
        }

        private void FixedUpdate()
        {
            if (!Alive)
            {
                if (_rb != null) _rb.linearVelocity = Vector2.zero;
                return;
            }

            float dt = Time.fixedDeltaTime;
            var player = ActorRegistry.Player;
            bool targetValid = player != null && player.Alive;
            Vector2 toPlayer = targetValid ? player.Position - Position : Vector2.zero;
            float distToPlayer = toPlayer.magnitude;

            switch (Archetype)
            {
                case InvaderArchetype.Swordsman:
                    UpdateSwordsman(dt, player, targetValid, toPlayer, distToPlayer);
                    break;
                case InvaderArchetype.Archer:
                    UpdateArcher(dt, player, targetValid, toPlayer, distToPlayer);
                    break;
                case InvaderArchetype.Cleric:
                    UpdateCleric(dt, player, targetValid, toPlayer, distToPlayer);
                    break;
                case InvaderArchetype.RoyalChampion:
                    UpdateBoss(dt, player, targetValid, toPlayer, distToPlayer);
                    break;
            }
        }

        private void UpdateSwordsman(float dt, PlayerActor player, bool targetValid, Vector2 toPlayer, float distToPlayer)
        {
            bool struck = _melee.Tick(dt, distToPlayer, targetValid);
            if (struck)
            {
                CueBus.Raise(CueIds.InvaderStrike, Position.x, Position.y);
                float dmg = CombatTuning.InvaderMeleeDamage * (IsElite ? CombatTuning.EliteDamageMultiplier : 1f);
                if (targetValid && distToPlayer <= _melee.Range * 1.15f)
                    player.ReceiveDamage(dmg);
            }

            bool approach = targetValid && _melee.Phase == MeleePhase.Idle && distToPlayer > _melee.Range * 0.8f;
            _rb.linearVelocity = approach ? toPlayer / distToPlayer * MoveSpeed : Vector2.zero;
        }

        private void UpdateArcher(float dt, PlayerActor player, bool targetValid, Vector2 toPlayer, float distToPlayer)
        {
            if (_actionCooldown > 0f)
                _actionCooldown -= dt;

            if (_isActionWinding)
            {
                _actionWindup -= dt;
                _rb.linearVelocity = Vector2.zero;

                if (targetValid && toPlayer.sqrMagnitude > 1e-4f)
                    _aimDirection = toPlayer.normalized;

                if (_actionWindup <= 0f)
                {
                    _isActionWinding = false;
                    _actionCooldown = CombatTuning.ArcherCooldown;

                    // Fire arrow projectile
                    float dmg = CombatTuning.ArcherArrowDamage * (IsElite ? CombatTuning.EliteDamageMultiplier : 1f);
                    if (ProjectileManager.Instance != null)
                    {
                        ProjectileManager.Instance.FireCustom(
                            Faction.Invader,
                            Position,
                            _aimDirection,
                            CombatTuning.ArcherArrowSpeed,
                            CombatTuning.ArcherArrowRadius,
                            dmg,
                            CombatTuning.ArcherArrowLifetime,
                            new Color(1f, 0.75f, 0.2f),
                            CueIds.ArrowFire);
                    }
                }
                return;
            }

            if (!targetValid)
            {
                _rb.linearVelocity = Vector2.zero;
                return;
            }

            // Kiting logic
            if (distToPlayer < CombatTuning.ArcherMinRange)
            {
                // Retreat away from player
                _rb.linearVelocity = (-toPlayer / distToPlayer) * MoveSpeed;
            }
            else if (distToPlayer > CombatTuning.ArcherMaxRange)
            {
                // Approach
                _rb.linearVelocity = (toPlayer / distToPlayer) * MoveSpeed;
            }
            else
            {
                // Inside sweet spot -> stop and prepare attack
                _rb.linearVelocity = Vector2.zero;
                _aimDirection = toPlayer / distToPlayer;

                if (_actionCooldown <= 0f)
                {
                    _isActionWinding = true;
                    _actionWindup = CombatTuning.ArcherWindup;
                    CueBus.Raise(CueIds.InvaderWindup, Position.x, Position.y);
                }
            }
        }

        private void UpdateCleric(float dt, PlayerActor player, bool targetValid, Vector2 toPlayer, float distToPlayer)
        {
            if (_actionCooldown > 0f)
                _actionCooldown -= dt;

            // Find most injured ally
            InvaderActor injuredAlly = null;
            float lowestHpPercent = 0.95f;

            var all = ActorRegistry.AllActors;
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i] is InvaderActor inv && inv != this && inv.Alive)
                {
                    float p = inv.Health.Current / inv.Health.Max;
                    if (p < lowestHpPercent && (inv.Position - Position).sqrMagnitude <= CombatTuning.ClericHealRange * CombatTuning.ClericHealRange * 1.5f)
                    {
                        lowestHpPercent = p;
                        injuredAlly = inv;
                    }
                }
            }

            if (_isActionWinding)
            {
                _actionWindup -= dt;
                _rb.linearVelocity = Vector2.zero;

                if (_actionWindup <= 0f)
                {
                    _isActionWinding = false;
                    _actionCooldown = CombatTuning.ClericHealCooldown;

                    if (injuredAlly != null && injuredAlly.Alive)
                    {
                        injuredAlly.Health.Heal(CombatTuning.ClericHealAmount);
                        CueBus.Raise(CueIds.ClericHeal, injuredAlly.Position.x, injuredAlly.Position.y);
                    }
                    else
                    {
                        Health.Heal(CombatTuning.ClericHealAmount);
                        CueBus.Raise(CueIds.ClericHeal, Position.x, Position.y);
                    }
                }
                return;
            }

            if (injuredAlly != null)
            {
                Vector2 toAlly = injuredAlly.Position - Position;
                float distToAlly = toAlly.magnitude;
                if (distToAlly > CombatTuning.ClericHealRange * 0.7f)
                {
                    _rb.linearVelocity = (toAlly / distToAlly) * MoveSpeed;
                }
                else
                {
                    _rb.linearVelocity = Vector2.zero;
                    if (_actionCooldown <= 0f)
                    {
                        _isActionWinding = true;
                        _actionWindup = CombatTuning.ClericHealWindup;
                    }
                }
            }
            else if (targetValid)
            {
                // Support keeps safe distance from player (6-8 units)
                if (distToPlayer < 5.5f)
                    _rb.linearVelocity = (-toPlayer / distToPlayer) * MoveSpeed;
                else if (distToPlayer > 8f)
                    _rb.linearVelocity = (toPlayer / distToPlayer) * MoveSpeed;
                else
                    _rb.linearVelocity = Vector2.zero;
            }
        }

        private void UpdateBoss(float dt, PlayerActor player, bool targetValid, Vector2 toPlayer, float distToPlayer)
        {
            // Dash charge timer
            if (_bossDashActive > 0f)
            {
                _bossDashActive -= dt;
                _rb.linearVelocity = _bossDashDir * CombatTuning.BossDashSpeed;
                if (targetValid && distToPlayer < Radius + player.Radius)
                {
                    player.ReceiveDamage(15f);
                }
                return;
            }

            _bossDashTimer -= dt;
            if (_bossDashTimer <= 0f && targetValid && distToPlayer > 4f)
            {
                _bossDashTimer = CombatTuning.BossDashCooldown;
                _bossDashActive = 0.35f;
                _bossDashDir = toPlayer.normalized;
                return;
            }

            bool struck = _melee.Tick(dt, distToPlayer, targetValid);
            if (struck)
            {
                CueBus.Raise(CueIds.BossCleave, Position.x, Position.y);
                float dmg = CombatTuning.BossCleaveDamage * (_isEnraged ? 1.25f : 1f);
                if (targetValid && distToPlayer <= _melee.Range * 1.15f)
                    player.ReceiveDamage(dmg);

                // Enraged boss cleave emits 3 shockwave bolts
                if (_isEnraged && ProjectileManager.Instance != null && targetValid)
                {
                    Vector2 baseDir = toPlayer.normalized;
                    float angle = Mathf.Atan2(baseDir.y, baseDir.x) * Mathf.Rad2Deg;

                    float[] offsets = { -22f, 0f, 22f };
                    for (int i = 0; i < offsets.Length; i++)
                    {
                        float rad = (angle + offsets[i]) * Mathf.Deg2Rad;
                        Vector2 dir = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
                        ProjectileManager.Instance.FireCustom(
                            Faction.Invader,
                            Position,
                            dir,
                            CombatTuning.ArcherArrowSpeed * 1.1f,
                            0.25f,
                            CombatTuning.BossShockwaveDamage,
                            1.5f,
                            new Color(0.9f, 0.1f, 0.2f),
                            CueIds.BossShockwave);
                    }
                }
            }

            bool approach = targetValid && _melee.Phase == MeleePhase.Idle && distToPlayer > _melee.Range * 0.8f;
            _rb.linearVelocity = approach ? toPlayer / distToPlayer * MoveSpeed : Vector2.zero;
        }

        private void Update()
        {
            // Telegraph visuals
            if (_telegraph != null)
            {
                bool isWinding = false;
                float progress = 0f;
                float range = 1.3f;

                if (Archetype == InvaderArchetype.Swordsman || Archetype == InvaderArchetype.RoyalChampion)
                {
                    isWinding = Alive && _melee != null && _melee.Phase == MeleePhase.Windup;
                    progress = _melee != null ? _melee.WindupProgress : 0f;
                    range = _melee != null ? _melee.Range : 1.3f;
                }
                else if (Archetype == InvaderArchetype.Archer)
                {
                    isWinding = Alive && _isActionWinding;
                    progress = 1f - (_actionWindup / CombatTuning.ArcherWindup);
                    range = CombatTuning.ArcherMinRange;
                }
                else if (Archetype == InvaderArchetype.Cleric)
                {
                    isWinding = Alive && _isActionWinding;
                    progress = 1f - (_actionWindup / CombatTuning.ClericHealWindup);
                    range = CombatTuning.ClericHealRange;
                }

                _telegraph.enabled = isWinding;
                if (isWinding)
                {
                    float d = range * 2f * Mathf.Max(0.2f, progress);
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

            float dealt = Health.TakeDamage(DamageCalculator.Compute(rawDamage, Armor));
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
            _melee?.Cancel();
            ActorRegistry.Unregister(this);
            CueBus.Raise(CueIds.InvaderDeath, Position.x, Position.y);

            SpawnLoot();

            var handler = Killed;
            Killed = null;
            handler?.Invoke(this);

            Destroy(gameObject);
        }

        private void SpawnLoot()
        {
            var session = GameSession.State;
            if (session == null || session.Run == null)
                return;

            var hud = session.Run.Hud;
            var inv = session.Run.Inventory;
            Vector2 dropPos = Position;

            if (IsBoss)
            {
                LootPickupActor.SpawnGold(dropPos + new Vector2(-0.4f, 0f), 50, hud);
                var rareItems = new[] { ItemCatalog.DeathScytheId, ItemCatalog.NecroticShroudId, ItemCatalog.AmuletOfTormentId };
                var pickedId = rareItems[UnityEngine.Random.Range(0, rareItems.Length)];
                var def = ItemCatalog.GetById(pickedId);
                if (def != null)
                {
                    LootPickupActor.SpawnItem(dropPos + new Vector2(0.4f, 0f), new ItemInstance(def), inv);
                }
            }
            else if (IsElite)
            {
                LootPickupActor.SpawnGold(dropPos, 15, hud);
                if (UnityEngine.Random.value < 0.5f)
                {
                    var allDefs = new List<ItemDefinition>(ItemCatalog.All);
                    var def = allDefs[UnityEngine.Random.Range(0, allDefs.Count)];
                    LootPickupActor.SpawnItem(dropPos + new Vector2(0.3f, 0.2f), new ItemInstance(def), inv);
                }
            }
            else
            {
                if (UnityEngine.Random.value < 0.75f)
                {
                    LootPickupActor.SpawnGold(dropPos, 5, hud);
                }
                else if (UnityEngine.Random.value < 0.2f)
                {
                    var allDefs = new List<ItemDefinition>(ItemCatalog.All);
                    var def = allDefs[UnityEngine.Random.Range(0, allDefs.Count)];
                    LootPickupActor.SpawnItem(dropPos, new ItemInstance(def), inv);
                }
            }
        }
    }
}
