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

    public float maxScore = 300; //<<<<<<<<<<<<<<<ADJUST

    public GameObject greenScoreFlash;
    public GameObject redScoreFlash;

    public Text p1ScoreText;

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

    }

    // Update is called once per frame
    void Update()
    {

        p1ScoreText.text = P1score.ToString();
        scoreSlider.value = P1score / maxScore;


        if (Input.GetKeyDown(KeyCode.T))
        {
            redNote.SetActive(true);
            if(redInside)
            {
                Debug.Log("SCORE!!!");
                P1score++;
                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);

            }
            else
            {
                Debug.Log("miss!");
                P1score--;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);


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
            if (greenInside)
            {
                Debug.Log("SCORE!!!");
                P1score++;
                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P1score--;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
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
            if (blueInside)
            {
                Debug.Log("SCORE!!!");
                P1score++;
                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P1score--;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
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



    //if object with tag: "redNote", is inside box collider then makew redInside bool true, else make it false
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
        }
        if (other.CompareTag("greenNote"))
        {
            greenInside = false;
        }
        if (other.CompareTag("blueNote"))
        {
            blueInside = false;
        }
    }


}
