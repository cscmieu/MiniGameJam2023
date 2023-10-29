using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class TileData : ScriptableObject
{
    public TileBase[] tiles;
    
    public bool canDecay;
    
    public float spreadInterval, decayTime;
}