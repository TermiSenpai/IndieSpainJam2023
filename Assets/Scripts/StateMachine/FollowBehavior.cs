using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavior : StateMachineBehaviour
{
    public EnemyBrainController controller;

    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller = animator.GetComponent<EnemyBrainController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //MoveToTarget(animator);
    }

    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    //private void MoveToTarget(Animator animator)
    //{
    //    if (controller.currentTarget == null)
    //        return;

    //    animator.transform.position = Vector2.MoveTowards(animator.transform.position,
    //                                  controller.currentTarget.transform.position,
    //                                  speed * Time.deltaTime);
    //    Vector2 direccion = ((Vector2)controller.currentTarget.transform.position - (Vector2)animator.transform.position).normalized;

    //    float direccionX = direccion.x;
    //    float direccionY = direccion.y;

    //    // floats en el Animator
    //    animator.SetFloat("Horizontal", direccionX);
    //    animator.SetFloat("Vertical", direccionY);
    //}


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
