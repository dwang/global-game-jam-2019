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
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectiles") || other.CompareTag("Enemy"))
            return;
        Debug.Log(LayerMask.LayerToName(other.gameObject.layer) + " " + other.tag);
        deathParticle.transform.SetParent(null);
        deathParticle.Play();
        Destroy(deathParticle.gameObject, deathParticle.main.duration);
        Destroy(gameObject);
    }
}
