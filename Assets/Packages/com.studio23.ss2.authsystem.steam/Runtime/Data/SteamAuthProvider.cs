using System;
using Cysharp.Threading.Tasks;
using Steamworks;
using Studio23.SS2.AuthSystem.Data;
using UnityEngine;

namespace Studio23.SS2.AuthSystem.Steam.Data
{
    public class SteamAuthProvider : AuthProviderBase
    {
        public uint AppId;

        public override async UniTask<int> Authenticate()
        {
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
                SteamClient.Init(AppId);
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
            var userData = new UserData();
            userData.UserID = SteamClient.SteamId.ToString();
            userData.UserName = SteamClient.Name;
            userData.UserNickname = SteamClient.Name;
            userData.UserAvatar = await GetAvatar();

            return userData;
        }


        private async UniTask<Texture2D> GetAvatar()
        {
            var largeAvatar = await SteamFriends.GetLargeAvatarAsync(SteamClient.SteamId);

            var image = largeAvatar.GetValueOrDefault();

            var avatar = new Texture2D((int)image.Width, (int)image.Height, TextureFormat.ARGB32, false)
            {
                // Set filter type, or else its really blury
                filterMode = FilterMode.Trilinear
            };

            // Flip image
            for (var x = 0; x < image.Width; x++)
            for (var y = 0; y < image.Height; y++)
            {
                var p = image.GetPixel(x, y);
                avatar.SetPixel(x, (int)image.Height - y,
                    new Color(p.r / 255.0f, p.g / 255.0f, p.b / 255.0f, p.a / 255.0f));
            }

            avatar.Apply();
            return avatar;
        }
    }
}