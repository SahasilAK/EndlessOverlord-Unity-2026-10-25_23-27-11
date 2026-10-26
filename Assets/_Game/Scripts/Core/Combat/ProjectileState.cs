using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public enum ProjectileOutcome { None, HitTarget, HitWall, Expired }

    public struct ProjectileResult
    {
        public ProjectileOutcome Outcome;
        public int TargetId;
    }

    /// <summary>
    /// Pure projectile simulation: ownership, straight travel, lifetime, arena-bounds impact and a swept
    /// circle test against candidates (no tunnelling at speed). Same-team candidates are ignored (no friendly
    /// fire) and a hit set prevents hitting the same target twice. After an outcome other than None the
    /// projectile is inactive and further steps do nothing.
    /// </summary>
    public sealed class ProjectileState
    {
        private readonly HashSet<int> _hitIds = new HashSet<int>();

        public bool Active { get; private set; }
        public Faction Owner { get; private set; }
        public float X { get; private set; }
        public float Y { get; private set; }
        public float DirX { get; private set; }
        public float DirY { get; private set; }
        public float Speed { get; private set; }
        public float Radius { get; private set; }
        public float Damage { get; private set; }
        public float LifeRemaining { get; private set; }
        public int Pierce { get; private set; }

        public void Launch(Faction owner, float x, float y, float dirX, float dirY, float speed,
                           float radius, float damage, float lifetime, int pierce = 0)
        {
            float len = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            if (float.IsNaN(len) || len < 1e-5f) { dirX = 1f; dirY = 0f; len = 1f; }
            Owner = owner;
            X = x; Y = y;
            DirX = dirX / len; DirY = dirY / len;
            Speed = speed;
            Radius = radius;
            Damage = damage;
            LifeRemaining = lifetime;
            Pierce = pierce < 0 ? 0 : pierce;
            _hitIds.Clear();
            Active = true;
        }

        /// <summary>Clears every field so a pooled projectile carries nothing from its previous use.</summary>
        public void Reset()
        {
            Active = false;
            Owner = Faction.Player;
            X = Y = DirX = DirY = Speed = Radius = Damage = LifeRemaining = 0f;
            Pierce = 0;
            _hitIds.Clear();
        }

        public int HitCount { get { return _hitIds.Count; } }

        public ProjectileResult Step(float dt, IReadOnlyList<TargetCandidate> candidates, float halfWidth, float halfHeight)
        {
            var none = new ProjectileResult { Outcome = ProjectileOutcome.None, TargetId = TargetSelector.None };
            if (!Active || float.IsNaN(dt) || dt <= 0f)
                return none;

            float x0 = X, y0 = Y;
            float x1 = x0 + DirX * Speed * dt;
            float y1 = y0 + DirY * Speed * dt;

            // Find the earliest valid target along this step's segment.
            int hitId = TargetSelector.None;
            float bestT = float.MaxValue;
            if (candidates != null)
            {
                for (int i = 0; i < candidates.Count; i++)
                {
                    var c = candidates[i];
                    if (!c.Alive || c.Team == Owner || _hitIds.Contains(c.Id))
                        continue;
                    float t;
                    if (SegmentHitsCircle(x0, y0, x1, y1, c.X, c.Y, c.Radius + Radius, out t) && t < bestT)
                    {
                        bestT = t;
                        hitId = c.Id;
                    }
                }
            }

            if (hitId != TargetSelector.None)
            {
                X = x0 + (x1 - x0) * bestT;
                Y = y0 + (y1 - y0) * bestT;
                _hitIds.Add(hitId);
                if (_hitIds.Count > Pierce)
                    Active = false;
                return new ProjectileResult { Outcome = ProjectileOutcome.HitTarget, TargetId = hitId };
            }

            X = x1;
            Y = y1;
            if (Math.Abs(X) > halfWidth || Math.Abs(Y) > halfHeight)
            {
                Active = false;
                return new ProjectileResult { Outcome = ProjectileOutcome.HitWall, TargetId = TargetSelector.None };
            }

            LifeRemaining -= dt;
            if (LifeRemaining <= 0f)
            {
                Active = false;
                return new ProjectileResult { Outcome = ProjectileOutcome.Expired, TargetId = TargetSelector.None };
            }
            return none;
        }

        /// <summary>Does segment (x0,y0)-(x1,y1) touch the circle? t in [0,1] is the entry fraction.</summary>
        public static bool SegmentHitsCircle(float x0, float y0, float x1, float y1,
                                             float cx, float cy, float r, out float t)
        {
            float dx = x1 - x0, dy = y1 - y0;
            float fx = x0 - cx, fy = y0 - cy;
            float a = dx * dx + dy * dy;
            float c = fx * fx + fy * fy - r * r;
            t = 0f;
            if (c <= 0f)
                return true;                       // already overlapping at the start
            if (a < 1e-12f)
                return false;
            float b = 2f * (fx * dx + fy * dy);
            float disc = b * b - 4f * a * c;
            if (disc < 0f)
                return false;
            float sq = (float)Math.Sqrt(disc);
            float t0 = (-b - sq) / (2f * a);
            if (t0 >= 0f && t0 <= 1f)
            {
                t = t0;
                return true;
            }
            return false;
        }
    }
}
