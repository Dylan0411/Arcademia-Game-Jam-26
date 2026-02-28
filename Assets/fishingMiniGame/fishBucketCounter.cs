using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class fishBucketCounter : MonoBehaviour
{


    public Slider fishCaughtSlider;
    public GameObject fishingUI;

    public playerRodControl1 pRodScript1;
    public playerRodControl pRodScript;

    public GameObject relic;
    public GameObject relicStartPos;
    public GameObject relicEndPos;

    public GameObject splashEffect;

    public GameObject Ice;

    public GameObject cursedLootUI;

    public GameObject fishMovingUp;
    public GameObject fishMovingUpStartPos;
    public GameObject fishMovingUpEndPos;

    public GameObject skyFish;

    public GameObject skyFishSpawnLocation1;
    public GameObject skyFishSpawnLocation2;
    public GameObject skyFishSpawnLocation3;
    public GameObject skyFishSpawnLocation4;
    public GameObject skyFishSpawnLocation5;
    public GameObject skyFishSpawnLocation6;
    public GameObject skyFishSpawnLocation7;
    public GameObject skyFishSpawnLocation8;

    public bool fadeScreen;

    public float elapsedTime;

    public bool doOnce;
    public bool doOnce1;
    public bool doOnce2;


    public GameObject pRods;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ice.SetActive(false);
        fishCaughtSlider.value = 0;
        pRodScript.enabled = true;
        pRodScript.enabled = true;
        relic.transform.position = relicStartPos.transform.position;
        cursedLootUI.SetActive(false);

        fishMovingUp.transform.position = fishMovingUpStartPos.transform.position;
        fadeScreen = false;

        elapsedTime = 0;

        doOnce = true;
        doOnce1 = true;
        doOnce2 = true;

        
        relic.GetComponent<Rigidbody>().isKinematic = true;

        relic.SetActive(false);

        pRods.SetActive(true);


    }

    // Update is called once per frame
    void Update()
    {

        if(fadeScreen == true)
        {
            ///<<<<<<<<<<<<<<<<<<<<<fade screen to black>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            ///

            //load the rythm game scene when fully black for 2 seconds via an invoke.
            Invoke("sceneChange", 5f);
        }

        fishCaughtSlider.value = playerRodControl1.fishCaughtTotal + playerRodControl.fishCaughtTotal;
        if (playerRodControl1.fishCaughtTotal + playerRodControl.fishCaughtTotal > 10)
        {

            //disable both fishing scripts + ui disappears
            pRodScript.enabled = false;
            pRodScript1.enabled = false;
            fishingUI.SetActive(false);
            relic.SetActive(true);
            //relic comes out of fish bucket w/ splash effect
            relic.transform.position = Vector3.MoveTowards(relic.transform.position, relicEndPos.transform.position, 5 * Time.deltaTime);
            relic.transform.Rotate(0, 5 * Time.deltaTime, 0);
            relic.GetComponent<Rigidbody>().isKinematic = false;

            if (doOnce == true)
            {
                Instantiate(splashEffect);
                doOnce = false;
            }

            //hideRods
            pRods.SetActive(false);



            ///////WAIT 3 SECONDS//////

            if (elapsedTime > 3)
            {


                // "cursed loot gained" message as new ui message
                if (doOnce1 == true)
                {
                    cursedLootUI.SetActive(true);
                    doOnce1 = false;
                }
                Invoke("hideCursedLootPopUp", 5);

                // loads of fish come out of water into the air
                fishMovingUp.transform.position = Vector3.MoveTowards(fishMovingUp.transform.position, fishMovingUpEndPos.transform.position, 5 * Time.deltaTime);


                // water freezes over into ice
                Ice.SetActive(true); //<<<<<<<<DONT FORGET TO MAKE SLIPPY/BOUNCY

                if (doOnce2 == true)
                {
                    //fish fall from sky onto ice (instantiate loads)
                    InstantiateFish();
                    Invoke("InstantiateFish", 1f);
                    Invoke("InstantiateFish", 2f);
                    Invoke("InstantiateFish", 3f);
                    Invoke("InstantiateFish", 4f);
                    Invoke("InstantiateFish", 5f);
                    Invoke("InstantiateFish", 6f);
                    Invoke("InstantiateFish", 7f);
                    Invoke("InstantiateFish", 8f);
                    Invoke("InstantiateFish", 9f);

                    //fade screen to black
                    Invoke("fadeToBlack", 20f);
                    doOnce2 = false;
                }
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }

         

        }
    }



    public void hideCursedLootPopUp()
    {
        cursedLootUI.SetActive(false);
    }

    public void InstantiateFish()
    {
        Instantiate(skyFish, skyFishSpawnLocation1.transform.position, skyFishSpawnLocation1.transform.rotation);
        Instantiate(skyFish, skyFishSpawnLocation2.transform.position, skyFishSpawnLocation2.transform.rotation);
        Instantiate(skyFish, skyFishSpawnLocation3.transform.position, skyFishSpawnLocation3.transform.rotation);
        Instantiate(skyFish, skyFishSpawnLocation4.transform.position, skyFishSpawnLocation4.transform.rotation);
        Instantiate(skyFish, skyFishSpawnLocation5.transform.position, skyFishSpawnLocation5.transform.rotation);
        Instantiate(skyFish, skyFishSpawnLocation6.transform.position, skyFishSpawnLocation6.transform.rotation);
        Instantiate(skyFish, skyFishSpawnLocation7.transform.position, skyFishSpawnLocation7.transform.rotation);
        Instantiate(skyFish, skyFishSpawnLocation8.transform.position, skyFishSpawnLocation8.transform.rotation);
    }

    public void fadeToBlack()
    {

        fadeScreen = true;

    }


    public void sceneChange()
    {
        SceneManager.LoadScene("CarpHero");

    }



}
