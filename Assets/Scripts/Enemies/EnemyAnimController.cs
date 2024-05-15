using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Obtener una referencia al componente Animator
        animator = GetComponent<Animator>();
    }

    public void SetFloat(string parameterName, float value)
    {
        // Establecer el valor del float en el Animator
        animator.SetFloat(parameterName, value);
    }
    public void Trigger(string parameterName)
    {
        // Establecer el valor del float en el Animator
        animator.SetTrigger(parameterName);
    }
}
