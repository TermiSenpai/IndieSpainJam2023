using UnityEngine;

public class FollowState : MonoBehaviour, IEnemyState
{
    public Transform currentTarget; // Objetivo actual
    private EnemyStateMachine stateMachine;
    public float attackRange = 2f; // Rango de ataque
    [SerializeField] EnemyStats stats;
    EnemyAnimController anim;
    Rigidbody2D rb;

    void Start()
    {
        // Obtener una referencia al estado de la máquina
        stateMachine = GetComponent<EnemyStateMachine>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<EnemyAnimController>();
    }

    public void EnterState()
    {
        // Establecer el objetivo inicial (por ejemplo, el jugador)
        currentTarget = GetClosestTarget();
    }

    public void UpdateState()
    {

        MoveToTarget();
        // Lógica de actualización del estado Follow
        if (currentTarget == null || !currentTarget.GetComponent<IDamageable>().IsAlive())
        {
            // Si el objetivo actual no está vivo, cambiar al siguiente objetivo disponible
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
        enabled = false;
    }

    private Transform GetClosestTarget()
    {
        Transform closestTarget = null;
        float closestDistance = Mathf.Infinity;

        // Buscar el objeto con la etiqueta "Campfire"
        GameObject campfireObject = GameObject.FindGameObjectWithTag("Campfire");
        if (campfireObject != null && campfireObject.GetComponent<IDamageable>().IsAlive())
        {
            Transform campfire = campfireObject.transform;
            float distanceToCampfire = Vector3.Distance(transform.position, campfire.position);

            // Asignar el campfire como objetivo más cercano inicialmente
            closestTarget = campfire;
            closestDistance = distanceToCampfire;
        }

        // Buscar el objeto con la etiqueta "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null && playerObject.GetComponent<IDamageable>().IsAlive())
        {
            Transform player = playerObject.transform;
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Si la distancia al jugador es menor que la distancia al objetivo más cercano hasta ahora, actualizar el objetivo más cercano
            if (distanceToPlayer < closestDistance)
            {
                closestTarget = player;                
            }
        }

        return closestTarget;
    }

    private void MoveToTarget()
    {
        if (currentTarget == null)
            return;

        // Usa la función `fromToVector` para calcular la dirección hacia el objetivo.
        Vector2 direccion = FromToVector(transform.position.x, transform.position.y, currentTarget.position.x, currentTarget.position.y).normalized;

        // Calcula la nueva posición deseada.
        Vector2 newPosition = (Vector2)transform.position + stats.moveSpeed * Time.deltaTime * direccion;


        // Mueve el Rigidbody2D hacia la nueva posición.
        rb.MovePosition(newPosition);

        float direccionX = direccion.x;
        float direccionY = direccion.y;

        // floats en el Animator
        anim.SetFloat("Horizontal", direccionX);
        anim.SetFloat("Vertical", direccionY);
    }

    // La función `fromToVector` adaptada de Lua a C#
    private Vector2 FromToVector(float fromX, float fromY, float toX, float toY)
    {
        return new Vector2(toX - fromX, toY - fromY).normalized;
    }
}
