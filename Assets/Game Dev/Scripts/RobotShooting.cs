using UnityEngine;

[RequireComponent(typeof(RobotFlying))]
public class RobotShooting : MonoBehaviour
{
    public float bulletDamage = 2f;
    public float fireRate = 30f;

    public ParticleSystem muzzleFlash;
    public GameObject bulletTrailPrefab;
    public GameObject hitParticlePrefab;

    private RobotFlying flightScript;
    private Animator animator;
    private float fireTime = 0f;

    void Start()
    {
        flightScript = GetComponent<RobotFlying>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!flightScript.HasPlayer())
            return;
        animator.SetBool("Firing", flightScript.HasSpottedPlayer());
        if (flightScript.HasSpottedPlayer()) {
            if(!muzzleFlash.isPlaying) muzzleFlash.Play();
            // fire the bullets
            fireTime -= Time.deltaTime;
            if(fireTime < 0f) {
                Ray ray = new Ray(muzzleFlash.transform.position, transform.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    // damage anything we hit
                    Health health = hit.collider.GetComponentInChildren<Health>();
                    if(health) {
                        health.Hurt(bulletDamage);
                    }
                    // spawn line trail
                    GameObject trail = Instantiate(bulletTrailPrefab);
                    LineRenderer line = trail.GetComponent<LineRenderer>();
                    line.SetPosition(0, ray.origin);
                    line.SetPosition(1, hit.point);
                    Destroy(trail, 0.1f);
                    // spawn hit particles
                    GameObject hitParticles = Instantiate(hitParticlePrefab, hit.point, Quaternion.identity);
                    Destroy(hitParticles, 3f);
                }
                fireTime = 1f / fireRate;
            }
        } else if(muzzleFlash.isPlaying) {
            muzzleFlash.Stop();
        }
    }

}
