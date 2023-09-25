using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    [SerializeField] private TurretStats stats;

    private void Update()
    {
        // Comprueba si hay colisiones en el radio de detección
        Collider2D[] objetosDetectados = Physics2D.OverlapCircleAll(transform.position, stats.rangeRadius, stats.enemyLayer);

        foreach (Collider2D objeto in objetosDetectados)
        {
            // Comprueba si el objeto detectado es el objetivo
            
                // Calcula la dirección hacia el objetivo
                Vector2 direccion = objeto.transform.position - transform.position;
                direccion.Normalize();

                // Instancia el proyectil y configura su dirección
                GameObject proyectil = Instantiate(stats.Proyectil, transform.position, Quaternion.identity);
                ProyectilScript proyectilScript = proyectil.GetComponent<ProyectilScript>();
                if (proyectilScript != null)
                {
                    proyectilScript.ConfigurarDireccion(direccion);
                }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, stats.rangeRadius);
    }
}
