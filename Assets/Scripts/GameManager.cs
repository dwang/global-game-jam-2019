using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour, IService
{
    public CameraVFX cameraVFX;
    public CameraController cameraController;
    public Image healthImageFill;
    public Image chargeUpImageFill;


    bool IsHeadless()
    {
        return SystemInfo.graphicsDeviceType == GraphicsDeviceType.Null;
    }

        private void Awake()
    {
        ServiceLocator.Instance.AddService(this);

        if (IsHeadless())
            NetworkManager.singleton.StartServer();
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<GameManager>();
    }
}
