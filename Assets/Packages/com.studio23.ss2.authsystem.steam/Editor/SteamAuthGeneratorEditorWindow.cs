using System.IO;
using Studio23.SS2.AuthSystem.Steam.Data;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace Studio23.SS2.AuthSystem.Steam.Editor
{
    public class SteamAuthGeneratorEditorWindow : EditorWindow
    {
        [MenuItem("Studio-23/AuthSystem/Providers/Steam", false, 10)]
        static void CreateDefaultProvider()
        {
            SteamAuthProvider providerSettings = ScriptableObject.CreateInstance<SteamAuthProvider>();
            
            string resourceFolderPath = "Assets/Resources/AuthSystem/Providers";

            if (!Directory.Exists(resourceFolderPath))
            {
                Directory.CreateDirectory(resourceFolderPath);
            }

            // Create the ScriptableObject asset in the resource folder
            string assetPath = resourceFolderPath + "/SteamAuthProvider.asset";
            AssetDatabase.CreateAsset(providerSettings, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            PlayerSettings.SetScriptingDefineSymbols(NamedBuildTarget.Standalone, "STEAMWORKS_ENABLED");
            Debug.Log($"Steam Auth Provider created at: {assetPath}");
        }
    }
}
