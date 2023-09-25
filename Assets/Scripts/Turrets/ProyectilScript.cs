using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ProyectilScript : MonoBehaviour
{
    public float velocidad = 5.0f; // Velocidad del proyectil
    float damage = 5f;
    private Vector2 direccion; // Dirección del proyectil

    // Método para configurar la dirección del proyectil
    public void ConfigurarDireccion(Vector2 nuevaDireccion)
    {
        direccion = nuevaDireccion;
    }

    private void Update()
    {
        // Mueve el proyectil en la dirección especificada
        Vector2 movimiento = direccion * velocidad * Time.deltaTime;
        transform.Translate(movimiento);

        // Si deseas destruir el proyectil cuando esté fuera de la pantalla u otra condición, puedes hacerlo aquí.
        // Por ejemplo, puedes usar un temporizador para destruir el proyectil después de un cierto tiempo.
    }

    // Puedes agregar lógica adicional para detectar colisiones u otros comportamientos aquí
    // Por ejemplo, puedes destruir el proyectil al colisionar con un objeto o al salir de la pantalla.

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta colisiones con otros objetos (si es necesario)
        // Por ejemplo, puedes destruir el proyectil cuando colisiona con un enemigo.
        if (other.gameObject.layer == 10)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(damage);
            // Realiza acciones cuando el proyectil colisiona con un enemigo
            Destroy(gameObject);
        }
    }
}
