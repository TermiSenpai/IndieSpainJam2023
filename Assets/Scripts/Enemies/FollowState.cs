using System.Collections.Generic;
using UnityEngine;

public class FollowState : MonoBehaviour, IEnemyState
{
    public Transform currentTarget; // Current target
    private EnemyStateMachine stateMachine;
    public float attackRange = 2f; // Attack range
    [SerializeField] EnemyStats stats;
    EnemyAnimController anim;
    Rigidbody2D rb;
    // List of potential targets
    private List<Transform> targets;
    void Start()
    {
        // Get a reference to the state machine
        stateMachine = GetComponent<EnemyStateMachine>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<EnemyAnimController>();
        // Initialize the list of targets
        targets = new List<Transform>();
        FindTargets();
    }

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

    private void MoveToTarget()
    {
        if (currentTarget == null)
            return;

        // Use the `fromToVector` function to calculate the direction towards the target.
        Vector2 direction = FromToVector(transform.position.x, transform.position.y, currentTarget.position.x, currentTarget.position.y).normalized;

        // Calculate the new desired position.
        Vector2 newPosition = (Vector2)transform.position + stats.moveSpeed * Time.deltaTime * direction;

        // Move the Rigidbody2D to the new position.
        rb.MovePosition(newPosition);

        float directionX = direction.x;
        float directionY = direction.y;

        // Set floats in the Animator
        anim.SetFloat("Horizontal", directionX);
        anim.SetFloat("Vertical", directionY);
    }

    // Calculates the directional vector from one point to another and normalizes it.
    private Vector2 FromToVector(float fromX, float fromY, float toX, float toY) => new Vector2(toX - fromX, toY - fromY).normalized;
}
