using UnityEngine;

public class RagdollController : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    Collider[] colliders;
    Animator animator;

    void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        animator = GetComponent<Animator>();

        SetRagdoll(false);
    }

    public void SetRagdoll(bool enabled)
    {
        foreach (var rb in rigidbodies)
            rb.isKinematic = !enabled;

        foreach (var col in colliders)
            col.enabled = enabled;

        if (animator)
            animator.enabled = !enabled;
    }
}