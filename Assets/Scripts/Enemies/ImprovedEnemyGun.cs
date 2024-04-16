using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImprovedEnemyGun : MonoBehaviour
{
    public Transform gunBarrel;
    //public float bloom;
    public int damage;
    //public float range;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip gunShotSound;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private bool bulletSpreadEnabled = true;
    [SerializeField] private Vector3 bloom = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private ParticleSystem impact;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float bulletSpeed = 100f;

    private float lastShootTime;
    
    

    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void Shoot()
    {
        if (lastShootTime + shootDelay < Time.time)
        {
            muzzleFlash.Play();
            AudioSource.PlayClipAtPoint(gunShotSound, gunBarrel.position);
            Vector3 direction = GetDirection();
            if (Physics.Raycast(gunBarrel.position, direction, out RaycastHit hit, float.MaxValue, mask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, gunBarrel.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, true));
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    playerController.ModifyHealth(-damage);
                }
                lastShootTime = Time.time;
            }
            else
            {
                TrailRenderer trail = Instantiate(bulletTrail, gunBarrel.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, gunBarrel.position + GetDirection() * 100, Vector3.zero, false));

                lastShootTime = Time.time;
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = (playerController.transform.position - transform.position).normalized;

        if (bulletSpreadEnabled)
        {
            direction += new Vector3(
                Random.Range(-bloom.x, bloom.x),
                Random.Range(-bloom.y, bloom.y),
                Random.Range(-bloom.z, bloom.z)
                );
            direction.Normalize();
        }

        return direction;
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
