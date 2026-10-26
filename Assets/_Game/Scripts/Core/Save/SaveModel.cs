using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    [Serializable]
    public struct UpgradeRecord
    {
        public string Id;
        public int Rank;

        public UpgradeRecord(string id, int rank)
        {
            Id = id;
            Rank = rank;
        }
    }

    [Serializable]
    public struct AbilityProgressRecord
    {
        public string AbilityId;
        public int Rank;
        public int MasteryXp;
        public int AscensionRank;

        public AbilityProgressRecord(string abilityId, int rank, int masteryXp = 0, int ascensionRank = 0)
        {
            AbilityId = abilityId;
            Rank = rank;
            MasteryXp = masteryXp;
            AscensionRank = ascensionRank;
        }
    }

    [Serializable]
    public struct CharacterRecord
    {
        public string CharacterId;
        public int Level;
        public int Experience;
        public int SpentSkillPoints;
        public int HighestWave;
        public long HighScore;
        public float LongestSurvival;
        public int TotalRuns;
        public int TotalKills;
        public int TotalBossKills;
        public List<AbilityProgressRecord> UnlockedAbilities;
        public List<string> EquippedLoadout;
        public List<bool> AutocastSettings;

        public int EarnedSkillPoints => 2 * Math.Max(0, Level - 1) + (Level / 10);
        public int AvailableSkillPoints => Math.Max(0, EarnedSkillPoints - SpentSkillPoints);

        public void EnsureInitialized()
        {
            if (UnlockedAbilities == null)
                UnlockedAbilities = new List<AbilityProgressRecord>();
            if (EquippedLoadout == null)
            {
                EquippedLoadout = new List<string> { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
            }
            while (EquippedLoadout.Count < 5)
            {
                EquippedLoadout.Add(string.Empty);
            }
            if (AutocastSettings == null)
            {
                AutocastSettings = new List<bool> { false, false, false, false, false };
            }
            while (AutocastSettings.Count < 5)
            {
                AutocastSettings.Add(false);
            }

            var charInfo = CharacterCatalog.Get(CharacterId);
            if (charInfo != null && !string.IsNullOrEmpty(charInfo.SignatureAbilityId))
            {
                bool hasAbility = false;
                for (int i = 0; i < UnlockedAbilities.Count; i++)
                {
                    if (UnlockedAbilities[i].AbilityId == charInfo.SignatureAbilityId)
                    {
                        hasAbility = true;
                        break;
                    }
                }
                if (!hasAbility)
                {
                    UnlockedAbilities.Add(new AbilityProgressRecord(charInfo.SignatureAbilityId, rank: 1));
                }

                if (string.IsNullOrEmpty(EquippedLoadout[0]))
                {
                    EquippedLoadout[0] = charInfo.SignatureAbilityId;
                }
            }
        }
    }

    [Serializable]
    public struct PersistentStats
    {
        public float BonusMaxHealth;
        public float BonusDamagePercent;
        public float BonusArmor;
        public float BonusMoveSpeedPercent;
    }

    /// <summary>
    /// Versioned persistent profile stored locally under persistentDataPath.
    /// Tracks meta currency, persistent stat upgrades, per-class progress, highscores,
    /// and claimed run GUIDs for payout idempotency.
    /// </summary>
    [Serializable]
    public sealed class PlayerSaveProfile
    {
        public const int CurrentSchemaVersion = 1;

        public int Version = CurrentSchemaVersion;
        public int MetaWallet;
        public List<UpgradeRecord> Upgrades = new List<UpgradeRecord>();
        public List<CharacterRecord> Characters = new List<CharacterRecord>();
        public List<string> ClaimedRunGuids = new List<string>();

        public int GlobalHighestWave;
        public long GlobalHighScore;
        public float GlobalLongestSurvival;

        public int GetUpgradeRank(string id)
        {
            if (string.IsNullOrEmpty(id)) return 0;
            for (int i = 0; i < Upgrades.Count; i++)
            {
                if (Upgrades[i].Id == id) return Upgrades[i].Rank;
            }
            return 0;
        }

        public void SetUpgradeRank(string id, int rank)
        {
            if (string.IsNullOrEmpty(id)) return;
            for (int i = 0; i < Upgrades.Count; i++)
            {
                if (Upgrades[i].Id == id)
                {
                    var u = Upgrades[i];
                    u.Rank = rank;
                    Upgrades[i] = u;
                    return;
                }
            }
            Upgrades.Add(new UpgradeRecord(id, rank));
        }

        public bool TryPurchaseUpgrade(string id, int cost)
        {
            if (cost <= 0 || MetaWallet < cost)
                return false;

            MetaWallet -= cost;
            int curRank = GetUpgradeRank(id);
            SetUpgradeRank(id, curRank + 1);
            return true;
        }

        public CharacterRecord GetOrAddCharacter(string characterId)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].CharacterId == characterId)
                {
                    var rec = Characters[i];
                    rec.EnsureInitialized();
                    Characters[i] = rec;
                    return rec;
                }
            }

            var fresh = new CharacterRecord
            {
                CharacterId = characterId,
                Level = 1,
                Experience = 0,
                SpentSkillPoints = 0
            };
            fresh.EnsureInitialized();
            Characters.Add(fresh);
            return fresh;
        }

        public void SaveCharacter(CharacterRecord record)
        {
            for (int i = 0; i < Characters.Count; i++)
            {
                if (Characters[i].CharacterId == record.CharacterId)
                {
                    Characters[i] = record;
                    return;
                }
            }
            Characters.Add(record);
        }

        public bool HasClaimedRun(string runGuid)
        {
            if (string.IsNullOrEmpty(runGuid)) return false;
            return ClaimedRunGuids.Contains(runGuid);
        }

        public void RecordClaimedRun(string runGuid)
        {
            if (!string.IsNullOrEmpty(runGuid) && !ClaimedRunGuids.Contains(runGuid))
            {
                ClaimedRunGuids.Add(runGuid);
            }
        }

        public PersistentStats GetPersistentStartingStats()
        {
            var stats = default(PersistentStats);
            int hpRank = GetUpgradeRank(PersistentUpgrades.SoulVitalityId);
            int dmgRank = GetUpgradeRank(PersistentUpgrades.DeathPotencyId);
            int armorRank = GetUpgradeRank(PersistentUpgrades.SpectralArmorId);
            int speedRank = GetUpgradeRank(PersistentUpgrades.SwiftStrideId);

            stats.BonusMaxHealth = hpRank * 15f;
            stats.BonusDamagePercent = dmgRank * 0.08f;
            stats.BonusArmor = armorRank * 3f;
            stats.BonusMoveSpeedPercent = speedRank * 0.06f;

            return stats;
        }
    }

    public static class PersistentUpgrades
    {
        public const string SoulVitalityId = "soul_vitality";
        public const string DeathPotencyId = "death_potency";
        public const string SpectralArmorId = "spectral_armor";
        public const string SwiftStrideId = "swift_stride";

        public static int GetUpgradeCost(string id, int currentRank)
        {
            switch (id)
            {
                case SoulVitalityId: return 30 + currentRank * 20;
                case DeathPotencyId: return 40 + currentRank * 25;
                case SpectralArmorId: return 35 + currentRank * 25;
                case SwiftStrideId: return 25 + currentRank * 15;
                default: return 50;
            }
        }

        public static string GetUpgradeName(string id)
        {
            switch (id)
            {
                case SoulVitalityId: return "Soul Vitality";
                case DeathPotencyId: return "Death Potency";
                case SpectralArmorId: return "Spectral Armor";
                case SwiftStrideId: return "Swift Stride";
                default: return id;
            }
        }

        public static string GetUpgradeEffect(string id)
        {
            switch (id)
            {
                case SoulVitalityId: return "+15 Starting Max HP / rank";
                case DeathPotencyId: return "+8% Starting Damage / rank";
                case SpectralArmorId: return "+3 Starting Armor / rank";
                case SwiftStrideId: return "+6% Starting Move Speed / rank";
                default: return "";
            }
        }
    }

    public sealed class RunSummary
    {
        public string RunId { get; set; }
        public string CharacterId { get; set; }
        public int WaveReached { get; set; }
        public float Duration { get; set; }
        public long Score { get; set; }
        public int Kills { get; set; }
        public int BossKills { get; set; }
        public int GoldCollected { get; set; }

        public int CalculateMetaSouls()
        {
            return (int)(Score / 10) + GoldCollected + WaveReached * 10 + BossKills * 50;
        }

        public int CalculateCharacterXp()
        {
            return (int)(Score / 5) + WaveReached * 20 + BossKills * 100;
        }
    }
}
