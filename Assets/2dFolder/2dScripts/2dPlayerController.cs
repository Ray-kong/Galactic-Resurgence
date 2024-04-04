using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoDPlayerController : MonoBehaviour
{
    public float hiinput;
    public float moveSpeed = 5;
    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        scoreText.text = "Score: " + Projectile.score;

        if (TwoDLevelManager.isGameOver == false)
        {
            hiinput = Input.GetAxisRaw("Horizontal") * -1;

            transform.Translate(Vector2.right * hiinput * moveSpeed * Time.deltaTime);
        }
        else
        { 
            transform.Translate(Vector2.up * -1 * moveSpeed * Time.deltaTime);
        }
    }
}
