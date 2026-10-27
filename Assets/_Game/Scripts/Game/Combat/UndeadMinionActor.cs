using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    public enum UndeadMinionType
    {
        Spearman,
        Hound,
        Mage,
        Guardian,
        Captain,
        Scavenger,
        Abomination,
        Dragon
    }

    /// <summary>
    /// Autonomous summoned minion for Lich Lord's Branch A abilities (03 through 10) and Ultimates.
    /// Supports 8 distinct archetypes with full AI behavior, combat animations/cues, and stat scaling.
    /// </summary>
    public sealed class UndeadMinionActor : MonoBehaviour, IDamageable
    {
        public static readonly List<UndeadMinionActor> ActiveCaptains = new List<UndeadMinionActor>();

        private Rigidbody2D _rb;
        private SpriteRenderer _sr;
        private Health _health;
        private UndeadMinionType _type;
        private float _lifetime = 25f;
        private float _attackCooldown = 1.0f;
        private float _cooldownTimer;
        private int _id;
        private float _damage = 15f;
        private float _baseSpeed = 4.0f;
        private float _armor = 0f;
        private float _attackRange = 1.5f;
        private readonly List<TargetCandidate> _buffer = new List<TargetCandidate>();

        public int Id => _id;
        public Faction Team => Faction.Player;
        public Vector2 Position => _rb != null ? _rb.position : (Vector2)transform.position;
        public float Radius => 0.45f;
        public bool Alive => _health != null && !_health.IsDead;
        public Health Health => _health;
        public UndeadMinionType Type => _type;

        public static (float damageMult, float speedMult) GetCaptainBuff(Vector2 pos, float maxRadius = 5.5f)
        {
            float maxDistSqr = maxRadius * maxRadius;
            for (int i = 0; i < ActiveCaptains.Count; i++)
            {
                var capt = ActiveCaptains[i];
                if (capt != null && capt.Alive && capt._type == UndeadMinionType.Captain)
                {
                    if (((Vector2)capt.transform.position - pos).sqrMagnitude <= maxDistSqr)
                    {
                        return (1.30f, 1.25f);
                    }
                }
            }
            return (1.0f, 1.0f);
        }

        public static UndeadMinionActor Spawn(
            UndeadMinionType type,
            Vector2 position,
            float overrideDamage = 0f,
            float overrideMaxHp = 0f,
            float lifetime = 25f)
        {
            var go = new GameObject($"Undead_{type}");
            go.transform.position = position;

            var minion = go.AddComponent<UndeadMinionActor>();
            minion._type = type;
            minion._lifetime = lifetime;
            minion.InitializeStats(type, overrideDamage, overrideMaxHp);
            minion.BuildVisuals(type);
            minion.RegisterMinion();
            return minion;
        }

        private void InitializeStats(UndeadMinionType type, float overrideDamage, float overrideMaxHp)
        {
            _id = ActorRegistry.NextActorId();

            float hp = 50f;
            _damage = 15f;
            _baseSpeed = 4.0f;
            _attackCooldown = 1.0f;
            _attackRange = 1.5f;
            _armor = 0f;

            switch (type)
            {
                case UndeadMinionType.Spearman:
                    hp = overrideMaxHp > 0f ? overrideMaxHp : 60f;
                    _damage = overrideDamage > 0f ? overrideDamage : 18f;
                    _baseSpeed = 4.2f;
                    _attackRange = 1.8f;
                    _attackCooldown = 1.0f;
                    break;

                case UndeadMinionType.Hound:
                    hp = overrideMaxHp > 0f ? overrideMaxHp : 35f;
                    _damage = overrideDamage > 0f ? overrideDamage : 10f;
                    _baseSpeed = 7.0f;
                    _attackRange = 1.1f;
                    _attackCooldown = 0.5f;
                    break;

                case UndeadMinionType.Mage:
                    hp = overrideMaxHp > 0f ? overrideMaxHp : 35f;
                    _damage = overrideDamage > 0f ? overrideDamage : 16f;
                    _baseSpeed = 3.2f;
                    _attackRange = 8.5f;
                    _attackCooldown = 1.6f;
                    break;

                case UndeadMinionType.Guardian:
                    hp = overrideMaxHp > 0f ? overrideMaxHp : 100f;
                    _damage = overrideDamage > 0f ? overrideDamage : 22f;
                    _armor = 8f;
                    _baseSpeed = 3.0f;
                    _attackRange = 1.3f;
                    _attackCooldown = 1.1f;
                    break;

                case UndeadMinionType.Captain:
                    hp = overrideMaxHp > 0f ? overrideMaxHp : 75f;
                    _damage = overrideDamage > 0f ? overrideDamage : 20f;
                    _armor = 3f;
                    _baseSpeed = 3.8f;
                    _attackRange = 1.4f;
                    _attackCooldown = 1.0f;
                    break;

                case UndeadMinionType.Scavenger:
                    hp = overrideMaxHp > 0f ? overrideMaxHp : 50f;
                    _damage = overrideDamage > 0f ? overrideDamage : 8f;
                    _baseSpeed = 5.0f;
                    _attackRange = 1.2f;
                    _attackCooldown = 0.9f;
                    break;

                case UndeadMinionType.Abomination:
                    hp = overrideMaxHp > 0f ? overrideMaxHp : 160f;
                    _damage = overrideDamage > 0f ? overrideDamage : 35f;
                    _armor = 5f;
                    _baseSpeed = 2.8f;
                    _attackRange = 2.0f;
                    _attackCooldown = 2.0f;
                    break;

                case UndeadMinionType.Dragon:
                    hp = overrideMaxHp > 0f ? overrideMaxHp : 200f;
                    _damage = overrideDamage > 0f ? overrideDamage : 45f;
                    _armor = 10f;
                    _baseSpeed = 4.5f;
                    _attackRange = 4.5f;
                    _attackCooldown = 2.2f;
                    break;
            }

            _health = new Health(hp);
            _health.Died += OnDied;
        }

        private void BuildVisuals(UndeadMinionType type)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
            _rb.gravityScale = 0f;
            _rb.freezeRotation = true;
            _rb.linearDamping = 2.5f;

            var col = gameObject.AddComponent<CircleCollider2D>();
            col.radius = 0.5f;

            _sr = gameObject.AddComponent<SpriteRenderer>();
            _sr.sortingOrder = 8;

            Color color;
            float scale;
            string tagText;
            Sprite customSprite = null;

            switch (type)
            {
                case UndeadMinionType.Spearman:
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Summons/sum_grave_spearman_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                    color = new Color(0.7f, 0.9f, 0.85f, 0.95f);
                    scale = 0.75f;
                    tagText = "[SPEARMAN]";
                    break;
                case UndeadMinionType.Hound:
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Summons/sum_grave_hound_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                    color = new Color(0.55f, 0.6f, 0.65f, 0.95f);
                    scale = 0.55f;
                    tagText = "[HOUND]";
                    break;
                case UndeadMinionType.Mage:
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Summons/sum_bone_mage_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                    color = new Color(0.6f, 0.5f, 0.95f, 0.95f);
                    scale = 0.65f;
                    tagText = "[MAGE]";
                    break;
                case UndeadMinionType.Guardian:
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Summons/sum_bone_sentinel_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                    color = new Color(0.85f, 0.85f, 0.92f, 0.95f);
                    scale = 0.95f;
                    tagText = "[GUARDIAN]";
                    break;
                case UndeadMinionType.Captain:
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Summons/sum_wight_captain_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                    color = new Color(1.0f, 0.9f, 0.5f, 0.95f);
                    scale = 0.85f;
                    tagText = "[CAPTAIN]";
                    break;
                case UndeadMinionType.Scavenger:
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Summons/sum_corpse_collector_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                    color = new Color(0.5f, 0.9f, 0.5f, 0.95f);
                    scale = 0.6f;
                    tagText = "[SCAVENGER]";
                    break;
                case UndeadMinionType.Abomination:
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Summons/sum_dread_abomination_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                    color = new Color(0.4f, 0.7f, 0.4f, 0.95f);
                    scale = 1.25f;
                    tagText = "[ABOMINATION]";
                    break;
                case UndeadMinionType.Dragon:
                    customSprite = SpriteSheetRegistry.GetIdleSprite("_Game/Art/Characters/Summons/sum_skeletal_dragon_sheet.png", 32, 32, 6, new Vector2(0.5f, 0.1f), 32f);
                    color = new Color(0.35f, 0.2f, 0.65f, 0.95f);
                    scale = 1.45f;
                    tagText = "[DRAGON]";
                    break;
                default:
                    color = Color.white;
                    scale = 0.7f;
                    tagText = "[MINION]";
                    break;
            }

            _sr.sprite = customSprite != null ? customSprite : PlaceholderSprites.Square;
            _sr.color = customSprite != null ? Color.white : color;
            transform.localScale = new Vector3(scale, scale, 1f);

            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(transform, false);
            labelGo.transform.localPosition = new Vector3(0f, 0.6f, 0f);
            var text = labelGo.AddComponent<TextMesh>();
            text.text = tagText;
            text.fontSize = 18;
            text.characterSize = 0.05f;
            text.anchor = TextAnchor.MiddleCenter;
            text.color = color;
        }

        private void RegisterMinion()
        {
            ActorRegistry.Register(this);
            if (_type == UndeadMinionType.Captain && !ActiveCaptains.Contains(this))
            {
                ActiveCaptains.Add(this);
            }
        }

        private void OnDestroy()
        {
            ActorRegistry.Unregister(this);
            if (_type == UndeadMinionType.Captain)
            {
                ActiveCaptains.Remove(this);
            }
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }

        public float ReceiveDamage(float rawDamage)
        {
            if (!Alive) return 0f;
            float effectiveDamage = Mathf.Max(1f, rawDamage - _armor);
            float dealt = _health.TakeDamage(effectiveDamage);
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
        }

        private void FixedUpdate()
        {
            if (!Alive) return;

            if (GameSession.Machine.Current != RunState.PlayingWave)
            {
                if (_rb != null) _rb.linearVelocity = Vector2.zero;
                return;
            }

            var (damageMult, speedMult) = GetCaptainBuff(Position);
            float currentSpeed = _baseSpeed * speedMult;

            // 1. Scavenger special check: pursue nearby loot drops
            if (_type == UndeadMinionType.Scavenger)
            {
                if (TryScavengeNearestLoot(currentSpeed))
                {
                    return;
                }
            }

            // 2. Standard invader targeting
            ActorRegistry.FillCandidates(_buffer);
            float searchRadius = _type == UndeadMinionType.Mage || _type == UndeadMinionType.Dragon ? 12f : 10f;
            int targetId = TargetSelector.FindNearest(Position.x, Position.y, searchRadius, Faction.Player, _buffer);

            if (targetId != TargetSelector.None && ActorRegistry.TryGet(targetId, out var target) && target.Alive)
            {
                Vector2 diff = target.Position - Position;
                float dist = diff.magnitude;
                Vector2 dir = dist > 0.001f ? diff / dist : Vector2.right;

                if (_type == UndeadMinionType.Mage)
                {
                    HandleMageCombat(target, dist, dir, currentSpeed, damageMult);
                }
                else if (_type == UndeadMinionType.Dragon)
                {
                    HandleDragonCombat(target, dist, dir, currentSpeed, damageMult);
                }
                else if (_type == UndeadMinionType.Abomination)
                {
                    HandleAbominationCombat(target, dist, dir, currentSpeed, damageMult);
                }
                else
                {
                    HandleMeleeCombat(target, dist, dir, currentSpeed, damageMult);
                }
            }
            else
            {
                // Follow player if no enemies found
                var player = ActorRegistry.Player;
                if (player != null && player.Alive)
                {
                    Vector2 diff = player.Position - Position;
                    if (diff.magnitude > 2.5f)
                    {
                        _rb.linearVelocity = diff.normalized * currentSpeed;
                    }
                    else
                    {
                        _rb.linearVelocity = Vector2.zero;
                    }
                }
            }
        }

        private bool TryScavengeNearestLoot(float currentSpeed)
        {
            var pickups = FindObjectsByType<LootPickupActor>();
            LootPickupActor nearest = null;
            float nearestDistSqr = 36f; // 6m search
            Vector2 pos = Position;

            for (int i = 0; i < pickups.Length; i++)
            {
                var p = pickups[i];
                if (p != null && !p.IsCollected)
                {
                    float dSqr = ((Vector2)p.transform.position - pos).sqrMagnitude;
                    if (dSqr < nearestDistSqr)
                    {
                        nearestDistSqr = dSqr;
                        nearest = p;
                    }
                }
            }

            if (nearest != null)
            {
                Vector2 diff = (Vector2)nearest.transform.position - pos;
                float dist = diff.magnitude;
                if (dist > 0.6f)
                {
                    _rb.linearVelocity = (diff / dist) * currentSpeed;
                }
                else
                {
                    _rb.linearVelocity = Vector2.zero;
                    if (nearest.TryCollect())
                    {
                        // Award extra +5 Soul Essence to Lich Lord
                        if (ActorRegistry.Player != null && ActorRegistry.Player.Run != null && ActorRegistry.Player.Run.Hud != null)
                        {
                            ActorRegistry.Player.Run.Hud.AddResource(5f);
                        }
                    }
                }
                return true;
            }

            return false;
        }

        private void HandleMeleeCombat(IDamageable target, float dist, Vector2 dir, float currentSpeed, float damageMult)
        {
            if (dist > _attackRange)
            {
                _rb.linearVelocity = dir * currentSpeed;
            }
            else
            {
                _rb.linearVelocity = Vector2.zero;
                if (_cooldownTimer <= 0f)
                {
                    float finalDamage = _damage * damageMult;
                    target.ReceiveDamage(finalDamage);
                    _cooldownTimer = _attackCooldown;

                    if (_type == UndeadMinionType.Spearman)
                    {
                        CueBus.Raise(CueIds.BossCleave, target.Position.x, target.Position.y);
                    }
                    else if (_type == UndeadMinionType.Hound)
                    {
                        CueBus.Raise(CueIds.InvaderStrike, target.Position.x, target.Position.y);
                    }
                    else if (_type == UndeadMinionType.Scavenger)
                    {
                        CueBus.Raise(CueIds.InvaderStrike, target.Position.x, target.Position.y);
                        // Award +5 Soul Essence to player on hit
                        if (ActorRegistry.Player != null && ActorRegistry.Player.Run != null && ActorRegistry.Player.Run.Hud != null)
                        {
                            ActorRegistry.Player.Run.Hud.AddResource(5f);
                        }
                    }
                    else
                    {
                        CueBus.Raise(CueIds.InvaderStrike, target.Position.x, target.Position.y);
                    }
                }
            }
        }

        private void HandleMageCombat(IDamageable target, float dist, Vector2 dir, float currentSpeed, float damageMult)
        {
            float desiredDist = 5.0f;
            if (dist > desiredDist + 1f)
            {
                _rb.linearVelocity = dir * currentSpeed;
            }
            else if (dist < desiredDist - 1f)
            {
                _rb.linearVelocity = -dir * (currentSpeed * 0.8f);
            }
            else
            {
                _rb.linearVelocity = Vector2.zero;
            }

            if (_cooldownTimer <= 0f && dist <= _attackRange && ProjectileManager.Instance != null)
            {
                _cooldownTimer = _attackCooldown;
                ProjectileManager.Instance.FireCustom(
                    Faction.Player,
                    Position,
                    dir,
                    speed: 10f,
                    radius: 0.22f,
                    damage: _damage * damageMult,
                    lifetime: 1.5f,
                    color: new Color(0.6f, 0.4f, 0.95f),
                    cueId: CueIds.BoltFire,
                    pierce: 1
                );
            }
        }

        private void HandleAbominationCombat(IDamageable target, float dist, Vector2 dir, float currentSpeed, float damageMult)
        {
            if (dist > _attackRange)
            {
                _rb.linearVelocity = dir * currentSpeed;
            }
            else
            {
                _rb.linearVelocity = Vector2.zero;
                if (_cooldownTimer <= 0f)
                {
                    _cooldownTimer = _attackCooldown;
                    float finalDamage = _damage * damageMult;

                    // Cleave smash: damage target and all invaders within 3.0m
                    target.ReceiveDamage(finalDamage);
                    Vector2 smashPos = target.Position;

                    var all = ActorRegistry.AllActors;
                    for (int i = 0; i < all.Count; i++)
                    {
                        var a = all[i];
                        if (a != null && a.Team == Faction.Invader && a.Alive && a.Id != target.Id)
                        {
                            if (((Vector2)a.Position - smashPos).sqrMagnitude <= 9f) // 3.0m radius
                            {
                                a.ReceiveDamage(finalDamage * 0.75f);
                            }
                        }
                    }

                    CueBus.Raise(CueIds.BossCleave, smashPos.x, smashPos.y);
                }
            }
        }

        private void HandleDragonCombat(IDamageable target, float dist, Vector2 dir, float currentSpeed, float damageMult)
        {
            if (dist > _attackRange)
            {
                _rb.linearVelocity = dir * currentSpeed;
            }
            else
            {
                _rb.linearVelocity = Vector2.zero;
                if (_cooldownTimer <= 0f)
                {
                    _cooldownTimer = _attackCooldown;
                    float finalDamage = _damage * damageMult;

                    // Cone flame breath: damage all invaders within 5.0m in front cone
                    Vector2 forward = dir;
                    var all = ActorRegistry.AllActors;
                    for (int i = 0; i < all.Count; i++)
                    {
                        var a = all[i];
                        if (a != null && a.Team == Faction.Invader && a.Alive)
                        {
                            Vector2 toA = a.Position - Position;
                            if (toA.sqrMagnitude <= 25f) // 5m radius
                            {
                                float angle = Vector2.Angle(forward, toA);
                                if (angle <= 50f) // 100 degree cone
                                {
                                    a.ReceiveDamage(finalDamage);
                                }
                            }
                        }
                    }

                    CueBus.Raise(CueIds.BossCleave, Position.x + forward.x * 2f, Position.y + forward.y * 2f);
                }
            }
        }
    }
}
