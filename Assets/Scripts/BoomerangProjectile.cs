using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangProjectile : Projectile
{
    public Rigidbody rb;
    public float height;
    public float width;
    public float speed;
    public float time;
    public Transform center;
    public ParticleSystem deathParticle;
    public AudioSource deathSound;

    private void Start()
    {
        Destroy(gameObject, 10);
    }

    public void Update()
    {
        if (time <= 0)
            DestroyProjectile();
        time -= Time.deltaTime * speed;
        rb.MovePosition(center.position + new Vector3(width * Mathf.Cos(Mathf.Deg2Rad * time), 0,
           height * Mathf.Sin(Mathf.Deg2Rad * time)));
        Debug.Log(rb.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Projectiles") || other.CompareTag("Enemy"))
            return;
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.GetComponent<PlayerController>().Damage(damage);
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        deathParticle.transform.SetParent(null);
        deathParticle.Play();
        deathSound.Play();
        Destroy(deathParticle.gameObject, deathParticle.main.duration);
        Destroy(gameObject);
    }
}
