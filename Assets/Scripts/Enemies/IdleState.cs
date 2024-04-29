using UnityEngine;

public class IdleState : MonoBehaviour, IEnemyState
{
    private GameObject campfire; // Referencia al objeto que representa el campamento
    private Transform player; // Referencia al jugador
    private EnemyStateMachine stateMachine;

    void Start()
    {
        // Obtener referencias necesarias
        campfire = GameObject.FindGameObjectWithTag("Campfire");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    public void EnterState()
    {
        // L�gica de entrada al estado Idle
        if (campfire != null)
        {
            // Si el campamento existe, cambiar al estado Follow
            stateMachine.ChangeState(GetComponent<FollowState>());
        }
        else
        {
            // Si el campamento no existe, priorizar al jugador como objetivo
            stateMachine.ChangeState(GetComponent<ChaseState>());
        }
    }

    public void UpdateState()
    {
        // L�gica de actualizaci�n del estado Idle
        // Aqu� puedes agregar cualquier comportamiento adicional que el enemigo realice mientras est� en estado Idle
    }

    public void ExitState()
    {
        // L�gica de salida del estado Idle
    }
}
