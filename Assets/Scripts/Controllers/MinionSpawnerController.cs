using UnityEngine;
using UnityEngine.Tilemaps;

public class MinionSpawnerController : MonoBehaviour
{
    public GameObject MinionPrefab;
    public Transform Player;

    [SerializeField]
    private PathingGrid _Grid = default;

    [SerializeField]
    private float _SpawnTimer = 5;

    private float _LastSpawnTime = 0;

    private void Start()
    {
        if (Player == null)
        {
            Debug.LogError("PlayerTransform is null. Minions can't follow player.");
        }
    }

    void Update()
    {
        if (Time.time - _LastSpawnTime > _SpawnTimer && _Grid != null && _Grid.Tilemap != null)
        {
            Vector3Int gridSize = _Grid.Tilemap.cellBounds.size;
            Vector3Int gridSizeMin = _Grid.Tilemap.cellBounds.min;

            Vector3Int randomGridCell = new Vector3Int(gridSizeMin.x + (int)(Random.value * gridSize.x), gridSizeMin.y + (int)(Random.value * gridSize.y), 0);
            while (!_Grid.IsOpen(randomGridCell))
            {
                randomGridCell = new Vector3Int(gridSizeMin.x + (int)(Random.value * gridSize.x), gridSizeMin.y + (int)(Random.value * gridSize.y), 0);
            }

            Vector3 position = _Grid.Tilemap.GetCellCenterWorld(randomGridCell);

            GameObject minion = Instantiate(MinionPrefab, position, Quaternion.identity);
            MinionController controller = minion.GetComponent<MinionController>();
            controller.PlayerTransform = Player;

            _LastSpawnTime = Time.time;
        }
    }
}
