using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Developer simulation and testing harness.
    /// Provides utilities to test deep level unlocks (levels 50, 100, 150, 200, 250),
    /// grant skill points, configure ability ranks and mastery XP, and simulate builds
    /// without manual grinding.
    /// Pure C# (no engine references).
    /// </summary>
    public static class DevHarness
    {
        public static void SetCharacterLevel(string characterId, int targetLevel, PlayerSaveProfile profile)
        {
            if (string.IsNullOrEmpty(characterId) || profile == null) return;
            var rec = profile.GetOrAddCharacter(characterId);
            rec.Level = Math.Max(1, targetLevel);
            rec.Experience = 0;
            profile.SaveCharacter(rec);
        }

        public static void GrantCharacterLevels(string characterId, int levelsToAdd, PlayerSaveProfile profile)
        {
            if (string.IsNullOrEmpty(characterId) || profile == null || levelsToAdd <= 0) return;
            var rec = profile.GetOrAddCharacter(characterId);
            rec.Level += levelsToAdd;
            profile.SaveCharacter(rec);
        }

        public static void SetAbilityRank(
            string characterId,
            string abilityId,
            int rank,
            int masteryXp = 0,
            int ascensionRank = 0,
            PlayerSaveProfile profile = null)
        {
            if (string.IsNullOrEmpty(characterId) || string.IsNullOrEmpty(abilityId) || profile == null) return;
            var rec = profile.GetOrAddCharacter(characterId);
            rec.EnsureInitialized();

            bool found = false;
            for (int i = 0; i < rec.UnlockedAbilities.Count; i++)
            {
                var entry = rec.UnlockedAbilities[i];
                if (string.Equals(entry.AbilityId, abilityId, StringComparison.OrdinalIgnoreCase))
                {
                    entry.Rank = rank;
                    entry.MasteryXp = masteryXp;
                    entry.AscensionRank = ascensionRank;
                    rec.UnlockedAbilities[i] = entry;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                rec.UnlockedAbilities.Add(new AbilityProgressRecord(abilityId, rank, masteryXp, ascensionRank));
            }

            profile.SaveCharacter(rec);
        }

        public static void UnlockBranch(string characterId, string branchName, PlayerSaveProfile profile)
        {
            if (string.IsNullOrEmpty(characterId) || profile == null) return;
            var rec = profile.GetOrAddCharacter(characterId);
            rec.EnsureInitialized();

            var abilities = AbilityRegistry.GetForCharacter(characterId);
            for (int i = 0; i < abilities.Count; i++)
            {
                var def = abilities[i];
                if (string.Equals(def.BranchName, branchName, StringComparison.OrdinalIgnoreCase))
                {
                    bool found = false;
                    for (int u = 0; u < rec.UnlockedAbilities.Count; u++)
                    {
                        if (string.Equals(rec.UnlockedAbilities[u].AbilityId, def.Id, StringComparison.OrdinalIgnoreCase))
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        rec.UnlockedAbilities.Add(new AbilityProgressRecord(def.Id, rank: 1));
                    }
                }
            }

            profile.SaveCharacter(rec);
        }

        public static void EquipLoadout(
            string characterId,
            IList<string> abilityIds,
            PlayerSaveProfile profile,
            AbilityLoadout activeRunLoadout = null)
        {
            if (string.IsNullOrEmpty(characterId) || profile == null || abilityIds == null) return;
            var rec = profile.GetOrAddCharacter(characterId);
            rec.EnsureInitialized();

            for (int i = 0; i < Math.Min(5, abilityIds.Count); i++)
            {
                string id = abilityIds[i] ?? string.Empty;
                rec.EquippedLoadout[i] = id;
                if (activeRunLoadout != null)
                {
                    activeRunLoadout.TryEquip(i, id, _ => true, out _);
                }
            }

            profile.SaveCharacter(rec);
        }
    }
}
