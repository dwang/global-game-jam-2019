using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseDrop : MonoBehaviour
{
    public AnimationCurve animationCurve;
    public float duration;
    public float height;
    public Transform player;
    public BoxCollider boxCollider;
    public string explosiveLayer;
    public Transform explosionPos;
    [Header("Explosion")]
    public float power;
    public float radius;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = player.position + new Vector3(0, 20, 0);
            StopAllCoroutines();
            StartCoroutine(DropHouseEnum());
        }
    }

    private IEnumerator DropHouseEnum()
    {
        boxCollider.enabled = false;
        float time = 0;
        while (time <= duration)
        {
            time += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, animationCurve.Evaluate(time / duration) * height, transform.position.z);
            yield return new WaitForEndOfFrame();
        }
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            
            if (rb != null)
                rb.AddExplosionForce(power, explosionPos.position, radius, 0, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(1f);
        boxCollider.enabled = true;
    }
}
