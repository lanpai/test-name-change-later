using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject projectile;
    public Transform projLocation;
    public ParticleSystem muzzleFlash;

    private float nextFireTimestamp;

    public float FIRE_ANGLE;
    public float SHOT_COUNT;
    public float RELOAD_COOLDOWN = 2f;
    public float KICKBACK_FORCE = 100000f;
    public float TORQUE = 300000f;

    public string KEY_FIRE;
    public string KEY_CCW;
    public string KEY_CW;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - nextFireTimestamp >= 0) {

            if (Input.GetKeyDown(KEY_FIRE)) {
                muzzleFlash.Emit(10);
                nextFireTimestamp = Time.time + RELOAD_COOLDOWN;

                // APPLY BACKWARDS FORCE
                rb.AddForce(transform.right * -1 * KICKBACK_FORCE);

                // FIRE PROJECTILE AND PARTICLES
                for (int i = 0; i < SHOT_COUNT; i++) {
                    GameObject bullet = Instantiate(projectile, projLocation.position, Quaternion.identity);
                    bullet.SetActive(true);
                    Rigidbody bulletRb = bullet.GetComponent(typeof(Rigidbody)) as Rigidbody;
                    bulletRb.velocity =
                        Quaternion.AngleAxis(
                            Random.Range(-1 * FIRE_ANGLE, FIRE_ANGLE),
                            Vector3.forward)
                        * transform.right * 800f;
                }
            }
        }

        bool ccw = Input.GetKeyDown(KEY_CCW);
        bool cw = Input.GetKeyDown(KEY_CW);
        float torque = 0;
        if (!(ccw && cw) && (ccw || cw)) {
            torque = TORQUE;
            if (cw) {
                // TURN CLOCKWISE
                torque *= -1;
            }
        }
        rb.AddTorque(transform.forward * torque);
    }
}
