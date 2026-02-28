using UnityEngine;

public class SimpleTest : MonoBehaviour
{
    // This will trigger regardless of Tags or Logic
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("!!! PHYSICS IS WORKING !!! I was hit by: " + other.name);
    }
}