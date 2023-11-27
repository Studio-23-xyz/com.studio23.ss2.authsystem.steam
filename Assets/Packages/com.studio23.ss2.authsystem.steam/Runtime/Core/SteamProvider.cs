using Steamworks;
using Studio23.SS2.AuthSystem.Data;
using UnityEngine;

namespace Studio23.SS2.AuthSystem.Steam.Core
{
    public class SteamProvider : ProviderBase
    {
        public uint AppId;

        public void SetAppId(uint appId)
        {
            AppId = appId;
        }

        /// <summary>
        /// Call this method to enable steam authentication per user
        /// </summary>
        public override void Authenticate()
        {
            if (SteamClient.RestartAppIfNecessary(AppId))
            {
                SteamClient.Shutdown();
                Application.Quit();
            }

            SteamClient.Init(AppId);
        }
    }
}

