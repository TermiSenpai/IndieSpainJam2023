using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] Transform AttackPoint;
    [SerializeField] float visibleWeaponTime;

    private AudioSource source;
    [SerializeField] AudioClip[] clips;
    private Animator anim;
    Vector2 mousePos;
    private float timerAttack;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        timerAttack = 0;
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
        timerAttack -= Time.deltaTime;

        if (timerAttack <= 0)
        {
            timerAttack = 0;
        }
    }

    private bool CanAttack()
    {
        // Comprueba si el temporizador de ataque ha llegado a cero y si el enemigo puede atacar nuevamente.
        if (timerAttack <= 0)
            return true;

        return false;
    }

    private void SwordAttack()
    {
        if (!CanAttack()) return;

        anim.SetFloat("Horizontal", mousePos.x);
        anim.SetFloat("Vertical", mousePos.y);

        StartCoroutine(VisibleAttack());

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, stats.raidusRange, stats.enemyLayer);

        // RaycastHit2D enemyHit = Physics2D.Raycast(AttackPoint.position, Vector2.right, stats.raidusRange, stats.enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {

            Debug.Log(enemy.gameObject.name);
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable?.TakeDamage(stats.damage);
        }

        timerAttack = stats.baseAttackDelay;

    }

    // TODO
    // Temporal, waiting to anim
    IEnumerator VisibleAttack()
    {
        AttackPoint.gameObject.SetActive(true);
        PlaySwingSound();
        yield return new WaitForSeconds(visibleWeaponTime);
        AttackPoint.gameObject.SetActive(false);
    }


    private AudioClip TakeRandomSwingSound()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlaySwingSound()
    {
        source.PlayOneShot(TakeRandomSwingSound());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;

        //Gizmos.DrawRay(AttackPoint.position, Vector2.right);
        Gizmos.DrawWireSphere(AttackPoint.position, stats.raidusRange);
    }

}
