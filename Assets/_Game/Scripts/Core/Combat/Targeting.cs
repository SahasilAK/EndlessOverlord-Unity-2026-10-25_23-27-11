using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    public enum Faction
    {
        Player = 0,
        Invader = 1
    }

    /// <summary>Plain-data snapshot of something that can be targeted or hit (position + circle radius).</summary>
    public struct TargetCandidate
    {
        public int Id;
        public Faction Team;
        public float X;
        public float Y;
        public float Radius;
        public bool Alive;
    }

    public static class TargetSelector
    {
        public const int None = -1;

        /// <summary>
        /// Returns the Id of the nearest alive candidate that is NOT on <paramref name="ownTeam"/> and whose
        /// centre is within <paramref name="range"/> of the origin. Ties break on the lowest Id so results are
        /// deterministic. Uses squared distances. Returns <see cref="None"/> if nothing is valid.
        /// </summary>
        public static int FindNearest(float ox, float oy, float range, Faction ownTeam, IReadOnlyList<TargetCandidate> candidates)
        {
            if (candidates == null || range <= 0f || float.IsNaN(range))
                return None;
            float bestSqr = range * range;
            int bestId = None;
            for (int i = 0; i < candidates.Count; i++)
            {
                var c = candidates[i];
                if (!c.Alive || c.Team == ownTeam)
                    continue;
                float dx = c.X - ox, dy = c.Y - oy;
                float sqr = dx * dx + dy * dy;
                if (float.IsNaN(sqr))
                    continue;
                if (sqr < bestSqr || (sqr == bestSqr && bestId != None && c.Id < bestId) || (sqr == bestSqr && bestId == None))
                {
                    bestSqr = sqr;
                    bestId = c.Id;
                }
            }
            return bestId;
        }
    }
}
