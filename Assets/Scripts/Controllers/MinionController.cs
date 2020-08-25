using System.Collections.Generic;
using UnityEngine;

public class MinionController : MonoBehaviour
{
    [SerializeField]
    private float _MoveSpeed = 3;

    // Components
    private Rigidbody2D _Rigidbody;
    private Animator _Animator;
    private GunController _GunController;

    private Queue<Vector3> _PlannedLocations = null;

    // Player Transform - Used for firing at the player for now
    public Transform PlayerTransform { get; set; }

    // Enemy Base Transform
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
        _GunController = GetComponent<GunController>();
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
        if (_EnemyBaseTransform != null && (transform.position - _EnemyBaseTransform.position).magnitude < 3)
        {
            _PlannedLocations.Clear();
            _GunController.Target = _EnemyBaseTransform.position;
            _GunController.IsFiring = true;
        }
        else if (PlayerTransform != null && (transform.position - PlayerTransform.position).magnitude < 3)
        {
            _GunController.Target = PlayerTransform.position;
            _GunController.IsFiring = true;
        }
        else
        {
            _GunController.IsFiring = false;
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
