using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public static class PlaymodeStartingSceneSetter
{
    private const string StartingSceneKey = "playmode_starting_scene";

    static PlaymodeStartingSceneSetter()
    {
        SceneAsset selectedScene = LoadStartingSceneFromEditorPrefs();
        if (!selectedScene)
        {
            return;
        }

        SetPlayModeStartScene(selectedScene);
    }

    public static void SaveStartingSceneFromEditorPrefs(SceneAsset selectedScene)
    {
        if (selectedScene == null)
        {
            EditorPrefs.SetString(StartingSceneKey + "_" + Application.productName, "");
            return;
        }

        string scenePath = AssetDatabase.GetAssetPath(selectedScene);
        string sceneGuid = AssetDatabase.AssetPathToGUID(scenePath);
        EditorPrefs.SetString(StartingSceneKey + "_" + Application.productName, sceneGuid);
    }

    public static SceneAsset LoadStartingSceneFromEditorPrefs()
    {
        string sceneGuid = EditorPrefs.GetString(StartingSceneKey + "_" + Application.productName);
        if (string.IsNullOrEmpty(sceneGuid))
        {
            return null;
        }

        string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
        if (string.IsNullOrEmpty(scenePath))
        {
            return null;
        }

        return AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
    }

    public static void SetPlayModeStartScene(SceneAsset selectedScene)
    {
        EditorSceneManager.playModeStartScene = selectedScene;
    }
}