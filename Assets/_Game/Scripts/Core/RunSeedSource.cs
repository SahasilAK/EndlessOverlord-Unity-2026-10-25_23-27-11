namespace FinalBoss.Core
{
    /// <summary>
    /// Produces a unique, recorded seed for every new run. Seed n is Mix(entropy + n * golden-ratio step).
    /// Because Mix is a bijection, two different run counters can never yield the same seed for the same
    /// entropy, and a fixed entropy value gives a fully reproducible seed sequence for tests.
    /// </summary>
    public sealed class RunSeedSource
    {
        private const ulong Step = 0x9E3779B97F4A7C15UL;
        private readonly ulong _entropy;
        private ulong _counter;

        public RunSeedSource(long entropy)
        {
            _entropy = unchecked((ulong)entropy);
        }

        public long NextSeed()
        {
            unchecked
            {
                _counter++;
                return (long)SplitMix64Rng.Mix(_entropy + _counter * Step);
            }
        }
    }
}
