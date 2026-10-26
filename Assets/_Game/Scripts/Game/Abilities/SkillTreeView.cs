using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Procedural uGUI modal for managing per-character skill trees, 5-slot active loadouts,
    /// point spending, refunds/respecs, autocast toggling, and ability rank mutations.
    /// </summary>
    public sealed class SkillTreeView : MonoBehaviour
    {
        private GameObject _panel;
        private Text _headerText;
        private Text _statusText;
        private Transform _loadoutContainer;
        private Transform _abilitiesContainer;
        private readonly List<GameObject> _spawnedCards = new List<GameObject>();
        private readonly List<GameObject> _spawnedSlots = new List<GameObject>();

        private string _characterId;
        private PlayerSaveProfile _profile;
        private Action _onClosed;

        public bool IsOpen => _panel != null && _panel.activeSelf;
        public string ActiveCharacterId => _characterId;

        public void Initialize()
        {
            UiFactory.EnsureEventSystem();
            var canvas = UiFactory.CreateCanvas("SkillTreeCanvas", 46);
            canvas.transform.SetParent(transform, false);

            var backdrop = UiFactory.CreatePanel(canvas.transform, "Backdrop", new Color(0.04f, 0.03f, 0.08f, 0.96f));
            _panel = backdrop.gameObject;

            var title = UiFactory.CreateText(_panel.transform, "Title", "SKILL TREE & 5-SLOT LOADOUT", 40, TextAnchor.MiddleCenter, new Color(1f, 0.85f, 0.35f));
            UiFactory.Place(title.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -30f), new Vector2(1200f, 50f));

            _headerText = UiFactory.CreateText(_panel.transform, "Header", "", 24, TextAnchor.MiddleCenter, new Color(0.85f, 0.9f, 1f));
            UiFactory.Place(_headerText.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -75f), new Vector2(1200f, 35f));

            _statusText = UiFactory.CreateText(_panel.transform, "Status", "", 20, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(_statusText.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -110f), new Vector2(1200f, 30f));

            // Loadout row
            var loadoutRowGo = new GameObject("LoadoutRow", typeof(RectTransform));
            loadoutRowGo.transform.SetParent(_panel.transform, false);
            _loadoutContainer = loadoutRowGo.transform;
            UiFactory.Place((RectTransform)_loadoutContainer, new Vector2(0.5f, 1f), new Vector2(0f, -170f), new Vector2(1200f, 75f));

            // Abilities container
            var containerGo = new GameObject("AbilitiesContainer", typeof(RectTransform));
            containerGo.transform.SetParent(_panel.transform, false);
            _abilitiesContainer = containerGo.transform;
            UiFactory.Place((RectTransform)_abilitiesContainer, new Vector2(0.5f, 0.5f), new Vector2(0f, -60f), new Vector2(1200f, 480f));

            // Respec Button
            var respecBtn = UiFactory.CreateButton(_panel.transform, "RespecBtn", "RESET & REFUND POINTS", OnRespecClicked);
            UiFactory.Place((RectTransform)respecBtn.transform, new Vector2(0.5f, 0f), new Vector2(-180f, 40f), new Vector2(300f, 55f));

            // Close Button
            var closeBtn = UiFactory.CreateButton(_panel.transform, "CloseBtn", "BACK", Close);
            UiFactory.Place((RectTransform)closeBtn.transform, new Vector2(0.5f, 0f), new Vector2(180f, 40f), new Vector2(260f, 55f));

            _panel.SetActive(false);
        }

        public void Show(string characterId, PlayerSaveProfile profile, Action onClosed = null)
        {
            _characterId = !string.IsNullOrEmpty(characterId) ? characterId : CharacterCatalog.LichLordId;
            _profile = profile ?? SaveStorage.Load();
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
            if (_profile == null) return;

            var charRec = _profile.GetOrAddCharacter(_characterId);
            charRec.EnsureInitialized();

            var charInfo = CharacterCatalog.Get(_characterId);
            string charName = charInfo != null ? charInfo.DisplayName : _characterId;

            if (_headerText != null)
            {
                _headerText.text = $"{charName} | Level {charRec.Level} | Available Points: {charRec.AvailableSkillPoints} (Spent: {charRec.SpentSkillPoints} / Earned: {charRec.EarnedSkillPoints})";
            }

            RefreshLoadoutRow(charRec);
            RefreshAbilitiesList(charRec);
        }

        private void RefreshLoadoutRow(CharacterRecord charRec)
        {
            for (int i = 0; i < _spawnedSlots.Count; i++)
            {
                if (_spawnedSlots[i] != null) Destroy(_spawnedSlots[i]);
            }
            _spawnedSlots.Clear();

            float slotWidth = 220f;
            float spacing = 15f;
            float startX = -((slotWidth + spacing) * 2f);

            for (int s = 0; s < AbilityLoadout.SlotCount; s++)
            {
                int slotIndex = s;
                string abilityId = s < charRec.EquippedLoadout.Count ? charRec.EquippedLoadout[s] : string.Empty;
                var def = AbilityRegistry.Get(abilityId);
                string abilityName = def != null ? def.Name : "[Empty]";
                bool isAuto = s < charRec.AutocastSettings.Count && charRec.AutocastSettings[s];

                var slotGo = new GameObject("Slot_" + (s + 1), typeof(RectTransform));
                slotGo.transform.SetParent(_loadoutContainer, false);
                _spawnedSlots.Add(slotGo);

                var rt = (RectTransform)slotGo.transform;
                rt.anchorMin = new Vector2(0.5f, 0.5f);
                rt.anchorMax = new Vector2(0.5f, 0.5f);
                rt.anchoredPosition = new Vector2(startX + s * (slotWidth + spacing), 0f);
                rt.sizeDelta = new Vector2(slotWidth, 70f);

                var bg = slotGo.AddComponent<Image>();
                bg.color = def != null ? new Color(0.12f, 0.16f, 0.25f, 0.95f) : new Color(0.08f, 0.08f, 0.12f, 0.8f);

                string keyLabel = s == 0 ? "1/E" : (s + 1).ToString();
                var label = UiFactory.CreateText(slotGo.transform, "Label", $"SLOT {keyLabel}\n{abilityName}", 16, TextAnchor.MiddleCenter, Color.white);
                UiFactory.Place(label.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 10f), new Vector2(slotWidth - 10f, 40f));

                // Auto toggle button
                if (def != null && def.EligibleForAutocast)
                {
                    string autoText = isAuto ? "AUTO: ON" : "AUTO: OFF";
                    Color autoCol = isAuto ? new Color(0.3f, 0.9f, 0.4f) : new Color(0.6f, 0.6f, 0.6f);
                    var autoBtn = UiFactory.CreateButton(slotGo.transform, "AutoBtn", autoText, () => OnToggleAutocast(slotIndex));
                    UiFactory.Place((RectTransform)autoBtn.transform, new Vector2(0.5f, 0f), new Vector2(-40f, 15f), new Vector2(100f, 24f));
                    var btnText = autoBtn.GetComponentInChildren<Text>();
                    if (btnText != null) { btnText.fontSize = 13; btnText.color = autoCol; }
                }

                // Unequip button
                if (def != null)
                {
                    var unequipBtn = UiFactory.CreateButton(slotGo.transform, "UnequipBtn", "X", () => OnUnequipSlot(slotIndex));
                    UiFactory.Place((RectTransform)unequipBtn.transform, new Vector2(1f, 1f), new Vector2(-15f, -15f), new Vector2(24f, 24f));
                    var btnText = unequipBtn.GetComponentInChildren<Text>();
                    if (btnText != null) { btnText.fontSize = 14; btnText.color = new Color(1f, 0.4f, 0.4f); }
                }
            }
        }

        private void RefreshAbilitiesList(CharacterRecord charRec)
        {
            for (int i = 0; i < _spawnedCards.Count; i++)
            {
                if (_spawnedCards[i] != null) Destroy(_spawnedCards[i]);
            }
            _spawnedCards.Clear();

            var abilities = AbilityRegistry.GetForCharacter(_characterId);
            float cardHeight = 100f;
            float spacing = 12f;
            float startY = 180f;

            for (int i = 0; i < abilities.Count; i++)
            {
                var def = abilities[i];
                int rank = CharacterSkillTree.GetAbilityRank(charRec, def.Id);
                var progress = CharacterSkillTree.GetAbilityProgress(charRec, def.Id);
                bool isUnlocked = rank > 0;

                var cardGo = new GameObject("Card_" + def.Id, typeof(RectTransform));
                cardGo.transform.SetParent(_abilitiesContainer, false);
                _spawnedCards.Add(cardGo);

                var rt = (RectTransform)cardGo.transform;
                rt.anchorMin = new Vector2(0.5f, 0.5f);
                rt.anchorMax = new Vector2(0.5f, 0.5f);
                rt.anchoredPosition = new Vector2(0f, startY - i * (cardHeight + spacing));
                rt.sizeDelta = new Vector2(1150f, cardHeight);

                var bg = cardGo.AddComponent<Image>();
                bg.color = isUnlocked ? new Color(0.12f, 0.15f, 0.22f, 0.95f) : new Color(0.07f, 0.07f, 0.10f, 0.85f);

                string rankText = isUnlocked
                    ? (progress.AscensionRank > 0 ? $"Rank {rank} (Asc {progress.AscensionRank})" : $"Rank {rank}/{def.MaxRank}")
                    : "LOCKED";
                Color rankColor = isUnlocked ? new Color(0.3f, 0.9f, 0.4f) : new Color(0.7f, 0.4f, 0.4f);

                float effCost = AbilityProgressionMath.CalculateEffectiveResourceCost(def, rank, progress.AscensionRank);
                float effCd = AbilityProgressionMath.CalculateEffectiveCooldown(def, rank, progress.AscensionRank);

                string titleStr = $"{def.Name} [{rankText}] - {def.BranchName}";
                string statsStr = $"Cost: {effCost:0} | CD: {effCd:0.1}s | Range: {def.Range:0}m | Mastery XP: {progress.MasteryXp}/{AbilityProgressionMath.GetXpRequiredForNextRank(rank)}";
                string descStr = def.Description;

                var txt = UiFactory.CreateText(cardGo.transform, "Info", $"{titleStr}\n{statsStr}\n{descStr}", 15, TextAnchor.MiddleLeft, Color.white);
                UiFactory.Place(txt.rectTransform, new Vector2(0f, 0.5f), new Vector2(320f, 0f), new Vector2(600f, 90f));

                // Upgrade / Unlock Button
                bool canUnlock = CharacterSkillTree.CanUnlock(def.Id, charRec, out string error);
                string btnLabel = isUnlocked ? "UPGRADE (1 Pt)" : "UNLOCK (1 Pt)";
                var upgradeBtn = UiFactory.CreateButton(cardGo.transform, "UpgradeBtn", btnLabel, () => OnUpgradeClicked(def.Id));
                upgradeBtn.interactable = canUnlock;
                UiFactory.Place((RectTransform)upgradeBtn.transform, new Vector2(1f, 0.5f), new Vector2(-200f, 18f), new Vector2(170f, 45f));

                if (!canUnlock && !string.IsNullOrEmpty(error))
                {
                    var errText = UiFactory.CreateText(cardGo.transform, "Err", error, 12, TextAnchor.MiddleRight, new Color(1f, 0.5f, 0.5f));
                    UiFactory.Place(errText.rectTransform, new Vector2(1f, 0.5f), new Vector2(-200f, -22f), new Vector2(240f, 25f));
                }

                // Equip to slots buttons
                if (isUnlocked)
                {
                    for (int s = 0; s < AbilityLoadout.SlotCount; s++)
                    {
                        int targetSlot = s;
                        bool isCurrentSlot = targetSlot < charRec.EquippedLoadout.Count && charRec.EquippedLoadout[targetSlot] == def.Id;
                        string sLabel = isCurrentSlot ? $"[S{s + 1}]" : $"S{s + 1}";
                        var equipBtn = UiFactory.CreateButton(cardGo.transform, $"EquipS{s + 1}", sLabel, () => OnEquipClicked(def.Id, targetSlot));
                        UiFactory.Place((RectTransform)equipBtn.transform, new Vector2(1f, 0.5f), new Vector2(-80f + s * 34f - (AbilityLoadout.SlotCount - 1) * 17f, -18f), new Vector2(30f, 32f));
                        var eTxt = equipBtn.GetComponentInChildren<Text>();
                        if (eTxt != null)
                        {
                            eTxt.fontSize = 11;
                            if (isCurrentSlot) eTxt.color = new Color(1f, 0.85f, 0.3f);
                        }
                    }
                }
            }
        }

        private void OnUpgradeClicked(string abilityId)
        {
            if (_profile == null) return;
            var charRec = _profile.GetOrAddCharacter(_characterId);

            if (CharacterSkillTree.TrySpendPoint(abilityId, ref charRec, out string error))
            {
                _profile.SaveCharacter(charRec);
                SaveStorage.SaveCurrentProfile();
                SetStatus($"Successfully upgraded {AbilityRegistry.Get(abilityId)?.Name}!");
                Refresh();
            }
            else
            {
                SetStatus(error, isError: true);
            }
        }

        private void OnEquipClicked(string abilityId, int slotIndex)
        {
            if (_profile == null) return;
            var charRec = _profile.GetOrAddCharacter(_characterId);
            charRec.EnsureInitialized();

            var loadout = new AbilityLoadout(charRec.EquippedLoadout, charRec.AutocastSettings);
            if (loadout.TryEquip(slotIndex, abilityId, id => CharacterSkillTree.GetAbilityRank(charRec, id) > 0, out string rejection))
            {
                charRec.EquippedLoadout = new List<string>(loadout.Slots);
                _profile.SaveCharacter(charRec);
                SaveStorage.SaveCurrentProfile();
                SetStatus($"Equipped {AbilityRegistry.Get(abilityId)?.Name} to Slot {slotIndex + 1}!");
                Refresh();
            }
            else
            {
                SetStatus(rejection, isError: true);
            }
        }

        private void OnUnequipSlot(int slotIndex)
        {
            if (_profile == null) return;
            var charRec = _profile.GetOrAddCharacter(_characterId);
            charRec.EnsureInitialized();

            var loadout = new AbilityLoadout(charRec.EquippedLoadout, charRec.AutocastSettings);
            if (loadout.TryUnequip(slotIndex))
            {
                charRec.EquippedLoadout = new List<string>(loadout.Slots);
                _profile.SaveCharacter(charRec);
                SaveStorage.SaveCurrentProfile();
                SetStatus($"Unequipped Slot {slotIndex + 1}.");
                Refresh();
            }
        }

        private void OnToggleAutocast(int slotIndex)
        {
            if (_profile == null) return;
            var charRec = _profile.GetOrAddCharacter(_characterId);
            charRec.EnsureInitialized();

            var loadout = new AbilityLoadout(charRec.EquippedLoadout, charRec.AutocastSettings);
            loadout.ToggleAutocast(slotIndex);

            charRec.AutocastSettings = new List<bool>(loadout.AutocastSettings);
            _profile.SaveCharacter(charRec);
            SaveStorage.SaveCurrentProfile();
            Refresh();
        }

        private void OnRespecClicked()
        {
            if (_profile == null) return;
            var charRec = _profile.GetOrAddCharacter(_characterId);

            CharacterSkillTree.Respec(ref charRec);
            _profile.SaveCharacter(charRec);
            SaveStorage.SaveCurrentProfile();

            SetStatus("Skill points refunded! (Mastery XP preserved)", isError: false);
            Refresh();
        }
    }
}
