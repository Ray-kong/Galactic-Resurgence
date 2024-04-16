using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
    public GameObject Enemies1;
    public GameObject Enemies2;
    public AudioSource item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            item.Play();
            PlayerInventory.HasKey = true; 
            gameObject.SetActive(false);

            Enemies1.SetActive(true);
            Enemies2.SetActive(true);

        }
    }
}
