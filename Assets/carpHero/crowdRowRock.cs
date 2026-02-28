using UnityEngine;

public class crowdRowRock : MonoBehaviour
{
    public GameObject row;
    public GameObject rowUpPos;
    public GameObject rowDownPos;

    public float rotationSpeed = 5f;
    private bool movingUp = true; // Track direction

    void Update()
    {
        if (movingUp)
        {
            row.transform.rotation = Quaternion.Lerp(row.transform.rotation, rowUpPos.transform.rotation, rotationSpeed * Time.deltaTime);

            // If close enough to target, switch direction
            if (Quaternion.Angle(row.transform.rotation, rowUpPos.transform.rotation) < 0.5f)
            {
                movingUp = false;
            }
        }
        else
        {
            row.transform.rotation = Quaternion.Lerp(row.transform.rotation, rowDownPos.transform.rotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(row.transform.rotation, rowDownPos.transform.rotation) < 0.5f)
            {
                movingUp = true;
            }
        }
    }
}