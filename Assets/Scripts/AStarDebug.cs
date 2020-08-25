using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AStarDebug : MonoBehaviour
{
    [SerializeField]
    private Grid _Grid = default;

    [SerializeField]
    private Tilemap _Tilemap = default;

    [SerializeField]
    private Tile _Tile = default;

    [SerializeField]
    private Color _OpenColor = default;
    [SerializeField]
    private Color _ClosedColor = default;
    [SerializeField]
    private Color _PathColor = default;
    [SerializeField]
    private Color _StartColor = default;
    [SerializeField]
    private Color _GoalColor = default;

    [SerializeField]
    private GameObject _DebugTextPrefab = default;

    [SerializeField]
    private Canvas _DebugCanvas = default;

    private List<GameObject> _DebugObjects = new List<GameObject>();

    private bool _IsDebug = false;

    private static AStarDebug _Instance;
    public static AStarDebug Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<AStarDebug>();
            }

            return _Instance;
        }
    }

    public void CreateTiles(HashSet<Node> openNodes, HashSet<Node> closedNodes, NodeGrid nodeGrid, Vector3Int start, Vector3Int goal, Stack<Vector3Int> path = null)
    {
        if (!_IsDebug)
        {
            return;
        }

        ClearObjects();

        foreach (Node node in openNodes)
        {
            ColorTile(node.Position, _OpenColor);
        }

        foreach (Node node in closedNodes)
        {
            ColorTile(node.Position, _ClosedColor);
        }

        if (path != null)
        {
            foreach (Vector3Int pos in path)
            {
                ColorTile(pos, _PathColor);
            }
        }

        ColorTile(start, _StartColor);
        ColorTile(goal, _GoalColor);

        Dictionary<Vector3Int, Node> allNodes = nodeGrid.AllNodes;
        foreach (KeyValuePair<Vector3Int, Node> node in allNodes)
        {
            if (node.Value.Parent != null)
            {
                GameObject debugText = Instantiate(_DebugTextPrefab, _DebugCanvas.transform);
                DebugText text = debugText.GetComponent<DebugText>();

                debugText.transform.position = _Grid.CellToWorld(node.Value.Position);

                text.Arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0,
                    180 * Mathf.Atan2(node.Value.Parent.Position.y - node.Value.Position.y, node.Value.Parent.Position.x - node.Value.Position.x) / Mathf.PI));

                text.F.text = $"F:{node.Value.F}";
                text.G.text = $"G:{node.Value.G}";
                text.H.text = $"H:{node.Value.H}";
                text.P.text = $"P:{node.Value.Position.x}, {node.Value.Position.y}";

                _DebugObjects.Add(debugText);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _IsDebug = !_IsDebug;

            if (!_IsDebug)
            {
                ClearObjects();
            }
        }
    }

    private void ClearObjects()
    {
        foreach (GameObject obj in _DebugObjects)
        {
            Destroy(obj);
        }

        _Tilemap.ClearAllTiles();
    }

    public void ColorTile(Vector3Int pos, Color color)
    {
        _Tilemap.SetTile(pos, _Tile);
        _Tilemap.SetTileFlags(pos, TileFlags.None);
        _Tilemap.SetColor(pos, color);
    }
}
