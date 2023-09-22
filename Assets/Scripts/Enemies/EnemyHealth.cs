using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    [SerializeField] EnemyStats stats;
    private float currentHealth;

    private void Start()
    {
        currentHealth = stats.MaxHealth;
    }

    // TODO
    // Cancelar anim actual, knocback y fijar como objetivo al causante del daño

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckHealth();
    }

    private void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
