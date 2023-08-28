using UnityEditor;
using UnityEngine;

namespace Submodules.SmallEditorTools.PlayerPrefsClearer
{
    public static class PlayerPrefsClearer
    {
        private const string Title = "Delete all player preferences";
        private const string Message = "Are you sure you want to delete all player preferences?\nThis action cannot be undone.";

        [MenuItem("Tools/Clear All PlayerPrefs")]
        private static void ClearAllEditorPrefs()
        {
            if (EditorUtility.DisplayDialog(Title, Message, "Yes", "No"))
                PlayerPrefs.DeleteAll();
        }
    }
}