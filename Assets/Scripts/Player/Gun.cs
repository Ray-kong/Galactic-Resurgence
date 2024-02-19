using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float range = 100f; 
    public float damage = 10f; 
    public float fireRate = 10f; // The number of shots per second

    private float nextTimeToFire; // Tracks when the player is allowed to fire again

    [Header("References")]
    [SerializeField] private Camera fpsCamera; // The camera to shoot the ray from
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip gunShotSound;
    
    void Update()
    {
        // Check if the player is pressing the fire button and if enough time has passed based on the fire rate
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; // Calculate the next time to fire
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        AudioSource.PlayClipAtPoint(gunShotSound, transform.position);

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, range))
        {
            Debug.Log(hit.transform.name); // Log the name of the object hit

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}