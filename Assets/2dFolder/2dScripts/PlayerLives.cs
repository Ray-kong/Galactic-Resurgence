using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerLives : MonoBehaviour
{
    public int lives = 3;
    public Image[] livesUI;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("triggered");
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyProjectile")
        {
            Destroy(collision.gameObject);
            lives -= 1;
            for (int i = 0; i < livesUI.Length; i++)
            {
                if (i < lives)
                {
                    livesUI[i].enabled = true;
                }
                else
                {
                    livesUI[i].enabled = false;
                }
            }

            if (lives <= 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                FindObjectOfType<TwoDLevelManager>().LevelLost();
            }

        }
    }
}
