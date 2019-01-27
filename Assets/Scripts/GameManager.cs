using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class GameManager : NetworkBehaviour, IService
{
    public CameraVFX cameraVFX;
    public CameraController cameraController;
    public Image healthImageFill;
    public Image chargeUpImageFill;
    public GameObject[] enemies;
    public GameObject house;


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

    private void Start()
    {
        if (isServer)
        {
            GameObject go;
            for (int i = 0; i < 5; i++)
            {
                go = Instantiate(enemies[0], new Vector3(i * 2, 1, 0), Quaternion.identity);
                NetworkServer.Spawn(go);
            }
            go = Instantiate(house, new Vector3(0, 1, 2), Quaternion.Euler(-90, 0, 0));
            NetworkServer.Spawn(go);
        }
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<GameManager>();
    }
}
