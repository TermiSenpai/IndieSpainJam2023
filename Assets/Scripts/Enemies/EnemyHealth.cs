using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    [SerializeField] EnemyStats stats;
    private float currentHealth;
    private Rigidbody2D rb;
    [SerializeField] private float knockbackForce;
    private EnemyBrainController controller;

    private void Start()
    {
        controller = GetComponent<EnemyBrainController>();
        currentHealth = stats.MaxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // TODO
    // Cancelar anim actual, knocback y fijar como objetivo al causante del daño

    public void TakeDamage(float damage, Vector2 knockbackDirection)
    {
        currentHealth -= damage;

        // Aplica el knockback solo si el objeto tiene un Rigidbody2D.
        if (rb != null)
        {
            // Aplica la fuerza de knockback al Rigidbody2D.
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            controller.currentState = EnemyState.Knockback;
        }

        CheckHealth();
    }

    public void TakeDamage(float damage)
    {

    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
