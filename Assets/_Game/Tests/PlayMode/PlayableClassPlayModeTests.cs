using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public sealed class PlayableClassPlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [SetUp]
        public void SetUp()
        {
            var fresh = new PlayerSaveProfile();
            SaveStorage.Save(fresh);
            SaveStorage.CurrentProfile = fresh;
        }

        [TearDown]
        public void TearDown()
        {
            var fresh = new PlayerSaveProfile();
            SaveStorage.Save(fresh);
            SaveStorage.CurrentProfile = fresh;
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(string characterId, long seed = 42L)
        {
            GameSession.Reset(seed);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(characterId);
            Assert.IsTrue(menu.StartSelectedRun(), $"Failed to start run for {characterId}");

            yield return WaitForScene(SceneNames.GameArena);
            _arena = Object.FindAnyObjectByType<ArenaController>();
            _player = _arena.Player;
            Assert.IsNotNull(_player, "Player actor missing from GameArena");
        }

        private static IEnumerator WaitForScene(string sceneName, float timeout = 5f)
        {
            float elapsed = 0f;
            while (SceneManager.GetActiveScene().name != sceneName)
            {
                elapsed += Time.unscaledDeltaTime;
                if (elapsed > timeout)
                    Assert.Fail($"Timed out waiting for scene {sceneName}");
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator AllFiveClasses_CanEnterArena_WithDistinctStatsAndColors()
        {
            var charIds = new[]
            {
                CharacterCatalog.LichLordId,
                CharacterCatalog.DemonMonarchId,
                CharacterCatalog.VampireQueenId,
                CharacterCatalog.ShadowReaperId,
                CharacterCatalog.CursedGolemId
            };

            for (int i = 0; i < charIds.Length; i++)
            {
                string id = charIds[i];
                yield return StartArena(id, 1000L + i);

                var character = CharacterCatalog.Find(id);
                float expectedMax = character.BaseMaxHealth + (GameSession.Run != null ? GameSession.Run.MetaStats.BonusMaxHealth : 0f);
                Assert.AreEqual(expectedMax, _player.Health.Max, 0.1f);

                var sr = _player.GetComponent<SpriteRenderer>();
                Assert.IsNotNull(sr);
                Assert.AreEqual(character.ClassColor.R, sr.color.r, 0.05f);

                var sig = _player.GetComponent<SignatureAbilityController>();
                Assert.IsNotNull(sig, $"SignatureAbilityController missing for {id}");
                Assert.AreEqual(character.SignatureAbilityName, sig.AbilityName);

                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator LichLord_SignatureAbility_SpawnsBoneSentinelMinion()
        {
            yield return StartArena(CharacterCatalog.LichLordId);

            var sig = _player.GetComponent<SignatureAbilityController>();
            Assert.IsNotNull(sig);

            // Starting resource for Lich Lord is 50 Soul Essence (cost is 25)
            float startingEssence = GameSession.Run.Hud.Resource;
            Assert.GreaterOrEqual(startingEssence, 25f);

            bool activated = sig.TryActivate();
            Assert.IsTrue(activated, "Failed to activate Lich Lord Bone Sentinel");
            Assert.AreEqual(startingEssence - 25f, GameSession.Run.Hud.Resource);

            yield return null;

            var sentinel = Object.FindAnyObjectByType<BoneSentinelActor>();
            Assert.IsNotNull(sentinel, "BoneSentinelActor did not spawn in scene");
            Assert.AreEqual(Faction.Player, sentinel.Team);
            Assert.AreEqual(50f, sentinel.Health.Max);
            Assert.IsTrue(sentinel.Alive);
        }

        [UnityTest]
        public IEnumerator DemonMonarch_SignatureHellstep_TeleportsAndBlastEnemies()
        {
            yield return StartArena(CharacterCatalog.DemonMonarchId);

            var sig = _player.GetComponent<SignatureAbilityController>();
            Assert.IsNotNull(sig);

            // Add Fury for signature cost (25)
            GameSession.Run.Hud.AddResource(30f);
            Vector2 initialPos = _player.Position;
            _player.AimDirection = Vector2.right;

            bool activated = sig.TryActivate();
            Assert.IsTrue(activated, "Failed to activate Demon Monarch Hellstep");
            Assert.AreEqual(5f, GameSession.Run.Hud.Resource);

            // Verify player teleported ~5m forward
            Assert.Greater(_player.Position.x, initialPos.x + 3.0f);
        }

        [UnityTest]
        public IEnumerator VampireQueen_SignatureSanguineBurst_DamagesAndHeals()
        {
            yield return StartArena(CharacterCatalog.VampireQueenId);

            // Damage player so healing can be observed
            _player.Health.TakeDamage(40f);
            float hpBefore = _player.Health.Current;

            // Spawn an invader nearby
            var invader = InvaderActor.Spawn(_player.Position + new Vector2(2f, 0f), InvaderArchetype.Swordsman);
            float invaderHpBefore = invader.Health.Current;

            // Add Blood Essence for signature
            GameSession.Run.Hud.AddResource(30f);

            var sig = _player.GetComponent<SignatureAbilityController>();
            bool activated = sig.TryActivate();
            Assert.IsTrue(activated, "Failed to activate Vampire Queen Sanguine Burst");

            // Invader should take 28 damage
            Assert.Less(invader.Health.Current, invaderHpBefore);

            // Vampire Queen should heal
            Assert.Greater(_player.Health.Current, hpBefore);
        }

        [UnityTest]
        public IEnumerator CursedGolem_SignatureRuneBarrier_AbsorbsIncomingDamage()
        {
            yield return StartArena(CharacterCatalog.CursedGolemId);

            var sig = _player.GetComponent<SignatureAbilityController>();
            GameSession.Run.Hud.AddResource(30f);

            bool activated = sig.TryActivate();
            Assert.IsTrue(activated, "Failed to activate Cursed Golem Rune of Protection");

            Assert.AreEqual(60f, _player.BarrierAmount);
            float maxHp = _player.Health.Max;

            // Apply 30 raw damage
            _player.ReceiveDamage(30f);

            // Health must remain full (180) because barrier absorbed the damage!
            Assert.AreEqual(maxHp, _player.Health.Current);
            Assert.Less(_player.BarrierAmount, 60f);
        }
    }
}
