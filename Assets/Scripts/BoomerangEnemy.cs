using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangEnemy : EnemyController
{
    public bool fired;
    public float reloadTime;
    public GameObject projectilePrefab;
    public ParticleSystem muzzleFlash;
    public AudioSource muzzleAudio;

    private void FixedUpdate()
    {
        if (target != null)
        {
            rb.velocity = transform.forward * speed;
            rb.MoveRotation(Quaternion.Euler(new Vector3(0, -Mathf.Atan2(target.position.z - transform.position.z, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90, 0)));
            if (!fired)
                StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        fired = true;
        muzzleFlash.Play();
        muzzleAudio.Play();
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 0))).GetComponent<BoomerangProjectile>().center = transform;
        yield return new WaitForSeconds(reloadTime);
        fired = false;
    }
}
