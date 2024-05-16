using UnityEngine;

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}

public class EnemyStateMachine : MonoBehaviour
{
    public IEnemyState currentState;

    // Initialize the first state
    void Start() => InitializeState(GetComponent<IdleState>());

    // Actualizar el estado actual
    void Update() => currentState.UpdateState();

    public void ChangeState(IEnemyState newState)
    {
        // Exit the current state
        currentState.ExitState();

        // Change to the new state
        InitializeState(newState);
    }

    private void InitializeState(IEnemyState newState)
    {
        // Set the current state to the new state
        currentState = newState;
        // Call the EnterState method of the new state
        currentState.EnterState();
    }

}