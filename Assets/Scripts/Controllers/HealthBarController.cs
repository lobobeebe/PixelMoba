using UnityEngine;

public class HealthBarController : MonoBehaviour
{
    [SerializeField]
    private Health _HealthComponent = default;

    [SerializeField]
    private GameObject _Foreground = default;

    void Start()
    {
        if (_HealthComponent == null)
        {
            Debug.LogError("HealthComponent is null. Cannot control Health Bar.");
            return;
        }

        if (_Foreground == null)
        {
            Debug.LogError("Foreground is null. Cannot change Health Bar size.");
            return;
        }

        _HealthComponent.OnHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(float newHealth)
    {
        if (_HealthComponent == null)
        {
            // Error logged in Start
            return;
        }

        Vector3 healthScale = _Foreground.transform.localScale;
        healthScale.x = newHealth / _HealthComponent.MaxHealth;
        _Foreground.transform.localScale = healthScale;
    }
}