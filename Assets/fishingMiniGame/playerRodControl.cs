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

    public AudioSource biteSplashSFX;
    public AudioSource reelSFX;

    // NEW: reel text for reeling prompt
    public GameObject reelText;

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

        fish.transform.position = fishDownPos.transform.position;
        fish.SetActive(false);

        moveRodFromBucket = false;

        elapsedTime2 = 0f;
        SplashOnce = true;
        elapsedTime3 = 0f;

        // hide reelText at start
        if (reelText != null)
            reelText.SetActive(false);
    }

    void Update()
    {
        fishingSlider.value = fishReelCounter;
        catchFishSlider.value = elapsedTime3;

        if (fishBite)
        {
            if (Input.GetKeyDown(KeyCode.S))
                biteSplashSFX.Play();

            pullBackCircle.SetActive(true);

            if (!rodPulledBack)
            {
                elapsedTime1 += Time.deltaTime;
                PullBackImage.fillAmount = 1f - (elapsedTime1 / 3f);
                if (elapsedTime1 > 3f)
                    defaultRod();
            }

            if (rodPulledBack)
                elapsedTime3 += Time.deltaTime;

            if (elapsedTime3 > 4f && !fishCaught)
            {
                defaultRod();
                fishLostPopup.SetActive(true);
                pullBackCircle.SetActive(false);
                if (reelText != null) reelText.SetActive(false);
                Invoke("hideLostFishPopupMessage", 2f);
            }
            else
            {
                CancelInvoke("hideLostFishPopupMessage");
            }
        }

        if (!fishCaught)
        {
            // Only allow pull after bite
            if (fishBite && !rodPulledBack && Input.GetKeyDown(KeyCode.S))
            {
                waitingForFishCircle.SetActive(false);
                pullBackCircle.SetActive(false);
                rodPulledBack = true;
                runTimer = false;
            }

            if (rodPulledBack && fishBite)
            {
                // show reel text while reeling
                if (reelText != null)
                    reelText.SetActive(elapsedTime3 < 4f);

                if (Input.GetKey(KeyCode.S))
                {
                    pullBackCircle.SetActive(false);
                    waitingForFishCircle.SetActive(false);
                    fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, reelFishRodRotation.transform.rotation, speed * Time.deltaTime);
                }
                else
                {
                    rodPulledBack = false;
                    waitingForFishCircle.SetActive(true);
                    pullBackCircle.SetActive(false);
                    runTimer = true;
                    if (reelText != null)
                        reelText.SetActive(false);
                }
            }
            else
            {
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * Time.deltaTime);
                if (reelText != null) reelText.SetActive(false);
            }

            if (Input.GetKeyUp(KeyCode.G) && fishBite && rodPulledBack)
                elapsedTime1 = 0f;

            if (Input.GetKeyDown(KeyCode.G) && fishBite && rodPulledBack && elapsedTime3 < 4f)
            {
                reelSFX.Play();
                fishReelCounter++;
                if (fishReelCounter >= 20)
                {
                    fishCaught = true;
                    moveRodToBucket = true;
                    fishCaughtPopup.SetActive(true);
                    if (reelText != null) reelText.SetActive(false);
                    CancelInvoke("hideLostFishPopupMessage");
                    Debug.Log("fish caught!");
                }
            }
        }

        if (fishCaught)
        {
            catchFishSlider.value = 0f;

            if (moveRodToBucket)
            {
                fish.SetActive(true);
                waitingForFishCircle.SetActive(false);
                pullBackCircle.SetActive(false);
                if (reelText != null) reelText.SetActive(false);

                fishingRod.transform.position = Vector3.MoveTowards(fishingRod.transform.position, fishCaughtLocRot.transform.position, speed * Time.deltaTime);
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, fishCaughtLocRot.transform.rotation, speed * 2f * Time.deltaTime);

                elapsedTime2 += Time.deltaTime;

                if (elapsedTime2 > 1f)
                {
                    if (SplashOnce)
                    {
                        Instantiate(waterSplash, fish.transform.position, transform.rotation);
                        SplashOnce = false;
                        fishCaughtTotal++;
                    }

                    fish.transform.position = Vector3.MoveTowards(fish.transform.position, fishDownPos.transform.position, speed / 2f * Time.deltaTime);
                }
                else
                {
                    fish.transform.position = Vector3.MoveTowards(fish.transform.position, fishUpPos.transform.position, speed * Time.deltaTime);
                }
            }

            if (moveRodFromBucket)
            {
                fish.SetActive(false);
                elapsedTime2 = 0f;

                fishingRod.transform.position = Vector3.MoveTowards(fishingRod.transform.position, defaultRodRotLoc.transform.position, speed * Time.deltaTime);
                fishingRod.transform.rotation = Quaternion.Slerp(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * 2f * Time.deltaTime);
            }

            if (doOnce)
            {
                fish.SetActive(false);
                doOnce = false;
            }

            Invoke("moveRodBack", 4f);
            Invoke("defaultRod", 6f);
        }
        else
        {
            fish.SetActive(false);
        }

        if (runTimer)
        {
            elapsedTime0 += Time.deltaTime;
            if (elapsedTime0 > timerLength)
            {
                fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, fishPullRodRotation.transform.rotation, speed * 50f * Time.deltaTime);
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
        rodPulledBack = false;
        if (reelText != null) reelText.SetActive(false);
    }

    void defaultRod()
    {
        fishingRod.transform.rotation = Quaternion.RotateTowards(fishingRod.transform.rotation, defaultRodRotLoc.transform.rotation, speed * 50f * Time.deltaTime);
        elapsedTime0 = 0f;
        fishBite = false;
        timerLength = Random.Range(2f, 5f);
        runTimer = true;
        fishCaught = false;
        fishReelCounter = 0;
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
        rodPulledBack = false;
        if (reelText != null) reelText.SetActive(false);
    }
}