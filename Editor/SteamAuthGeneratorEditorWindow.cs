using System.IO;
using Studio23.SS2.AuthSystem.Steam.Data;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.AuthSystem.Steam.Editor
{
    public class SteamAuthGeneratorEditorWindow : EditorWindow
    {
        [MenuItem("Studio-23/AuthSystem/Providers/Steam", false, 10)]
        static void CreateDefaultProvider()
        {
            SteamAuthProvider providerSettings = ScriptableObject.CreateInstance<SteamAuthProvider>();
            providerSettings.AppId = 480;//Setting Public default test game

            string resourceFolderPath = "Assets/Resources/AuthSystem/Providers";

            if (!Directory.Exists(resourceFolderPath))
            {
                Directory.CreateDirectory(resourceFolderPath);
            }

            // Create the ScriptableObject asset in the resource folder
            string assetPath = resourceFolderPath + "/AuthProvider.asset";
            AssetDatabase.CreateAsset(providerSettings, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log($"Steam Auth Provider created at: {assetPath}");
        }
    }
}
