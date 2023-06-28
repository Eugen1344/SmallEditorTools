using UnityEditor;
using UnityEngine;

namespace SmallEditorTools.KeystoreSaver
{
    [InitializeOnLoad]
    public static class KeystoreSaver
    {
        private const string KEYSTORE_SAVER_STATE_EDITOR_PREF_KEY = "119E0D5A-B754-4102-80F7-3D353EBFF1C2";
        private const string KEYSTORE_NAME_EDITOR_PREF_KEY = "629C449E-D99B-4B88-BE6A-F9C7475BB872";
        private const string KEYSTORE_PASS_EDITOR_PREF_KEY = "FE3FAA6D-4BCE-4D53-A981-A96651D0193E";
        private const string KEYALIAS_NAME_EDITOR_PREF_KEY = "4095965F-3F58-46F2-A086-32BD88C2DD7A";
        private const string KEYALIAS_PASS_EDITOR_PREF_KEY = "D545EC99-8CEA-4B8A-8CD5-D27CB98DE430";

        private static bool? _enabled = null;

        public static bool Enabled
        {
            get
            {
                _enabled ??= LoadKeystoreSaverStateFromEditorPrefs();

                return (bool) _enabled;
            }
            set
            {
                _enabled = value;
                SaveKeystoreSaverStateToEditorPrefs(value);
            }
        }

        static KeystoreSaver()
        {
            Enabled = LoadKeystoreSaverStateFromEditorPrefs();

            if (Enabled)
            {
                PlayerSettings.Android.keystoreName = LoadKeystoreNameFromEditorPrefs();
                PlayerSettings.Android.keystorePass = LoadKeystorePassFromEditorPrefs();
                PlayerSettings.Android.keyaliasName = LoadKeystoreAliasNameFromEditorPrefs();
                PlayerSettings.Android.keyaliasPass = LoadKeystoreAliasPassFromEditorPrefs();
            }
        }

        public static void SaveKeystoreSettings(string keystoreName, string keystorePass, string keyaliasName, string keyaliasPass)
        {
            EditorPrefs.SetString(AppendTechName(KEYSTORE_NAME_EDITOR_PREF_KEY), keystoreName);
            EditorPrefs.SetString(AppendTechName(KEYSTORE_PASS_EDITOR_PREF_KEY), keystorePass);
            EditorPrefs.SetString(AppendTechName(KEYALIAS_NAME_EDITOR_PREF_KEY), keyaliasName);
            EditorPrefs.SetString(AppendTechName(KEYALIAS_PASS_EDITOR_PREF_KEY), keyaliasPass);
        }

        private static void SaveKeystoreSaverStateToEditorPrefs(bool state)
        {
            EditorPrefs.SetBool(AppendTechName(KEYSTORE_SAVER_STATE_EDITOR_PREF_KEY), state);
        }

        private static bool LoadKeystoreSaverStateFromEditorPrefs()
        {
            return EditorPrefs.GetBool(AppendTechName(KEYSTORE_SAVER_STATE_EDITOR_PREF_KEY), false);
        }

        private static string LoadKeystoreNameFromEditorPrefs()
        {
            string result = "";
            if (EditorPrefs.HasKey(AppendTechName(KEYSTORE_NAME_EDITOR_PREF_KEY)))
            {
                result = EditorPrefs.GetString(AppendTechName(KEYSTORE_NAME_EDITOR_PREF_KEY));
                PlayerSettings.Android.useCustomKeystore = !string.IsNullOrEmpty(result);
            }

            return result;
        }

        private static string LoadKeystorePassFromEditorPrefs()
        {
            return EditorPrefs.GetString(AppendTechName(KEYSTORE_PASS_EDITOR_PREF_KEY), "");
        }

        private static string LoadKeystoreAliasNameFromEditorPrefs()
        {
            return EditorPrefs.GetString(AppendTechName(KEYALIAS_NAME_EDITOR_PREF_KEY), "");
        }

        private static string LoadKeystoreAliasPassFromEditorPrefs()
        {
            return EditorPrefs.GetString(AppendTechName(KEYALIAS_PASS_EDITOR_PREF_KEY), "");
        }

        private static string AppendTechName(string key)
        {
            return $"{Application.companyName}_{Application.productName}_{key}";
        }
    }
}