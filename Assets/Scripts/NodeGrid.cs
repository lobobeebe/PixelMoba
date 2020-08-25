using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeGrid : MonoBehaviour
{
    [SerializeField]
    private Tilemap _Tilemap;
    public Tilemap Tilemap { get => _Tilemap; set => _Tilemap = value; }

    public Dictionary<Vector3Int, Node> AllNodes { get; set; } = new Dictionary<Vector3Int, Node>();

    public Node GetNode(Vector3Int position)
    {
        if (AllNodes.ContainsKey(position))
        {
            return AllNodes[position];
        }

        Node node = new Node(position);
        AllNodes.Add(position, node);
        return node;
    }
    
    public bool IsOpen(Vector3Int pos)
    {
        AStarTile tile = _Tilemap.GetTile<AStarTile>(pos);

        if (tile != null && tile.TileType == TileType.OPEN)
        {
            return true;
        }

        return false;
    }

    public bool CanMove(Node current, Node neighbor)
    {
        Vector3Int direct = current.Position - neighbor.Position;

        Vector3Int first = new Vector3Int(current.Position.x + (direct.x * -1), current.Position.y, current.Position.z);
        Vector3Int second = new Vector3Int(current.Position.x, current.Position.y + (direct.y * -1), current.Position.z);

        if (!IsOpen(first) || !IsOpen(second))
        {
            return false;
        }

        return true;
    }

    public List<Node> FindNeighbors(Vector3Int parentPos)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; ++x)
        {
            for (int y = -1; y <= 1; ++y)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int neighborPos = parentPos + new Vector3Int(x, y, 0);

                    if (IsOpen(neighborPos))
                    {
                        neighbors.Add(GetNode(neighborPos));
                    }
                }
            }
        }

        return neighbors;
    }
}
