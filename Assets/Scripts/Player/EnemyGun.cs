using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float range = 100f;
    public float damage = 10f;
    public float fireRate = 10f; // The number of shots per second

    private float nextTimeToFire; // Tracks when the player is allowed to fire again

    [Header("References")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private LayerMask playerMask;

    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void EnemyShoot()
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; // Calculate the next time to fire
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        AudioSource.PlayClipAtPoint(gunShotSound, transform.position);

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, range))
        {
            Debug.Log(hit.transform.name); // Log the name of the object hit
            if (hit.transform.CompareTag("Player"))
            {
                playerController.ModifyHealth((int) -damage);
            }
        }
    }
}
