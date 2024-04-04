using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;
    private float spawnRate = 3.5f;
    public float spawnTime = 1.0f;
    public float spawnnumer = 5;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemies", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
    }


  
    void SpawnEnemies()
    {
        if (TwoDLevelManager.isGameOver == false)
        {
            for (int i = 0; i < spawnnumer; i++)
            {

                GameObject spawnedEnemy = Instantiate(enemyPrefab, new Vector3(Random.Range(-5f, 5), 3.7f, 0), transform.rotation)
                    as GameObject;

                spawnedEnemy.transform.parent = gameObject.transform;
            }
        }

    }
}
