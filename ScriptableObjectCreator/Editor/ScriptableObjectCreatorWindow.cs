using UnityEditor;
using UnityEngine;

namespace SmallEditorTools.ScriptableObjectCreator
{
    public class ScriptableObjectCreatorWindow : EditorWindow
    {
        private ScriptableObjectCreator _creator;

        private string _typeName;
        private string _objectName = "New Object";

        [MenuItem("Tools/Scriptable Object Creator")]
        private static void Open()
        {
            GetWindow<ScriptableObjectCreatorWindow>("Scriptable Object Creator");
        }

        private void OnEnable()
        {
            _creator = new ScriptableObjectCreator();
        }

        private void OnGUI()
        {
            EditorGUIUtility.labelWidth = 200;
            _typeName = EditorGUILayout.TextField(new GUIContent("Scriptable object TYPE name"), _typeName);
            _objectName = EditorGUILayout.TextField(new GUIContent("New scriptable object name"), _objectName);

            if (GUILayout.Button(new GUIContent("Create")))
            {
                _creator.CreateObject(_typeName, _objectName, CreateInstance);
            }
        }
    }
}