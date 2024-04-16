using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTrigger : MonoBehaviour
{
    private bool hasPlayed = false;
    public AudioClip clip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayed)
        {
            other.gameObject.GetComponentInChildren<PlayVoice>().Play(clip);
            hasPlayed = true;
        }
    }
}
