using UnityEngine;

public class PlayerPhysicsMovement : MonoBehaviour
{
    public float forceAmount = 10f;
    private Rigidbody rb;
    private bool moveForward;
    private bool moveBackward;

    void Start()
    {
        // Get the Rigidbody component attached to this object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for key presses every frame
        moveForward = Input.GetKey(KeyCode.W);
        moveBackward = Input.GetKey(KeyCode.S);
    }

    void FixedUpdate()
    {
        // Apply physics forces at a constant rate
        if (moveForward)
        {
            rb.AddForce(transform.forward * forceAmount);
        }

        if (moveBackward)
        {
            rb.AddForce(-transform.forward * forceAmount);
        }
    }
}
