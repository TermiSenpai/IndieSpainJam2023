using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    const string campfireTag = "Campfire";

    public GameObject campfire;
    private readonly float stopDistance = 1.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        campfire = GameObject.FindGameObjectWithTag(campfireTag);
        CheckStopDistance(animator);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CheckStopDistance(animator);
    }

   
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private void CheckStopDistance(Animator animator)
    {
        // Comprobar si tenemos un objetivo y si estamos lo suficientemente lejos de él.
        if (campfire != null && Vector3.Distance(animator.transform.position, campfire.transform.position) > stopDistance)
        {
            animator.SetBool("isFollowing", true);
        }
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
