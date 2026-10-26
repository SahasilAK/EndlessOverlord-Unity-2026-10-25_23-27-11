using System;
using System.Collections.Generic;
using System.Text;
using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Controls endless discrete wave progression, queued staggered spawning, active concurrency caps,
    /// safe arena spawn positioning, reward selection gating, and intermission UI flow.
    /// </summary>
    public sealed class WaveDirector : MonoBehaviour
    {
        public const int MaxConcurrentEnemies = 12;
        public const float MinSpawnDistance = 7f;
        public const float SpawnEdgeMargin = 2.5f;

        private RunContext _run;
        private SplitMix64Rng _rng;
        private WaveGenerator _generator;
        private WaveLedger _ledger;

        private readonly Queue<InvaderSpawnDef> _spawnQueue = new Queue<InvaderSpawnDef>();
        private readonly List<InvaderActor> _activeInvaders = new List<InvaderActor>();
        private float _spawnCooldown;

        // Reward & Intermission UI
        private RewardModalView _rewardModal;
        private GameObject _intermissionPanel;
        private Text _bannerText;
        private Text _upgradesText;
        private Button _nextWaveButton;
        private GameObject _merchantPopup;
        private bool _isIntermission;

        public WaveLedger Ledger => _ledger;
        public int CurrentWave => _ledger != null ? _ledger.WaveNumber : 1;
        public bool IsIntermission => _isIntermission;
        public int ActiveInvaderCount => _activeInvaders.Count;
        public RewardModalView RewardModal => _rewardModal;

        /// <summary>Raised strictly once when a wave is cleared (all spawns exhausted AND all enemies dead).</summary>
        public event Action<int> WaveCompleted;

        public void Initialize(RunContext run)
        {
            _run = run;
            _rng = new SplitMix64Rng(run.Seed ^ 0x93C4A51BL);
            _generator = new WaveGenerator(run.Seed);
            _ledger = new WaveLedger();

            _ledger.WaveCleared += OnWaveCleared;

            BuildRewardModal();
            BuildIntermissionUi();
            StartWave(1);
        }

        private void OnDestroy()
        {
            if (_ledger != null)
            {
                _ledger.WaveCleared -= OnWaveCleared;
            }
            ClearAllEnemies();
        }

        private void BuildRewardModal()
        {
            var modalGo = new GameObject("RewardModal");
            modalGo.transform.SetParent(transform, false);
            _rewardModal = modalGo.AddComponent<RewardModalView>();
            _rewardModal.Initialize();
        }

        public void StartWave(int waveNumber)
        {
            _isIntermission = false;

            if (GameSession.Machine.Current == RunState.ChoosingReward ||
                GameSession.Machine.Current == RunState.Intermission ||
                GameSession.Machine.Current == RunState.RunSetup)
            {
                GameSession.Machine.TryTransition(RunState.PlayingWave);
            }

            if (_rewardModal != null)
                _rewardModal.Hide();

            if (_intermissionPanel != null)
                _intermissionPanel.SetActive(false);

            if (_merchantPopup != null)
                _merchantPopup.SetActive(false);

            ClearAllEnemies();
            _spawnQueue.Clear();

            var spec = _generator.Generate(waveNumber);
            for (int i = 0; i < spec.Spawns.Count; i++)
            {
                _spawnQueue.Enqueue(spec.Spawns[i]);
            }

            _spawnCooldown = 0.5f;
            _ledger.StartWave(waveNumber, spec.Spawns.Count);

            if (_run != null && _run.Hud != null)
            {
                _run.Hud.SetWave(waveNumber);
            }
        }

        private void Update()
        {
            if (_run == null || GameSession.Machine.Current != RunState.PlayingWave)
                return;

            if (_isIntermission)
                return;

            // Spawning loop
            if (_spawnQueue.Count > 0 && _activeInvaders.Count < MaxConcurrentEnemies)
            {
                _spawnCooldown -= Time.deltaTime;
                if (_spawnCooldown <= 0f)
                {
                    var def = _spawnQueue.Dequeue();
                    SpawnInvader(def);
                    _spawnCooldown = Math.Max(0.25f, def.DelayBeforeSpawn);
                }
            }
        }

        private void SpawnInvader(InvaderSpawnDef def)
        {
            var player = ActorRegistry.Player;
            Vector2 playerPos = player != null ? player.Position : Vector2.zero;
            Vector2 spawnPos = PickSpawnPoint(playerPos);

            var invader = InvaderActor.Spawn(spawnPos, def.Archetype, def.IsElite);
            invader.Killed += OnInvaderKilled;

            _activeInvaders.Add(invader);
            _ledger.TryRecordSpawn();
        }

        private Vector2 PickSpawnPoint(Vector2 playerPos)
        {
            float halfW = ArenaConfig.HalfWidth - SpawnEdgeMargin;
            float halfH = ArenaConfig.HalfHeight - SpawnEdgeMargin;

            for (int i = 0; i < 25; i++)
            {
                float x = (float)(_rng.NextDouble() * 2.0 - 1.0) * halfW;
                float y = (float)(_rng.NextDouble() * 2.0 - 1.0) * halfH;
                var pt = new Vector2(x, y);

                if ((pt - playerPos).sqrMagnitude >= MinSpawnDistance * MinSpawnDistance)
                    return pt;
            }

            // Guaranteed fallback: far quadrant
            return new Vector2(playerPos.x > 0f ? -halfW : halfW, playerPos.y > 0f ? -halfH : halfH);
        }

        private void OnInvaderKilled(InvaderActor actor)
        {
            _activeInvaders.Remove(actor);

            if (_run != null && _run.Hud != null)
            {
                _run.Hud.SetResource(_run.Hud.Resource + actor.SoulReward);
                _run.Hud.AddScore(actor.ScoreReward);
            }

            _ledger.TryRecordDeath();
        }

        private void OnWaveCleared(int waveNumber)
        {
            _isIntermission = true;
            WaveCompleted?.Invoke(waveNumber);

            // Flow: Transition to ChoosingReward and present reward draft
            GameSession.Machine.TryTransition(RunState.ChoosingReward);

            var offer = RewardDraftSystem.GenerateOffer(waveNumber, _run.Seed, _run.Rewards, _run.Character.Id);
            _rewardModal.Show(offer, _run.Rewards, () => OnRewardCommitted(waveNumber));
        }

        private void OnRewardCommitted(int waveNumber)
        {
            GameSession.Machine.TryTransition(RunState.Intermission);
            ShowIntermissionScreen(waveNumber);
        }

        private void ShowIntermissionScreen(int waveNumber)
        {
            if (_bannerText != null)
                _bannerText.text = $"WAVE {waveNumber} CLEARED!";

            UpdateUpgradesSummary();

            if (_intermissionPanel != null)
                _intermissionPanel.SetActive(true);
        }

        private void UpdateUpgradesSummary()
        {
            if (_upgradesText == null || _run == null || _run.Rewards == null)
                return;

            var sb = new StringBuilder();
            sb.AppendLine("ACTIVE UPGRADES:");
            var ranks = _run.Rewards.GetAllRanks();
            if (ranks.Count == 0)
            {
                sb.AppendLine("  (None acquired yet)");
            }
            else
            {
                foreach (var kvp in ranks)
                {
                    var def = RewardRegistry.GetById(kvp.Key);
                    string name = def != null ? def.Name : kvp.Key;
                    int max = def != null ? def.MaxRank : 5;
                    sb.AppendLine($"  â€¢ {name} [Rank {kvp.Value}/{max}]");
                }
            }

            _upgradesText.text = sb.ToString();
        }

        public void ClearAllEnemies()
        {
            for (int i = _activeInvaders.Count - 1; i >= 0; i--)
            {
                var inv = _activeInvaders[i];
                if (inv != null)
                {
                    inv.Killed -= OnInvaderKilled;
                    Destroy(inv.gameObject);
                }
            }
            _activeInvaders.Clear();
        }

        private void BuildIntermissionUi()
        {
            UiFactory.EnsureEventSystem();
            var canvas = UiFactory.CreateCanvas("WaveIntermissionCanvas", 25);
            canvas.transform.SetParent(transform, false);

            var panel = UiFactory.CreatePanel(canvas.transform, "IntermissionPanel", new Color(0.04f, 0.04f, 0.08f, 0.90f));
            _intermissionPanel = panel.gameObject;

            _bannerText = UiFactory.CreateText(panel.transform, "Banner", "WAVE CLEARED!", 64, TextAnchor.MiddleCenter, new Color(1f, 0.9f, 0.3f));
            UiFactory.Place(_bannerText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 260f), new Vector2(1000f, 100f));

            _upgradesText = UiFactory.CreateText(panel.transform, "UpgradesSummary", "", 24, TextAnchor.UpperLeft, new Color(0.85f, 0.9f, 1f));
            UiFactory.Place(_upgradesText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 80f), new Vector2(700f, 220f));

            _nextWaveButton = UiFactory.CreateButton(panel.transform, "NextWaveBtn", "NEXT WAVE", () => StartWave(CurrentWave + 1));
            UiFactory.Place((RectTransform)_nextWaveButton.transform, new Vector2(0.5f, 0.5f), new Vector2(-200f, -140f), new Vector2(280f, 80f));

            var jumpBossBtn = UiFactory.CreateButton(panel.transform, "JumpBossBtn", "DEV: JUMP WAVE 10", () => StartWave(10));
            UiFactory.Place((RectTransform)jumpBossBtn.transform, new Vector2(0.5f, 0.5f), new Vector2(120f, -140f), new Vector2(280f, 80f));

            var merchantBtn = UiFactory.CreateButton(panel.transform, "MerchantBtn", "MERCHANT", ToggleMerchantPopup);
            UiFactory.Place((RectTransform)merchantBtn.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, -240f), new Vector2(280f, 60f));

            // Merchant popup
            var merchantPanel = UiFactory.CreatePanel(panel.transform, "MerchantPopup", new Color(0.1f, 0.08f, 0.16f, 0.95f));
            _merchantPopup = merchantPanel.gameObject;
            UiFactory.Place(merchantPanel.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), new Vector2(600f, 300f));

            var mTitle = UiFactory.CreateText(_merchantPopup.transform, "MTitle", "DUNGEON MERCHANT", 32, TextAnchor.MiddleCenter, new Color(1f, 0.8f, 0.2f));
            UiFactory.Place(mTitle.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 80f), new Vector2(500f, 60f));

            var mBody = UiFactory.CreateText(_merchantPopup.transform, "MBody", "The Merchant is traveling between dimensions.\nFull inventory arriving in Phase P7.", 22, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(mBody.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 0f), new Vector2(500f, 100f));

            var mClose = UiFactory.CreateButton(_merchantPopup.transform, "MClose", "CLOSE", () => _merchantPopup.SetActive(false));
            UiFactory.Place((RectTransform)mClose.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, -80f), new Vector2(180f, 50f));

            _merchantPopup.SetActive(false);
            _intermissionPanel.SetActive(false);
        }

        private void ToggleMerchantPopup()
        {
            if (_merchantPopup != null)
                _merchantPopup.SetActive(!_merchantPopup.activeSelf);
        }
    }
}
