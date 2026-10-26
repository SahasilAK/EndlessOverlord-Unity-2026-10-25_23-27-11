using System;

namespace FinalBoss.Core
{
    /// <summary>
    /// Live data the HUD binds to. Views subscribe to <see cref="Changed"/> and read these values;
    /// nothing in the UI hardcodes gameplay numbers. Simulation systems will write to this in later phases.
    /// </summary>
    public sealed class HudModel
    {
        public event Action Changed;

        public float Health { get; private set; }
        public float MaxHealth { get; private set; }
        public string ResourceName { get; private set; }
        public float Resource { get; private set; }
        public float MaxResource { get; private set; }
        public int Wave { get; private set; }
        public int Gold { get; private set; }
        public long Score { get; private set; }

        public HudModel(float maxHealth, string resourceName, float maxResource, int wave, float initialResource = 0f)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
            ResourceName = resourceName;
            MaxResource = maxResource;
            Resource = Clamp(initialResource, 0f, maxResource);
            Wave = wave;
        }

        public void AddResource(float amount)
        {
            SetResource(Resource + amount);
        }

        public bool TrySpendResource(float amount)
        {
            if (amount <= 0f) return true;
            if (Resource >= amount)
            {
                SetResource(Resource - amount);
                return true;
            }
            return false;
        }

        public void SetHealth(float value)
        {
            Health = Clamp(value, 0f, MaxHealth);
            Changed?.Invoke();
        }

        public void SetMaxHealth(float maxHealth)
        {
            MaxHealth = maxHealth < 1f ? 1f : maxHealth;
            if (Health > MaxHealth) Health = MaxHealth;
            Changed?.Invoke();
        }

        public void SetResource(float value)
        {
            Resource = Clamp(value, 0f, MaxResource);
            Changed?.Invoke();
        }

        public void SetWave(int wave)
        {
            Wave = wave < 1 ? 1 : wave;
            Changed?.Invoke();
        }

        public void AddGold(int amount)
        {
            long total = (long)Gold + amount;
            Gold = total < 0 ? 0 : (total > int.MaxValue ? int.MaxValue : (int)total);
            Changed?.Invoke();
        }

        public bool TrySpendGold(int amount)
        {
            if (amount <= 0 || Gold < amount)
                return false;
            Gold -= amount;
            Changed?.Invoke();
            return true;
        }

        public void AddScore(long amount)
        {
            long total;
            try { total = checked(Score + amount); }
            catch (OverflowException) { total = amount > 0 ? long.MaxValue : 0; }
            Score = total < 0 ? 0 : total;
            Changed?.Invoke();
        }

        private static float Clamp(float v, float min, float max)
        {
            return v < min ? min : (v > max ? max : v);
        }
    }
}
