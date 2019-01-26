﻿using System.Collections;
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
            if (throwableObject.velocity.magnitude > 2 && rb.mass >= health)
                Death();
        } else if (rb.velocity.magnitude > 2 && rb.mass >= health)
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
