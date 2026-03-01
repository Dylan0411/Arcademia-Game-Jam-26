using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject startButton;
    public GameObject fishingButton;

    [Header("Menu Music")]
    public AudioSource menuMusic;
    public float defaultMusicVolume = 1f;

    private static int counter = 0;

    void Start()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        Time.timeScale = 0f;

        // Mute level SFX while in menu
        AudioListener.volume = 0f;

        Menu.SetActive(true);
        fishingButton.SetActive(counter >= 1);
        startButton.SetActive(true);

        // 🎵 Instantly START the music
        if (menuMusic != null)
        {
            menuMusic.ignoreListenerVolume = true; // Make sure it bypasses the AudioListener mute
            menuMusic.volume = defaultMusicVolume;

            if (!menuMusic.isPlaying)
            {
                menuMusic.Play();
            }
        }
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.enterKey.wasPressedThisFrame || Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            if (counter < 1)
            {
                startGame();
            }
            else
            {
                startFishing();
            }
        }
    }

    public void startGame()
    {
        if (counter < 1) counter++;

        Time.timeScale = 1f;
        AudioListener.volume = 1f; // Unmute level SFX instantly
        Menu.SetActive(false);

        // 🛑 Instantly KILL the music
        if (menuMusic != null)
        {
            menuMusic.Stop();
        }
    }

    public void startFishing()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;

        // 🛑 Instantly KILL the music
        if (menuMusic != null)
        {
            menuMusic.Stop();
        }

        // 🚀 Load the scene INSTANTLY (No more 2-second delay!)
        SceneManager.LoadScene("fishing mini-game");
    }
}