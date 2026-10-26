using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Combined Merchant Shop and Player Inventory procedural uGUI modal.
    /// Handles buying, selling (including equipped gear), equipping, unequipping, and stock rerolls.
    /// Fully atomic and backed by pure C# systems (<see cref="MerchantSystem"/> and <see cref="PlayerInventory"/>).
    /// </summary>
    public sealed class MerchantInventoryView : MonoBehaviour
    {
        private GameObject _panel;
        private Text _goldText;
        private Text _statusText;
        private Transform _stockContainer;
        private Transform _equippedContainer;
        private Transform _bagContainer;
        private Text _bagCapacityText;

        private RunContext _run;
        private Action _onClosed;
        private readonly List<GameObject> _spawnedStockElements = new List<GameObject>();
        private readonly List<GameObject> _spawnedEquippedElements = new List<GameObject>();
        private readonly List<GameObject> _spawnedBagElements = new List<GameObject>();

        public bool IsOpen => _panel != null && _panel.activeSelf;

        public void Initialize()
        {
            UiFactory.EnsureEventSystem();
            var canvas = UiFactory.CreateCanvas("MerchantInventoryCanvas", 35);
            canvas.transform.SetParent(transform, false);

            var backdrop = UiFactory.CreatePanel(canvas.transform, "Backdrop", new Color(0.03f, 0.03f, 0.07f, 0.95f));
            _panel = backdrop.gameObject;

            // Header
            var title = UiFactory.CreateText(_panel.transform, "Title", "DUNGEON MERCHANT & INVENTORY", 44, TextAnchor.MiddleCenter, new Color(1f, 0.85f, 0.25f));
            UiFactory.Place(title.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -40f), new Vector2(1000f, 60f));

            _goldText = UiFactory.CreateText(_panel.transform, "GoldText", "Gold: 0", 28, TextAnchor.MiddleRight, new Color(1f, 0.9f, 0.3f));
            UiFactory.Place(_goldText.rectTransform, new Vector2(1f, 1f), new Vector2(-60f, -40f), new Vector2(300f, 50f));

            _statusText = UiFactory.CreateText(_panel.transform, "StatusText", "", 22, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(_statusText.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -95f), new Vector2(1000f, 40f));

            // Left Section: Merchant Stock
            var merchantHeader = UiFactory.CreateText(_panel.transform, "MerchantHeader", "MERCHANT STOCK", 30, TextAnchor.MiddleCenter, new Color(0.9f, 0.9f, 1f));
            UiFactory.Place(merchantHeader.rectTransform, new Vector2(0.25f, 1f), new Vector2(0f, -145f), new Vector2(400f, 45f));

            var rerollBtn = UiFactory.CreateButton(_panel.transform, "RerollBtn", $"REROLL ({MerchantSystem.RerollCost}g)", OnRerollClicked);
            UiFactory.Place((RectTransform)rerollBtn.transform, new Vector2(0.25f, 1f), new Vector2(0f, -195f), new Vector2(260f, 45f));

            var stockGo = new GameObject("StockContainer", typeof(RectTransform));
            stockGo.transform.SetParent(_panel.transform, false);
            _stockContainer = stockGo.transform;
            UiFactory.Place((RectTransform)_stockContainer, new Vector2(0.25f, 0.5f), new Vector2(0f, -80f), new Vector2(600f, 550f));

            // Right Section: Equipment & Inventory
            var equipHeader = UiFactory.CreateText(_panel.transform, "EquipHeader", "EQUIPPED GEAR", 30, TextAnchor.MiddleCenter, new Color(0.9f, 0.9f, 1f));
            UiFactory.Place(equipHeader.rectTransform, new Vector2(0.75f, 1f), new Vector2(0f, -145f), new Vector2(400f, 45f));

            var equipGo = new GameObject("EquippedContainer", typeof(RectTransform));
            equipGo.transform.SetParent(_panel.transform, false);
            _equippedContainer = equipGo.transform;
            UiFactory.Place((RectTransform)_equippedContainer, new Vector2(0.75f, 1f), new Vector2(0f, -280f), new Vector2(650f, 220f));

            _bagCapacityText = UiFactory.CreateText(_panel.transform, "BagCapacity", "INVENTORY BAG (0/12)", 26, TextAnchor.MiddleCenter, new Color(0.9f, 0.9f, 1f));
            UiFactory.Place(_bagCapacityText.rectTransform, new Vector2(0.75f, 0.5f), new Vector2(0f, -30f), new Vector2(400f, 40f));

            var bagGo = new GameObject("BagContainer", typeof(RectTransform));
            bagGo.transform.SetParent(_panel.transform, false);
            _bagContainer = bagGo.transform;
            UiFactory.Place((RectTransform)_bagContainer, new Vector2(0.75f, 0.5f), new Vector2(0f, -220f), new Vector2(650f, 320f));

            // Close Button
            var closeBtn = UiFactory.CreateButton(_panel.transform, "CloseBtn", "BACK TO INTERMISSION", Close);
            UiFactory.Place((RectTransform)closeBtn.transform, new Vector2(0.5f, 0f), new Vector2(0f, 45f), new Vector2(340f, 60f));

            _panel.SetActive(false);
        }

        public void Show(RunContext run, Action onClosed = null)
        {
            _run = run;
            _onClosed = onClosed;

            if (_run != null)
            {
                if (_run.Merchant.Stock.Count == 0)
                {
                    _run.Merchant.GenerateStock(_run.Hud.Wave, _run.Seed);
                }
                _run.Merchant.StockChanged += Refresh;
                _run.Inventory.Changed += Refresh;
                _run.Hud.Changed += RefreshGold;
            }

            _panel.SetActive(true);
            SetStatus("");
            Refresh();
        }

        public void Close()
        {
            if (_run != null)
            {
                _run.Merchant.StockChanged -= Refresh;
                _run.Inventory.Changed -= Refresh;
                _run.Hud.Changed -= RefreshGold;
            }

            if (_panel != null)
                _panel.SetActive(false);

            _onClosed?.Invoke();
        }

        private void RefreshGold()
        {
            if (_goldText != null && _run != null && _run.Hud != null)
                _goldText.text = $"Gold: {_run.Hud.Gold}";
        }

        private void SetStatus(string message, bool isError = false)
        {
            if (_statusText == null) return;
            _statusText.text = message;
            _statusText.color = isError ? new Color(1f, 0.4f, 0.4f) : new Color(0.5f, 1f, 0.6f);
        }

        public void Refresh()
        {
            RefreshGold();
            RefreshStock();
            RefreshEquipped();
            RefreshBag();
        }

        private void RefreshStock()
        {
            ClearList(_spawnedStockElements);
            if (_run == null || _run.Merchant == null) return;

            var stock = _run.Merchant.Stock;
            float cardHeight = 110f;
            float spacing = 15f;
            float startY = 160f;

            for (int i = 0; i < stock.Count; i++)
            {
                var item = stock[i];
                float y = startY - i * (cardHeight + spacing);
                CreateStockCard(item, y);
            }
        }

        private void CreateStockCard(ItemInstance item, float posY)
        {
            var card = new GameObject("Stock_" + item.InstanceId, typeof(RectTransform));
            card.transform.SetParent(_stockContainer, false);
            var rt = (RectTransform)card.transform;
            UiFactory.Place(rt, new Vector2(0.5f, 0.5f), new Vector2(0f, posY), new Vector2(560f, 105f));

            var img = card.AddComponent<Image>();
            img.color = new Color(0.12f, 0.14f, 0.2f, 0.95f);

            Color rarityCol = LootPickupActor.GetRarityColor(item.Definition.Rarity);

            // Name & slot
            var nameTxt = UiFactory.CreateText(card.transform, "Name", $"{item.Definition.Name} [{item.Definition.Slot}]", 22, TextAnchor.UpperLeft, rarityCol);
            UiFactory.Place(nameTxt.rectTransform, new Vector2(0f, 1f), new Vector2(15f, -12f), new Vector2(360f, 30f));

            // Stats
            var statsTxt = UiFactory.CreateText(card.transform, "Stats", item.Definition.Stats.GetSummary(), 18, TextAnchor.UpperLeft, Color.white);
            UiFactory.Place(statsTxt.rectTransform, new Vector2(0f, 1f), new Vector2(15f, -42f), new Vector2(360f, 50f));

            // Price & Buy
            var buyBtn = UiFactory.CreateButton(card.transform, "BuyBtn", $"BUY ({item.BuyPrice}g)", () =>
            {
                string error;
                if (_run.Merchant.TryBuy(item.InstanceId, _run.Inventory, _run.Hud, out error))
                {
                    SetStatus($"Purchased {item.Definition.Name}!");
                }
                else
                {
                    SetStatus(error, isError: true);
                }
            });
            UiFactory.Place((RectTransform)buyBtn.transform, new Vector2(1f, 0.5f), new Vector2(-15f, 0f), new Vector2(140f, 55f));

            _spawnedStockElements.Add(card);
        }

        private void RefreshEquipped()
        {
            ClearList(_spawnedEquippedElements);
            if (_run == null || _run.Inventory == null) return;

            var inv = _run.Inventory;
            CreateEquippedSlotRow(ItemSlot.Weapon, inv.EquippedWeapon, 60f);
            CreateEquippedSlotRow(ItemSlot.Armor, inv.EquippedArmor, -10f);
            CreateEquippedSlotRow(ItemSlot.Accessory, inv.EquippedAccessory, -80f);
        }

        private void CreateEquippedSlotRow(ItemSlot slot, ItemInstance item, float posY)
        {
            var row = new GameObject("Slot_" + slot, typeof(RectTransform));
            row.transform.SetParent(_equippedContainer, false);
            var rt = (RectTransform)row.transform;
            UiFactory.Place(rt, new Vector2(0.5f, 0.5f), new Vector2(0f, posY), new Vector2(620f, 60f));

            var img = row.AddComponent<Image>();
            img.color = new Color(0.1f, 0.12f, 0.16f, 0.9f);

            if (item == null)
            {
                var emptyTxt = UiFactory.CreateText(row.transform, "Empty", $"[{slot.ToString().ToUpperInvariant()} EMPTY]", 20, TextAnchor.MiddleLeft, new Color(0.6f, 0.6f, 0.6f));
                UiFactory.Place(emptyTxt.rectTransform, new Vector2(0f, 0.5f), new Vector2(15f, 0f), new Vector2(400f, 40f));
            }
            else
            {
                Color rarityCol = LootPickupActor.GetRarityColor(item.Definition.Rarity);
                var infoTxt = UiFactory.CreateText(row.transform, "Info", $"{item.Definition.Name}: {item.Definition.Stats.GetSummary()}", 19, TextAnchor.MiddleLeft, rarityCol);
                UiFactory.Place(infoTxt.rectTransform, new Vector2(0f, 0.5f), new Vector2(15f, 0f), new Vector2(400f, 40f));

                var unequipBtn = UiFactory.CreateButton(row.transform, "UnequipBtn", "UNEQUIP", () =>
                {
                    if (_run.Inventory.TryUnequip(slot))
                    {
                        SetStatus($"Unequipped {item.Definition.Name}");
                    }
                    else
                    {
                        SetStatus("Cannot unequip: Bag is full!", isError: true);
                    }
                });
                UiFactory.Place((RectTransform)unequipBtn.transform, new Vector2(1f, 0.5f), new Vector2(-120f, 0f), new Vector2(100f, 42f));

                var sellBtn = UiFactory.CreateButton(row.transform, "SellBtn", $"SELL (+{item.SellPrice}g)", () =>
                {
                    string error;
                    if (_run.Merchant.TrySell(item.InstanceId, _run.Inventory, _run.Hud, out error))
                    {
                        SetStatus($"Sold equipped {item.Definition.Name} for +{item.SellPrice} gold!");
                    }
                    else
                    {
                        SetStatus(error, isError: true);
                    }
                });
                UiFactory.Place((RectTransform)sellBtn.transform, new Vector2(1f, 0.5f), new Vector2(-10f, 0f), new Vector2(100f, 42f));
            }

            _spawnedEquippedElements.Add(row);
        }

        private void RefreshBag()
        {
            ClearList(_spawnedBagElements);
            if (_run == null || _run.Inventory == null) return;

            var bag = _run.Inventory.Bag;
            if (_bagCapacityText != null)
                _bagCapacityText.text = $"INVENTORY BAG ({bag.Count}/{_run.Inventory.Capacity})";

            float cardHeight = 65f;
            float spacing = 8f;
            float startY = 120f;

            for (int i = 0; i < bag.Count && i < 4; i++) // Show top 4 bag items for clean layout
            {
                var item = bag[i];
                float y = startY - i * (cardHeight + spacing);
                CreateBagCard(item, y);
            }
        }

        private void CreateBagCard(ItemInstance item, float posY)
        {
            var card = new GameObject("Bag_" + item.InstanceId, typeof(RectTransform));
            card.transform.SetParent(_bagContainer, false);
            var rt = (RectTransform)card.transform;
            UiFactory.Place(rt, new Vector2(0.5f, 0.5f), new Vector2(0f, posY), new Vector2(620f, 60f));

            var img = card.AddComponent<Image>();
            img.color = new Color(0.14f, 0.16f, 0.22f, 0.92f);

            Color rarityCol = LootPickupActor.GetRarityColor(item.Definition.Rarity);
            var infoTxt = UiFactory.CreateText(card.transform, "Info", $"{item.Definition.Name} [{item.Definition.Slot}]: {item.Definition.Stats.GetSummary()}", 18, TextAnchor.MiddleLeft, rarityCol);
            UiFactory.Place(infoTxt.rectTransform, new Vector2(0f, 0.5f), new Vector2(15f, 0f), new Vector2(400f, 40f));

            var equipBtn = UiFactory.CreateButton(card.transform, "EquipBtn", "EQUIP", () =>
            {
                if (_run.Inventory.TryEquip(item.InstanceId))
                {
                    SetStatus($"Equipped {item.Definition.Name}!");
                }
            });
            UiFactory.Place((RectTransform)equipBtn.transform, new Vector2(1f, 0.5f), new Vector2(-120f, 0f), new Vector2(100f, 42f));

            var sellBtn = UiFactory.CreateButton(card.transform, "SellBtn", $"SELL (+{item.SellPrice}g)", () =>
            {
                string error;
                if (_run.Merchant.TrySell(item.InstanceId, _run.Inventory, _run.Hud, out error))
                {
                    SetStatus($"Sold {item.Definition.Name} for +{item.SellPrice} gold!");
                }
                else
                {
                    SetStatus(error, isError: true);
                }
            });
            UiFactory.Place((RectTransform)sellBtn.transform, new Vector2(1f, 0.5f), new Vector2(-10f, 0f), new Vector2(100f, 42f));

            _spawnedBagElements.Add(card);
        }

        private void OnRerollClicked()
        {
            if (_run == null || _run.Merchant == null || _run.Hud == null) return;
            if (_run.Merchant.TryReroll(_run.Seed, _run.Hud))
            {
                SetStatus("Merchant wares restocked!");
            }
            else
            {
                SetStatus($"Cannot reroll: Need {MerchantSystem.RerollCost} Gold!", isError: true);
            }
        }

        private void ClearList(List<GameObject> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != null) Destroy(list[i]);
            }
            list.Clear();
        }
    }
}
