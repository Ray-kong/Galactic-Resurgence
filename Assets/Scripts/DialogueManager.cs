using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public AudioClip dialogueAudio;
    private AudioSource audioSource;

    public PlayerController playerController;
    public Gun gun;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = dialogueAudio;
        audioSource.playOnAwake = false;
        StartDialogue();
    }

    void StartDialogue()
    {
        playerController.enabled = false;
        gun.enabled = false;
        audioSource.Play();
        StartCoroutine(WaitForDialogueToEnd());

    }

    IEnumerator WaitForDialogueToEnd()
    {
        while (audioSource.isPlaying)
        {
            yield return null; 
        }

        playerController.enabled = true;
        gun.enabled = true;
    }
}