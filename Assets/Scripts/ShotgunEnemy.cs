using System;
using System.Collections;
using UnityEngine;

public class ShotgunEnemy : EnemyController
{
    public bool fired;
    public float reloadTime;
    public float chargeTime;
    public GameObject projectilePrefab;

    private void Update()
    {
        transform.LookAt(target);
        rb.MovePosition(rb.position + transform.forward * speed * Time.deltaTime);
        if (target != null && !fired)
            StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        fired = true;
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles + new Vector3(0, 10, 0)));
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles));
        Instantiate(projectilePrefab, transform.position, Quaternion.Euler(transform.eulerAngles - new Vector3(0, 10, 0)));
        yield return new WaitForSeconds(reloadTime);
        fired = false;
    }
}
