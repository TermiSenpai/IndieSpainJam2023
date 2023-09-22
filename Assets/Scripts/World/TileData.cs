using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Construct = 0,
    Fireplace = 1,
    Nothing = 2,
    Forest = 3
}

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    // Start is called before the first frame update
    public TileBase[] tiles;

    public TileType Type;

    public GameObject GObject;

    public Dictionary<Vector3Int, GameObject> OcupedPos = new Dictionary<Vector3Int, GameObject>();
}
