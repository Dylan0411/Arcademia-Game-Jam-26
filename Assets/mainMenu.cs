using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public GameObject Menu;
    public GameObject startButton;
    public GameObject fishingButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //start paused
        Time.timeScale = 0f;

        //menu visible
        Menu.SetActive(true);

        //fish menu game button gone
        fishingButton.SetActive(false);

        //play button visible
        startButton.SetActive(true);
    }




    public void startGame()
    {
        //play time
        Time.timeScale = 1f;
        //hide menu
        Menu.SetActive(false);
    }

    public void startFishing()
    {
        //start time up again
        Time.timeScale = 1f;
        SceneManager.LoadScene("fishing mini-game");
    }



    //infrank pandora script:
    //invoke main menui 5ish seconds after box is open
        //pauses time
        //shows menu canvas

}
