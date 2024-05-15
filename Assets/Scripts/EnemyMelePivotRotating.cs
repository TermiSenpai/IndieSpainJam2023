using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelePivotRotating : MonoBehaviour
{
    public float rotationSpeed = 45.0f;
    public float distance = 2.0f;
    public float stopAngle = 30.0f; // �ngulo en grados para detener la rotaci�n
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
        // Calcular la direcci�n hacia el rat�n desde la posici�n del objeto.
        Vector3 targetDirection = (target.position - transform.position).normalized;

        // Calcular el �ngulo de rotaci�n en grados.
        float angulo = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        // Aplicar la rotaci�n al objeto.
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        // Verificar si el �ngulo es menor que el �ngulo de parada.
        if (Mathf.Abs(angulo) <= stopAngle)
        {
            detenerRotacion = true;
        }
        else
        {
            detenerRotacion = false;
        }

        // Establecer la velocidad de rotaci�n en funci�n de si debemos detenernos o no.
        if (detenerRotacion)
        {
            rotationSpeed = 0; // Detener la rotaci�n.
        }
        else
        {
            rotationSpeed = 45.0f; // Puedes ajustar la velocidad aqu�.
        }
    }


}
