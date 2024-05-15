using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelePivotRotating : MonoBehaviour
{
    public float rotationSpeed = 45.0f;
    public float distance = 2.0f;
    public float stopAngle = 30.0f; // Ángulo en grados para detener la rotación
    Transform target;
    FollowState followState;

    private bool detenerRotacion = false;

    private void Start()
    {
        followState = GetComponentInParent<FollowState>();
    }

    void Update()
    {
        target = followState.currentTarget;
        if (target == null) return;
        // Calcular la dirección hacia el ratón desde la posición del objeto.
        Vector3 targetDirection = (target.position - transform.position).normalized;

        // Calcular el ángulo de rotación en grados.
        float angulo = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // Aplicar la rotación al objeto.
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        // Verificar si el ángulo es menor que el ángulo de parada.
        if (Mathf.Abs(angulo) <= stopAngle)
        {
            detenerRotacion = true;
        }
        else
        {
            detenerRotacion = false;
        }

        // Establecer la velocidad de rotación en función de si debemos detenernos o no.
        if (detenerRotacion)
        {
            rotationSpeed = 0; // Detener la rotación.
        }
        else
        {
            rotationSpeed = 45.0f; // Puedes ajustar la velocidad aquí.
        }
    }


}
