using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(NodeGrid))]
public class PathMaker : MonoBehaviour
{
    private static PathMaker _Instance;
    public static PathMaker Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<PathMaker>();
            }
            return _Instance;
        }
    }

    // Using AStar to create paths
    private AStar _AStar = new AStar();

    // Components
    private NodeGrid _Grid;

    private void Start()
    {
        _Grid = GetComponent<NodeGrid>();

        if (_Grid == null)
        {
            Debug.LogError("Grid is null. Cannot make paths.");
        }
    }

    public Queue<Vector3> GetWorldPath(Vector3 start, Vector3 goal)
    {
        Queue<Vector3> worldPath = new Queue<Vector3>();
        Vector3Int startPos = _Grid.Tilemap.WorldToCell(start);
        Vector3Int goalPos = _Grid.Tilemap.WorldToCell(goal);

        Stack<Vector3Int> path = _AStar.Algorithm(startPos, goalPos, _Grid);

        if (path != null)
        {
            foreach (Vector3Int pos in path)
            {
                Vector3 worldPos = _Grid.Tilemap.GetCellCenterWorld(pos);
                worldPath.Enqueue(worldPos);
            }
        }

        return worldPath;
    }
}
