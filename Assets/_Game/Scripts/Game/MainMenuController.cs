using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Main menu and character select. Lists every catalog character; only playable ones are
    /// clickable, the rest are disabled and labelled "Coming later".
    /// Integrates Soul Forge persistent upgrades between runs.
    /// </summary>
    public sealed class MainMenuController : MonoBehaviour
    {
        private GameObject _mainPanel;
        private GameObject _selectPanel;
        private Text _detail;
        private Text _walletText;
        private Button _startButton;
        private Button _skillTreeButton;
        private MetaUpgradesView _upgradesView;
        private SkillTreeView _skillTreeView;
        private readonly Dictionary<string, Image> _characterButtons = new Dictionary<string, Image>();

        public MetaUpgradesView UpgradesView => _upgradesView;
        public SkillTreeView SkillTreeView => _skillTreeView;

        private void Start()
        {
            // Safe entry: whatever state we arrive in, clear leftover run data and show the menu.
            GameSession.State.ReturnToMenu();
            GameSession.State.Profile = SaveStorage.Load();
            Time.timeScale = 1f;
            BuildUi();
            ShowMain();
        }

        private void OnDestroy()
        {
            // UI lives in this scene only; nothing global was subscribed.
        }

        private void BuildUi()
        {
            UiFactory.EnsureEventSystem();
            var canvas = UiFactory.CreateCanvas("MenuCanvas", 0);
            UiFactory.CreatePanel(canvas.transform, "Background", new Color(0.07f, 0.06f, 0.1f, 1f));

            // Main panel
            var main = UiFactory.CreateRect("MainPanel", canvas.transform);
            UiFactory.Stretch(main);
            _mainPanel = main.gameObject;
            var title = UiFactory.CreateText(main, "Title", "FINAL BOSS: ETERNAL DUNGEON", 80, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(title.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 260f), new Vector2(1500f, 140f));

            _walletText = UiFactory.CreateText(main, "Wallet", $"Ender Souls: {GetWallet()}", 26, TextAnchor.MiddleRight, new Color(0.85f, 0.6f, 1f));
            UiFactory.Place(_walletText.rectTransform, new Vector2(1f, 1f), new Vector2(-60f, -40f), new Vector2(400f, 50f));

            var play = UiFactory.CreateButton(main, "PlayButton", "Play", OnPlayClicked);
            UiFactory.Place((RectTransform)play.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, 40f), new Vector2(420f, 85f));

            var forge = UiFactory.CreateButton(main, "ForgeButton", "Soul Forge (Upgrades)", OnForgeClicked);
            UiFactory.Place((RectTransform)forge.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, -60f), new Vector2(420f, 85f));

            var quit = UiFactory.CreateButton(main, "QuitButton", "Quit", OnQuitClicked);
            UiFactory.Place((RectTransform)quit.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, -160f), new Vector2(420f, 85f));

            // Character select panel
            var select = UiFactory.CreateRect("SelectPanel", canvas.transform);
            UiFactory.Stretch(select);
            _selectPanel = select.gameObject;
            var header = UiFactory.CreateText(select, "Header", "Choose your Final Boss", 64, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(header.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 400f), new Vector2(1500f, 100f));

            float y = 260f;
            foreach (var info in CharacterCatalog.Characters)
            {
                var captured = info;
                string label = info.Playable ? info.DisplayName : info.DisplayName + " - Coming later";
                var button = UiFactory.CreateButton(select, "Char_" + info.Id, label, () => OnCharacterClicked(captured.Id));
                button.interactable = info.Playable;
                UiFactory.Place((RectTransform)button.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, y), new Vector2(700f, 80f));
                _characterButtons[info.Id] = button.GetComponent<Image>();
                y -= 100f;
            }

            _detail = UiFactory.CreateText(select, "Detail", "Select a character.", 30, TextAnchor.MiddleCenter, new Color(0.85f, 0.85f, 0.9f));
            UiFactory.Place(_detail.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, -270f), new Vector2(1400f, 80f));

            _startButton = UiFactory.CreateButton(select, "StartButton", "Start Run", OnStartClicked);
            UiFactory.Place((RectTransform)_startButton.transform, new Vector2(0.5f, 0.5f), new Vector2(270f, -380f), new Vector2(240f, 90f));
            _startButton.interactable = false;

            _skillTreeButton = UiFactory.CreateButton(select, "SkillTreeButton", "Skill Tree", OnSkillTreeClicked);
            UiFactory.Place((RectTransform)_skillTreeButton.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, -380f), new Vector2(260f, 90f));
            _skillTreeButton.interactable = false;

            var back = UiFactory.CreateButton(select, "BackButton", "Back", OnBackClicked);
            UiFactory.Place((RectTransform)back.transform, new Vector2(0.5f, 0.5f), new Vector2(-270f, -380f), new Vector2(200f, 90f));

            // Persistent Upgrades Modal
            var forgeGo = new GameObject("MetaUpgradesModal");
            forgeGo.transform.SetParent(canvas.transform, false);
            _upgradesView = forgeGo.AddComponent<MetaUpgradesView>();
            _upgradesView.Initialize();

            // Skill Tree & Loadout Modal
            var skillTreeGo = new GameObject("SkillTreeModal");
            skillTreeGo.transform.SetParent(canvas.transform, false);
            _skillTreeView = skillTreeGo.AddComponent<SkillTreeView>();
            _skillTreeView.Initialize();
        }

        private int GetWallet()
        {
            return GameSession.State.Profile != null ? GameSession.State.Profile.MetaWallet : 0;
        }

        private void RefreshWallet()
        {
            if (_walletText != null)
                _walletText.text = $"Ender Souls: {GetWallet()}";
        }

        private void OnForgeClicked()
        {
            if (_upgradesView != null)
            {
                var profile = GameSession.State.Profile ?? SaveStorage.Load();
                _upgradesView.Show(profile, RefreshWallet);
            }
        }

        private void ShowMain()
        {
            _mainPanel.SetActive(true);
            _selectPanel.SetActive(false);
            RefreshWallet();
        }

        private void ShowSelect()
        {
            _mainPanel.SetActive(false);
            _selectPanel.SetActive(true);
            RefreshSelection();
        }

        private void RefreshSelection()
        {
            string selected = GameSession.State.SelectedCharacterId;
            foreach (var pair in _characterButtons)
                pair.Value.color = pair.Key == selected
                    ? new Color(0.35f, 0.6f, 0.35f, 1f)
                    : new Color(0.22f, 0.22f, 0.3f, 1f);

            var info = selected != null ? CharacterCatalog.Find(selected) : null;
            _detail.text = info != null
                ? info.DisplayName + "  |  Resource: " + info.ResourceName
                : "Select a character.";
            _startButton.interactable = info != null && info.Playable;
            if (_skillTreeButton != null)
                _skillTreeButton.interactable = info != null && info.Playable;
        }

        private void OnPlayClicked()
        {
            GameSession.State.OpenCharacterSelect();
            ShowSelect();
        }

        private void OnSkillTreeClicked()
        {
            if (_skillTreeView != null)
            {
                string charId = GameSession.State.SelectedCharacterId ?? CharacterCatalog.LichLordId;
                var profile = GameSession.State.Profile ?? SaveStorage.Load();
                _skillTreeView.Show(charId, profile, RefreshWallet);
            }
        }

        private void OnQuitClicked()
        {
            Application.Quit();
        }

        private void OnBackClicked()
        {
            GameSession.State.CloseCharacterSelect();
            ShowMain();
        }

        private void OnCharacterClicked(string id)
        {
            GameSession.State.TrySelectCharacter(id);
            RefreshSelection();
        }

        /// <summary>Starts a run with the selected character and loads the arena. Guarded against double clicks.</summary>
        public bool StartSelectedRun()
        {
            if (SceneFlow.IsLoading || !GameSession.State.TryStartRun())
                return false;
            _startButton.interactable = false;
            if (!SceneFlow.Load(SceneNames.GameArena))
            {
                GameSession.State.ReturnToMenu();
                return false;
            }
            return true;
        }

        private void OnStartClicked()
        {
            StartSelectedRun();
        }

        // Test hooks (public so play-mode tests can drive the same paths as the buttons).
        public void SelectCharacterForTest(string id) { OnCharacterClicked(id); }
        public void OpenSelectForTest() { OnPlayClicked(); }
        public void OpenSkillTreeForTest(string charId = null)
        {
            if (_skillTreeView != null)
            {
                string id = charId ?? GameSession.State.SelectedCharacterId ?? CharacterCatalog.LichLordId;
                _skillTreeView.Show(id, GameSession.State.Profile ?? SaveStorage.Load());
            }
        }
    }
}
