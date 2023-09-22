using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class CreatingWorld : MonoBehaviour
{

    [SerializeField] private Tilemap m_Tilemap = null;

    [SerializeField] private List<TileData> tileDatas;
    private Dictionary<TileBase, TileData> dataFromTiles;


    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                Debug.Log(tile + "    "+ tileData);
                dataFromTiles.Add(tile, tileData);

            }
        }
        Debug.Log(dataFromTiles.Values);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
