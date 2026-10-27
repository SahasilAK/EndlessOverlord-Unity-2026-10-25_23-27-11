using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Generic pool. Reset logic is supplied by the caller (<c>onRelease</c> runs on every Release and
    /// <c>onGet</c> on every Get) so reused objects never carry old state. Releasing the same object twice
    /// is ignored instead of putting it in the pool twice.
    /// </summary>
    public sealed class ObjectPool<T> where T : class
    {
        private readonly Stack<T> _inactive = new Stack<T>();
        private readonly HashSet<T> _inactiveSet = new HashSet<T>();
        private readonly Func<T> _factory;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;

        public int CountAll { get; private set; }
        public int CountInactive { get { return _inactive.Count; } }
        public int CountActive { get { return CountAll - CountInactive; } }

        public ObjectPool(Func<T> factory, Action<T> onGet = null, Action<T> onRelease = null)
        {
            _factory = factory;
            _onGet = onGet;
            _onRelease = onRelease;
        }

        public T Get()
        {
            T item;
            if (_inactive.Count > 0)
            {
                item = _inactive.Pop();
                _inactiveSet.Remove(item);
            }
            else
            {
                item = _factory();
                CountAll++;
            }
            _onGet?.Invoke(item);
            return item;
        }

        public bool Release(T item)
        {
            if (item == null || !_inactiveSet.Add(item))
                return false;
            _onRelease?.Invoke(item);
            _inactive.Push(item);
            return true;
        }
    }

    /// <summary>A request for presentation (visual/audio) to react. Gameplay never waits for or depends on it.</summary>
    public struct CueRequest
    {
        public string Id;
        public float X;
        public float Y;
        public float Value;
    }

    public static class CueIds
    {
        public const string BoltFire = "cue.bolt.fire";
        public const string BoltHit = "cue.bolt.hit";
        public const string ArrowFire = "cue.arrow.fire";
        public const string PlayerDash = "cue.player.dash";
        public const string PlayerHit = "cue.player.hit";
        public const string PlayerDeath = "cue.player.death";
        public const string InvaderWindup = "cue.invader.windup";
        public const string InvaderStrike = "cue.invader.strike";
        public const string InvaderHit = "cue.invader.hit";
        public const string InvaderDeath = "cue.invader.death";
        public const string ClericHeal = "cue.cleric.heal";
        public const string BossCleave = "cue.boss.cleave";
        public const string BossEnrage = "cue.boss.enrage";
        public const string BossShockwave = "cue.boss.shockwave";
        public const string ShieldBlock = "cue.shield.block";
        public const string Dash = "cue.dash";
        public const string Explosion = "cue.explosion";
        public const string FrostNova = "cue.frost.nova";
        public const string LightningStrike = "cue.lightning.strike";

        // Magic Schools & Spells
        public const string SpellCastFire = "cue.spell.fire";
        public const string SpellCastIce = "cue.spell.ice";
        public const string SpellCastLightning = "cue.spell.lightning";
        public const string SpellCastWater = "cue.spell.water";
        public const string SpellCastWind = "cue.spell.wind";
        public const string SpellCastEarth = "cue.spell.earth";
        public const string SpellCastPoison = "cue.spell.poison";
        public const string SpellCastBlood = "cue.spell.blood";
        public const string SpellCastShadow = "cue.spell.shadow";
        public const string SpellCastLight = "cue.spell.light";
        public const string SpellCastHoly = "cue.spell.holy";
        public const string SpellCastDark = "cue.spell.dark";
        public const string SpellCastNecromancy = "cue.spell.necromancy";
        public const string SpellCastSpirit = "cue.spell.spirit";
        public const string SpellCastArcane = "cue.spell.arcane";
        public const string SpellCastNature = "cue.spell.nature";
        public const string SpellCastVoid = "cue.spell.void";
        public const string SpellCastChaos = "cue.spell.chaos";
        public const string SpellCastTime = "cue.spell.time";
        public const string SpellCastMetal = "cue.spell.metal";
        public const string MagicMasteryLevelUp = "cue.magic.mastery";
        public const string SpellEvolved = "cue.spell.evolved";

        // In-Run Events
        public const string CrusadeEvent = "cue.event.crusade";
        public const string TreasureHeroSpawn = "cue.event.treasure_hero";
        public const string ShrineActivated = "cue.event.shrine";
        public const string DungeonMutationActive = "cue.event.mutation";
        public const string DoubleBossSpawn = "cue.event.double_boss";
    }

    /// <summary>
    /// Event hub from simulation to presentation. Subscribers (placeholder shapes today, real VFX/audio
    /// later) must unsubscribe on teardown. With no subscribers, Raise is a safe no-op.
    /// </summary>
    public static class CueBus
    {
        public static event Action<CueRequest> Requested;

        public static void Raise(string id, float x, float y, float value = 0f)
        {
            Requested?.Invoke(new CueRequest { Id = id, X = x, Y = y, Value = value });
        }

        public static void ClearSubscribers() { Requested = null; }
    }
}
