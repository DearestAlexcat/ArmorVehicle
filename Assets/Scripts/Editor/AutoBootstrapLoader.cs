#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class AutoBootstrapLoader
{
    private const string BootstrapScenePath = "Assets/StaticAssets/Scenes/Bootstrap.unity"; // Path to the starting scene

    static AutoBootstrapLoader()
    {
        // Subscribe to mode change event
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state != PlayModeStateChange.ExitingEditMode)
            return;

        // If the active scene is not Bootstrap, we substitute it as the starting one
        Scene activeScene = SceneManager.GetActiveScene();

        if (!activeScene.path.Equals(BootstrapScenePath))
        {
            var bootstrapScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(BootstrapScenePath);
            if (bootstrapScene != null)
            {
                Debug.LogWarning($"[AutoBootstrapLoader] Scene \"{activeScene.name}\" is not the starting scene. Replace with \"{bootstrapScene.name}\".");
                EditorSceneManager.playModeStartScene = bootstrapScene;
            }
            else
            {
                Debug.LogError($"[AutoBootstrapLoader] Could not find scene on path: {BootstrapScenePath}");
            }
        }
        else
        {
            // If you are already on the desired scene, reset the custom start
            EditorSceneManager.playModeStartScene = null;
        }
    }
}
#endif
