using System;

namespace FinalBoss.Core
{
    /// <summary>
    /// Small deterministic PRNG (SplitMix64). Pure C#, no Unity dependency, so the same seed always
    /// reproduces the same sequence in edit-mode tests and at runtime. Use one instance per logical
    /// stream (world, waves, rewards, loot...) so streams never perturb each other.
    /// </summary>
    public sealed class SplitMix64Rng
    {
        private ulong _state;

        public SplitMix64Rng(long seed)
        {
            _state = unchecked((ulong)seed);
        }

        /// <summary>Stateless SplitMix64 finalizer. It is a bijection on 64-bit values.</summary>
        public static ulong Mix(ulong z)
        {
            unchecked
            {
                z = (z ^ (z >> 30)) * 0xBF58476D1CE4E5B9UL;
                z = (z ^ (z >> 27)) * 0x94D049BB133111EBUL;
                return z ^ (z >> 31);
            }
        }

        public ulong NextUInt64()
        {
            unchecked
            {
                _state += 0x9E3779B97F4A7C15UL;
                return Mix(_state);
            }
        }

        /// <summary>Uniform integer in [minInclusive, maxExclusive).</summary>
        public int NextInt(int minInclusive, int maxExclusive)
        {
            if (maxExclusive <= minInclusive)
                throw new ArgumentException("maxExclusive must be greater than minInclusive.");
            ulong range = (ulong)((long)maxExclusive - minInclusive);
            return (int)((long)minInclusive + (long)(NextUInt64() % range));
        }

        /// <summary>Uniform double in [0, 1).</summary>
        public double NextDouble()
        {
            return (NextUInt64() >> 11) * (1.0 / 9007199254740992.0);
        }
    }
}
