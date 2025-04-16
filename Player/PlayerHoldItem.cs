using UnityEngine;

public class PlayerHoldItem : MonoBehaviour
{

    [SerializeField] private Transform playerCameraTransform; // Reference to the player's camera transform
    [SerializeField] private Transform objectGrabPointTransform; // Reference to the point where the object will be grabbed
    [SerializeField] private LayerMask pickUpLayerMask; // Layer mask for the pickup items

    // private bool isHoldingObject = false; // Flag to check if the player is holding an object

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        //if (Input.GetKeyDown(KeyCode.E)) // E key
        {   
            float pickupDistance = 2f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out objectGrab objectGrab))
                {
                    objectGrab.Grab(objectGrabPointTransform); // Call the Grab method on the objectGrab component
                }
            }
            //if (Input.GetMouseButtonDown(1)) // Right mouse button
            //{
                // So while holding an object with left click, we hold right click to rotate it
                // We will need to disable the player camera movement while holding right click
                // so that we only rotate the object and not the player camera.
            //    Debug.Log("Holding right click to rotate the object.");
            //}
        }
        else if (Input.GetMouseButtonUp(0))
        {   
            float pickupDistance = 2f;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out objectGrab objectGrab))
                {
                    objectGrab.Release(objectGrabPointTransform); // Call the Grab method on the objectGrab component
                }
            }
        }
    }
}