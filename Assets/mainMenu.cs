using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject Menu;
    public GameObject startButton;
    public GameObject fishingButton;

    [Header("Menu Music")]
    public AudioSource menuMusic;
    public float defaultMusicVolume = 1f;

    [Header("Menu Random SFX")]
    public AudioSource menuSFXSource;
    public AudioClip[] randomSounds;

    private Coroutine randomSFXRoutine;
    private static int counter = 0;

    public AudioSource boxHolyMusic;

    void Awake()
    {
        // Ensure holy music starts muted immediately
        if (boxHolyMusic != null)
            boxHolyMusic.mute = true;
    }

    void Start()
    {
        OpenMenu();
    }

    public void OpenMenu()
    {
        Time.timeScale = 0f;

        // Mute level SFX while in menu
        if (boxHolyMusic != null)
            boxHolyMusic.mute = true;

        if (Menu != null)
            Menu.SetActive(true);

        if (fishingButton != null)
            fishingButton.SetActive(counter >= 1);

        if (startButton != null)
            startButton.SetActive(true);

        // Instantly start the menu music
        if (menuMusic != null)
        {
            menuMusic.ignoreListenerVolume = true;
            menuMusic.volume = defaultMusicVolume;

            if (!menuMusic.isPlaying)
                menuMusic.Play();
        }

        // Start random menu SFX loop
        if (randomSFXRoutine == null)
            randomSFXRoutine = StartCoroutine(RandomMenuSounds());
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // Only register input if the menu is active
        if (Menu != null && Menu.activeSelf)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame ||
                Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                if (counter < 1)
                    StartGame();
                else
                    StartFishing();
            }
        }
    }

    public void StartGame()
    {
        if (counter < 1)
            counter++;

        Time.timeScale = 1f;
        AudioListener.volume = 1f;

        if (boxHolyMusic != null)
            boxHolyMusic.mute = false;

        if (Menu != null)
            Menu.SetActive(false);

        if (menuMusic != null)
            menuMusic.Stop();

        if (randomSFXRoutine != null)
        {
            StopCoroutine(randomSFXRoutine);
            randomSFXRoutine = null;
        }
    }

    public void StartFishing()
    {
        Time.timeScale = 1f;
        AudioListener.volume = 1f;

        if (boxHolyMusic != null)
            boxHolyMusic.mute = false;

        if (menuMusic != null)
            menuMusic.Stop();

        if (randomSFXRoutine != null)
        {
            StopCoroutine(randomSFXRoutine);
            randomSFXRoutine = null;
        }

        SceneManager.LoadScene("fishing mini-game");
    }

    private IEnumerator RandomMenuSounds()
    {
        while (true)
        {
            float waitTime = Random.Range(5f, 10f);
            yield return new WaitForSecondsRealtime(waitTime);

            if (Menu != null &&
                Menu.activeSelf &&
                randomSounds != null &&
                randomSounds.Length > 0 &&
                menuSFXSource != null)
            {
                int index = Random.Range(0, randomSounds.Length);
                menuSFXSource.PlayOneShot(randomSounds[index]);
            }
        }
    }
}