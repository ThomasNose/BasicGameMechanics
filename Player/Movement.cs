using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float speed = 10.0f;
    public float rotationSpeed = 100f; // Speed at which the character rotates
    public float jumpForce = 5.0f;

    public float BodySize = 1.0f;

    public int jumps = 0;

    public GameObject character;
    private Rigidbody rb;
    private bool isGrounded = true;

    void Start()
    {
        if (character != null)
        {
            rb = character.GetComponent<Rigidbody>();
        }
        else
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (rb == null) return;

        Vector3 velocity = rb.linearVelocity;

        // Allow movement only on the ground
        if (isGrounded)
        {
            float Rotate = 0f;
            float moveZ = 0f;

            if (Input.GetKey(KeyCode.D)) Rotate = 1f;
            if (Input.GetKey(KeyCode.A)) Rotate = -1f;
            if (Input.GetKey(KeyCode.W)) moveZ = 1f;
            if (Input.GetKey(KeyCode.S)) moveZ = -0.25f;

            // Calculate the movement direction relative to the player's current facing
            Vector3 moveDirection = transform.forward * moveZ;

            // Apply the movement to the rigidbody
            velocity.x = moveDirection.x * speed;
            velocity.z = moveDirection.z * speed;
            
            transform.Rotate(Vector3.up * Rotate * rotationSpeed * Time.deltaTime);
        }

        // Jump logic (preserve horizontal velocity)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpForce;
            jumps += 1;
            isGrounded = false;
        }

        rb.linearVelocity = velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            // Check if the normal is pointing upwards and the collision is not too slanted
            if (contact.normal.y > 0.5f && Mathf.Abs(contact.normal.x) < 0.1f && Mathf.Abs(contact.normal.z) < 0.1f)
            {
                isGrounded = true;
                return;  // Exit once we know we're grounded
            }
        }
    }
}
