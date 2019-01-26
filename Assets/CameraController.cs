using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [Header("Settings")]
    public float movementSmoothTime;
    public float zoomSmoothTime;
    public float minSize = 5f;
    public float buffer;

    public float size;
    public Vector3 position;

    [Header("Dependencies")]
    public Camera cam;
    private Vector3 velocity;
    private float velocity2;

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position, ref velocity, movementSmoothTime * Time.deltaTime);
    }
}