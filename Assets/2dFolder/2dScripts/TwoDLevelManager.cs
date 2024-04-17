using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TwoDLevelManager : MonoBehaviour
{
    public Text gameText;
    public static bool isGameOver = false;
    public AudioClip gameoverSFX;
    public AudioClip gameWonSFX;
    public string nextLevel;
    public GameObject dialoguePanel;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelLost()
    {
        isGameOver = true;
        gameText.text = "GAME OVER!";
        gameText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(gameoverSFX, Camera.main.transform.position);
        Projectile.score = 0;
        Invoke("LoadCurrentLevel", 2);
        
    }

    public void LevelBeat()
    {
        dialoguePanel.gameObject.SetActive(true);
        isGameOver = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Projectile.score = 0;
       // gameText.text = "YOU WIN!";
      //  gameText.gameObject.SetActive(true);
        AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);

        /*
        if (!string.IsNullOrEmpty(nextLevel))
        {
            Invoke("LoadNextLevel", 2);
        }
        */
    }

    public void LoadNextLevel()
    {
        Projectile.score = 0;
        SceneManager.LoadScene(nextLevel);
    }

    void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
