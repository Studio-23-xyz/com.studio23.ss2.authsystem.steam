using System;
using Cysharp.Threading.Tasks;
using Steamworks;
using Steamworks.Data;
using Studio23.SS2.AuthSystem.Data;
using UnityEngine;

namespace Studio23.SS2.AuthSystem.Steam.Data
{
    [CreateAssetMenu(fileName = "Steam Auth Provider", menuName = "Studio-23/AuthSystem/Providers/Steam", order = 1)]
    public class SteamAuthProvider : ProviderBase
    {

        public uint AppId;

        void OnEnable()
        {
            _providerType = ProviderTypes.Steam;
        }


        void OnDisable()
        {
            SteamClient.Shutdown();
        }

        public override async UniTask<int> Authenticate()
        {

            if (string.IsNullOrEmpty(AppId.ToString()))
            {
                Debug.Log("Steam <color=red>App ID not found!</color> Check Resource, Should be at <color=white>Resources/AuthSystem/Providers/SteamAuthProvider.asset</color>");
                return -1;
            }

#if !UNITY_EDITOR

            if (SteamClient.RestartAppIfNecessary(AppId))
            {
                SteamClient.Shutdown();
                Application.Quit();
                return -1;
            }
#endif
            try
            {
                SteamClient.Init(AppId, true);
            }
            catch (Exception)
            {
                SteamClient.Shutdown();
                Application.Quit();
                return -1;
            }

            return 0;

        }

        public override async UniTask<UserData> GetUserData()
        {
            UserData userData = new UserData();
            userData.UserID = SteamClient.SteamId.ToString();
            userData.UserName = SteamClient.Name;
            userData.UserAvatar = await GetAvatar();

            return userData;
        }


        private async UniTask<Texture2D> GetAvatar()
        {

            Image? largeAvatar = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);

            Image image = largeAvatar.GetValueOrDefault();

            var avatar = new Texture2D((int)image.Width, (int)image.Height, TextureFormat.ARGB32, false)
                {
                    // Set filter type, or else its really blury
                    filterMode = FilterMode.Trilinear
                };

            // Flip image
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var p = image.GetPixel(x, y);
                    avatar.SetPixel(x, (int)image.Height - y, new UnityEngine.Color(p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f));
                }
            }

            avatar.Apply();
            return avatar;

           
        }





    }
}

