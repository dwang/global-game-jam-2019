using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public Rigidbody rb;
    public ParticleSystem explosion;
    public GameManager gameManager;
    public float speed;
    public Transform player;

    private void Start()
    {
        gameManager = ServiceLocator.Instance.GetService<GameManager>();
    }

    private void Update()
    {
        transform.LookAt(player);
        rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Throwable"))
        {
            Rigidbody throwableObject = collision.gameObject.GetComponent<Rigidbody>();
            if (throwableObject.velocity.magnitude > health)
                Death();
        } else if (rb.velocity.magnitude > health)
        {
            Death();
        }
    }

    public void Death()
    {
        explosion.transform.SetParent(null);
        explosion.Play();
        gameManager.Death();
        Destroy(explosion.gameObject, explosion.main.duration);
        Destroy(gameObject);
    }
}
