using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUp", order = 51)]
public class PowerUp : ScriptableObject
{
    public string name;
    public float multiplier;

    public void ApplyPowerUp(PlayerController player)
    {
        if (name == "Speed")
        {
            player.speed *= multiplier;
        } 
        else if (name == "Jump")
        {
            player.jumpForce *= multiplier;
        }
        else
        {
            // Do nothing
            Debug.Log("No Power Up Assigned!");
        }
    }
}
