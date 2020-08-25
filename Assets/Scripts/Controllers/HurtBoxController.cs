using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBoxController : MonoBehaviour
{
    [SerializeField]
    private Health _Health = default;

    public void DealDamage(float damage)
    {
        _Health.DealDamage(damage);
    }
}
