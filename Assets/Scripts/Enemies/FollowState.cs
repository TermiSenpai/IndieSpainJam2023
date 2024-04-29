using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FollowState : MonoBehaviour, IEnemyState
{
    private Transform currentTarget; // Objetivo actual
    private NavMeshAgent agent;
    private EnemyStateMachine stateMachine;
    public float attackRange = 2f; // Rango de ataque

    void Start()
    {
        // Obtener una referencia al estado de la máquina
        stateMachine = GetComponent<EnemyStateMachine>();
        // Obtener una referencia al componente NavMeshAgent
        agent = GetComponent<NavMeshAgent>();
    }

    public void EnterState()
    {
        // Lógica de entrada al estado Follow
        // Establecer el objetivo inicial (por ejemplo, el jugador)
        currentTarget = GetClosestNonEnemyTarget();
        // Iniciar el seguimiento del objetivo
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);
        }
    }

    public void UpdateState()
    {
        // Lógica de actualización del estado Follow
        if (currentTarget == null || !currentTarget.GetComponent<IDamageable>().IsAlive())
        {
            // Si el objetivo actual no está vivo, cambiar al siguiente objetivo disponible
            currentTarget = GetClosestNonEnemyTarget();
            // Reiniciar el seguimiento del nuevo objetivo
            if (currentTarget != null)
            {
                agent.SetDestination(currentTarget.position);
            }
        }

        // Si estamos lo suficientemente cerca del objetivo, cambiar al estado de ataque
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
        {
            stateMachine.ChangeState(GetComponent<AttackState>());
        }
    }

    public void ExitState()
    {
        // Detener el movimiento cuando salimos del estado de seguimiento
        agent.ResetPath();
    }

    private Transform GetClosestNonEnemyTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        // Buscar el objeto con la etiqueta "Campfire"
        GameObject campfireObject = GameObject.FindGameObjectWithTag("Campfire");

        // Verificar si el campfire existe y está vivo
        if (campfireObject != null)
        {
            Transform campfire = campfireObject.transform;
            float distanceToCampfire = Vector3.Distance(transform.position, campfire.position);

            // Si el campfire está vivo y es el más cercano hasta ahora, asignarlo como el objetivo más cercano
            if (distanceToCampfire < closestDistance && campfire.GetComponent<IDamageable>().IsAlive())
            {
                closestTarget = campfire;
                closestDistance = distanceToCampfire;
            }
        }

        // Buscar al jugador
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        // Verificar si el jugador existe y está vivo
        if (playerObject != null)
        {
            Transform player = playerObject.transform;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Si el jugador está vivo y es el más cercano hasta ahora, asignarlo como el objetivo más cercano
            if (distanceToPlayer < closestDistance && player.GetComponent<IDamageable>().IsAlive())
            {
                closestTarget = player;
                closestDistance = distanceToPlayer;
            }
        }

        return closestTarget;
    }


}
