using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;
using Zenject;
using System;

public class CMChangeView : MonoBehaviour, IInitializable, IDisposable
{
    [SerializeField] private Vector3 right;
    [SerializeField] private Vector3 left;
    [SerializeField] private Transform target;
    [SerializeField] private float duration;
    [SerializeField] private bool isRight = true;

    [SerializeField] private InputActionProperty actionChange;

    private void Action_performedR(InputAction.CallbackContext obj)
    {
        isRight = !isRight;
        var tweeen = isRight switch
        {
            true => target.transform.DOLocalMove(right, duration),
            false => target.transform.DOLocalMove(left, duration)
        };
        tweeen.Play();
    }

    private void Action_performedL(InputAction.CallbackContext obj)
    {
        var tween = target.transform.DOLocalMove(left, duration);
        tween.Play();
    }

    public void Initialize()
    {
        actionChange.action.Enable();
        actionChange.action.performed += Action_performedR;
    }

    public void Dispose()
    {
        actionChange.action.Disable();
        actionChange.action.performed -= Action_performedR;
    }
}
