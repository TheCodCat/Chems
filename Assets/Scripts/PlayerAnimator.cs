using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Updates Animator parameters based on player movement.
/// Animation always follows gameplay — never the other way around.
/// </summary>
[RequireComponent(typeof(ThirdPersonMovement))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private ThirdPersonMovement playerMovement;

    [Header("Animation Settings")]
    [SerializeField] private float speedDampTime = 0.1f;

    PlayerInput input;
    InputAction sprintAction;

    int speedHash;
    int isSprintingHash;

    void Awake()
    {
        if (!playerMovement) playerMovement = GetComponent<ThirdPersonMovement>();

        input = GetComponent<PlayerInput>();
        sprintAction = input.actions["Sprint"];

        // Cache animator parameter hashes
        speedHash = Animator.StringToHash("Speed");
        isSprintingHash = Animator.StringToHash("IsSprinting");
    }

    void Update()
    {
        UpdateMovementAnimation();
    }

    void UpdateMovementAnimation()
    {
        // ЛОГИКА СОХРАНЕНА:
        // берём реальную скорость персонажа
        float speed = playerMovement.CurrentSpeed;

        animator.SetFloat(speedHash, speed, speedDampTime, Time.deltaTime);

        AimController aim = GetComponent<AimController>();

        bool isSprinting =
            sprintAction.IsPressed() &&
            speed > 0.1f &&
            (aim == null || !aim.IsAiming());

        animator.SetBool(isSprintingHash, isSprinting);
    }
}
