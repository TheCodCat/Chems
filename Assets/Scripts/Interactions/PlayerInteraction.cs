using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 3f;
    public InputActionProperty interactKey;

    Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        interactKey.action.Enable();

        interactKey.action.performed += Action_performed;
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
            TryInteract();
    }

    void TryInteract()
    {
        if (!mainCam) return;

        Ray ray = new Ray(mainCam.transform.position, mainCam.transform.forward * interactDistance);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.black, 20);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.Interact();
            }
        }
    }
}