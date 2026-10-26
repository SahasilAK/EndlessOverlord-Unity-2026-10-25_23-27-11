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
    /// </summary>
    public sealed class ProjectileManager : MonoBehaviour
    {
        private readonly List<BoltProjectile> _active = new List<BoltProjectile>();
        private readonly List<TargetCandidate> _buffer = new List<TargetCandidate>();
        private ObjectPool<BoltProjectile> _pool;

        public int ActiveCount { get { return _active.Count; } }
        public int PoolTotal { get { return _pool.CountAll; } }
        public int PoolInactive { get { return _pool.CountInactive; } }

        private void Awake()
        {
            _pool = new ObjectPool<BoltProjectile>(CreateBolt, b => b.gameObject.SetActive(true), ResetBolt);
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
            b.gameObject.SetActive(false);
        }

        public BoltProjectile Fire(Faction owner, Vector2 origin, Vector2 direction)
        {
            var bolt = _pool.Get();
            bolt.State.Launch(owner, origin.x, origin.y, direction.x, direction.y,
                CombatTuning.BoltSpeed, CombatTuning.BoltRadius, CombatTuning.BoltDamage, CombatTuning.BoltLifetime);
            bolt.SyncTransform();
            _active.Add(bolt);
            CueBus.Raise(CueIds.BoltFire, origin.x, origin.y);
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
    /// cooldown; there is no attack button.
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
            int targetId = TargetSelector.FindNearest(pos.x, pos.y, CombatTuning.BoltRange, Faction.Player, _buffer);

            IDamageable target = null;
            bool valid = targetId != TargetSelector.None && ActorRegistry.TryGet(targetId, out target) && target.Alive;
            _player.HasTargetAim = valid;

            if (_sim.Tick(Time.fixedDeltaTime, valid))
            {
                Vector2 dir = target.Position - pos;
                if (dir.sqrMagnitude < 1e-6f) dir = _player.AimDirection;
                dir.Normalize();
                _player.AimDirection = dir;
                _projectiles.Fire(Faction.Player, pos, dir);
                ShotsFired++;
            }
            else if (valid)
            {
                Vector2 d = target.Position - pos;
                if (d.sqrMagnitude > 1e-6f) _player.AimDirection = d.normalized;
            }
        }
    }
}
