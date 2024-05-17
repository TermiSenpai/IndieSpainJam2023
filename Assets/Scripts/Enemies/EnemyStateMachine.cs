using UnityEngine;

public enum EnemyState
{
    Idle,
    Chasing,
    Emerging,
    Attacking
}

public class EnemyStateMachine : MonoBehaviour
{
    public IEnemyState currentMachineState;
    public EnemyState currentEnemyState;
    public IdleState idle;

    // Initialize the first state
    void Start() => idle = GetComponent<IdleState>();

    // Actualizar el estado actual
    void Update() => currentMachineState.UpdateState();

    public void ChangeState(IEnemyState newState)
    {
        // Exit the current state
        currentMachineState.ExitState();

        // Change to the new state
        InitializeState(newState);
    }

    private void InitializeState(IEnemyState newState)
    {
        // Set the current state to the new state
        currentMachineState = newState;
        // Call the EnterState method of the new state
        currentMachineState.EnterState();
    }

    private void OnEnable()
    {
        InitializeState(idle);
    }

}