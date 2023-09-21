using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogUpdate : MonoBehaviour
{
    public GameObject fogObj;
    private Tilemap fog;

    // Start is called before the first frame update
    void Start()
    {
        fogObj.SetActive(true);
        fog = fogObj.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        UpdateFog();
    }

    private void UpdateFog()
    {
        Vector3Int currentPlayerPos = fog.WorldToCell(transform.position);

        for (int i = -3; i <= 3; i++)
        {
            for (int j = -5; j <= 5; j++)
            {
                fog.SetTile(currentPlayerPos + new Vector3Int(i, j, 0), null);
            }
        }
    }
}
