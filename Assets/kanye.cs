using UnityEngine;

public class kanye : MonoBehaviour
{

    public GameObject fishGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("BeginFishGame", 20f);
        fishGame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BeginFishGame()
    {
        fishGame.SetActive(true);
    }

}
