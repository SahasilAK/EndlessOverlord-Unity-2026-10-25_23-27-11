using System;

namespace FinalBoss.Core
{
    public static class DamageCalculator
    {
        /// <summary>
        /// Armor reduction: damage * 100 / (100 + armor). Negative armor is treated as 0. Any positive raw
        /// damage of at least 1 deals at least 1. Invalid input (NaN/infinity/non-positive) deals 0.
        /// </summary>
        public static float Compute(float rawDamage, float armor)
        {
            if (float.IsNaN(rawDamage) || float.IsInfinity(rawDamage) || rawDamage <= 0f)
                return 0f;
            if (float.IsNaN(armor)) armor = 0f;
            if (armor < 0f) armor = 0f;
            float result = rawDamage * 100f / (100f + armor);
            return rawDamage >= 1f && result < 1f ? 1f : result;
        }
    }

    /// <summary>
    /// Hit points with a death event that fires exactly once per life. Damage on a dead actor is ignored,
    /// so a projectile and a melee hit landing in the same frame can never kill twice.
    /// </summary>
    public sealed class Health
    {
        public event Action Changed;
        public event Action Died;

        public float Max { get; private set; }
        public float Current { get; private set; }
        public bool IsDead { get; private set; }

        public Health(float max)
        {
            Reset(max);
        }

        /// <summary>Restores a full, living state (used by pooled actors on reuse). Existing listeners are kept.</summary>
        public void Reset(float max)
        {
            Max = max < 1f ? 1f : max;
            Current = Max;
            IsDead = false;
            Changed?.Invoke();
        }

        /// <summary>Applies damage and returns the amount actually removed (0 if dead/invalid).</summary>
        public float TakeDamage(float amount)
        {
            if (IsDead || float.IsNaN(amount) || amount <= 0f)
                return 0f;
            float applied = amount > Current ? Current : amount;
            Current -= applied;
            if (Current <= 0f)
            {
                Current = 0f;
                IsDead = true;
                Changed?.Invoke();
                Died?.Invoke();
            }
            else
            {
                Changed?.Invoke();
            }
            return applied;
        }

        /// <summary>Heals a living actor, never above Max. Returns the amount healed.</summary>
        public float Heal(float amount)
        {
            if (IsDead || float.IsNaN(amount) || amount <= 0f)
                return 0f;
            float healed = Current + amount > Max ? Max - Current : amount;
            Current += healed;
            Changed?.Invoke();
            return healed;
        }
    }
}
