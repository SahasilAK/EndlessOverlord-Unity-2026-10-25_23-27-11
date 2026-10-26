using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class MovementAndDashTests
    {
        [Test]
        public void DiagonalSpeedEqualsStraightSpeed()
        {
            float vx, vy;
            MovementMath.Velocity(1f, 0f, 6f, out vx, out vy);
            float straight = (float)System.Math.Sqrt(vx * vx + vy * vy);
            MovementMath.Velocity(1f, 1f, 6f, out vx, out vy);
            float diagonal = (float)System.Math.Sqrt(vx * vx + vy * vy);
            Assert.AreEqual(6f, straight, 1e-4f);
            Assert.AreEqual(straight, diagonal, 1e-4f);
        }

        [Test]
        public void AnalogueInputKeepsMagnitudeAndZeroGivesZero()
        {
            float vx, vy;
            MovementMath.Velocity(0.5f, 0f, 6f, out vx, out vy);
            Assert.AreEqual(3f, vx, 1e-4f);
            MovementMath.Velocity(0f, 0f, 6f, out vx, out vy);
            Assert.AreEqual(0f, vx);
            Assert.AreEqual(0f, vy);
            MovementMath.Velocity(float.NaN, 1f, 6f, out vx, out vy);
            Assert.AreEqual(0f, vx);
        }

        [Test]
        public void DashRunsForDurationThenEnforcesCooldown()
        {
            var dash = new DashSim(speed: 20f, duration: 0.2f, cooldownTime: 1f);
            Assert.IsTrue(dash.TryStart(1f, 0f));
            Assert.IsFalse(dash.TryStart(1f, 0f), "cannot restart during dash");
            float vx, vy, travelled = 0f;
            int steps = 0;
            while (dash.Tick(0.02f, out vx, out vy))
            {
                Assert.AreEqual(20f, vx, 1e-4f);
                travelled += vx * 0.02f;
                steps++;
                Assert.Less(steps, 100);
            }
            Assert.AreEqual(4f, travelled, 0.5f);          // ~ speed * duration
            Assert.IsFalse(dash.Active);
            Assert.IsFalse(dash.CooldownReady);
            Assert.IsFalse(dash.TryStart(1f, 0f), "still on cooldown after the dash ends");
            for (int i = 0; i < 60; i++) dash.Tick(0.02f, out vx, out vy);   // 1.2s total elapsed
            Assert.IsTrue(dash.CooldownReady);
            Assert.IsTrue(dash.TryStart(0f, 1f));
        }

        [Test]
        public void DashIsInvulnerableOnlyWhileActive()
        {
            var dash = new DashSim(20f, 0.1f, 1f);
            Assert.IsFalse(dash.Invulnerable);
            dash.TryStart(1f, 0f);
            Assert.IsTrue(dash.Invulnerable);
            float vx, vy;
            for (int i = 0; i < 10; i++) dash.Tick(0.02f, out vx, out vy);
            Assert.IsFalse(dash.Invulnerable);
        }

        [Test]
        public void ZeroTimePassesNoCooldownOrDashProgress()
        {
            // Paused game => dt = 0: cooldown must not drain and the dash must not expire.
            var dash = new DashSim(20f, 0.2f, 1f);
            dash.TryStart(1f, 0f);
            float vx, vy;
            for (int i = 0; i < 100; i++) dash.Tick(0f, out vx, out vy);
            Assert.IsTrue(dash.Active);
            Assert.AreEqual(1f, dash.CooldownRemaining, 1e-5f);
        }

        [Test]
        public void DashRejectsZeroDirection()
        {
            var dash = new DashSim(20f, 0.2f, 1f);
            Assert.IsFalse(dash.TryStart(0f, 0f));
            Assert.IsTrue(dash.CooldownReady);
        }
    }

    public class DamageAndHealthTests
    {
        [Test]
        public void ArmorReducesDamageAsDocumented()
        {
            Assert.AreEqual(12f, DamageCalculator.Compute(12f, 0f), 1e-4f);
            Assert.AreEqual(6f, DamageCalculator.Compute(12f, 100f), 1e-4f);
            Assert.AreEqual(12f, DamageCalculator.Compute(12f, -50f), 1e-4f);   // negative armor ignored
            Assert.AreEqual(1f, DamageCalculator.Compute(5f, 10000f), 1e-4f);   // min 1 for real hits
            Assert.AreEqual(0f, DamageCalculator.Compute(0f, 0f));
            Assert.AreEqual(0f, DamageCalculator.Compute(float.NaN, 0f));
            Assert.AreEqual(0f, DamageCalculator.Compute(float.PositiveInfinity, 0f));
        }

        [Test]
        public void ActorDiesExactlyOnce()
        {
            var h = new Health(30f);
            int deaths = 0;
            h.Died += () => deaths++;
            Assert.AreEqual(20f, h.TakeDamage(20f));
            Assert.IsFalse(h.IsDead);
            Assert.AreEqual(10f, h.TakeDamage(50f));        // clamped to remaining HP
            Assert.IsTrue(h.IsDead);
            Assert.AreEqual(0f, h.TakeDamage(50f));         // already dead: ignored
            Assert.AreEqual(0f, h.Heal(10f));               // no healing the dead
            Assert.AreEqual(1, deaths);
        }

        [Test]
        public void ResetRestoresLivingStateAndAllowsNewDeath()
        {
            var h = new Health(10f);
            int deaths = 0;
            h.Died += () => deaths++;
            h.TakeDamage(10f);
            h.Reset(10f);
            Assert.IsFalse(h.IsDead);
            Assert.AreEqual(10f, h.Current);
            h.TakeDamage(10f);
            Assert.AreEqual(2, deaths);
        }

        [Test]
        public void InvalidDamageAndHealAreIgnored()
        {
            var h = new Health(10f);
            Assert.AreEqual(0f, h.TakeDamage(-5f));
            Assert.AreEqual(0f, h.TakeDamage(float.NaN));
            h.TakeDamage(4f);
            Assert.AreEqual(4f, h.Heal(100f));
            Assert.AreEqual(10f, h.Current);
        }
    }

    public class TargetingTests
    {
        private static TargetCandidate C(int id, Faction team, float x, float y, bool alive = true)
        {
            return new TargetCandidate { Id = id, Team = team, X = x, Y = y, Radius = 0.5f, Alive = alive };
        }

        [Test]
        public void PicksNearestEnemyInRange()
        {
            var list = new List<TargetCandidate>
            {
                C(1, Faction.Player, 0.1f, 0f),          // self/friendly: ignored
                C(100, Faction.Invader, 5f, 0f),
                C(101, Faction.Invader, 2f, 0f),
                C(102, Faction.Invader, 20f, 0f),        // out of range
            };
            Assert.AreEqual(101, TargetSelector.FindNearest(0f, 0f, 9f, Faction.Player, list));
        }

        [Test]
        public void IgnoresDeadAndReturnsNoneWhenNothingValid()
        {
            var list = new List<TargetCandidate> { C(100, Faction.Invader, 1f, 0f, alive: false), C(101, Faction.Invader, 30f, 0f) };
            Assert.AreEqual(TargetSelector.None, TargetSelector.FindNearest(0f, 0f, 9f, Faction.Player, list));
            Assert.AreEqual(TargetSelector.None, TargetSelector.FindNearest(0f, 0f, 9f, Faction.Player, new List<TargetCandidate>()));
            Assert.AreEqual(TargetSelector.None, TargetSelector.FindNearest(0f, 0f, 9f, Faction.Player, null));
            Assert.AreEqual(TargetSelector.None, TargetSelector.FindNearest(0f, 0f, 0f, Faction.Player, list));
        }

        [Test]
        public void TieBreaksOnLowestIdDeterministically()
        {
            var list = new List<TargetCandidate> { C(105, Faction.Invader, 3f, 0f), C(103, Faction.Invader, 0f, 3f), C(104, Faction.Invader, -3f, 0f) };
            Assert.AreEqual(103, TargetSelector.FindNearest(0f, 0f, 9f, Faction.Player, list));
        }

        [Test]
        public void EnemyTargetsPlayerNotOwnTeam()
        {
            var list = new List<TargetCandidate> { C(100, Faction.Invader, 1f, 0f), C(1, Faction.Player, 4f, 0f) };
            Assert.AreEqual(1, TargetSelector.FindNearest(0f, 0f, 9f, Faction.Invader, list));
        }
    }

    public class ProjectileTests
    {
        private static readonly List<TargetCandidate> NoTargets = new List<TargetCandidate>();

        private static TargetCandidate Enemy(int id, float x, float y, bool alive = true)
        {
            return new TargetCandidate { Id = id, Team = Faction.Invader, X = x, Y = y, Radius = 0.5f, Alive = alive };
        }

        private static ProjectileState Fire(float dirX = 1f, float dirY = 0f, float speed = 14f)
        {
            var p = new ProjectileState();
            p.Launch(Faction.Player, 0f, 0f, dirX, dirY, speed, 0.2f, 12f, 1.4f);
            return p;
        }

        [Test]
        public void HitsTargetAndDeactivates()
        {
            var p = Fire();
            var targets = new List<TargetCandidate> { Enemy(100, 3f, 0f) };
            ProjectileResult r = default(ProjectileResult);
            for (int i = 0; i < 50 && p.Active; i++)
                r = p.Step(0.02f, targets, 20f, 12f);
            Assert.AreEqual(ProjectileOutcome.HitTarget, r.Outcome);
            Assert.AreEqual(100, r.TargetId);
            Assert.IsFalse(p.Active);
            Assert.AreEqual(ProjectileOutcome.None, p.Step(0.02f, targets, 20f, 12f).Outcome, "inactive projectile does nothing");
        }

        [Test]
        public void FastProjectileDoesNotTunnelThroughTarget()
        {
            var p = Fire(speed: 300f);   // 6 units per 0.02s step, target only 1.4 wide
            var targets = new List<TargetCandidate> { Enemy(100, 3f, 0f) };
            var r = p.Step(0.02f, targets, 20f, 12f);
            Assert.AreEqual(ProjectileOutcome.HitTarget, r.Outcome);
        }

        [Test]
        public void NoFriendlyFireAndDeadTargetsArePassedThrough()
        {
            var p = Fire();
            var targets = new List<TargetCandidate>
            {
                new TargetCandidate { Id = 1, Team = Faction.Player, X = 2f, Y = 0f, Radius = 0.5f, Alive = true },
                Enemy(100, 4f, 0f, alive: false),
            };
            ProjectileOutcome last = ProjectileOutcome.None;
            for (int i = 0; i < 200 && p.Active; i++)
                last = p.Step(0.02f, targets, 20f, 12f).Outcome;
            Assert.AreEqual(ProjectileOutcome.Expired, last);
        }

        [Test]
        public void TargetDyingInFlightDoesNotCrashAndProjectileExpires()
        {
            var p = Fire();
            var targets = new List<TargetCandidate> { Enemy(100, 8f, 0f) };
            p.Step(0.02f, targets, 20f, 12f);
            targets[0] = Enemy(100, 8f, 0f, alive: false);    // died after firing
            ProjectileOutcome last = ProjectileOutcome.None;
            for (int i = 0; i < 200 && p.Active; i++)
                last = p.Step(0.02f, targets, 20f, 12f).Outcome;
            Assert.AreEqual(ProjectileOutcome.Expired, last);
        }

        [Test]
        public void ExpiresAfterLifetimeAndDespawnsAtWall()
        {
            var p = Fire(speed: 1f);
            ProjectileOutcome last = ProjectileOutcome.None;
            int steps = 0;
            while (p.Active && steps++ < 1000)
                last = p.Step(0.02f, NoTargets, 20f, 12f).Outcome;
            Assert.AreEqual(ProjectileOutcome.Expired, last);
            Assert.That(steps, Is.InRange(65, 75));            // ~1.4s / 0.02

            var q = Fire(speed: 40f);
            q.Launch(Faction.Player, 19.5f, 0f, 1f, 0f, 40f, 0.2f, 12f, 5f);
            Assert.AreEqual(ProjectileOutcome.HitWall, q.Step(0.02f, NoTargets, 20f, 12f).Outcome);
            Assert.IsFalse(q.Active);
        }

        [Test]
        public void NonPiercingHitsOnlyOnceAndPiercingHitsEachTargetOnce()
        {
            var targets = new List<TargetCandidate> { Enemy(100, 2f, 0f), Enemy(101, 3f, 0f) };
            var p = Fire();
            int hits = 0;
            for (int i = 0; i < 100 && p.Active; i++)
                if (p.Step(0.02f, targets, 20f, 12f).Outcome == ProjectileOutcome.HitTarget) hits++;
            Assert.AreEqual(1, hits);

            var pierce = new ProjectileState();
            pierce.Launch(Faction.Player, 0f, 0f, 1f, 0f, 14f, 0.2f, 12f, 1.4f, pierce: 1);
            var ids = new HashSet<int>();
            for (int i = 0; i < 100 && pierce.Active; i++)
            {
                var r = pierce.Step(0.02f, targets, 20f, 12f);
                if (r.Outcome == ProjectileOutcome.HitTarget) Assert.IsTrue(ids.Add(r.TargetId), "same target hit twice");
            }
            Assert.AreEqual(2, ids.Count);
        }

        [Test]
        public void ResetClearsAllState()
        {
            var p = Fire();
            p.Step(0.02f, new List<TargetCandidate> { Enemy(100, 0.1f, 0f) }, 20f, 12f);
            p.Reset();
            Assert.IsFalse(p.Active);
            Assert.AreEqual(0, p.HitCount);
            Assert.AreEqual(0f, p.X);
            Assert.AreEqual(0f, p.Damage);
            Assert.AreEqual(0f, p.LifeRemaining);
        }
    }

    public class PoolAndAttackTests
    {
        private sealed class Item { public int Dirty; }

        [Test]
        public void PoolResetsOnReleaseReusesObjectsAndIgnoresDoubleRelease()
        {
            int created = 0;
            var pool = new ObjectPool<Item>(() => { created++; return new Item(); }, null, i => i.Dirty = 0);
            var a = pool.Get();
            a.Dirty = 99;
            Assert.IsTrue(pool.Release(a));
            Assert.IsFalse(pool.Release(a), "double release must be ignored");
            Assert.AreEqual(1, pool.CountInactive);
            var b = pool.Get();
            Assert.AreSame(a, b);
            Assert.AreEqual(0, b.Dirty);
            Assert.AreEqual(1, created);
            Assert.AreEqual(1, pool.CountActive);
        }

        [Test]
        public void AutoAttackFiresWithoutInputOnlyWithTargetAndKeepsCooldownWhenNoTarget()
        {
            var sim = new AutoAttackSim(0.55f);
            Assert.IsFalse(sim.Tick(0.02f, false));
            Assert.IsFalse(sim.Tick(5f, false));           // time passes with no target
            Assert.IsTrue(sim.Tick(0.02f, true), "fires immediately once a target appears (cooldown was never spent)");
            Assert.IsFalse(sim.Tick(0.02f, true));
        }

        [Test]
        public void AutoAttackRateMatchesCooldown()
        {
            var sim = new AutoAttackSim(0.55f);
            int shots = 0;
            for (int i = 0; i < 500; i++)          // 10 seconds at 50 Hz
                if (sim.Tick(0.02f, true)) shots++;
            // Expected about 10 / 0.55 = 18.2 -> 18 or 19 shots (first is immediate).
            Assert.That(shots, Is.InRange(17, 19));
        }

        [Test]
        public void MeleeWindsUpStrikesOnceThenRecovers()
        {
            var melee = new MeleeAttackSim(range: 1.3f, windupTime: 0.5f, recoverTime: 0.9f);
            int strikes = 0, strikeStep = -1, steps = 0;
            bool sawWindup = false;
            for (; steps < 200; steps++)
            {
                bool s = melee.Tick(0.02f, 1f, true);
                if (melee.Phase == MeleePhase.Windup) sawWindup = true;
                if (s) { strikes++; if (strikeStep < 0) strikeStep = steps; }
            }
            Assert.IsTrue(sawWindup);
            // 4s total: each cycle ~ windup 0.5 + recover 0.9 (+1 tick idle) => 2-3 strikes
            Assert.That(strikes, Is.InRange(2, 3));
            Assert.That(strikeStep * 0.02f, Is.InRange(0.45f, 0.65f));   // first strike ~0.5s after contact
        }

        [Test]
        public void MeleeDoesNotWindUpOutOfRangeOrWithoutTargetAndCancelResets()
        {
            var melee = new MeleeAttackSim(1.3f, 0.5f, 0.9f);
            for (int i = 0; i < 100; i++)
            {
                Assert.IsFalse(melee.Tick(0.02f, 5f, true));
                Assert.IsFalse(melee.Tick(0.02f, 0.5f, false));
            }
            Assert.AreEqual(MeleePhase.Idle, melee.Phase);
            melee.Tick(0.02f, 1f, true);
            Assert.AreEqual(MeleePhase.Windup, melee.Phase);
            melee.Cancel();
            Assert.AreEqual(MeleePhase.Idle, melee.Phase);
            Assert.AreEqual(0f, melee.WindupProgress);
        }

        [Test]
        public void CueBusWithNoSubscribersIsSafeAndSubscribersReceive()
        {
            CueBus.ClearSubscribers();
            Assert.DoesNotThrow(() => CueBus.Raise(CueIds.BoltFire, 1f, 2f));
            CueRequest got = default(CueRequest);
            System.Action<CueRequest> h = c => got = c;
            CueBus.Requested += h;
            CueBus.Raise(CueIds.InvaderHit, 3f, 4f, 12f);
            CueBus.Requested -= h;
            Assert.AreEqual(CueIds.InvaderHit, got.Id);
            Assert.AreEqual(12f, got.Value);
        }
    }
}
