using System;

namespace FinalBoss.Core
{
    /// <summary>Placeholder tuning for P2. Real numbers belong in balance data later (BALANCE.md).</summary>
    public static class CombatTuning
    {
        // Player (Lich Lord)
        public const float PlayerMoveSpeed = 6f;
        public const float PlayerRadius = 0.45f;
        public const float PlayerArmor = 0f;
        public const float DashSpeed = 22f;
        public const float DashDuration = 0.16f;
        public const float DashCooldown = 1.2f;

        // Necrotic Bolt
        public const float BoltDamage = 12f;
        public const float BoltCooldown = 0.55f;
        public const float BoltRange = 9f;
        public const float BoltSpeed = 14f;
        public const float BoltRadius = 0.2f;
        public const float BoltLifetime = 1.4f;

        // Basic invader (melee)
        public const float InvaderHealth = 40f;
        public const float InvaderArmor = 5f;
        public const float InvaderMoveSpeed = 3f;
        public const float InvaderRadius = 0.5f;
        public const float InvaderMeleeDamage = 10f;
        public const float InvaderMeleeRange = 1.3f;
        public const float InvaderWindup = 0.5f;
        public const float InvaderRecover = 0.9f;

        // Sandbox rewards on kill
        public const float SoulPerKill = 10f;
        public const long ScorePerKill = 100;
    }

    public static class MovementMath
    {
        /// <summary>
        /// Converts raw input into a velocity: magnitude is clamped to 1 first so diagonals are not faster,
        /// then scaled by speed. Analogue values below 1 keep their magnitude.
        /// </summary>
        public static void Velocity(float inX, float inY, float speed, out float vx, out float vy)
        {
            float mag = (float)Math.Sqrt(inX * inX + inY * inY);
            if (float.IsNaN(mag) || mag < 1e-5f)
            {
                vx = 0f; vy = 0f;
                return;
            }
            float scale = mag > 1f ? 1f / mag : 1f;
            vx = inX * scale * speed;
            vy = inY * scale * speed;
        }
    }

    /// <summary>Simple countdown. Time only advances through <see cref="Tick"/>, so a paused game never ticks it.</summary>
    public sealed class Cooldown
    {
        public float Duration { get; set; }
        public float Remaining { get; private set; }
        public bool Ready { get { return Remaining <= 0f; } }

        public Cooldown(float duration) { Duration = duration; }

        public void Trigger() { Remaining = Duration; }
        public void Reset() { Remaining = 0f; }

        public void Tick(float dt)
        {
            if (float.IsNaN(dt) || dt <= 0f) return;
            Remaining -= dt;
            if (Remaining < 0f) Remaining = 0f;
        }
    }

    /// <summary>
    /// Decides when the automatic basic attack fires: only with a valid target and a ready cooldown.
    /// No input is involved, and a missing target never consumes the cooldown.
    /// </summary>
    public sealed class AutoAttackSim
    {
        private readonly Cooldown _cooldown;

        public AutoAttackSim(float cooldownSeconds)
        {
            _cooldown = new Cooldown(cooldownSeconds);
        }

        public float CooldownRemaining { get { return _cooldown.Remaining; } }

        /// <summary>Advances time and returns true on the step the attack should fire.</summary>
        public bool Tick(float dt, bool hasValidTarget)
        {
            _cooldown.Tick(dt);
            if (!hasValidTarget || !_cooldown.Ready)
                return false;
            _cooldown.Trigger();
            return true;
        }

        public void Reset() { _cooldown.Reset(); }
    }

    /// <summary>
    /// Dash state. Moves along a fixed direction for <see cref="Duration"/> seconds at <see cref="Speed"/>,
    /// grants invulnerability for that window, then enforces a cooldown (measured from dash start).
    /// Time advances only through <see cref="Tick"/>, so pausing cannot be exploited.
    /// </summary>
    public sealed class DashSim
    {
        public float Speed { get; }
        public float Duration { get; }
        public float CooldownTime { get; }

        private float _active;
        private float _cooldown;
        private float _dx, _dy;

        public DashSim(float speed, float duration, float cooldownTime)
        {
            Speed = speed;
            Duration = duration;
            CooldownTime = cooldownTime;
        }

        public bool Active { get { return _active > 0f; } }
        public bool Invulnerable { get { return _active > 0f; } }
        public bool CooldownReady { get { return _cooldown <= 0f && _active <= 0f; } }
        public float CooldownRemaining { get { return _cooldown; } }

        public bool TryStart(float dirX, float dirY)
        {
            if (!CooldownReady)
                return false;
            float len = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            if (float.IsNaN(len) || len < 1e-4f)
                return false;
            _dx = dirX / len;
            _dy = dirY / len;
            _active = Duration;
            _cooldown = CooldownTime;
            return true;
        }

        /// <summary>Advances time; returns true and the dash velocity while the dash is active this step.</summary>
        public bool Tick(float dt, out float vx, out float vy)
        {
            vx = 0f; vy = 0f;
            if (float.IsNaN(dt) || dt <= 0f)
                return _active > 0f;
            if (_cooldown > 0f)
            {
                _cooldown -= dt;
                if (_cooldown < 0f) _cooldown = 0f;
            }
            if (_active > 0f)
            {
                vx = _dx * Speed;
                vy = _dy * Speed;
                _active -= dt;
                if (_active < 0f) _active = 0f;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _active = 0f;
            _cooldown = 0f;
        }
    }

    public enum MeleePhase { Idle, Windup, Recover }

    /// <summary>
    /// Telegraphed melee attack: when the target is in range it winds up (visible telegraph), then strikes,
    /// then recovers. The caller re-checks range at the strike moment, so the attack is dodgeable.
    /// </summary>
    public sealed class MeleeAttackSim
    {
        public float Range { get; }
        public float WindupTime { get; }
        public float RecoverTime { get; }

        private float _timer;

        public MeleePhase Phase { get; private set; }

        public MeleeAttackSim(float range, float windupTime, float recoverTime)
        {
            Range = range;
            WindupTime = windupTime;
            RecoverTime = recoverTime;
        }

        /// <summary>0..1 progress through the windup (0 when not winding up).</summary>
        public float WindupProgress
        {
            get { return Phase == MeleePhase.Windup && WindupTime > 0f ? 1f - _timer / WindupTime : 0f; }
        }

        /// <summary>Advances time. Returns true on the step the strike lands (then recovery begins).</summary>
        public bool Tick(float dt, float distanceToTarget, bool targetValid)
        {
            if (float.IsNaN(dt) || dt <= 0f)
                return false;
            switch (Phase)
            {
                case MeleePhase.Idle:
                    if (targetValid && distanceToTarget <= Range)
                    {
                        Phase = MeleePhase.Windup;
                        _timer = WindupTime;
                    }
                    return false;
                case MeleePhase.Windup:
                    _timer -= dt;
                    if (_timer <= 0f)
                    {
                        Phase = MeleePhase.Recover;
                        _timer = RecoverTime;
                        return true;
                    }
                    return false;
                default:
                    _timer -= dt;
                    if (_timer <= 0f)
                        Phase = MeleePhase.Idle;
                    return false;
            }
        }

        public void Cancel()
        {
            Phase = MeleePhase.Idle;
            _timer = 0f;
        }
    }
}
