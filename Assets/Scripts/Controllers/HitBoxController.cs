using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBoxController : MonoBehaviour
{
    [SerializeField]
    private List<string> _HitTags = default;

    [SerializeField]
    private float _Damage = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (_HitTags != null && _HitTags.Contains(col.gameObject.tag))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
