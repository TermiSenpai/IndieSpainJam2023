using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance;

    public Transform[] spawnpoints;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        spawnpoints = GetComponentsInChildren<Transform>();
    }

    public Transform GetRandomSpawnpoint()
    {
        return spawnpoints[Random.Range(0, spawnpoints.Length)];
    }
}
