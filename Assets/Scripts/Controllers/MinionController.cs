using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    [SerializeField]
    private GameObject _BulletPrefab;

    [SerializeField]
    private float _MoveSpeed = 3;

    private Rigidbody2D _Rigidbody;
    private Animator _Animator;

    private Queue<Vector3> _PlannedLocations = null;

    private Transform _EnemyBaseTransform;
    public Transform EnemyBaseTransform
    {
        get => _EnemyBaseTransform;
        set
        {
            _EnemyBaseTransform = value;
            InvalidatePath();
        }
    }

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        if (_PlannedLocations != null && _PlannedLocations.Count > 0)
        {
            Vector3 nextLocation = _PlannedLocations.Peek();
            nextLocation.z = transform.position.z;

            _Rigidbody.velocity = (nextLocation - transform.position).normalized * _MoveSpeed;
            
            if ((transform.position - nextLocation).magnitude < .05f)
            {
                transform.position = nextLocation;
                _PlannedLocations.Dequeue();
            }
        }

        _Animator.SetFloat("Speed", _Rigidbody.velocity.magnitude);
    }

    private void Update()
    {
        // TODO: Make this line of sight
        // Stop moving towards base if within range
        if ((transform.position - _EnemyBaseTransform.position).magnitude < 3)
        {
            _PlannedLocations.Clear();
        }
    }

    void InvalidatePath()
    {
        if (_EnemyBaseTransform != null)
        {
            _PlannedLocations = PathMaker.Instance.GetWorldPath(transform.position, _EnemyBaseTransform.position);
        }
    }
}
