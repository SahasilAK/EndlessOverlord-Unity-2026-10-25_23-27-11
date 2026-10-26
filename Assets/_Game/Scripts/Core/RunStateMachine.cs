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
            { RunState.PlayingWave,     new[] { RunState.Paused, RunState.Death } },
            { RunState.Paused,          new[] { RunState.PlayingWave, RunState.MainMenu } },
            { RunState.Death,           new[] { RunState.Results } },
            { RunState.Results,         new[] { RunState.MainMenu } },
        };

        /// <summary>Raised after a successful change: (previous, current).</summary>
        public event Action<RunState, RunState> Changed;

        public RunState Current { get; private set; }

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

        public bool TryPause() { return Current == RunState.PlayingWave && TryTransition(RunState.Paused); }

        public bool TryResume() { return Current == RunState.Paused && TryTransition(RunState.PlayingWave); }

        /// <summary>
        /// Unconditional reset used only for safe teardown/recovery (e.g. entering the menu scene from an
        /// unexpected state). Still notifies listeners.
        /// </summary>
        public void ForceReset(RunState state = RunState.MainMenu)
        {
            var previous = Current;
            Current = state;
            if (previous != state)
                Changed?.Invoke(previous, state);
        }
    }
}
