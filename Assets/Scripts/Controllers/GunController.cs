using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField]
    private GameObject _BulletPrefab = default;

    [SerializeField]
    private float _SecondsPerBullets = .25f;

    [SerializeField]
    private List<string> _HitTags = default;

    private float _LastShotTimeSeconds;

    public bool IsFiring { get; set; }
    public Vector3 Target { get; set; }

    private void Start()
    {
        if (_BulletPrefab == null)
        {
            Debug.LogError("No Bullet Prefab");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFiring && Time.time >= _LastShotTimeSeconds + _SecondsPerBullets)
        {
            CreateBullet();
            _LastShotTimeSeconds = Time.time;
        }
    }

    private void CreateBullet()
    {
        float angle = Mathf.Atan2(Target.y - transform.position.y, Target.x - transform.position.x);
        if (_BulletPrefab != null)
        {
            GameObject bullet = Instantiate(_BulletPrefab, transform.position, Quaternion.Euler(0, 0, angle * 180 / Mathf.PI));
            bullet.tag = tag;

            HitBoxController bulletHitBox = bullet.GetComponent<HitBoxController>();
            bulletHitBox.HitTags = _HitTags;
        }
        else
        {
            Debug.LogError("No Bullet Prefab");
        }
    }
}
