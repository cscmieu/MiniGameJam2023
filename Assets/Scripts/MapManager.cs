using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;
    
    [SerializeField] 
    private Tilemap tilemap;
    [SerializeField] 
    private List<TileData> tilesData;
    
    private readonly Dictionary<TileBase, TileData> _dataFromTiles = new Dictionary<TileBase, TileData>();

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
        
        foreach (var tileData in tilesData)
        {
            foreach (var tile in tileData.tiles)
            {
                _dataFromTiles.Add(tile, tileData);
            }
        }
    }

    public TileData GetTileData(Vector3Int position)
    {
        var tile = tilemap.GetTile(position);
        return tile.IsUnityNull() ? null : _dataFromTiles[tile];
    }
}
