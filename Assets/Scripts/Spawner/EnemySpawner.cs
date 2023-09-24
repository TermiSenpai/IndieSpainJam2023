using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyObjectPool enemyPool;
    [SerializeField] private bool canSpawn;

    [SerializeField] float maxSpawnDelay;
    [SerializeField] float currentTimer;

    [SerializeField] SpawnPointManager spawnPointManager;


    private void OnEnable()
    {
        DayCycle.NightStart += SpawnEnemies;
        DayCycle.DayStart += StopSpawn;
    }

    private void OnDisable()
    {
        DayCycle.NightStart -= SpawnEnemies;
        DayCycle.DayStart -= StopSpawn;
    }

    private void Update()
    {
        if (!canSpawn) return;

        currentTimer -= Time.deltaTime;

        if (currentTimer <= 0)
        {
            GameObject enemy = enemyPool.GetEnemyFromPool();
            if (enemy != null)
            {
                // Configura la posición y otros atributos del enemigo
                enemy.transform.position = spawnPointManager.GetRandomSpawnpoint().position;
                // Activa el enemigo
                enemy.SetActive(true);
                currentTimer = maxSpawnDelay;
            }
        }
    }

    void SpawnEnemies()
    {
        canSpawn = true;
        currentTimer = 0;
    }

    void StopSpawn()
    {
        canSpawn = false;
        // Detén la generación de enemigos o realiza otras acciones al terminar el ciclo nocturno
    }
}
