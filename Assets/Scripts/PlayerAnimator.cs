using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Updates Animator parameters based on player movement.
/// Animation always follows gameplay — never the other way around.
/// </summary>
[RequireComponent(typeof(Animator))]
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
        if (!animator) animator = GetComponent<Animator>();
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
        // ËÎÃÈÊÀ ÑÎÕÐÀÍÅÍÀ:
        // áåð¸ì ðåàëüíóþ ñêîðîñòü ïåðñîíàæà
        float speed = playerMovement.CurrentSpeed;

        animator.SetFloat(speedHash, speed, speedDampTime, Time.deltaTime);

        bool isSprinting =
            sprintAction.IsPressed() &&
            speed > 0.1f;

        animator.SetBool(isSprintingHash, isSprinting);
    }
}
