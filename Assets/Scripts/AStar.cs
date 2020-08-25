using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum TileType { OPEN, CLOSED }

public class AStar
{
    private Node _CurrentNode;
    private HashSet<Node> _OpenList;
    private HashSet<Node> _ClosedList;

    public Stack<Vector3Int> Algorithm(Vector3Int start, Vector3Int goal, NodeGrid grid)
    {
        _CurrentNode = grid.GetNode(start);

        _OpenList = new HashSet<Node>();
        _ClosedList = new HashSet<Node>();

        _OpenList.Add(_CurrentNode);

        Stack<Vector3Int> path = null;

        while (_OpenList.Count > 0 && path == null)
        {
            List<Node> neighbors = grid.FindNeighbors(_CurrentNode.Position);

            ExamineNeighbors(neighbors, _CurrentNode, goal, grid);

            UpdateCurrentTile(ref _CurrentNode);

            path = GeneratePath(_CurrentNode, start, goal);
        }

        AStarDebug.Instance?.CreateTiles(_OpenList, _ClosedList, grid, start, goal, path);

        return path;
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

    private void ExamineNeighbors(List<Node> neighbors, Node currentNode, Vector3Int goalPos, NodeGrid grid)
    {
        foreach (Node neighbor in neighbors)
        {
            int gScore = DetermineGScore(neighbor.Position, currentNode.Position);

            if (!grid.CanMove(currentNode, neighbor))
            {
                continue;
            }

            if (_OpenList.Contains(neighbor))
            {
                if (currentNode.G + gScore < neighbor.G)
                {
                    CalcValues(currentNode, neighbor, goalPos, gScore);
                }
            }
            else if (!_ClosedList.Contains(neighbor))
            {
                CalcValues(currentNode, neighbor, goalPos, gScore);
                _OpenList.Add(neighbor);
            }
        }
    }

    private void CalcValues(Node parent, Node neighbor, Vector3Int goalPos, int cost)
    {
        neighbor.Parent = parent;

        neighbor.G = parent.G + cost;
        neighbor.H = (Mathf.Abs(neighbor.Position.x - goalPos.x) + Mathf.Abs(neighbor.Position.y - goalPos.y)) * 10;
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

    private Stack<Vector3Int> GeneratePath(Node currentNode, Vector3Int startPos, Vector3Int goalPos)
    {
        if (currentNode.Position == goalPos)
        {
            Stack<Vector3Int> finalPath = new Stack<Vector3Int>();

            while (currentNode.Position != startPos)
            {
                finalPath.Push(currentNode.Position);
                currentNode = currentNode.Parent;
            }

            return finalPath;
        }

        return null;
    }
}
