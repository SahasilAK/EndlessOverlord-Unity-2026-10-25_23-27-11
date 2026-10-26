using System.Collections.Generic;
using FinalBoss.Core;
using NUnit.Framework;

namespace FinalBoss.Tests
{
    public class RunStateMachineTests
    {
        [Test]
        public void StartsInMainMenu()
        {
            Assert.AreEqual(RunState.MainMenu, new RunStateMachine().Current);
        }

        [Test]
        public void RejectsInvalidTransitions()
        {
            var m = new RunStateMachine();
            Assert.IsFalse(m.TryTransition(RunState.PlayingWave));
            Assert.IsFalse(m.TryPause());
            Assert.IsFalse(m.TryResume());
            Assert.AreEqual(RunState.MainMenu, m.Current);
        }

        [Test]
        public void PauseResumeOnlyFromValidStatesAndNeverDouble()
        {
            var m = new RunStateMachine(RunState.PlayingWave);
            Assert.IsTrue(m.TryPause());
            Assert.IsFalse(m.TryPause());
            Assert.IsTrue(m.TryResume());
            Assert.IsFalse(m.TryResume());
            Assert.AreEqual(RunState.PlayingWave, m.Current);
        }

        [Test]
        public void ChangedEventReportsPreviousAndCurrent()
        {
            var m = new RunStateMachine();
            RunState prev = RunState.Results, cur = RunState.Results;
            int calls = 0;
            m.Changed += (p, c) => { prev = p; cur = c; calls++; };
            m.TryTransition(RunState.CharacterSelect);
            m.TryTransition(RunState.PlayingWave); // invalid: no event
            Assert.AreEqual(1, calls);
            Assert.AreEqual(RunState.MainMenu, prev);
            Assert.AreEqual(RunState.CharacterSelect, cur);
        }

        [Test]
        public void ForceResetAlwaysReachesMenu()
        {
            var m = new RunStateMachine(RunState.Paused);
            m.ForceReset();
            Assert.AreEqual(RunState.MainMenu, m.Current);
        }
    }

    public class GameSessionStateTests
    {
        [Test]
        public void CannotSelectUnavailableOrUnknownCharacters()
        {
            var s = new GameSessionState(1);
            Assert.IsFalse(s.TrySelectCharacter("unknown_character"));
            Assert.IsFalse(s.TrySelectCharacter("nobody"));
            Assert.IsNull(s.SelectedCharacterId);
            Assert.IsFalse(s.TryStartRun());
        }

        [Test]
        public void MenuSelectStartCreatesRunWithSeed()
        {
            var s = new GameSessionState(42);
            s.OpenCharacterSelect();
            Assert.AreEqual(RunState.CharacterSelect, s.Machine.Current);
            Assert.IsTrue(s.TrySelectCharacter(CharacterCatalog.LichLordId));
            Assert.IsTrue(s.TryStartRun());
            Assert.AreEqual(RunState.RunSetup, s.Machine.Current);
            Assert.IsNotNull(s.Run);
            Assert.AreEqual(CharacterCatalog.LichLordId, s.Run.Character.Id);
            Assert.AreEqual(1, s.Run.Hud.Wave);
            Assert.IsTrue(s.TryBeginPlaying());
            Assert.AreEqual(RunState.PlayingWave, s.Machine.Current);
        }

        [Test]
        public void SecondRunCannotStartWhileOneIsActive()
        {
            var s = new GameSessionState(7);
            s.TrySelectCharacter(CharacterCatalog.LichLordId);
            Assert.IsTrue(s.TryStartRun());
            Assert.IsFalse(s.TryStartRun());
        }

        [Test]
        public void ReturnToMenuClearsRunFromAnyState()
        {
            var s = new GameSessionState(7);
            s.TrySelectCharacter(CharacterCatalog.LichLordId);
            s.TryStartRun();
            s.TryBeginPlaying();
            s.Machine.TryPause();
            s.ReturnToMenu();
            Assert.IsNull(s.Run);
            Assert.AreEqual(RunState.MainMenu, s.Machine.Current);
        }

        [Test]
        public void EachNewRunGetsADifferentSeedAndNumber()
        {
            var s = new GameSessionState(1234);
            s.TrySelectCharacter(CharacterCatalog.LichLordId);
            var seeds = new HashSet<long>();
            for (int i = 1; i <= 50; i++)
            {
                Assert.IsTrue(s.TryStartRun());
                Assert.AreEqual(i, s.Run.RunNumber);
                Assert.IsTrue(seeds.Add(s.Run.Seed), "duplicate seed on run " + i);
                s.ReturnToMenu();
            }
        }

        [Test]
        public void SameEntropyReproducesSeedSequence()
        {
            var a = new RunSeedSource(99);
            var b = new RunSeedSource(99);
            for (int i = 0; i < 10; i++)
                Assert.AreEqual(a.NextSeed(), b.NextSeed());
            Assert.AreNotEqual(new RunSeedSource(1).NextSeed(), new RunSeedSource(2).NextSeed());
        }
    }

    public class RngAndCatalogTests
    {
        [Test]
        public void RngIsDeterministicPerSeed()
        {
            var a = new SplitMix64Rng(12345);
            var b = new SplitMix64Rng(12345);
            for (int i = 0; i < 100; i++)
                Assert.AreEqual(a.NextUInt64(), b.NextUInt64());
            Assert.AreNotEqual(new SplitMix64Rng(1).NextUInt64(), new SplitMix64Rng(2).NextUInt64());
        }

        [Test]
        public void NextIntStaysInRange()
        {
            var r = new SplitMix64Rng(5);
            for (int i = 0; i < 1000; i++)
            {
                int v = r.NextInt(-3, 4);
                Assert.That(v, Is.InRange(-3, 3));
                double d = r.NextDouble();
                Assert.That(d, Is.GreaterThanOrEqualTo(0.0).And.LessThan(1.0));
            }
        }

        [Test]
        public void CatalogHasFiveCharactersAndAllArePlayable()
        {
            Assert.AreEqual(5, CharacterCatalog.Characters.Count);
            int playable = 0;
            foreach (var c in CharacterCatalog.Characters)
                if (c.Playable) playable++;
            Assert.AreEqual(5, playable);
        }

        [Test]
        public void HudModelClampsAndBroadcasts()
        {
            var hud = new HudModel(100f, "Soul Essence", 50f, 1);
            int changes = 0;
            hud.Changed += () => changes++;
            hud.SetHealth(-5f);
            Assert.AreEqual(0f, hud.Health);
            hud.SetResource(999f);
            Assert.AreEqual(50f, hud.Resource);
            hud.SetWave(0);
            Assert.AreEqual(1, hud.Wave);
            hud.AddGold(int.MaxValue);
            hud.AddGold(10);
            Assert.AreEqual(int.MaxValue, hud.Gold);
            hud.AddScore(long.MaxValue);
            hud.AddScore(1);
            Assert.AreEqual(long.MaxValue, hud.Score);
            Assert.AreEqual(7, changes);
        }
    }
}
