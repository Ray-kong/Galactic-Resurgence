using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp", order = 51)]
public class PowerUp : ScriptableObject
{
    public string name;

    public void ApplyPowerUp(PlayerController player, float multiplier, float duration)
    {
        if (name == "Speed")
        {
            player.StartCoroutine(player.AdjustSpeed(multiplier, duration));
        }
        else if (name == "Jump")
        {
            player.StartCoroutine(player.AdjustJumpForce(multiplier, duration));
        }
        else if (name == "Gun")
        {
            player.StartCoroutine(player.AdjustDamage(multiplier, duration));
        }
        else
        {
            Debug.Log("No Power Up Assigned!");
        }
    }
}
