using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public float speed;

    [Header("Dependencies")]
    public Rigidbody rb;
    public ParticleSystem explosion;
    public GameManager gameManager;
    public Transform target;

    private void Start()
    {
        gameManager = ServiceLocator.Instance.GetService<GameManager>();
    }

    private void Update()
    {
        transform.LookAt(target);
        rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            target = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Throwable"))
        {
            Rigidbody throwableObject = collision.gameObject.GetComponent<Rigidbody>();
            if (throwableObject.velocity.magnitude > 1 && rb.mass >= health)
                Death();
        } else if (rb.velocity.magnitude > 1 && rb.mass >= health)
            Death();
    }

    public void Death()
    {
        explosion.transform.SetParent(null);
        explosion.Play();
        Destroy(explosion.gameObject, explosion.main.duration);
        Destroy(gameObject);
    }
}
