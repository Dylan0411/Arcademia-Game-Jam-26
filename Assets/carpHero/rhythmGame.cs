using UnityEngine;

public class rhythmGame : MonoBehaviour
{


    public GameObject redNote;
    public GameObject greenNote;
    public GameObject blueNote;

    public float currentTime;

    public float redNoteActivationTime;
    public float greenNoteActivationTime;
    public float blueNoteActivationTime;

    public bool redNoteValid;
    public bool greenNoteValid;
    public bool blueNoteValid;

    public float validNoteLength = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        redNote.SetActive(false);
        greenNote.SetActive(false);
        blueNote.SetActive(false);

        redNoteActivationTime = 0f;
        greenNoteActivationTime = 0f;
        blueNoteActivationTime = 0f;

        redNoteValid = false;
        greenNoteValid = false;
        blueNoteValid = false;
    }

    // Update is called once per frame
    void Update()
    {

        currentTime += Time.deltaTime;

        if (currentTime < redNoteActivationTime + validNoteLength)
        {
            redNoteValid = true;
        }
        else
        {
            greenNoteValid = false;
        }

        if (currentTime < greenNoteActivationTime + validNoteLength)
        {
            greenNoteValid = true;
        }
        else
        {
            redNoteValid = false;
        }

        if (currentTime < blueNoteActivationTime + validNoteLength)
        {
            blueNoteValid = true;
        }
        else
        {
            blueNoteValid = false;
        }




        if (Input.GetKeyDown(KeyCode.T))
        {
            redNote.SetActive(true);
            redNoteActivationTime = currentTime;
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            redNote.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            greenNote.SetActive(true);
            greenNoteActivationTime = currentTime;
        }
        if (Input.GetKeyUp(KeyCode.Y))
        {
            greenNote.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            blueNote.SetActive(true);
            blueNoteActivationTime = currentTime;
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            blueNote.SetActive(false);
        }
    }
}
