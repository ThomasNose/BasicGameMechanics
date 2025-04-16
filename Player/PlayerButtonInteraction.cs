using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform playerCameraTransform; // Reference to the player's camera
    [SerializeField] private float interactionDistance = 2f; // Maximum distance to interact with objects
    [SerializeField] private LayerMask interactionLayerMask; // Layer mask for interactable objects

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Check if the E key is pressed
        {
            // Perform a raycast from the player's camera
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit hit, interactionDistance, interactionLayerMask))
            {
                // Check if the object hit by the raycast has the OpenDoor component
                if (hit.transform.TryGetComponent<OpenDoor>(out OpenDoor button))
                {
                    // Call the Pushed method on the button
                    button.Pushed(0.2f, 0.5f); // Example: Sink downward by 0.2 units over 0.5 seconds
                }
            }
        }
    }
}