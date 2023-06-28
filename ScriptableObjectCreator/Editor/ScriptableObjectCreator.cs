using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace SmallEditorTools.ScriptableObjectCreator
{
    public class ScriptableObjectCreator
    {
        public delegate ScriptableObject ScriptableObjectFactoryMethod(Type type);

        public void CreateObject(string typeName, string objectName, ScriptableObjectFactoryMethod factoryMethod)
        {
            if (!ValidateName(objectName))
                return;

            if (string.IsNullOrWhiteSpace(typeName))
                return;

            Type objectType = GetObjectType(typeName);

            if (objectType == null)
            {
                Debug.LogError($"Type \"{typeName}\" was not found!");
                return;
            }

            string path = GetPath();
            string assetPath = Path.Combine(path, $"{objectName}.asset");
            string uniqueAssetPath = AssetDatabase.GenerateUniqueAssetPath(assetPath);

            ScriptableObject newObject = factoryMethod(objectType);
            AssetDatabase.CreateAsset(newObject, uniqueAssetPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = newObject;
        }

        private bool ValidateName(string objectName)
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