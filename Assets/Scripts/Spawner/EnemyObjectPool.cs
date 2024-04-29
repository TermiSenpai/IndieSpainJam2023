using UnityEngine;
using System.Collections.Generic;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool Instance;
    public GameObject enemyPrefab;
    public int poolSize = 10;

    private List<GameObject> enemyPool = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemyPool.Add(enemy);
        }
    }

    public GameObject GetEnemyFromPool()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return enemy;
            }
        }
        return null;
    }

    public void ReturnEnemyToPool(GameObject enemy)
    {
        enemy.SetActive(false);
    }
}
