using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRotating : MonoBehaviour
{
    public float velocidadRotacion = 45.0f;
    public float distanciaDesdeJugador = 2.0f;
    public float anguloParada = 30.0f; // Ángulo en grados para detener la rotación

    private bool detenerRotacion = false;

    void Update()
    {
        // Seguir la posición del ratón en pantalla.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcular la dirección hacia el ratón desde la posición del objeto.
        Vector3 direccionAlRaton = (mousePos - transform.position).normalized;

        // Calcular el ángulo de rotación en grados.
        float angulo = Mathf.Atan2(direccionAlRaton.y, direccionAlRaton.x) * Mathf.Rad2Deg;

        // Aplicar la rotación al objeto.
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        // Verificar si el ángulo es menor que el ángulo de parada.
        if (Mathf.Abs(angulo) <= anguloParada)
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
            velocidadRotacion = 0; // Detener la rotación.
        }
        else
        {
            velocidadRotacion = 45.0f; // Puedes ajustar la velocidad aquí.
        }
    }




}
