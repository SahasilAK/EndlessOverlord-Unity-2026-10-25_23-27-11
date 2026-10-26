using UnityEngine;
using UnityEngine.SceneManagement;

namespace FinalBoss.Game
{
    /// <summary>
    /// Loads scenes with a re-entrancy guard so rapid double clicks cannot queue two loads.
    /// </summary>
    public static class SceneFlow
    {
        private static bool _loading;

        public static bool IsLoading { get { return _loading; } }

        public static bool Load(string sceneName)
        {
            if (_loading)
                return false;
            _loading = true;
            SceneManager.sceneLoaded += OnLoaded;
            SceneManager.LoadScene(sceneName);
            return true;
        }

        private static void OnLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnLoaded;
            _loading = false;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetOnPlayModeEntry()
        {
            _loading = false;
        }
    }
}
