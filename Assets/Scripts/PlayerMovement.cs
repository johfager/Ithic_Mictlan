using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public HeroStats playerStats; // Reference to the scriptable object
    public Transform playerCamera; // Reference to the player's camera
    public float mouseSensitivity = 2.0f; // Mouse sensitivity for camera movement

    private CharacterController characterController;
    private Vector3 moveDirection;
    private float verticalVelocity;
    private float rotX = 0; // Current camera rotation around the X-axis
    private float currentSpeed;
    private bool isSprinting;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (playerStats != null)
        {
            Debug.Log("PlayerStats successfully loaded");
        }
        else
        {
            Debug.LogError(
                "Failed to load the scriptable object. Check the path and ensure it's in the Resources folder.");
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovementInput();
        HandleMouseLook();
    }

    private void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput);
        inputDirection = transform.TransformDirection(inputDirection);
        inputDirection.Normalize();

        // Sprinting toggle
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        // Gradual sprint acceleration
        float targetSpeed = isSprinting
            ? playerStats.movementAttributes.movementSpeed * playerStats.movementAttributes.sprintSpeedMultiplier
            : playerStats.movementAttributes.movementSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed,
            playerStats.movementAttributes.sprintGradualAcceleration * Time.deltaTime);

        // Apply movement speed
        moveDirection = inputDirection * currentSpeed;

        // Apply gravity
        ApplyGravity();

        // Jumping
        if (characterController.isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = playerStats.movementAttributes.jumpHeight;
            }
        }

        // Apply vertical velocity
        moveDirection.y = verticalVelocity;

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            verticalVelocity -= playerStats.movementAttributes.gravitySpeed * Time.deltaTime;
        }
        else
        {
            verticalVelocity = -playerStats.movementAttributes.gravitySpeed * Time.deltaTime;
        }
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the character (Y-axis)
        transform.Rotate(Vector3.up * mouseX);

        // Rotate the camera (X-axis)
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, -90f, 90f); // Limit camera rotation to prevent flipping

        // Apply camera rotation
        playerCamera.localRotation = Quaternion.Euler(rotX, 0, 0);
    }
}
