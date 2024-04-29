using UnityEngine;

public class IdleState : MonoBehaviour, IEnemyState
{
    private GameObject campObject; // Referencia al objeto que representa el campamento
    private Transform player; // Referencia al jugador
    private EnemyStateMachine stateMachine;

    void Start()
    {
        // Obtener referencias necesarias
        campObject = GameObject.FindGameObjectWithTag("Camp");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine = GetComponent<EnemyStateMachine>();
    }

    public void EnterState()
    {
        // Lógica de entrada al estado Idle
        if (campObject != null)
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
        // Lógica de actualización del estado Idle
        // Aquí puedes agregar cualquier comportamiento adicional que el enemigo realice mientras está en estado Idle
    }

    public void ExitState()
    {
        // Lógica de salida del estado Idle
    }
}
