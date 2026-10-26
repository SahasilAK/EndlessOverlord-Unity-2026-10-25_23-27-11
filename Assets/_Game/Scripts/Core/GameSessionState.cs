namespace FinalBoss.Core
{
    /// <summary>
    /// Pure-C# session logic: owns the state machine, the character selection and run creation rules.
    /// The Unity layer (GameSession) holds one instance and does the scene work. Keeping the rules here
    /// lets edit-mode tests cover them without any scene.
    /// </summary>
    public sealed class GameSessionState
    {
        private readonly RunSeedSource _seeds;
        private int _runCounter;

        public RunStateMachine Machine { get; }
        public string SelectedCharacterId { get; private set; }
        public RunContext Run { get; private set; }

        public GameSessionState(long seedEntropy)
        {
            _seeds = new RunSeedSource(seedEntropy);
            Machine = new RunStateMachine(RunState.MainMenu);
        }

        /// <summary>Selects a character. Only characters flagged playable can be selected.</summary>
        public bool TrySelectCharacter(string id)
        {
            var info = CharacterCatalog.Find(id);
            if (info == null || !info.Playable)
                return false;
            SelectedCharacterId = id;
            return true;
        }

        public void OpenCharacterSelect()
        {
            if (Machine.Current == RunState.MainMenu)
                Machine.TryTransition(RunState.CharacterSelect);
        }

        public void CloseCharacterSelect()
        {
            if (Machine.Current == RunState.CharacterSelect)
                Machine.TryTransition(RunState.MainMenu);
        }

        /// <summary>
        /// Creates a new run with a fresh seed and moves to RunSetup. Fails if no playable character is
        /// selected, if a run already exists, or if the state does not allow starting.
        /// </summary>
        public bool TryStartRun()
        {
            if (Run != null || SelectedCharacterId == null)
                return false;
            var info = CharacterCatalog.Find(SelectedCharacterId);
            if (info == null || !info.Playable)
                return false;
            if (Machine.Current == RunState.MainMenu)
                Machine.TryTransition(RunState.CharacterSelect);
            if (!Machine.TryTransition(RunState.RunSetup))
                return false;
            _runCounter++;
            Run = new RunContext(_seeds.NextSeed(), _runCounter, info);
            return true;
        }

        /// <summary>Called by the arena once it has initialised. RunSetup -> PlayingWave.</summary>
        public bool TryBeginPlaying()
        {
            return Run != null && Machine.TryTransition(RunState.PlayingWave);
        }

        /// <summary>Leaves the run and returns to the main menu, clearing run data. Always safe.</summary>
        public void ReturnToMenu()
        {
            if (!Machine.TryTransition(RunState.MainMenu))
                Machine.ForceReset(RunState.MainMenu);
            Run = null;
        }
    }
}
