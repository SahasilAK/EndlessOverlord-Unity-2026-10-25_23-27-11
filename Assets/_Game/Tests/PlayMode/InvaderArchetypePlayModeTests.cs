using System.Collections;
using System.Collections.Generic;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class InvaderArchetypePlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed = 1234)
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
            Assert.IsNotNull(_player, "Player actor must exist");

            // Disable standard wave spawning so tests can isolate individual archetypes
            if (_arena.WaveDirector != null)
            {
                _arena.WaveDirector.ClearAllEnemies();
                _arena.WaveDirector.enabled = false;
            }
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
        public IEnumerator Invaders_SpawnWithCatalogTunedAttributes()
        {
            yield return StartArena(2001);

            // Test Tank (ShieldGuardian)
            var tankDef = InvaderCatalog.Get(InvaderArchetype.ShieldGuardian);
            var tank = InvaderActor.Spawn(_player.Position + new Vector2(8f, 0f), InvaderArchetype.ShieldGuardian);
            Assert.IsNotNull(tank);
            Assert.AreEqual(tankDef.BaseHealth, tank.Health.Max, 0.1f);
            Assert.AreEqual(tankDef.BaseMoveSpeed, tank.MoveSpeed, 0.1f);

            // Test Assassin (ShadowDuelist)
            var assassinDef = InvaderCatalog.Get(InvaderArchetype.ShadowDuelist);
            var assassin = InvaderActor.Spawn(_player.Position + new Vector2(-8f, 0f), InvaderArchetype.ShadowDuelist);
            Assert.IsNotNull(assassin);
            Assert.AreEqual(assassinDef.BaseHealth, assassin.Health.Max, 0.1f);
            Assert.AreEqual(assassinDef.BaseMoveSpeed, assassin.MoveSpeed, 0.1f);
            Assert.Greater(assassin.MoveSpeed, tank.MoveSpeed, "Assassin should move faster than Tank");
        }

        [UnityTest]
        public IEnumerator CasterInvader_FiresProjectileAtPlayer()
        {
            yield return StartArena(2002);
            _player.GetComponent<AutoAttacker>().enabled = false;

            // Spawn FireMage within casting range (around 5 units)
            Vector2 spawnPos = _player.Position + new Vector2(5f, 0f);
            var mage = InvaderActor.Spawn(spawnPos, InvaderArchetype.FireMage);
            Assert.IsNotNull(mage);

            int projCount = 0;
            float deadline = Time.realtimeSinceStartup + 4f;
            while (projCount == 0 && Time.realtimeSinceStartup < deadline)
            {
                if (ProjectileManager.Instance != null && ProjectileManager.Instance.ActiveCount > 0)
                    projCount = ProjectileManager.Instance.ActiveCount;
                yield return null;
            }

            Assert.Greater(projCount, 0, "Fire Mage should cast and spawn a projectile toward the player");
        }

        [UnityTest]
        public IEnumerator SupportInvader_HealsWoundedAlly()
        {
            yield return StartArena(2003);
            _player.GetComponent<AutoAttacker>().enabled = false;

            // Spawn injured Knight ally and Priest healer
            var knight = InvaderActor.Spawn(_player.Position + new Vector2(6f, 2f), InvaderArchetype.Knight);
            var priest = InvaderActor.Spawn(_player.Position + new Vector2(7f, 2f), InvaderArchetype.Priest);

            knight.Health.TakeDamage(25f);
            float woundedHp = knight.Health.Current;
            Assert.Less(woundedHp, knight.Health.Max);

            float deadline = Time.realtimeSinceStartup + 5f;
            while (knight.Health.Current <= woundedHp && Time.realtimeSinceStartup < deadline)
            {
                yield return null;
            }

            Assert.Greater(knight.Health.Current, woundedHp, "Priest should heal wounded ally");
        }
    }
}
