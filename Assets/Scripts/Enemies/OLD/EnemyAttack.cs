using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyBrainController controller;
    [SerializeField] EnemyStats stats;
    [SerializeField] Transform AttackPoint;
    [SerializeField] AudioClip attackSound;
    BoxCollider2D col;

    protected float attackTimer;
    protected bool canAttack;

    private void Awake()
    {
        controller = GetComponent<EnemyBrainController>();
        attackTimer = stats.hitDelay;
        col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        CheckAttackDelay();
    }

    public virtual void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, stats.radiusRange, stats.damageableLayer);

        // RaycastHit2D enemyHit = Physics2D.Raycast(AttackPoint.position, Vector2.right, stats.raidusRange, stats.enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {

            Debug.Log(enemy.gameObject.name);
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable?.TakeDamage(stats.damage);
        }


        OnFinishAttack();
    }

    protected virtual void CheckAttackDelay()
    {
        // Comprueba si el temporizador de ataque ha llegado a cero y si el enemigo puede atacar nuevamente.
        if (attackTimer <= 0 && !canAttack)
        {
            attackTimer = Mathf.Max(0, stats.hitDelay); // Evita valores negativos
            canAttack = true;
        }

        attackTimer -= Time.deltaTime;
    }

    public void OnFinishAttack()
    {
        controller.StopAttack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(AttackPoint.position, stats.radiusRange);
    }
}
