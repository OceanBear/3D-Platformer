using UnityEngine;
using System.Collections;
using UnityEngine.Windows;

public class Player : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private float maxSpeed;            // Maximum horizontal speed
    [SerializeField] private float accelerationRate;    // Rate at which the player accelerates (units per second^2)
    [SerializeField] private float decelerationRate;    // Rate at which the player decelerates (units per second^2)
    [SerializeField] private float jumpForce;           // Jump force applied when jumping
    [SerializeField] private Transform freeLookCamera;  // Assign the FreeLook Camera's Transform in the Inspector
    
    private Rigidbody rb;
    private Vector3 movementInput; // Raw movement input from the InputManager
    private bool isGrounded = false;      // Is player on the ground
    private bool doubleJumpUsed = false;  // Is double jump used
    //private int groundContacts = 0;       // How many contacts with the ground

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Adding MovePlayer as a listener to OnMove event
        inputManager.OnMove.AddListener(ReceiveMovementInput);
        inputManager.OnSpacePressed.AddListener(Jump);
        rb = GetComponent<Rigidbody>();
    }
    private void ReceiveMovementInput(Vector3 input)
    {
        movementInput = input;
    }
    void FixedUpdate()
    {
        // Calculate the camera's forward and right directions on the horizontal plane
        Vector3 camForward = freeLookCamera.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = freeLookCamera.right;
        camRight.y = 0;
        camRight.Normalize();

        // Compute the desired horizontal velocity based on input and camera orientation
        Vector3 desiredDirection = (movementInput.x * camRight + movementInput.z * camForward);
        // Normalize to prevent faster diagonal movement; if no input, desiredDirection is zero
        desiredDirection = desiredDirection.magnitude > 0 ? desiredDirection.normalized : Vector3.zero;
        Vector3 desiredVelocity = desiredDirection * maxSpeed;

        // Extract the current horizontal velocity (ignoring the vertical component)
        Vector3 currentVelocity = rb.linearVelocity;
        Vector3 currentHorizontalVelocity = new Vector3(currentVelocity.x, 0, currentVelocity.z);

        // Choose the appropriate rate: accelerate if input exists; otherwise, decelerate
        float rate = (movementInput.magnitude > 0) ? accelerationRate : decelerationRate;

        // Gradually change the horizontal velocity toward the desired velocity
        Vector3 newHorizontalVelocity = Vector3.MoveTowards(currentHorizontalVelocity, desiredVelocity, rate * Time.fixedDeltaTime);

        // Apply the updated horizontal velocity while preserving the current vertical velocity
        rb.linearVelocity = new Vector3(newHorizontalVelocity.x, rb.linearVelocity.y, newHorizontalVelocity.z);
    }

    private void Jump()
    {
        // If the player is grounded, perform the first jump
        if (isGrounded)
        {
            // Reset vertical velocity before applying jump force
            Vector3 vel = rb.linearVelocity;
            vel.y = 0;
            rb.linearVelocity = vel;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            // Start coroutine to disable grounded state after a short delay
            StartCoroutine(DisableGrounded());
        }
        else if (!doubleJumpUsed)
        {
            // Reset vertical velocity before applying jump force
            Vector3 vel = rb.linearVelocity;
            vel.y = 0;
            rb.linearVelocity = vel;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            doubleJumpUsed = true;
        }
    }
    private IEnumerator DisableGrounded()
    {
        // Wait for a short duration to ensure the jump has been initiated
        yield return new WaitForSeconds(0.1f);
        isGrounded = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is tagged as "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            doubleJumpUsed = false;
        }
    }

}
