using Studio23.SS2.AuthSystem.Steam.Core;
using UnityEditor;
using UnityEngine;

namespace Studio23.SS2.AuthSystem.Steam.Editor
{
    public class SteamAuthGeneratorEditorWindow : EditorWindow
    {
        private uint _steamAppId;

        [MenuItem("Studio-23/Authentication/Provide Steam Auth Data")]
        public static void ShowWindow()
        {
            GetWindow<SteamAuthGeneratorEditorWindow>("Steam Authenticator");
        }

        private void OnGUI()
        {
            // Display the title image
            var titleImage = Resources.Load<Texture2D>("TitleImage");
            GUILayout.Label(titleImage, GUILayout.MaxHeight(150f));

            // Display a hint about where to get the Steam AppID
            EditorGUILayout.HelpBox("To get your Steam AppID, visit: https://partner.steamgames.com/apps/create",
                MessageType.Info);

            EditorGUILayout.LabelField("Enter Steam App ID:");
            _steamAppId = (uint)EditorGUILayout.IntField((int)_steamAppId);

            if (GUILayout.Button("Create Steam Provider"))
            {
                CreateSteamProviderAsset();
            }
        }

        private void CreateSteamProviderAsset()
        {
            // Check if the Resources folder exists, if not, create it
            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }
            // Check if the AuthSystem folder exists within Resources, if not, create it
            if (!AssetDatabase.IsValidFolder("Assets/Resources/AuthSystem"))
            {
                AssetDatabase.CreateFolder("Assets/Resources", "AuthSystem");
            }
            // Create a new instance of your SteamProvider scriptable object
            SteamProvider steamProvider = ScriptableObject.CreateInstance<SteamProvider>();
            steamProvider.AppId = _steamAppId;
            steamProvider.SetAppId();
            // Save the scriptable object to the AuthSystem folder within the Resources folder
            AssetDatabase.CreateAsset(steamProvider, "Assets/Resources/AuthSystem/SteamAuthData.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.SetDirty(steamProvider);
        }
    }
}
