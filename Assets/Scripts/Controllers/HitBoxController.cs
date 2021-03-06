﻿using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitBoxController : MonoBehaviour
{
    [SerializeField]
    private List<string> _HitTags = default;
    public List<string> HitTags { get => _HitTags; set => _HitTags = value; }

    [SerializeField]
    private float _Damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.isTrigger && HitTags.Contains(collision.tag))
        {
            HurtBoxController hurtBox = collision.GetComponent<HurtBoxController>();

            if (hurtBox != null)
            {
                hurtBox.DealDamage(_Damage);
            }
        }
    }
}
