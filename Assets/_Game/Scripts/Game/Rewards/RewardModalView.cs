using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Procedural modal dialog presenting end-of-wave reward choices (1-of-3 or rare 2-of-4).
    /// Enforces atomic selection, prevents duplicate spending, and displays accurate rank progress.
    /// </summary>
    public sealed class RewardModalView : MonoBehaviour
    {
        private GameObject _panel;
        private Text _titleText;
        private Text _subtitleText;
        private Transform _cardContainer;
        private readonly List<GameObject> _activeCardCards = new List<GameObject>();

        private RewardOffer _currentOffer;
        private RewardInventory _inventory;
        private Action _onCommitted;

        public bool IsOpen => _panel != null && _panel.activeSelf;
        public RewardOffer CurrentOffer => _currentOffer;

        public void Initialize()
        {
            UiFactory.EnsureEventSystem();
            var canvas = UiFactory.CreateCanvas("RewardModalCanvas", 30);
            canvas.transform.SetParent(transform, false);

            var panelImg = UiFactory.CreatePanel(canvas.transform, "ModalBackdrop", new Color(0.02f, 0.02f, 0.05f, 0.92f));
            _panel = panelImg.gameObject;

            _titleText = UiFactory.CreateText(_panel.transform, "Title", "WAVE REWARD", 58, TextAnchor.MiddleCenter, new Color(1f, 0.85f, 0.3f));
            UiFactory.Place(_titleText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 340f), new Vector2(1000f, 80f));

            _subtitleText = UiFactory.CreateText(_panel.transform, "Subtitle", "Choose 1 power to absorb", 28, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(_subtitleText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 270f), new Vector2(900f, 50f));

            var containerGo = new GameObject("CardContainer", typeof(RectTransform));
            containerGo.transform.SetParent(_panel.transform, false);
            _cardContainer = containerGo.transform;
            UiFactory.Place((RectTransform)_cardContainer, new Vector2(0.5f, 0.5f), new Vector2(0f, -20f), new Vector2(1600f, 500f));

            _panel.SetActive(false);
        }

        public void Show(RewardOffer offer, RewardInventory inventory, Action onCommitted)
        {
            _currentOffer = offer;
            _inventory = inventory;
            _onCommitted = onCommitted;

            ClearCards();

            if (_titleText != null)
                _titleText.text = $"WAVE {offer.WaveNumber} REWARDS";

            UpdateSubtitle();

            int count = offer.Choices.Count;
            float cardWidth = count > 3 ? 340f : 400f;
            float spacing = 30f;
            float totalWidth = count * cardWidth + (count - 1) * spacing;
            float startX = -totalWidth * 0.5f + cardWidth * 0.5f;

            for (int i = 0; i < count; i++)
            {
                var def = offer.Choices[i];
                float x = startX + i * (cardWidth + spacing);
                CreateCard(def, x, cardWidth);
            }

            _panel.SetActive(true);
        }

        private void UpdateSubtitle()
        {
            if (_subtitleText == null || _currentOffer == null)
                return;

            if (_currentOffer.PicksAllowed > 1)
            {
                _subtitleText.text = $"SPECIAL DRAFT: Choose {_currentOffer.PicksRemaining} of {_currentOffer.Choices.Count} (Picks remaining: {_currentOffer.PicksRemaining})";
            }
            else
            {
                _subtitleText.text = "Choose 1 upgrade to strengthen your Lich Lord";
            }
        }

        private void CreateCard(RewardDefinition def, float posX, float width)
        {
            var cardGo = new GameObject("Card_" + def.Id, typeof(RectTransform));
            cardGo.transform.SetParent(_cardContainer, false);
            var cardRt = (RectTransform)cardGo.transform;
            UiFactory.Place(cardRt, new Vector2(0.5f, 0.5f), new Vector2(posX, 0f), new Vector2(width, 460f));

            Color rarityCol = GetRarityColor(def.Rarity);

            // Card background border
            var bgImg = cardGo.AddComponent<Image>();
            bgImg.color = new Color(0.12f, 0.13f, 0.18f, 0.95f);

            // Rarity banner at top
            var bannerGo = new GameObject("Banner", typeof(RectTransform));
            bannerGo.transform.SetParent(cardGo.transform, false);
            var bannerRt = (RectTransform)bannerGo.transform;
            UiFactory.Place(bannerRt, new Vector2(0.5f, 1f), new Vector2(0f, -25f), new Vector2(width - 16f, 44f));
            var bannerImg = bannerGo.AddComponent<Image>();
            bannerImg.color = rarityCol * 0.35f;

            var rarityText = UiFactory.CreateText(bannerGo.transform, "RarityText", def.Rarity.ToString().ToUpperInvariant(), 22, TextAnchor.MiddleCenter, rarityCol);
            UiFactory.Stretch(rarityText.rectTransform);

            // Title
            var nameText = UiFactory.CreateText(cardGo.transform, "Name", def.Name, 26, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(nameText.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -85f), new Vector2(width - 24f, 60f));

            // Rank indicator
            int curRank = _inventory.GetRank(def.Id);
            string rankStr = curRank >= def.MaxRank ? "[MAX RANK]" : $"Rank {curRank} -> {curRank + 1} / {def.MaxRank}";
            var rankText = UiFactory.CreateText(cardGo.transform, "Rank", rankStr, 20, TextAnchor.MiddleCenter, new Color(0.8f, 0.8f, 0.8f));
            UiFactory.Place(rankText.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -145f), new Vector2(width - 24f, 35f));

            // Description / Tooltip
            string tooltip = def.GetTooltip(curRank);
            var descText = UiFactory.CreateText(cardGo.transform, "Desc", tooltip, 20, TextAnchor.MiddleCenter, new Color(0.9f, 0.95f, 1f));
            UiFactory.Place(descText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, -20f), new Vector2(width - 32f, 150f));

            // Select button
            Button selectBtn = null;
            selectBtn = UiFactory.CreateButton(cardGo.transform, "SelectBtn", "ABSORB", () =>
            {
                OnCardPicked(def, selectBtn);
            });
            UiFactory.Place((RectTransform)selectBtn.transform, new Vector2(0.5f, 0f), new Vector2(0f, 45f), new Vector2(width - 40f, 60f));

            _activeCardCards.Add(cardGo);
        }

        private void OnCardPicked(RewardDefinition def, Button btn)
        {
            if (_currentOffer == null || _currentOffer.IsCommitted)
                return;

            bool picked = _currentOffer.TryPick(def.Id, _inventory);
            if (!picked)
                return;

            // Mark button as claimed
            btn.interactable = false;
            var txt = btn.GetComponentInChildren<Text>();
            if (txt != null) txt.text = "[CLAIMED]";

            UpdateSubtitle();

            if (_currentOffer.IsCommitted)
            {
                Hide();
                _onCommitted?.Invoke();
            }
        }

        public void Hide()
        {
            if (_panel != null)
                _panel.SetActive(false);
            ClearCards();
        }

        private void ClearCards()
        {
            for (int i = 0; i < _activeCardCards.Count; i++)
            {
                if (_activeCardCards[i] != null)
                    Destroy(_activeCardCards[i]);
            }
            _activeCardCards.Clear();
        }

        public static Color GetRarityColor(RewardRarity rarity)
        {
            switch (rarity)
            {
                case RewardRarity.Common: return new Color(0.75f, 0.8f, 0.85f);
                case RewardRarity.Uncommon: return new Color(0.3f, 0.85f, 0.4f);
                case RewardRarity.Rare: return new Color(0.25f, 0.7f, 1.0f);
                case RewardRarity.Epic: return new Color(0.78f, 0.4f, 0.98f);
                case RewardRarity.Mythic: return new Color(1.0f, 0.82f, 0.2f);
                default: return Color.white;
            }
        }
    }
}
