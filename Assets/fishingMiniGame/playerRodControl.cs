using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class playerRodControl : MonoBehaviour
{

    public GameObject defaultRodRotLoc;
    public GameObject reelFishRodRotation;
    public GameObject fishingRod;
    public GameObject fishPullRodRotation;
    public GameObject fishCaughtLocRot;
    public float speed;
    public GameObject fishCaughtPopup;

    bool runTimer;


    public float timerLength;
    public float elapsedTime;
    public float savedElapsedTime;

    public bool fishBite;

    public int fishReelCounter;

    public bool fishCaught;

    public bool rodPulledBack;

    public bool moveRodToBucket;

    public Slider fishingSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 2;
        fishingRod.transform.rotation = defaultRodRotLoc.transform.rotation;
        fishingRod.transform.position = defaultRodRotLoc.transform.position;


        elapsedTime = 0f;

        runTimer = true;

        fishBite = false;

        timerLength = Random.Range(2f, 5f);

        fishReelCounter = 0;

        fishCaught = false;

        rodPulledBack = false;

        moveRodToBucket = false;   
    
        fishCaughtPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        fishingSlider.value = fishReelCounter;



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
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.N) && fishBite == true && rodPulledBack == true)
            {
                fishReelCounter++;
                if (fishReelCounter == 20)//press 20 times to reel fish
                {
                    fishCaught = true;
                    moveRodToBucket = true;
                    Debug.Log("fish caught!");
                    fishCaughtPopup.SetActive(true);

                    //add fish caught total up by 1<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                }

            }
        }
        if (fishCaught == true)
        {
         
            if (moveRodToBucket == true)
            {
                //move fish to buckets loc and rot
                fishingRod.transform.position = Vector3.MoveTowards(fishingRod.transform.position, fishCaughtLocRot.transform.position, speed * Time.deltaTime);
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, fishCaughtLocRot.transform.rotation, speed * 2 * Time.deltaTime);
            }
            else
            {
                //move fish to default loc and rot
                fishingRod.transform.position = Vector3.MoveTowards(fishingRod.transform.position, defaultRodRotLoc.transform.position, speed * Time.deltaTime);
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * 2 * Time.deltaTime);
            }


            Invoke("moveRodBack", 4);
            //splash effect<<<<<<<<<<<<<<<<<<<<<<
            Invoke("defaultRod", 6);
        }



        if (runTimer == true) // SET TRUE WHEN FISH IS IN BUCKET OR FISH LOST
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timerLength)
            {
                fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, fishPullRodRotation.transform.rotation, speed * 50 * Time.deltaTime);

                fishBite = true;
                Debug.Log("fish bite!");


            }
        }







    }

     void moveRodBack()
    {
        moveRodToBucket = false;
    }

    void defaultRod()
    {
        //make this trigger only once
        fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * 50 * Time.deltaTime);
        elapsedTime = 0f;
        fishBite = false;
        timerLength = Random.Range(2f, 5f);
        runTimer = true;
        fishCaught = false;
        fishReelCounter = 0;
        Debug.Log("RESET!");
        fishCaughtPopup.SetActive(false);


        CancelInvoke("moveRodBack");
        CancelInvoke("defaultRod");
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
//hide+show fish model
// change length of fishing line (OR CHEAT and make fish just move over it vertically
//if player hasnt pulled back in x seconds then reset

//add bouncy fish and slippery fish

//DUPLICATE FOR PLAYER 2 + Apply arcade control scheme