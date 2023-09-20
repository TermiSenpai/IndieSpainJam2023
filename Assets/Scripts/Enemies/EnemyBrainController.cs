using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyBrainController : MonoBehaviour
{
    Animator stateMachine;
    public GameObject currentTarget;
    public GameObject player;
    public GameObject campfire;
    public GameObject turret;

    const string playerTag = "Player";
    const string campfireTag = "Campfire";
    const string turretTag = "Turret";


    private readonly float stopDistance = 1.5f;

    private void Start()
    {
        stateMachine = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag);
        campfire = GameObject.FindGameObjectWithTag(campfireTag);
    }

    private void Update()
    {
        TryUpdateTarget();
        CheckStopDistance();
    }

    private void TryUpdateTarget()
    {
        Vector3 actualPos = transform.position;
        Vector3 campfirePos = campfire.transform.position;
        Vector3 playerPos = player.transform.position;
        //Vector3 turretPos = turret.transform.position; // Asumiendo que tienes una referencia al GameObject de la torreta

        float campfireDistance = Vector3.Distance(actualPos, campfirePos);
        float playerDistance = Vector3.Distance(actualPos, playerPos);
        // float turretDistance = Vector3.Distance(actualPos, turretPos);

        //float minDistance = Mathf.Min(campfireDistance, playerDistance, turretDistance);
        float minDistance = Mathf.Min(campfireDistance, playerDistance);

        if (minDistance == campfireDistance)
        {
            currentTarget = campfire; // Campamento es el objetivo más cercano
        }
        else if (minDistance == playerDistance)
        {
            currentTarget = player; // Jugador es el objetivo más cercano
        }
        //else
        //{
        //    currentTarget = turret.transform; // Torreta es el objetivo más cercano
        //}
    }

    private void CheckStopDistance()
    {
        float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
        // Comprobar si tenemos un objetivo y si estamos lo suficientemente lejos de él.
        if (distance > stopDistance)
            stateMachine.SetBool("isFollowing", true);
        else
            stateMachine.SetBool("isFollowing", false);

    }

}
