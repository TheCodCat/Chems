using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerAnimator : ITickable
{
    Animator animator;
    ThirdPersonMovement playerMovement;
    PlayerInput input;
    AimController aimController;

    InputAction sprintAction;

    int speedHash;
    int isSprintingHash;
    int isAimingHash;

    public PlayerAnimator(
        Animator animator,
        ThirdPersonMovement thirdPersonMovement,
        PlayerInput playerInput,
        AimController aimController)
    {
        this.animator = animator;
        this.playerMovement = thirdPersonMovement;
        this.input = playerInput;
        this.aimController = aimController;

        sprintAction = input.actions["Sprint"];

        speedHash = Animator.StringToHash("Speed");
        isSprintingHash = Animator.StringToHash("IsSprinting");
        isAimingHash = Animator.StringToHash("IsAiming");
    }

    public void Tick()
    {
        if (!animator) return;

        UpdateMovementAnimation();
    }

    void UpdateMovementAnimation()
    {
        float speed = playerMovement != null ? playerMovement.CurrentSpeed : 0f;
        bool isSprinting = sprintAction != null && sprintAction.IsPressed();
        bool isAiming = aimController != null && aimController.IsAiming();

        if (HasParameter(speedHash))
            animator.SetFloat(speedHash, speed, 0.1f, Time.deltaTime);

        if (HasParameter(isSprintingHash))
            animator.SetBool(isSprintingHash, isSprinting);

        if (HasParameter(isAimingHash))
            animator.SetBool(isAimingHash, isAiming);
    }

    bool HasParameter(int hash)
    {
        foreach (var p in animator.parameters)
        {
            if (p.nameHash == hash)
                return true;
        }
        return false;
    }
}