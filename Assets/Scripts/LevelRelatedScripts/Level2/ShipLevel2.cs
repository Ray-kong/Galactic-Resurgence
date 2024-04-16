using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLevel2 : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerInventory.HasKey)
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }
}