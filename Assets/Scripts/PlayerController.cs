using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float turnSpeed = 10;
    public int health;
    public int maxHealth;
    public Rigidbody rb;
    public Transform holdObjectTransform;
    public ThrowableObject heldObject;
    public float forwardThrowPower;
    public float upwardThrowPower;
    public float pickUpRadius;
    public bool canThrow = false;
    public float heldObjectFolllowSmooth;
    public Image chargeUpImageFill;
    public Image healthFill;
    private Vector3 offset;
    private Vector3 smoothVelocity;
    private float chargeSmoothVelocity;

    private void Update()
    {
        healthFill.fillAmount = Mathf.SmoothDamp(healthFill.fillAmount, (float) health / maxHealth, ref chargeSmoothVelocity, 0.75f);
        Vector3 movement = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rb.MovePosition(rb.position + movement * Time.deltaTime);

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0));

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (canThrow && heldObject != null)
                StartCoroutine(ThrowObject());
            else if (heldObject == null)
            {
                var colliders = Physics.OverlapSphere(transform.position, pickUpRadius).Where(x => x.GetComponent<ThrowableObject>() != null && !ReferenceEquals(x.gameObject, gameObject));
                if (colliders.Count() > 0)
                    PickUpObject(colliders.First().GetComponent<ThrowableObject>());
            }
        }

        if (heldObject != null)
            heldObject.transform.position = Vector3.SmoothDamp(heldObject.transform.position, holdObjectTransform.position + offset, ref smoothVelocity, heldObjectFolllowSmooth);
    }

    public void PickUpObject(ThrowableObject pickUpGameObject)
    {
        if (heldObject == null)
        {
            heldObject = pickUpGameObject;

            heldObject.rb.velocity = Vector3.zero;
            offset = new Vector3(0, heldObject.mesh.bounds.size.magnitude * heldObject.transform.lossyScale.x / 2, 0);
            heldObject.rb.useGravity = false;
            heldObject.GetComponent<Collider>().enabled = false;
            canThrow = true;
        }
    }

    public IEnumerator ThrowObject()
    {
        canThrow = false;
        heldObject.GetComponent<Collider>().enabled = true;
        heldObject.rb.useGravity = true;
        heldObject.thrown = true;
        float time = 0;
        while (Input.GetKey(KeyCode.Space) && time < 5)
        {
            time += Time.deltaTime;
            chargeUpImageFill.fillAmount = time / 5;
            yield return new WaitForEndOfFrame();
        }
        chargeUpImageFill.fillAmount = 1;
        if (time > 5)
            time = 5;
        heldObject.rb.AddForce(((transform.forward * forwardThrowPower) + (transform.up * upwardThrowPower)) * (time / 5), ForceMode.Impulse);
        heldObject = null;
        yield return new WaitForSeconds(1f);
        chargeUpImageFill.fillAmount = 0;
    }
}
