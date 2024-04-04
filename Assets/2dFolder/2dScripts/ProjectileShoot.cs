using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public AudioClip shootSFX;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TwoDLevelManager.isGameOver == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position);
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            }
        }
    }

   
}

