using UnityEngine;

public class noteSpawner1 : MonoBehaviour
{

    public GameObject redNoteOBJ;
    public GameObject greenNoteOBJ;
    public GameObject blueNoteOBJ;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(redNoteOBJ);
        Invoke("test", 2f);
        Invoke("test1", 5f);
    }

    // Update is called once per frame
    void Update()
    {


    }



    public void test()
    {
        Instantiate(greenNoteOBJ);
    }

    public void test1()
    {
        Instantiate(blueNoteOBJ);
    }

}
