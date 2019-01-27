using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ThrowableObject : MonoBehaviour
{
    public Rigidbody rb;
    public MeshRenderer mesh;
    public bool thrown;
    public float jumpAmount;
    public CameraVFX cameraVFX;

    private void Start()
    {
        cameraVFX = ServiceLocator.Instance.GetService<CameraVFX>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (thrown && collision.gameObject.GetComponent<EnemyController>() != null)
            rb.AddForce(Vector3.up * jumpAmount);
        else if (thrown)
        {
            cameraVFX.Shake(Mathf.Clamp(0.05f * rb.mass, 0, 0.1f), 0.1f * rb.mass, 1f);
            thrown = false;
        }
    }
}
