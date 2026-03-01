using UnityEngine;

public class EXIT : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if ESC or Q is pressed
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q))
        {
           Application.Quit();
        }
    }

}