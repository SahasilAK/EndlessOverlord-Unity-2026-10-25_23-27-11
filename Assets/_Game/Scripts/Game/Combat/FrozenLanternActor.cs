using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Orbiting wisp companion for Lich Lord's Frozen Lantern ability.
    /// Rotates around the player, periodically firing frost shards at approaching invaders.
    /// </summary>
    public sealed class FrozenLanternActor : MonoBehaviour
    {
        private PlayerActor _owner;
        private float _orbitRadius = 1.6f;
        private float _orbitAngle;
        private float _orbitSpeed = 160f; // deg/sec
        private float _lifetime = 15f;
        private float _fireCooldown = 1.5f;
        private float _fireTimer;
        private float _damage = 14f;
        private readonly List<TargetCandidate> _buffer = new List<TargetCandidate>();

        public float RemainingLifetime => _lifetime;

        public static FrozenLanternActor Spawn(PlayerActor owner, float initialAngle = 0f, float damage = 14f, float lifetime = 15f)
        {
            var go = new GameObject("FrozenLantern");
            var lantern = go.AddComponent<FrozenLanternActor>();
            lantern._owner = owner;
            lantern._orbitAngle = initialAngle;
            lantern._damage = damage;
            lantern._lifetime = lifetime;

            go.transform.localScale = new Vector3(0.45f, 0.45f, 1f);
            var sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = PlaceholderSprites.Square;
            sr.color = new Color(0.4f, 0.85f, 1f, 0.9f);
            sr.sortingOrder = 9;

            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform, false);
            labelGo.transform.localPosition = new Vector3(0f, 0.5f, 0f);
            var text = labelGo.AddComponent<TextMesh>();
            text.text = "[LANTERN]";
            text.fontSize = 16;
            text.characterSize = 0.045f;
            text.anchor = TextAnchor.MiddleCenter;
            text.color = new Color(0.6f, 0.9f, 1f);

            return lantern;
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0f || _owner == null || !_owner.Alive)
            {
                Destroy(gameObject);
                return;
            }

            _orbitAngle += _orbitSpeed * Time.deltaTime;
            float rad = _orbitAngle * Mathf.Deg2Rad;
            Vector2 offset = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * _orbitRadius;
            transform.position = _owner.Position + offset;

            _fireTimer -= Time.deltaTime;
            if (_fireTimer <= 0f && GameSession.Machine.Current == RunState.PlayingWave)
            {
                _fireTimer = _fireCooldown;
                FireShard();
            }
        }

        private void FireShard()
        {
            ActorRegistry.FillCandidates(_buffer);
            int targetId = TargetSelector.FindNearest(transform.position.x, transform.position.y, 9f, Faction.Player, _buffer);
            if (targetId != TargetSelector.None && ActorRegistry.TryGet(targetId, out var target) && target.Alive)
            {
                Vector2 diff = target.Position - (Vector2)transform.position;
                Vector2 dir = diff.sqrMagnitude > 0.001f ? diff.normalized : Vector2.right;
                if (ProjectileManager.Instance != null)
                {
                    ProjectileManager.Instance.FireCustom(
                        Faction.Player,
                        transform.position,
                        dir,
                        speed: 11f,
                        radius: 0.18f,
                        damage: _damage,
                        lifetime: 1.2f,
                        color: new Color(0.5f, 0.9f, 1f),
                        cueId: CueIds.BoltFire,
                        pierce: 1
                    );
                }
            }
        }
    }
}
