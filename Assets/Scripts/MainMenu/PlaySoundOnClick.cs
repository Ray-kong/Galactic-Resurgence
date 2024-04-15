using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PlaySoundOnClick : MonoBehaviour
{
    public Button button; 
    public AudioSource click; 

    void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(PlaySound);  // Add listener to play sound on click
        }
    }

    void PlaySound()
    {
        if (click != null)
        {
            click.Play();  // Play the sound once
        }
    }
}