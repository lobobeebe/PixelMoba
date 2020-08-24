using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _MoveForce = 300f;

    [SerializeField]
    private GameObject _BulletPrefab = default;

    [SerializeField]
    private float _SecondsPerBullets = .25f;

    private float _LastShotTimeSeconds;

    // Components
    private Rigidbody2D _RigidBody;
    private Animator _Animator;
    private Health _Health;

    void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _Health = GetComponent<Health>();

        if (_BulletPrefab == null)
        {
            Debug.LogError("No Bullet Prefab");
        }
    }
    
    void FixedUpdate()
    {   
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _RigidBody.AddForce(new Vector2(horizontalInput * _MoveForce, verticalInput * _MoveForce)); 

        // Pass in the current velocity of the RigidBody2D
        // The speed parameter of the Animator now knows
        // how fast the player is moving and responds accordingly
        _Animator.SetFloat("Speed", _RigidBody.velocity.magnitude);
    }

    private void Update()
    {

        // Fire if primary mouse is pressed
        if (Input.GetMouseButton(0) && Time.time >= _LastShotTimeSeconds + _SecondsPerBullets)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x);
            if (_BulletPrefab != null)
            {
                Instantiate(_BulletPrefab, transform.position, Quaternion.Euler(0, 0, angle * 180 / Mathf.PI));
            }
            else
            {
                Debug.LogError("No Bullet Prefab");
            }

            _LastShotTimeSeconds = Time.time;
        }
    }
}