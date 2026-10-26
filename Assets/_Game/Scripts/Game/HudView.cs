using FinalBoss.Core;
using UnityEngine;
using UnityEngine.UI;

namespace FinalBoss.Game
{
    /// <summary>
    /// Placeholder HUD bound to <see cref="HudModel"/>. Shows health, resource, wave, gold, score,
    /// seed, flow state and the last input received (proves the input adapter is wired).
    /// </summary>
    public sealed class HudView : MonoBehaviour
    {
        private HudModel _model;
        private RunContext _run;
        private Text _statusText;
        private Text _inputText;
        private string _lastAction = "-";

        public void Bind(RunContext run, PlayerInputAdapter input)
        {
            Unbind();
            _run = run;
            _model = run.Hud;
            _model.Changed += Refresh;
            if (input != null)
            {
                input.DashPressed += OnDash;
                input.SkillPressed += OnSkill;
            }
            _boundInput = input;
            Refresh();
        }

        private PlayerInputAdapter _boundInput;

        private void Awake()
        {
            var canvas = UiFactory.CreateCanvas("HudCanvas", 10);
            canvas.transform.SetParent(transform, false);
            _statusText = UiFactory.CreateText(canvas.transform, "Status", "", 34, TextAnchor.UpperLeft, Color.white);
            UiFactory.Place(_statusText.rectTransform, new Vector2(0f, 1f), new Vector2(30f, -30f), new Vector2(900f, 400f));
            _inputText = UiFactory.CreateText(canvas.transform, "InputDebug", "", 26, TextAnchor.LowerLeft, new Color(0.8f, 0.9f, 1f));
            UiFactory.Place(_inputText.rectTransform, new Vector2(0f, 0f), new Vector2(30f, 30f), new Vector2(1200f, 60f));
        }

        private void Update()
        {
            if (_boundInput != null)
            {
                var m = _boundInput.Move;
                _inputText.text = "Move " + m.x.ToString("0.00") + "," + m.y.ToString("0.00") + "   Last: " + _lastAction;
            }

            // Keep signature ability cooldown updated on HUD
            Refresh();
        }

        private void OnDestroy()
        {
            Unbind();
        }

        private void Unbind()
        {
            if (_model != null)
                _model.Changed -= Refresh;
            if (_boundInput != null)
            {
                _boundInput.DashPressed -= OnDash;
                _boundInput.SkillPressed -= OnSkill;
            }
            _model = null;
            _boundInput = null;
        }

        private void OnDash() { _lastAction = "Dash"; }
        private void OnSkill(int slot) { _lastAction = "Skill " + (slot + 1); }

        private void Refresh()
        {
            if (_model == null || _run == null)
                return;
            string biomeLabel = _run.Layout != null
                ? $"   [{_run.Layout.Biome.Name} - {_run.Layout.TopologyType}]"
                : "";

            string slotsInfo = "";
            var player = ActorRegistry.Player;
            if (player != null)
            {
                var loadout = player.GetComponent<AbilityLoadoutController>();
                if (loadout != null)
                {
                    var sb = new System.Text.StringBuilder();
                    sb.Append("\nAbilities:");
                    for (int s = 0; s < AbilityLoadout.SlotCount; s++)
                    {
                        var def = loadout.GetSlotAbility(s);
                        string keyLabel = s == 0 ? "1/E" : (s + 1).ToString();
                        if (def != null)
                        {
                            float cd = loadout.GetSlotCooldown(s);
                            float cost = loadout.GetSlotResourceCost(s);
                            string autoTag = loadout.IsAutocastEnabled(s) ? "[AUTO]" : "";
                            string status = cd <= 0f
                                ? (loadout.HasSufficientResourceForSlot(s) ? "READY" : "NO_RES")
                                : cd.ToString("0.0") + "s";
                            sb.Append($"  [{keyLabel}:{def.Name} {autoTag} {status} ({cost:0})]");
                        }
                        else
                        {
                            sb.Append($"  [{keyLabel}:Empty]");
                        }
                    }
                    slotsInfo = sb.ToString();
                }
            }

            _statusText.text =
                _run.Character.DisplayName + "   (run #" + _run.RunNumber + ")" + biomeLabel + "\n" +
                "HP " + Mathf.CeilToInt(_model.Health) + " / " + Mathf.CeilToInt(_model.MaxHealth) + "\n" +
                _model.ResourceName + " " + Mathf.CeilToInt(_model.Resource) + " / " + Mathf.CeilToInt(_model.MaxResource) + "\n" +
                "Wave " + _model.Wave + "   Gold " + _model.Gold + "   Score " + _model.Score + "\n" +
                "Seed " + _run.Seed + "   State " + GameSession.Machine.Current + slotsInfo;
        }

        /// <summary>Re-reads state-dependent text (flow state changes do not touch the HudModel).</summary>
        public void RefreshState() { Refresh(); }
    }
}
