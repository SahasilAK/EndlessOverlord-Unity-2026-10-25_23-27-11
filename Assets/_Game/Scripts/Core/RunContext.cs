namespace FinalBoss.Core
{
    /// <summary>
    /// Ephemeral data for one run: the recorded seed, selected character, live HUD model,
    /// skill/reward inventory, equipment inventory, and merchant system.
    /// Never persisted as permanent progression; a new instance is created for every new run.
    /// </summary>
    public sealed class RunContext
    {
        public long Seed { get; }
        public int RunNumber { get; }
        public CharacterInfo Character { get; }
        public HudModel Hud { get; }
        public RewardInventory Rewards { get; }
        public PlayerInventory Inventory { get; }
        public MerchantSystem Merchant { get; }

        public RunContext(long seed, int runNumber, CharacterInfo character)
        {
            Seed = seed;
            RunNumber = runNumber;
            Character = character;
            Hud = new HudModel(character.BaseMaxHealth, character.ResourceName, character.BaseMaxResource, wave: 1);
            Rewards = new RewardInventory();
            Inventory = new PlayerInventory();
            Merchant = new MerchantSystem();
        }
    }
}
