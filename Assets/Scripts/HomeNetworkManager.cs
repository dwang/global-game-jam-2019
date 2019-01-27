using System;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class HomeNetworkManager : NetworkManager
{
    public bool IsHeadless()
    {
        return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
    }

    private void Awake()
    {
        if (IsHeadless())
        {
            StartHost();
            print("headless server started");
        }
    }
}