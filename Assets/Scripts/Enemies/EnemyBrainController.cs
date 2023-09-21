using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyBrainController : MonoBehaviour
{
    Animator stateMachine;
    public GameObject currentTarget;
    private GameObject player;
    private GameObject campfire;
    private GameObject turret;

    const string playerTag = "Player";
    const string campfireTag = "Campfire";
    EnemyAttack attackState;

    [SerializeField] float attackDelay;
    private float attackTimer;
    private bool canAttack;


    [SerializeField] private float stopDistance = 1.5f;

    private void Start()
    {
        stateMachine = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag);
        campfire = GameObject.FindGameObjectWithTag(campfireTag);
        attackState = GetComponent<EnemyAttack>();
        attackTimer = attackDelay;
    }

    private void Update()
    {
        TryUpdateTarget();
        CheckStopDistance();
        LookTarget();
        CheckAttackDelay();
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

        float minDistance = Mathf.Min(campfireDistance, playerDistance, turretDistance);

        if (minDistance == campfireDistance)
            currentTarget = campfire; // Campamento es el objetivo más cercano

        else if (minDistance == playerDistance)
            currentTarget = player; // Jugador es el objetivo más cercano

        else
            currentTarget = turret; // Torreta es el objetivo más cercano
    }

    protected virtual void CheckStopDistance()
    {
        float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
        // Comprobar si tenemos un objetivo y si estamos lo suficientemente lejos de él.
        if (distance > stopDistance)
            stateMachine.SetBool("isFollowing", true);
        else
        {
            if (canAttack)
            {
                canAttack = false;
                attackState.Attack();
            }

            stateMachine.SetBool("isFollowing", false);
        }
    }

    protected virtual void CheckAttackDelay()
    {
        // Comprueba si el temporizador de ataque ha llegado a cero y si el enemigo puede atacar nuevamente.
        if (attackTimer <= 0 && !canAttack)
        {
            attackTimer = Mathf.Max(0, attackDelay); // Evita valores negativos
            canAttack = true;
        }

        attackTimer -= Time.deltaTime;
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
