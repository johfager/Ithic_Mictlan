using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class in charge of player movement using blend trees
/// </summary>
public class PlayerMovementAlien : MonoBehaviour
{
    [SerializeField] private float forwardMoveSpeed = 2f; //Forward movement speed
    [SerializeField] private float backwardMoveSpeed = 1f; //Backward movement speed
    [SerializeField] private float rotationSpeed = 100f; //Rotation speed
    [SerializeField] private float jumpForce = 10f; //Jump force

    private Rigidbody rb; //Reference to the rigidbody used for movement

    private RaycastHit hit; // Used for raycasting to check if the player is grounded
    [SerializeField] private bool isGrounded; //Prevent double jump
    [SerializeField] private float groundRaycastDistance = 0.2f; //Distance of raycast to check if the player is grounded
    [SerializeField] private LayerMask groundLayerMask; //Assign the ground layer to this in the inspector

    private float _walkSpeed; //Current walk speed
    public float WalkSpeed => _walkSpeed; //Public getter for walk speed

    private float _strafeSpeed; //Current strafe speed
    public float StrafeSpeed => _strafeSpeed; //Public getter for strafe speed

    private float yaw = 0f; //Current yaw rotation

    public Action OnJump; //Calls jump animatin from other scripts
    public Action OnDance; //Calls dance animation from other scripts
    public Action OnDeath; //Calls death animation from other scripts

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isGrounded = true;
        //Locks the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Input handling for movement and rotation
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Varies the walk speed based on the direction of movement
        if (verticalInput > 0)
        {
            _walkSpeed = verticalInput * forwardMoveSpeed;
        }
        else if (verticalInput < 0)
        {
            _walkSpeed = verticalInput * backwardMoveSpeed;
        }
        else
        {
            _walkSpeed = 0;
        }

        _strafeSpeed = horizontalInput;

        //Calculate the movement vector based on the input
        // Vector3 movement = transform.forward * (_walkSpeed * Time.deltaTime);

        //Calculate forwarrd and strafe movement vectors based on the input
        Vector3 forwardMovement = transform.forward * (_walkSpeed * Time.deltaTime);
        Vector3 strafeMovement = transform.right * (_strafeSpeed * Time.deltaTime);

        //Combine the movement vectors
        Vector3 movement = forwardMovement + strafeMovement;

        //Apply the movement to the rigidbody
        rb.MovePosition(rb.position + movement);

        //Rotate the player based on the mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        yaw += mouseX * rotationSpeed * Time.deltaTime;

        //Apply the yaw rotation to the player
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        //Check for jump input
        //Jump is related to the mass of the rigidbody, so we need to scale the force based on the mass
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Apply the jump force
            rb.AddForce(Vector3.up * jumpForce * rb.mass, ForceMode.Impulse);
            //Set isGrounded to false so the player can't double jump
            // isGrounded = false;
            OnJump?.Invoke();
        }

        //Check for dance input
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnDance?.Invoke();
        }

        //Check for death input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnDeath?.Invoke();
        }

        //Raycast to check if the player is grounded
        // if (Physics.Raycast(transform.position, Vector3.down, groundRaycastDistance, groundLayerMask))
        // {
        //     //If the raycast hits the ground, set isGrounded to true
        //     isGrounded = true;
        // }

    }
}
