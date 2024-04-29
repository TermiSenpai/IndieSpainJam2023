using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    // References
    [SerializeField] PlayerStats stats;
    private SpriteRenderer render;
    private Animator anim;
    private AudioSource source;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip[] clips;

    // properties
    private float currentHealth;
    private bool canBeDamaged = true;
    private bool playerLose = false;
    [SerializeField] private float redTime = 2f;
    [SerializeField] private float invincibleTime = 2f;
    // Events
    public delegate void PlayerHealthDelegate();
    public delegate void PlayerHitDelegate(float hp);
    public static PlayerHealthDelegate PlayerDeathRelease;
    public static PlayerHitDelegate PlayerTakeDamageRelease;
    public static PlayerHitDelegate PlayerHealRelease;

    private void Start()
    {
        render = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        currentHealth = stats.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Stop if player is in invencible mode
        if (!canBeDamaged) return;

        currentHealth -= damage;
        PlayerTakeDamageRelease?.Invoke(currentHealth);
        CheckCurrentHealth();
        if (currentHealth > 0)
        {

            PlayHitSound();
        }

        // stop if player die
        if (playerLose) return;

        StartCoroutine(ChangeColor());
        StartCoroutine(InvincibleMode());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && stats.currentFood > 0)
        {
            currentHealth += 10;
            stats.currentFood -= 1;
            PlayerTakeDamageRelease?.Invoke(currentHealth);
        }
    }

    private AudioClip TakeRandomHitSound()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlayHitSound()
    {
        source.PlayOneShot(TakeRandomHitSound());
    }

    private void CheckCurrentHealth()
    {
        if (currentHealth <= 0)
        {
            PlayerDeathRelease?.Invoke();
            anim.SetTrigger("Death");
            source.PlayOneShot(deathSound);
            //gameObject.SetActive(false);
            playerLose = true;
            canBeDamaged = false;
        }
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

    public bool IsAlive()
    {
        return !playerLose;
    }
}
