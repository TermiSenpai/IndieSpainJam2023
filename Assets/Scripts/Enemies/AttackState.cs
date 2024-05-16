using UnityEngine;

public class AttackState : MonoBehaviour, IEnemyState
{
    [SerializeField] EnemyStats stats;
    [SerializeField] Transform AttackPoint;
    [SerializeField] AudioClip attackSound;

    EnemyStateMachine stateMachine;
    EnemyAnimController anim;
    IdleState idle;

    private void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        anim = GetComponent<EnemyAnimController>();
        idle = GetComponent<IdleState>();
    }
    public void EnterState()
    {
        anim.Trigger("Emerge");
        stateMachine.currentEnemyState = EnemyState.Emerging;
    }

    public void ExitState()
    {
        //
    }

    public void UpdateState()
    {
        //throw new System.NotImplementedException();
    }

    public void Attack()
    {
        stateMachine.currentEnemyState = EnemyState.Attacking;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, stats.radiusRange, stats.damageableLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.gameObject.name);
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable?.TakeDamage(stats.damage);
        }
    }

    public void OnFinishAttack()
    {
        stateMachine.ChangeState(idle);
        enabled = false;
    }

    public void PlayEmergeSound()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, stats.radiusRange);
    }
}
