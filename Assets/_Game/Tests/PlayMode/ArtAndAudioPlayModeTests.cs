using System.Collections;
using FinalBoss.Core;
using FinalBoss.Game;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace FinalBoss.Tests
{
    public class ArtAndAudioPlayModeTests
    {
        private ArenaController _arena;
        private PlayerActor _player;

        [TearDown]
        public void TearDown()
        {
            Time.timeScale = 1f;
        }

        private static IEnumerator WaitForScene(string name)
        {
            float deadline = Time.realtimeSinceStartup + 10f;
            while (SceneManager.GetActiveScene().name != name && Time.realtimeSinceStartup < deadline)
                yield return null;
            Assert.AreEqual(name, SceneManager.GetActiveScene().name);
            yield return null;
            yield return null;
        }

        [UnityTest]
        public IEnumerator LichLord_SpawnsWithPixelArtPresenter_AndCyclesAnimationStates()
        {
            GameSession.Reset(12345);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.IsTrue(menu.StartSelectedRun());

            yield return WaitForScene(SceneNames.GameArena);
            _arena = Object.FindAnyObjectByType<ArenaController>();
            _player = _arena.Player;
            Assert.IsNotNull(_player);

            var presenter = _player.GetComponent<LichLordVisualPresenter>();
            Assert.IsNotNull(presenter, "LichLordVisualPresenter should be attached to Lich Lord actor");
            Assert.IsTrue(presenter.IsLoaded, "Presenter should have loaded pixel art sprites from LichLord.png");

            var sr = _player.GetComponent<SpriteRenderer>();
            Assert.IsNotNull(sr);
            Assert.AreEqual("LichLord_Idle", sr.sprite.name);

            // Trigger cast visual
            presenter.TriggerCastVisual(0.3f);
            yield return null;
            Assert.AreEqual("LichLord_Cast", sr.sprite.name);

            yield return null;
        }

        [UnityTest]
        public IEnumerator AudioCuePresenter_RespondsToCueBus_WithoutErrors()
        {
            GameSession.Reset(12346);
            SceneManager.LoadScene(SceneNames.MainMenu);
            yield return WaitForScene(SceneNames.MainMenu);

            var menu = Object.FindAnyObjectByType<MainMenuController>();
            menu.OpenSelectForTest();
            menu.SelectCharacterForTest(CharacterCatalog.LichLordId);
            Assert.IsTrue(menu.StartSelectedRun());

            yield return WaitForScene(SceneNames.GameArena);
            _arena = Object.FindAnyObjectByType<ArenaController>();

            var audioPresenter = _arena.GetComponent<AudioCuePresenter>();
            Assert.IsNotNull(audioPresenter, "AudioCuePresenter must be attached to ArenaController");

            // Raise various combat and event audio cues
            CueBus.Raise(CueIds.BoltFire, 0f, 0f);
            CueBus.Raise(CueIds.PlayerDash, 0f, 0f);
            CueBus.Raise(CueIds.PlayerHit, 0f, 0f, 15f);
            CueBus.Raise(CueIds.ShrineActivated, 0f, 0f);

            yield return null;
            yield return null;
        }
    }
}
