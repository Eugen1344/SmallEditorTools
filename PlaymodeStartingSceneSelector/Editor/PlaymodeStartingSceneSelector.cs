using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SmallEditorTools.PlaymodeStartingSceneSelector
{
    public class PlaymodeStartingSceneSelector : EditorWindow
    {
        [MenuItem("Tools/Playmode Starting Scene")]
        static void Open()
        {
            GetWindow<PlaymodeStartingSceneSelector>("Starting Scene");
        }

        private void OnEnable()
        {
            PlaymodeStartingSceneSetter.SetPlayModeStartScene(PlaymodeStartingSceneSetter.LoadStartingSceneFromEditorPrefs());
        }

        private void OnGUI()
        {
            EditorGUIUtility.labelWidth = 80;
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.HelpBox("This scene will always be loaded when you press play. " +
                                    "After you press stop you will return to scene you'll been working on.", MessageType.Info);
            SceneAsset selectedScene = (SceneAsset) EditorGUILayout.ObjectField(new GUIContent("Start Scene"),
                EditorSceneManager.playModeStartScene, typeof(SceneAsset), false);
            if (EditorGUI.EndChangeCheck())
            {
                SetAndSaveStartingScene(selectedScene);
            }
        }

        private static void SetAndSaveStartingScene(SceneAsset selectedScene)
        {
            PlaymodeStartingSceneSetter.SetPlayModeStartScene(selectedScene);
            PlaymodeStartingSceneSetter.SaveStartingSceneFromEditorPrefs(selectedScene);
        }
    }
}