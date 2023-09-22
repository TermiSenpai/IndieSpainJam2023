using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStats stats;

    private float currentHealth;

    private void Start()
    {
        currentHealth = stats.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        CheckCurrentHealth();
    }

    private void CheckCurrentHealth()
    {
        if(currentHealth <= 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
