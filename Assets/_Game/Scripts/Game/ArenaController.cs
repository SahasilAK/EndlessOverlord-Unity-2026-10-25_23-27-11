using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Graybox arena scene driver. Initialises the run (seed/character), builds a placeholder floor and
    /// player marker, binds the HUD, and handles Pause / Resume / Return to menu through the explicit
    /// state machine. No combat or movement yet (P2).
    /// </summary>
    public sealed class ArenaController : MonoBehaviour
    {
        private PlayerInputAdapter _input;
        private HudView _hud;
        private GameObject _pausePanel;
        private bool _returning;

        public PlayerInputAdapter Input { get { return _input; } }

        private void Start()
        {
            var session = GameSession.State;
            if (session.Run == null)
            {
                // Arena opened directly in the editor: start a default run so the scene is testable.
                Debug.LogWarning("[ArenaController] No active run; starting a default Lich Lord run (editor direct-open).");
                session.TrySelectCharacter(CharacterCatalog.LichLordId);
                session.TryStartRun();
            }
            if (session.Run == null || !session.TryBeginPlaying())
            {
                Debug.LogError("[ArenaController] Could not begin the run (state " + session.Machine.Current + "). Returning to menu.");
                ReturnToMenu();
                return;
            }

            Time.timeScale = 1f;
            BuildPlaceholderWorld(session.Run);
            BuildUi(session.Run);
            session.Machine.Changed += OnStateChanged;
        }

        private void OnDestroy()
        {
            if (GameSession.State != null)
                GameSession.Machine.Changed -= OnStateChanged;
            if (_input != null)
                _input.PausePressed -= OnPausePressed;
            Time.timeScale = 1f;   // never leave the game frozen after leaving the arena
        }

        private void BuildPlaceholderWorld(RunContext run)
        {
            var cam = Camera.main;
            if (cam != null)
            {
                cam.orthographic = true;
                cam.orthographicSize = 8f;
                cam.transform.position = new Vector3(0f, 0f, -10f);
                cam.backgroundColor = new Color(0.04f, 0.04f, 0.06f);
            }

            CreateBox("ArenaFloor", new Vector3(0f, 0f, 1f), new Vector2(28f, 15f), new Color(0.16f, 0.16f, 0.2f), -10);
            CreateBox("Player_" + run.Character.Id, Vector3.zero, new Vector2(0.9f, 0.9f), new Color(0.45f, 0.85f, 0.9f), 10);
        }

        private static void CreateBox(string name, Vector3 position, Vector2 size, Color color, int order)
        {
            var go = new GameObject(name);
            go.transform.position = position;
            go.transform.localScale = new Vector3(size.x, size.y, 1f);
            var renderer = go.AddComponent<SpriteRenderer>();
            renderer.sprite = PlaceholderSprites.Square;
            renderer.color = color;
            renderer.sortingOrder = order;
        }

        private void BuildUi(RunContext run)
        {
            UiFactory.EnsureEventSystem();

            var inputGo = new GameObject("PlayerInputAdapter");
            _input = inputGo.AddComponent<PlayerInputAdapter>();
            _input.PausePressed += OnPausePressed;

            var hudGo = new GameObject("Hud");
            _hud = hudGo.AddComponent<HudView>();
            _hud.Bind(run, _input);

            var canvas = UiFactory.CreateCanvas("PauseCanvas", 20);
            var panel = UiFactory.CreatePanel(canvas.transform, "PausePanel", new Color(0f, 0f, 0f, 0.7f));
            _pausePanel = panel.gameObject;
            var title = UiFactory.CreateText(panel.transform, "PauseTitle", "PAUSED", 80, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Place(title.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0f, 200f), new Vector2(800f, 120f));
            var resume = UiFactory.CreateButton(panel.transform, "ResumeButton", "Resume", () => Resume());
            UiFactory.Place((RectTransform)resume.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, 40f), new Vector2(420f, 90f));
            var back = UiFactory.CreateButton(panel.transform, "ReturnButton", "Return to Menu", () => ReturnToMenu());
            UiFactory.Place((RectTransform)back.transform, new Vector2(0.5f, 0.5f), new Vector2(0f, -80f), new Vector2(420f, 90f));
            _pausePanel.SetActive(false);
        }

        private void OnPausePressed()
        {
            var machine = GameSession.Machine;
            if (machine.Current == RunState.PlayingWave)
                machine.TryPause();
            else if (machine.Current == RunState.Paused)
                machine.TryResume();
        }

        public bool Resume() { return GameSession.Machine.TryResume(); }

        /// <summary>Single place that reacts to flow changes, so pause UI and time scale can never disagree.</summary>
        private void OnStateChanged(RunState previous, RunState current)
        {
            bool paused = current == RunState.Paused;
            Time.timeScale = paused ? 0f : 1f;
            if (_pausePanel != null)
                _pausePanel.SetActive(paused);
            if (_hud != null)
                _hud.RefreshState();
        }

        /// <summary>Leaves the arena, resets run data and loads the menu. Safe to call more than once.</summary>
        public void ReturnToMenu()
        {
            if (_returning)
                return;
            _returning = true;
            Time.timeScale = 1f;
            GameSession.State.ReturnToMenu();
            if (!SceneFlow.Load(SceneNames.MainMenu))
                _returning = false;
        }
    }

    /// <summary>Generated white square sprite (1 world unit) used by all graybox shapes.</summary>
    public static class PlaceholderSprites
    {
        private static Sprite _square;

        public static Sprite Square
        {
            get
            {
                if (_square == null)
                {
                    var tex = new Texture2D(4, 4, TextureFormat.RGBA32, false) { filterMode = FilterMode.Point };
                    var pixels = new Color32[16];
                    for (int i = 0; i < pixels.Length; i++) pixels[i] = new Color32(255, 255, 255, 255);
                    tex.SetPixels32(pixels);
                    tex.Apply();
                    _square = Sprite.Create(tex, new Rect(0, 0, 4, 4), new Vector2(0.5f, 0.5f), 4f);
                }
                return _square;
            }
        }
    }
}
