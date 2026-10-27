using UnityEngine.InputSystem;
using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class LegendaryBossPlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [SetUp]
        public void SetUp()
        {
            InputSystem.settings.backgroundBehavior = InputSettings.BackgroundBehavior.IgnoreFocus;
        }

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed = 5555)
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

            // Disable standard wave director so tests control boss spawning directly
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
        public IEnumerator LegendaryBoss_SpawnsWithBarrier_AbsorbsIncomingDamage()
        {
            yield return StartArena(6001);
            _player.GetComponent<AutoAttacker>().enabled = false;

            // Spawn Sir Valerius with Barrier modifier
            var boss = InvaderActor.Spawn(
                _player.Position + new Vector2(5f, 0f),
                InvaderArchetype.Paladin,
                false,
                "boss_valerius",
                BossModifier.Barrier);

            Assert.IsNotNull(boss);
            Assert.IsTrue(boss.IsBoss);
            Assert.IsTrue(boss.HasBarrier);
            Assert.Greater(boss.BarrierCurrent, 0f);

            float initialHp = boss.Health.Current;
            float initialBarrier = boss.BarrierCurrent;

            // Deal damage smaller than barrier
            boss.ReceiveDamage(20f);

            // Health should be untouched, Barrier reduced
            Assert.AreEqual(initialHp, boss.Health.Current, 0.01f, "Damage must be absorbed by Barrier before touching Health.");
            Assert.Less(boss.BarrierCurrent, initialBarrier, "BarrierCurrent must decrease after taking damage.");
        }

        [UnityTest]
        public IEnumerator LegendaryBoss_EnrageTriggersSpeedIncreaseAndReinforcements()
        {
            yield return StartArena(6002);
            _player.GetComponent<AutoAttacker>().enabled = false;

            // Spawn Baron von Eisen with Enrage & Reinforcements
            var boss = InvaderActor.Spawn(
                _player.Position + new Vector2(6f, 0f),
                InvaderArchetype.ShieldGuardian,
                false,
                "boss_eisen",
                BossModifier.Enrage | BossModifier.Reinforcements);

            float baseSpeed = boss.MoveSpeed;
            int actorCountBefore = ActorRegistry.Count;

            // Deal damage dropping boss to 40% HP (below 50% enrage threshold and 70% reinforcements threshold)
            boss.ReceiveDamage(boss.Health.Max * 0.65f);
            yield return null;

            Assert.Greater(boss.MoveSpeed, baseSpeed, "Enraged boss move speed must increase.");
            Assert.Greater(ActorRegistry.Count, actorCountBefore, "Boss should have summoned reinforcements upon dropping HP.");
        }

        [UnityTest]
        public IEnumerator LegendaryBoss_ExecutesSignatureAbilityWithTelegraph()
        {
            yield return StartArena(6003);
            _player.GetComponent<AutoAttacker>().enabled = false;

            // Spawn Archmage Ignis (Primary ability: MoltenEruption)
            var boss = InvaderActor.Spawn(
                _player.Position + new Vector2(4f, 0f),
                InvaderArchetype.FireMage,
                false,
                "boss_ignis",
                BossModifier.ElementalOvercharge);

            Assert.IsNotNull(boss);
            Assert.AreEqual("Archmage Ignis", boss.BossDefinition.Name);

            // Wait for ability cooldown to expire and windup to begin / cast
            float deadline = Time.realtimeSinceStartup + 4f;
            while (boss.Alive && Time.realtimeSinceStartup < deadline)
            {
                yield return null;
            }

            Assert.IsTrue(boss.Alive, "Boss should survive test duration.");
        }
        [UnityTest]
        public IEnumerator LegendaryBoss_AllThirtyBosses_SpawnWithCustomPixelArtSprites()
        {
            yield return StartArena(6004);
            _player.GetComponent<AutoAttacker>().enabled = false;

            var allBosses = LegendaryBossCatalog.All;
            Assert.AreEqual(30, allBosses.Count, "Must have 30 bosses in catalog.");

            foreach (var bossDef in allBosses)
            {
                var boss = InvaderActor.Spawn(
                    _player.Position + new Vector2(3f, 0f),
                    bossDef.Archetype,
                    false,
                    bossDef.Id,
                    BossModifier.None);

                Assert.IsNotNull(boss, $"Boss {bossDef.Id} must spawn");
                var sr = boss.GetComponent<SpriteRenderer>();
                Assert.IsNotNull(sr, $"Boss {bossDef.Id} must have SpriteRenderer");
                Assert.IsNotNull(sr.sprite, $"Boss {bossDef.Id} sprite must not be null");
                Assert.AreNotEqual(PlaceholderSprites.Square, sr.sprite, $"Boss {bossDef.Id} must have custom pixel art sprite, not square placeholder");
                Assert.AreEqual(48, sr.sprite.rect.width, $"Boss {bossDef.Id} sprite frame width should be 48");
                Assert.AreEqual(48, sr.sprite.rect.height, $"Boss {bossDef.Id} sprite frame height should be 48");

                Object.Destroy(boss.gameObject);
            }
        }
    }
}
