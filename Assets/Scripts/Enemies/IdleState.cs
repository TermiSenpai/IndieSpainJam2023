using UnityEngine;

public class IdleState : MonoBehaviour, IEnemyState
{
    private GameObject campfire; // Reference to the object representing the campfire
    private Transform player; // Reference to the player
    private EnemyStateMachine stateMachine;
    private FollowState followState;

    void Start()
    {
        // Get necessary references
        campfire = GameObject.FindGameObjectWithTag("Campfire");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine = GetComponent<EnemyStateMachine>();
        followState = GetComponent<FollowState>();
    }

    public void EnterState()
    {
        // If the campfire exists, change to Follow state
        if (campfire != null || player != null)
            stateMachine.ChangeState(followState);
    }

    public void UpdateState()
    {
    }

    public void ExitState() => enabled = false;
}
