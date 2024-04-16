using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class Gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float range = 100f; 
    public float damage = 10f; 
    public float fireRate = 10f; // The number of shots per second
    public int maxAmmo = 35; // Maximum ammo capacity
    public float reloadTime = 3f; // Time it takes to reload

    public LayerMask bulletLayers;

    private float nextTimeToFire; // Tracks when the player is allowed to fire again
    private int currentAmmo; // Current ammo
    private bool isReloading = false; // Is the gun currently reloading?]
    private Animator animator;

    [Header("References")]
    [SerializeField] private Camera fpsCamera; // The camera to shoot the ray from
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip gunReadySound;

    [Header("BulletTrail")] 
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private Transform gunBarrel;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;

    private float bulletSpeed = 100f;

    void Start()
    {
        currentAmmo = maxAmmo;
        reloadText.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        ammoText.text = $"{currentAmmo}/{maxAmmo}";

        if (isReloading)
            return;

        // Check if the player is pressing the reload button and the gun is not fully loaded
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            return;
        }

        if (currentAmmo == 0)
        {
            StartCoroutine(Reload());
            return;
        }

        // Check if the player is pressing the fire button and if enough time has passed based on the fire rate
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate; // Calculate the next time to fire
            Shoot();
        }
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        reloadText.gameObject.SetActive(true);
        animator.SetTrigger("Reload");

        AudioSource.PlayClipAtPoint(reloadSound, transform.position);

        // Wait for the reload time
        yield return new WaitForSeconds(reloadTime);

        AudioSource.PlayClipAtPoint(gunReadySound, transform.position);

        Debug.Log("Done!");

        animator.SetTrigger("DoneReloading");
        currentAmmo = maxAmmo;
        isReloading = false;
        reloadText.gameObject.SetActive(false);
    }

    void Shoot()
    {
        currentAmmo--;

        muzzleFlash.Play();
        AudioSource.PlayClipAtPoint(gunShotSound, transform.position);

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out RaycastHit hit, range, bulletLayers))
        {
            //Debug.Log(hit.transform.name); // Log the name of the object hit
            TrailRenderer trail = Instantiate(bulletTrail, gunBarrel.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
        else
        {
            TrailRenderer trail = Instantiate(bulletTrail, gunBarrel.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, gunBarrel.position + gunBarrel.forward * 100, Vector3.zero, false));
        }
    }
    
    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, bool madeImpact)
    {
        Vector3 startPosition = Trail.transform.position;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (remainingDistance / distance));

            remainingDistance -= bulletSpeed * Time.deltaTime;

            yield return null;
        }
        Trail.transform.position = HitPoint;
        if (madeImpact)
        {
            //Instantiate(impact, HitPoint, Quaternion.LookRotation(HitNormal));
        }

        Destroy(Trail.gameObject, Trail.time);
    }
}