using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New AStarTile", menuName = "Tiles/AStarTile")]
public class AStarTile : Tile
{
    [SerializeField]
    private TileType _TileType;

    public TileType TileType { get => _TileType; set => _TileType = value; }
}
