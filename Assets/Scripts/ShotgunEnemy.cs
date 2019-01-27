using System;
using System.Collections;
using UnityEngine;

public class ShotgunEnemy : EnemyController
{
    public bool fired;
    public float reloadTime;
    public GameObject projectilePrefab;
    public ParticleSystem muzzleFlash;
    public AudioSource muzzleAudio;

    private void Update()
    {
        if (target != null)
        {
            rb.MoveRotation(Quaternion.Euler(0, -Mathf.Atan2(target.position.z - transform.position.z, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90, 0));
            if (!fired)
                StartCoroutine(Fire());
        }
    }

    private IEnumerator Fire()
    {
        fired = true;
        muzzleFlash.Play();
        muzzleAudio.Play();
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 10, 0)));
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles - new Vector3(0, 10, 0)));
        yield return new WaitForSeconds(reloadTime);
        fired = false;
    }
}
