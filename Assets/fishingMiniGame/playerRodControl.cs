using System.Reflection;
using UnityEngine;

public class playerRodControl : MonoBehaviour
{

    public GameObject defaultRodRotation;
    public GameObject reelFishRodRotation;
    public GameObject fishingRod;
    public GameObject fishPullRodRotation;
    public float speed;

    bool runTimer;


    public float timerLength;
    public float elapsedTime;
    public float savedElapsedTime;

    public bool fishBite;

    public int fishReelCounter;

    public bool fishCaught;

    public bool rodPulledBack;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 2;
        fishingRod.transform.rotation = defaultRodRotation.transform.rotation;
        fishingRod.transform.position = defaultRodRotation.transform.position;


        elapsedTime = 0f;

        runTimer = true;

        fishBite = false;

        timerLength = Random.Range(2f, 5f);

        fishReelCounter = 0;

        fishCaught = false;

        rodPulledBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (fishCaught == false)
        {
            if (Input.GetKey(KeyCode.S) && fishBite == true)
            {
                rodPulledBack = true;
                runTimer = false;
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, reelFishRodRotation.transform.rotation, speed * Time.deltaTime);
            }
            else
            {
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, defaultRodRotation.transform.rotation, speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.N) && fishBite == true && rodPulledBack == true)
            {
                if (fishReelCounter >= 20)
                {
                    fishCaught = true;
                    Debug.Log("fish caught!");
                }
                else
                {
                    fishReelCounter++;
                }
            }
        }
        if (fishCaught == true)
        {
            //move fish to buckets loc and rot

            //move rod back to default loc and rot
            //reset values here
        }



        if (runTimer == true) // SET TRUE WHEN FISH IS IN BUCKET OR FISH LOST
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timerLength)
            {
                fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, fishPullRodRotation.transform.rotation, speed * 50 * Time.deltaTime);

                fishBite = true;
                Debug.Log("fish bite!");


                savedElapsedTime = elapsedTime;
                if (elapsedTime > savedElapsedTime+5) //if player not reeled fish 5 seconds after bite
                {
                    fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, defaultRodRotation.transform.rotation, speed * 50 * Time.deltaTime);
                    elapsedTime = 0f;
                    fishBite = false;
                    timerLength = Random.Range(2f, 5f);
                    Debug.Log("fish lost!");

                }
            }
        }







    }
}




// randomly bwteeen 2-5 seconds the rod gets pulled down.. player pulls back on rod and holds a to reel in fish
// when rod is fully reeled.. player uses right stick to move fish to bucket.
// when fish in bucket.. move rod back to default position.


//WHEN FISH IN BUCKET (RESET):
/*
                    fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, defaultRodRotation.transform.rotation, speed * 50 * Time.deltaTime);
                    elapsedTime = 0f;
                    fishBite = false;
                    timerLength = Random.Range(2f, 5f);
                    runTime = true;
                    fishCaught = false;
 */


//animation to move fish to bucket w/ splash effect triggered
//delete 2nd script
//on screen effect to visibly show when fish bites (begin to pull back and reel in - bar fills up the more they reel?) + when fish is lost (wait for another to bite), when fish is caught (when x are caught then turn the next fish caught is the relic)
//if player takes long to reel in fish then the fish is lost

//add bouncy fish and slippery fish

//DUPLICATE FOR PLAYER 2 + Apply arcade control scheme