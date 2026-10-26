using System;
using System.Text;
using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Procedural results view presenting end-of-run stats, calculating idempotent meta rewards,
    /// committing progress to local save, and providing navigation to Meta Upgrades and Main Menu.
    /// </summary>
    public sealed class ResultsView : MonoBehaviour
    {
        private GameObject _panel;
        private Text _titleText;
        private Text _statsText;
        private Text _rewardsText;
        private MetaUpgradesView _upgradesView;

        private RunSummary _currentSummary;
        private Action _onReturnToMenu;

        public bool IsOpen => _panel != null && _panel.activeSelf;
        public RunSummary CurrentSummary => _currentSummary;

        public void Initialize(Action onReturnToMenu)
        {
            _onReturnToMenu = onReturnToMenu;

            UiFactory.EnsureEventSystem();
            var canvas = UiFactory.CreateCanvas("ResultsCanvas", 40);
            canvas.transform.SetParent(transform, false);

            var backdrop = UiFactory.CreatePanel(canvas.transform, "Backdrop", new Color(0.04f, 0.02f, 0.05f, 0.95f));
            _panel = backdrop.gameObject;

            _titleText = UiFactory.CreateText(_panel.transform, "Title", "RUN CONCLUDED", 54, TextAnchor.MiddleCenter, new Color(1f, 0.35f, 0.35f));
            UiFactory.Place(_titleText.rectTransform, new Vector2(0.5f, 1f), new Vector2(0f, -50f), new Vector2(1000f, 70f));

            _statsText = UiFactory.CreateText(_panel.transform, "Stats", "", 24, TextAnchor.UpperLeft, Color.white);
            UiFactory.Place(_statsText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 60f), new Vector2(800f, 260f));

            _rewardsText = UiFactory.CreateText(_panel.transform, "Rewards", "", 24, TextAnchor.UpperLeft, new Color(0.85f, 0.7f, 1f));
            UiFactory.Place(_rewardsText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, -120f), new Vector2(800f, 120f));

            // Buttons
            var upgradesBtn = UiFactory.CreateButton(_panel.transform, "UpgradesBtn", "SOUL FORGE", OpenUpgrades);
            UiFactory.Place((RectTransform)upgradesBtn.transform, new Vector2(0.5f, 0f), new Vector2(-160f, 60f), new Vector2(280f, 60f));

            var menuBtn = UiFactory.CreateButton(_panel.transform, "MenuBtn", "MAIN MENU", () => _onReturnToMenu?.Invoke());
            UiFactory.Place((RectTransform)menuBtn.transform, new Vector2(0.5f, 0f), new Vector2(160f, 60f), new Vector2(280f, 60f));

            // Sub-view for persistent upgrades
            var upgGo = new GameObject("MetaUpgradesModal");
            upgGo.transform.SetParent(transform, false);
            _upgradesView = upgGo.AddComponent<MetaUpgradesView>();
            _upgradesView.Initialize();

            _panel.SetActive(false);
        }

        public void Show(RunSummary summary, PlayerSaveProfile profile)
        {
            _currentSummary = summary;

            // Idempotent save commit
            bool newlyCommitted = SaveStorage.CommitRunResult(summary, profile);

            if (_titleText != null)
                _titleText.text = summary.BossKills > 0 ? "CHAMPION OVERTHROWN - RUN END" : "FALLEN IN DUNGEON";

            int min = (int)(summary.Duration / 60f);
            int sec = (int)(summary.Duration % 60f);

            var sbStats = new StringBuilder();
            sbStats.AppendLine($"â€¢ Character: {summary.CharacterId.ToUpperInvariant()}");
            sbStats.AppendLine($"â€¢ Highest Wave Reached: {summary.WaveReached}");
            sbStats.AppendLine($"â€¢ Survival Time: {min:D2}:{sec:D2}");
            sbStats.AppendLine($"â€¢ Invaders Slain: {summary.Kills}");
            sbStats.AppendLine($"â€¢ Bosses Slain: {summary.BossKills}");
            sbStats.AppendLine($"â€¢ Total Score: {summary.Score:N0}");
            sbStats.AppendLine($"â€¢ In-Run Gold Collected: {summary.GoldCollected}");

            if (_statsText != null)
                _statsText.text = sbStats.ToString();

            int souls = summary.CalculateMetaSouls();
            int xp = summary.CalculateCharacterXp();
            var charRec = profile.GetOrAddCharacter(summary.CharacterId);

            var sbRewards = new StringBuilder();
            sbRewards.AppendLine("PERMANENT REWARDS EARNED:");
            sbRewards.AppendLine($"  â€¢ Ender Souls: +{souls} (Wallet: {profile.MetaWallet}) {(newlyCommitted ? "" : "[Already Claimed]")}");
            sbRewards.AppendLine($"  â€¢ Character XP: +{xp} (Current Level: {charRec.Level}, Skill Points: {charRec.AvailableSkillPoints})");

            if (_rewardsText != null)
                _rewardsText.text = sbRewards.ToString();

            _panel.SetActive(true);
        }

        private void OpenUpgrades()
        {
            var profile = GameSession.State.Profile ?? SaveStorage.Load();
            _upgradesView.Show(profile, () =>
            {
                // Refresh rewards text when returning from upgrades
                if (_currentSummary != null)
                {
                    Show(_currentSummary, profile);
                }
            });
        }
    }
}
