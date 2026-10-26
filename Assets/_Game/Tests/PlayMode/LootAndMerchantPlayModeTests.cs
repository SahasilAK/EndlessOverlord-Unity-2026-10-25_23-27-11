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
    public class LootAndMerchantPlayModeTests
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

        private IEnumerator StartArena(long seed = 777L)
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
        public IEnumerator WorldPickup_MagnetPullsAndPlayerCollectsGoldOnce()
        {
            yield return StartArena(101L);
            var run = GameSession.State.Run;

            int initialGold = run.Hud.Gold;
            Vector2 spawnPos = _player.Position + new Vector2(1.5f, 0f);

            var pickup = LootPickupActor.SpawnGold(spawnPos, 25, run.Hud);
            Assert.IsNotNull(pickup);
            Assert.IsFalse(pickup.IsCollected);

            // Step simulation: magnet will attract pickup to player
            float timer = 0f;
            while (!pickup.IsCollected && timer < 2f)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            Assert.IsTrue(pickup.IsCollected, "Pickup should be collected via magnet");
            Assert.AreEqual(initialGold + 25, run.Hud.Gold, "HUD gold must increment by exact amount");

            // Pickup cannot be collected again
            Assert.IsFalse(pickup.TryCollect());
        }

        [UnityTest]
        public IEnumerator WorldPickup_ItemCollection_AddedToInventory()
        {
            yield return StartArena(202L);
            var run = GameSession.State.Run;

            var wandDef = ItemCatalog.GetById(ItemCatalog.BoneWandId);
            var itemInst = new ItemInstance(wandDef);
            Vector2 spawnPos = _player.Position + new Vector2(1.2f, 0f);

            var pickup = LootPickupActor.SpawnItem(spawnPos, itemInst, run.Inventory);
            Assert.IsNotNull(pickup);
            Assert.AreEqual(0, run.Inventory.Bag.Count);

            float timer = 0f;
            while (!pickup.IsCollected && timer < 2f)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            Assert.IsTrue(pickup.IsCollected);
            Assert.AreEqual(1, run.Inventory.Bag.Count);
            Assert.AreEqual(itemInst.InstanceId, run.Inventory.Bag[0].InstanceId);
        }

        [UnityTest]
        public IEnumerator EquipItem_ChangesPlayerRuntimeCombatStats()
        {
            yield return StartArena(303L);
            var run = GameSession.State.Run;

            float initialMaxHp = _player.Health.Max;

            // Add Crypt Robes (+6 Armor, +25 Max HP)
            var robes = new ItemInstance(ItemCatalog.GetById(ItemCatalog.CryptRobesId));
            run.Inventory.TryAddItem(robes);
            run.Inventory.TryEquip(robes.InstanceId);
            yield return null;

            Assert.AreEqual(initialMaxHp + 25f, _player.Health.Max, 1e-4f);

            // With +6 Armor: raw 20 dmg -> 20 * 100 / (100 + 6) = 18.8679f
            float dealt = _player.ReceiveDamage(20f);
            Assert.AreEqual(18.8679f, dealt, 0.05f);

            // Add Skull Ring (+12% Move Speed)
            var ring = new ItemInstance(ItemCatalog.GetById(ItemCatalog.SkullRingId));
            run.Inventory.TryAddItem(ring);
            run.Inventory.TryEquip(ring.InstanceId);
            yield return null;

            Assert.AreEqual(0.12f, run.Inventory.GetEquippedStats().MoveSpeedPercent, 1e-4f);
        }

        [UnityTest]
        public IEnumerator MerchantUI_BuyItemAndSellEquippedItem_ExactAccounting()
        {
            yield return StartArena(404L);
            var run = GameSession.State.Run;

            // Give player 50 gold and buffer health
            _player.Health.IncreaseMax(500f, healDelta: true);
            run.Hud.AddGold(50);
            Assert.AreEqual(50, run.Hud.Gold);

            // Clear wave 1 to reach reward phase
            _arena.WaveDirector.ClearAllEnemies();
            while (_arena.WaveDirector.Ledger.SpawnsRemaining > 0)
            {
                _arena.WaveDirector.Ledger.TryRecordSpawn();
                _arena.WaveDirector.Ledger.TryRecordDeath();
            }
            yield return null;

            // Pick reward to enter Intermission
            var offer = _arena.WaveDirector.RewardModal.CurrentOffer;
            offer.TryPick(offer.Choices[0].Id, run.Rewards);
            GameSession.Machine.TryTransition(RunState.Intermission);
            yield return null;

            Assert.AreEqual(RunState.Intermission, GameSession.Machine.Current);

            // Open merchant modal
            _arena.WaveDirector.OpenMerchant();
            yield return null;

            Assert.AreEqual(RunState.Merchant, GameSession.Machine.Current);
            Assert.AreEqual(0f, Time.timeScale, "Simulation must pause during merchant");
            Assert.IsTrue(_arena.WaveDirector.MerchantView.IsOpen);

            // Buy first item in stock
            var stockItem = run.Merchant.Stock[0];
            int buyPrice = stockItem.BuyPrice;
            int sellPrice = stockItem.SellPrice;

            string error;
            bool bought = run.Merchant.TryBuy(stockItem.InstanceId, run.Inventory, run.Hud, out error);
            Assert.IsTrue(bought, error);
            Assert.AreEqual(50 - buyPrice, run.Hud.Gold);
            Assert.AreEqual(1, run.Inventory.Bag.Count);

            // Equip the bought item
            Assert.IsTrue(run.Inventory.TryEquip(stockItem.InstanceId));
            Assert.AreEqual(0, run.Inventory.Bag.Count);
            Assert.IsNotNull(run.Inventory.GetEquippedInSlot(stockItem.Definition.Slot));

            // Sell the equipped item directly
            int goldBeforeSale = run.Hud.Gold;
            bool sold = run.Merchant.TrySell(stockItem.InstanceId, run.Inventory, run.Hud, out error);
            Assert.IsTrue(sold, error);
            Assert.AreEqual(goldBeforeSale + sellPrice, run.Hud.Gold);
            Assert.IsNull(run.Inventory.GetEquippedInSlot(stockItem.Definition.Slot));

            // Close merchant
            _arena.WaveDirector.MerchantView.Close();
            yield return null;

            Assert.AreEqual(RunState.Intermission, GameSession.Machine.Current);
        }
    }
}
