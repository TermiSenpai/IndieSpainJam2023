using UnityEngine;

public class AttackState : MonoBehaviour, IEnemyState
{
    [SerializeField] EnemyStats stats; // Reference to enemy stats
    [SerializeField] Transform AttackPoint; // Reference to the attack point
    [SerializeField] AudioClip attackSound; // Sound clip for the attack

    EnemyStateMachine stateMachine; // Reference to the enemy state machine
    EnemyAnimController anim; // Reference to the enemy animator
    IdleState idle; // Reference to the idle state
    EnemySoundManager soundManager; // Reference to the enemy sound manager

    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>(); // Get the EnemyStateMachine component
        anim = GetComponent<EnemyAnimController>(); // Get the EnemyAnimController component
        idle = GetComponent<IdleState>(); // Get the IdleState component
        soundManager = GetComponent<EnemySoundManager>(); // Get the EnemySoundManager component
    }
    public void EnterState()
    {
        anim.Trigger("Emerge"); // Trigger the "Emerge" animation
        stateMachine.currentEnemyState = EnemyState.Emerging; // Set the current state to Emerging
    }

    public void ExitState()
    {
        //
    }

    public void UpdateState()
    {
        //throw new System.NotImplementedException();
    }

    // Method to execute the attack
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, stats.radiusRange, stats.damageableLayer); // Get all colliders within the attack range

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log(enemy.gameObject.name);
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            damageable?.TakeDamage(stats.damage); // Damage the enemy if it implements the IDamageable interface
        }
    }

    // Method called when the attack animation finishes
    public void OnFinishAttack()
    {
        stateMachine.ChangeState(idle); // Switch to the idle state
        enabled = false;
    }

    // Method to play the emerge sound
    public void PlayEmergeSound()
    {
        soundManager.PlayOneTime(stats.emergeClip); // Play the emerge sound
    }

    // Visualize the attack range in the scene view
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, stats.radiusRange);
    }
}
