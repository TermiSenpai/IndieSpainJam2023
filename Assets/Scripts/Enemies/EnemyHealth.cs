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
        currentHealth = stats.MaxHealth;
    }

    // TODO
    // Cancelar anim actual, knocback y fijar como objetivo al causante del da�o

    public void TakeDamage(float damage)
    {
        if (!canBeDamaged) return;

        PlayHurtSound();
        currentHealth -= damage;
        CheckHealth();

        if (isDead) return;

        StartCoroutine(ChangeColor());
        StartCoroutine(invincibleMode());

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
        }
    }

    public void DisableEnemy()
    {
        gameObject.SetActive(false);
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
    private IEnumerator invincibleMode()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(invincibleTime);
        canBeDamaged = true;
    }
}
