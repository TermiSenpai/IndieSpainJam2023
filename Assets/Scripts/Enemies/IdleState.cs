using UnityEngine;

public class IdleState : MonoBehaviour, IEnemyState
{
    private GameObject campfire; // Reference to the object representing the campfire
    private Transform player; // Reference to the player
    [SerializeField] private EnemyStateMachine stateMachine; // Reference to the enemy state machine
    private FollowState followState; // Reference to the follow state

    private void Awake()
    {
        // Get necessary references
        followState = GetComponent<FollowState>(); // Get the FollowState component        
        campfire = GameObject.FindGameObjectWithTag("Campfire"); // Find the campfire GameObject
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player GameObject
    }

    public void EnterState()
    {
        stateMachine.currentEnemyState = EnemyState.Idle; // Set the current state to Idle
        CheckTargets();
    }

    public void UpdateState()
    {
        CheckTargets();
    }

    public void ExitState() => enabled = false; // Disable the component when exiting the state

    private void CheckTargets()
    {
        // If there is a campfire or player nearby, switch to the follow state
        if (campfire != null || player != null)
            stateMachine.ChangeState(followState);
    }
   
}
