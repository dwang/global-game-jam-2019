using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{
    public Rigidbody rb;
    public float speed;
    public ParticleSystem deathParticle;

    private void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            Damage(other.GetComponent<PlayerController>());
        deathParticle.transform.SetParent(null);
        deathParticle.Play();
        Destroy(deathParticle.gameObject, deathParticle.main.duration);
    }
}
