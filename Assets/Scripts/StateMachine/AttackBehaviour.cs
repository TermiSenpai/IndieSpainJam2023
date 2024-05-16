using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : StateMachineBehaviour
{
    [SerializeField] EnemyStats stats;
    private float attackTimer;
    private bool canAttack;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Attack(animator);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    private IEnumerator Attack(Animator animator)
    {
        yield return new WaitForSeconds(0.2f);

        // Lanzar un Raycast hacia la derecha desde la posición del objeto
        Vector2 raycastDirection = animator.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(animator.transform.position, raycastDirection, stats.attackLength, stats.damageableLayer);

        // Si el Raycast golpea algo
        if (hit.collider != null)
        {
            // Intenta obtener el componente IDamageable del objeto golpeado
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            // Si el objeto golpeado implementa la interfaz IDamageable, llama al método TakeDamage() en el objeto
            damageable?.TakeDamage(stats.damage);
        }

        canAttack = false;
        //animator.GetComponent<EnemyBrainController>().TryUpdateTarget();
    }

    protected virtual void CheckAttackDelay()
    {
        // Comprueba si el temporizador de ataque ha llegado a cero y si el enemigo puede atacar nuevamente.
        if (attackTimer <= 0 && !canAttack)
        {
            attackTimer = Mathf.Max(0, stats.hitDelay); // Evita valores negativos
            canAttack = true;
        }

        attackTimer -= Time.deltaTime;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
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
