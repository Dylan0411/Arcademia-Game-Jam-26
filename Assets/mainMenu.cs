using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public GameObject Menu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //start paused
        //fish minu game button gone
        //play button visible
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void startGame()
    {
        //play time
        //hide menu
    }

    void startFishing()
    {
        SceneManager.LoadScene("fishing mini-game");
    }



    //infrank pandora script:
    //invoke main menui 5ish seconds after box is open
        //pauses time
        //shows menu canvas

}
