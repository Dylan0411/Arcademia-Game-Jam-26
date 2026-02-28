using UnityEngine;

public class skyFish : MonoBehaviour
{
    public GameObject skyFish1;
    public GameObject skyFish2;
    public GameObject skyFish3;
    public GameObject skyFish4;
    public GameObject skyFish5;

    public float fishBodySelector;
    public float fishBehaviourSelector;

    private Rigidbody rb;
    private float nextForceTime;
    public float forceAmount = 5f;
    public float bounceForce = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        fishBodySelector = Random.Range(1f, 21f);


        if (fishBodySelector == 1)
        {
           skyFish1.SetActive(false);
           skyFish2.SetActive(false);
           skyFish3.SetActive(false); 
           skyFish4.SetActive(false); 
           skyFish5.SetActive(true);
        }

        if (fishBodySelector == 2 || fishBodySelector == 3)
        {
            skyFish1.SetActive(false);
            skyFish2.SetActive(true);
            skyFish3.SetActive(false);
            skyFish4.SetActive(false);
            skyFish5.SetActive(false);
        }
        if (fishBodySelector == 4 || fishBodySelector == 5)
        {
            skyFish1.SetActive(false);
            skyFish2.SetActive(false);
            skyFish3.SetActive(false);
            skyFish4.SetActive(true);
            skyFish5.SetActive(false);
        }
        if (fishBodySelector == 6 || fishBodySelector == 7 || fishBodySelector == 8)
        {
           skyFish1.SetActive(false);
           skyFish2.SetActive(false);
           skyFish3.SetActive(true);
           skyFish4.SetActive(false);
           skyFish5.SetActive(false);
        }
        if (fishBodySelector >= 9)
        {
           skyFish1.SetActive(true);
           skyFish2.SetActive(false);
           skyFish3.SetActive(false); 
           skyFish4.SetActive(false); 
           skyFish5.SetActive(false);
        }
        
        


        fishBehaviourSelector = Random.Range(1, 3); // gives 1 or 2
    }

    // Update is called once per frame
    void Update()
    {
        if (fishBehaviourSelector == 1)
        {
            // Remove friction
            rb.linearDamping = 0f;
            rb.angularDamping = 0f;

            // Add random force at intervals
            if (Time.time >= nextForceTime)
            {
                Vector3 randomDirection = Random.onUnitSphere;
                randomDirection.y = 0; // optional: keep horizontal

                rb.AddForce(randomDirection.normalized * forceAmount, ForceMode.Impulse);

                nextForceTime = Time.time + 1.5f;
            }
        }

        if (fishBehaviourSelector == 2)
        {
            // Remove friction
            rb.linearDamping = 0f;
            rb.angularDamping = 0f;

            // If stopped, give initial velocity
            if (rb.linearVelocity.magnitude < 0.1f)
            {
                Vector3 randomDirection = Random.onUnitSphere;
                randomDirection.y = 0;

                rb.linearVelocity = randomDirection.normalized * bounceForce;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (fishBehaviourSelector == 2)
        {
            // Reflect velocity when hitting something
            Vector3 reflectDirection = Vector3.Reflect(rb.linearVelocity.normalized, collision.contacts[0].normal);
            rb.linearVelocity = reflectDirection * bounceForce;
        }
    }
}