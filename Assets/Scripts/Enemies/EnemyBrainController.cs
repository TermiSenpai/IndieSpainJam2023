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

    protected virtual void TryUpdateTarget()
    {
        Vector2 actualPos = transform.position;
        Vector2 campfirePos = campfire.transform.position;
        Vector2 playerPos = player.transform.position;
        Vector2 turretPos = Vector2.positiveInfinity;
        if (turret != null)
            turretPos = turret.transform.position;

        float campfireDistance = Vector3.Distance(actualPos, campfirePos);
        float playerDistance = Vector3.Distance(actualPos, playerPos);
        float turretDistance = Vector3.Distance(actualPos, turretPos);

        float minDistance = Mathf.Min(campfireDistance, playerDistance, turretDistance);
        //float minDistance = Mathf.Min(campfireDistance, playerDistance);

        if (minDistance == campfireDistance)
        {
            currentTarget = campfire; // Campamento es el objetivo más cercano
        }
        else if (minDistance == playerDistance)
        {
            currentTarget = player; // Jugador es el objetivo más cercano
        }
        else
        {
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
            stateMachine.SetBool("isFollowing", false);

    }

}
