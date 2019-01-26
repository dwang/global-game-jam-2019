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

    public void Death()
    {
        StartCoroutine(DeathEnum());
    }

    private IEnumerator DeathEnum()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSecondsRealtime(0.5f);
        cameraVFX.Shake(0.5f, 0.7f, 1f);
        Time.timeScale = 1;
    }
}
