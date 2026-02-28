using UnityEngine;

public class soundsforcover : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        GetComponent<AudioSource>().Play(); // Plays a sound on impact
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
