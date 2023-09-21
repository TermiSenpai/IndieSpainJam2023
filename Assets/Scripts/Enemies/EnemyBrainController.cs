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

    [Header("Config")]
    [SerializeField] private float stopDistance = 1.25f;
    readonly float maxCampfireDistance = 50f; // Ajusta este valor según tu necesidad
    [SerializeField] bool prioriceCampfire = false;



    private void Start()
    {
        stateMachine = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag);
        campfire = GameObject.FindGameObjectWithTag(campfireTag);
        attackState = GetComponent<EnemyAttack>();

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

    protected virtual void TryUpdateTarget()
    {
        Vector2 actualPos = transform.position;
        Vector2 campfirePos = campfire.transform.position;
        Vector2 playerPos = player.transform.position;
        Vector2 turretPos = Vector2.positiveInfinity;

        // Check if exist a torret nearby
        if (turret != null)
            turretPos = turret.transform.position;

        float campfireDistance = Vector3.Distance(actualPos, campfirePos);
        float playerDistance = Vector3.Distance(actualPos, playerPos);
        float turretDistance = Vector3.Distance(actualPos, turretPos);

        // Define una distancia máxima para la prioridad del "Campfire"
        float maxCampfireDistance = 10f; // Ajusta este valor según tu necesidad

        if (campfire != null && campfireDistance <= maxCampfireDistance || prioriceCampfire)
        {
            // Campamento está presente y dentro de la distancia máxima
            currentTarget = campfire; // Campamento es el objetivo más cercano
        }
        else
        {
            float minDistance = Mathf.Min(playerDistance, turretDistance);

            if (minDistance == playerDistance)
                currentTarget = player; // Jugador es el objetivo más cercano

            else
                currentTarget = turret; // Torreta es el objetivo más cercano
        }
    }

    protected virtual void CheckStopDistance()
    {
        float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
        // Comprobar si tenemos un objetivo y si estamos lo suficientemente lejos de él.
        if (distance > stopDistance)
            stateMachine.SetBool("isFollowing", true);
        else
        {
            attackState.Attack();
            stateMachine.SetBool("isFollowing", false);
        }
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
