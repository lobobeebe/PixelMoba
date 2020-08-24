using UnityEngine;
using UnityEngine.Tilemaps;

public class PathingGrid : MonoBehaviour
{
    [SerializeField]
    private Tilemap _Tilemap;

    public Tilemap Tilemap { get => _Tilemap; set => _Tilemap = value; }

    public bool IsOpen(Vector3Int pos)
    {
        AStarTile tile = _Tilemap.GetTile<AStarTile>(pos);

        if (tile != null && tile.TileType == TileType.OPEN)
        {
            return true;
        }

        return false;
    }
}
