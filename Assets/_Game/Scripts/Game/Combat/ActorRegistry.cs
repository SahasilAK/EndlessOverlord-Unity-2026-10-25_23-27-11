using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    public static class ArenaConfig
    {
        public const float HalfWidth = 20f;
        public const float HalfHeight = 12f;
        public const float CameraSize = 6f;
    }

    /// <summary>Anything that can be targeted and damaged by the combat simulation.</summary>
    public interface IDamageable
    {
        int Id { get; }
        Faction Team { get; }
        Vector2 Position { get; }
        float Radius { get; }
        bool Alive { get; }
        /// <summary>Applies raw damage (armor is applied inside). Returns damage actually dealt.</summary>
        float ReceiveDamage(float rawDamage);
    }

    /// <summary>
    /// Lookup of live actors by stable id for the current arena. Cleared when the arena is torn down so
    /// nothing leaks between runs.
    /// </summary>
    public static class ActorRegistry
    {
        public const int PlayerId = 1;

        private static readonly Dictionary<int, IDamageable> ById = new Dictionary<int, IDamageable>();
        private static readonly List<IDamageable> All = new List<IDamageable>();
        private static int _nextId = 100;

        public static PlayerActor Player { get; set; }

        public static int NextActorId() { return _nextId++; }

        public static int Count { get { return All.Count; } }
        public static IReadOnlyList<IDamageable> AllActors { get { return All; } }

        public static void Register(IDamageable actor)
        {
            if (actor == null || ById.ContainsKey(actor.Id))
                return;
            ById.Add(actor.Id, actor);
            All.Add(actor);
        }

        public static void Unregister(IDamageable actor)
        {
            if (actor == null)
                return;
            if (ById.Remove(actor.Id))
                All.Remove(actor);
        }

        public static bool TryGet(int id, out IDamageable actor) { return ById.TryGetValue(id, out actor); }

        public static void FillCandidates(List<TargetCandidate> buffer)
        {
            buffer.Clear();
            for (int i = 0; i < All.Count; i++)
            {
                var a = All[i];
                var p = a.Position;
                buffer.Add(new TargetCandidate { Id = a.Id, Team = a.Team, X = p.x, Y = p.y, Radius = a.Radius, Alive = a.Alive });
            }
        }

        public static void Clear()
        {
            ById.Clear();
            All.Clear();
            Player = null;
            _nextId = 100;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnPlayModeEntry() { Clear(); }
    }
}
