using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLevel1 : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerInventory.HasKeyLevel1)
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }
}