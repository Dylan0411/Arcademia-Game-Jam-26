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

    // NEW: track if note was hit
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

        P2score = 0;

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


        if (Input.GetKeyDown(KeyCode.F))
        {
            redNote.SetActive(true);
            redHit = false;

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
                P2score--;
                redHit = true;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            redNote.SetActive(false);
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            greenNote.SetActive(true);
            greenHit = false;

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
                P2score--;
                greenHit = true;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.G))
        {
            greenNote.SetActive(false);
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            blueNote.SetActive(true);
            blueHit = false;

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
                P2score--;
                blueHit = true;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                badNote.Play();
            }
        }

        if (Input.GetKeyUp(KeyCode.H))
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
        }
        if (other.CompareTag("greenNote"))
        {
            greenInside = true;
        }
        if (other.CompareTag("blueNote"))
        {
            blueInside = true;
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
                P2score--;
                redScoreFlash.SetActive(true);
                Invoke("HideRedFlash", 0.5f);
            }
        }
        if (other.CompareTag("greenNote"))
        {
            greenInside = false;
            if (!greenHit)
            {
                Debug.Log("Missed without attempt!");
                P2score--;
                redScoreFlash.SetActive(true);
                Invoke("HideRedFlash", 0.5f);
            }
        }
        if (other.CompareTag("blueNote"))
        {
            blueInside = false;
            if (!blueHit)
            {
                Debug.Log("Missed without attempt!");
                P2score--;
                redScoreFlash.SetActive(true);
                Invoke("HideRedFlash", 0.5f);
            }
        }
    }

    void HideRedFlash()
    {
        redScoreFlash.SetActive(false);
    }
}