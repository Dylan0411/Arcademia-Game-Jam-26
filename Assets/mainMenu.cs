using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject startButton;
    public GameObject fishingButton;

    private int counter = 0; // track how many times Enter/Backspace was pressed

    void Start()
    {
        // start paused
        Time.timeScale = 0f;

        // menu visible
        Menu.SetActive(true);

        // fishing button hidden initially
        fishingButton.SetActive(false);

        // play/start button visible
        startButton.SetActive(true);
    }

    void Update()
    {
        // Check if Enter or Backspace key is pressed
        if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (counter < 1)
            {
                startGame();
                counter++;
                // Optionally make the fishing button visible after first press
                fishingButton.SetActive(true);
            }
            else
            {
                startFishing();
            }
        }
    }

    public void startGame()
    {
        // resume time
        Time.timeScale = 1f;

        // hide menu
        Menu.SetActive(false);

        Debug.Log("Game Started!");
    }

    public void startFishing()
    {
        // resume time
        Time.timeScale = 1f;

        // load fishing mini-game scene
        SceneManager.LoadScene("fishing mini-game");

        Debug.Log("Fishing Mini-game Started!");
    }
}