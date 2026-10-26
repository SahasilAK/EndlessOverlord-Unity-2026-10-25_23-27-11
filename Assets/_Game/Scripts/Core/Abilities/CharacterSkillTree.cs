using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>
    /// Pure C# manager for skill tree unlocking, prerequisite validation, point allocation,
    /// respec/refunds, and mastery progression.
    /// Operates directly on <see cref="CharacterRecord"/>.
    /// </summary>
    public static class CharacterSkillTree
    {
        public static int CalculateEarnedSkillPoints(int level)
        {
            if (level <= 1) return 0;
            return 2 * (level - 1) + (level / 10);
        }

        public static int CalculateAvailableSkillPoints(int level, int spent)
        {
            int earned = CalculateEarnedSkillPoints(level);
            return Math.Max(0, earned - spent);
        }

        public static int GetAbilityRank(CharacterRecord record, string abilityId)
        {
            if (string.IsNullOrEmpty(abilityId) || record.UnlockedAbilities == null) return 0;
            for (int i = 0; i < record.UnlockedAbilities.Count; i++)
            {
                if (string.Equals(record.UnlockedAbilities[i].AbilityId, abilityId, StringComparison.OrdinalIgnoreCase))
                {
                    return record.UnlockedAbilities[i].Rank;
                }
            }
            return 0;
        }

        public static AbilityProgressRecord GetAbilityProgress(CharacterRecord record, string abilityId)
        {
            if (string.IsNullOrEmpty(abilityId) || record.UnlockedAbilities == null)
                return new AbilityProgressRecord(abilityId, 0, 0, 0);

            for (int i = 0; i < record.UnlockedAbilities.Count; i++)
            {
                if (string.Equals(record.UnlockedAbilities[i].AbilityId, abilityId, StringComparison.OrdinalIgnoreCase))
                {
                    return record.UnlockedAbilities[i];
                }
            }

            return new AbilityProgressRecord(abilityId, 0, 0, 0);
        }

        public static bool CanUnlock(string abilityId, CharacterRecord record, out string reason)
        {
            reason = string.Empty;

            if (string.IsNullOrEmpty(abilityId))
            {
                reason = "Invalid ability ID.";
                return false;
            }

            var def = AbilityRegistry.Get(abilityId);
            if (def == null)
            {
                reason = $"Ability '{abilityId}' not found in registry.";
                return false;
            }

            if (!string.Equals(def.CharacterId, record.CharacterId, StringComparison.OrdinalIgnoreCase))
            {
                reason = $"Ability '{abilityId}' belongs to character '{def.CharacterId}', not '{record.CharacterId}'.";
                return false;
            }

            int curRank = GetAbilityRank(record, abilityId);
            if (curRank >= def.MaxRank)
            {
                reason = $"Ability '{def.Name}' is already at maximum rank ({def.MaxRank}).";
                return false;
            }

            // Check level prerequisite (e.g. ultimates require level 50)
            if (record.Level < def.RequiredLevel)
            {
                reason = $"Requires character level {def.RequiredLevel}. Current level is {record.Level}.";
                return false;
            }

            // Check prerequisite ability
            if (!string.IsNullOrEmpty(def.PrerequisiteAbilityId))
            {
                int prereqRank = GetAbilityRank(record, def.PrerequisiteAbilityId);
                if (prereqRank <= 0)
                {
                    var prereqDef = AbilityRegistry.Get(def.PrerequisiteAbilityId);
                    string prereqName = prereqDef != null ? prereqDef.Name : def.PrerequisiteAbilityId;
                    reason = $"Prerequisite '{prereqName}' must be unlocked first.";
                    return false;
                }
            }

            // Check available skill points
            if (record.AvailableSkillPoints <= 0)
            {
                reason = "No available skill points.";
                return false;
            }

            return true;
        }

        public static bool TrySpendPoint(string abilityId, ref CharacterRecord record, out string error)
        {
            if (!CanUnlock(abilityId, record, out error))
            {
                return false;
            }

            if (record.UnlockedAbilities == null)
            {
                record.UnlockedAbilities = new List<AbilityProgressRecord>();
            }

            bool found = false;
            for (int i = 0; i < record.UnlockedAbilities.Count; i++)
            {
                var entry = record.UnlockedAbilities[i];
                if (string.Equals(entry.AbilityId, abilityId, StringComparison.OrdinalIgnoreCase))
                {
                    entry.Rank += 1;
                    record.UnlockedAbilities[i] = entry;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                record.UnlockedAbilities.Add(new AbilityProgressRecord(abilityId, rank: 1, masteryXp: 0, ascensionRank: 0));
            }

            record.SpentSkillPoints += 1;
            return true;
        }

        public static void Respec(ref CharacterRecord record)
        {
            record.SpentSkillPoints = 0;

            if (record.UnlockedAbilities != null)
            {
                for (int i = 0; i < record.UnlockedAbilities.Count; i++)
                {
                    var entry = record.UnlockedAbilities[i];
                    entry.Rank = 0; // Rank reset to 0, mastery XP preserved!
                    record.UnlockedAbilities[i] = entry;
                }
            }

            // Clear equipped loadout slots of now-locked abilities
            if (record.EquippedLoadout != null)
            {
                for (int i = 0; i < record.EquippedLoadout.Count; i++)
                {
                    string id = record.EquippedLoadout[i];
                    if (!string.IsNullOrEmpty(id) && GetAbilityRank(record, id) <= 0)
                    {
                        record.EquippedLoadout[i] = string.Empty;
                    }
                }
            }
        }

        public static bool AwardMasteryXp(string abilityId, ref CharacterRecord record, int xpAmount)
        {
            if (string.IsNullOrEmpty(abilityId) || xpAmount <= 0) return false;
            if (record.UnlockedAbilities == null) return false;

            for (int i = 0; i < record.UnlockedAbilities.Count; i++)
            {
                var entry = record.UnlockedAbilities[i];
                if (string.Equals(entry.AbilityId, abilityId, StringComparison.OrdinalIgnoreCase))
                {
                    entry.MasteryXp += xpAmount;
                    bool rankedUp = false;

                    var def = AbilityRegistry.Get(abilityId);
                    int maxRank = def != null ? def.MaxRank : AbilityProgressionMath.MaxStandardRank;

                    // Check if XP leveled up rank
                    while (entry.Rank < maxRank)
                    {
                        int req = AbilityProgressionMath.GetXpRequiredForNextRank(entry.Rank);
                        if (entry.MasteryXp >= req)
                        {
                            entry.MasteryXp -= req;
                            entry.Rank += 1;
                            rankedUp = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    // Post-20 ascension
                    if (entry.Rank >= maxRank && entry.AscensionRank < AbilityProgressionMath.MaxAscensionRank)
                    {
                        int ascReq = AbilityProgressionMath.GetXpRequiredForNextRank(entry.Rank + entry.AscensionRank);
                        if (entry.MasteryXp >= ascReq)
                        {
                            entry.MasteryXp -= ascReq;
                            entry.AscensionRank += 1;
                            rankedUp = true;
                        }
                    }

                    record.UnlockedAbilities[i] = entry;
                    return rankedUp;
                }
            }

            return false;
        }
    }
}
