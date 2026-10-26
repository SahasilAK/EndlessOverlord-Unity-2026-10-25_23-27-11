using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Procedural uGUI modal for purchasing permanent between-run upgrades.
    /// Modifies starting player stats for all future runs and persists changes locally.
    /// </summary>
    public sealed class MetaUpgradesView : MonoBehaviour
    {
        private GameObject _panel;
        private Text _walletText;
        private Text _statusText;
        private Transform _cardsContainer;
        private readonly List<GameObject> _spawnedCards = new List<GameObject>();

        private PlayerSaveProfile _profile;
        private Action _onClosed;

        public bool IsOpen => _panel != null && _panel.activeSelf;

        public void Initialize()
        {
            UiFactory.EnsureEventSystem();
            var canvas = UiFactory.CreateCanvas("MetaUpgradesCanvas", 45);
            canvas.transform.SetParent(transform, false);

            var backdrop = UiFactory.CreatePanel(canvas.transform, "Backdrop", new Color(0.04f, 0.02f, 0.08f, 0.95f));
            _panel = backdrop.gameObject;

            var title = UiFactory.CreateText(_panel.transform, "Title", "PERMANENT SOUL FORGE", 46, TextAnchor.MiddleCenter, new Color(1f, 0.85f, 0.3f));
            UiFactory.Place(title.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -40f), new Vector2(1000f, 60f));

            _walletText = UiFactory.CreateText(_panel.transform, "Wallet", "Ender Souls: 0", 28, TextAnchor.MiddleRight, new Color(0.85f, 0.6f, 1f));
            UiFactory.Place(_walletText.rectTransform, new Vector2(1f, 1f), new Vector2(-60f, -40f), new Vector2(400f, 50f));

            _statusText = UiFactory.CreateText(_panel.transform, "Status", "", 22, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(_statusText.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -95f), new Vector2(1000f, 40f));

            var containerGo = new GameObject("CardsContainer", typeof(RectTransform));
            containerGo.transform.SetParent(_panel.transform, false);
            _cardsContainer = containerGo.transform;
            UiFactory.Place((RectTransform)_cardsContainer, new Vector2(0.5f, 0.5f), new Vector2(0f, -10f), new Vector2(1000f, 500f));

            var closeBtn = UiFactory.CreateButton(_panel.transform, "CloseBtn", "BACK", Close);
            UiFactory.Place((RectTransform)closeBtn.transform, new Vector2(0.5f, 0f), new Vector2(0f, 50f), new Vector2(280f, 60f));

            _panel.SetActive(false);
        }

        public void Show(PlayerSaveProfile profile, Action onClosed = null)
        {
            _profile = profile;
            _onClosed = onClosed;

            _panel.SetActive(true);
            SetStatus("");
            Refresh();
        }

        public void Close()
        {
            if (_panel != null)
                _panel.SetActive(false);
            _onClosed?.Invoke();
        }

        private void SetStatus(string message, bool isError = false)
        {
            if (_statusText == null) return;
            _statusText.text = message;
            _statusText.color = isError ? new Color(1f, 0.4f, 0.4f) : new Color(0.5f, 1f, 0.6f);
        }

        public void Refresh()
        {
            if (_walletText != null && _profile != null)
                _walletText.text = $"Ender Souls: {_profile.MetaWallet}";

            for (int i = 0; i < _spawnedCards.Count; i++)
            {
                if (_spawnedCards[i] != null) Destroy(_spawnedCards[i]);
            }
            _spawnedCards.Clear();

            var upgrades = new[]
            {
                PersistentUpgrades.SoulVitalityId,
                PersistentUpgrades.DeathPotencyId,
                PersistentUpgrades.SpectralArmorId,
                PersistentUpgrades.SwiftStrideId
            };

            float cardHeight = 95f;
            float spacing = 15f;
            float startY = 160f;

            for (int i = 0; i < upgrades.Length; i++)
            {
                string id = upgrades[i];
                float y = startY - i * (cardHeight + spacing);
                CreateUpgradeCard(id, y);
            }
        }

        private void CreateUpgradeCard(string id, float posY)
        {
            var card = new GameObject("Upgrade_" + id, typeof(RectTransform));
            card.transform.SetParent(_cardsContainer, false);
            var rt = (RectTransform)card.transform;
            UiFactory.Place(rt, new Vector2(0.5f, 0.5f), new Vector2(0f, posY), new Vector2(960f, 90f));

            var img = card.AddComponent<Image>();
            img.color = new Color(0.12f, 0.10f, 0.18f, 0.95f);

            int currentRank = _profile != null ? _profile.GetUpgradeRank(id) : 0;
            int maxRank = 5;
            int cost = PersistentUpgrades.GetUpgradeCost(id, currentRank);
            string name = PersistentUpgrades.GetUpgradeName(id);
            string effect = PersistentUpgrades.GetUpgradeEffect(id);

            // Name & Rank
            string rankStr = currentRank >= maxRank ? "[MAX RANK]" : $"Rank {currentRank} / {maxRank}";
            var nameTxt = UiFactory.CreateText(card.transform, "Name", $"{name} ({rankStr})", 24, TextAnchor.MiddleLeft, new Color(1f, 0.9f, 0.4f));
            UiFactory.Place(nameTxt.rectTransform, new Vector2(0f, 0.5f), new Vector2(25f, 16f), new Vector2(500f, 35f));

            // Description
            var effectTxt = UiFactory.CreateText(card.transform, "Effect", effect, 20, TextAnchor.MiddleLeft, Color.white);
            UiFactory.Place(effectTxt.rectTransform, new Vector2(0f, 0.5f), new Vector2(25f, -16f), new Vector2(500f, 30f));

            // Purchase Button
            if (currentRank >= maxRank)
            {
                var maxTxt = UiFactory.CreateText(card.transform, "Maxed", "MAXED", 22, TextAnchor.MiddleCenter, new Color(0.6f, 0.6f, 0.6f));
                UiFactory.Place(maxTxt.rectTransform, new Vector2(1f, 0.5f), new Vector2(-30f, 0f), new Vector2(180f, 50f));
            }
            else
            {
                var buyBtn = UiFactory.CreateButton(card.transform, "BuyBtn", $"UPGRADE ({cost}s)", () =>
                {
                    if (_profile.TryPurchaseUpgrade(id, cost))
                    {
                        SaveStorage.Save(_profile);
                        SetStatus($"Upgraded {name} to Rank {currentRank + 1}!");
                        Refresh();
                    }
                    else
                    {
                        SetStatus($"Insufficient Ender Souls! Need {cost}.", isError: true);
                    }
                });
                UiFactory.Place((RectTransform)buyBtn.transform, new Vector2(1f, 0.5f), new Vector2(-30f, 0f), new Vector2(200f, 52f));
            }

            _spawnedCards.Add(card);
        }
    }
}
