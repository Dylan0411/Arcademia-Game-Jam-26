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

    // NEW: track if note was hit
    private bool redHit;
    private bool greenHit;
    private bool blueHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        redNote.SetActive(false);
        greenNote.SetActive(false);
        blueNote.SetActive(false);

        greenScoreFlash.SetActive(false);
        redScoreFlash.SetActive(false);

        P1score = 0;

        redInside = false;
        blueInside = false;
        greenInside = false;

        P1score = maxScore / 4;

        // Initialize hit flags
        redHit = false;
        greenHit = false;
        blueHit = false;
    }

    // Update is called once per frame
    void Update()
    {

        p1ScoreText.text = P1score.ToString();
        scoreSlider.value = P1score / maxScore;


        if (Input.GetKeyDown(KeyCode.T))
        {
            redNote.SetActive(true);
            redHit = false; // reset hit flag when note appears

            if (redInside)
            {
                Debug.Log("SCORE!!!");
                P1score++;
                redHit = true; // mark as hit
                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);

            }
            else
            {
                Debug.Log("miss!");
                P1score--;
                redHit = true; // mark as attempted
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
                babyShark.mute = true;
                badNote.Play();
                Invoke("unMuteBabyShark", 0.75f);
            }
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            redNote.SetActive(false);
            //
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.Y))
        {
            greenNote.SetActive(true);
            greenHit = false; // reset hit flag

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
                babyShark.mute = true;
                badNote.Play();
                Invoke("unMuteBabyShark", 0.75f);
            }
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            greenNote.SetActive(false);
            //
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            blueNote.SetActive(true);
            blueHit = false; // reset hit flag

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
                babyShark.mute = true;
                badNote.Play();
                Invoke("unMuteBabyShark", 0.75f);
            }
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            blueNote.SetActive(false);
            //
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }
    }

    //if object with tag: "redNote", is inside box collider then make redInside bool true, else make it false
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
                P1score--;
                redScoreFlash.SetActive(true);
                Invoke("HideRedFlash", 0.5f);
                babyShark.mute = true;
                Invoke("unMuteBabyShark", 0.75f);
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
                Invoke("HideRedFlash", 0.5f);
                babyShark.mute = true;
                Invoke("unMuteBabyShark", 0.75f);
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
                Invoke("HideRedFlash", 0.5f);
                babyShark.mute = true;
                Invoke("unMuteBabyShark", 0.75f);
            }
        }
    }

    void unMuteBabyShark()
    {
        babyShark.mute = false;
    }

    void HideRedFlash()
    {
        redScoreFlash.SetActive(false);
    }

}