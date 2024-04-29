using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordRotating : MonoBehaviour
{
    public float velocidadRotacion = 45.0f;
    public float distanciaDesdeJugador = 2.0f;
    public float anguloParada = 30.0f; // �ngulo en grados para detener la rotaci�n

    private bool detenerRotacion = false;

    void Update()
    {
        // Seguir la posici�n del rat�n en pantalla.
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calcular la direcci�n hacia el rat�n desde la posici�n del objeto.
        Vector3 direccionAlRaton = (mousePos - transform.position).normalized;

        // Calcular el �ngulo de rotaci�n en grados.
        float angulo = Mathf.Atan2(direccionAlRaton.y, direccionAlRaton.x) * Mathf.Rad2Deg;

        // Aplicar la rotaci�n al objeto.
        transform.rotation = Quaternion.Euler(0, 0, angulo);

        // Verificar si el �ngulo es menor que el �ngulo de parada.
        if (Mathf.Abs(angulo) <= anguloParada)
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
            velocidadRotacion = 0; // Detener la rotaci�n.
        }
        else
        {
            velocidadRotacion = 45.0f; // Puedes ajustar la velocidad aqu�.
        }
    }




}
