using UnityEngine;

public class FollowState : MonoBehaviour, IEnemyState
{
    private Transform currentTarget; // Objetivo actual
    private EnemyStateMachine stateMachine;
    public float attackRange = 2f; // Rango de ataque
    [SerializeField] EnemyStats stats;
    EnemyAnimController anim;
    Rigidbody2D rb;

    void Start()
    {
        // Obtener una referencia al estado de la m�quina
        stateMachine = GetComponent<EnemyStateMachine>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<EnemyAnimController>();
    }

    public void EnterState()
    {
        Debug.Log("Follow state on");
        // L�gica de entrada al estado Follow
        // Establecer el objetivo inicial (por ejemplo, el jugador)
        currentTarget = GetClosestTarget();
    }

    public void UpdateState()
    {

        MoveToTarget();
        // L�gica de actualizaci�n del estado Follow
        if (currentTarget == null || !currentTarget.GetComponent<IDamageable>().IsAlive())
        {
            // Si el objetivo actual no est� vivo, cambiar al siguiente objetivo disponible
            currentTarget = GetClosestTarget();
        }

        // Si estamos lo suficientemente cerca del objetivo, cambiar al estado de ataque
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.position) <= attackRange)
        {
            stateMachine.ChangeState(GetComponent<AttackState>());
        }
    }

    public void ExitState()
    {
        Debug.Log("Follow state off");
        // L�gica de salida del estado Follow
    }

    private Transform GetClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        // Buscar el objeto con la etiqueta "Campfire"
        GameObject campfireObject = GameObject.FindGameObjectWithTag("Campfire");

        // Verificar si el campfire existe y est� vivo
        if (campfireObject != null)
        {
            Transform campfire = campfireObject.transform;
            float distanceToCampfire = Vector3.Distance(transform.position, campfire.position);

            // Si el campfire est� vivo y es el m�s cercano hasta ahora, asignarlo como el objetivo m�s cercano
            if (distanceToCampfire < closestDistance && campfire.GetComponent<IDamageable>().IsAlive())
            {
                closestTarget = campfire;
                closestDistance = distanceToCampfire;
            }
        }

        // Buscar al jugador solo si no se encontr� un campamento v�lido
        if (closestTarget == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            // Verificar si el jugador existe y est� vivo
            if (playerObject != null)
            {
                Transform player = playerObject.transform;
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);

                // Si el jugador est� vivo y es el m�s cercano hasta ahora, asignarlo como el objetivo m�s cercano
                if (distanceToPlayer < closestDistance && player.GetComponent<IDamageable>().IsAlive())
                {
                    closestTarget = player;
                }
            }
        }

        return closestTarget;
    }
    private void MoveToTarget()
    {
        if (currentTarget == null)
            return;

        // Calcula la direcci�n hacia el objetivo.
        Vector2 direccion = ((Vector2)currentTarget.transform.position - (Vector2)transform.position).normalized;

        // Calcula la nueva posici�n deseada.
        Vector2 newPosition = (Vector2)transform.position + stats.moveSpeed * Time.deltaTime * direccion;

        // Mueve el Rigidbody2D hacia la nueva posici�n.
        rb.MovePosition(newPosition);

        float direccionX = direccion.x;
        float direccionY = direccion.y;

        // floats en el Animator
        anim.SetFloat("Horizontal", direccionX);
        anim.SetFloat("Vertical", direccionY);
    }
}
