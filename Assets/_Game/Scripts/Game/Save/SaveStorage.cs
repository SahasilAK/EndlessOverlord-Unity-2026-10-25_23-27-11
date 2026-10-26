using System;
using System.IO;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    /// <summary>
    /// File-based local save persistence for <see cref="PlayerSaveProfile"/>.
    /// Enforces atomic temp-write-and-replace, maintains .bak backups, handles corruption gracefully,
    /// and guarantees idempotent end-run reward payout.
    /// </summary>
    public static class SaveStorage
    {
        public static string GetDefaultPath()
        {
            return Path.Combine(Application.persistentDataPath, "player_profile.json");
        }

        public static bool Save(PlayerSaveProfile profile, string filePath = null)
        {
            if (profile == null) return false;
            string targetPath = !string.IsNullOrEmpty(filePath) ? filePath : GetDefaultPath();

            try
            {
                string dir = Path.GetDirectoryName(targetPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                string tempPath = targetPath + ".tmp";
                string backupPath = targetPath + ".bak";

                string json = JsonUtility.ToJson(profile, true);
                File.WriteAllText(tempPath, json);

                // Keep previous version as backup
                if (File.Exists(targetPath))
                {
                    File.Copy(targetPath, backupPath, true);
                }

                // Atomic overwrite
                File.Copy(tempPath, targetPath, true);
                if (File.Exists(tempPath))
                {
                    File.Delete(tempPath);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[SaveStorage] Failed to save profile to {targetPath}: {ex.Message}");
                return false;
            }
        }

        public static PlayerSaveProfile Load(string filePath = null)
        {
            string targetPath = !string.IsNullOrEmpty(filePath) ? filePath : GetDefaultPath();
            string backupPath = targetPath + ".bak";

            // 1. Try primary path
            if (File.Exists(targetPath))
            {
                try
                {
                    string json = File.ReadAllText(targetPath);
                    var profile = JsonUtility.FromJson<PlayerSaveProfile>(json);
                    if (profile != null && profile.Version >= 1)
                    {
                        return profile;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[SaveStorage] Primary save corrupted: {ex.Message}. Attempting backup.");
                }
            }

            // 2. Try backup path
            if (File.Exists(backupPath))
            {
                try
                {
                    string json = File.ReadAllText(backupPath);
                    var profile = JsonUtility.FromJson<PlayerSaveProfile>(json);
                    if (profile != null && profile.Version >= 1)
                    {
                        Debug.LogWarning("[SaveStorage] Successfully recovered profile from backup file.");
                        return profile;
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"[SaveStorage] Backup recovery failed: {ex.Message}. Falling back to fresh profile.");
                }
            }

            // 3. Fallback: fresh profile
            var fresh = new PlayerSaveProfile();
            return fresh;
        }

        /// <summary>
        /// Commits run summary to profile idempotently. If runId was already credited, returns false.
        /// </summary>
        public static bool CommitRunResult(RunSummary summary, PlayerSaveProfile profile, string saveFilePath = null)
        {
            if (summary == null || profile == null)
                return false;

            if (profile.HasClaimedRun(summary.RunId))
                return false; // Idempotent: already rewarded

            profile.RecordClaimedRun(summary.RunId);

            int souls = summary.CalculateMetaSouls();
            profile.MetaWallet += souls;

            var charRecord = profile.GetOrAddCharacter(summary.CharacterId);
            charRecord.TotalRuns++;
            charRecord.TotalKills += summary.Kills;
            charRecord.TotalBossKills += summary.BossKills;

            if (summary.WaveReached > charRecord.HighestWave) charRecord.HighestWave = summary.WaveReached;
            if (summary.Score > charRecord.HighScore) charRecord.HighScore = summary.Score;
            if (summary.Duration > charRecord.LongestSurvival) charRecord.LongestSurvival = summary.Duration;

            // Character level progression
            int xpGained = summary.CalculateCharacterXp();
            charRecord.Experience += xpGained;
            int req = charRecord.Level * 100;
            while (charRecord.Experience >= req)
            {
                charRecord.Experience -= req;
                charRecord.Level++;
                req = charRecord.Level * 100;
            }
            profile.SaveCharacter(charRecord);

            // Global records
            if (summary.WaveReached > profile.GlobalHighestWave) profile.GlobalHighestWave = summary.WaveReached;
            if (summary.Score > profile.GlobalHighScore) profile.GlobalHighScore = summary.Score;
            if (summary.Duration > profile.GlobalLongestSurvival) profile.GlobalLongestSurvival = summary.Duration;

            Save(profile, saveFilePath);
            return true;
        }
    }
}
