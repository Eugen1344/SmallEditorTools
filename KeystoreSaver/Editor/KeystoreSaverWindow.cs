using UnityEngine;
using UnityEditor;

public class KeystoreSaverWindow : EditorWindow
{
	[MenuItem("Tools/Keystore Saver")]
	private static void Open()
	{
		GetWindow<KeystoreSaverWindow>("Keystore Saver");
	}
	
	private void OnGUI()
	{
		KeystoreSaver.Enabled = EditorGUILayout.Toggle(new GUIContent("Enable Keystore Saver"), KeystoreSaver.Enabled);
		EditorGUIUtility.labelWidth = 80;
		PlayerSettings.Android.keystoreName = EditorGUILayout.TextField(new GUIContent("Keystore Path"), PlayerSettings.Android.keystoreName);
		PlayerSettings.Android.keystorePass = EditorGUILayout.TextField(new GUIContent("Keystore Password"), PlayerSettings.Android.keystorePass);
		PlayerSettings.Android.keyaliasName = EditorGUILayout.TextField(new GUIContent("Alias"), PlayerSettings.Android.keyaliasName);
		PlayerSettings.Android.keyaliasPass = EditorGUILayout.TextField(new GUIContent("Alias Password"), PlayerSettings.Android.keyaliasPass);
		if (GUILayout.Button(new GUIContent("Save")))
		{
			SaveKeystoreSettings();
		}
	}

	public void SaveKeystoreSettings()
	{
		KeystoreSaver.SaveKeystoreSettings(PlayerSettings.Android.keystoreName, PlayerSettings.Android.keystorePass, PlayerSettings.Android.keyaliasName, PlayerSettings.Android.keyaliasPass);
	}
}