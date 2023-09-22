using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStats stats;
    private SpriteRenderer render;

    private float currentHealth;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        currentHealth = stats.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(ChangeColor());
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

    private IEnumerator ChangeColor()
    {
        Color damagedColor = Color.red;
        render.color = damagedColor;
        yield return new WaitForSeconds(0.1f);
        render.color = Color.white;
    }

     
}
