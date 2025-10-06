using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMotor : MonoBehaviour
{
    [Header("Movement Settings")]
    public float walkSpeed = 4.5f;
    public float sprintSpeed = 7.5f;
    public float crouchSpeed = 2.5f;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Jump / Gravity")]
    public float jumpHeight = 1.1f;
    public float gravity = -20f;
    public float groundedStick = -2f;

    [Header("Crouch Settings")]
    public float crouchHeight = 1.2f;
    public float standHeight = 2f;
    public float heightLerpSpeed = 12f;

    CharacterController controller;
    Vector3 velocity;
    bool isGrounded;
    bool isCrouching;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        controller.height = standHeight;
        controller.center = new Vector3(0, standHeight / 2f);
    }

    void Update()
    {
        // --- Grounded check ---
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0f)
            velocity.y = groundedStick;

        // --- Movement input ---
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        // --- Sprint / crouch logic ---
        bool wantsSprint = Input.GetKey(sprintKey) && !isCrouching && z > 0.1f;
        if (Input.GetKeyDown(crouchKey))
            isCrouching = !isCrouching;

        float targetSpeed = walkSpeed;
        if (wantsSprint) targetSpeed = sprintSpeed;
        if (isCrouching) targetSpeed = crouchSpeed;

        controller.Move(move * targetSpeed * Time.deltaTime);

        // --- Jump ---
        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouching)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // --- Apply gravity ---
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // --- Smooth crouch height change ---
        float desiredHeight = isCrouching ? crouchHeight : standHeight;
        controller.height = Mathf.Lerp(controller.height, desiredHeight, Time.deltaTime * heightLerpSpeed);
        controller.center = new Vector3(0, controller.height / 2f);
    }
}
