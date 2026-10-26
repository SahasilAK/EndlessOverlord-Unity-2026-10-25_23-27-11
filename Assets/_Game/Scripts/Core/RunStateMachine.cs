using System;
using System.Collections.Generic;

namespace FinalBoss.Core
{
    /// <summary>Explicit application/run flow states (see GDD "Core loop / state machine").</summary>
    public enum RunState
    {
        MainMenu,
        CharacterSelect,
        RunSetup,
        PlayingWave,
        ChoosingReward,
        Intermission,
        Paused,
        Death,
        Results
    }

    /// <summary>
    /// Guards every state change against a fixed transition table so invalid or concurrent transitions
    /// (for example pausing from the menu, or resuming twice) are rejected instead of silently applied.
    /// </summary>
    public sealed class RunStateMachine
    {
        private static readonly Dictionary<RunState, RunState[]> Allowed = new Dictionary<RunState, RunState[]>
        {
            { RunState.MainMenu,        new[] { RunState.CharacterSelect } },
            { RunState.CharacterSelect, new[] { RunState.MainMenu, RunState.RunSetup } },
            { RunState.RunSetup,        new[] { RunState.PlayingWave, RunState.MainMenu } },
            { RunState.PlayingWave,     new[] { RunState.Paused, RunState.Death, RunState.ChoosingReward } },
            { RunState.ChoosingReward,  new[] { RunState.Intermission, RunState.PlayingWave, RunState.Paused, RunState.Death } },
            { RunState.Intermission,    new[] { RunState.PlayingWave, RunState.Paused, RunState.Death, RunState.MainMenu } },
            { RunState.Paused,          new[] { RunState.PlayingWave, RunState.ChoosingReward, RunState.Intermission, RunState.MainMenu } },
            { RunState.Death,           new[] { RunState.Results } },
            { RunState.Results,         new[] { RunState.MainMenu } },
        };

        /// <summary>Raised after a successful change: (previous, current).</summary>
        public event Action<RunState, RunState> Changed;

        public RunState Current { get; private set; }
        private RunState _prePauseState = RunState.PlayingWave;

        public RunStateMachine(RunState initial = RunState.MainMenu)
        {
            Current = initial;
        }

        public bool CanTransition(RunState to)
        {
            return Allowed.TryGetValue(Current, out var targets) && Array.IndexOf(targets, to) >= 0;
        }

        public bool TryTransition(RunState to)
        {
            if (!CanTransition(to))
                return false;
            var previous = Current;
            Current = to;
            Changed?.Invoke(previous, to);
            return true;
        }

        public bool TryPause()
        {
            if (Current == RunState.PlayingWave || Current == RunState.ChoosingReward || Current == RunState.Intermission)
            {
                _prePauseState = Current;
                return TryTransition(RunState.Paused);
            }
            return false;
        }

        public bool TryResume()
        {
            if (Current == RunState.Paused)
            {
                return TryTransition(_prePauseState);
            }
            return false;
        }

        /// <summary>
        /// Unconditional reset used only for safe teardown/recovery (e.g. entering the menu scene from an
        /// unexpected state). Still notifies listeners.
        /// </summary>
        public void ForceReset(RunState state = RunState.MainMenu)
        {
            var previous = Current;
            Current = state;
            _prePauseState = RunState.PlayingWave;
            if (previous != state)
                Changed?.Invoke(previous, state);
        }
    }
}
