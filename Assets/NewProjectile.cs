using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class NewProjectile : MonoBehaviour
{
    //  public Text scoreText;
    public float moveSpeed;
    public static int winScore = 10;
    public static int score = 0;
    public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("bruh");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("what");
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
            score++;
            //   scoreText.text = "Score: " + score;
            //    Debug.Log(score);
            if (score >= winScore)
            {
                FindObjectOfType<TwoDLevelManager>().LevelBeat();
            }
        }

        if (collision.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
