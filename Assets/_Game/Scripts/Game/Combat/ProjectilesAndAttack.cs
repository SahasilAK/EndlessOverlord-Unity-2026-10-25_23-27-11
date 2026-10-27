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
            var customBolt = SpriteSheetRegistry.GetIdleSprite("_Game/Art/VFX/vfx_projectile_bolt.png", 16, 16, 1, new Vector2(0.5f, 0.5f), 32f);
            sr.sprite = customBolt != null ? customBolt : PlaceholderSprites.Square;
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
            float speed, float radius, float damage, float lifetime, Color color, string cueId = null, int pierce = 0)
        {
            var bolt = _pool.Get();
            var sr = bolt.GetComponent<SpriteRenderer>();
            if (sr != null) sr.color = color;
            bolt.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);
            bolt.State.Launch(owner, origin.x, origin.y, direction.x, direction.y,
                speed, radius, damage, lifetime, pierce);
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

                        // Life steal mechanic for player & class resource gain
                        if (bolt.State.Owner == Faction.Player && ActorRegistry.Player != null && ActorRegistry.Player.Alive)
                        {
                            var player = ActorRegistry.Player;
                            float ls = player.LifeStealFraction;
                            if (ls > 0f && dealt > 0f)
                            {
                                player.Health.Heal(dealt * ls);
                            }

                            if (player.Run != null && player.Run.Hud != null && player.Run.Character != null)
                            {
                                if (player.Run.Character.Id == CharacterCatalog.LichLordId)
                                {
                                    player.Run.Hud.AddResource(2f); // Lich Lord gains Soul Essence on bolt hit
                                }
                                else if (player.Run.Character.Id == CharacterCatalog.VampireQueenId)
                                {
                                    player.Run.Hud.AddResource(6f); // Vampire Queen gains Blood Essence on needle hit
                                    if (dealt > 0f)
                                    {
                                        player.Health.Heal(dealt * 0.10f); // Base 10% lifesteal
                                    }
                                }
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
    /// Automatic combat simulator supporting all 5 playable class archetypes:
    /// - Lich Lord: Necrotic Bolt projectile (+2 Soul Essence on hit).
    /// - Demon Monarch: Wide 120° flaming sword cleave (+8 Infernal Fury per hit).
    /// - Vampire Queen: Fast piercing Crimson Needle projectile (+6 Blood Essence on hit, 10% base lifesteal).
    /// - Shadow Reaper: Fast 140° spectral scythe sweep (+1 Reaper's Mark per hit, max 10).
    /// - Cursed Golem: Colossus stone punch with stagger and knockback (+8 Runic Charge on hit).
    /// Fires on its own cooldown automatically based on nearest enemy acquisition; no manual attack key required.
    /// </summary>
    public sealed class AutoAttacker : MonoBehaviour
    {
        private readonly List<TargetCandidate> _buffer = new List<TargetCandidate>();
        private readonly List<IDamageable> _meleeHitBuffer = new List<IDamageable>();
        private AutoAttackSim _sim;
        private PlayerActor _player;
        private ProjectileManager _projectiles;

        public int ShotsFired { get; private set; }

        public void Initialize(PlayerActor player, ProjectileManager projectiles)
        {
            _player = player;
            _projectiles = projectiles;
            _sim = new AutoAttackSim(GetBaseCooldown());

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

        public float GetBaseCooldown()
        {
            if (_player?.Run?.Character == null) return CombatTuning.BoltCooldown;
            switch (_player.Run.Character.Id)
            {
                case CharacterCatalog.LichLordId: return 0.55f;
                case CharacterCatalog.DemonMonarchId: return 0.65f;
                case CharacterCatalog.VampireQueenId: return 0.40f;
                case CharacterCatalog.ShadowReaperId: return 0.45f;
                case CharacterCatalog.CursedGolemId: return 0.85f;
                default: return 0.55f;
            }
        }

        public float GetBaseRange()
        {
            if (_player?.Run?.Character == null) return CombatTuning.BoltRange;
            switch (_player.Run.Character.Id)
            {
                case CharacterCatalog.LichLordId: return 9.0f;
                case CharacterCatalog.DemonMonarchId: return 2.6f;
                case CharacterCatalog.VampireQueenId: return 8.5f;
                case CharacterCatalog.ShadowReaperId: return 2.2f;
                case CharacterCatalog.CursedGolemId: return 1.8f;
                default: return CombatTuning.BoltRange;
            }
        }

        public float GetBaseDamage()
        {
            if (_player?.Run?.Character == null) return CombatTuning.BoltDamage;
            switch (_player.Run.Character.Id)
            {
                case CharacterCatalog.LichLordId: return 12f;
                case CharacterCatalog.DemonMonarchId: return 22f;
                case CharacterCatalog.VampireQueenId: return 9f;
                case CharacterCatalog.ShadowReaperId: return 16f;
                case CharacterCatalog.CursedGolemId: return 32f;
                default: return CombatTuning.BoltDamage;
            }
        }

        private void UpdateCooldown()
        {
            var equipStats = _player.Run != null && _player.Run.Inventory != null ? _player.Run.Inventory.GetEquippedStats() : default(ItemStats);
            float speedMult = 1f + (_player.Run != null && _player.Run.Rewards != null ? _player.Run.Rewards.BonusAttackSpeedPercent : 0f) + equipStats.AttackSpeedPercent;
            _sim.SetCooldownDuration(GetBaseCooldown() / speedMult);
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

            float range = GetBaseRange() + (_player.Run != null && _player.Run.Rewards != null ? _player.Run.Rewards.BonusRange : 0f);
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
                float metaDmg = _player.Run != null ? _player.Run.MetaStats.BonusDamagePercent : 0f;
                float baseDmg = GetBaseDamage() + equipStats.FlatDamage;
                float dmgMult = 1f + (_player.Run != null && _player.Run.Rewards != null ? _player.Run.Rewards.BonusDamagePercent : 0f) + equipStats.DamagePercent + metaDmg;
                float damage = baseDmg * dmgMult;
                int extra = _player.Run != null && _player.Run.Rewards != null ? _player.Run.Rewards.ExtraProjectiles : 0;

                string charId = _player.Run?.Character?.Id ?? CharacterCatalog.LichLordId;

                switch (charId)
                {
                    case CharacterCatalog.LichLordId:
                        FireRangedProjectiles(pos, dir, damage, extra, CombatTuning.BoltSpeed, CombatTuning.BoltRadius,
                            CombatTuning.BoltLifetime, new Color(0.6f, 1f, 0.55f), CueIds.BoltFire);
                        break;

                    case CharacterCatalog.DemonMonarchId:
                        ExecuteMeleeCleave(pos, dir, 2.6f, 120f, damage, new Color(1f, 0.4f, 0.1f, 0.8f), 8f);
                        break;

                    case CharacterCatalog.VampireQueenId:
                        FireRangedProjectiles(pos, dir, damage, extra, 16f, 0.18f, 1.5f,
                            new Color(0.9f, 0.1f, 0.25f), CueIds.BoltFire, pierce: 1);
                        break;

                    case CharacterCatalog.ShadowReaperId:
                        ExecuteMeleeCleave(pos, dir, 2.2f, 140f, damage, new Color(0.7f, 0.2f, 0.9f, 0.8f), 1f);
                        break;

                    case CharacterCatalog.CursedGolemId:
                        ExecuteStonePunch(target, dir, damage);
                        break;

                    default:
                        FireRangedProjectiles(pos, dir, damage, extra, CombatTuning.BoltSpeed, CombatTuning.BoltRadius,
                            CombatTuning.BoltLifetime, new Color(0.6f, 1f, 0.55f), CueIds.BoltFire);
                        break;
                }

                ShotsFired++;
            }
            else if (valid)
            {
                Vector2 d = target.Position - pos;
                if (d.sqrMagnitude > 1e-6f) _player.AimDirection = d.normalized;
            }
        }

        private void FireRangedProjectiles(Vector2 pos, Vector2 dir, float damage, int extra,
            float speed, float radius, float lifetime, Color color, string cueId, int pierce = 0)
        {
            if (extra <= 0)
            {
                _projectiles.FireCustom(Faction.Player, pos, dir, speed, radius, damage, lifetime, color, cueId, pierce);
            }
            else
            {
                float step = 12f;
                float startAngle = -step * extra * 0.5f;
                for (int p = 0; p <= extra; p++)
                {
                    float angle = startAngle + step * p;
                    Vector2 spreadDir = RotateVector(dir, angle);
                    _projectiles.FireCustom(Faction.Player, pos, spreadDir, speed, radius, damage, lifetime, color, p == 0 ? cueId : null, pierce);
                }
            }
        }

        private void ExecuteMeleeCleave(Vector2 pos, Vector2 aimDir, float range, float arcDegrees, float damage, Color slashColor, float resourcePerHit)
        {
            _meleeHitBuffer.Clear();
            float halfArc = arcDegrees * 0.5f;
            var all = ActorRegistry.AllActors;

            for (int i = 0; i < all.Count; i++)
            {
                var actor = all[i];
                if (actor.Team == Faction.Invader && actor.Alive)
                {
                    Vector2 toEnemy = actor.Position - pos;
                    float dist = toEnemy.magnitude;
                    if (dist <= range + actor.Radius)
                    {
                        float angle = Vector2.Angle(aimDir, toEnemy);
                        if (angle <= halfArc)
                        {
                            _meleeHitBuffer.Add(actor);
                        }
                    }
                }
            }

            for (int i = 0; i < _meleeHitBuffer.Count; i++)
            {
                var enemy = _meleeHitBuffer[i];
                float dealt = enemy.ReceiveDamage(damage);
                if (dealt > 0f && _player.Run?.Hud != null)
                {
                    _player.Run.Hud.AddResource(resourcePerHit);
                }
            }

            SpawnSlashVisual(pos + aimDir * (range * 0.5f), range, slashColor);
            CueBus.Raise(CueIds.InvaderStrike, pos.x, pos.y);
        }

        private void ExecuteStonePunch(IDamageable target, Vector2 dir, float damage)
        {
            float dealt = target.ReceiveDamage(damage);
            if (dealt > 0f && _player.Run?.Hud != null)
            {
                _player.Run.Hud.AddResource(8f); // Cursed Golem gains Runic Charge
            }

            if (target is InvaderActor invader)
            {
                invader.ApplyKnockback(dir * 3.0f);
            }

            SpawnSlashVisual(target.Position, 1.2f, new Color(0.2f, 0.8f, 0.7f, 0.8f));
            CueBus.Raise(CueIds.InvaderStrike, target.Position.x, target.Position.y);
        }

        private static void SpawnSlashVisual(Vector2 pos, float size, Color color)
        {
            var go = new GameObject("MeleeSlashVisual");
            go.transform.position = pos;
            go.transform.localScale = new Vector3(size, size * 0.4f, 1f);

            var sr = go.AddComponent<SpriteRenderer>();
            var customSlash = SpriteSheetRegistry.GetIdleSprite("_Game/Art/VFX/vfx_melee_slash.png", 32, 16, 1, new Vector2(0.5f, 0.5f), 32f);
            sr.sprite = customSlash != null ? customSlash : PlaceholderSprites.Square;
            sr.color = color;
            sr.sortingOrder = 12;

            go.AddComponent<TemporaryFade>().Initialize(0.12f);
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
