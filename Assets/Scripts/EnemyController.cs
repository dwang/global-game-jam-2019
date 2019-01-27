﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

<<<<<<< HEAD
public class EnemyController : MonoBehaviour
=======
public class EnemyController :  MonoBehaviour
>>>>>>> 95b668a9fd1137fce2a6add11fd2c3bcc4ee2c24
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
    }

    private void Update()
    {
        if (target != null)
        {
            rb.MoveRotation(Quaternion.Euler(0, -Mathf.Atan2(target.position.z - transform.position.z, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90, 0));
            rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
        }
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
