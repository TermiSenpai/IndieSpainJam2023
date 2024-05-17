using System.Collections.Generic;
using UnityEngine;

public class FollowState : MonoBehaviour, IEnemyState
{
    public Transform currentTarget; // Current target
    private EnemyStateMachine stateMachine;
    public float attackRange = 2f; // Attack range
    [SerializeField] EnemyStats stats; // Reference to enemy stats
    EnemyAnimController anim; // Reference to the enemy animator
    Rigidbody2D rb; // Reference to the enemy Rigidbody2D
    // List of potential targets
    private List<Transform> targets;

    private ObstacleAvoidance obstacleAvoidance; // Reference to the ObstacleAvoidance script

    void Start()
    {
        // Get a reference to the state machine
        stateMachine = GetComponent<EnemyStateMachine>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<EnemyAnimController>();
        obstacleAvoidance = GetComponent<ObstacleAvoidance>();
        // Initialize the list of targets
        targets = new List<Transform>();
        FindTargets();
    }

    // Method to find potential targets for the enemy
    void FindTargets()
    {
        targets.Clear();

        // Add campfires to the targets list
        GameObject campfireObject = GameObject.FindGameObjectWithTag("Campfire");
        if (campfireObject != null && campfireObject.GetComponent<IDamageable>().IsAlive())
        {
            targets.Add(campfireObject.transform);
        }

        // Add players to the targets list
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null && playerObject.GetComponent<IDamageable>().IsAlive())
        {
            targets.Add(playerObject.transform);
        }
    }

    public void EnterState()
    {
        // Set initial target
        currentTarget = GetClosestTarget();
        stateMachine.currentEnemyState = EnemyState.Chasing;
    }

    public void UpdateState()
    {
        MoveToTarget();
        // Follow state update logic
        if (currentTarget == null || !currentTarget.GetComponent<IDamageable>().IsAlive())
        {
            // If the current target is not alive, switch to the next available target
            currentTarget = GetClosestTarget();
        }

        // If we are close enough to the target, switch to attack state
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
        {
            stateMachine.ChangeState(GetComponent<AttackState>());
        }
    }

    public void ExitState()
    {
        enabled = false;
    }

    // Method to get the closest target from the list of potential targets
    private Transform GetClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.position);
            if (distance < closestDistance)
            {
                closestTarget = target;
                closestDistance = distance;
            }
        }

        return closestTarget;
    }

    // Method to move the enemy towards the current target
    private void MoveToTarget()
    {
        if (currentTarget == null)
            return;

        // Calculate the direction towards the target
        Vector2 directionToTarget = ((Vector2)currentTarget.position - (Vector2)transform.position).normalized;

        // Get the best direction from the ObstacleAvoidance script
        Vector2 bestDirection = obstacleAvoidance.GetBestDirection(directionToTarget);

        // Move the Rigidbody2D to the new position
        Vector2 newPosition = (Vector2)transform.position + bestDirection * stats.moveSpeed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // Set floats in the Animator
        anim.SetFloat("Horizontal", bestDirection.x);
        anim.SetFloat("Vertical", bestDirection.y);
    }
}
