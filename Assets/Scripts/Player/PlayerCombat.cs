using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private PlayerStats stats; // Reference to the PlayerStats Scriptable Object

    [SerializeField] Transform AttackPoint;
    [SerializeField] float visibleWeaponTime;

    private AudioSource source;
    [SerializeField] AudioClip[] clips;
    private Animator anim;
    Vector2 mousePos;
    private float timerAttack;

    private void Start()
    {
        // Getting necessary components and initializing variables
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        timerAttack = 0;

        // Setting reference to the PlayerStats Scriptable Object
        stats = PlayerStats.Instance;
    }

    private void Update()
    {
        // Getting mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Checking for attack input
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SwordAttack();
        }

        // Checking attack delay
        CheckAttackDelay();
    }

    private void CheckAttackDelay()
    {
        // Decreasing attack timer
        timerAttack -= Time.deltaTime;

        // Ensuring timer doesn't go below zero
        if (timerAttack <= 0)
        {
            timerAttack = 0;
        }
    }

    private bool CanAttack()
    {
        // Checking if the attack timer has reached zero and if the player can attack again
        return timerAttack <= 0;
    }

    private void SwordAttack()
    {
        // Checking if player can attack
        if (!CanAttack()) return;

        // Setting attack animation direction
        anim.SetFloat("Horizontal", mousePos.x);
        anim.SetFloat("Vertical", mousePos.y);

        // Showing attack animation and dealing damage to enemies within attack range
        StartCoroutine(VisibleAttack());

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, stats.raidusRange, stats.enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.gameObject.name);
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable?.TakeDamage(stats.damage);
        }

        // Resetting attack timer
        timerAttack = stats.baseAttackDelay;
    }

    IEnumerator VisibleAttack()
    {
        // Showing weapon attack animation and playing swing sound
        AttackPoint.gameObject.SetActive(true);
        PlaySwingSound();
        yield return new WaitForSeconds(visibleWeaponTime);
        AttackPoint.gameObject.SetActive(false);
    }

    private AudioClip TakeRandomSwingSound()
    {
        // Returning a random swing sound from the array
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlaySwingSound()
    {
        // Playing a random swing sound
        source.PlayOneShot(TakeRandomSwingSound());
    }

    private void OnDrawGizmos()
    {
        // Drawing attack range gizmos
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(AttackPoint.position, stats.raidusRange);
    }
}
