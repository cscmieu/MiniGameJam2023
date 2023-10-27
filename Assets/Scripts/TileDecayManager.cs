using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDecayManager : MonoBehaviour
{
    [SerializeField] 
    private Tilemap tilemap;
    [SerializeField] 
    private TileBase decayTile;
    [SerializeField] 
    private Damaged damagedPrefab;
    [SerializeField] 
    private Vector3Int decayStartPosition;
    [SerializeField] 
    private float spreadChanceSide;
    [SerializeField] 
    private float spreadChanceUp; 
    [SerializeField] 
    private float spreadChanceDown;
    
    private List<Vector3Int> _activeDecay = new List<Vector3Int>();

    private void Start()
    {
        TileData data = MapManager.Instance.GetTileData(decayStartPosition);
        SetTileOnDecay(decayStartPosition, data);
    }

    public void FinishedDecaying(Vector3Int position)
    {
        tilemap.SetTile(position, decayTile);
        _activeDecay.Remove(position);
    }

    public void TryToSpread(Vector3Int position)
    {
        TryToDecayTile(new Vector3Int(position.x-1, position.y, 0),Direction.Side);
        TryToDecayTile(new Vector3Int(position.x+1, position.y, 0),Direction.Side);
        TryToDecayTile(new Vector3Int(position.x, position.y-1, 0),Direction.Down);
        TryToDecayTile(new Vector3Int(position.x, position.y+1, 0),Direction.Up);

        //Local function
        void TryToDecayTile(Vector3Int tilePosition, Direction spreadDirection)
        {
            if (_activeDecay.Contains(tilePosition)) return;

            TileData data = MapManager.Instance.GetTileData(tilePosition);

            if(data != null && data.canDecay)
            {
                if (UnityEngine.Random.Range(0f, 100f) <= ( (spreadDirection == Direction.Side)?  spreadChanceSide : (spreadDirection == Direction.Up)?  spreadChanceUp : spreadChanceDown ))
                    SetTileOnDecay(tilePosition, data);

            }

        }
    }

    private void SetTileOnDecay(Vector3Int position, TileData data)
    {
        Damaged newDamaged = Instantiate(damagedPrefab);
        newDamaged.transform.position = tilemap.GetCellCenterWorld(position);
        newDamaged.StartDecayed(position, data, this);
        
        _activeDecay.Add(position);
    }
    
    public enum Direction
    {
        Side,
        Up,
        Down
    }
}
