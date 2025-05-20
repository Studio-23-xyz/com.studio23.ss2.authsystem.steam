using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.Events;


public class SteamOverlayUtility : MonoBehaviour
{

    public static SteamOverlayUtility Instance;
    public UnityEvent<bool> OnSteamOverlayToggled;

    private void Awake()
    {
        Instance = this;
    }

    public void Initialize()
    {
        Steamworks.SteamFriends.OnGameOverlayActivated += SteamFriends_OnGameOverlayActivated;
    }

    private void OnDestroy()
    {
        Steamworks.SteamFriends.OnGameOverlayActivated -= SteamFriends_OnGameOverlayActivated;
    }

    private void SteamFriends_OnGameOverlayActivated(bool obj)
    {
        OnSteamOverlayToggled?.Invoke(obj);
    }

}
