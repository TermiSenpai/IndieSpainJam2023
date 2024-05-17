using UnityEngine;

public class IdleState : MonoBehaviour, IEnemyState
{
    private GameObject campfire; // Reference to the object representing the campfire
    private Transform player; // Reference to the player
    private EnemyStateMachine stateMachine; // Reference to the enemy state machine
    private FollowState followState; // Reference to the follow state

    void Start()
    {
        // Get necessary references
        campfire = GameObject.FindGameObjectWithTag("Campfire"); // Find the campfire GameObject
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player GameObject
        stateMachine = GetComponent<EnemyStateMachine>(); // Get the EnemyStateMachine component
        followState = GetComponent<FollowState>(); // Get the FollowState component
    }

    public void EnterState()
    {
        stateMachine.currentEnemyState = EnemyState.Idle; // Set the current state to Idle
        // If there is a campfire or player nearby, switch to the follow state
        if (campfire != null || player != null)
            stateMachine.ChangeState(followState);
    }

    public void UpdateState()
    {
        // No update logic for the idle state
    }

    public void ExitState() => enabled = false; // Disable the component when exiting the state
}
