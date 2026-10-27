using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class MagicCombatPlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed = 8888)
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
        public IEnumerator Player_CanEquipAndCastSpell_DealingDamageToInvader_AndGainingMagicMasteryXp()
        {
            yield return StartArena(9101);

            Assert.IsNotNull(_player.Run, "RunContext must exist");
            Assert.IsNotNull(_player.Run.Magic, "MagicSchoolProgression must exist on RunContext");

            var fireBoltDef = MagicSchoolCatalog.GetSpell("spell_fire_bolt");
            Assert.IsNotNull(fireBoltDef, "Fire Bolt definition must exist in catalog");

            bool equipped = _player.Run.Magic.TryEquipSpell(fireBoltDef, out var spellInst);
            Assert.IsTrue(equipped, "Spell should be equipped into magic progression");
            Assert.IsNotNull(spellInst);

            // Spawn a test invader near the player
            Vector2 spawnPos = (Vector2)_player.transform.position + new Vector2(2f, 0f);
            var invaderActor = InvaderActor.Spawn(spawnPos, InvaderArchetype.Knight, isElite: false, bossId: null, modifiers: BossModifier.None);

            float initialInvaderHp = invaderActor.Health.Current;
            Assert.Greater(initialInvaderHp, 0f);

            float initialSchoolXp = _player.Run.Magic.GetMasteryXp(MagicSchoolType.Fire);

            // Cast spell at invader
            var castResult = _player.CastSpell(spellInst, invaderActor);

            Assert.IsTrue(castResult.Success, $"Cast failed: {castResult.Reason}");
            Assert.Greater(castResult.DamageDealt, 0f);

            // Verify invader took damage
            Assert.Less(invaderActor.Health.Current, initialInvaderHp, "Invader should have received spell damage");

            // Verify player's magic school gained mastery XP
            float afterSchoolXp = _player.Run.Magic.GetMasteryXp(MagicSchoolType.Fire);
            Assert.Greater(afterSchoolXp, initialSchoolXp, "Magic school should have gained mastery XP from casting");

            Object.Destroy(invaderActor.gameObject);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Player_MultiSchoolProgression_RecognizesEquippedSynergies()
        {
            yield return StartArena(9102);

            var magic = _player.Run.Magic;
            Assert.IsNotNull(magic);

            var fireDef = MagicSchoolCatalog.GetSpell("spell_fire_bolt");
            var iceDef = MagicSchoolCatalog.GetSpell("spell_frost_shard");
            Assert.IsNotNull(fireDef);
            Assert.IsNotNull(iceDef);

            var thermalShock = MagicSchoolCatalog.FindSynergy(MagicSchoolType.Fire, MagicSchoolType.Ice);
            Assert.IsNotNull(thermalShock);

            // Initially not active
            Assert.IsFalse(magic.HasActiveSynergy(thermalShock));

            // Equip Fire spell
            magic.TryEquipSpell(fireDef, out _);
            Assert.IsFalse(magic.HasActiveSynergy(thermalShock));

            // Equip Ice spell
            magic.TryEquipSpell(iceDef, out _);

            // Now synergy is active!
            Assert.IsTrue(magic.HasActiveSynergy(thermalShock), "Equipping both Fire and Ice should activate Thermal Shock synergy");

            // Unequipping Fire disables the synergy
            magic.TryUnequipSpell(fireDef.Id);
            Assert.IsFalse(magic.HasActiveSynergy(thermalShock), "Unequipping Fire should deactivate Thermal Shock synergy");

            yield return null;
        }
    }
}
