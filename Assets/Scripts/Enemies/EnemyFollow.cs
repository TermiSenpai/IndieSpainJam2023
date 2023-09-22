using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private EnemyBrainController controller;
    private Animator stateMachine;

    [SerializeField] private float speed;

    private void Start()
    {
        controller = GetComponent<EnemyBrainController>();
        stateMachine = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void OnEnable()
    {
        controller.currentState = EnemyState.Follow;
        stateMachine.SetBool("isFollowing", true);
    }

    private void OnDisable()
    {
        stateMachine.SetBool("isFollowing", false);
    }

    private void MoveToTarget()
    {
        if (controller.currentTarget == null)
            return;

        stateMachine.transform.position = Vector2.MoveTowards(transform.position,
                                      controller.currentTarget.transform.position,
                                      speed * Time.deltaTime);
        Vector2 direccion = ((Vector2)controller.currentTarget.transform.position - (Vector2)transform.position).normalized;

        float direccionX = direccion.x;
        float direccionY = direccion.y;

        // floats en el Animator
        stateMachine.SetFloat("Horizontal", direccionX);
        stateMachine.SetFloat("Vertical", direccionY);
    }

}
