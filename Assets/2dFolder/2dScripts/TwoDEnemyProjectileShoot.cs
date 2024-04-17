using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDEnemyProjectileShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public AudioClip shootSFX;
    public float Timer = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TwoDLevelManager.isGameOver == false)
        {
            Timer -= 1 * Time.deltaTime;
            if (Timer <= 0)
            {
                AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position);
                Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                Timer = 2;
            }
        }
    
    }
}
