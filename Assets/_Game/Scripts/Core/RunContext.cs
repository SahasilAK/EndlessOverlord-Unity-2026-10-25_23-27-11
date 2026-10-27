using System;
using System.Collections.Generic;

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
        public AbilityLoadout Loadout { get; set; }
        public MagicSchoolProgression Magic { get; }
        public DungeonMutationType ActiveMutation { get; set; } = DungeonMutationType.None;
        public InRunEventType LastWaveEvent { get; set; } = InRunEventType.None;
        public List<ShrineInstance> ActiveShrines { get; } = new List<ShrineInstance>();

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
            Hud = new HudModel(startingMaxHp, character.ResourceName, character.BaseMaxResource, wave: 1, initialResource: character.StartingResource);
            Rewards = new RewardInventory();
            Inventory = new PlayerInventory();
            Merchant = new MerchantSystem();
            Layout = ArenaGenerator.Generate(seed);
            Loadout = new AbilityLoadout();
            Magic = new MagicSchoolProgression();
            if (character != null && !string.IsNullOrEmpty(character.SignatureAbilityId))
            {
                Loadout.TryEquip(0, character.SignatureAbilityId, null, out _);
            }
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
