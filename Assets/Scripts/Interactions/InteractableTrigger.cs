using UnityEngine;

public class InteractableTrigger : MonoBehaviour
{
    public IInteractable interactable;

    void Awake()
    {
        if (interactable == null)
            interactable = GetComponent<IInteractable>();
    }
}