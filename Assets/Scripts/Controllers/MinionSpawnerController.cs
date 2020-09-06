using System.Collections.Generic;
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
    private float _SpawnTimerSeconds = 5;

    [SerializeField]
    private Transform _SpawnPoint = default;

    [SerializeField]
    private int _MaxNumMinions = 4;
    
    private float _LastSpawnTime = 0;
    private List<MinionController> _Minions = new List<MinionController>();

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
        if (Time.time - _LastSpawnTime > _SpawnTimerSeconds && _SpawnPoint != null)
        {
            // Using for loop because deletions will occur within loop
            for (int i = 0; i < _Minions.Count; ++i)
            {
                MinionController exitingMinion = _Minions[i];

                // Check if it's destroyed
                if (exitingMinion == null)
                {
                    _Minions.RemoveAt(i);
                    --i;
                }
            }

            // If we haven't reached the max, respawn
            if (_Minions.Count < _MaxNumMinions)
            {
                Vector3 position = _SpawnPoint.position;
                position.z = -1;

                GameObject minion = Instantiate(_MinionPrefab, position, Quaternion.identity);
                MinionController controller = minion.GetComponent<MinionController>();
                controller.PlayerTransform = _Player;
                controller.EnemyBaseTransform = _PlayerBase;
                _Minions.Add(controller);
            }

            _LastSpawnTime = Time.time;
        }
    }
}
