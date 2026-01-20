using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    public float walkSpeed = 4f;
    public float sprintSpeed = 7f;
    public float rotationSpeed = 12f;
    public float gravity = -20f;

    public float CurrentSpeed { get; private set; }


    CharacterController controller;
    PlayerInput input;
    Transform cam;


    InputAction moveAction;
    InputAction sprintAction;

    Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        input = GetComponent<PlayerInput>();
        cam = Camera.main.transform;


        moveAction = input.actions["Move"];
        sprintAction = input.actions["Sprint"];
    }

    void Update()
    {
        HandleMovement();
        HandleGravity();
        Debug.Log(CurrentSpeed);

    }

    void HandleMovement()
    {
        Vector2 inputVector = moveAction.ReadValue<Vector2>();

        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * inputVector.y + camRight * inputVector.x;

        float speed = sprintAction.IsPressed() ? sprintSpeed : walkSpeed;

        controller.Move(moveDir * speed * Time.deltaTime);
        CurrentSpeed = controller.velocity.magnitude;

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Vector3 lookDir = camForward;
            lookDir.y = 0;

            Quaternion targetRot = Quaternion.LookRotation(lookDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
        }

    }

    void HandleGravity()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
