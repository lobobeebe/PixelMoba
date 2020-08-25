using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float DeleteAfterSeconds = 5;

    private float _TimeCreatedSeconds;

    // Start is called before the first frame update
    void Start()
    {
        _TimeCreatedSeconds = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _TimeCreatedSeconds + DeleteAfterSeconds)
        {
            Destroy(gameObject);
        }
    }
}
