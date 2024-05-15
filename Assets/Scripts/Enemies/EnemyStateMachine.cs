using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public IEnemyState currentState;

    void Start()
    {
        // Inicializar el primer estado
        currentState = GetComponent<IdleState>();
        currentState.EnterState();
    }

    void Update()
    {
        // Actualizar el estado actual
        currentState.UpdateState();
    }

    public void ChangeState(IEnemyState newState)
    {
        // Salir del estado actual
        currentState.ExitState();


        // Cambiar al nuevo estado
        currentState = newState;
        currentState.EnterState();
    }
}