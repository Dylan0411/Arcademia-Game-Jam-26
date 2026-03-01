using UnityEngine;

public class kanye : MonoBehaviour
{

    public GameObject fishGame;
    public GameObject tutorial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("BeginFishGame", 20f);
        fishGame.SetActive(false);
        tutorial.SetActive(true);
        Invoke("hideTurotial", 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginFishGame()
    {
        fishGame.SetActive(true);
    }
    void hideTurotial()
    {
        tutorial.SetActive(false);
    }
}
