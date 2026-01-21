using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    bool isOn = false;
    [SerializeField] private GameObject target;

    public void Interact()
    {
        if (isOn)
        {
            target.SetActive(false);
            isOn = false;
        }

        else
        {
            target.SetActive(true);
            isOn = true;
        }
    }
}