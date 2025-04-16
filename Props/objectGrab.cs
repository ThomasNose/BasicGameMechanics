using UnityEngine;

public class objectGrab : MonoBehaviour
{
    private Rigidbody objectRigidbody; // Reference to the object's Rigidbody component
    private Transform objectGrabPointTransform; // Reference to the point where the object will be grabbed

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>(); // Get the Rigidbody component attached to this object
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform; // Set the grab point transform
        objectRigidbody.useGravity = false; // Disable gravity on the object
    }

    public void Release(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = null; // Clear the grab point transform
        objectRigidbody.useGravity = true; // Enable gravity on the object
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 20f; // Speed of interpolation
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition); // Smoothly move the object to the grab point
        }
    }
}