using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cam; // Your actual camera

    [Header("Path Points (In Order)")]
    public GameObject[] destinations; // Drag as many GameObjects as you want here

    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float reachThreshold = 0.05f;

    private int currentIndex = 0;

    void Start()
    {
        if (destinations.Length == 0)
        {
            return;
        }

        // Snap to first point
        cam.transform.position = destinations[0].transform.position;
        cam.transform.rotation = destinations[0].transform.rotation;

        currentIndex = 1; // Start moving to the next point
    }

    void Update()
    {
        if (destinations.Length == 0)
        {
            return;
        }

        Transform target = destinations[currentIndex].transform;

        // Move
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, target.position, moveSpeed * Time.deltaTime);

        // Rotate
        cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, target.rotation, rotationSpeed * Time.deltaTime);

        // If close enough, go to next point
        if (Vector3.Distance(cam.transform.position, target.position) < reachThreshold)
        {
            currentIndex++;

            //  Loop back to start
            if (currentIndex >= destinations.Length)
            {
                currentIndex = 0;
            }
        }
    }
}