using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] PlayerStats stats;
    private SpriteRenderer render;

    private float currentHealth;
    private bool canBeDamaged = true;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        currentHealth = stats.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!canBeDamaged) return;
        StartCoroutine(ChangeColor());
        currentHealth -= damage;
        StartCoroutine(InvencibleMode());
        CheckCurrentHealth();
    }

    public void TakeDamage(float damage, Vector2 knockbackDirection)
    {

    }

    private void CheckCurrentHealth()
    {
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private IEnumerator ChangeColor()
    {
        Color damagedColor = Color.red;
        render.color = damagedColor;
        yield return new WaitForSeconds(2f);
        render.color = Color.white;
    }

    private IEnumerator InvencibleMode()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(2f);
        canBeDamaged = true;
    }


}
