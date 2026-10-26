using System;
using System.IO;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;

namespace FinalBoss.Tests
{
    public class SaveAndProgressionTests
    {
        private string _tempSaveDir;

        [SetUp]
        public void SetUp()
        {
            _tempSaveDir = Path.Combine(Application.temporaryCachePath, "TestSaves_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_tempSaveDir);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(_tempSaveDir))
            {
                Directory.Delete(_tempSaveDir, true);
            }
        }

        [Test]
        public void Profile_SkillPointFormulaMatchesSpec()
        {
            // earned(level) = 2*(level-1) + floor(level/10)
            var c1 = new CharacterRecord { CharacterId = "lich_lord", Level = 1, SpentSkillPoints = 0 };
            Assert.AreEqual(0, c1.EarnedSkillPoints);
            Assert.AreEqual(0, c1.AvailableSkillPoints);

            var c5 = new CharacterRecord { CharacterId = "lich_lord", Level = 5, SpentSkillPoints = 2 };
            Assert.AreEqual(8, c5.EarnedSkillPoints); // 2*4 + 0
            Assert.AreEqual(6, c5.AvailableSkillPoints); // 8 - 2

            // Tenth-level milestone bonus point
            var c10 = new CharacterRecord { CharacterId = "lich_lord", Level = 10, SpentSkillPoints = 5 };
            Assert.AreEqual(19, c10.EarnedSkillPoints); // 2*9 + 1
            Assert.AreEqual(14, c10.AvailableSkillPoints);

            var c20 = new CharacterRecord { CharacterId = "lich_lord", Level = 20, SpentSkillPoints = 10 };
            Assert.AreEqual(40, c20.EarnedSkillPoints); // 2*19 + 2
            Assert.AreEqual(30, c20.AvailableSkillPoints);
        }

        [Test]
        public void Profile_TryPurchaseUpgrade_DeductsWalletAndIncrementsRank()
        {
            var profile = new PlayerSaveProfile();
            profile.MetaWallet = 100;

            Assert.AreEqual(0, profile.GetUpgradeRank(PersistentUpgrades.SoulVitalityId));

            // Buy Rank 1 (cost 30)
            Assert.IsTrue(profile.TryPurchaseUpgrade(PersistentUpgrades.SoulVitalityId, 30));
            Assert.AreEqual(70, profile.MetaWallet);
            Assert.AreEqual(1, profile.GetUpgradeRank(PersistentUpgrades.SoulVitalityId));

            // Try buy when funds insufficient (cost 100, wallet 70)
            Assert.IsFalse(profile.TryPurchaseUpgrade(PersistentUpgrades.SoulVitalityId, 100));
            Assert.AreEqual(70, profile.MetaWallet);
            Assert.AreEqual(1, profile.GetUpgradeRank(PersistentUpgrades.SoulVitalityId));
        }

        [Test]
        public void SaveStorage_SaveAndLoad_RoundTripExactData()
        {
            string savePath = Path.Combine(_tempSaveDir, "profile_test.json");

            var profile = new PlayerSaveProfile();
            profile.MetaWallet = 250;
            profile.GlobalHighestWave = 12;
            profile.GlobalHighScore = 45000;
            profile.SetUpgradeRank(PersistentUpgrades.SoulVitalityId, 2);
            profile.SetUpgradeRank(PersistentUpgrades.DeathPotencyId, 1);

            var charRec = profile.GetOrAddCharacter("lich_lord");
            charRec.Level = 4;
            charRec.Experience = 60;
            charRec.HighestWave = 12;
            profile.SaveCharacter(charRec);

            Assert.IsTrue(SaveStorage.Save(profile, savePath));
            Assert.IsTrue(File.Exists(savePath));

            var loaded = SaveStorage.Load(savePath);
            Assert.IsNotNull(loaded);
            Assert.AreEqual(250, loaded.MetaWallet);
            Assert.AreEqual(12, loaded.GlobalHighestWave);
            Assert.AreEqual(45000, loaded.GlobalHighScore);
            Assert.AreEqual(2, loaded.GetUpgradeRank(PersistentUpgrades.SoulVitalityId));
            Assert.AreEqual(1, loaded.GetUpgradeRank(PersistentUpgrades.DeathPotencyId));

            var loadedChar = loaded.GetOrAddCharacter("lich_lord");
            Assert.AreEqual(4, loadedChar.Level);
            Assert.AreEqual(60, loadedChar.Experience);
            Assert.AreEqual(12, loadedChar.HighestWave);
        }

        [Test]
        public void SaveStorage_AtomicWrite_LeavesBackupFile()
        {
            string savePath = Path.Combine(_tempSaveDir, "profile_backup_test.json");
            string backupPath = savePath + ".bak";

            var v1 = new PlayerSaveProfile { MetaWallet = 10 };
            SaveStorage.Save(v1, savePath);
            Assert.IsTrue(File.Exists(savePath));

            var v2 = new PlayerSaveProfile { MetaWallet = 50 };
            SaveStorage.Save(v2, savePath);

            Assert.IsTrue(File.Exists(backupPath), "Backup file .bak should exist after second save");
            var backupLoaded = SaveStorage.Load(backupPath);
            Assert.AreEqual(10, backupLoaded.MetaWallet);

            var currentLoaded = SaveStorage.Load(savePath);
            Assert.AreEqual(50, currentLoaded.MetaWallet);
        }

        [Test]
        public void SaveStorage_CorruptedFile_RecoversFromBackup()
        {
            string savePath = Path.Combine(_tempSaveDir, "profile_corrupt_test.json");
            string backupPath = savePath + ".bak";

            // Save v1 then v2 so backup exists
            var v1 = new PlayerSaveProfile { MetaWallet = 88 };
            SaveStorage.Save(v1, savePath);
            var v2 = new PlayerSaveProfile { MetaWallet = 99 };
            SaveStorage.Save(v2, savePath);

            // Corrupt primary file with broken JSON
            File.WriteAllText(savePath, "{ broken json content !@#$");

            // Load should fall back to backup containing v1
            var recovered = SaveStorage.Load(savePath);
            Assert.IsNotNull(recovered);
            Assert.AreEqual(88, recovered.MetaWallet);
        }

        [Test]
        public void SaveStorage_CorruptedFileAndNoBackup_FallsBackToFreshProfile()
        {
            string savePath = Path.Combine(_tempSaveDir, "profile_corrupt_nobak.json");
            File.WriteAllText(savePath, "{ entirely corrupt without backup");

            var fallback = SaveStorage.Load(savePath);
            Assert.IsNotNull(fallback);
            Assert.AreEqual(0, fallback.MetaWallet);
            Assert.AreEqual(1, fallback.Version);
        }

        [Test]
        public void CommitRunResult_Idempotent_RejectsDuplicateCredit()
        {
            string savePath = Path.Combine(_tempSaveDir, "profile_idempotent.json");
            var profile = new PlayerSaveProfile();
            var summary = new RunSummary
            {
                RunId = "run_guid_unique_123",
                CharacterId = "lich_lord",
                WaveReached = 5,
                Duration = 120f,
                Score = 5000,
                Kills = 25,
                BossKills = 1,
                GoldCollected = 40
            };

            int expectedSouls = summary.CalculateMetaSouls();

            // First commit succeeds
            Assert.IsTrue(SaveStorage.CommitRunResult(summary, profile, savePath));
            Assert.AreEqual(expectedSouls, profile.MetaWallet);
            Assert.IsTrue(profile.HasClaimedRun(summary.RunId));

            // Second commit with same RunId must be rejected
            Assert.IsFalse(SaveStorage.CommitRunResult(summary, profile, savePath));
            Assert.AreEqual(expectedSouls, profile.MetaWallet, "Meta wallet should not double credit");
        }

        [Test]
        public void CharacterProgress_ClassIsolation()
        {
            var profile = new PlayerSaveProfile();

            var lich = profile.GetOrAddCharacter("lich_lord");
            lich.Level = 5;
            lich.HighestWave = 10;
            profile.SaveCharacter(lich);

            var demon = profile.GetOrAddCharacter("demon_monarch");
            Assert.AreEqual(1, demon.Level, "Demon Monarch progress must be isolated from Lich Lord");
            Assert.AreEqual(0, demon.HighestWave);
        }

        [Test]
        public void PersistentStartingStats_AppliedToFreshRun()
        {
            var profile = new PlayerSaveProfile();
            profile.SetUpgradeRank(PersistentUpgrades.SoulVitalityId, 2); // +30 HP
            profile.SetUpgradeRank(PersistentUpgrades.DeathPotencyId, 1); // +8% Dmg
            profile.SetUpgradeRank(PersistentUpgrades.SpectralArmorId, 1); // +3 Armor
            profile.SetUpgradeRank(PersistentUpgrades.SwiftStrideId, 1); // +6% Spd

            var stats = profile.GetPersistentStartingStats();
            Assert.AreEqual(30f, stats.BonusMaxHealth);
            Assert.AreEqual(0.08f, stats.BonusDamagePercent, 1e-4f);
            Assert.AreEqual(3f, stats.BonusArmor);
            Assert.AreEqual(0.06f, stats.BonusMoveSpeedPercent, 1e-4f);

            var session = new GameSessionState(42L);
            session.Profile = profile;
            session.TrySelectCharacter(CharacterCatalog.LichLordId);
            Assert.IsTrue(session.TryStartRun());

            Assert.IsNotNull(session.Run);
            Assert.AreEqual(stats.BonusMaxHealth, session.Run.MetaStats.BonusMaxHealth);
            Assert.AreEqual(stats.BonusDamagePercent, session.Run.MetaStats.BonusDamagePercent);
            Assert.AreEqual(stats.BonusArmor, session.Run.MetaStats.BonusArmor);
            Assert.AreEqual(stats.BonusMoveSpeedPercent, session.Run.MetaStats.BonusMoveSpeedPercent);
        }
    }
}
