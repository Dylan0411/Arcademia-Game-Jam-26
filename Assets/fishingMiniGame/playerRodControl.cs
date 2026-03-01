
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

    public GameObject fish;
    public GameObject fishDownPos;
    public GameObject fishUpPos;    

    bool runTimer;

    public GameObject waterSplash;

    public float timerLength;
    public float elapsedTime0;
    public float elapsedTime1;

    public float savedElapsedTime;

    public bool fishBite;

    public int fishReelCounter;

    public bool fishCaught;

    public bool rodPulledBack;

    public bool moveRodToBucket;

    public Slider fishingSlider;

    public static int fishCaughtTotal;

    //public Slider fishCaughtSlider;

    public bool doOnce;

    public GameObject fishLostPopup;

    public GameObject pullBackCircle;
    public GameObject waitingForFishCircle;

    public Image PullBackImage;

    bool moveRodFromBucket;

    public float elapsedTime2;

    public bool SplashOnce;

    public Slider catchFishSlider;

    public float elapsedTime3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed = 2;
        fishingRod.transform.rotation = defaultRodRotLoc.transform.rotation;
        fishingRod.transform.position = defaultRodRotLoc.transform.position;


        elapsedTime0 = 0f;
        elapsedTime1 = 0f;

        runTimer = true;

        fishBite = false;

        timerLength = Random.Range(2f, 5f);

        fishReelCounter = 0;

        fishCaught = false;

        rodPulledBack = false;

        moveRodToBucket = false;   
    
        fishCaughtPopup.SetActive(false);
        fishLostPopup.SetActive(false);

        fishCaughtTotal = 0;
        doOnce = true;

        waitingForFishCircle.SetActive(true);
        pullBackCircle.SetActive(false);

        fish.transform.position =fishDownPos.transform.position;   
        fish.SetActive(false);

        moveRodFromBucket = false;

        elapsedTime2 = 0f;

        SplashOnce = true;

        elapsedTime3 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        fishingSlider.value = fishReelCounter;
        //fishCaughtSlider.value = fishCaughtTotal;
        catchFishSlider.value = elapsedTime3;

        if (fishBite == true)
        {
            pullBackCircle.SetActive(true);
            if (rodPulledBack == false)
            {
                elapsedTime1 += Time.deltaTime;
                PullBackImage.fillAmount = 1f - (elapsedTime1 / 3);
                if (elapsedTime1 > 3)
                {
                    defaultRod();
                }

            }

            //begin 4 second timer
            //make slider represent this value



            if (elapsedTime3 > 4 && fishCaught == false)
            {
                defaultRod();
                fishLostPopup.SetActive(true);
                pullBackCircle.SetActive (false);
                Invoke("hideLostFishPopupMessage", 2f);
                
            }
            else
            {
                CancelInvoke("hideLostFishPopupMessage");
                elapsedTime3 += Time.deltaTime;
                
            }


        }
        if (fishCaught == false)
        {
            if (Input.GetKey(KeyCode.DownArrow) && fishBite == true)
            {
                waitingForFishCircle.SetActive(false);
                pullBackCircle.SetActive(false);

                rodPulledBack = true;
                runTimer = false;
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, reelFishRodRotation.transform.rotation, speed * Time.deltaTime);
            }
            else
            {
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * Time.deltaTime);
                rodPulledBack = false;
                rodPulledBack = false;
            }

            if (Input.GetKeyUp(KeyCode.Z) && fishBite == true)
            {
                elapsedTime1 = 0;
            }

            if (Input.GetKeyDown(KeyCode.Z) && fishBite == true && rodPulledBack == true && elapsedTime3 < 4)
            {
                fishReelCounter++;
                if (fishReelCounter == 20)//press 20 times to reel fish
                {
                    fishCaught = true;
                    moveRodToBucket = true;
                    Debug.Log("fish caught!");
                    fishCaughtPopup.SetActive(true);
                    CancelInvoke("hideLostFishPopupMessage");

                }

            }
        }
        if (fishCaught == true)
        {
            catchFishSlider.value = 0;
            if (moveRodToBucket == true)
            {
                fish.SetActive(true);
                waitingForFishCircle.SetActive(false);
                pullBackCircle.SetActive(false);
                //move fish to buckets loc and rot
                fishingRod.transform.position = Vector3.MoveTowards(fishingRod.transform.position, fishCaughtLocRot.transform.position, speed * Time.deltaTime);
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, fishCaughtLocRot.transform.rotation, speed * 2 * Time.deltaTime);


                elapsedTime2 += Time.deltaTime;
                if (elapsedTime2 > 1)
                {
                    if(SplashOnce == true)
                    {
                        Instantiate(waterSplash, fish.transform.position, transform.rotation);
                        SplashOnce = false;
                        fishCaughtTotal++;
                    }
                    fish.transform.position = Vector3.MoveTowards(fish.transform.position, fishDownPos.transform.position, speed/2 * Time.deltaTime);
                }
                else
                {
                    fish.transform.position = Vector3.MoveTowards(fish.transform.position, fishUpPos.transform.position, speed * Time.deltaTime);
                }
            }
            if (moveRodFromBucket)
            {
                fish.SetActive (false);
                elapsedTime2 = 0f;

                fishingRod.transform.position = Vector3.MoveTowards(fishingRod.transform.position, defaultRodRotLoc.transform.position, speed * Time.deltaTime);
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * 2 * Time.deltaTime);



            }


            if (doOnce == true)
            {
               
                fish.SetActive(false);
                doOnce = false;

            }
            Invoke("moveRodBack", 4);
            //splash effect<<<<<<<<<<<<<<<<<<<<<<
            Invoke("defaultRod", 6);
        }
        else
        {
            fish.SetActive(false);
        }



        if (runTimer == true) // SET TRUE WHEN FISH IS IN BUCKET OR FISH LOST
        {
            elapsedTime0 += Time.deltaTime;
            if (elapsedTime0 > timerLength)
            {
                fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, fishPullRodRotation.transform.rotation, speed * 50 * Time.deltaTime);

                fishBite = true;
                Debug.Log("fish bite!");


            }
        }







    }

    void hideLostFishPopupMessage()
    {
        fishLostPopup.SetActive(false);
    }
    void fishCaughtFunc()
    {
        fishCaughtTotal++;
    }
     void moveRodBack()
    {
        moveRodToBucket = false;
        moveRodFromBucket = true;
        pullBackCircle.SetActive(false);
        fishBite = false;
    }

    void defaultRod()
    {
        //make this trigger only once
        fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * 50 * Time.deltaTime);
        elapsedTime0 = 0f;
        fishBite = false;
        timerLength = Random.Range(2f, 5f);
        runTimer = true;
        fishCaught = false;
        fishReelCounter = 0;
        Debug.Log("RESET!");
        fishCaughtPopup.SetActive(false);
        doOnce = true;
        elapsedTime1 = 0f;

        CancelInvoke("moveRodBack");
        CancelInvoke("defaultRod");

        pullBackCircle.SetActive(false);
        waitingForFishCircle.SetActive(true);
        moveRodFromBucket = false;

        fish.transform.position = fishDownPos.transform.position;

        SplashOnce = true;
        elapsedTime3 = 0f;
        fishLostPopup.SetActive(false);

    }

}


