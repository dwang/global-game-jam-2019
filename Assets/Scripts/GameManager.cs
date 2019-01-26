using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IService
{
    public CameraVFX cameraVFX;

    private void Awake()
    {
        ServiceLocator.Instance.AddService(this);
    }
}
