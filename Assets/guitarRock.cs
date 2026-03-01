using UnityEngine;

public class GuitarFishRotate : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float minZAngle = 0f;
    public float maxZAngle = 180f;
    public float rotationSpeed = 1.5f;

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        float t = (Mathf.Sin(Time.time * rotationSpeed) + 1f) * 0.5f;
        float zAngle = Mathf.Lerp(minZAngle, maxZAngle, t);

        transform.localRotation = startRotation * Quaternion.Euler(0f, 0f, zAngle);
    }
}