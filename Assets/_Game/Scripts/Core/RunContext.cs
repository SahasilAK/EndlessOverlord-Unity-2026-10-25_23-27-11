using System;

namespace FinalBoss.Core
{
    /// <summary>
    /// Ephemeral data for one run: the recorded seed, selected character, live HUD model,
    /// skill/reward inventory, equipment inventory, merchant system, and run metrics.
    /// Never persisted directly; at the end of the run a <see cref="RunSummary"/> is produced.
    /// </summary>
    public sealed class RunContext
    {
        public string RunId { get; }
        public long Seed { get; }
        public int RunNumber { get; }
        public CharacterInfo Character { get; }
        public HudModel Hud { get; }
        public RewardInventory Rewards { get; }
        public PlayerInventory Inventory { get; }
        public MerchantSystem Merchant { get; }
        public PersistentStats MetaStats { get; }
        public ArenaLayout Layout { get; set; }

        public float ElapsedTime { get; set; }
        public int TotalKills { get; set; }
        public int BossesSlain { get; set; }
        public int HighestWave { get; set; }

        public RunContext(
            long seed,
            int runNumber,
            CharacterInfo character,
            PersistentStats metaStats = default(PersistentStats),
            string runId = null)
        {
            RunId = !string.IsNullOrEmpty(runId) ? runId : $"run_{runNumber}_{Guid.NewGuid():N}";
            Seed = seed;
            RunNumber = runNumber;
            Character = character;
            MetaStats = metaStats;

            float startingMaxHp = character.BaseMaxHealth + metaStats.BonusMaxHealth;
            Hud = new HudModel(startingMaxHp, character.ResourceName, character.BaseMaxResource, wave: 1);
            Rewards = new RewardInventory();
            Inventory = new PlayerInventory();
            Merchant = new MerchantSystem();
            Layout = ArenaGenerator.Generate(seed);
            HighestWave = 1;
        }

        public RunSummary BuildSummary()
        {
            return new RunSummary
            {
                RunId = RunId,
                CharacterId = Character != null ? Character.Id : "unknown",
                WaveReached = Math.Max(1, HighestWave),
                Duration = ElapsedTime,
                Score = Hud != null ? Hud.Score : 0,
                Kills = TotalKills,
                BossKills = BossesSlain,
                GoldCollected = Hud != null ? Hud.Gold : 0
            };
        }
    }
}
