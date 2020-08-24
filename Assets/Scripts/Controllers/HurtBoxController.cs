using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBoxController : MonoBehaviour
{
    [SerializeField]
    private Health _Health;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            _Health.DealDamage(1);
        }
    }
}
