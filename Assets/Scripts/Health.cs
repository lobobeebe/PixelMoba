using UnityEngine;

public class Health : MonoBehaviour
{
    public float MaxHealth = 5f;

    public delegate void HealthChanged(float newHealth);
    public HealthChanged OnHealthChanged;

    public delegate void Death();
    public Death OnDeath;

    private float _CurrentHealth;
    public float CurrentHealth
    {
        get
        {
            return _CurrentHealth;
        }
        private set
        {
            // Don't set the same value
            if (value == CurrentHealth)
            {
                return;
            }

            if (value < 0)
            {
                _CurrentHealth = 0;
            }
            else if (value > MaxHealth)
            {
                _CurrentHealth = MaxHealth;
            }
            else
            {
                _CurrentHealth = value;
            }

            if (_CurrentHealth == 0)
            {
                OnDeath?.Invoke();
            }
            else
            {
                OnHealthChanged?.Invoke(_CurrentHealth);
            }
        }
    }

    public bool IsDead
    {
        get
        {
            return CurrentHealth <= 0;
        }
    }

    public void DealDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        // Just faking damage for testing so you can see how I do it
        // Your damage will be coming from somewhere else
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsDead)
            {
                DealDamage(Random.Range(.1f, .5f));
            }
        }
    }
}
