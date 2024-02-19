using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPowerUp : MonoBehaviour
{
    public int healingAmount = 10; // Total health to restore
    public float duration = 5f; // How long the healing takes
    public float healInterval = 1f; // How often to apply healing

    void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            StartCoroutine(HealOverTime(player));
            Destroy(gameObject); // Destroy the power-up object after pickup
        }
    }

    private IEnumerator HealOverTime(PlayerController player)
    {
        float elapsed = 0; // Time elapsed since the start of healing
        int appliedHealing = 0; // Healing applied so far

        while (elapsed < duration)
        {
            elapsed += healInterval;
            // Calculate healing per interval to spread it evenly
            int healThisInterval = (int)(healingAmount * (healInterval / duration));

            // Apply healing
            player.ModifyHealth(healThisInterval);
            appliedHealing += healThisInterval;

            // Wait for the next interval
            yield return new WaitForSeconds(healInterval);
        }

        // Apply any remaining healing not covered by the interval loop
        if (appliedHealing < healingAmount)
        {
            player.ModifyHealth(healingAmount - appliedHealing);
        }
    }
}