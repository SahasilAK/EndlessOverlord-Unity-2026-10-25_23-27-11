using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FinalBoss.Game
{
    /// <summary>
    /// Single place that owns the Input System actions: WASD/arrows/left stick move, Space dash,
    /// 1-5 skill slots, Esc pause. Gameplay reads <see cref="Move"/> and subscribes to the events, so a
    /// touch/mobile adapter can later raise the same events. In P1 dash and skills only raise events;
    /// real behaviour arrives in P2 (dash) and P9 (skill slots).
    /// </summary>
    public sealed class PlayerInputAdapter : MonoBehaviour
    {
        public const int SkillSlotCount = 5;

        private InputAction _move;
        private InputAction _dash;
        private InputAction _pause;
        private InputAction[] _skills;

        public Vector2 Move { get; private set; }

        public event Action DashPressed;
        public event Action<int> SkillPressed;   // zero-based slot index
        public event Action PausePressed;

        private void Awake()
        {
            _move = new InputAction("Move", InputActionType.Value);
            _move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w").With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a").With("Right", "<Keyboard>/d");
            _move.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/upArrow").With("Down", "<Keyboard>/downArrow")
                .With("Left", "<Keyboard>/leftArrow").With("Right", "<Keyboard>/rightArrow");
            _move.AddBinding("<Gamepad>/leftStick");

            _dash = new InputAction("Dash", InputActionType.Button);
            _dash.AddBinding("<Keyboard>/space");
            _dash.AddBinding("<Gamepad>/buttonSouth");

            _pause = new InputAction("Pause", InputActionType.Button);
            _pause.AddBinding("<Keyboard>/escape");
            _pause.AddBinding("<Gamepad>/start");

            _skills = new InputAction[SkillSlotCount];
            for (int i = 0; i < SkillSlotCount; i++)
            {
                _skills[i] = new InputAction("Skill" + (i + 1), InputActionType.Button);
                _skills[i].AddBinding("<Keyboard>/" + (i + 1));
            }
            _skills[0].AddBinding("<Keyboard>/e");
            _skills[0].AddBinding("<Mouse>/rightButton");
        }

        private void OnEnable()
        {
            _move.Enable();
            _dash.Enable();
            _pause.Enable();
            _dash.performed += OnDash;
            _pause.performed += OnPause;
            for (int i = 0; i < _skills.Length; i++)
            {
                _skills[i].Enable();
                _skills[i].performed += OnSkill;
            }
        }

        private void OnDisable()
        {
            _dash.performed -= OnDash;
            _pause.performed -= OnPause;
            _move.Disable();
            _dash.Disable();
            _pause.Disable();
            for (int i = 0; i < _skills.Length; i++)
            {
                _skills[i].performed -= OnSkill;
                _skills[i].Disable();
            }
            Move = Vector2.zero;
        }

        private void OnDestroy()
        {
            _move?.Dispose();
            _dash?.Dispose();
            _pause?.Dispose();
            if (_skills != null)
                for (int i = 0; i < _skills.Length; i++)
                    _skills[i]?.Dispose();
        }

        private void Update()
        {
            var raw = _move.ReadValue<Vector2>();
            // Normalise so diagonals are not faster; keep analogue magnitude below 1.
            Move = raw.sqrMagnitude > 1f ? raw.normalized : raw;
        }

        private void OnDash(InputAction.CallbackContext ctx) { DashPressed?.Invoke(); }

        private void OnPause(InputAction.CallbackContext ctx) { RaisePause(); }

        private void OnSkill(InputAction.CallbackContext ctx)
        {
            for (int i = 0; i < _skills.Length; i++)
                if (ReferenceEquals(_skills[i], ctx.action))
                {
                    SkillPressed?.Invoke(i);
                    return;
                }
        }

        // Entry points for other adapters (touch UI) and for tests.
        public void RaisePause() { PausePressed?.Invoke(); }
        public void RaiseDash() { DashPressed?.Invoke(); }
        public void RaiseSkill(int slot)
        {
            if (slot >= 0 && slot < SkillSlotCount)
                SkillPressed?.Invoke(slot);
        }
    }
}
