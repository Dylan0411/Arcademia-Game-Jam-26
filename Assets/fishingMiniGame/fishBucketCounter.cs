using UnityEngine;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Ice.SetActive(false);
        fishCaughtSlider.value = 0;
        pRodScript.enabled = true;
        pRodScript.enabled = true;
        relic.transform.position = relicStartPos.transform.position;
        cursedLootUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        fishCaughtSlider.value = playerRodControl1.fishCaughtTotal + playerRodControl.fishCaughtTotal;
        if (playerRodControl1.fishCaughtTotal + playerRodControl.fishCaughtTotal > 10)
        {
            //disable both fishing scripts + ui disappears
            pRodScript.enabled = false;
            pRodScript1.enabled = false;
            fishingUI.SetActive(false);
            //relic comes out of fish bucket w/ splash effect
            relic.transform.position = Vector3.MoveTowards(relic.transform.position, relicEndPos.transform.position, 5 * Time.deltaTime);
            relic.transform.Rotate(0, 5*Time.deltaTime, 0);
            Instantiate(splashEffect);


            ///////WAIT 3 SECONDS//////


            // "cursed loot gained" message as new ui message
            cursedLootUI.SetActive(true);
            Invoke("hideCursedLootPopUp", 5);

            // loads of fish come out of water into the air


            // water freezes over into ice
            Ice.SetActive(true); //<<<<<<<<DONT FORGET TO MAKE SLIPPY/BOUNCY


            //fish fall from sky onto ice (instantiate loads)


            //on fish spawn they either addforce with 0 friction OR bounce 

        }
    }



    public void hideCursedLootPopUp()
    {
        cursedLootUI.SetActive(false);
    }

}
