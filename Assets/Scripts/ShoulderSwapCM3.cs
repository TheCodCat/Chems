using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class ShoulderSwapCM3 : MonoBehaviour
{
    public float shoulderOffset = 0.6f;
    public float swapSpeed = 8f;

    CinemachineOrbitalFollow orbital;

    PlayerInput input;
    InputAction swapAction;

    float targetX;
    float currentX;

    void Awake()
    {
        orbital = GetComponent<CinemachineOrbitalFollow>();

        input = FindObjectOfType<PlayerInput>();
        swapAction = input.actions["ShoulderSwap"];

        currentX = shoulderOffset;
        targetX = shoulderOffset;
    }

    void Update()
    {
        if (swapAction.WasPressedThisFrame())
            targetX *= -1f;

        currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * swapSpeed);

        var offset = orbital.TargetOffset;
        offset.x = currentX;
        orbital.TargetOffset = offset;
    }
}
