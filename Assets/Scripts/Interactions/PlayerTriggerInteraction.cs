using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTriggerInteraction : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject interactHintUI;

    [Header("Input")]
    [SerializeField] private InputActionProperty interactAction;

    IInteractable currentInteractable;

    void Awake()
    {
        interactAction.action.Enable();
        interactAction.action.performed += OnInteract;
    }

    void OnDestroy()
    {
        interactAction.action.performed -= OnInteract;
    }

    void OnInteract(InputAction.CallbackContext ctx)
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var trigger = other.GetComponent<InteractableTrigger>();
        if (trigger != null)
        {
            currentInteractable = trigger.interactable;
            interactHintUI.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var trigger = other.GetComponent<InteractableTrigger>();
        if (trigger != null && trigger.interactable == currentInteractable)
        {
            currentInteractable = null;
            interactHintUI.SetActive(false);
        }
    }
}