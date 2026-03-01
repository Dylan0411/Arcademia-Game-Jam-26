using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class fishBucketCounter : MonoBehaviour
{
    public Image targetImage;
    public float fadeDuration = 2f;

    private bool isFading = false;

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
    public float elapsedTime1;

    public bool doOnce;
    public bool doOnce1;
    public bool doOnce2;

    public GameObject pRods;

    public GameObject tutorial;

    // --- Audio additions ---
    public AudioSource relicAudioSource;   // AudioSource for relicSFX
    public AudioClip relicSFX;

    public AudioSource loopAudioSource;    // Separate AudioSource for looping tracks
    public AudioClip[] loopTracks;         // Array of 3 clips to loop

    private bool audioStarted = false;     // Ensure we trigger audio only once

    void Start()
    {
        StartCoroutine(StartTutorial());

        Ice.SetActive(false);
        fishCaughtSlider.value = 0;
        pRodScript.enabled = true;
        pRodScript1.enabled = true;
        relic.transform.position = relicStartPos.transform.position;
        cursedLootUI.SetActive(false);

        fadeScreen = false;

        elapsedTime = 0;
        elapsedTime1 = 0;

        doOnce = true;
        doOnce1 = true;
        doOnce2 = true;

        relic.GetComponent<Rigidbody>().isKinematic = true;
        relic.SetActive(false);
        pRods.SetActive(true);
    }

    IEnumerator StartTutorial()
    {
        Time.timeScale = 0f;
        tutorial.SetActive(true);

        yield return new WaitForSecondsRealtime(10f);

        tutorial.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (fadeScreen && !isFading)
        {
            isFading = true;
            StartCoroutine(FadeToBlackAndLoad());
        }

        fishCaughtSlider.value = playerRodControl1.fishCaughtTotal + playerRodControl.fishCaughtTotal;

        if (playerRodControl1.fishCaughtTotal + playerRodControl.fishCaughtTotal > 10)
        {
            pRodScript.enabled = false;
            pRodScript1.enabled = false;
            fishingUI.SetActive(false);
            relic.SetActive(true);

            relic.transform.position = Vector3.MoveTowards(
                relic.transform.position,
                relicEndPos.transform.position,
                5 * Time.deltaTime);

            if (elapsedTime1 > 3)
            {
                relic.transform.Rotate(0, 5 * Time.deltaTime, 0);
            }
            else
            {
                elapsedTime1 += Time.deltaTime;
            }

            relic.GetComponent<Rigidbody>().isKinematic = false;

            if (doOnce == true)
            {
                Instantiate(splashEffect);
                doOnce = false;
            }

            pRods.SetActive(false);

            // --- Play relic audio once and start looping tracks ---
            if (!audioStarted)
            {
                audioStarted = true;

                // Play the one-time relic SFX immediately
                if (relicAudioSource != null && relicSFX != null)
                {
                    relicAudioSource.PlayOneShot(relicSFX);
                }

                // Start the coroutine to play loop tracks after 5 seconds
                if (loopAudioSource != null && loopTracks.Length > 0)
                {
                    StartCoroutine(PlayLoopTracksWithDelay(6f));
                }
            }

            if (elapsedTime > 3)
            {
                if (doOnce1 == true)
                {
                    cursedLootUI.SetActive(true);
                    doOnce1 = false;
                }

                Invoke("hideCursedLootPopUp", 5);

                Ice.SetActive(true);

                if (doOnce2 == true)
                {
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

                    Invoke("fadeToBlack", 15f);
                    doOnce2 = false;
                }
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }
        }
    }

    // --- Coroutine for looping audio tracks ---
    private IEnumerator PlayLoopTracksWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            for (int i = 0; i < loopTracks.Length; i++)
            {
                loopAudioSource.clip = loopTracks[i];
                loopAudioSource.Play();

                yield return new WaitForSeconds(loopAudioSource.clip.length);
                yield return new WaitForSeconds(4f); // 5-second gap between tracks
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

    private IEnumerator FadeToBlackAndLoad()
    {
        yield return StartCoroutine(FadeImage(0f, 1f));

        yield return new WaitForSecondsRealtime(2f);

        SceneManager.LoadScene("CarpHero");
    }

    private IEnumerator FadeImage(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        Color color = targetImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            targetImage.color = color;
            yield return null;
        }

        color.a = endAlpha;
        targetImage.color = color;
    }
}