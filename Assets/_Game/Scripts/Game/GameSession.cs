using System;
using FinalBoss.Core;
using UnityEngine;

namespace FinalBoss.Game
{
    public static class SceneNames
    {
        public const string MainMenu = "MainMenu";
        public const string GameArena = "GameArena";
    }

    /// <summary>
    /// Static holder for the single <see cref="GameSessionState"/>. It is not a DontDestroyOnLoad object,
    /// so there is nothing to tear down between scenes; it is reset on every play-mode entry
    /// (works even with domain reload disabled).
    /// </summary>
    public static class GameSession
    {
        private static GameSessionState _state;

        public static GameSessionState State
        {
            get
            {
                if (_state == null)
                    _state = new GameSessionState(CreateEntropy());
                return _state;
            }
        }

        public static RunStateMachine Machine { get { return State.Machine; } }
        public static RunContext Run { get { return State.Run; } }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnPlayModeEntry()
        {
            _state = null;
        }

        /// <summary>Replaces the session (tests / explicit reset). Optional fixed entropy for reproducibility.</summary>
        public static void Reset(long? entropy = null)
        {
            _state = new GameSessionState(entropy ?? CreateEntropy());
        }

        private static long CreateEntropy()
        {
            unchecked
            {
                return DateTime.UtcNow.Ticks ^ ((long)Guid.NewGuid().GetHashCode() << 32) ^ Environment.TickCount;
            }
        }
    }
}
