using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IService
{
    public CameraVFX cameraVFX;
    public CameraController cameraController;
    public Image healthImageFill;
    public Image chargeUpImageFill;

    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<GameManager>();
    }
}
