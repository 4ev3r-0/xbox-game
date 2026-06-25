using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement settings
    public float moveSpeed = 5f;
    public float turnSpeed = 150f;
    public float lookSpeed = 150f;

    // Jump settings
    public float jumpForce = 8f;
    public float gravity = 20f;

    // Drag your Camera here in the Inspector
    public Transform playerCamera;

    private CharacterController controller;

    // Current vertical speed
    private float verticalVelocity = 0f;

    // Camera up/down rotation
    private float cameraPitch = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (playerCamera == null)
        {
            Debug.LogError("Player Camera has not been assigned!");
        }
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
        RotateCamera();
    }

    void MovePlayer()
    {
        float dt = Time.deltaTime;

        // Read left stick
        float moveX = Input.GetAxis("Left Joystick X");
        float moveZ = -Input.GetAxis("Left Joystick Y");

        // Movement relative to player direction
        Vector3 moveDirection =
            (transform.right * moveX) +
            (transform.forward * moveZ);

        moveDirection *= moveSpeed;

        // Ground check
        if (controller.isGrounded)
        {
            // Keeps player stuck to ground
            verticalVelocity = -1f;

            // Jump
            if (Input.GetButtonDown("A"))
            {
                verticalVelocity = jumpForce;
            }
        }

        // Gravity
        verticalVelocity -= gravity * dt;

        moveDirection.y = verticalVelocity;

        // Move player
        controller.Move(moveDirection * dt);
    }

    void RotatePlayer()
    {
        float dt = Time.deltaTime;

        float turnInput =
            Input.GetAxis("Right Joystick X");

        transform.Rotate(
            0f,
            turnInput * turnSpeed * dt,
            0f
        );
    }

    void RotateCamera()
    {
        float dt = Time.deltaTime;

        float lookInput =
            Input.GetAxis("Right Joystick Y");

        cameraPitch += lookInput * lookSpeed * dt;

        cameraPitch = Mathf.Clamp(
            cameraPitch,
            -90f,
            90f
        );

        playerCamera.localRotation =
            Quaternion.Euler(
                cameraPitch,
                0f,
                0f
            );
    }
}