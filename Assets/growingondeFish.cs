using UnityEngine;

public class BouncingGrowingFish : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float forceStrength = 15f;
    public float forceInterval = 1f;

    [Header("Growth Settings")]
    public Vector3 startScale = Vector3.one;
    public Vector3 endScale = Vector3.one * 2f;
    public float growDuration = 5f;

    private Rigidbody rb;
    private float forceTimer;
    private float growTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.localScale = startScale;
    }

    void Update()
    {
        HandleBounce();
        HandleGrowth();
    }

    void HandleBounce()
    {
        forceTimer += Time.deltaTime;

        if (forceTimer >= forceInterval)
        {
            forceTimer = 0f;

            Vector3 randomDirection = Random.onUnitSphere;
            rb.AddForce(randomDirection * forceStrength, ForceMode.Impulse);
        }
    }

    void HandleGrowth()
    {
        if (growTimer < growDuration)
        {
            growTimer += Time.deltaTime;
            float t = Mathf.Clamp01(growTimer / growDuration);
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
        }
    }
}