using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    private Transform[] spawnpoints;


    private void Start()
    {
        spawnpoints = GetComponentsInChildren<Transform>();
    }

    public Transform GetRandomSpawnpoint()
    {
        return spawnpoints[Random.Range(0, spawnpoints.Length)];
    }
}
