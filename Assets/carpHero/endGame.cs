using UnityEngine;
using UnityEngine.UI;

public class endGame : MonoBehaviour
{

    public GameObject TieText;
    public GameObject p1WinText;
    public GameObject p2WinText;

    public float gameLength = 20f; //ADJUST VALUE <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

    public bool fadeToBlack = false;   // CHANGED: start by fading to clear

    // --- FADE VARIABLES ADDED ---
    public Image blackScreen;        // Assign full screen black UI Image
    public float fadeSpeed = 1.5f;
    private float alpha = 1f;        // CHANGED: start fully black
    // --------------------------------

    void Start()
    {
        TieText.SetActive(false);
        p1WinText.SetActive(false);
        p2WinText.SetActive(false);

        // CHANGED: force screen fully black at start
        alpha = 1f;
        blackScreen.color = new Color(0f, 0f, 0f, alpha);
        Invoke("unfadeInitial", 3f);


        Invoke("gameEnd", gameLength);
        Invoke("quitGame", gameLength + 10f);
    }

    void Update()
    {
        if (fadeToBlack == true)
        {
            if (alpha < 1f)
            {
                alpha += fadeSpeed * Time.deltaTime;
                blackScreen.color = new Color(0f, 0f, 0f, alpha);
            }

        }
        else
        {
            if (alpha > 0f)
            {
                alpha -= fadeSpeed * Time.deltaTime;
                blackScreen.color = new Color(0f, 0f, 0f, alpha);
            }
        }

    }

    void gameEnd()
    {
        if (rhythmGame1.P2score > rhythmGame.P1score)
        {
            p2WinText.SetActive(true);
        }
        else if (rhythmGame1.P2score < rhythmGame.P1score)
        {
            p1WinText.SetActive(true);
        }
        else
        {
            TieText.SetActive(true);
        }

        fadeToBlack = true;
    }

    void unfadeInitial()
    {
        fadeToBlack = false;
    }

    void quitGame()
    {
        Application.Quit();
    }

}