using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{
    public Rigidbody rb;

    private float spawnTimestamp;
    private Vector3 prevVel;
    private int reflectCount = 0;

    public int REFLECT_MAX = 2;
    public float LIFETIME_MAX = 2;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimestamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - spawnTimestamp > LIFETIME_MAX)
            Destroy(gameObject);
    }

    void FixedUpdate() {
        prevVel = rb.velocity;
    }

    void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts[0];
        Vector3 reflectedVel = Vector3.Reflect(prevVel, contact.normal);
        rb.velocity = reflectedVel;

        reflectCount++;
        if (reflectCount >= REFLECT_MAX) Destroy(gameObject);
    }
}
