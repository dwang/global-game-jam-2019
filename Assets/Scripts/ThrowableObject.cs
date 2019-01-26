using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public Rigidbody rb;
    public bool thrown;
    public float jumpAmount;

    private void OnCollisionEnter(Collision collision)
    {
        if (thrown && collision.gameObject.GetComponent<EnemyController>() != null)
            rb.AddForce(Vector3.up * jumpAmount);
        else
            thrown = false;
    }
}
