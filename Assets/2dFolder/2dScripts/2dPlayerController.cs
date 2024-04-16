using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TwoDPlayerController : MonoBehaviour
{
    public float hiinput;
    public float moveSpeed = 5;
    public Text scoreText;
    public Slider progressBar;
    public float targetProgress = 5;
    //   private float fillSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (progressBar.value < Projectile.score)
        {
            IncrementProgess();
        }

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

    public void IncrementProgess()
    {
        progressBar.value += 1.0f * Time.deltaTime;
    }

}
