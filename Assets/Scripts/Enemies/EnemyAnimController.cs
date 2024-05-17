using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    private Animator animator;

    // Obtener una referencia al componente Animator
    private void Awake() => animator = GetComponentInChildren<Animator>();

    // Establecer el valor del float en el Animator
    public void SetFloat(string parameterName, float value) => animator.SetFloat(parameterName, value);

    // Establecer el valor del float en el Animator
    public void Trigger(string parameterName) => animator.SetTrigger(parameterName);
}
