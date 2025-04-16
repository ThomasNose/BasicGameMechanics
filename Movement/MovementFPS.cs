using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScriptFPS : MonoBehaviour
{
    public float jumpForce = 5.0f;

    public GameObject character;
    public Camera PlayerCamera;
    private float cameraPitch = 0f; // Tracks the vertical rotation of the camera

    private Rigidbody rb;
    private bool isGrounded = true;

    public float sensitivity = 200f;
    private float moveHorizontal;
    private float moveForward;
    public float MoveSpeed = 10f;
    public float sprintSpeed = 1.5f;
    public Light CharLight;
    private bool LightOn = false;

    public Text text;

    public Transform BoxPos;
    public Transform PlayerPos;

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

        CharLight.enabled = false;
    }

    void Jump()
    {

        Vector3 velocity = rb.linearVelocity;

        // Jump logic (preserve horizontal velocity)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = jumpForce;
            isGrounded = false;
        }

        rb.linearVelocity = velocity;
    }

    void CamMovement()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;

        // Vertical rotation (pitch) - affects only the camera
        cameraPitch -= mouseY; // Subtract to invert Y-axis (common in FPS games)
        cameraPitch = Mathf.Clamp(cameraPitch, -75f, 75f); // Clamp to prevent over-rotation
        PlayerCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0, 0);

        // Horizontal rotation (yaw) - affects the player model
        rb.transform.Rotate(0, mouseX, 0);
    }

    void PlayerMovement()
    {

        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;
        if(Input.GetKey("left shift"))
        {
            Vector3 targetVelocity = movement * MoveSpeed * sprintSpeed;
            // Apply movement to the Rigidbody
            Vector3 velocity = rb.linearVelocity;
            velocity.x = targetVelocity.x;
            velocity.z = targetVelocity.z;
            rb.linearVelocity = velocity;
        }
        else{
            Vector3 targetVelocity = movement * MoveSpeed;
            // Apply movement to the Rigidbody
            Vector3 velocity = rb.linearVelocity;
            velocity.x = targetVelocity.x;
            velocity.z = targetVelocity.z;
            rb.linearVelocity = velocity;
        }

        if (text != null)
        {
            text.text = rb.linearVelocity.ToString();
        }


        // If we aren't moving and are on the ground, stop velocity so we don't slide
        if (isGrounded && moveHorizontal == 0 && moveForward == 0)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }
    }

    void FlashLight()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            LightOn = !LightOn;
            CharLight.enabled = LightOn;
            
        }

        if(Input.GetMouseButton(1))
        {
            CharLight.intensity = 2.0f;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            CharLight.intensity = 1.0f;
        }
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

    void Update()
    {   
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveForward = Input.GetAxisRaw("Vertical");

        //Debug.Log($"BoxPos Local Position: {BoxPos.localPosition}, BoxPos World Position: {BoxPos.position}, PlayerPos World Position: {PlayerPos.position}");

        Cursor.lockState = CursorLockMode.Locked;
        if (rb == null) return;
        Jump();
        CamMovement();
        PlayerMovement();
        FlashLight();
        
    }
}
