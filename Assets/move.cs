using UnityEngine;

public class PlayerPhysicsMovement : MonoBehaviour
{
    public float forceAmount = 10f;
    private Rigidbody rb;
    private bool moveForward;
    private bool moveBackward;
    private bool moveForward1;
    private bool moveBackward1;

    void Start()
    {
        // Get the Rigidbody component attached to this object
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check for key presses every frame
        moveForward = Input.GetKey(KeyCode.S);
        moveBackward = Input.GetKey(KeyCode.W);
        moveForward1 = Input.GetKey(KeyCode.DownArrow);
        moveBackward1 = Input.GetKey(KeyCode.UpArrow);
    }

    void FixedUpdate()
    {
        // Apply physics forces at a constant rate
        if (moveForward || moveForward1)
        {
            rb.AddForce(transform.forward * forceAmount);
        }

        if (moveBackward || moveBackward1)
        {
            rb.AddForce(-transform.forward * forceAmount);
        }
    }
}
