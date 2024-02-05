using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PickUpPowerUp : MonoBehaviour
{
    public PowerUp powerUp;

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            powerUp.ApplyPowerUp(player);
            Destroy(gameObject); // Destroy the power-up object after pickup
        }
    }
}
