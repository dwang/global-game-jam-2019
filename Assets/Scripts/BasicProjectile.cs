using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : Projectile
{
    public Rigidbody rb;
    public float speed;
    public ParticleSystem deathParticle;
    public AudioSource deathSound;

    private void Start()
    {
        Destroy(gameObject, 10);
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectiles") || other.CompareTag("Enemy"))
            return;
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerController>().Damage(damage);
        Debug.Break();
        deathParticle.transform.SetParent(null);
        deathParticle.Play();
        deathSound.Play();
        Destroy(deathParticle.gameObject, deathParticle.main.duration);
        Destroy(gameObject);
    }
}
