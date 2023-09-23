using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private EnemyBrainController controller;
    private Animator stateMachine;
    private Rigidbody2D rb;

    

    [SerializeField] private EnemyStats stats;
    float distance;
    [SerializeField] private float stopDistance = 1.25f;

    private void Awake()
    {
        controller = GetComponent<EnemyBrainController>();
        stateMachine = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MoveToTarget();
        CheckStopDistance();
    }

    //private void OnEnable()
    //{
    //    controller.currentState = EnemyState.Follow;
    //    stateMachine.SetBool("isFollowing", true);
    //}

    //private void OnDisable()
    //{
    //    stateMachine.SetBool("isFollowing", false);
    //}

    private void MoveToTarget()
    {
        if (controller.currentTarget == null)
            return;

        if (!controller.canmove) return;

        // Calcula la dirección hacia el objetivo.
        Vector2 direccion = ((Vector2)controller.currentTarget.transform.position - (Vector2)transform.position).normalized;

        // Calcula la nueva posición deseada.
        Vector2 newPosition = (Vector2)transform.position + stats.moveSpeed * Time.deltaTime * direccion;

        // Mueve el Rigidbody2D hacia la nueva posición.
        rb.MovePosition(newPosition);

        float direccionX = direccion.x;
        float direccionY = direccion.y;

        // floats en el Animator
        stateMachine.SetFloat("Horizontal", direccionX);
        stateMachine.SetFloat("Vertical", direccionY);
    }

    protected virtual void CheckStopDistance()
    {
        if (controller.currentTarget != null)
            distance = Vector2.Distance(transform.position, controller.currentTarget.transform.position);


        // Comprobar si tenemos un objetivo y si estamos lo suficientemente lejos de él.
        if (distance > stopDistance)
            controller.StartFollow();

        else if (distance <= stopDistance)
        {
            controller.StopFollow();
            Emerge();
        }
    }

    public void Emerge()
    {
        stateMachine.SetTrigger("Emerge");
        controller.StopFollow();
        controller.canmove = false;
    }

}
