using UnityEngine;

public class MinionSpawnerController : MonoBehaviour
{
    [SerializeField]
    private GameObject _MinionPrefab = default;

    [SerializeField]
    private Transform _Player = default;

    [SerializeField]
    private Transform _PlayerBase = default;

    [SerializeField]
    private float _SpawnTimer = 5;

    [SerializeField]
    private Transform _SpawnPoint = default;
    
    private float _LastSpawnTime = 0;

    private void Start()
    {
        if (_Player == null)
        {
            Debug.LogError("Player is null. Minions can't attack player.");
        }

        if (_PlayerBase == null)
        {
            Debug.LogError("Player Base is null. Minions can't move to player base.");
        }

        if (_SpawnPoint == null)
        {
            Debug.LogError("SpawnPoint is null. Minions cannot spawn.");
        }
    }

    void Update()
    {
        if (Time.time - _LastSpawnTime > _SpawnTimer && _SpawnPoint != null)
        {
            Vector3 position = _SpawnPoint.position;
            position.z = -1;

            GameObject minion = Instantiate(_MinionPrefab, position, Quaternion.identity);
            MinionController controller = minion.GetComponent<MinionController>();
            controller.PlayerTransform = _Player;
            controller.EnemyBaseTransform = _PlayerBase;

            _LastSpawnTime = Time.time;
        }
    }
}
