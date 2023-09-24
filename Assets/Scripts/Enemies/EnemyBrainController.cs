using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

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
    [HideInInspector]
    public AudioSource source;    
    // States scripts
    [Header("States references")]
    [SerializeField] private EnemyAttack attackState;
    [SerializeField] private EnemyFollow followState;

    // tags
    const string campfireTag = "Campfire";
    const string playerTag = "Player";

    Vector2 campfirePos = Vector2.zero;
    Vector2 playerPos = Vector2.zero;
    Vector2 turretPos = Vector2.zero;

    public EnemyState currentState = EnemyState.Idle;

    [Header("Config")]

    [SerializeField] bool prioritizeCampfire = false;
    public float maxCampfireDistance;
    public bool canmove = true;


    private void Awake()
    {
        stateMachine = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag);
        campfire = GameObject.FindGameObjectWithTag(campfireTag);
        source = GetComponent<AudioSource>();
    }
    private void Start()
    {
        TryUpdateTarget();
    }

    private void Update()
    {
        if (currentState == EnemyState.Idle && currentTarget != null)
        {
            currentState = EnemyState.Follow;
            StartFollow();
        }

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

    private void OnEnable()
    {
        PlayerHealth.PlayerDeathRelease += OnPlayerDeath;
        currentState = EnemyState.Follow;
        canmove = true;
    }

    private void OnDisable()
    {
        PlayerHealth.PlayerDeathRelease -= OnPlayerDeath;
    }

    protected void OnPlayerDeath()
    {
        prioritizeCampfire = true;
    }

    protected virtual void LookTarget()
    {
        if (currentTarget != null)
        {
            Vector3 direccion = currentTarget.transform.position - transform.position;
            //float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(new Vector3(0, 0, angulo));

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

        if ((campfire != null && campfire.activeInHierarchy && campfireDistance <= maxCampfireDistance) || prioritizeCampfire)
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

    public void StopFollow()
    {
        stateMachine.SetBool("isFollowing", false);
    }

    public void StartFollow()
    {
        currentState = EnemyState.Follow;
        stateMachine.SetBool("isFollowing", true);
    }

    protected void StartAttack()
    {
        attackState.enabled = true;
    }

    public void StopAttack()
    {
        currentState = EnemyState.Follow;
        canmove = true;
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
