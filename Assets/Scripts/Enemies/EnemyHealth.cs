using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{

    [Header("References")]
    [SerializeField] EnemyStats stats;
    [SerializeField] AudioClip[] hurtClips;
    [SerializeField] AudioClip deathClip;
    private AudioSource source;
    private SpriteRenderer render;
    private Animator anim;

    [Header("Timers")]
    [SerializeField] private float redTime;
    [SerializeField] private float invincibleTime;

    // Variables
    private float currentHealth;
    private bool canBeDamaged = true;
    private bool isDead = false;

    private void Start()
    {
        anim = GetComponent<Animator>();    
        render = GetComponent<SpriteRenderer>();
        source = GetComponent<AudioSource>();
        ResetHealth();
    }

    // TODO
    // Cancelar anim actual, knocback y fijar como objetivo al causante del daño

    public void TakeDamage(float damage)
    {
        if (!canBeDamaged) return;

        PlayHurtSound();
        currentHealth -= damage;
        CheckHealth();

        if (isDead) return;

        StartCoroutine(ChangeColor());
        StartCoroutine(InvincibleMode());

    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            canBeDamaged = false;
            anim.SetTrigger("Death");
            source.Stop();
            source.PlayOneShot(deathClip);
            Invoke(nameof(DisableEnemy), 4f);
        }
    }

    public void DisableEnemy()
    {
        EnemyObjectPool.Instance.ReturnEnemyToPool(gameObject);
    }

    private AudioClip TakeRandomStepSound()
    {
        return hurtClips[Random.Range(0, hurtClips.Length)];
    }
    public void PlayHurtSound()
    {
        source.Stop();
        source.PlayOneShot(TakeRandomStepSound());
    }
    private IEnumerator ChangeColor()
    {
        Color damagedColor = Color.red;
        render.color = damagedColor;
        yield return new WaitForSeconds(redTime);
        render.color = Color.white;
    }
    private IEnumerator InvincibleMode()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(invincibleTime);
        canBeDamaged = true;
    }
    private void ResetHealth()
    {
        currentHealth = stats.MaxHealth;
        canBeDamaged = true;
        isDead = false;
    }

    private void OnEnable()
    {
        ResetHealth();
    }
}
