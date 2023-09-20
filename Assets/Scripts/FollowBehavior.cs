using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : StateMachineBehaviour
{
    const string playerTag = "Player";
    const string campfireTag = "Campfire";
    const string turretTag = "Turret";

    public GameObject player;
    public GameObject campfire;
    public GameObject turret;

    Vector3 currentTargetPos;
    private readonly float stopDistance = 1.5f;

    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        campfire = GameObject.FindGameObjectWithTag(campfireTag);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TryUpdateTarget(animator);
        MoveToTarget(animator);
        CheckStopDistance(animator);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void TryUpdateTarget(Animator animator)
    {
        Vector3 actualPos = animator.transform.position;
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
            currentTargetPos = campfire.transform.position; // Campamento es el objetivo más cercano
        }
        else if (minDistance == playerDistance)
        {
            currentTargetPos = player.transform.position; // Jugador es el objetivo más cercano
        }
        //else
        //{
        //    currentTarget = turret.transform; // Torreta es el objetivo más cercano
        //}
    }

    private void MoveToTarget(Animator animator)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, currentTargetPos, speed * Time.deltaTime);
    }

    private void CheckStopDistance(Animator animator)
    {
        // Comprobar si tenemos un objetivo y si estamos lo suficientemente lejos de él.
        if (currentTargetPos != null && Vector3.Distance(animator.transform.position, currentTargetPos) < stopDistance)
        {
            animator.SetBool("isFollowing", false);

        }
    }

    private void CheckTurret()
    {

    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
