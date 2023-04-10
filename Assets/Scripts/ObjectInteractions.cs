using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractions : MonoBehaviour
{
    private Rigidbody objectRigidbody;
    private Transform objectGrabPointTransform;
    private string objectName;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectName = this.gameObject.tag;
    }

    public void Grab(Transform objGrabPointTransformer)
    {
        this.gameObject.tag = "DefaultApple";
        this.objectGrabPointTransform = objGrabPointTransformer;    // This is also called in an update function from the other script so this is also updating every frame
        objectRigidbody.useGravity = false;
        this.gameObject.layer = 8;
    }

    public void Drop()
    {
        this.gameObject.tag = objectName;
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;
        this.gameObject.layer = 7;
    }

    private void Update()
    {
        if(this.objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;

            // To smoothe the movement better, we'll use Lerp. This interpolates the object between two position with Time.deltaTime. The current with the target.
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);

            // This moves the Rigidbody to the position that we want, which is the position of the empty gameobject called from the other script until we assign it equal to null to drop
            objectRigidbody.MovePosition(this.objectGrabPointTransform.position);
        }
    }
}
