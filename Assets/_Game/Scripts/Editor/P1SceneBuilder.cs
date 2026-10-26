using System.IO;
using FinalBoss.Game;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace FinalBoss.EditorTools
{
    /// <summary>
    /// Creates the MainMenu and GameArena scenes by copying the template SampleScene (which keeps the URP 2D
    /// camera and Global Light 2D setup) and adding one controller object each. SampleScene itself is never
    /// modified. Idempotent: re-running reuses existing scenes.
    /// </summary>
    public static class P1SceneBuilder
    {
        private const string SourceScene = "Assets/Scenes/SampleScene.unity";
        private const string SceneDir = "Assets/_Game/Scenes";

        [MenuItem("FinalBoss/Build P1 Scenes")]
        public static void BuildScenes()
        {
            if (!Directory.Exists(SceneDir))
                Directory.CreateDirectory(SceneDir);
            AssetDatabase.Refresh();

            string menuPath = SceneDir + "/MainMenu.unity";
            string arenaPath = SceneDir + "/GameArena.unity";

            Prepare(menuPath, "MenuController", typeof(MainMenuController));
            Prepare(arenaPath, "ArenaController", typeof(ArenaController));

            EditorBuildSettings.scenes = new[]
            {
                new EditorBuildSettingsScene(menuPath, true),
                new EditorBuildSettingsScene(arenaPath, true),
                new EditorBuildSettingsScene(SourceScene, false),
            };

            EditorSceneManager.OpenScene(menuPath, OpenSceneMode.Single);
            Debug.Log("[FinalBoss] P1 scenes ready: MainMenu, GameArena (build settings updated; SampleScene kept, disabled).");
        }

        private static void Prepare(string path, string objectName, System.Type controller)
        {
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(path) == null)
            {
                if (!AssetDatabase.CopyAsset(SourceScene, path))
                    throw new IOException("Could not copy " + SourceScene + " to " + path);
            }

            var scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
            var existing = GameObject.Find(objectName);
            if (existing == null)
            {
                var go = new GameObject(objectName);
                go.AddComponent(controller);
            }
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
        }
    }
}
