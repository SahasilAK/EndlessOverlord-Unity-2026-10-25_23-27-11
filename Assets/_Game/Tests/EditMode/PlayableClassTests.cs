using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    [TestFixture]
    public sealed class PlayableClassTests
    {
        [Test]
        public void AllFiveClasses_DefinedWithDistinctWeaponsResourcesAndStats()
        {
            var characters = CharacterCatalog.Characters;
            Assert.AreEqual(5, characters.Count);

            var expectedIds = new HashSet<string>
            {
                CharacterCatalog.LichLordId,
                CharacterCatalog.DemonMonarchId,
                CharacterCatalog.VampireQueenId,
                CharacterCatalog.ShadowReaperId,
                CharacterCatalog.CursedGolemId
            };

            var weapons = new HashSet<string>();
            var resources = new HashSet<string>();
            var basicAttacks = new HashSet<string>();
            var signatures = new HashSet<string>();

            for (int i = 0; i < characters.Count; i++)
            {
                var c = characters[i];
                Assert.IsTrue(c.Playable, $"{c.DisplayName} should be marked playable.");
                Assert.IsTrue(expectedIds.Contains(c.Id), $"Unexpected character id: {c.Id}");
                Assert.Greater(c.BaseMaxHealth, 0f);
                Assert.Greater(c.BaseMaxResource, 0f);
                Assert.Greater(c.BaseMoveSpeed, 0f);
                Assert.GreaterOrEqual(c.BaseArmor, 0f);
                Assert.Greater(c.SignatureResourceCost, 0f);
                Assert.Greater(c.SignatureCooldown, 0f);

                weapons.Add(c.StartingWeapon);
                resources.Add(c.ResourceName);
                basicAttacks.Add(c.BasicAttackName);
                signatures.Add(c.SignatureAbilityId);
            }

            Assert.AreEqual(5, weapons.Count, "All 5 classes must have unique starting weapons.");
            Assert.AreEqual(5, resources.Count, "All 5 classes must have unique class resources.");
            Assert.AreEqual(5, basicAttacks.Count, "All 5 classes must have unique basic attacks.");
            Assert.AreEqual(5, signatures.Count, "All 5 classes must have unique signature abilities.");
        }

        [Test]
        public void HudModel_ResourceAddAndSpend_EnforcesBoundsAndAtomicSpending()
        {
            var hud = new HudModel(100f, "Soul Essence", 100f, 1, initialResource: 50f);
            Assert.AreEqual(50f, hud.Resource);

            hud.AddResource(30f);
            Assert.AreEqual(80f, hud.Resource);

            // Spend success
            bool spent = hud.TrySpendResource(25f);
            Assert.IsTrue(spent);
            Assert.AreEqual(55f, hud.Resource);

            // Spend failure when insufficient
            bool overspend = hud.TrySpendResource(90f);
            Assert.IsFalse(overspend);
            Assert.AreEqual(55f, hud.Resource, "Failed spend must not deduct resources.");

            // Clamp on overflow
            hud.AddResource(500f);
            Assert.AreEqual(100f, hud.Resource);

            // Clamp on underflow
            hud.SetResource(-50f);
            Assert.AreEqual(0f, hud.Resource);
        }

        [Test]
        public void GameSessionState_CanSelectAndStartRunWithAllFiveClasses()
        {
            var characterIds = new[]
            {
                CharacterCatalog.LichLordId,
                CharacterCatalog.DemonMonarchId,
                CharacterCatalog.VampireQueenId,
                CharacterCatalog.ShadowReaperId,
                CharacterCatalog.CursedGolemId
            };

            for (int i = 0; i < characterIds.Length; i++)
            {
                string id = characterIds[i];
                var session = new GameSessionState(100 + i);
                session.OpenCharacterSelect();

                bool selected = session.TrySelectCharacter(id);
                Assert.IsTrue(selected, $"Failed to select {id}");

                bool started = session.TryStartRun();
                Assert.IsTrue(started, $"Failed to start run with {id}");

                Assert.AreEqual(id, session.Run.Character.Id);
                Assert.AreEqual(session.Run.Character.ResourceName, session.Run.Hud.ResourceName);
                Assert.AreEqual(session.Run.Character.BaseMaxResource, session.Run.Hud.MaxResource);
            }
        }

        [Test]
        public void CharacterRecord_ProgressIsolation_EnsuresNoCrossClassLeakage()
        {
            var profile = new PlayerSaveProfile();

            var lichRecord = profile.GetOrAddCharacter(CharacterCatalog.LichLordId);
            lichRecord.Level = 5;
            lichRecord.Experience = 1500;
            profile.SaveCharacter(lichRecord);

            Assert.Greater(lichRecord.Level, 1);
            Assert.Greater(lichRecord.EarnedSkillPoints, 0);

            // Verify all other 4 classes remain untouched at level 1 with 0 points
            var otherIds = new[]
            {
                CharacterCatalog.DemonMonarchId,
                CharacterCatalog.VampireQueenId,
                CharacterCatalog.ShadowReaperId,
                CharacterCatalog.CursedGolemId
            };

            for (int i = 0; i < otherIds.Length; i++)
            {
                var record = profile.GetOrAddCharacter(otherIds[i]);
                Assert.AreEqual(1, record.Level, $"Class {otherIds[i]} level should remain 1");
                Assert.AreEqual(0, record.EarnedSkillPoints, $"Class {otherIds[i]} earned points should be 0");
                Assert.AreEqual(0, record.Experience, $"Class {otherIds[i]} XP should be 0");
            }
        }

        [Test]
        public void ShadowReaper_ResourceCap_IsExactlyTenMarks()
        {
            var reaper = CharacterCatalog.Find(CharacterCatalog.ShadowReaperId);
            Assert.IsNotNull(reaper);
            Assert.AreEqual(10f, reaper.BaseMaxResource);
            Assert.AreEqual(3f, reaper.SignatureResourceCost);

            var hud = new HudModel(reaper.BaseMaxHealth, reaper.ResourceName, reaper.BaseMaxResource, 1, reaper.StartingResource);
            Assert.AreEqual(0f, hud.Resource);

            hud.AddResource(1f);
            Assert.AreEqual(1f, hud.Resource);

            hud.AddResource(20f);
            Assert.AreEqual(10f, hud.Resource); // Clamped at 10
        }
    }
}
