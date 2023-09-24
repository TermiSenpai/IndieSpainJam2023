using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPointManager spawnPointManager;

    [SerializeField] GameObject[] itemsList;

    private void Start()
    {
        foreach(var item in itemsList)
        {
            Instantiate(item, spawnPointManager.GetRandomSpawnpoint());
        }
    }
}
