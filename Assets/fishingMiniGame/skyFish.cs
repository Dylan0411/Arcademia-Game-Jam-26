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

        fishBodySelector = Random.Range(0f, 20f);


        /*

         if (fishBodySelector == between x and x).. spawn fish 1/5  (repeat for all fish with correct bodies)







         */
          
         




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