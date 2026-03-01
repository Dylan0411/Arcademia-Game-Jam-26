using UnityEngine;

public class SpeakerCone : MonoBehaviour
{
    public Vector3 axis = Vector3.forward; // in/out axis
    public float amplitude = 0.02f;        // VERY small distance
    public float frequency = 6f;           // vibration speed

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        transform.localPosition = startPos + axis.normalized * offset;
    }
}