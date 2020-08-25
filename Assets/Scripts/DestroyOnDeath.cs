using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class DestroyOnDeath : MonoBehaviour
{
    private Health _Health;

    // Start is called before the first frame update
    void Start()
    {
        _Health = GetComponent<Health>();

        _Health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
