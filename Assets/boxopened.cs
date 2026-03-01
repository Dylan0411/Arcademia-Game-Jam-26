using UnityEngine;
using System.Collections;

public class BoxOpener : MonoBehaviour
{
    [Header("Interaction Settings")]
    public GameObject interactionText;
    public float interactionDistance = 3.5f; // How close the player needs to be
    public KeyCode interactionKey = KeyCode.X;

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
    public AudioSource musicSource;   // The box/scary music
    public AudioSource otherMusicSource; // The loop to stop
    public AudioClip ohNoMusic;
    public AudioClip openSound;

    private bool isOpened = false;
    private Transform playerTransform;





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
        else
        {
            //Debug.LogError("BoxOpener: No object with the tag 'Player' found in the scene!");
        }
    }

    void Update()
    {
        if (isOpened || playerTransform == null) return;

        // Calculate distance between the Box and the Player
        float dist = Vector3.Distance(transform.position, playerTransform.position);

        if (dist <= interactionDistance)
        {
            // Player is close enough
            if (interactionText != null) interactionText.SetActive(true);

            if (Input.GetKeyDown(interactionKey))
            {
                BoxOpened();
            }
        }
        else
        {
            // Player is too far away
            if (interactionText != null) interactionText.SetActive(false);
        }
    }

    public void BoxOpened()
    {
        isOpened = true;
        if (interactionText != null) interactionText.SetActive(false);

        // 1. Swap Pupils
        foreach (GameObject part in normalPupils) part.SetActive(false);
        foreach (GameObject part in deadPupils) part.SetActive(true);

        // 2. Physics: Wall cover falls
        if (wallCover != null)
        {
            Rigidbody rb = wallCover.GetComponent<Rigidbody>();
            if (rb != null) rb.isKinematic = false;
        }

        // 3. Audio Swap
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

        // 4. Start Movements and Shake
        StartCoroutine(MoveOverTime(lava.transform, lavaTargetPos));
        StartCoroutine(MoveOverTime(boxLid.transform, lidTargetPos));
        StartCoroutine(ShakeCamera());



        //////////////Dylan was here

        Invoke("openMenu", 5f);





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
        //pause time
        Time.timeScale = 0f;

        //show menu
        menu.SetActive(true);

        //hide main button
        startButton.SetActive(false);

        //show fish minigame button
        fishingButton.SetActive(true);
    }

}