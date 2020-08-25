using UnityEngine;
using UnityEngine.Tilemaps;

public class MinionSpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _MinionPrefab = default;

    [SerializeField]
    private Transform _Player = default;

    [SerializeField]
    private PathingGrid _Grid = default;

    [SerializeField]
    private float _SpawnTimer = 5;

    [SerializeField]
    private Transform _SpawnPoint = default;
    
    private float _LastSpawnTime = 0;

    private void Start()
    {
        if (_Player == null)
        {
            Debug.LogError("PlayerTransform is null. Minions can't follow player.");
        }

        if (_SpawnPoint == null)
        {
            Debug.LogError("SpawnPoint is null. Minions cannot spawn.");
        }
    }

    void Update()
    {
        if (Time.time - _LastSpawnTime > _SpawnTimer && _Grid != null && _Grid.Tilemap != null && _SpawnPoint != null)
        {
            Vector3 position = _Grid.Tilemap.GetCellCenterWorld(_Grid.Tilemap.WorldToCell(_SpawnPoint.position));
            position.z = -1;

            GameObject minion = Instantiate(_MinionPrefab, position, Quaternion.identity);
            MinionController controller = minion.GetComponent<MinionController>();
            controller.PlayerTransform = _Player;

            _LastSpawnTime = Time.time;
        }
    }
}
