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
    private Vector2 mousePos;
    private float timerAttack;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        timerAttack = 0;
        stats = PlayerStats.Instance; // Setting reference to the PlayerStats Scriptable Object
    }

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SwordAttack();
        }

        CheckAttackDelay();
    }

    private void CheckAttackDelay()
    {
        // Limiting timer to zero
        timerAttack = Mathf.Max(0, timerAttack - Time.deltaTime);
    }

    private bool CanAttack()
    {
        // Checking if the player can attack
        return timerAttack <= 0;
    }

    private void SwordAttack()
    {
        if (!CanAttack()) return;

        SetAttackAnimationDirection();
        StartCoroutine(VisibleAttack());
        DealDamageToEnemies();

        timerAttack = stats.baseAttackDelay; // Resetting attack timer
    }

    private void SetAttackAnimationDirection()
    {
        // Setting attack animation direction based on mouse position
        anim.SetFloat("Horizontal", mousePos.x);
        anim.SetFloat("Vertical", mousePos.y);
    }

    private IEnumerator VisibleAttack()
    {
        // Showing weapon attack animation and playing swing sound
        AttackPoint.gameObject.SetActive(true);
        PlaySwingSound();
        yield return new WaitForSeconds(visibleWeaponTime);
        AttackPoint.gameObject.SetActive(false);
    }

    private void DealDamageToEnemies()
    {
        // Dealing damage to enemies within attack range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, stats.raidusRange, stats.enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.gameObject.name);
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable?.TakeDamage(stats.damage);
        }
    }

    private void PlaySwingSound()
    {
        // Playing a random swing sound
        if (clips != null && clips.Length > 0)
        {
            source.PlayOneShot(TakeRandomSwingSound());
        }
    }

    private AudioClip TakeRandomSwingSound()
    {
        // Returning a random swing sound from the array
        return clips[Random.Range(0, clips.Length)];
    }

    private void OnDrawGizmos()
    {
        // Drawing attack range gizmos
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(AttackPoint.position, stats.raidusRange);
    }
}
