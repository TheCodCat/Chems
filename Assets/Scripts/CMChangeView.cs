using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class CMChangeView : MonoBehaviour
{
    [SerializeField] private Vector3 right;
    [SerializeField] private Vector3 left;
    [SerializeField] private Transform target;
    [SerializeField] private float duration;

    [SerializeField] private InputActionProperty actionRight;
    [SerializeField] private InputActionProperty actionLeft;

    private void Start()
    {
        actionRight.action.Enable();
        actionLeft.action.Enable();

        actionRight.action.performed += Action_performedR;
        actionLeft.action.performed += Action_performedL;
    }

    private void Action_performedR(InputAction.CallbackContext obj)
    {
        var tween = target.transform.DOLocalMove(right, duration);
        tween.Play();
    }

    private void Action_performedL(InputAction.CallbackContext obj)
    {
        var tween = target.transform.DOLocalMove(left, duration);
        tween.Play();
    }
}
