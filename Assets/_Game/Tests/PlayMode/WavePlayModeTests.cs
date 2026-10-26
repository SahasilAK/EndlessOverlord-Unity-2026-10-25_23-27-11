using System.Collections;
using System.Collections.Generic;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class WavePlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed)
        {
            GameSession.Reset(seed);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.IsTrue(menu.StartSelectedRun());

            yield return WaitForScene(SceneNames.GameArena);
            _arena = Object.FindAnyObjectByType<ArenaController>();
            _player = _arena.Player;
            Assert.IsNotNull(_player, "player actor missing");
        }

        private static IEnumerator WaitForScene(string name)
        {
            float deadline = Time.realtimeSinceStartup + 10f;
            while (SceneManager.GetActiveScene().name != name && Time.realtimeSinceStartup < deadline)
                yield return null;
            Assert.AreEqual(name, SceneManager.GetActiveScene().name);
            yield return null;
            yield return null;
        }

        [UnityTest]
        public IEnumerator Wave1ClearsAndTriggersIntermissionOnceAllEnemiesDie()
        {
            yield return StartArena(1001);
            var director = _arena.WaveDirector;
            Assert.IsNotNull(director, "WaveDirector missing");

            // Give player safety margin so stationary test player survives all 5 enemies while auto-attacking
            _player.Health.Reset(400f);

            int completedWave = 0;
            director.WaveCompleted += w => completedWave = w;

            // Wait for Wave 1 to complete via auto attack
            float deadline = Time.realtimeSinceStartup + 25f;
            while (!director.IsIntermission && Time.realtimeSinceStartup < deadline)
                yield return null;

            Assert.IsTrue(director.IsIntermission, "Wave 1 should be in intermission after completion");
            Assert.AreEqual(1, completedWave, "WaveCompleted event should fire with wave 1");
            Assert.AreEqual(0, director.Ledger.LivingEnemies, "No enemies should be living on wave clear");
            Assert.AreEqual(0, director.Ledger.SpawnsRemaining, "No spawns should remain on wave clear");
        }

        [UnityTest]
        public IEnumerator ArcherKitesWhenPlayerIsTooCloseAndFiresArrow()
        {
            yield return StartArena(1002);
            _arena.WaveDirector.ClearAllEnemies();
            _arena.WaveDirector.enabled = false; // isolate

            // Spawn Archer close to player (2 units away)
            Vector2 archerSpawn = _player.Position + new Vector2(2f, 0f);
            var archer = InvaderActor.Spawn(archerSpawn, InvaderArchetype.Archer, false);

            yield return new WaitForSeconds(0.4f);

            // Archer should retreat away from player
            float distAfterRetreat = (archer.Position - _player.Position).magnitude;
            Assert.Greater(distAfterRetreat, 2.0f, "Archer should kite backwards when inside min range");

            // Wait for arrow to be fired
            int activeProjectiles = 0;
            float deadline = Time.realtimeSinceStartup + 4f;
            while (activeProjectiles == 0 && Time.realtimeSinceStartup < deadline)
            {
                if (ProjectileManager.Instance != null && ProjectileManager.Instance.ActiveCount > 0)
                    activeProjectiles = ProjectileManager.Instance.ActiveCount;
                yield return null;
            }

            Assert.Greater(activeProjectiles, 0, "Archer should have fired an arrow projectile");
        }

        [UnityTest]
        public IEnumerator ClericHealsInjuredAlly()
        {
            yield return StartArena(1003);
            _arena.WaveDirector.ClearAllEnemies();
            _arena.WaveDirector.enabled = false;
            _player.GetComponent<AutoAttacker>().enabled = false; // Prevent player from killing test targets

            // Spawn Swordsman and Cleric
            var swordsman = InvaderActor.Spawn(_player.Position + new Vector2(5f, 0f), InvaderArchetype.Swordsman);
            var cleric = InvaderActor.Spawn(_player.Position + new Vector2(7f, 0f), InvaderArchetype.Cleric);

            // Injure swordsman
            swordsman.Health.TakeDamage(20f);
            float woundedHp = swordsman.Health.Current;
            Assert.AreEqual(swordsman.Health.Max - 20f, woundedHp, 0.01f);

            // Wait for Cleric to heal swordsman
            float deadline = Time.realtimeSinceStartup + 5f;
            while (swordsman.Health.Current <= woundedHp && Time.realtimeSinceStartup < deadline)
                yield return null;

            Assert.Greater(swordsman.Health.Current, woundedHp, "Cleric should have restored health to injured ally");
        }

        [UnityTest]
        public IEnumerator Wave10BossSpawnsAndEnragesAtHalfHealth()
        {
            yield return StartArena(1004);
            var director = _arena.WaveDirector;

            // Jump directly to Wave 10 (Boss Wave)
            director.StartWave(10);
            Assert.AreEqual(10, director.CurrentWave);

            yield return new WaitForSeconds(0.6f);

            // Verify Royal Champion Boss spawned
            InvaderActor boss = null;
            var all = ActorRegistry.AllActors;
            for (int i = 0; i < all.Count; i++)
            {
                if (all[i] is InvaderActor inv && inv.IsBoss)
                {
                    boss = inv;
                    break;
                }
            }

            Assert.IsNotNull(boss, "Royal Champion Boss should exist on Wave 10");
            Assert.AreEqual(CombatTuning.BossHealth, boss.Health.Max, 0.1f);

            // Deal true health damage to drop boss below 50% HP
            boss.Health.TakeDamage(boss.Health.Max * 0.55f);
            yield return null;

            // Boss should enrage, speed up, and summon 2 swordsman reinforcements
            Assert.AreEqual(CombatTuning.BossEnragedSpeed, boss.MoveSpeed, 0.1f);
            Assert.GreaterOrEqual(ActorRegistry.Count, 3, "Boss enrage should summon reinforcements");
        }

        [UnityTest]
        public IEnumerator RepeatedWaveAdvancementLeavesNoLeakedActors()
        {
            yield return StartArena(1005);
            var director = _arena.WaveDirector;

            // Advance through 3 waves rapidly
            for (int w = 1; w <= 3; w++)
            {
                director.StartWave(w);
                yield return new WaitForSeconds(0.2f);
            }

            // Return to menu
            _arena.ReturnToMenu();
            yield return WaitForScene(SceneNames.MainMenu);

            Assert.AreEqual(0, ActorRegistry.Count, "ActorRegistry must be completely empty after returning to menu");
        }
    }
}
