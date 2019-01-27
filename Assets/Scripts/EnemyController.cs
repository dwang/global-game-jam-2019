using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyController : MonoBehaviour
{
    public int health;
    public float speed;

    [Header("Dependencies")]
    public Rigidbody rb;
    public ThrowableObject throwable;
    public ParticleSystem explosion;
    public AudioSource explosionAudio;
    public GameManager gameManager;
    public Transform target;

    private void Start()
    {
        gameManager = ServiceLocator.Instance.GetService<GameManager>();
        target = gameManager.player.transform;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            rb.velocity = transform.forward * speed;
            rb.MoveRotation(Quaternion.Euler(0, -Mathf.Atan2(target.position.z - transform.position.z, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90, 0));
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Throwable") && collision.gameObject.GetComponent<ThrowableObject>().thrown)
        {
            Rigidbody throwableObject = collision.gameObject.GetComponent<Rigidbody>();
            if (throwableObject.velocity.magnitude > 1 && rb.mass >= health)
                Death();
        } else if (throwable.thrown && rb.velocity.magnitude > 2 && rb.mass >= health)
            Death();
    }

    public void Death()
    {
        gameManager.EnemyDeath(this);
        explosion.transform.SetParent(null);
        explosionAudio.Play();
        explosion.Play();
        Destroy(explosion.gameObject, explosion.main.duration);
        Destroy(gameObject);
    }
}
