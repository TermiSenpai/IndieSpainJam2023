using System.Collections;
using UnityEngine;

public enum EnemyState
{
    Idle,
    Follow,
    Attack,
    Knockback
}

public class EnemyBrainController : MonoBehaviour
{
    [Header("References")]
    Animator stateMachine;
    public GameObject currentTarget;
    private GameObject player;
    private GameObject campfire;
    private GameObject turret;

    // States scripts
    private EnemyAttack attackState;
    private EnemyFollow followState;

    // tags
    const string campfireTag = "Campfire";
    const string playerTag = "Player";

    Vector2 campfirePos = Vector2.zero;
    Vector2 playerPos = Vector2.zero;
    Vector2 turretPos = Vector2.zero;

    public EnemyState currentState = EnemyState.Idle;

    [Header("Config")]
    [SerializeField] private float stopDistance = 1.25f;
    [SerializeField] bool prioriceCampfire = false;
    public float maxCampfireDistance;
    float distance;

    private void Awake()
    {
        stateMachine = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag);
        campfire = GameObject.FindGameObjectWithTag(campfireTag);

        attackState = GetComponent<EnemyAttack>();
        followState = GetComponent<EnemyFollow>();
    }
    private void Start()
    {
        TryUpdateTarget();
    }

    private void Update()
    {
        if (currentState == EnemyState.Idle && currentTarget != null)
            currentState = EnemyState.Follow;

        CheckStopDistance();


        switch (currentState)
        {
            case EnemyState.Idle:
                StopFollow();
                break;
            case EnemyState.Follow:
                TryUpdateTarget();
                LookTarget();
                break;
            case EnemyState.Knockback:
                StartCoroutine(OnKnockback());
                break;

        }

    }

    protected virtual void LookTarget()
    {
        if (currentTarget != null)
        {
            Vector3 direccion = currentTarget.transform.position - transform.position;
            float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));

            // Change blend tree anim
            stateMachine.SetFloat("Horizontal", direccion.x);
            stateMachine.SetFloat("Vertical", direccion.y);
        }
    }

    public virtual void TryUpdateTarget()
    {

        if (campfire != null)
            campfirePos = campfire.transform.position;

        if (player != null)
            playerPos = player.transform.position;

        if (turret != null)
            turretPos = turret.transform.position;

        // first target will be campfire
        if (currentTarget == null)
            currentTarget = campfire;
        
        float campfireDistance = Vector3.Distance(transform.position, campfirePos);
        float playerDistance = Vector3.Distance(transform.position, playerPos);
        float turretDistance = Vector3.Distance(transform.position, turretPos);

        // Define una distancia máxima para la prioridad del "Campfire"
        float maxCampfireDistance = 10f; 

        if ((campfire != null && campfire.activeInHierarchy && campfireDistance <= maxCampfireDistance) || prioriceCampfire)
        {
            // Campamento está presente y dentro de la distancia máxima
            currentTarget = campfire; // Campamento es el objetivo más cercano
        }
        else
        {
            float minDistance = Mathf.Min(playerDistance, turretDistance);

            if (player != null && player.activeInHierarchy && minDistance == playerDistance)
                currentTarget = player; // Jugador es el objetivo más cercano

            else if (turret != null && turret.activeInHierarchy)
                currentTarget = turret; // Torreta es el objetivo más cercano
            else StopFollow();
        }
    }

    protected virtual void CheckStopDistance()
    {
        if (currentTarget != null)
            distance = Vector2.Distance(transform.position, currentTarget.transform.position);
        else
        {
            TryUpdateTarget();
            return;
        }

        // Comprobar si tenemos un objetivo y si estamos lo suficientemente lejos de él.
        if (distance > stopDistance && currentState != EnemyState.Follow)
            StartFollow();
        else
        {
            attackState.enabled = true;
        }
    }

    protected void StopFollow()
    {
        followState.enabled = false;
    }

    protected void StartFollow()
    {
        followState.enabled = true;
    }

    public void SetTurret(GameObject newTurret)
    {
        turret = newTurret;
    }

    public GameObject GetTurret()
    {
        return turret;
    }

    private IEnumerator OnKnockback()
    {
        StopFollow();
        yield return new WaitForSeconds(1.5f);
        StartFollow();
    }

}
