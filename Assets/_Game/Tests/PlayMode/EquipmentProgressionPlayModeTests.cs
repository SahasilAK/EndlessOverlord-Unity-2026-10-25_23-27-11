using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class EquipmentProgressionPlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        private IEnumerator StartArena(long seed = 7777)
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
        public IEnumerator Player_EquippingLeveledArmor_ImmediatelyIncreasesPlayerMaxHealth()
        {
            yield return StartArena(8001);

            float initialMaxHp = _player.Health.Max;
            var session = GameSession.State;
            Assert.IsNotNull(session);
            var inv = session.Run.Inventory;
            Assert.IsNotNull(inv);

            // Create Titan Granite Bulwark armor, level it to level 3 (+30% bonus stats)
            var armorDef = ArmorCatalog.Get("arm_titan_granite_bulwark");
            Assert.IsNotNull(armorDef);

            var armorInstance = new ItemInstance(armorDef);
            armorInstance.AddMasteryXp(300); // Level 3
            Assert.AreEqual(3, armorInstance.Level);

            inv.TryAddItem(armorInstance);
            Assert.IsTrue(inv.TryEquip(armorInstance.InstanceId));

            yield return null;

            // Player Max HP should have increased by the effective stats of the armor
            float expectedEffectiveHp = armorInstance.GetEffectiveStats().FlatMaxHealth;
            Assert.AreEqual(initialMaxHp + expectedEffectiveHp, _player.Health.Max, 0.1f, "Equipping leveled armor must scale player MaxHealth.");
        }

        [UnityTest]
        public IEnumerator Merchant_StockContainsDiverseWeaponsAndArmors_AcrossRarities()
        {
            yield return StartArena(8002);

            var merchant = new MerchantSystem();
            merchant.GenerateStock(5, 99999L, 6);

            Assert.AreEqual(6, merchant.Stock.Count);
            bool hasWeaponOrArmor = false;
            for (int i = 0; i < merchant.Stock.Count; i++)
            {
                var item = merchant.Stock[i];
                Assert.IsNotNull(item.Definition);
                Assert.Greater(item.BuyPrice, 0);
                if (item.Definition.Slot == ItemSlot.Weapon || item.Definition.Slot == ItemSlot.Armor)
                {
                    hasWeaponOrArmor = true;
                }
            }

            Assert.IsTrue(hasWeaponOrArmor, "Merchant stock should feature weapons or armor from the expanded catalog.");
        }
    }
}
