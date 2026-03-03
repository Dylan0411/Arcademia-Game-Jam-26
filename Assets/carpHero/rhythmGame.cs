using UnityEngine;
using UnityEngine.UI;

public class rhythmGame : MonoBehaviour
{
    public GameObject redNote;
    public GameObject greenNote;
    public GameObject blueNote;

    public static float P1score;

    public bool redInside;
    public bool blueInside;
    public bool greenInside;

    public Slider scoreSlider;

    public float maxScore = 291; //<<<<<<<<<<<<<<<ADJUST

    public GameObject greenScoreFlash;
    public GameObject redScoreFlash;

    public Text p1ScoreText;

    public AudioSource babyShark;
    public AudioSource badNote;

    // Track if the CURRENT note in the lane was hit
    private bool redHit;
    private bool greenHit;
    private bool blueHit;

    void Start()
    {
        redNote.SetActive(false);
        greenNote.SetActive(false);
        blueNote.SetActive(false);

        greenScoreFlash.SetActive(false);
        redScoreFlash.SetActive(false);

        redInside = false;
        blueInside = false;
        greenInside = false;

        P1score = maxScore / 4;

        redHit = false;
        greenHit = false;
        blueHit = false;
    }

    void Update()
    {
        p1ScoreText.text = P1score.ToString();
        scoreSlider.value = P1score / maxScore;

        // RED (A)
        if (Input.GetKeyDown(KeyCode.A))
        {
            redNote.SetActive(true);

            if (redInside)
            {
                Debug.Log("SCORE!!!");
                P1score++;
                redHit = true;

                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P1score--;
                redHit = true; // mark as attempted so exit doesn’t double-penalize

                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            redNote.SetActive(false);
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }

        // GREEN (S)
        if (Input.GetKeyDown(KeyCode.S))
        {
            greenNote.SetActive(true);

            if (greenInside)
            {
                Debug.Log("SCORE!!!");
                P1score++;
                greenHit = true;

                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P1score--;
                greenHit = true;

                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            greenNote.SetActive(false);
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }

        // BLUE (D)
        if (Input.GetKeyDown(KeyCode.D))
        {
            blueNote.SetActive(true);

            if (blueInside)
            {
                Debug.Log("SCORE!!!");
                P1score++;
                blueHit = true;

                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P1score--;
                blueHit = true;

                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            blueNote.SetActive(false);
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("redNote"))
        {
            redInside = true;
            redHit = false; // Reset hit state for THIS note
        }

        if (other.CompareTag("greenNote"))
        {
            greenInside = true;
            greenHit = false;
        }

        if (other.CompareTag("blueNote"))
        {
            blueInside = true;
            blueHit = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("redNote"))
        {
            redInside = false;

            if (!redHit)
            {
                Debug.Log("Missed without attempt!");
                P1score--;

                redScoreFlash.SetActive(true);
                badNote.Play();
                Invoke("HideRedFlash", 0.5f);
            }
        }

        if (other.CompareTag("greenNote"))
        {
            greenInside = false;

            if (!greenHit)
            {
                Debug.Log("Missed without attempt!");
                P1score--;

                redScoreFlash.SetActive(true);
                badNote.Play();
                Invoke("HideRedFlash", 0.5f);
            }
        }

        if (other.CompareTag("blueNote"))
        {
            blueInside = false;

            if (!blueHit)
            {
                Debug.Log("Missed without attempt!");
                P1score--;

                redScoreFlash.SetActive(true);
                badNote.Play();
                Invoke("HideRedFlash", 0.5f);
            }
        }
    }

    void HideRedFlash()
    {
        redScoreFlash.SetActive(false);
    }
}