using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class for player movement using a blend tree for 2D freeform movement
/// </summary>
public class PlayerMovementYBot : MonoBehaviour
{
    [SerializeField] private float forwardMoveSpeed = 2f; //Forward movement speed
    [SerializeField] private float backwardMoveSpeed = 1f; //Backward movement speed
    [SerializeField] private float rotationSpeed = 100f; //Rotation speed
    [SerializeField] private float jumpForce = 1f; //Jump force

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
    public Action OnSlide; //Calls slide animatin from other scripts
    public Action OnEmote1; //Calls emote 1 animatin from other scripts
    public Action OnEmote2; //Calls emote 2 animatin from other scripts
    public Action OnDeath; //Calls death animatin from other scripts
    private float _runMultiplier = 1f; //Multiplier for sprinting
    private readonly float MAX_RUN_SPEED = 2f; //Max multiplier for sprinting

    private void OnEnable()
    {
        PauseGame.OnGamePaused += GamePaused;
    }

    private void OnDisable()
    {
        PauseGame.OnGamePaused -= GamePaused;
    }

    
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
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        //Varies the walk speed based on the direction of movement
       if(verticalInput > 0)
            _walkSpeed = verticalInput * forwardMoveSpeed;
        else if(verticalInput < 0)
            _walkSpeed = verticalInput * backwardMoveSpeed;
        else
            _walkSpeed = 0;
            
        // Increase run speed gradually.
        if (isSprinting && _runMultiplier < MAX_RUN_SPEED)
            _runMultiplier += Time.deltaTime * MAX_RUN_SPEED;
        else if (!isSprinting && _runMultiplier > 1f)
            _runMultiplier -= Time.deltaTime * MAX_RUN_SPEED;

        // Apply the run multiplier to the walk velocity.
        _walkSpeed *= _runMultiplier;
        _strafeSpeed = horizontalInput;

        //Calculate forward and lateral movement vectors based on the input
        Vector3 forwardMovement = transform.forward * (_walkSpeed * Time.deltaTime);
        Vector3 strafeMovement = transform.right * (_strafeSpeed * Time.deltaTime);

        //Combine the movement vectors
        Vector3 movement = forwardMovement + strafeMovement;

        //Apply the movement to the rigidbody
        rb.MovePosition(rb.position + movement);

        //Rotate the player based on the mouse movement
        float mouseX = Input.GetAxis("Mouse X");
        yaw += mouseX * rotationSpeed * Time.deltaTime;

        //Apply the rotation to the player
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);

        //Check for jump input
        //Jump is related to the mass of the rigidbody, so we need to scale the force based on the mass
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //Apply the jump force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            OnJump?.Invoke();
        }

        //Check for emote input
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnEmote1?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            OnEmote2?.Invoke();
        }
        

        if(isGrounded &&  isSprinting && Input.GetKeyDown(KeyCode.LeftControl))
                OnSlide?.Invoke();

        //Raycast to check if the player is grounded
        if (Physics.Raycast(transform.position, Vector3.down, groundRaycastDistance, groundLayerMask))
        {
             //If the raycast hits the ground, set isGrounded to true
             isGrounded = true;
        }
    }

    private void GamePaused(bool isPaused)
    {
        //Locks the cursor to the center of the screen
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Box"))
            {
                OnDeath?.Invoke();
            }
        }
}
