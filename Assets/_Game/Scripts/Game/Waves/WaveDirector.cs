using System;
using System.Collections.Generic;
using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Controls endless discrete wave progression, queued staggered spawning, active concurrency caps,
    /// safe arena spawn positioning, and wave completion invariants.
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

        // Intermission / Dev Controls
        private GameObject _intermissionPanel;
        private Text _bannerText;
        private Text _countdownText;
        private Button _nextWaveButton;
        private float _autoAdvanceTimer;
        private bool _isIntermission;

        public WaveLedger Ledger => _ledger;
        public int CurrentWave => _ledger != null ? _ledger.WaveNumber : 1;
        public bool IsIntermission => _isIntermission;
        public int ActiveInvaderCount => _activeInvaders.Count;

        /// <summary>Raised strictly once when a wave is cleared (all spawns exhausted AND all enemies dead).</summary>
        public event Action<int> WaveCompleted;

        public void Initialize(RunContext run)
        {
            _run = run;
            _rng = new SplitMix64Rng(run.Seed ^ 0x93C4A51BL);
            _generator = new WaveGenerator(run.Seed);
            _ledger = new WaveLedger();

            _ledger.WaveCleared += OnWaveCleared;
            _ledger.Changed += OnLedgerChanged;

            BuildIntermissionUi();
            StartWave(1);
        }

        private void OnDestroy()
        {
            if (_ledger != null)
            {
                _ledger.WaveCleared -= OnWaveCleared;
                _ledger.Changed -= OnLedgerChanged;
            }
            ClearAllEnemies();
        }

        public void StartWave(int waveNumber)
        {
            _isIntermission = false;
            if (_intermissionPanel != null)
                _intermissionPanel.SetActive(false);

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
            {
                if (_autoAdvanceTimer > 0f)
                {
                    _autoAdvanceTimer -= Time.deltaTime;
                    if (_countdownText != null)
                        _countdownText.text = $"Next wave in {_autoAdvanceTimer:0.0}s (or click below)";

                    if (_autoAdvanceTimer <= 0f)
                    {
                        StartWave(CurrentWave + 1);
                    }
                }
                return;
            }

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
            _autoAdvanceTimer = 4.0f; // 4s countdown or immediate button click

            if (_intermissionPanel != null)
                _intermissionPanel.SetActive(true);

            if (_bannerText != null)
                _bannerText.text = $"WAVE {waveNumber} CLEARED!";

            WaveCompleted?.Invoke(waveNumber);
        }

        private void OnLedgerChanged()
        {
            // Can be used for HUD or debug
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

            var panel = UiFactory.CreatePanel(canvas.transform, "IntermissionPanel", new Color(0.05f, 0.05f, 0.1f, 0.85f));
            _intermissionPanel = panel.gameObject;

            _bannerText = UiFactory.CreateText(panel.transform, "Banner", "WAVE CLEARED!", 70, TextAnchor.MiddleCenter, new Color(1f, 0.9f, 0.3f));
            UiFactory.Place(_bannerText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 150f), new Vector2(1000f, 120f));

            _countdownText = UiFactory.CreateText(panel.transform, "Countdown", "", 28, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(_countdownText.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 40f), new Vector2(800f, 60f));

            _nextWaveButton = UiFactory.CreateButton(panel.transform, "NextWaveBtn", "NEXT WAVE", () => StartWave(CurrentWave + 1));
            UiFactory.Place((RectTransform)_nextWaveButton.transform, new Vector2(0.5f, 0.5f), new Vector2(-160f, -60f), new Vector2(280f, 80f));

            var jumpBossBtn = UiFactory.CreateButton(panel.transform, "JumpBossBtn", "DEV: JUMP WAVE 10", () => StartWave(10));
            UiFactory.Place((RectTransform)jumpBossBtn.transform, new Vector2(0.5f, 0.5f), new Vector2(160f, -60f), new Vector2(280f, 80f));

            _intermissionPanel.SetActive(false);
        }
    }
}
