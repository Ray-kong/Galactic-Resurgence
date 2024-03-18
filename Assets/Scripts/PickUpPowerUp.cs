using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPowerUp : MonoBehaviour
{
    public PowerUp powerUp;
    public float multiplier = 2f;
    public float duration = 4f; // Duration in seconds
    public AudioClip pickupSound;

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            powerUp.ApplyPowerUp(player, multiplier, duration);
            Destroy(gameObject); // Destroy the power-up object after pickup
        }
    }
}