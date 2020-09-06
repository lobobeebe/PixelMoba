using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _MoveForce = 300f;

    // Components
    private Rigidbody2D _RigidBody;
    private Animator _Animator;
    private Health _Health;
    private GunController _GunController;

    void Start()
    {
        _RigidBody = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _Health = GetComponent<Health>();
        _GunController = GetComponent<GunController>();
    }
    
    void FixedUpdate()
    {   
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        _RigidBody.AddForce(new Vector2(horizontalInput * _MoveForce, verticalInput * _MoveForce)); 

        // Pass in the current velocity of the RigidBody2D
        // The speed parameter of the Animator now knows
        // how fast the player is moving and responds accordingly
        _Animator.SetFloat("XSpeed", _RigidBody.velocity.x);
        _Animator.SetFloat("YSpeed", _RigidBody.velocity.y);
        _Animator.SetFloat("Speed", _RigidBody.velocity.magnitude);
    }

    private void Update()
    {
        // Fire if primary mouse is pressed
        if (Input.GetMouseButton(0))
        {
            _GunController.Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _GunController.IsFiring = true;
        }
        else
        {
            _GunController.IsFiring = false;
        }
    }
}