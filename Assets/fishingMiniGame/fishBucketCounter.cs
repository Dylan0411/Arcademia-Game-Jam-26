using UnityEngine;
using UnityEngine.UI;



public class fishBucketCounter : MonoBehaviour
{


    public Slider fishCaughtSlider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fishCaughtSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        fishCaughtSlider.value = playerRodControl1.fishCaughtTotal + playerRodControl.fishCaughtTotal;

    }
}
