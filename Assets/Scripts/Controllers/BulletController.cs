
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float Speed = 10;

    // Components
    private Rigidbody2D _Rigidbody;

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();

        float angleRads = transform.eulerAngles.z * Mathf.PI / 180;
        _Rigidbody.velocity = new Vector2(Mathf.Cos(angleRads) * Speed, Mathf.Sin(angleRads) * Speed);
    }
}
