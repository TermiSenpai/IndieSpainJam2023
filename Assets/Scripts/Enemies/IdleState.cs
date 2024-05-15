using UnityEngine;

public class IdleState : MonoBehaviour, IEnemyState
{
    private GameObject campfire; // Referencia al objeto que representa el campamento
    private Transform player; // Referencia al jugador
    private EnemyStateMachine stateMachine;
    private FollowState followState;

    void Start()
    {
        // Obtener referencias necesarias
        campfire = GameObject.FindGameObjectWithTag("Campfire");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        stateMachine = GetComponent<EnemyStateMachine>();
        followState = GetComponent<FollowState>();
    }

    public void EnterState()
    {
        // Lógica de entrada al estado Idle
        if (campfire != null || player != null)
        {
            // Si el campamento existe, cambiar al estado Follow
            stateMachine.ChangeState(followState);
        }
    }

    public void UpdateState()
    {
    }

    public void ExitState()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        EnterState();
    }
}
