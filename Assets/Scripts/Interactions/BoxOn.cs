using UnityEngine;

public class BoxOn : MonoBehaviour, IInteractable
{
    public GameObject target;
    bool isOn = false;

    public void Interact()
    {
        if (isOn)
        {
            isOn = false;
            target.SetActive(false);
        }
        else
        {
            isOn = true;
            target.SetActive(true);
        }
    }
}
