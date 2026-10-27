using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// Autonomous persistent field or hazard zone for area abilities like
    /// Permafrost Sigil, Wailing Blizzard, and Hail Crown.
    /// Handles area damaging ticks, crowd-control slows, optional movement velocity,
    /// and clean lifecycle destruction.
    /// </summary>
    public sealed class HazardZoneActor : MonoBehaviour
    {
        private float _lifetime;
        private float _radius;
        private float _damagePerTick;
        private float _tickInterval;
        private float _tickTimer;
        private float _slowPercent;
        private float _slowDuration;
        private Vector2 _velocity;
        private SpriteRenderer _sr;

        public float Radius => _radius;
        public float RemainingLifetime => _lifetime;

        public static HazardZoneActor Spawn(
            Vector2 position,
            float radius,
            float duration,
            float damagePerTick,
            float tickInterval = 0.5f,
            float slowPercent = 0.5f,
            float slowDuration = 1.0f,
            Vector2 velocity = default,
            Color color = default,
            string label = "[HAZARD]")
        {
            var go = new GameObject("HazardZone");
            go.transform.position = position;

            var hazard = go.AddComponent<HazardZoneActor>();
            hazard._lifetime = duration;
            hazard._radius = radius;
            hazard._damagePerTick = damagePerTick;
            hazard._tickInterval = tickInterval > 0f ? tickInterval : 0.5f;
            hazard._slowPercent = slowPercent;
            hazard._slowDuration = slowDuration;
            hazard._velocity = velocity;

            var sr = go.AddComponent<SpriteRenderer>();
            var customPool = SpriteSheetRegistry.GetIdleSprite("_Game/Art/VFX/vfx_hazard_pool.png", 32, 32, 1, new Vector2(0.5f, 0.5f), 32f);
            sr.sprite = customPool != null ? customPool : PlaceholderSprites.Square;
            sr.color = color == default ? new Color(0.3f, 0.7f, 1f, 0.35f) : color;
            sr.sortingOrder = 3;
            hazard._sr = sr;
            go.transform.localScale = new Vector3(radius * 2f, radius * 2f, 1f);

            var labelGo = new GameObject("Label");
            labelGo.transform.SetParent(go.transform, false);
            labelGo.transform.localPosition = new Vector3(0f, 0.4f, 0f);
            var text = labelGo.AddComponent<TextMesh>();
            text.text = label;
            text.fontSize = 18;
            text.characterSize = 0.05f;
            text.anchor = TextAnchor.MiddleCenter;
            text.color = new Color(color.r, color.g, color.b, 0.9f);

            return hazard;
        }

        private void Update()
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0f)
            {
                Destroy(gameObject);
                return;
            }

            _tickTimer -= Time.deltaTime;
            if (_tickTimer <= 0f)
            {
                _tickTimer = _tickInterval;
                ApplyHazardTick();
            }
        }

        private void FixedUpdate()
        {
            if (_velocity != Vector2.zero)
            {
                transform.position += (Vector3)(_velocity * Time.fixedDeltaTime);
            }
        }

        private void ApplyHazardTick()
        {
            if (GameSession.Machine.Current != RunState.PlayingWave)
                return;

            Vector2 center = transform.position;
            float rSqr = _radius * _radius;
            var all = ActorRegistry.AllActors;

            for (int i = 0; i < all.Count; i++)
            {
                var actor = all[i];
                if (actor != null && actor.Team == Faction.Invader && actor.Alive)
                {
                    if (((Vector2)actor.Position - center).sqrMagnitude <= rSqr)
                    {
                        if (_damagePerTick > 0f)
                        {
                            actor.ReceiveDamage(_damagePerTick);
                        }

                        if (actor is InvaderActor invader && _slowPercent > 0f)
                        {
                            invader.ApplySlow(_slowPercent, _slowDuration);
                        }
                    }
                }
            }
        }
    }
}
