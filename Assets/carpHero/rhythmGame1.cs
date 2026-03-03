using UnityEngine;
using UnityEngine.UI;

public class rhythmGame1 : MonoBehaviour
{
    public GameObject redNote;
    public GameObject greenNote;
    public GameObject blueNote;

    public static float P2score;

    public bool redInside;
    public bool blueInside;
    public bool greenInside;

    public Slider scoreSlider;

    public float maxScore = 291; //<<<<<<<<<<<<<<<ADJUST

    public GameObject greenScoreFlash;
    public GameObject redScoreFlash;

    public Text p2ScoreText;

    public AudioSource babyShark;
    public AudioSource badNote;

    // Track if CURRENT note was hit
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

        P2score = maxScore / 4;

        redHit = false;
        greenHit = false;
        blueHit = false;
    }

    void Update()
    {
        p2ScoreText.text = P2score.ToString();
        scoreSlider.value = P2score / maxScore;

        // RED (J)
        if (Input.GetKeyDown(KeyCode.J))
        {
            redNote.SetActive(true);

            if (redInside)
            {
                Debug.Log("SCORE!!!");
                P2score++;
                redHit = true;

                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P2score = Mathf.Max(P2score - 1, 0); // clamp at 0
                redHit = true; // prevent double penalty

                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            redNote.SetActive(false);
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }

        // GREEN (K)
        if (Input.GetKeyDown(KeyCode.K))
        {
            greenNote.SetActive(true);

            if (greenInside)
            {
                Debug.Log("SCORE!!!");
                P2score++;
                greenHit = true;

                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P2score = Mathf.Max(P2score - 1, 0);
                greenHit = true;

                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            greenNote.SetActive(false);
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }

        // BLUE (L)
        if (Input.GetKeyDown(KeyCode.L))
        {
            blueNote.SetActive(true);

            if (blueInside)
            {
                Debug.Log("SCORE!!!");
                P2score++;
                blueHit = true;

                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P2score = Mathf.Max(P2score - 1, 0);
                blueHit = true;

                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.L))
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
            redHit = false; // reset per note
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
                P2score = Mathf.Max(P2score - 1, 0);

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
                P2score = Mathf.Max(P2score - 1, 0);

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
                P2score = Mathf.Max(P2score - 1, 0);

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