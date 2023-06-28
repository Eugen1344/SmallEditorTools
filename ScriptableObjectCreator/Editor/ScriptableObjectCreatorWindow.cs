using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SmallEditorTools.ScriptableObjectCreator
{
    public class ScriptableObjectCreatorWindow : EditorWindow
    {
        private string _typeName;
        private string _objectName = "New Object";

        [MenuItem("Tools/Scriptable Object Creator")]
        static void Open()
        {
            GetWindow<ScriptableObjectCreatorWindow>("Scriptable Object Creator");
        }

        void OnGUI()
        {
            EditorGUIUtility.labelWidth = 200;
            _typeName = EditorGUILayout.TextField(new GUIContent("Scriptable object TYPE name"), _typeName);
            _objectName = EditorGUILayout.TextField(new GUIContent("New scriptable object name"), _objectName);

            if (GUILayout.Button(new GUIContent("Create")))
            {
                CreateObject();
            }
        }

        public void CreateObject()
        {
            if (!CheckName(_objectName))
                return;

            if (string.IsNullOrWhiteSpace(_typeName))
                return;

            Type objectType = GetObjectType(_typeName);

            if (objectType == null)
            {
                Debug.LogError($"Type \"{_typeName}\" was not found!");
                return;
            }

            string path = GetPath();
            string assetPath = Path.Combine(path, $"{_objectName}.asset");
            string uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

            ScriptableObject newObject = CreateInstance(objectType);
            AssetDatabase.CreateAsset(newObject, uniqueAssetPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newObject;
        }

        private bool CheckName(string objectName)
        {
            if (string.IsNullOrWhiteSpace(objectName))
            {
                Debug.LogError($"Name: \"{objectName}\" is invalid");
                return false;
            }

            return true;
        }

        private Type GetObjectType(string name)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(ScriptableObject)) && string.Equals(type.Name, name, StringComparison.OrdinalIgnoreCase)))
                {
                    return type;
                }
            }

            return null;
        }

        private string GetPath()
        {
            string selectedObjectPath = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());

            if (string.IsNullOrWhiteSpace(selectedObjectPath))
                return "Assets";

            if (Directory.Exists(selectedObjectPath))
                return selectedObjectPath;

            return Path.GetDirectoryName(selectedObjectPath);
        }
    }
}