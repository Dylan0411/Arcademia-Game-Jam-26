using UnityEngine;
using UnityEngine.UI;

public class rhythmGame1 : MonoBehaviour
{


    public GameObject redNote;
    public GameObject greenNote;
    public GameObject blueNote;



    public float P2score;

    public bool redInside;
    public bool blueInside;
    public bool greenInside;

    public Slider scoreSlider;

    public float maxScore = 300; //<<<<<<<<<<<<<<<ADJUST

    public GameObject greenScoreFlash;
    public GameObject redScoreFlash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    }

    // Update is called once per frame
    void Update()
    {


        scoreSlider.value = P2score / maxScore;


        if (Input.GetKeyDown(KeyCode.I))
        {
            redNote.SetActive(true);
            if(redInside)
            {
                Debug.Log("SCORE!!!");
                P2score++;
                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);

            }
            else
            {
                Debug.Log("miss!");
                P2score--;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);


            }
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            redNote.SetActive(false);
            //
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }


        if (Input.GetKeyDown(KeyCode.O))
        {
            greenNote.SetActive(true);
            if (greenInside)
            {
                Debug.Log("SCORE!!!");
                P2score++;
                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P2score--;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            greenNote.SetActive(false);
            //
            greenScoreFlash.SetActive(false);
            redScoreFlash.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            blueNote.SetActive(true);
            if (blueInside)
            {
                Debug.Log("SCORE!!!");
                P2score++;
                redScoreFlash.SetActive(false);
                greenScoreFlash.SetActive(true);
            }
            else
            {
                Debug.Log("miss!");
                P2score--;
                greenScoreFlash.SetActive(false);
                redScoreFlash.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.P))
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
