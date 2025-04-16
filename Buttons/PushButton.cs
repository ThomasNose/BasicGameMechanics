using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private Transform ObjectTransform; // Make button disappear after interaction
    public Vector3 direction; // Direction in which the button sinks
    public Transform Door; // Reference to the door object
    public Vector3 DoorRotateDirection; // Direction in which the door rotates

    private void Awake()
    {
        ObjectTransform = GetComponent<Transform>(); // Get the Transform component of the button
    }

    public void Pushed(float sinkDistance, float sinkDuration)
    {
        StartCoroutine(SinkButton(sinkDistance, sinkDuration));
    }

    private System.Collections.IEnumerator SinkButton(float distance, float duration)
    {
        Vector3 startPosition = ObjectTransform.position;
        Vector3 targetPosition = startPosition + direction.normalized * distance;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            ObjectTransform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ObjectTransform.position = targetPosition;

        // Destroy the button after sinking
        //Destroy(gameObject);

        RotateDoor();
    }

    private void RotateDoor()
    {
        if (Door != null)
        {
            // Apply the rotation to the door
            StartCoroutine(LerpRotateDoor());
        }
        else
        {
            Debug.LogWarning("Door reference is not set!");
        }
    }

    private System.Collections.IEnumerator LerpRotateDoor()
{
    Quaternion startRotation = Door.rotation;
    Quaternion targetRotation = Quaternion.Euler(DoorRotateDirection); // Absolute Rotation
    float duration = 5f; // Duration of the rotation in seconds
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        Door.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime / duration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    Door.rotation = targetRotation; // Ensure the final rotation is set

}
}