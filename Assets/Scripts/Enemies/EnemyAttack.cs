using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    EnemyBrainController controller;
    [SerializeField] EnemyStats stats;

    protected float attackTimer;
    protected bool canAttack;



    private void Start()
    {
        attackTimer = stats.hitDelay;
        controller = GetComponent<EnemyBrainController>();
    }

    private void Update()
    {
        CheckAttackDelay();
    }

    public virtual void Attack()
    {
        if (!canAttack) return;

        // Lanzar un Raycast hacia la derecha desde la posición del objeto
        Vector2 raycastDirection = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, stats.attackLength, stats.damageableLayer);

        // Si el Raycast golpea algo
        if (hit.collider != null)
        {
            // Intenta obtener el componente IDamageable del objeto golpeado
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            // Si el objeto golpeado implementa la interfaz IDamageable

            // Llama al método TakeDamage() en el objeto
            damageable?.TakeDamage(stats.damage);
        }

        canAttack = false;
        controller.TryUpdateTarget();
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.right);
    }
}
