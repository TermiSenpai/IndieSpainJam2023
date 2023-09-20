using System;
using UnityEngine;

public class EnemyTurretSearcher : MonoBehaviour
{
    EnemyBrainController controller;
    public float detectionRadius;
    public LayerMask turretLayer;

    GameObject[] turrets;
    GameObject currentTurret;
    float closeDistance;

    private void Start()
    {
        controller = GetComponent<EnemyBrainController>();
        closeDistance = Mathf.Infinity;

        turrets = GameObject.FindGameObjectsWithTag("Turret");

    }

    private void OnEnable()
    {
        turrets = GameObject.FindGameObjectsWithTag("Turret");
    }

    private void Update()
    {
        foreach (GameObject turret in turrets)
        {
            if (turret.activeInHierarchy)
            {
                // Calcular la distancia entre el jugador y la torreta.
                float distance = Vector3.Distance(transform.position, turret.transform.position);

                // Comprobar si esta torreta está más cerca que la anteriormente almacenada.
                if (distance < closeDistance)
                {
                    closeDistance = distance;
                    currentTurret = turret.gameObject;
                    controller.turret = currentTurret;
                }
            }
        }
    }
}
