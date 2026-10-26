using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>Visual shell for one pooled projectile. The simulation state lives in <see cref="State"/>.</summary>
    public sealed class BoltProjectile : MonoBehaviour
    {
        public readonly ProjectileState State = new ProjectileState();

        public void SyncTransform()
        {
            transform.position = new Vector3(State.X, State.Y, 0f);
        }
    }

    /// <summary>
    /// Owns the projectile pool and steps every active projectile in FixedUpdate. Damage is applied here,
    /// from the simulation result, never from physics callbacks or animation events. If a target dies
    /// between firing and arrival the projectile simply continues (or expires) - nothing dereferences it.
    /// Supports friendly fire separation and player life steal.
    /// </summary>
    public sealed class ProjectileManager : MonoBehaviour
    {
        private readonly List<BoltProjectile> _active = new List<BoltProjectile>();
        private readonly List<TargetCandidate> _buffer = new List<TargetCandidate>();
        private ObjectPool<BoltProjectile> _pool;

        public static ProjectileManager Instance { get; private set; }

        public int ActiveCount { get { return _active.Count; } }
        public int PoolTotal { get { return _pool.CountAll; } }
        public int PoolInactive { get { return _pool.CountInactive; } }

        private void Awake()
        {
            Instance = this;
            _pool = new ObjectPool<BoltProjectile>(CreateBolt, b => b.gameObject.SetActive(true), ResetBolt);
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        private BoltProjectile CreateBolt()
        {
            var go = new GameObject("NecroticBolt");
            go.transform.SetParent(transform, false);
            go.transform.localScale = new Vector3(CombatTuning.BoltRadius * 2f, CombatTuning.BoltRadius * 2f, 1f);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = PlaceholderSprites.Square;
            sr.color = new Color(0.6f, 1f, 0.55f);
            sr.sortingOrder = 15;
            return go.AddComponent<BoltProjectile>();
        }

        private static void ResetBolt(BoltProjectile b)
        {
            b.State.Reset();
            var sr = b.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = new Color(0.6f, 1f, 0.55f);
            b.gameObject.SetActive(false);
        }

        public BoltProjectile Fire(Faction owner, Vector2 origin, Vector2 direction)
        {
            return FireCustom(owner, origin, direction, CombatTuning.BoltSpeed, CombatTuning.BoltRadius,
                CombatTuning.BoltDamage, CombatTuning.BoltLifetime, new Color(0.6f, 1f, 0.55f), CueIds.BoltFire);
        }

        public BoltProjectile FireCustom(Faction owner, Vector2 origin, Vector2 direction,
            float speed, float radius, float damage, float lifetime, Color color, string cueId = null)
        {
            var bolt = _pool.Get();
            var sr = bolt.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = color;
            bolt.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
            bolt.State.Launch(owner, origin.x, origin.y, direction.x, direction.y,
                speed, radius, damage, lifetime);
            bolt.SyncTransform();
            _active.Add(bolt);
            if (!string.IsNullOrEmpty(cueId))
                CueBus.Raise(cueId, origin.x, origin.y);
            return bolt;
        }

        private void FixedUpdate()
        {
            if (_active.Count == 0)
                return;
            ActorRegistry.FillCandidates(_buffer);
            float dt = Time.fixedDeltaTime;
            for (int i = _active.Count - 1; i >= 0; i--)
            {
                var bolt = _active[i];
                var result = bolt.State.Step(dt, _buffer, ArenaConfig.HalfWidth, ArenaConfig.HalfHeight);
                bolt.SyncTransform();

                if (result.Outcome == ProjectileOutcome.HitTarget)
                {
                    IDamageable target;
                    if (ActorRegistry.TryGet(result.TargetId, out target))
                    {
                        float dealt = target.ReceiveDamage(bolt.State.Damage);
                        CueBus.Raise(CueIds.BoltHit, bolt.State.X, bolt.State.Y, dealt);

                        // Life steal mechanic for player
                        if (bolt.State.Owner == Faction.Player && ActorRegistry.Player != null && ActorRegistry.Player.Alive)
                        {
                            float ls = ActorRegistry.Player.LifeStealFraction;
                            if (ls > 0f && dealt > 0f)
                            {
                                ActorRegistry.Player.Health.Heal(dealt * ls);
                            }
                        }

                        if (!target.Alive)
                            MarkDead(result.TargetId);
                    }
                }

                if (!bolt.State.Active)
                {
                    _active.RemoveAt(i);
                    _pool.Release(bolt);
                }
            }
        }

        private void MarkDead(int id)
        {
            for (int i = 0; i < _buffer.Count; i++)
            {
                if (_buffer[i].Id == id)
                {
                    var c = _buffer[i];
                    c.Alive = false;
                    _buffer[i] = c;
                }
            }
        }
    }

    /// <summary>
    /// Necrotic Bolt auto attack. Acquires the nearest valid invader in range and fires on its own
    /// cooldown; there is no attack button. Integrates runtime damage, range, attack speed and multi-shot buffs.
    /// </summary>
    public sealed class AutoAttacker : MonoBehaviour
    {
        private readonly List<TargetCandidate> _buffer = new List<TargetCandidate>();
        private AutoAttackSim _sim;
        private PlayerActor _player;
        private ProjectileManager _projectiles;

        public int ShotsFired { get; private set; }

        public void Initialize(PlayerActor player, ProjectileManager projectiles)
        {
            _player = player;
            _projectiles = projectiles;
            _sim = new AutoAttackSim(CombatTuning.BoltCooldown);

            if (_player != null && _player.Run != null)
            {
                if (_player.Run.Rewards != null)
                    _player.Run.Rewards.RewardAcquired += OnRewardAcquired;
                if (_player.Run.Inventory != null)
                    _player.Run.Inventory.Changed += OnEquipmentChanged;
                UpdateCooldown();
            }
        }

        private void OnDestroy()
        {
            if (_player != null && _player.Run != null)
            {
                if (_player.Run.Rewards != null)
                    _player.Run.Rewards.RewardAcquired -= OnRewardAcquired;
                if (_player.Run.Inventory != null)
                    _player.Run.Inventory.Changed -= OnEquipmentChanged;
            }
        }

        private void OnRewardAcquired(RewardDefinition def, int newRank)
        {
            if (def.ModifierType == RewardModifierType.AttackSpeedPercent)
            {
                UpdateCooldown();
            }
        }

        private void OnEquipmentChanged()
        {
            UpdateCooldown();
        }

        private void UpdateCooldown()
        {
            var equipStats = _player.Run != null && _player.Run.Inventory != null ? _player.Run.Inventory.GetEquippedStats() : default(ItemStats);
            float speedMult = 1f + (_player.Run != null && _player.Run.Rewards != null ? _player.Run.Rewards.BonusAttackSpeedPercent : 0f) + equipStats.AttackSpeedPercent;
            _sim.SetCooldownDuration(CombatTuning.BoltCooldown / speedMult);
        }

        private void FixedUpdate()
        {
            if (_player == null || !_player.Alive)
            {
                if (_player != null) _player.HasTargetAim = false;
                return;
            }

            var pos = _player.Position;
            ActorRegistry.FillCandidates(_buffer);

            float range = CombatTuning.BoltRange + (_player.Run != null && _player.Run.Rewards != null ? _player.Run.Rewards.BonusRange : 0f);
            int targetId = TargetSelector.FindNearest(pos.x, pos.y, range, Faction.Player, _buffer);

            IDamageable target = null;
            bool valid = targetId != TargetSelector.None && ActorRegistry.TryGet(targetId, out target) && target.Alive;
            _player.HasTargetAim = valid;

            if (_sim.Tick(Time.fixedDeltaTime, valid))
            {
                Vector2 dir = target.Position - pos;
                if (dir.sqrMagnitude < 1e-6f) dir = _player.AimDirection;
                dir.Normalize();
                _player.AimDirection = dir;

                var equipStats = _player.Run != null && _player.Run.Inventory != null ? _player.Run.Inventory.GetEquippedStats() : default(ItemStats);
                float baseDmg = CombatTuning.BoltDamage + equipStats.FlatDamage;
                float dmgMult = 1f + (_player.Run != null && _player.Run.Rewards != null ? _player.Run.Rewards.BonusDamagePercent : 0f) + equipStats.DamagePercent;
                float damage = baseDmg * dmgMult;
                int extra = _player.Run != null && _player.Run.Rewards != null ? _player.Run.Rewards.ExtraProjectiles : 0;

                if (extra <= 0)
                {
                    _projectiles.FireCustom(Faction.Player, pos, dir, CombatTuning.BoltSpeed, CombatTuning.BoltRadius,
                        damage, CombatTuning.BoltLifetime, new Color(0.6f, 1f, 0.55f), CueIds.BoltFire);
                }
                else
                {
                    float step = 12f;
                    float startAngle = -step * extra * 0.5f;
                    for (int p = 0; p <= extra; p++)
                    {
                        float angle = startAngle + step * p;
                        Vector2 spreadDir = RotateVector(dir, angle);
                        _projectiles.FireCustom(Faction.Player, pos, spreadDir, CombatTuning.BoltSpeed, CombatTuning.BoltRadius,
                            damage, CombatTuning.BoltLifetime, new Color(0.6f, 1f, 0.55f), p == 0 ? CueIds.BoltFire : null);
                    }
                }

                ShotsFired++;
            }
            else if (valid)
            {
                Vector2 d = target.Position - pos;
                if (d.sqrMagnitude > 1e-6f) _player.AimDirection = d.normalized;
            }
        }

        private static Vector2 RotateVector(Vector2 v, float degrees)
        {
            float rad = degrees * Mathf.Deg2Rad;
            float cos = Mathf.Cos(rad);
            float sin = Mathf.Sin(rad);
            return new Vector2(v.x * cos - v.y * sin, v.x * sin + v.y * cos);
        }
    }
}
