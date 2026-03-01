using UnityEngine;
using System.Collections;

public class BoxOpener : MonoBehaviour
{
    [Header("Interaction Settings")]
    public GameObject interactionText;
    public float interactionDistance = 3.5f; // How close the player needs to be
    public KeyCode interactionKey = KeyCode.Z;
    public KeyCode interactionKey1 = KeyCode.F;

    [Header("Movement Settings")]
    public float transitionTime = 3.0f;
    public GameObject lava;
    public GameObject boxLid;
    public Vector3 lavaTargetPos;
    public Vector3 lidTargetPos;

    [Header("Camera Shake")]
    public Transform playerCamera;
    public float shakeIntensity = 0.05f;

    [Header("Pupil Toggle")]
    public GameObject[] normalPupils;
    public GameObject[] deadPupils;

    [Header("Wall Cover")]
    public GameObject wallCover;

    [Header("Audio")]
    public AudioSource musicSource;       // The box/scary music
    public AudioSource otherMusicSource;  // Optional other music
    public AudioClip ohNoMusic;
    public AudioClip openSound;

    [Header("Looping Sounds While Closed")]
    public AudioSource loopAudioSource;   // AudioSource for looping sounds while box is closed
    public AudioClip[] loopClips;         // Array of 3 different sounds
    public float delayBetweenClips = 5f;  // Delay between closed-loop sounds

    [Header("Single Open Box Sound")]
    public AudioSource singleOpenAudioSource; // AudioSource to play when box opens
    public AudioClip singleOpenClip;          // The single sound

    private bool isOpened = false;
    private Transform playerTransform;
    private Coroutine loopingCoroutine;

    [Header("UI & Buttons")]
    public GameObject fishingButton;
    public GameObject menu;
    public GameObject startButton;

    void Start()
    {
        // Find the player automatically at the start
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }

        // Start looping sounds while box is closed
        if (!isOpened && loopClips.Length > 0 && loopAudioSource != null)
        {
            loopingCoroutine = StartCoroutine(PlayLoopingSounds());
        }
    }

    void Update()
    {
        if (isOpened || playerTransform == null) return;

        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= interactionDistance)
        {
            if (interactionText != null) interactionText.SetActive(true);

            if (Input.GetKeyDown(interactionKey) || Input.GetKeyDown(interactionKey1))
            {
                BoxOpened();
            }
        }
        else
        {
            if (interactionText != null) interactionText.SetActive(false);
        }
    }

    public void BoxOpened()
    {
        isOpened = true;

        if (interactionText != null) interactionText.SetActive(false);

        // Stop closed-loop sounds
        if (loopingCoroutine != null)
        {
            StopCoroutine(loopingCoroutine);
            loopAudioSource.Stop();
        }

        // Play single sound after box opens (if menu is inactive)
        if (singleOpenAudioSource != null && singleOpenClip != null && (menu == null || !menu.activeSelf))
        {
            singleOpenAudioSource.clip = singleOpenClip;
            singleOpenAudioSource.Play();
        }

        // Swap Pupils
        foreach (GameObject part in normalPupils) part.SetActive(false);
        foreach (GameObject part in deadPupils) part.SetActive(true);

        // Wall cover physics
        if (wallCover != null)
        {
            Rigidbody rb = wallCover.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
        }

        // Audio Swap
        if (musicSource != null)
        {
            musicSource.Stop();
            musicSource.loop = false;
            musicSource.clip = ohNoMusic;
            musicSource.Play();
        }

        if (otherMusicSource != null)
        {
            otherMusicSource.loop = false;
            otherMusicSource.Stop();
        }

        AudioSource.PlayClipAtPoint(openSound, transform.position);

        // Movements and Camera Shake
        StartCoroutine(MoveOverTime(lava.transform, lavaTargetPos));
        StartCoroutine(MoveOverTime(boxLid.transform, lidTargetPos));
        StartCoroutine(ShakeCamera());

        // Open Menu after delay
        Invoke("openMenu", 10f);
    }

    IEnumerator PlayLoopingSounds()
    {
        int index = 0;
        while (!isOpened)
        {
            // Only play if menu is inactive
            if (menu != null && !menu.activeSelf)
            {
                loopAudioSource.clip = loopClips[index];
                loopAudioSource.Play();
                yield return new WaitForSeconds(loopAudioSource.clip.length + delayBetweenClips);
            }
            else
            {
                // Check again after 1 second if menu is active
                yield return new WaitForSeconds(1f);
            }

            index = (index + 1) % loopClips.Length; // Loop through the closed-loop sounds
        }
    }

    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = playerCamera.localPosition;
        float elapsed = 0;
        while (elapsed < transitionTime)
        {
            playerCamera.localPosition = originalPos + (Random.insideUnitSphere * shakeIntensity);
            elapsed += Time.deltaTime;
            yield return null;
        }
        playerCamera.localPosition = originalPos;
    }

    IEnumerator MoveOverTime(Transform obj, Vector3 target)
    {
        Vector3 start = obj.position;
        float elapsed = 0;
        while (elapsed < transitionTime)
        {
            obj.position = Vector3.Lerp(start, target, elapsed / transitionTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.position = target;
    }

    void openMenu()
    {
        Time.timeScale = 0f;
        menu.SetActive(true);
        startButton.SetActive(false);
        fishingButton.SetActive(true);
    }
}