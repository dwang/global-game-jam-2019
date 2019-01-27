﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleEnemy : EnemyController
{
    public bool fired;
    public float reloadTime;
    public GameObject projectilePrefab;
    public ParticleSystem muzzleFlash;

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
        for (int i = 0; i < 360; i += 30)
            Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, i, 0)));
        yield return new WaitForSeconds(reloadTime);
        fired = false;
    }
}
