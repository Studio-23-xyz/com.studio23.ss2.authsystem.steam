using Steamworks;
using Studio23.SS2.AuthSystem.Data;
using UnityEngine;

namespace Studio23.SS2.AuthSystem.Steam.Core
{
    public class SteamProvider : ProviderBase
    {
        [SerializeField] private uint _appID;

        public uint AppId { get; set; }

        public void SetAppId()
        {
            _appID = AppId;
        }

        /// <summary>
        ///     Call this method to enable steam authentication per user
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

