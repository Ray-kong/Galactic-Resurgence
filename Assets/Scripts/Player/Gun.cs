using UnityEngine;

public class Gun : MonoBehaviour
{
    public float range = 100f; 
    public float damage = 10f; 
    public float fireRate = 10f; // The number of shots per second

    private float nextTimeToFire = 0f; // Tracks when the player is allowed to fire again

    public Camera fpsCamera; // The camera to shoot the ray from
    public ParticleSystem muzzleFlash;
    public AudioClip gunShotSound;
    
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
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
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