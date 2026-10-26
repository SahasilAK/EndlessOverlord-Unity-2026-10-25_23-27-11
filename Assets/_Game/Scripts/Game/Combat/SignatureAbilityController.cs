using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Executes the active signature ability for the current player character.
    /// Gated by class resource cost and cooldown.
    /// Subscribes to SkillPressed(slot 0) from PlayerInputAdapter.
    /// </summary>
    public sealed class SignatureAbilityController : MonoBehaviour
    {
        private PlayerActor _player;
        private PlayerInputAdapter _input;
        private RunContext _run;
        private float _cooldownTimer;
        private readonly List<IDamageable> _hitBuffer = new List<IDamageable>();

        public float CooldownRemaining => _cooldownTimer;
        public bool IsReady => _cooldownTimer <= 0f && HasSufficientResource;
        public string AbilityName => _run?.Character?.SignatureAbilityName ?? "Signature";
        public float ResourceCost => _run?.Character?.SignatureResourceCost ?? 0f;

        public bool HasSufficientResource
        {
            get
            {
                if (_run == null || _run.Hud == null) return false;
                return _run.Hud.Resource >= ResourceCost;
            }
        }

        public void Initialize(PlayerActor player, PlayerInputAdapter input, RunContext run)
        {
            _player = player;
            _input = input;
            _run = run;

            if (_input != null)
            {
                _input.SkillPressed += OnSkillPressed;
            }
        }

        private void OnDestroy()
        {
            if (_input != null)
            {
                _input.SkillPressed -= OnSkillPressed;
            }
        }

        private void Update()
        {
            if (_cooldownTimer > 0f)
            {
                _cooldownTimer -= Time.deltaTime;
                if (_cooldownTimer < 0f) _cooldownTimer = 0f;
            }
        }

        private void OnSkillPressed(int slot)
        {
            if (slot == 0) // Slot 0 is the signature ability
            {
                TryActivate();
            }
        }

        public bool TryActivate()
        {
            if (_player == null || !_player.Alive || _run == null)
                return false;

            if (GameSession.Machine.Current != RunState.PlayingWave)
                return false;

            if (_cooldownTimer > 0f)
                return false;

            if (!_run.Hud.TrySpendResource(ResourceCost))
                return false;

            _cooldownTimer = _run.Character.SignatureCooldown;

            switch (_run.Character.Id)
            {
                case CharacterCatalog.LichLordId:
                    ExecuteLichSentinel();
                    break;
                case CharacterCatalog.DemonMonarchId:
                    ExecuteDemonHellstep();
                    break;
                case CharacterCatalog.VampireQueenId:
                    ExecuteVampireSanguineBurst();
                    break;
                case CharacterCatalog.ShadowReaperId:
                    ExecuteReaperShadowstep();
                    break;
                case CharacterCatalog.CursedGolemId:
                    ExecuteGolemRuneBarrier();
                    break;
                default:
                    ExecuteLichSentinel();
                    break;
            }

            return true;
        }

        private void ExecuteLichSentinel()
        {
            Vector2 spawnOffset = _player.AimDirection.normalized * 1.0f;
            Vector2 spawnPos = _player.Position + spawnOffset;
            BoneSentinelActor.Spawn(spawnPos);
            CueBus.Raise(CueIds.BoltFire, spawnPos.x, spawnPos.y);
            SpawnVisualBurst(spawnPos, 1.2f, new Color(0.7f, 1f, 0.9f, 0.8f), 0.25f);
        }

        private void ExecuteDemonHellstep()
        {
            Vector2 startPos = _player.Position;
            Vector2 dir = _player.AimDirection.normalized;
            Vector2 targetPos = startPos + dir * 5.0f;

            // Clamp to arena bounds
            targetPos.x = Mathf.Clamp(targetPos.x, -ArenaConfig.HalfWidth + 1.5f, ArenaConfig.HalfWidth - 1.5f);
            targetPos.y = Mathf.Clamp(targetPos.y, -ArenaConfig.HalfHeight + 1.5f, ArenaConfig.HalfHeight - 1.5f);

            // Move player
            _player.Teleport(targetPos);

            // Blast start and destination
            DamageArea(startPos, 3.0f, 30f);
            DamageArea(targetPos, 3.0f, 30f);

            SpawnVisualBurst(startPos, 3.0f, new Color(1f, 0.35f, 0.1f, 0.85f), 0.3f);
            SpawnVisualBurst(targetPos, 3.0f, new Color(1f, 0.35f, 0.1f, 0.85f), 0.3f);
            CueBus.Raise(CueIds.PlayerDash, targetPos.x, targetPos.y);
        }

        private void ExecuteVampireSanguineBurst()
        {
            Vector2 pos = _player.Position;
            float totalDamageDealt = DamageArea(pos, 5.0f, 28f);

            // Lifesteal 35% of damage dealt
            if (totalDamageDealt > 0f)
            {
                float heal = totalDamageDealt * 0.35f;
                _player.Health.Heal(heal);
                CueBus.Raise(CueIds.ClericHeal, pos.x, pos.y, heal);
            }

            SpawnVisualBurst(pos, 5.0f, new Color(0.9f, 0.1f, 0.25f, 0.8f), 0.35f);
        }

        private void ExecuteReaperShadowstep()
        {
            Vector2 startPos = _player.Position;
            Vector2 dir = _player.AimDirection.normalized;
            Vector2 targetPos = startPos + dir * 6.0f;

            // Validate against walls and arena bounds
            targetPos = ValidateLandingDestination(startPos, targetPos);
            _player.Teleport(targetPos);

            // Devastating 360 degree shadow whirlwind
            DamageArea(targetPos, 3.5f, 40f);
            SpawnVisualBurst(targetPos, 3.5f, new Color(0.6f, 0.2f, 0.9f, 0.85f), 0.3f);
            CueBus.Raise(CueIds.InvaderStrike, targetPos.x, targetPos.y);
        }

        private void ExecuteGolemRuneBarrier()
        {
            Vector2 pos = _player.Position;
            _player.SetBarrier(60f, duration: 8.0f);

            // Ground tremor shockwave
            DamageArea(pos, 3.5f, 15f, knockbackForce: 3.5f);
            SpawnVisualBurst(pos, 3.5f, new Color(0.1f, 0.8f, 0.7f, 0.85f), 0.4f);
            CueBus.Raise(CueIds.InvaderStrike, pos.x, pos.y);
        }

        private Vector2 ValidateLandingDestination(Vector2 start, Vector2 target)
        {
            float halfW = ArenaConfig.HalfWidth - 1.5f;
            float halfH = ArenaConfig.HalfHeight - 1.5f;
            target.x = Mathf.Clamp(target.x, -halfW, halfW);
            target.y = Mathf.Clamp(target.y, -halfH, halfH);

            var layout = _run?.Layout;
            if (layout == null) return target;

            // Check if target is inside any wall or obstacle
            bool insideObstacle = true;
            int steps = 10;
            Vector2 current = target;

            for (int s = 0; s < steps; s++)
            {
                var pt = new Vector2D(current.x, current.y);
                insideObstacle = false;

                for (int w = 0; w < layout.Walls.Count; w++)
                {
                    if (layout.Walls[w].Contains(pt, 0.8f))
                    {
                        insideObstacle = true;
                        break;
                    }
                }

                if (!insideObstacle)
                {
                    for (int ob = 0; ob < layout.Obstacles.Count; ob++)
                    {
                        if (layout.Obstacles[ob].Contains(pt, 0.8f))
                        {
                            insideObstacle = true;
                            break;
                        }
                    }
                }

                if (!insideObstacle)
                    return current;

                // Step back towards start
                current = Vector2.Lerp(target, start, (s + 1) / (float)steps);
            }

            return start; // Fallback to start if no point along ray is clear
        }

        private float DamageArea(Vector2 center, float radius, float damage, float knockbackForce = 0f)
        {
            float totalDealt = 0f;
            _hitBuffer.Clear();

            var all = ActorRegistry.AllActors;
            for (int i = 0; i < all.Count; i++)
            {
                var a = all[i];
                if (a.Team == Faction.Invader && a.Alive)
                {
                    float dist = Vector2.Distance(center, a.Position);
                    if (dist <= radius)
                    {
                        _hitBuffer.Add(a);
                    }
                }
            }

            for (int i = 0; i < _hitBuffer.Count; i++)
            {
                var target = _hitBuffer[i];
                float dealt = target.ReceiveDamage(damage);
                totalDealt += dealt;

                if (knockbackForce > 0f && target is InvaderActor invader)
                {
                    Vector2 pushDir = (invader.Position - center).normalized;
                    invader.ApplyKnockback(pushDir * knockbackForce);
                }
            }

            return totalDealt;
        }

        private static void SpawnVisualBurst(Vector2 pos, float size, Color color, float duration)
        {
            var go = new GameObject("BurstVisual");
            go.transform.position = pos;
            go.transform.localScale = new Vector3(size, size, 1f);

            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = PlaceholderSprites.Square;
            sr.color = color;
            sr.sortingOrder = 9;

            go.AddComponent<TemporaryFade>().Initialize(duration);
        }
    }

    public sealed class TemporaryFade : MonoBehaviour
    {
        private float _duration;
        private float _elapsed;
        private SpriteRenderer _sr;
        private Color _initialColor;

        public void Initialize(float duration)
        {
            _duration = duration;
            _sr = GetComponent<SpriteRenderer>();
            if (_sr != null) _initialColor = _sr.color;
        }

        private void Update()
        {
            _elapsed += Time.deltaTime;
            if (_elapsed >= _duration)
            {
                Destroy(gameObject);
                return;
            }

            if (_sr != null)
            {
                float a = 1f - (_elapsed / _duration);
                _sr.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, _initialColor.a * a);
            }
        }
    }
}
