using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType { OPEN, CLOSED }

public class AStar : MonoBehaviour
{
    private TileType _TileType;

    [SerializeField]
    private PathingGrid _Grid = default;

    [SerializeField]
    private Camera _Camera = default;

    [SerializeField]
    private LayerMask _LayerMask = default;

    [SerializeField]
    private Vector3Int _StartPos = default;

    [SerializeField]
    private Vector3Int _GoalPos = default;

    private Node _CurrentNode;
    private HashSet<Node> _OpenList;
    private HashSet<Node> _ClosedList;
    private Stack<Vector3Int> _Path;

    private Dictionary<Vector3Int, Node> _AllNodes = new Dictionary<Vector3Int, Node>();
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = _Camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity, _LayerMask);

            if (hit.collider != null)
            {
                Vector3Int clickPos = _Grid.Tilemap.WorldToCell(mousePos);

                ChangeTile(clickPos);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Algorithm(_StartPos, _GoalPos);
        }
    }

    private void ChangeTile(Vector3Int clickPos)
    {
        //_Tilemap.SetTile(clickPos, _Tiles[0]);
    }

    private void Algorithm(Vector3Int start, Vector3Int goal)
    {
        _CurrentNode = GetNode(_StartPos);

        _OpenList = new HashSet<Node>();
        _ClosedList = new HashSet<Node>();

        _OpenList.Add(_CurrentNode);

        while (_OpenList.Count > 0 && _Path == null)
        {
            List<Node> neighbors = FindNeighbors(_CurrentNode.Position);

            ExamineNeighbors(neighbors, _CurrentNode);

            UpdateCurrentTile(ref _CurrentNode);

            _Path = GeneratePath(_CurrentNode);
        }

        AStarDebug.Instance?.CreateTiles(_OpenList, _ClosedList, _AllNodes, _StartPos, _GoalPos, _Path);
    }

    private Node GetNode(Vector3Int position)
    {
        if (_AllNodes.ContainsKey(position))
        {
            return _AllNodes[position];
        }

        Node node = new Node(position);
        _AllNodes.Add(position, node);
        return node;
    }

    private List<Node> FindNeighbors(Vector3Int parentPos)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; ++x)
        {
            for (int y = -1; y <= 1; ++y)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int neighborPos = parentPos + new Vector3Int(x, y, 0);

                    if (neighborPos != _StartPos)
                    {
                        if (_Grid.IsOpen(neighborPos))
                        {
                            neighbors.Add(GetNode(neighborPos));
                        }
                    }
                }
            }
        }

        return neighbors;
    }

    private bool CanMove(Node current, Node neighbor)
    {
        Vector3Int direct = current.Position - neighbor.Position;

        Vector3Int first = new Vector3Int(current.Position.x + (direct.x * -1), current.Position.y, current.Position.z);
        Vector3Int second = new Vector3Int(current.Position.x, current.Position.y + (direct.y * -1), current.Position.z);

        if (!_Grid.IsOpen(first) || !_Grid.IsOpen(first))
        {
            return false;
        }

        return true;
    }

    private int DetermineGScore(Vector3Int neighbor, Vector3Int current)
    {
        int gScore;

        int x = current.x - neighbor.x;
        int y = current.y - neighbor.y;

        if (Mathf.Abs(x - y) % 2 == 1)
        {
            gScore = 10;
        }
        else
        {
            gScore = 14;
        }

        return gScore;
    }

    private void ExamineNeighbors(List<Node> neighbors, Node currentNode)
    {
        foreach (Node neighbor in neighbors)
        {
            int gScore = DetermineGScore(neighbor.Position, currentNode.Position);

            if (!CanMove(currentNode, neighbor))
            {
                continue;
            }

            if (_OpenList.Contains(neighbor))
            {
                if (currentNode.G + gScore < neighbor.G)
                {
                    CalcValues(currentNode, neighbor, gScore);
                }
            }
            else if (!_ClosedList.Contains(neighbor))
            {
                CalcValues(currentNode, neighbor, gScore);
                _OpenList.Add(neighbor);
            }
        }
    }

    private void CalcValues(Node parent, Node neighbor, int cost)
    {
        neighbor.Parent = parent;

        neighbor.G = parent.G + cost;
        neighbor.H = (Mathf.Abs(neighbor.Position.x - _GoalPos.x) + Mathf.Abs(neighbor.Position.y - _GoalPos.y)) * 10;
        neighbor.F = neighbor.G + neighbor.H;
    }

    private void UpdateCurrentTile(ref Node current)
    {
        _OpenList.Remove(current);
        _ClosedList.Add(current);

        if (_OpenList.Count > 0)
        {
            current = _OpenList.OrderBy(x => x.F).First();
        }
    }

    private Stack<Vector3Int> GeneratePath(Node currentNode)
    {
        if (currentNode.Position == _GoalPos)
        {
            Stack<Vector3Int> finalPath = new Stack<Vector3Int>();

            while (currentNode.Position != _StartPos)
            {
                finalPath.Push(currentNode.Position);
                currentNode = currentNode.Parent;
            }

            return finalPath;
        }

        return null;
    }
}
