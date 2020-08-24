using UnityEngine;

public class MinionController : MonoBehaviour
{
    [SerializeField]
    private float _MoveForce = 200;

    private Rigidbody2D _Rigidbody;
    private Animator _Animator;

    public Transform PlayerTransform
    {
        get;
        set;
    }

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        if (PlayerTransform != null)
        {
            _Rigidbody.AddForce((PlayerTransform.position - transform.position).normalized * _MoveForce);
            _Animator.SetFloat("Speed", _Rigidbody.velocity.magnitude);
        }
    }
}
