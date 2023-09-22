using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyBrainController : MonoBehaviour
{
    [Header("References")]
    Animator stateMachine;
    public GameObject currentTarget;
    private GameObject player;
    private GameObject campfire;
    private GameObject turret;
    private EnemyAttack attackState;

    // tags
    const string campfireTag = "Campfire";
    const string playerTag = "Player";

    Vector2 campfirePos = Vector2.zero;
    Vector2 playerPos = Vector2.zero;
    Vector2 turretPos = Vector2.zero;


    [Header("Config")]
    [SerializeField] private float stopDistance = 1.25f;    
    [SerializeField] bool prioriceCampfire = false;
    public float maxCampfireDistance;


    private void Awake()
    {
        stateMachine = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag);
        campfire = GameObject.FindGameObjectWithTag(campfireTag);
        attackState = GetComponent<EnemyAttack>();        
    }
    private void Start()
    {
        maxCampfireDistance = Mathf.Infinity;
        currentTarget = campfire;
    }

    private void Update()
    {
        TryUpdateTarget();
        CheckStopDistance();
        LookTarget();
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
        currentTarget = campfire;
        Vector2 actualPos = transform.position;        

        if (campfire != null)
            campfirePos = campfire.transform.position;

        if (player != null)
            playerPos = player.transform.position;

        if (turret != null)
            turretPos = turret.transform.position;

        float campfireDistance = Vector3.Distance(actualPos, campfirePos);
        float playerDistance = Vector3.Distance(actualPos, playerPos);
        float turretDistance = Vector3.Distance(actualPos, turretPos);

        // Define una distancia máxima para la prioridad del "Campfire"
        float maxCampfireDistance = 10f; // Ajusta este valor según tu necesidad

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
            else StopEnemy();
        }
    }

    protected virtual void CheckStopDistance()
    {
        float distance;
        if (currentTarget != null)
            distance = Vector2.Distance(transform.position, currentTarget.transform.position);
        else
        {
            TryUpdateTarget();
            return;
        }
        // Comprobar si tenemos un objetivo y si estamos lo suficientemente lejos de él.
        if (distance > stopDistance)
            stateMachine.SetBool("isFollowing", true);
        else
        {
            attackState.Attack();
            StopEnemy();
        }
    }

    protected void StopEnemy()
    {
        stateMachine.SetBool("isFollowing", false);
    }

    public void SetTurret(GameObject newTurret)
    {
        turret = newTurret;
    }

    public GameObject GetTurret()
    {
        return turret;
    }

}
