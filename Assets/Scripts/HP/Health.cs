using UnityEngine;
using Zenject.SpaceFighter;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] bool isPlayer;

    public float CurrentHealth { get; private set; }

    void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (CurrentHealth <= 0) return;

        CurrentHealth -= amount;

        Debug.Log($"{name} took {amount} damage. HP: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{name} died");

        if (isPlayer)
        {
            // Disable logic on root
            var move = GetComponent<ThirdPersonMovement>();
            if (move) move.enabled = false;

            var shooter = GetComponent<Shooter>();
            if (shooter) shooter.enabled = false;

            var controller = GetComponent<CharacterController>();
            if (controller) controller.enabled = false;

            // Enable ragdoll on model
            RagdollController ragdoll = GetComponentInChildren<RagdollController>();
            if (ragdoll)
                ragdoll.SetRagdoll(true);

            return;
        }

        Destroy(gameObject);
    }
}