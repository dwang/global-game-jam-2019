using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public float turnSpeed = 10;
    public Rigidbody rb;
    public Transform holdObjectTransform;
    public Rigidbody heldObject;
    public float forwardThrowPower;
    public float upwardThrowPower;
    public float pickUpRadius;
    public bool canThrow = false;
    public float heldObjectFolllowSmooth;
    private Vector3 offset;
    private Vector3 smoothVelocity;

    private void Update()
    {
        Vector3 movement = transform.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rb.MovePosition(rb.position + movement);

        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, Input.GetAxis("Horizontal") * turnSpeed, 0));

        if (Input.GetKeyDown(KeyCode.Space) && canThrow)
            ThrowObject();
        else if (Input.GetKeyDown(KeyCode.Space) && heldObject == null)
        {
            var colliders = Physics.OverlapSphere(transform.position, pickUpRadius).Where(x => x.GetComponent<Rigidbody>() != null && !x.CompareTag("Player"));
            if (colliders.Count() > 0)
                PickUpObject(colliders.First().GetComponent<Rigidbody>());
        }
        if (heldObject != null)
            heldObject.transform.position = Vector3.SmoothDamp(heldObject.transform.position, holdObjectTransform.position + offset, ref smoothVelocity, heldObjectFolllowSmooth);
    }

    public void PickUpObject(Rigidbody pickUpGameObject)
    {
        if (heldObject == null)
        {
            heldObject = pickUpGameObject;

            offset = new Vector3(0, heldObject.GetComponent<MeshRenderer>().bounds.size.magnitude / 2, 0);
            heldObject.useGravity = false;
            heldObject.GetComponent<Collider>().enabled = false;
            canThrow = true;
        }
    }

    public void ThrowObject()
    {
        canThrow = false;
        heldObject.GetComponent<Collider>().enabled = true;
        heldObject.useGravity = true;
        heldObject.AddForce((transform.forward * forwardThrowPower) + (transform.up * upwardThrowPower), ForceMode.Impulse);
        heldObject = null;
    }
}
