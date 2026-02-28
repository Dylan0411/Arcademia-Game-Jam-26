using UnityEngine;
using System.Collections;

public class BoxOpener : MonoBehaviour
{
    [Header("Interaction Settings")]
    public GameObject interactionText;
    public KeyCode interactionKey = KeyCode.X;

    [Header("Movement Settings")]
    public float transitionTime = 3.0f;
    public GameObject lava;
    public GameObject boxLid;
    public Vector3 lavaTargetPos;
    public Vector3 lidTargetPos;

    [Header("Camera Shake")]
    public Transform playerCamera;   // Drag your Main Camera here
    public float shakeIntensity = 0.05f;

    [Header("Pupil Toggle")]
    public GameObject[] normalPupils;
    public GameObject[] deadPupils;

    [Header("Wall Cover")]
    public GameObject wallCover;

    [Header("External Audio")]
    public AudioSource otherMusicSource;


    [Header("Audio")]
    public AudioSource musicSource;
    public AudioClip ohNoMusic;
    public AudioClip openSound;

    private bool isOpened = false;

    void Start()
    {
        // THE MOMENT THE GAME STARTS, RUN THE EVENT
        Debug.Log("Game Started: Triggering Box Event...");
        BoxOpened();
    }

    public void BoxOpened()
    {
        if (isOpened) return;
        isOpened = true;

        if (interactionText != null) interactionText.SetActive(false);

        // 1. Swap Pupils
        foreach (GameObject part in normalPupils) part.SetActive(false);
        foreach (GameObject part in deadPupils) part.SetActive(true);

        // 2. Physics: Wall cover falls
        Rigidbody rb = wallCover.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = false;

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
            otherMusicSource.Stop();
            otherMusicSource.loop = false;
        }
        AudioSource.PlayClipAtPoint(openSound, transform.position);

        // 4. Start Movements AND Camera Shake
        StartCoroutine(MoveOverTime(lava.transform, lavaTargetPos));
        StartCoroutine(MoveOverTime(boxLid.transform, lidTargetPos));
        StartCoroutine(ShakeCamera());
    }

    IEnumerator ShakeCamera()
    {
        Vector3 originalPos = playerCamera.localPosition;
        float elapsed = 0;

        while (elapsed < transitionTime)
        {
            // Calculate a random tiny offset
            float x = Random.Range(-1f, 1f) * shakeIntensity;
            float y = Random.Range(-1f, 1f) * shakeIntensity;

            playerCamera.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Return camera to normal
        playerCamera.localPosition = originalPos;
    }



    IEnumerator MoveOverTime(Transform objTransform, Vector3 target)
    {
        Vector3 startPos = objTransform.position;
        float elapsed = 0;

        while (elapsed < transitionTime)
        {
            objTransform.position = Vector3.Lerp(startPos, target, elapsed / transitionTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        objTransform.position = target;
    }
}